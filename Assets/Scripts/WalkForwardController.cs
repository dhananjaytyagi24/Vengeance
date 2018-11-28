using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WalkForwardController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
       // Debug.Log("Down2");
        FighterController.mvFwd = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
       // Debug.Log("Up2");
        FighterController.mvFwd = false;
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
