using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {
    [SerializeField]
    private AudioSource aus;	
	// Use this for initialization
	void Start () {
		aus = GetComponent<AudioSource> ();
		aus.volume = 0.5f;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
