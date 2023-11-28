using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMGDealer : MonoBehaviour
{
    [SerializeField] private bool canBeBlocked = true;
    [SerializeField] private LayerMask mask;

    private bool canAttack = false;
    private float d;
    public float timeSegment = 0f;
    private void UseAttack(Player p)
    {
        p.TakeDamage(canBeBlocked, d);
        StartCoroutine(DelayAttack());
    }
    public IEnumerator SetFor(bool state, float v, float t)
    {
        canAttack = state;
        d = v;
        yield return new WaitForSeconds(t);
        d = 0f;
        canAttack = !state;
        this.gameObject.SetActive(false);
    }
    private IEnumerator DelayAttack()
    {
        canAttack = false;
        yield return new WaitForSeconds(timeSegment);
        canAttack = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.layer == 6 || collision.gameObject.layer == 7) && canAttack) 
        {
            UseAttack(collision.gameObject.GetComponent<Player>());
        }
    }
}
