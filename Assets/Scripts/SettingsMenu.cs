using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider musicSlider;
    public Slider fxSlider;

    private int musicValueForSlider;
    private int fxValueForSlider;

    public void Start()
    {
        musicValueForSlider = PlayerPrefs.GetInt("musicValueForSlider", -3);
        audioMixer.SetFloat("music", musicValueForSlider);
        //audioMixer.GetFloat("music", out float musicValueForSlider);
        musicSlider.value = musicValueForSlider;

        fxValueForSlider = PlayerPrefs.GetInt("fxValueForSlider", -3);
        audioMixer.SetFloat("fx", fxValueForSlider);
        //audioMixer.GetFloat("fx", out float fxValueForSlider);
        fxSlider.value = fxValueForSlider;
    }

    public void SetMusicVolume(float volume)
    {
        //Debug.Log(volume);
        audioMixer.SetFloat("music", volume * 10);
        PlayerPrefs.SetInt("musicValueForSlider", musicValueForSlider);
    }

    public void SetFxVolume(float volume)
    {
        //Debug.Log(volume);
        audioMixer.SetFloat("fx", volume * 10);
        PlayerPrefs.SetInt("fxValueForSlider", fxValueForSlider);
    }

    public void ClearSavedData()
    {
        PlayerPrefs.DeleteAll();
    }
}
