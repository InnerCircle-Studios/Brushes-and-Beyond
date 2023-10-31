using UnityEngine;
using System;

public class TutorialMovement : MonoBehaviour {
    private bool _up, _down, _left, _right = false;
    public bool tutorial = false;
    private bool wasd = false;

    public void setBool(String text)
    {
        if (text == "up" && tutorial)
        {
            _up = true;
            checkWasd();
        } 
        else if ( text == "down" && tutorial)
        {
            _down = true;
            checkWasd();
        }
        else if (text == "left" && tutorial)
        {
            _left = true;
            checkWasd();
        }
        else if (text == "right" && tutorial)
        {
            _right = true;
            checkWasd();
        }
    }

    public void checkWasd()
    {
        if (_up && _down && _left && _right && !wasd)
        {
            wasd = true;
            Debug.Log("Het werk enzo");
        }
    }
}