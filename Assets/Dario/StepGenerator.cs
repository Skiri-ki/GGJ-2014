using UnityEngine;
using System;

public class StepGenerator : MonoBehaviour
{
	// un-optimized version
	public double frequency = 200;
	public double gain = 0.05;
	
	private double increment;
	private double phase;
	private double sampling_frequency = 48000;
	
	void OnAudioFilterRead(float[] data, int channels)
	{
		// update increment in case frequency has changed
		increment = frequency * 2 * Math.PI / sampling_frequency;
		for (var i = 0; i < data.Length; i = i + channels)
		{
			phase = phase + increment;
			// this is where we copy audio data to make them “available” to Unity
			data[i] = (float)(gain*phase) - 1;
			// if we have stereo, we copy the mono data to each channel
			if (channels == 2) data[i + 1] = (float)(gain*phase) - 1;
			if (phase > 2 * Math.PI) phase = 0;
		}
	}
} 