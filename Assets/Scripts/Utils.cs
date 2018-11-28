using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Utils
{

    public static void loadNextScene()
    {
        int nextLevelNum = SceneManager.GetActiveScene().buildIndex + 1;
        int nextLevel = nextLevelNum % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextLevel);
    }

    public static void loadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
