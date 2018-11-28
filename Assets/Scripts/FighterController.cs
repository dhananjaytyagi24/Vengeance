using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FighterController : MonoBehaviour {
    public Transform enemyTarget;

    public static Animator anim;
    public static bool mvBack = false;
    public static bool mvFwd = false;
    public static FighterController instance;
    public static bool isAttacking = false;
    private Vector3 direction;
    public int health = 100;
    public Slider playerHB;
    public BoxCollider[] c;
    public AudioClip[] audioClip;
    AudioSource audioSource;
    public Vector3 playerPosition;
    Vector3 playerKnockoutPosition;
    public bool active = true;
    public static bool isdefending = false;
    public static bool finisher = false;

    void Awake()
    {
        if (instance == null) {
            instance = this;
        }
    }
    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
        setAllBoxColliders(false);
        audioSource = GetComponent<AudioSource>();
        playerPosition = transform.position;
    }
    private void setAllBoxColliders(bool state)
    {
        c[0].enabled = state;
        c[1].enabled = state;
    }
    private void playAudio(int clip)
    {
        audioSource.clip = audioClip[clip];
        audioSource.Play();
    }


    // Update is called once per frame
    void Update () {

       // Debug.Log(isdefending);
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        direction = enemyTarget.position - this.transform.position;
        direction.y = 0;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.3f);

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("fight_idle"))
          {
              isAttacking = false;
              setAllBoxColliders(false);
        }
       

        if (GameController.allowMovement == true)
        {
            if (isAttacking == false)
            {
                movement();
               // isdefending = false;

            }
            else if(isAttacking==true)
            {
                setAllBoxColliders(true);
                
              //  isdefending = false;
              
              
            }

         
        }
        
    }

    private void movement()
    {
        
        setAllBoxColliders(false);
        if (mvBack == true)
        {
            anim.SetTrigger("wkBack");
            anim.ResetTrigger("idle");
       
        }
        else
        {
            anim.SetTrigger("idle");
            anim.ResetTrigger("wkBack");
        }
        if (mvFwd == true)
        {
            anim.SetTrigger("wkFwd");
           anim.ResetTrigger("idle");
       
        }
        else if(mvBack==false)
        {
            anim.SetTrigger("idle");
            anim.ResetTrigger("wkFwd");
        }
       
    }
    public void Finisher() {
        if (active)
        {
            isAttacking = true;
           // anim.SetTrigger("idle");
            anim.SetTrigger("finisher");
            playAudio(0);
        }
    }
    public void punch()
    {
        if (active)
        {
            isAttacking = true;
            anim.SetTrigger("idle");
            anim.SetTrigger("punch");
            playAudio(0);
        }
    }

    public void defense()
    {
        if (active)
        {
            StartCoroutine(defendFor2Sec());
            setAllBoxColliders(false);
            anim.SetTrigger("defenses");
            playAudio(0);
            
            
        }
    }
    IEnumerator defendFor2Sec()
    {
        isdefending = true;
        yield return new WaitForSeconds(0.5f);
        

    }


    public void kick()
    {
        if (active)
        {
            isAttacking = true;
            anim.SetTrigger("idle");
            anim.SetTrigger("kick");
            playAudio(1);
        }
    }
    public void defensereact()
    {

        health = health - 2;
        if (health < 10 && active)
        {
            setAllBoxColliders(false);
            knockout();
            active = false;
            playAudio(3);
        }
        else
        {
            anim.SetTrigger("idle");

            playAudio(2);
        }
        if (health <= 30)
        {

            // finisher button appears
            GameController.instance.finisherButton.SetActive(true);
            // onclick finisher 
        }

        playerHB.value = health;
    }
    public void react()
    {
       // isAttacking = true;
        
        health = health - 10;
        if (health < 10 && active)
        {
            setAllBoxColliders(false);
            knockout();
            active = false;
            playAudio(3);
        }
        else {

            //  anim.ResetTrigger("idle");
            anim.SetTrigger("idle");
            anim.SetTrigger("reaction");

            playAudio(2);
        }

        if (health <= 30)
        {

            // finisher button appears
            GameController.instance.finisherButton.SetActive(true);
            // onclick finisher 
        }
    

        playerHB.value = health;
    }
    public void reactFinish()
    {
        // isAttacking = true;

        health = health - 20;
        if (health < 10 && active)
        {
            setAllBoxColliders(false);
            knockout();
            active = false;
            playAudio(3);
        }
        else
        {

            //  anim.ResetTrigger("idle");
            anim.SetTrigger("idle");
            anim.SetTrigger("reaction");

            playAudio(2);
        }
        if (health <= 30)
        {

            // finisher button appears
            GameController.instance.finisherButton.SetActive(true);
            // onclick finisher 
        }

        playerHB.value = health;
    }
    public void knockout() {
        GameController.allowMovement = false;
        setAllBoxColliders(false);
        health = 100;
        anim.SetTrigger("knockout");
        GameController.instance.scoreEnemy();
        GameController.instance.OnScreenPoints();
        GameController.instance.rounds();
       
        if (GameController.enemyScore == 2)
        {
            GameController.instance.doReset();
            StartCoroutine(resetCharactersW());
            
           


        }
        else
        {
            StartCoroutine(resetCharacters());
        }

    }
    IEnumerator resetCharacters()
    {
        yield return new WaitForSeconds(3);
        playerHB.value = 100;
        EnemyController.instance.enemyHB.value = 100;
        EnemyController.instance.enemyHealth = 100;
        //transform.position = playerPosition;
        //EnemyController.instance.transform.position = EnemyController.instance.enemyPosition;
        GameObject[] theclone = GameObject.FindGameObjectsWithTag("PlayerPos");
        Transform t = theclone[1].GetComponent<Transform>();
        t.position = playerPosition;
        t.position = new Vector3(t.position.x, 0, t.position.z);
        StartCoroutine(allowPlayerMovement());
    }
    IEnumerator allowPlayerMovement()
    {
        yield return new WaitForSeconds(2);
        
        anim.SetTrigger("idle");
        anim.ResetTrigger("knockout");
        Debug.Log("in allow player coroutine");
        GameController.allowMovement = true;
        setAllBoxColliders(true);
        active = true;
    }
    IEnumerator resetCharactersW()
    {
        yield return new WaitForSeconds(4);
        GameController.allowMovement = false;
        playerHB.value = 100;
        EnemyController.instance.enemyHB.value = 100;
        EnemyController.instance.enemyHealth = 100;
        GameObject[] theclone = GameObject.FindGameObjectsWithTag("PlayerPos");
        Transform t = theclone[1].GetComponent<Transform>();
        t.position = playerPosition;
        t.position = new Vector3(t.position.x, 0, t.position.z);
        //transform.position = playerPosition;
        //EnemyController.instance.transform.position = EnemyController.instance.enemyPosition;
        StartCoroutine(allowPlayerMovement2());
    }
    IEnumerator allowPlayerMovement2()
    {
        yield return new WaitForSeconds(2);

        anim.SetTrigger("idle");
        anim.ResetTrigger("knockout");
        Debug.Log("in allow player coroutine");
        setAllBoxColliders(true);
        active = true;
    }

    public void loadCurrentScene() {

        GameController.instance.doResetForReloadButton();


    }
}
