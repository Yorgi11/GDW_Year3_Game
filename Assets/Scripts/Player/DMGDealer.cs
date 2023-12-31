using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMGDealer : MonoBehaviour
{
    [SerializeField] private bool canBeBlocked = true;
    [SerializeField] private LayerMask mask;

    private Player player;

    private bool canAttack = false;
    private float d;
    public float timeSegment = 0f;
    private void Start()
    {
        player = GetComponentInParent<Player>();
    }
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
    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.layer == 6 || other.gameObject.layer == 7) && canAttack)
        {
            UseAttack(other.gameObject.GetComponentInParent<Player>());
            player.Source.clip = player.SFXAtk[(int)Random.Range(0f, player.SFXAtk.Length - 1)];
            player.Source.Play();
        }
    }
}
