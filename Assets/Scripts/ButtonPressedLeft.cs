using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPressedLeft : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool buttonPressed;

    void Update()
    {
        if (buttonPressed)
        {
            PlayerMovement.pressedLeft = true;
        }
        else
        {
            PlayerMovement.pressedLeft = false;
        }
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData) => buttonPressed = true;
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData) => buttonPressed = false;
}
