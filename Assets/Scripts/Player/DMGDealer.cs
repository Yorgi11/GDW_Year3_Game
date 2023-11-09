using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMGDealer : MonoBehaviour
{
    [SerializeField] private bool canBeBlocked = true;
    [SerializeField] private LayerMask mask;
    [SerializeField] private Player p;

    private bool canDamage = true;

    private List<Collider> hits = new List<Collider>();
    private void Update()
    {
        hits = Physics.OverlapSphere(transform.position, 0.6f, mask).ToList<Collider>();
        if (hits.Count > 0 && canDamage)
        {
            foreach (Collider h in hits)
            {
                Player op = h.gameObject.GetComponentInParent<Player>();
                if (op) { op.TakeDamage(canBeBlocked, p.CurrentDMG); op.GetComponent<Rigidbody>().AddForce(op.transform.forward * -30f, ForceMode.Impulse); }
            }
        }
        canDamage = false;
    }
    public IEnumerator EnableDamage(float t)
    {
        canDamage = true;
        yield return new WaitForSeconds(t);
        canDamage = false;
    }
    /*public bool CanDamage
    {
        set { canDamage = value; }
    }*/
}
