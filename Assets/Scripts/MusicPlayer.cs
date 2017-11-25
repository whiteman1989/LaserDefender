using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;

	public AudioClip startClip;
	public AudioClip gameClip;
	public AudioClip endClip;

	private AudioSource music;

	void Awake (){
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad (gameObject);
		}

	}

	// Use this for initialization
	void Start () {
		if ((!music) && (instance == this)) {
			music = GetComponent<AudioSource>();
			PlayNewClip (0);
		} else {
			Debug.Log ("Not find audioSource!");
		}
	}

	void OnLevelWasLoaded (int level){
		Debug.Log ("MusicPlayer: loaded level " + level);
		PlayNewClip (level);

	}

	void PlayNewClip(int level)
	{
		if (instance == this && music) {
			music.Stop ();
			/// select tarck for the level
			switch (level) {
			case 0:
				music.clip = startClip;
				break;
			case 1:
				music.clip = gameClip;
				break;
			default:
				music.clip = endClip;
				break;
			}
			// END select tarck for the level
		
			music.loop = true;
		
			/// play selected track
			Debug.Log (music.clip);
			if (music.clip) {
				music.Play ();
			} else {
				Debug.Log ("selected musci is NULL");
			}
		}
		// END play selected track
	}
	

}
