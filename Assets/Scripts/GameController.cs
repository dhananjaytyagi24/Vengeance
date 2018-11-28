using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class GameController : MonoBehaviour {
    public static GameController instance;
    public static bool allowMovement=false;
    public GameObject flashButton;
    public GameObject cameraButton;
    public GameObject playerScoreOnScreen;
    public GameObject enemyScoreOnScreen;
    public GameObject backButton;
    public GameObject forwardButton;
    public GameObject punchButton;
    public GameObject kickButton;
    public GameObject blockButton;
    public GameObject finisherButton;
    private bool played321 = false;
    public AudioClip[] audioclip;
    AudioSource audioSource;
    public static int playerScore = 0;
    public static int enemyScore = 0;
    public GameObject[] points;
    public static int round = 0;


    // Use this for initialization
    void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        
    }
    void Start () {
        kickButton.SetActive(false);
        punchButton.SetActive(false);
        blockButton.SetActive(false);
        finisherButton.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        Button button = cameraButton.GetComponent<Button>();
        button.onClick.AddListener(TaskUpdate);
	}

    void playAudio(int clip) {
        audioSource.clip = audioclip[clip];
        audioSource.Play();
    }

    public void scorePlayer()
    {
        playerScore++;

    }

    public void scoreEnemy()
    {
        enemyScore++;
    }

    public void doReset() {
       
        allowMovement = false;
        if (playerScore == 2)
        {
            playAudio(6);
        }
        else
        {
            playAudio(5);
        }
       

        kickButton.SetActive(false);
        punchButton.SetActive(false);
        blockButton.SetActive(false);
        finisherButton.SetActive(false);
        StartCoroutine(restartGame());


    }


    IEnumerator restartGame() {
        yield return new WaitForSeconds(4.5f);
        points[0].SetActive(false);
        points[1].SetActive(false);
        points[2].SetActive(false);
        points[3].SetActive(false);
        //StartCoroutine(round1());
        // Load NEXT LEVEL
        if (playerScore == 2)
        {
            playerScore = 0;
            enemyScore = 0;
             Utils.loadNextScene();
        }
        else if(enemyScore==2)
        {
            enemyScore = 0;
            playerScore = 0;
            Utils.loadCurrentScene();
        }
    }
    public void doResetForReloadButton()
    {

        allowMovement = false;
        enemyScore = 0;
        playerScore = 0;
        kickButton.SetActive(false);
        punchButton.SetActive(false);
        blockButton.SetActive(false);
        finisherButton.SetActive(false);
        StartCoroutine(reloadGame());


    }
    public void next() {
        Utils.loadNextScene();
    }
    IEnumerator reloadGame()
    {
        yield return new WaitForSeconds(1f);
        points[0].SetActive(false);
        points[1].SetActive(false);
        points[2].SetActive(false);
        points[3].SetActive(false);
        Utils.loadCurrentScene();
       
    }

        // Update is called once per frame
        void Update () {
        
           
        
    }
    void TaskUpdate()
    {
        if (played321 == false)
        {
            played321 = true;
            //Disabling camera button to avoid multiple rendering instantiation of player objects
            cameraButton.SetActive(false);
            StartCoroutine(round1());

        }
    }

    IEnumerator round1() {

        yield return new WaitForSeconds(0f);
        playAudio(0);
        StartCoroutine(prepareyourself());
    }
    IEnumerator prepareyourself()
    {
        yield return new WaitForSeconds(1.2f);
        playAudio(1);
        StartCoroutine(start321());
    }
    IEnumerator start321() {
        yield return new WaitForSeconds(2f);
        playAudio(2);
     
        StartCoroutine(allowPlayerMovement());
    }
    IEnumerator allowPlayerMovement() {
        
        yield return new WaitForSeconds(5f);
        kickButton.SetActive(true);
        punchButton.SetActive(true);
        blockButton.SetActive(true);
        allowMovement = true;
        cameraButton.SetActive(false);
    }

    public void OnScreenPoints() {
        if (playerScore == 1)
        {
            points[0].SetActive(true);
        }
        else if (playerScore == 2) {
            points[1].SetActive(true);
        }

        if (enemyScore == 1)
        {
            points[2].SetActive(true);
        }
        else if (enemyScore == 2)
        {
            points[3].SetActive(true);
        }
    }

    public void rounds() {
       
        round = playerScore + enemyScore;
        if (round == 1) {
            playAudio(3);
            finisherButton.SetActive(false);
        }
        if (round == 2 && playerScore == 1 && enemyScore == 1)
        {  
            playAudio(4);
            finisherButton.SetActive(false);

        }
    }
}
