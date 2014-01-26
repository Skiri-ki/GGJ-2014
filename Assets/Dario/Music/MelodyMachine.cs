using System;
using UnityEngine;
using System.Collections.Generic;

using Random = UnityEngine.Random;

public enum NoteName {
	C,
	CSharp,
	D,
	DSharp,
	E,
	F,
	FSharp,
	G,
	GSharp,
	A,
	ASharp,
	B,
}

public class Note {
	public int Index { get { return (int)(((int)NoteName + 1)  + 7 * ( Octave + 1)) + 3; } }
	public NoteName NoteName;
	public uint Octave;

	public double GetFrequency(int _tansposeSteps = 0) {
		int index = Index + _tansposeSteps;

		if(index < 0)
			index = 0;

		return 440 * Math.Pow(2.0, (double)(index - 49)/12);
	}

	public Note( NoteName _name, uint _octave) {
		NoteName = _name;
		Octave = _octave;
	}

	public override string ToString ()
	{
		return string.Format ("[Note: {0}|{1}]", NoteName, Octave);
	}
}

public struct MelodyTansitionInfo {
	public int A;
	public int B;
	public float Probability;

	public MelodyTansitionInfo(int _a, int _b, float _p) {
		A = _a;
		B = _b;
		Probability = _p;
	}
}

public class MelodyMachine
{
	class MelodyState {
		public Note Note;
	}

	class MelodyTransition {
		public float Probability;
		public MelodyState TargetState;
	}

	public static int CurrentTranspose = 0;

	List<MelodyState> states = new List<MelodyState>();
	Dictionary<MelodyState, List<MelodyTransition>> transitions = new Dictionary<MelodyState, List<MelodyTransition>>();

	MelodyState currentState = null;

	int transposeIndex = 0;

	System.Random random;

	public double CurrentFrequency { get { return currentState != null ? currentState.Note.GetFrequency(CurrentTranspose + transposeIndex) : 440; }}
	public Note CurrentNote { get { return currentState.Note; }}
	
	public MelodyMachine ( List<Note> _notes, List<MelodyTansitionInfo> _transitions )
	{
		random = new System.Random();
		//Read in states
		foreach( var note in _notes) {
			states.Add(new MelodyState() { Note = note });
		}

		//Read in transitions
		foreach( var transition in _transitions) {
			MelodyState start = states[transition.A];
			MelodyState end = states[transition.B];

			if( !transitions.ContainsKey(start) )
				transitions[start] = new List<MelodyTransition>();

			transitions[start].Add(new MelodyTransition() { Probability = transition.Probability, TargetState = end});
		}

		//Normalize probabilities
		foreach( var stateTrans in transitions ) {
			float totalProbability = 0;
			foreach(var trans in stateTrans.Value) {
				totalProbability += trans.Probability;
			}

			for(int i = 0; i < stateTrans.Value.Count; i++) {
				stateTrans.Value[i].Probability /= totalProbability;
			}
		}

		//Start in random state;
		currentState = states[0];
	}

	public void Transpose(int _steps) {
		transposeIndex = _steps;
	}

	public void SetOctave(uint _octave) {
		uint baseOctave = states[0].Note.Octave;
		foreach(var state in states) {
			state.Note.Octave = state.Note.Octave - baseOctave + _octave;
		}
	}

	public void Next() {
		float rand = (float)random.NextDouble();

		var availableTransitions = transitions[currentState];

		foreach (var trans in availableTransitions){
			if (rand < trans.Probability) {
				currentState = trans.TargetState;
				break;
			}
			
			rand -= trans.Probability;
		}
	}
}

