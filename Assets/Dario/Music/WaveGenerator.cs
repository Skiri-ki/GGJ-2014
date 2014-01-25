using UnityEngine;
using System;

public enum WaveType
{
	Sin,
	Triangle,
	Step
}

public class WaveGenerator : MonoBehaviour
{
	public WaveType Type;
	// un-optimized version
	public double frequency = 200;
	public double gain = 0.05;
	
	private double increment;
	private double phase;
	private double sampling_frequency = 48000;

	double GetSinVal(){
		return Math.Sin(phase);
	}

	double GetStepval() {
		return GetSinVal() > 0 ? 1 : -1;
	}

	double GetTriangleVal() {
		return (phase/Mathf.PI) - 1;
	}

	double GetVal() {
		switch(Type) {
		case WaveType.Sin: return GetSinVal();
		case WaveType.Step: return GetStepval();
		case WaveType.Triangle: return GetTriangleVal();
		}
		return 0;
	}
	
	void OnAudioFilterRead(float[] data, int channels)
	{
		// update increment in case frequency has changed
		increment = frequency * 2 * Math.PI / sampling_frequency;
		for (var i = 0; i < data.Length; i = i + channels)
		{
			phase = phase + increment;
			// this is where we copy audio data to make them “available” to Unity
			float val = (float)(gain* GetVal() );
			data[i] *= val;
			// if we have stereo, we copy the mono data to each channel
			if (channels == 2) data[i + 1] *= val;
			if (phase > 2 * Math.PI) phase = 0;
		}
	}
} 