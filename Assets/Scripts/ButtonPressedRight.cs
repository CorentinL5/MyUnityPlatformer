using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPressedRight : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool buttonPressed;

    void Update()
    {
        if (buttonPressed)
        {
            PlayerMovement.pressedRight = true;
        }
        else
        {
            PlayerMovement.pressedRight = false;
        }
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData) => buttonPressed = true;
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData) => buttonPressed = false;
}
