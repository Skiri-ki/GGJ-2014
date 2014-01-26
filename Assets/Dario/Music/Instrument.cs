using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;
public class Instrument : MonoBehaviour {
	public AnimationCurve Falloff;

	public int BPM = 20;
	public int Measures = 4;
	public int Speed = 1;
	public int Offset = 0;
	public int LowLevel = 1;
	public int HighLevel = 6;
	public float Volume = 0.75f;
	public float Vibrato = 0.0f;
	public bool FixedSpeed = false;
	public int MinSpeed = 1;
	public int MaxSpeed = 1;
	
	public static readonly List<int> TRANSPOSE_STEPS = new List<int>() { 0, 3, 8 };

	float currentBar = 0;

	WaveGenerator generator;
	MelodyMachine machine;
	bool hasGenerator = false;
	System.Random rand = new System.Random();

	bool isDrum;
	bool wasInitialized = false;

	public void Init(List<Note> _scale) {
		Reset();
		wasInitialized = true;
		generator = GetComponent<WaveGenerator>();
		hasGenerator = generator;
		isDrum = GetComponent<NoiseGenerator>();
		
		if(hasGenerator) {
			uint octave = (uint)RandomRange(LowLevel, HighLevel);
			List<MelodyTansitionInfo> transitions = new List<MelodyTansitionInfo>();

			for(int i = 0; i < _scale.Count; i++) {
				transitions.Add( new MelodyTansitionInfo(i, Random.Range(0, _scale.Count), 1));
			}

			int transitionCount = Random.Range(7, 15);
			for(int i = 0; i < transitionCount; i++) {
				transitions.Add( new MelodyTansitionInfo( 
				                        Random.Range(0, _scale.Count), 
				                        Random .Range(0, _scale.Count), 
				                        Random.value * 2));

			}

			machine = new MelodyMachine(_scale, transitions);
			machine.SetOctave(octave);
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

	public void Reset() {
		time = 0;
		currentBar = 0;
	}

	double time;
	private double sampling_frequency = 48000;
	int currentNoteMeasure = 2;
	double baseFrequence;

	void OnAudioFilterRead(float[] data, int channels)
	{
		if(!wasInitialized)
			return;

		// update increment in case frequency has changed
		double increment = 1.0f / sampling_frequency;

		if(machine != null)
			generator.frequency = machine.CurrentFrequency; 

		for (var i = 0; i < data.Length; i = i + channels)
		{
			time = time + increment;
			// this is where we copy audio data to make them “available” to Unity
			float val = Falloff.Evaluate((float)time);
			data[i] *= val * Volume + Vibrato * (float)Math.Sin(time*15);;
			// if we have stereo, we copy the mono data to each channel
			if (channels == 2) data[i + 1] *= val * Volume;

			if (time > 60.0/(BPM *currentNoteMeasure)) {
				time -= 60.0/(BPM *currentNoteMeasure);
				currentBar = (currentBar + 1.0f/currentNoteMeasure) % Measures;
				currentNoteMeasure = 2 * Speed;
				if(hasGenerator) {
					machine.Next();
					if(!isDrum){
						currentNoteMeasure = GetRandomMeasure();
					}
					if((int)currentBar == 0) {
						machine.Transpose(GetRandomTranspose());
					}
					Volume = (float)RandomRange(5, 10) / 10;
					if(rand.NextDouble() < 0.2f && !FixedSpeed) {
						Speed = (int)Math.Pow(2, RandomRange(MinSpeed, MaxSpeed));
					}
				}
			}
		}
	}

}
