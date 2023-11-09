using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    [SerializeField] private Animator ani;
    [SerializeField] private AnimationClip[] clips;
    [SerializeField] private float[] speeds;

    private bool playing = false;

    private string currentAnimation = "";
    public IEnumerator PlayAnimation(int i)
    {
        playing = true;
        currentAnimation = clips[i].name;
        ani.SetBool(currentAnimation, true);
        yield return new WaitForSeconds(clips[i].length * speeds[i]);
        ani.SetBool(currentAnimation, false);
        playing = false;
        currentAnimation = "";
    }
    public void SetAnimation(string name, bool state)
    {
        currentAnimation = name;
        ani.SetBool(name, state);
    }
    public bool IsPlaying
    {
        get { return playing; }
    }
    public string CurrentAni
    {
        get { return currentAnimation; }
    }
}
