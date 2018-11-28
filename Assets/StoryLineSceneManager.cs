using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryLineSceneManager : MonoBehaviour {

    AudioSource buttonPress;
    public GameObject menuSceneManager;

    bool controlDirection;
 
    public void loadNextScene()
    {
        playButtonPressSound();
        
        {
            
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
        }

    }

    private void Start()
    {
        buttonPress = GetComponent<AudioSource>();
        
    }

    public void playButtonPressSound()
    {
        buttonPress.Play(0);
    }
}
