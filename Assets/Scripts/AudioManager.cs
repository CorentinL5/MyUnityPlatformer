using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioClip[]  playlist;
    public AudioSource audioSource;
    private int musicIndex = 0;

    public AudioMixerGroup soundEffectMixer;

    public static AudioManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de AudioManager dans la scène");
            return;
        }

        instance = this;
    }

    void Start()
    {
        audioSource.clip = playlist[0];;

    }

    // Update is called once per frame
    void Update()
    {
        if(!audioSource.isPlaying)
        {
            PlayNextsong();
        }
    }

    void PlayNextsong()
    {
        musicIndex = (musicIndex + 1) % playlist.Length;
        audioSource.clip = playlist[musicIndex];
        audioSource.Play();
    }

    public AudioSource PlayClipAt(AudioClip clip, Vector3 pos)
    {
        GameObject tempGo = new GameObject("TempAudio");
        tempGo.transform.position = pos;
        AudioSource audioSource = tempGo.AddComponent<AudioSource>();

        audioSource.clip = clip;
        audioSource.outputAudioMixerGroup = soundEffectMixer;
        audioSource.Play();
        Destroy(tempGo, clip.length);
        return audioSource;
    }
}
