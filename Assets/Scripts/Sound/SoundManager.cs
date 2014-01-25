using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : Manager {
	
	/////////////////////  BOOTSTRAP  /////////////////////

	private static Manager instance;

	public static SoundManager getInstance() 
	{
		return getInstance(ref instance, "SoundManager") as SoundManager;
	}

	void Awake() {
		prepareSources();	
	}

	///////////////////////////////////////////////////////////

	private bool musicEnabled;
	private bool soundEnabled;

	private AudioSource musicSource;
	private AudioSource musicAlertSource;
	private AudioSource victorySource;
	private AudioSource heroLaunchSource;

	private static Hashtable musicFiles = new Hashtable() {};

	private Hashtable cachedClips;

	private AudioClip heroLaunchClip;
	private AudioClip heroLaunchAttackClip;
	private List<AudioSource> loopingSfx;

    private bool fadingMusic = false;
    private float fadeMusicVolume;
    private float fadeMusicDuration;

    private bool fadingVictory = false;
    private float fadeVictoryVolume;
    private float fadeVictoryDuration;

	private float energyAlertTime;
	private bool energyAlertMuted;


	////////////////////////////////////////////////////////////

    public void Update() {
		if (fadingMusic) {
			checkFadeMusic();
		}
		if (fadingVictory) {
			checkFadeVictory();
		}
    }

    /////////////////
    //  Music API  //
    /////////////////

	public void switchMusic(string worldName) {
		if (musicEnabled) {
			energyAlertTime = 0;
			energyAlertMuted = false;

			// Debug.Log("Load new music clip? "+(musicSource.clip == null ? "clipIsNull" : "currentClip["+musicSource.clip.name+"]" + (string.Compare(musicSource.clip.name, musicFiles[worldName] as string, true) == 0 ? " ALREADY PLAYING" : " YES")));
			
			if (musicSource.clip == null || string.Compare(musicSource.clip.name, musicFiles[worldName] as string, true) != 0) {	
				AudioClip clip = UnityEngine.Resources.Load("Audio/Raw/" + musicFiles[worldName]) as AudioClip;
				musicSource.clip = clip;
				musicSource.volume = fadeMusicVolume;
				musicSource.loop = true;
					
				AudioClip alertclip = UnityEngine.Resources.Load("Audio/Raw/" + musicFiles[worldName] + "_noise") as AudioClip;
				musicAlertSource.clip = alertclip;
				musicAlertSource.volume = 0;
				musicAlertSource.loop = true;

				// Debug.Log("Switching music: "+worldName);
				this.playMusic();
			}			
		}
	}

	public void playMusic() {
		if (musicEnabled) {
			// Debug.Log("Play Music");
			musicSource.Play();
			musicAlertSource.Play();
		}
	}

	public void stopMusic() {
		if (musicEnabled) {
			// Debug.Log("Stop Music");
			musicSource.Stop();
			musicAlertSource.Stop();
		}
	}

	public void pauseMusic() {
		if (musicEnabled) {
			// Debug.Log("Pause Music");
			musicSource.Pause();
			musicAlertSource.Pause();
		}
	}

	public void resumeMusic() {
		if (musicEnabled) {
			// Debug.Log("Resume Music");
			this.playMusic();
		}
	}

	public void fadeMusic(float volume, float duration) {
		if (musicEnabled) {
			// Debug.Log("Starting music fade: time["+Time.time+"] volume["+volume+"] duration["+duration+"]");
			this.fadingMusic = true;
		    this.fadeMusicVolume = volume;
		    this.fadeMusicDuration = duration;
		}
	}

	public bool isMusicEnabled() {
		return musicEnabled;
	}
	

	public void playButtonPressedSound() {
		playSoundEffect("button_toggle");
	}


	// based on score
	public void playCorrectWordSound(int wordScore) {
		if (wordScore > 100) {
			hugeWordSound();
		} else if (wordScore > 70) {
			mediumWordSound();
		} else if (wordScore > 40) {
			smallWordSound();
		} else {
			playSoundEffect("coin");
		}
	}

	void smallWordSound() {
		// sweet
		// outstanding
		// genius
		int choice = getRandIndexInLength(3);
		string sfxname = "";
		switch (choice) {
			case 0:
			sfxname = "sweet";
			break;
			case 1:
			sfxname = "outstanding";
			break;
			case 2:
			sfxname = "genius";
			break;
		}
		playSoundEffect(sfxname);
	}

	void mediumWordSound() {
		// exceptional
		// outrageous
		// impressive
		int choice = getRandIndexInLength(3);
		string sfxname = "";
		switch (choice) {
			case 0:
			sfxname = "exceptional";
			break;
			case 1:
			sfxname = "outrageous";
			break;
			case 2:
			sfxname = "impressive";
			break;
		}
		playSoundEffect(sfxname);
	}

	void hugeWordSound() {
		// sensational
		// unbeatable
		// incredible
		int choice = getRandIndexInLength(3);
		string sfxname = "";
		switch (choice) {
			case 0:
			sfxname = "sensational";
			break;
			case 1:
			sfxname = "unbeatable";
			break;
			case 2:
			sfxname = "incredible";
			break;
		}
		playSoundEffect(sfxname);
	} 

	int getRandIndexInLength(int length) {
		return (int)Mathf.Floor(UnityEngine.Random.Range(0,length));
	}

	////////////////////
	//  Sound FX API  //
	////////////////////

	public void playSoundEffect(string effectName) {
		playSoundEffect(effectName, 1.0f, false, true, null, null, 0);
	}
	
	public void playSoundEffect(string effectName, float volume) {
		playSoundEffect(effectName, volume, false, true, null, null, 0);
	}
	
	public void playSoundEffect(string effectName, float volume, bool loop) {
		playSoundEffect(effectName, volume, loop, true, null, null, 0);
	}

	public void playSoundEffect(string effectName, float volume, bool loop, bool replace) {
		playSoundEffect(effectName, volume, loop, replace, null, null, 0);
	}

	public void playSoundEffect(string effectName, float volume, bool loop, bool replace, AudioSource providedSource, AudioClip providedClip, float afterDelay) {
		if (soundEnabled) {
	
			// Debug.Log("Attempting to play sfx: "+(effectName != null ? "name["+effectName+"]" : "clip["+providedClip+"]")+" volume["+volume+"] loop["+loop+"] replace["+replace+"]");
	
			bool playSound = true;
			if (loop && effectName != null) {
				if (isLoopingSoundEffectAlreadyPlaying(effectName)) {
					if (replace) {
						stopLoopingSoundEffect(effectName);
					}
					else {
						playSound = false;
					}
				}
			}
	
			if (playSound) {
				AudioSource source;
				AudioClip clip;
		
				if (providedSource != null) {
					source = providedSource;
				}
				else {
					source = this.gameObject.AddComponent("AudioSource") as AudioSource;
				}
		
				if (providedClip != null) {
					clip = providedClip;
				} 
				else {
					if (cachedClips.ContainsKey(effectName)){
						clip = (AudioClip)cachedClips[effectName];
					} else {
						clip = UnityEngine.Resources.Load("Audio/Raw/"+effectName) as AudioClip;
						trimCachedClips();
						cachedClips[effectName] = clip;
					}
				}
		
				if (clip != null && source != null) {
					source.clip = clip;
					source.loop = loop;
					source.volume = volume;
		
					// Debug.Log(Time.time + " Playing sfx: "+(effectName != null ? "name["+effectName+"]" : "clip["+clip+"]")+" volume["+volume+"] loop["+loop+"] replace["+replace+"] length["+clip.length+"]");
					
					if (!loop) {
						StartCoroutine(playAudioSourceAfterDelay(source, providedSource == null, afterDelay));
					}
					else {
						loopingSfx.Add(source);
						StartCoroutine(playAudioSourceAfterDelay(source, false, afterDelay));
					}
				} 
				else {
					Debug.LogWarning("Could not locate sound effect named : " + effectName);
				}
			}
		}
	}

	public void cacheClip (string clipname){
		StartCoroutine(cacheClipHelper(clipname));
	}

	IEnumerator cacheClipHelper (string clipname) {
		yield return null;
		AudioClip clip = UnityEngine.Resources.Load("Audio/Raw/"+clipname) as AudioClip;
		cachedClips[clipname] = clip;
	}

	private void trimCachedClips() {
		ArrayList keys = new ArrayList(cachedClips.Keys);
		if (keys.Count > 30) {
			int counter = keys.Count - 30 + 10; // allow for 10 extra slots
			foreach(var key in keys){
				cachedClips.Remove(key);
				// Debug.Log("Removing cached clip : " +key);
				counter --;
				if (counter <=0){
					break;
				}
			}
		}
	}
	
	public void playHeroLaunchSfx(float volume, bool attackActive) {
		if (soundEnabled) {
			if (heroLaunchClip == null) {
				heroLaunchClip = UnityEngine.Resources.Load("Audio/Raw/fly") as AudioClip;
				heroLaunchAttackClip = UnityEngine.Resources.Load("Audio/Raw/fly_attack") as AudioClip;
			}
			
			if (!attackActive && heroLaunchClip != null) {
				playSoundEffect(null, volume, false, false, heroLaunchSource, heroLaunchClip, 0);
			} 
			else if (heroLaunchAttackClip != null) {
				playSoundEffect(null, volume, false, false, heroLaunchSource, heroLaunchAttackClip, 0);
			}
		}
	}
	
	public void playVictorySfx(float volume) {		
		if (soundEnabled) {
			if (victorySource != null) {
				fadeMusic(0f, 0f);
				playSoundEffect("victory_cheer", volume, false, false, victorySource, null, 0);
			}
		} 
	}

	public void fadeVictorySfx(float volume, float duration) {
		if (soundEnabled) {
			// Debug.Log("Trying to fade victory SFX: fadingVictory["+fadingVictory+"] victorySource.isPlaying["+(victorySource && victorySource.isPlaying)+"] duration["+duration+"]");
			if (!fadingVictory && victorySource != null) {
				if (!victorySource.isPlaying || duration == 0) {
					victorySource.Stop();
					victorySource.volume = 0;
					fadeMusic(1f, 5f);
				}
				else {
					this.fadingVictory = true;
				    this.fadeVictoryVolume = volume;
				    this.fadeVictoryDuration = Mathf.Min(duration, victorySource.clip.length - victorySource.time);
				    // Debug.Log("Fading victory sfx: volume["+fadeVictoryVolume+"] duration["+fadeVictoryDuration+"] currentTime["+victorySource.time+"]");
					StartCoroutine(fadeMusicAfterDelay(1f, 5f, this.fadeVictoryDuration + 2f));
				}
			}
			else {
				if (duration == 0) {
					// Debug.Log("Forcing stop of victory SFX");
					victorySource.Stop();
					victorySource.volume = 0;
				}
			}
		}
	}	

	public void pauseAllSoundEffects() {
		if (soundEnabled) {
			// Debug.Log("Pausing All SFX");
			AudioSource[] sources = this.gameObject.GetComponents<AudioSource>();
			foreach (AudioSource source in sources){
				if (source.clip != null && source != musicSource && source != musicAlertSource) {
					// Debug.Log("Pausing sfx: name["+source.clip.name+"]");
					source.Pause();
				}
			}
		}
	}

	public void resumeAllSoundEffects() {
		if (soundEnabled) {
			// Debug.Log("Resuming All SFX");
			AudioSource[] sources = this.gameObject.GetComponents<AudioSource>();
			foreach (AudioSource source in sources) {
				if (source.clip != null && source != musicSource && source != musicAlertSource) {
					// Debug.Log("Resuming sfx: name["+source.clip.name+"]");
					source.Play();
				}
			}
		}
	}

	public void stopAllSoundEffects() {
		if (soundEnabled) {
			// Debug.Log("Stopping All SFX");
			AudioSource[] sources = this.gameObject.GetComponents<AudioSource>();
			foreach (AudioSource source in sources){
				if (source.clip != null && source != musicSource && source != musicAlertSource) {
					// Debug.Log("Stopping sfx: name["+source.clip.name+"]");
					source.Stop();
				}
			}
		}
	}

	public void stopLoopingSoundEffect(string effectName) {
		if (soundEnabled) {
			// sndDebug("Stopping looping SFX: name["+effectName+"]");
	
			AudioSource source;
			for (int i = 0; i < loopingSfx.Count; i++) {
				source = loopingSfx[i];
				if (source.clip.name == effectName) {
					// Debug.Log("Stopping sfx: name["+source.clip.name+"] volume["+source.volume+"]");
					source.Stop();
					loopingSfx.Remove(source);
					Destroy(source);
					break;
				}
			}
	
			// sndDebug("Done stopping looping SFX: name["+effectName+"]");
		}
	}

	public void stopAllLoopingSoundEffects() {
		if (soundEnabled) {
			// sndDebug("Stopping all looping SFX");
	
			AudioSource source;
			for (int i = loopingSfx.Count - 1; i >= 0; i--) {
				source = loopingSfx[i];
				if (source != musicSource && source != musicAlertSource) {
					// Debug.Log("Stopping sfx: name["+source.clip.name+"] volume["+source.volume+"]");
					source.Stop();
					loopingSfx.Remove(source);
					Destroy(source);
				}
			}
	
			// sndDebug("Done stopping all looping SFX");
		}
	}

	public bool isLoopingSoundEffectAlreadyPlaying(string effectName) {
		if (!soundEnabled) {
			return false;
		}
		
		for (int i = 0 ; i < loopingSfx.Count; i++) {
			if (loopingSfx[i].clip.name == effectName) {
				return true;
			}
		}
		return false;
	}
	
	public bool isSoundEnabled() {
		return soundEnabled;
	}
	

	////////////////////////////
	//  Special SFX Handling  //
	////////////////////////////

	public void crossfadeEnergyAlert(bool energyLow) {
		if (!energyAlertMuted && musicAlertSource != null) {
			if (energyLow) {
				if (musicAlertSource.volume < 0.6f) {
					energyAlertTime += Time.deltaTime;
					musicAlertSource.volume = Mathf.Min(0.6f, energyAlertTime/20f);
				}
			}
			else {
				if (musicAlertSource.volume > 0) {
					energyAlertTime -= Time.deltaTime;
					musicAlertSource.volume = Mathf.Max(0, energyAlertTime/20f);
				}
			}
		}
	}

	public void muteEnergyAlert(bool mute) {
		energyAlertMuted = mute;
		if (mute) {
			energyAlertTime = 0;
			musicAlertSource.volume = 0;
		}
	}

	public void pauseEverything() {
		pauseAllSoundEffects();
		pauseMusic();
	}

	public void resumeEverything() {
		resumeAllSoundEffects();
		resumeMusic();
	}

	///////////////////////
	//  Private Methods  //
	///////////////////////
	
	private void prepareSources() {
		this.musicEnabled = true;
		this.soundEnabled = true;

		// Debug.Log("SoundManger initial sound enabled: "+this.soundEnabled);

		cachedClips =  new Hashtable();

		musicSource = this.gameObject.AddComponent("AudioSource") as AudioSource;
		musicSource.volume = 1.0f;
		
		musicAlertSource = this.gameObject.AddComponent("AudioSource") as AudioSource;	
		musicAlertSource.volume = 0;

		victorySource = this.gameObject.AddComponent("AudioSource") as AudioSource;	
		heroLaunchSource = this.gameObject.AddComponent("AudioSource") as AudioSource;	
		
		loopingSfx = new List<AudioSource>();

		fadeMusicVolume = 1f;
		fadeVictoryVolume = 1f;

		cacheClip("button_toggle");
	}

	private void playSource(AudioSource source, bool removeWhenFinished) {
		source.Play();
		if (removeWhenFinished) {
			Destroy(source, source.clip.length + 1f);
		}
	}
	private void checkFadeMusic() {
		if (musicSource != null) {
			// Debug.Log("Music fade start volume="+musicSource.volume);
			if (this.fadeMusicDuration == 0) {
				musicSource.volume = this.fadeMusicVolume;
			}
			else if (musicSource.volume < this.fadeMusicVolume) {
				musicSource.volume = Mathf.Min(this.fadeMusicVolume, musicSource.volume + (Time.deltaTime/(Time.timeScale != 0 ? Time.timeScale : 1))/this.fadeMusicDuration);
			}
			else if (musicSource.volume > this.fadeMusicVolume) {
				musicSource.volume = Mathf.Max(this.fadeMusicVolume, musicSource.volume - (Time.deltaTime/(Time.timeScale != 0 ? Time.timeScale : 1))/this.fadeMusicDuration);
			}			
			// Debug.Log("Music fade end volume="+musicSource.volume);
			
			if (musicSource.volume == this.fadeMusicVolume) {
				// Debug.Log("Finished music fade at: "+Time.time);
				this.fadingMusic = false;
			}
		}
		else {
			this.fadingMusic = false;
		}
	}
	
	private void checkFadeVictory() {
		if (victorySource != null) {
			// Debug.Log("Victory fade start volume="+victorySource.volume);
			if (this.fadeVictoryDuration == 0) {
				victorySource.volume = this.fadeVictoryVolume;
			}
			else if (victorySource.volume < this.fadeVictoryVolume) {
				victorySource.volume = Mathf.Min(this.fadeVictoryVolume, victorySource.volume + (Time.deltaTime/(Time.timeScale != 0 ? Time.timeScale : 1))/this.fadeVictoryDuration);
			}
			else if (victorySource.volume > this.fadeVictoryVolume) {
				victorySource.volume = Mathf.Max(this.fadeVictoryVolume, victorySource.volume - (Time.deltaTime/(Time.timeScale != 0 ? Time.timeScale : 1))/this.fadeVictoryDuration);
			}			
			// Debug.Log("Victory fade end volume="+victorySource.volume);
			
			if (victorySource.volume == this.fadeVictoryVolume) {
				// Debug.Log("Finished victory fade at: "+Time.time);
				this.fadingVictory = false;
				this.victorySource.Stop();
			}
		}
		else {
			this.fadingVictory = false;
		}		
	}

	IEnumerator playAudioSourceAfterDelay(AudioSource source, bool removeWhenFinished, float delay) {
		if (delay > 0 && Time.timeScale > 0) {
			yield return new WaitForSeconds(delay / Time.timeScale);
		}
		playSource(source, removeWhenFinished);
	}

	IEnumerator fadeMusicAfterDelay(float volume, float duration, float delay) {
		if (delay > 0) {
			yield return new WaitForSeconds(delay);
		}
		fadeMusic(volume, duration);
	}

	private void sndDebug(string msg) {
		string debugStr = msg+"\n";
		AudioSource[] sources = this.gameObject.GetComponents<AudioSource>();
		foreach (AudioSource x in sources) {
			debugStr += ("   AUDIO SOURCE: "+(x.clip ? x.clip.name : "unknown")+"\n");
		}
		Debug.Log(debugStr);
	}
}
