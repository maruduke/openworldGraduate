using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpController : MonoBehaviour, IPointerClickHandler
{
    public ChracterController chracterController;

    public void OnPointerClick(PointerEventData eventData)
    {
        chracterController.jump();
    }
}
