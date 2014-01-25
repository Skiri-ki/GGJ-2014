using UnityEngine;
using System;
using System.Collections.Generic;

public class Instrument : MonoBehaviour {
	public AnimationCurve Falloff;

	public static int BPM = 50;
	public static int Measures = 4;
	public int Speed = 1;

	public static readonly List<int> TRANSPOSE_STEPS = new List<int>() { 0, 2, 4, 5, -1, -2, -4 };

	float currentBar = 0;

	WaveGenerator generator;
	MelodyMachine machine;
	bool hasGenerator = false;
	System.Random rand = new System.Random();

	bool isDrum;

	void Awake() {
		generator = GetComponent<WaveGenerator>();
		hasGenerator = generator;
		isDrum = GetComponent<NoiseGenerator>();

		if(hasGenerator) {
			uint octave = (uint)RandomRange(2, 5);
			machine = new MelodyMachine( 
				new List<Note>() { 
					new Note( NoteName.C, octave), 
					new Note( NoteName.D, octave), 
					new Note( NoteName.E, octave), 
					new Note( NoteName.F, octave), 
					new Note( NoteName.G, octave), 
					new Note( NoteName.A, octave), 
					new Note( NoteName.B, octave)},
				new List<MelodyTansitionInfo>() {
					new MelodyTansitionInfo(0, 1, 1),
					new MelodyTansitionInfo(1, 2, 1),
					new MelodyTansitionInfo(1, 5, 1),
					new MelodyTansitionInfo(2, 3, 1),
					new MelodyTansitionInfo(2, 2, 1),
					new MelodyTansitionInfo(3, 4, 1),
					new MelodyTansitionInfo(3, 2, 1),
					new MelodyTansitionInfo(4, 5, 1),
					new MelodyTansitionInfo(4, 1, 1),
					new MelodyTansitionInfo(5, 6, 1),
					new MelodyTansitionInfo(5, 3, 1),
					new MelodyTansitionInfo(6, 3, 1),
					new MelodyTansitionInfo(5, 2, 1),
					new MelodyTansitionInfo(6, 0, 1),
			});
		}
	}

	int RandomRange(int _min, int _max) {
		return (int)( rand.NextDouble() * (_max - _min) - 0.001f ) + _min;
	}

	int GetRandomMeasure() {
		return (int)Math.Pow(2, RandomRange(1, 3)) * Speed;
	}

	int GetRandomTranspose() {
		return TRANSPOSE_STEPS[RandomRange(0, TRANSPOSE_STEPS.Count)];
	}

	double time;
	private double sampling_frequency = 48000;
	int currentNoteMeasure = 2;

	void OnAudioFilterRead(float[] data, int channels)
	{
		// update increment in case frequency has changed
		double increment = 1.0f / sampling_frequency;
		for (var i = 0; i < data.Length; i = i + channels)
		{
			time = time + increment;
			// this is where we copy audio data to make them “available” to Unity
			float val = Falloff.Evaluate((float)time);
			data[i] *= val;
			// if we have stereo, we copy the mono data to each channel
			if (channels == 2) data[i + 1] *= val;
			if (time > 60.0/(BPM *currentNoteMeasure)) {
				time -= 60.0/(BPM *currentNoteMeasure);
				currentBar = (currentBar + 1.0f/currentNoteMeasure) % Measures;
				
				if(hasGenerator) {
					machine.Next();
					generator.frequency = machine.CurrentFrequency;
					if(!isDrum)
						currentNoteMeasure = GetRandomMeasure();

					if((int)currentBar == 0) {
						machine.Transpose(GetRandomTranspose());
					}
				}
			}
		}
	}

}
