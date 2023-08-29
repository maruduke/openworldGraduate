using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RunController : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    public ChracterController chracterController;


    public void OnPointerClick(PointerEventData eventData)
    {
        chracterController.speedExchange(true);
    }

}
