using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCollision : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (EnemyController.finisher == true && !FighterController.isdefending) {
                FighterController.instance.reactFinish();
            }
            if (!FighterController.isdefending)
            {
                FighterController.instance.react();
                Debug.Log("Enemy HIT");
            }
            else {
                
                FighterController.instance.defensereact();
                FighterController.isdefending = false;
            }
        }
    }
}
