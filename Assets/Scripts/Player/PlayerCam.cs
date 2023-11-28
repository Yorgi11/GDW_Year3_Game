using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] private float maxDist;
    [SerializeField] private float camSpeed;
    [SerializeField] private float camRotSpeed;
    [SerializeField] private Vector3 camOffset;
    private Player[] players;
    void Start()
    {
        players = FindObjectsOfType<Player>();
        Debug.Log(players[0].name + ", " + players[1].name);
    }
    void Update()
    {
        Vector3 pos = (players[0].transform.position + players[1].transform.position) * 0.5f;
        transform.parent.position = pos;
        transform.localPosition = Vector3.Lerp(transform.localPosition, camOffset, camSpeed * Time.deltaTime);

        transform.parent.rotation = Quaternion.Slerp(transform.parent.rotation, Quaternion.LookRotation((players[1].transform.position - pos).normalized, Vector3.up), camRotSpeed * Time.deltaTime);
        //transform.parent.forward = Vector3.Slerp(transform.parent.forward, (players[1].transform.position - transform.parent.position).normalized, camRotSpeed * Time.deltaTime);
        players[0].transform.forward = Vector3.Lerp(players[0].transform.forward, (players[1].transform.position - players[0].transform.position).normalized, players[0].RotateSpeed * Time.deltaTime);
        players[1].transform.forward = Vector3.Lerp(players[1].transform.forward, (players[0].transform.position - players[1].transform.position).normalized, players[1].RotateSpeed * Time.deltaTime);

        if (Vector3.Distance(players[0].transform.position, players[1].transform.position) > maxDist) { players[0].transform.position += players[0].transform.forward * Time.deltaTime; players[1].transform.position += players[1].transform.forward * Time.deltaTime; }
    }
}
