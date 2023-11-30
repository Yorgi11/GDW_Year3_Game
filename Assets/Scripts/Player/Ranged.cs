using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : MonoBehaviour
{
    [SerializeField] private Transform obj;
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    public void StartMoveToTarget(AnimationClip c)
    {
        obj.gameObject.SetActive(true);
        StartCoroutine(MoveToTarget(c.length / speed));
    }
    private IEnumerator MoveToTarget(float time)
    {
        float t = 0f;
        float dist = Vector3.Distance(obj.position, target.position);
        while (t <= time * 0.5f) { }
        while (t <= time)
        {
            t += Time.deltaTime;
            float Delta = ((dist / time) * t) - (0.5f * time);
            obj.position = Vector3.Lerp(obj.position, target.position, Delta);
            yield return null;
        }
        obj.position = transform.position;
    }
}
