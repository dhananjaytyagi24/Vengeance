using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {
    public Transform playerTransform;
    private Vector3 direction;
    static Animator anim2;
    public int enemyHealth = 100;
    public static EnemyController instance;
    public Slider enemyHB;
    public BoxCollider[] c;
    public AudioClip[] audioClip;
    AudioSource audioSource;
    public Vector3 enemyPosition;
    Vector3 enemyKnockoutPosition;
    bool active=true;
    public GameObject dreyer;
    public static bool finisher = false;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        dreyer = this.gameObject;
    }

    // Use this for initialization
    void Start () {
        anim2 = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        setAllBoxColliders(false);
        enemyPosition = transform.position;
    }
    private void playAudio(int clip)
    {
        audioSource.clip = audioClip[clip];
        audioSource.Play();
    }
    private void setAllBoxColliders(bool state)
    {
        c[0].enabled = state;
        c[1].enabled = state;
    }





    // Update is called once per frame
    void Update () {
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
      
        if (anim2.GetCurrentAnimatorStateInfo(0).IsName("fight_idleCopy"))
        {
            direction = playerTransform.position - this.transform.position;
            direction.y = 0;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.3f);
            setAllBoxColliders(false);
            audioSource.Stop();
        }

        if (direction.magnitude > 1.5f && GameController.allowMovement)
        {
            anim2.SetTrigger("walkFWD");
            setAllBoxColliders(false);
            audioSource.Stop();
        }
        else {
            anim2.ResetTrigger("walkFWD");
        }
        if (direction.magnitude < 1.5f && direction.magnitude > 0.75f && GameController.allowMovement) {
            setAllBoxColliders(true);
            if (!audioSource.isPlaying && !anim2.GetCurrentAnimatorStateInfo(0).IsName("roundhouse_kick-mine"))
            {
                playAudio(1);
                anim2.SetTrigger("kick");
                int number = Random.Range(1, 50);
                if (number == 25 &&  enemyHealth < 100)
                {
                    anim2.SetTrigger("regain");
                    StartCoroutine(regainHealth(1f));
                   
                }
                int number2 = Random.Range(1, 50);
                if (number2 == 25)
                {
                    anim2.SetTrigger("finisher");
                    StartCoroutine(finisherRoutine(1f));
                   

                }
                else
                {
                    finisher = false;
                }
            }

        }
        else
        {
            anim2.ResetTrigger("kick");
        }
        if (direction.magnitude <= 0.75f && direction.magnitude > 0.2f && GameController.allowMovement)
        {
            setAllBoxColliders(true);
            if (!audioSource.isPlaying && !anim2.GetCurrentAnimatorStateInfo(0).IsName("cross_punch"))
            {
                playAudio(0);
                anim2.SetTrigger("punch");
            }

        }
        else
        {
            anim2.ResetTrigger("punch");
        }
        if (direction.magnitude <= 0.2f && GameController.allowMovement)
        {
            setAllBoxColliders(false);
            anim2.SetTrigger("walkBack");
            audioSource.Stop();

        }
        else
        {
            anim2.ResetTrigger("walkBack");
        }
    }
    IEnumerator finisherRoutine(float sec) {
        yield return new WaitForSeconds(sec);
        finisher = true;
    }
    IEnumerator regainHealth(float sec) {
        float curr = 0;
        while (curr < sec)
        {   yield return null;
            curr += Time.deltaTime;
            enemyHealth++;
            enemyHB.value = enemyHealth;
        }
        
    }
    public void enemyReact()
    {
        enemyHealth = enemyHealth - 10;
        enemyHB.value = enemyHealth;
        if(enemyHealth < 10 && active)
        {
            enemyKnockout();
            active = false;
            playAudio(3);
        }
        else
        {
            anim2.ResetTrigger("idle");
            playAudio(2);
            anim2.SetTrigger("react");
        }
      
    }

    public void enemyKnockout()
    {
        GameController.allowMovement = false;
        FighterController.instance.active = false;
        setAllBoxColliders(false);
        enemyHealth = 100;
        anim2.SetTrigger("knockout");
        GameController.instance.scorePlayer();
        GameController.instance.OnScreenPoints();
        GameController.instance.rounds();
        if (GameController.playerScore == 2) {
            StartCoroutine(resetCharactersW());
            GameController.instance.doReset();
            
        }
        else
        {
            StartCoroutine(resetCharacters());
        }

    }
    IEnumerator resetCharacters()
    {
        yield return new WaitForSeconds(4);
        enemyHB.value = 100;
        GameObject[] theclone = GameObject.FindGameObjectsWithTag("EnemyPos");
        Transform t = theclone[1].GetComponent<Transform>();
        Debug.Log("Hola"+theclone[0]);
        t.position = enemyPosition;
        t.position = new Vector3(t.position.x, 0, t.position.z);
        FighterController.instance.health = 100;
        FighterController.instance.playerHB.value = 100;
        // transform.position = enemyPosition;
        //FighterController.instance.transform.position = FighterController.instance.playerPosition;
        StartCoroutine(allowPlayerMovement());
    }

IEnumerator allowPlayerMovement()
{
    yield return new WaitForSeconds(4);
    anim2.ResetTrigger("knockout");
    anim2.SetTrigger("idle");
    GameController.allowMovement = true;
    setAllBoxColliders(true);
    active = true;
    FighterController.instance.active = true;
}
IEnumerator resetCharactersW()
   {
       yield return new WaitForSeconds(4);
       FighterController.instance.health = 100;
       enemyHB.value = 100;
       FighterController.instance.playerHB.value = 100;
      // transform.position = enemyPosition;
       //FighterController.instance.transform.position = FighterController.instance.playerPosition;
       GameController.allowMovement = false;
      
        StartCoroutine(allowPlayerMovement2());

    }
    IEnumerator allowPlayerMovement2()
    {
        yield return new WaitForSeconds(4);
        anim2.ResetTrigger("knockout");
        anim2.SetTrigger("idle");
        setAllBoxColliders(true);
        active = true;
        FighterController.instance.active = true;
    }
}
