using UnityEngine;
using System.Collections.Generic;

public class MusicGenerator : MonoBehaviour {
	public GameObject DrumPrefab;
	public GameObject MelodyPrefab;

	List<Instrument> drums;
	List<Instrument> bass;
	List<Instrument> melody;

	List<Note> chordProgression;

	List<Note> availableNotes;
	int bpm = 20;
	int measures = 4;


	void Awake() {
		DrumPrefab = Resources.Load<GameObject>("DrumPrefab");
		MelodyPrefab = Resources.Load<GameObject>("MelodyPrefab");
		bpm = 35; // Random.Range(20, 50);
		availableNotes = GenerateScale();
		melody = new List<Instrument>();

		if(Random.value < 0.6f)
			melody.Add(MusicGenerator.GenerateMelodyInstrument(bpm, measures, transform, MelodyPrefab));
		//melody.Add(MusicGenerator.GenerateMelodyInstrument(bpm, measures, transform, MelodyPrefab));

		bass = new List<Instrument>();
		if(Random.value < 0.3f)
			bass.Add(MusicGenerator.GenerateBassInstrument(bpm, measures, transform, MelodyPrefab));
		//bass.Add(MusicGenerator.GenerateBassInstrument(bpm, measures, transform, MelodyPrefab));

		drums = new List<Instrument>();
		if(Random.value < 0.3f)
			drums.Add(MusicGenerator.GenerateDrumInstrument(bpm, measures, transform, DrumPrefab));
		else
			drums.Add(MusicGenerator.GenerateDrumInstrument(bpm, measures, transform, DrumPrefab));


	}

	void Start() {
		foreach(var instrument in melody) {
			instrument.Init(availableNotes);
			instrument.audio.Play();
		}
		
		foreach(var instrument in drums) {
			instrument.Init(availableNotes);
			instrument.audio.Play();
		}
		
		foreach(var instrument in bass) {
			instrument.Init(availableNotes);
			instrument.audio.Play();
		}
	}

	List<Note> GenerateScale() {
		NoteName baseNote = (NoteName)Random.Range(0, 12);

		int baseOctave = Random.Range(2, 5);

		List<Note> notes = new List<Note>();

		bool isMajor = Random.value < 0.5f;

		int halfSteps = (int)baseNote;
		int stepsInScale = 0;
		for(int i = 0; i < 7; i++) {
			notes.Add(new Note( (NoteName) halfSteps, (uint)baseOctave));

			if(( isMajor && (stepsInScale == 4 || stepsInScale == 11)) ||
			   (!isMajor && (stepsInScale == 2 || stepsInScale == 7))) {
				halfSteps += 1;
				stepsInScale += 1;
			} else {
				halfSteps += 2;
				stepsInScale += 2;
			}

			if(halfSteps > 11) {
				baseOctave++;
				halfSteps = 0;
			}
		}

		int extraNotes = Random.Range(2, 5);
		for(int i = 0; i < extraNotes; i++) {
			Note toDuplicate = notes[Random.Range(0, 7)];
			notes.Add( new Note(toDuplicate.NoteName, toDuplicate.Octave + (uint)(Random.value < 0.5f ? 1 : -1)));
		}

		return notes;
	}

