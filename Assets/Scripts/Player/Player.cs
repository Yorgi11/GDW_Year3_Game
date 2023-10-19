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

    [SerializeField] private string[] ComboSequences;

    [SerializeField] private Transform rangedSpawn;

    [SerializeField] private Projectile projectilePrefab;

    [SerializeField] private LayerMask groundMask;

    private bool isGrounded = true;
    private bool canRangedAttack = true;
    private bool[] attacks;

    private int currentChain = 0;

    private float currentHp;
    private float currentDMG;

    private float inputReadTime = 0.25f;

    private Rigidbody rb;
    private Animations ani;
    private InputManager inputManager;
    void Start()
    {
        attacks = new bool[ComboSequences.Length];
        rb = GetComponent<Rigidbody>();
        ani = GetComponent<Animations>();
        inputManager = GetComponent<InputManager>();
        currentHp = maxHp;
        currentDMG = baseAttack;
    }
    void Update()
    {
        if (currentHp <= 0f) gameObject.SetActive(false);
        currentDMG = baseAttack * (comboMulti > 0f ? comboMulti : 1f) * (chainMulti > 0f ? chainMulti : 1f);

        isGrounded = CheckGround();

        //CheckSequence();

        //if (inputManager.RangedInput && canRangedAttack) RangedAttack();

        // Movement // Start
        if (inputManager.Vertin != 0f && inputManager.Horzin == 0f) rb.velocity = Vector3.Lerp(rb.velocity, (inputManager.Vertin / inputManager.Vertin) * rb.velocity.magnitude * transform.right, SlowRate * Time.deltaTime); // if no vertical input lerp the forward force towards zero
        else if (inputManager.Vertin == 0f && inputManager.Horzin != 0f) rb.velocity = Vector3.Lerp(rb.velocity, (inputManager.Horzin / inputManager.Horzin) * rb.velocity.magnitude * transform.forward, SlowRate * Time.deltaTime); // if no horizontal input lerp the right force towards zero
        else if (inputManager.Vertin == 0f && inputManager.Horzin == 0f) rb.velocity = Vector3.Lerp(rb.velocity, isGrounded ? Vector3.zero : new Vector3(0f, rb.velocity.y, 0f), SlowRate * Time.deltaTime); // if no movement input lerp the velocity to zero

        if (rb.velocity.magnitude > speed) rb.velocity = rb.velocity.normalized * speed;
        // Movement // End
    }
    private void FixedUpdate()
    {
        rb.AddForce((isGrounded ? speed : speed * 0.5f) * (inputManager.Vertin * transform.right + inputManager.Horzin * transform.forward), ForceMode.VelocityChange);
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
        else currentHp = 0f;
    }
    private void CheckSequence()
    {
        for (int i = inputManager.CurrentSequence.Length - 1; i >= 0; i--) // loops from the last combo(most inputs) to the first combo (least inputs)
        {
            if (inputManager.CurrentSequence.Contains(ComboSequences[i])) StartCoroutine(ani.PlayAnimation(i)); // checks if the current input sequence matches the combosequence at i
        }
    }
    private void RangedAttack()
    {
        Projectile p = Instantiate(projectilePrefab, rangedSpawn.position, transform.rotation, transform);
        p.gameObject.layer = (gameObject.layer == 6 ? 8 : 9);
        p.Direction = transform.forward;
        StartCoroutine(ResetProjectileBall());
    }
    private IEnumerator ResetProjectileBall()
    {
        canRangedAttack = false;
        yield return new WaitForSeconds(rangedAttackRate);
        canRangedAttack = true;
    }
    public bool[] Attacks
    {
        get { return attacks; }
    }
    public float CurrentDMG
    {
        get { return currentDMG; }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6 || collision.gameObject.layer == 7)
        {
            TakeDamage(collision.gameObject.GetComponentInParent<Player>().CurrentDMG);
        }
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 9)
        {
            TakeDamage(collision.gameObject.GetComponentInParent<Projectile>().DMG);
        }
    }*/
}
