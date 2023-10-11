using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private bool X = false, Y = false, A = false, B = false;
    private bool addTime = false;
    private bool jumpInput = false;
    [SerializeField] private int id = 0;
    private float vertin = 0f, horzin = 0f;
    private float inputTime = 0.25f;
    private float currentTime = 0f;
    private string sequence = "";
    private void Update()
    {
        jumpInput = Input.GetKey(KeyCode.Space);
        if (currentTime >= inputTime)
        {
            addTime = false;
            currentTime = 0;
            sequence = "";
        }
        A = Input.GetButton("Controller" + id + "_Button_0"); // X/A
        B = Input.GetButton("Controller" + id + "_Button_1"); // Circle/B
        X = Input.GetButton("Controller" + id + "_Button_2"); // Square/X
        Y = Input.GetButton("Controller" + id + "_Button_3"); // Triangle/Y
        vertin = -Input.GetAxisRaw("Controller" + id + "_LeftAxis_Y"); // Left joystick Back/Fourth
        horzin = Input.GetAxisRaw("Controller" + id + "_LeftAxis_X"); // Left joysstick Left/Right

        if (X || Y || A || B) addTime = true;
        sequence += X ? 'x' : Y ? 'y' : A ? 'a' : B ? 'b' : "";
        if (addTime) currentTime += Time.deltaTime;
    }
    public string CurrentSequence
    {
        get { return sequence; }
    }
    /*public bool RangedInput
    {
        get { return rangedInput; }
    }
    public bool Punch
    {
        get { return punch; }
    }
    public bool Cross
    {
        get { return cross; }
    }*/
    public bool Jump
    {
        get { return jumpInput; }
    }
    public float Vertin
    {
        get { return vertin; }
    }
    public float Horzin
    {
        get { return horzin; }
    }
}
