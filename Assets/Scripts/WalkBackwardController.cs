using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WalkBackwardController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Down1");
        FighterController.mvBack = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("Up1");
        FighterController.mvBack = false;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
