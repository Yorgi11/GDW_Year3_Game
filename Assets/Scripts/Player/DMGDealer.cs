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

    public IEnumerator Attack(float t, float d)
    {
        while (t > 0f)
        {
            hits = Physics.SphereCastAll(transform.position, 0.3f, Vector3.up, 0.5f, mask);
            if (hits.Length <= 1)
            {
                foreach (RaycastHit hit in hits)
                {
                    hit.collider.gameObject.GetComponentInParent<Player>().TakeDamage(canBeBlocked, d * Time.deltaTime);
                }
            }
            else break;
            t -= Time.deltaTime;
            yield return null;
        }
    }
}
