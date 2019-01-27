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
                      clothClip, trashClip, shitClip, mudClip,
                      trashBinClip, washerClip, outWindowClip, closetClip;

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

        trashBinClip = Resources.Load<AudioClip>("SFX/TrashBin1");
        washerClip = Resources.Load<AudioClip>("SFX/Dishwasher1");
        outWindowClip = Resources.Load<AudioClip>("SFX/OutWindow1");
        closetClip = Resources.Load<AudioClip>("SFX/OpenCloset1");
        
        TrashFXSource.clip = trashBinClip;
        WasherFXSource.clip = washerClip;
        DishWasherFXSource.clip = washerClip;
        WindowFXSource.clip = outWindowClip;
        DoorFXSource.clip = closetClip;
        
        TrashFXSource.outputAudioMixerGroup = SFXGroup;
        WasherFXSource.outputAudioMixerGroup = SFXGroup;
        DishWasherFXSource.outputAudioMixerGroup = SFXGroup;
        WindowFXSource.outputAudioMixerGroup = SFXGroup;
        DoorFXSource.outputAudioMixerGroup = SFXGroup;
        
        DontDestroyOnLoad (gameObject);
    }
    
    public void PlayMessFX(MessType mess)
    {
        switch (mess)
        {    
            case MessType.Wet:
                MessFXSource.clip = wetClip;
                break;
            case MessType.Mud:
                MessFXSource.clip = dirtClip;
                break;
            case MessType.Dishes:
                MessFXSource.clip = dishClip;
                break;
            case MessType.Trash:
                MessFXSource.clip = trashClip;
                break;
            case MessType.Poop:
                MessFXSource.clip = shitClip;
                break;
            default:
                MessFXSource.clip = cleanClip;
                break;
        }

        MessFXSource.Play();
    }
    
    public void PlayTrashFX(AudioClip clip)
    {
        TrashFXSource.Play();
    }
    
    public void PlayWasherFX(AudioClip clip)
    {
        WasherFXSource.loop = true;
        WasherFXSource.Play();
    }

    public void StopWasherFX()
    {
        WasherFXSource.Stop();
    }
    
    public void PlayDishWasherFX(AudioClip clip)
    {
        DishWasherFXSource.loop = true;
        DishWasherFXSource.Play();
    }

    public void StopDishWasherFX()
    {
        DishWasherFXSource.Stop();
    }
    
    public void PlayWindowFX(AudioClip clip)
    {
        WindowFXSource.Play();
    }
    
    public void PlayDoorFx(AudioClip clip)
    {
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
