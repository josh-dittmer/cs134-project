using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class VolumeController : MonoBehaviour
{

    private Slider volumeSlider;
    public AudioMixer audioMixer;
    public string exposedVolume;

    // Start is called before the first frame update
    void Start()
    {
        // get volume slider
        volumeSlider = GetComponent<Slider>();

        // set volume
        SetVolume();
    }

    // Update is called once per frame
    public void SetVolume()
    {
        // set volume of audio mixer
        audioMixer.SetFloat(exposedVolume, Mathf.Log10(volumeSlider.value) * 20);
    }
}
