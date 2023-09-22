using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private string Name;
    [SerializeField] private float baseAttack;
    [SerializeField] private float comboMulti;
    [SerializeField] private float chainMulti;
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float SlowRate;

    [SerializeField] private LayerMask groundMask;

    private bool isGrounded = true;
    private bool canJump = true;
    private bool jabInput;
    private bool crossInput;
    private bool combo1Input;

    private int currentChain = 0;

    private float vertin = 0f, horzin = 0f;

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        vertin = -Input.GetAxisRaw("Vertical");
        horzin = Input.GetAxisRaw("Horizontal");

        isGrounded = CheckGround();
        canJump = Input.GetKey(KeyCode.Space);
        combo1Input = Input.GetKey(KeyCode.L) && Input.GetKey(KeyCode.P);
        jabInput = Input.GetKey(KeyCode.P) && !combo1Input;
        crossInput = Input.GetKey(KeyCode.L) && !combo1Input;

        if (vertin != 0f && horzin == 0f) rb.velocity = Vector3.Lerp(rb.velocity, transform.right * rb.velocity.magnitude * (vertin / vertin), SlowRate * Time.deltaTime); // if no vertical input lerp the forward force towards zero
        else if (vertin == 0f && horzin != 0f) rb.velocity = Vector3.Lerp(rb.velocity, transform.forward * rb.velocity.magnitude * (horzin / horzin), SlowRate * Time.deltaTime); // if no horizontal input lerp the right force towards zero
        else if (vertin == 0f && horzin == 0f) rb.velocity = Vector3.Lerp(rb.velocity, isGrounded ? Vector3.zero : new Vector3(0f, rb.velocity.y, 0f), SlowRate * Time.deltaTime); // if no movement input lerp the velocity to zero

        if (rb.velocity.magnitude > speed) rb.velocity = rb.velocity.normalized * speed;

        //transform.position += speed * Time.deltaTime * (-vertin * transform.right + horzin * transform.forward);

        //if (isGrounded && canJump && Input.GetKey(KeyCode.Space)) StartCoroutine(Jump());

        //if (!isGrounded) transform.position += gravity * Time.deltaTime * Vector3.down;
    }
    private void FixedUpdate()
    {
        rb.AddForce((isGrounded ? speed : speed * 0.5f) * (vertin * transform.right + horzin * transform.forward), ForceMode.VelocityChange);
        if (isGrounded && canJump) 
        { 
            rb.velocity = new (rb.velocity.x, rb.velocity.y * 0.25f, rb.velocity.z); 
            rb.AddForce(transform.up * jumpSpeed, ForceMode.Impulse); 
        }

    }
    private bool CheckGround()
    {
        return Physics.Raycast(transform.position + Vector3.up * 0.25f, Vector3.down, 0.35f, groundMask);
    }
    /*private IEnumerator Jump()
    {
        canJump = false;
        float t = 0;
        while (t < 0.25f)
        {
            t += Time.deltaTime;
            transform.position += jumpSpeed * Time.deltaTime * transform.up * (1 / (t * 4f));
            yield return null;
        }
        canJump = true;
    }*/
    public bool Jab
    {
        get { return jabInput; }
    }
    public bool Cross
    {
        get { return crossInput; }
    }
    public bool PunchCombo1
    {
        get { return combo1Input; }
    }
}
