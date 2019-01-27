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

    private AudioClip cleanClip, wetClip, dirtClip, dishClip, 
                      clothClip, trashClip, shitClip, mudClip;

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

        MainMusic.outputAudioMixerGroup = MusicGroup;
        MessFXSource.outputAudioMixerGroup = SFXGroup;
        cleanClip = Resources.Load<AudioClip>("SFX/CleanTile1");
        wetClip = Resources.Load<AudioClip>("SFX/Wet1");
        dirtClip = Resources.Load<AudioClip>("SFX/SweepDirt1");
        dishClip = Resources.Load<AudioClip>("SFX/Dishes1");
        clothClip = Resources.Load<AudioClip>("SFX/OutWindow2");
        trashClip = Resources.Load<AudioClip>("SFX/TrashTile1");
        shitClip = Resources.Load<AudioClip>("SFX/DogShit1");
        mudClip = Resources.Load<AudioClip>("SFX/Mud1");
        
        DontDestroyOnLoad (gameObject);
    }
    
    public void PlayMessFX(MessType mess)
    {
        switch (mess)
        {    case MessType.Clean:
                MessFXSource.clip = cleanClip;
                break;
            case MessType.Wet:
                MessFXSource.clip = wetClip;
                break;
            case MessType.Dirt:
                MessFXSource.clip = dirtClip;
                break;
            case MessType.Dishes:
                MessFXSource.clip = dishClip;
                break;
            case MessType.Clothes:
                MessFXSource.clip = clothClip;
                break;
            case MessType.Trash:
                MessFXSource.clip = trashClip;
                break;
            case MessType.DogShit:
                MessFXSource.clip = shitClip;
                break;
            case MessType.Mud:
                MessFXSource.clip = mudClip;
                break;
            default:
                break;
        }

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
        MainMusic.Play();
    }

    public void ChangePitchBendMusic(float PitchBendAmount)
    {
        MainMusic.pitch += PitchBendAmount; 
        MusicGroup.audioMixer.SetFloat("pitchBend", 1f / MainMusic.pitch);
    }
}
