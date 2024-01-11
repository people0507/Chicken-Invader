using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundManager : MonoBehaviour
{
    [SerializeField] private Slider volumnSlider;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolumn"))
        {
            PlayerPrefs.SetFloat("musicVolumn", 1);
            Load();
        }
        else
            Load();
    }

    public void ChangeVolumn()
    {
        AudioListener.volume = volumnSlider.value;
        Save();
    }

    private void Load()
    {
        volumnSlider.value = PlayerPrefs.GetFloat("musicVolumn");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolumn", volumnSlider.value);
    }
}
