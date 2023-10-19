using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private string Name;
    [SerializeField] private bool canBeBlocked = true;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private ParticleSystem p;
    [SerializeField] private ParticleSystem destroyedParticle;

    private Vector3 dir = Vector3.positiveInfinity;

    private GameObject g;
    private void Awake()
    {
        gameObject.name = "Projectile " + Name;
        g = Instantiate(p, transform.position, Quaternion.identity, transform).gameObject;
        Destroy(gameObject, 1f);
    }
    private void Update()
    {
        if (dir != Vector3.positiveInfinity)
        {
            g.layer = gameObject.layer;
            GetComponent<Rigidbody>().AddForce(dir.normalized * speed);
        }
    }
    private void DestroyAttack(Player p)
    {
        p.TakeDamage(canBeBlocked, damage);
        /*p.Stop();
        Instantiate(destroyedParticle, transform.position, Quaternion.identity, transform);
        Destroy(gameObject, destroyedParticle.main.duration);*/
        Destroy(gameObject);
    }

    public Vector3 Direction
    {
        set { dir = value; }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != gameObject.layer) DestroyAttack(collision.gameObject.GetComponentInParent<Player>());
    }
}
