using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] private Vector3 offset;

    private Player[] players;

    GameObject temp;
    void Start()
    {
        players = FindObjectsOfType<Player>();
        Debug.Log(players[0].name + ", " + players[1].name);
    }
    void Update()
    {
        Vector3 pos = (players[0].transform.position + players[1].transform.position) * 0.5f;
        transform.localPosition = pos + offset;
        transform.right = (transform.position - players[0].transform.position).normalized;
        transform.GetChild(0).right = (transform.GetChild(0).position - transform.position).normalized;
        //transform.forward = transform.GetChild(0).right;
    }
}
