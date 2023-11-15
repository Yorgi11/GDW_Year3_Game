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
    private void UseAttack(Player p)
    {
        Debug.Log(d);
        p.TakeDamage(canBeBlocked, d);
    }
    public IEnumerator SetFor(bool state, float v, float t)
    {
        canAttack = state;
        d = v;
        yield return new WaitForSeconds(t);
        d = 0f;
        canAttack = !state;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.layer == 6 || collision.gameObject.layer == 7) && canAttack) UseAttack(collision.gameObject.GetComponent<Player>());
    }
}
