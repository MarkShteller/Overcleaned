using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSource MainMusic;
    
    public AudioSource MessFXSource;
    public AudioSource TrashFXSource;
    public AudioSource WasherFXSource;
    public AudioSource DishWasherFXSource;
    public AudioSource WindowFXSource;
    public AudioSource DoorFXSource;

    public AudioMixerGroup MasterGroup;
    public AudioMixer MasterMixer;
    
    public AudioMixerGroup MusicGroup;
    public AudioMixerGroup SFXGroup;

    public static AudioManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad (gameObject);
    }
    
    public void PlayMessFX(AudioClip clip)
    {
        MessFXSource.clip = clip;
        MessFXSource.outputAudioMixerGroup = SFXGroup;
        MessFXSource.Play();
    }
    
    public void PlayTrashFX(AudioClip clip)
    {
        TrashFXSource.clip = clip;
        TrashFXSource.outputAudioMixerGroup = SFXGroup;
        TrashFXSource.Play();
    }
    
    public void PlayWasherFX(AudioClip clip)
    {
        WasherFXSource.clip = clip;
        WasherFXSource.outputAudioMixerGroup = SFXGroup;
        WasherFXSource.Play();
        // This one can loop
    }
    
    public void PlayDishWasherFX(AudioClip clip)
    {
        DishWasherFXSource.clip = clip;
        DishWasherFXSource.outputAudioMixerGroup = SFXGroup;
        DishWasherFXSource.Play();
        // This one can loop
    }
    
    public void PlayWindowFX(AudioClip clip)
    {
        WindowFXSource.clip = clip;
        WindowFXSource.outputAudioMixerGroup = SFXGroup;
        WindowFXSource.Play();
    }
    
    public void PlayDoorFx(AudioClip clip)
    {
        DoorFXSource.clip = clip;
        DoorFXSource.outputAudioMixerGroup = SFXGroup;
        DoorFXSource.Play();
    }
    
    public void PlayMainMusic()
    {
        MainMusic.pitch = 1.0f;
        MainMusic.outputAudioMixerGroup = MusicGroup;
        MainMusic.Play();
    }

    public void ChangePitchBendMusic(float PitchBendAmount)
    {
        MainMusic.pitch += PitchBendAmount; 
        MusicGroup.audioMixer.SetFloat("pitchBend", 1f / MainMusic.pitch);
    }
}
