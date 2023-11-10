using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMGDealer : MonoBehaviour
{
    [SerializeField] private bool canBeBlocked = true;
    [SerializeField] private LayerMask mask;
    [SerializeField] private Player p;

    private RaycastHit[] hits;

    public void Attack()
    {
        hits = Physics.SphereCastAll(transform.position, 0.3f, Vector3.up, 0.5f, mask);
        foreach (RaycastHit hit in hits)
        {
            hit.collider.gameObject.GetComponentInParent<Player>().TakeDamage(canBeBlocked, p.CurrentDMG * Time.deltaTime);
            Debug.Log("Hit");
        }
    }
}
