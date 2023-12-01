using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeet : MonoBehaviour
{
    private AudioSource Source;
    private Player player;
    private void Start()
    {
        Source = GetComponent<AudioSource>();
        player = GetComponentInParent<Player>();
        Source.clip = player.SFXsteps[(int)Random.Range(0f, player.SFXsteps.Length - 1)];
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            Debug.Log("Ran");
            Source.clip = player.SFXsteps[(int)Random.Range(0f, player.SFXsteps.Length - 1)];
            Source.Play();
        }
    }
}
