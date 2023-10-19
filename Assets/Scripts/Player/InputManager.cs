using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private bool X = false, Y = false, A = false, B = false, LB = false, RB = false;

    private bool addTime = false;
    private bool jumpInput = false;
    private bool blockInput = false;

    [SerializeField] private int id = 0;

    private float vertin = 0f, horzin = 0f;
    private float Dvertin = 0f, Dhorzin = 0f;
    private float triggers = 0f;

    private float inputTime = 0.75f;
    private float currentTime = 0f;
    private string currentSequence = "";
    private string lastSequence = "";
    private void Update()
    {
        jumpInput = Input.GetKey(KeyCode.Space);
        blockInput = Input.GetKey(KeyCode.Q);
        if (currentSequence.Length == 8) currentTime = inputTime;
        if (currentTime >= inputTime)
        {
            addTime = false;
            currentTime = 0;
            Debug.Log(currentSequence);
            lastSequence = currentSequence;
            currentSequence = "";
        }
        if (Input.GetButtonDown("Xbox_A" + id)) { A = true; currentSequence += 'a'; } // X/A
        else A = false;
        if (Input.GetButtonDown("Xbox_B" + id)) { B = true; currentSequence += 'b'; } // Circle/B
        else B = false;
        if (Input.GetButtonDown("Xbox_X" + id)) { X = true; currentSequence += 'x'; } // Square/X
        else X = false;
        if (Input.GetButtonDown("Xbox_Y" + id)) { Y = true; currentSequence += 'y'; } // Triangle/Y
        else Y = false;
        if (Input.GetButtonDown("Xbox_LB" + id)) { LB = true; currentSequence += 'l'; } // Left bumper
        else LB = false;
        if (Input.GetButtonDown("Xbox_RB" + id)) { RB = true; currentSequence += 'r'; } // Right bumper
        else RB = false;

        vertin = -Input.GetAxisRaw("Xbox_LeftStickY" + id); // Left joystick Back/Fourth
        horzin = Input.GetAxisRaw("Xbox_LeftStickX" + id); // Left joysstick Left/Right
        Dvertin = -Input.GetAxisRaw("Xbox_DpadV" + id); // Dpad Back/Fourth
        Dhorzin = Input.GetAxisRaw("Xbox_DpadH" + id); // Dpad Left/Right
        triggers = Input.GetAxisRaw("Xbox_Triggers" + id); // Triggers

        if (X || Y || A || B || LB || RB) { addTime = false; currentTime -= (inputTime * 0.125f); }
        else addTime = true;
        if (addTime) currentTime += Time.deltaTime;
    }
    public string CurrentSequence
    {
        get { return lastSequence; }
    }
    public bool Jump
    {
        get { return jumpInput; }
    }
    public bool Blocking
    {
        get { return blockInput; }
    }
    public float Vertin
    {
        get { return vertin > Dvertin ? vertin : Dvertin; }
    }
    public float Horzin
    {
        get { return horzin > Dhorzin ? horzin : Dhorzin; }
    }
}