	public static Instrument GenerateDrumInstrument(int _bpm, int _measures, Transform _parent, GameObject _prefab) {
		GameObject obj = (GameObject)GameObject.Instantiate(_prefab);
		obj.transform.parent = _parent;
		obj.transform.localPosition = Vector3.zero;
		NoiseGenerator noiseGen = obj.GetComponent<NoiseGenerator>();
		WaveGenerator waveGen = obj.GetComponent<WaveGenerator>();
		Instrument instrument = obj.GetComponent<Instrument>();

		if(Random.value < 0.5f) {
			var highPass = obj.AddComponent<AudioHighPassFilter>();
			highPass.cutoffFrequency = 3000;
		} else {
			var lowPass = obj.AddComponent<AudioLowPassFilter>();
			lowPass.cutoffFrequency = 1200;
		}
		instrument.FixedSpeed = true;


		noiseGen.offset = Random.value;
		
		waveGen.Type = (WaveType)Random.Range(0, 3);
		waveGen.gain = Random.Range(0.15f, 0.25f) * (waveGen.Type == WaveType.Sin ? 5 : 1);
		
		instrument.BPM = _bpm;
		instrument.Measures = _measures;
		
		instrument.Speed = Random.Range(1, 3);
		instrument.LowLevel = Random.Range(1, 2);
		instrument.HighLevel = instrument.LowLevel + Random.Range(0, 2);
		
		float fallofTime = Random.Range(0.1f, 0.55f);
		
		Keyframe[] keyFrames = new Keyframe[4];
		keyFrames[0] = new Keyframe(fallofTime * 0.0f, 1);
		keyFrames[1] = new Keyframe(fallofTime * 0.25f, Mathf.Clamp01(Random.value * 2));
		keyFrames[2] = new Keyframe(fallofTime * 0.5f, Random.value/2);
		keyFrames[3] = new Keyframe(fallofTime * 1.0f, 0.0f);
		
		instrument.Falloff = new AnimationCurve(keyFrames);
		instrument.Vibrato = Random.Range(0.0f, 0.2f);
		return instrument;
	}

	public static Instrument GenerateBassInstrument(int _bpm, int _measures, Transform _parent, GameObject _prefab) {
		GameObject obj = (GameObject)GameObject.Instantiate(_prefab);
		obj.transform.parent = _parent;
		obj.transform.localPosition = Vector3.zero;
		WaveGenerator waveGen = obj.GetComponent<WaveGenerator>();
		Instrument instrument = obj.GetComponent<Instrument>();
		
		waveGen.Type = WaveType.Sin;
		waveGen.gain = Random.Range(0.25f, 0.45f);
		
		instrument.BPM = _bpm;
		instrument.Measures = _measures;
		
		instrument.Speed = 1;
		instrument.LowLevel = 0;
		instrument.HighLevel = instrument.LowLevel + Random.Range(0, 1);
		instrument.MinSpeed = 0;
		instrument.MaxSpeed = 2;

		Keyframe[] keyFrames = new Keyframe[1];
		keyFrames[0] = new Keyframe(0.0f, 1);
		instrument.Falloff = new AnimationCurve(keyFrames);
		return instrument;
	}

	public static Instrument GenerateMelodyInstrument(int _bpm, int _measures, Transform _parent, GameObject _prefab) {
		GameObject obj = (GameObject)GameObject.Instantiate(_prefab);
		obj.transform.parent = _parent;
		obj.transform.localPosition = Vector3.zero;
		WaveGenerator waveGen = obj.GetComponent<WaveGenerator>();
		Instrument instrument = obj.GetComponent<Instrument>();

		waveGen.Type = (WaveType)Random.Range(0, 3);
		waveGen.gain = Random.Range(0.01f, 0.15f) * (waveGen.Type == WaveType.Sin ? 5 : 1);

		instrument.BPM = _bpm;
		instrument.Measures = _measures;

		instrument.Speed = 2;
		instrument.LowLevel = Random.Range(1, 3);
		instrument.HighLevel = instrument.LowLevel + Random.Range(0, 2);

		float fallofTime = Random.Range(0.25f, 0.55f);

		Keyframe[] keyFrames = new Keyframe[4];
		keyFrames[0] = new Keyframe(fallofTime * 0.0f, Random.value < 0.5f ? 0 : 1);
		keyFrames[1] = new Keyframe(fallofTime * 0.25f, Random.value);
		keyFrames[2] = new Keyframe(fallofTime * 0.5f, Mathf.Clamp01(Random.value * 2));
		keyFrames[3] = new Keyframe(fallofTime * 1.0f, 0.0f);
		
		instrument.Falloff = new AnimationCurve(keyFrames);
		return instrument;
	}
}
