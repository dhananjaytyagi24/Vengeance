using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class utilsNonStatic : MonoBehaviour {

    // Use this for initialization
    public  void loadNextScene()
    {
        Utils.loadNextScene();
    }

    public void loadCurrentScene()
    {
        Utils.loadCurrentScene();
    }
}
