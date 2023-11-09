using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool X = false, Y = false, A = false, B = false, LB = false, RB = false;
    private bool jumpInput = false;
    private bool blockInput = false;

    [SerializeField] private int id = 0;

    private float vertin = 0f, horzin = 0f;
    private float Dvertin = 0f, Dhorzin = 0f;
    private float triggers = 0f;
    private void Update()
    {
        jumpInput = Input.GetKey(KeyCode.Space);
        blockInput = Input.GetKey(KeyCode.Q);

        A = Input.GetButtonDown("Xbox_A" + id);
        B = Input.GetButtonDown("Xbox_B" + id);
        X = Input.GetButtonDown("Xbox_X" + id);
        Y = Input.GetButtonDown("Xbox_Y" + id);
        LB = Input.GetButtonDown("Xbox_LB" + id);
        RB = Input.GetButtonDown("Xbox_RB" + id);

        vertin = Input.GetAxisRaw("Xbox_LeftStickY" + id); // Left joystick Back/Fourth //Input.GetKey(KeyCode.W) ? 1f : Input.GetKey(KeyCode.S) ? -1f : 0f;
        horzin = Input.GetAxisRaw("Xbox_LeftStickX" + id); // Left joysstick Left/Right
        Dvertin = Input.GetAxisRaw("Xbox_DpadV" + id); // Dpad Back/Fourth
        Dhorzin = Input.GetAxisRaw("Xbox_DpadH" + id); // Dpad Left/Right
        triggers = Input.GetAxisRaw("Xbox_Triggers" + id); // Triggers
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
        get { return Mathf.Abs(vertin) > Mathf.Abs(Dvertin) ? vertin : Dvertin; }
    }
    public float Horzin
    {
        get { return Mathf.Abs(horzin) > Mathf.Abs(Dhorzin) ? horzin : Dhorzin; }
    }
}
