using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RunController : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    public ChracterController chracterController;
    private bool runStatus = false;


    public void OnPointerClick(PointerEventData eventData)
    {
        runStatus = !runStatus;
        chracterController.speedExchange(runStatus);
    }

}
