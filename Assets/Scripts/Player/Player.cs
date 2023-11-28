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
    [SerializeField] private string forward;
    [SerializeField] private string backward;
    [SerializeField] private string left;
    [SerializeField] private string right;

    [SerializeField] private Animator ani;
    [SerializeField] private UIAnimation HPBar;
    [SerializeField] private Transform rangedSpawn;

    [SerializeField] private AnimationClip[] deathClips;

    [SerializeField] private Projectile projectilePrefab;

    [SerializeField] private LayerMask groundMask;

    private bool isGrounded = true;
    private bool isAttacking = false;
    private bool canRangedAttack = true;

    public int id = 0;
    private int currentChain = 0;

    private float currentHp;
    private float currentDMG;

    private Rigidbody rb;
    private InputManager inputManager;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputManager = GetComponent<InputManager>();
        id = inputManager.ID;
        currentHp = maxHp;
        currentDMG = baseAttack;
    }
    void Update()
    {
        HPBar.SliderBar(maxHp, currentHp);

        currentDMG = baseAttack * (comboMulti > 0f ? comboMulti : 1f) * (chainMulti > 0f ? chainMulti : 1f);

        isGrounded = CheckGround();

        // Movement // Start
        if (inputManager.Vertin != 0f && inputManager.Horzin == 0f) rb.velocity = Vector3.Lerp(rb.velocity, (inputManager.Vertin / inputManager.Vertin) * rb.velocity.magnitude * transform.right, SlowRate * Time.deltaTime); // if no vertical input lerp the forward force towards zero
        else if (inputManager.Vertin == 0f && inputManager.Horzin != 0f) rb.velocity = Vector3.Lerp(rb.velocity, (inputManager.Horzin / inputManager.Horzin) * rb.velocity.magnitude * transform.forward, SlowRate * Time.deltaTime); // if no horizontal input lerp the right force towards zero
        else if (inputManager.Vertin == 0f && inputManager.Horzin == 0f) rb.velocity = Vector3.Lerp(rb.velocity, isGrounded ? Vector3.zero : new Vector3(0f, rb.velocity.y, 0f), SlowRate * Time.deltaTime); // if no movement input lerp the velocity to zero

        ani.SetBool(left, inputManager.Vertin > 0.1f);
        ani.SetBool(right, inputManager.Vertin < -0.1f);
        ani.SetBool(forward, inputManager.Horzin > 0.1f);
        ani.SetBool(backward, inputManager.Horzin < -0.1f);

        if (rb.velocity.magnitude > speed) rb.velocity = rb.velocity.normalized * speed;
        // Movement // End
    }
    private void FixedUpdate()
    {
        if (!isAttacking)
        {
            rb.velocity += ((isGrounded ? speed : speed * 0.25f) * (id == 1 ? -1 : 1) * (transform.right * inputManager.Vertin + transform.forward * inputManager.Horzin));
            if (isGrounded && inputManager.Jump)
            {
                rb.velocity = new(rb.velocity.x, rb.velocity.y * 0.15f, rb.velocity.z);
                rb.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
            }
        }
    }
    private bool CheckGround()
    {
        return Physics.Raycast(transform.position + Vector3.up * 0.25f, Vector3.down, 0.35f, groundMask);
    }
    public void TakeDamage(bool blockable, float d)
    {
        if (inputManager.Blocking && blockable) d *= blockingFactor;
        currentHp -= d;
        rb.AddForce(10f * -transform.forward, ForceMode.Impulse);
        if (currentHp <= 0) 
        { 
            currentHp = 0f;
            GameManager.Instance().SetP(id);
            StartCoroutine(Die()); 
        }
    }
    private IEnumerator Die()
    {
        AnimationClip c = deathClips[(int)Random.Range(0f, deathClips.Length - 1f)];
        ani.Play(c.name);
        yield return new WaitForSeconds(c.length * 0.75f);
        ani.enabled = false;
        this.enabled = false;
    }
    public float CurrentDMG
    {
        get { return currentDMG; }
    }
    public float RotateSpeed
    {
        get { return rotSpeed; }
    }
    public bool IsAttacking
    {
        get { return isAttacking; }
        set { isAttacking = value; }
    }
}
