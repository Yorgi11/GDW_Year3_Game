using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMGDealer : MonoBehaviour
{
    [SerializeField] private bool canBeBlocked = true;
    [SerializeField] private Player p;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6 || collision.gameObject.layer == 7)
        {
            collision.gameObject.GetComponentInParent<Player>().TakeDamage(canBeBlocked, p.CurrentDMG);
        }
    }
}
