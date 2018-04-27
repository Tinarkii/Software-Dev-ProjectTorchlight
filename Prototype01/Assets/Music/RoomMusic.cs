using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;



public class RoomMusic : MonoBehaviour {
	public AudioMixerSnapshot MusicToFadeIn;

	private float m_TransitionIn;
	private float m_TransitionOut;
	private float m_QuarterNote;
	public float bpmFadeInSpeed = 128;

	void Start () {
		m_QuarterNote = 60 / bpmFadeInSpeed;
		m_TransitionIn = m_QuarterNote;
		MusicToFadeIn.TransitionTo(m_TransitionIn);
	}

}
