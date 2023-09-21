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

    [SerializeField] private LayerMask groundMask;

    private bool isGrounded = true;
    private bool canJump = true;

    private int currentChain = 0;

    private float vertin = 0f, horzin = 0f;
    private float gravity = 9.81f;

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        vertin = Input.GetAxisRaw("Vertical");
        horzin = Input.GetAxisRaw("Horizontal");

        isGrounded = CheckGround();

        transform.position += speed * Time.deltaTime * (-vertin * transform.right + horzin * transform.forward);

        if (isGrounded && canJump && Input.GetKey(KeyCode.Space)) StartCoroutine(Jump());

        if (!isGrounded) transform.position += gravity * Time.deltaTime * Vector3.down;
    }
    private bool CheckGround()
    {
        return Physics.Raycast(transform.position + Vector3.up * 0.25f, Vector3.down, 0.35f, groundMask);
    }
    private IEnumerator Jump()
    {
        canJump = false;
        float t = 0;
        while (t < 0.3f)
        {
            t += Time.deltaTime;
            transform.position += jumpSpeed * Time.deltaTime * transform.up;
            yield return null;
        }
        canJump = true;
    }
}
