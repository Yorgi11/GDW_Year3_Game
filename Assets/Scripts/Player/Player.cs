using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private string Name;
    [SerializeField] private float maxHp;
    [SerializeField] private float baseAttack;
    [SerializeField] private float blockingFactor;
    [SerializeField] private float rangedAttackRate;
    [SerializeField] private float comboMulti;
    [SerializeField] private float chainMulti;
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float SlowRate;
    [SerializeField] private float rotSpeed;

    [SerializeField] private Animator ani;
    [SerializeField] private Transform rangedSpawn;

    [SerializeField] private Projectile projectilePrefab;

    [SerializeField] private LayerMask groundMask;

    private bool isGrounded = true;
    private bool canRangedAttack = true;

    private int currentChain = 0;

    private float currentHp;
    private float currentDMG;

    private Rigidbody rb;
    private InputManager inputManager;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputManager = GetComponent<InputManager>();
        currentHp = maxHp;
        currentDMG = baseAttack;
    }
    void Update()
    {
        if (currentHp <= 0f) gameObject.SetActive(false);
        currentDMG = baseAttack * (comboMulti > 0f ? comboMulti : 1f) * (chainMulti > 0f ? chainMulti : 1f);

        isGrounded = CheckGround();

        // Movement // Start
        if (inputManager.Vertin != 0f && inputManager.Horzin == 0f) rb.velocity = Vector3.Lerp(rb.velocity, (inputManager.Vertin / inputManager.Vertin) * rb.velocity.magnitude * transform.right, SlowRate * Time.deltaTime); // if no vertical input lerp the forward force towards zero
        else if (inputManager.Vertin == 0f && inputManager.Horzin != 0f) rb.velocity = Vector3.Lerp(rb.velocity, (inputManager.Horzin / inputManager.Horzin) * rb.velocity.magnitude * transform.forward, SlowRate * Time.deltaTime); // if no horizontal input lerp the right force towards zero
        else if (inputManager.Vertin == 0f && inputManager.Horzin == 0f) rb.velocity = Vector3.Lerp(rb.velocity, isGrounded ? Vector3.zero : new Vector3(0f, rb.velocity.y, 0f), SlowRate * Time.deltaTime); // if no movement input lerp the velocity to zero

        ani.SetBool("WalkL", inputManager.Vertin > 0.1f);
        ani.SetBool("WalkR", inputManager.Vertin < -0.1f);
        ani.SetBool("WalkF", inputManager.Horzin > 0.1f);
        ani.SetBool("WalkB", inputManager.Horzin < -0.1f);

        if (rb.velocity.magnitude > speed) rb.velocity = rb.velocity.normalized * speed;
        // Movement // End
    }
    private void FixedUpdate()
    {
        rb.velocity = ((isGrounded ? speed : speed * 0.25f) * (transform.right * inputManager.Vertin + transform.forward * inputManager.Horzin));
        if (isGrounded && inputManager.Jump) 
        { 
            rb.velocity = new (rb.velocity.x, rb.velocity.y * 0.25f, rb.velocity.z); 
            rb.AddForce(transform.up * jumpSpeed, ForceMode.Impulse); 
        }
    }
    private bool CheckGround()
    {
        return Physics.Raycast(transform.position + Vector3.up * 0.25f, Vector3.down, 0.35f, groundMask);
    }
    public void TakeDamage(bool blockable, float d)
    {
        if (inputManager.Blocking && blockable) d *= blockingFactor;
        if (currentHp - d > 0) currentHp -= d;
        else
        {
            currentHp = 0f;
            //ani.SetAnimation("KO_" + (int)Random.Range(0f, 0f), true);
        }
        rb.AddForce(10f * -transform.forward, ForceMode.Impulse);
    }
    public float CurrentDMG
    {
        get { return currentDMG; }
    }
    public float RotateSpeed
    {
        get { return rotSpeed; }
    }
}
