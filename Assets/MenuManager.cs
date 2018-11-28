using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    AudioSource buttonPress;
    public AudioMixer audioMixer;
    static bool isRightPlay = false;
    public void loadNextScene()
    {
        playButtonPressSound();
        if (isRightPlay)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
        }

    }

    private void Start()
    {
        buttonPress = GetComponent<AudioSource>();

    }

    public void quitGame()
    {
        buttonPress.Play(0);
        Application.Quit();
    }

    public void playButtonPressSound()
    {
        buttonPress.Play(0);
    }

    public void setVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void rightHandedControlScene()
    {
        isRightPlay = true;
    }

    
}
