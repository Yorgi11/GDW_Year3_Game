using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    [SerializeField] private Animator ani;

    private bool punch2 = false;

    private Player p;
    private void Start()
    {
        p = GetComponent<Player>();
    }
    void Update()
    {
        ani.SetBool("Jab", p.Jab);
        ani.SetBool("Cross", p.Cross);
        ani.SetBool("PunchCombo1", p.PunchCombo1);
       /* ani.SetBool("Moving", p.IsMoving);
        ani.SetBool("Run", p.IsRunning && rb.velocity.magnitude >= p.WalkSpeed * 0.25f);
        ani.SetBool("Grounded", p.IsGrounded);
        ani.SetBool("AboutToLand", p.IsAboutToLand);
        if (p.IsMelee && p.CanAttack) StartCoroutine(Melee());*/
        //ani.SetBool("Melee", p.IsMelee && p.CanAttack);
        //ani.SetBool("Punch2", punch2);
        //punch2 = !punch2;
        //p.CanAttack = false;
    }
}
