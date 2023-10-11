using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    [SerializeField] private Animator ani;
    [SerializeField] private AnimationClip[] clips;
    [SerializeField] private float[] speeds;

    private bool playing = false;
    public IEnumerator PlayAnimation(int i)
    {
        playing = true;
        ani.SetBool(clips[i].name, true);
        yield return new WaitForSeconds(clips[i].length / speeds[i]);
        ani.SetBool(clips[i].name, false);
        playing = false;
    }
    public bool IsPlaying
    {
        get { return playing; }
    }
}
