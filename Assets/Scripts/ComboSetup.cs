using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum AttackType { Y = 0, X = 1, A = 2, B = 3 };
public class ComboSetup : MonoBehaviour
{
    [Header("Hands")]
    [SerializeField] private DMGDealer LeftHand;
    [SerializeField] private DMGDealer RightHand;

    [Header("Attacks")]
    public Attack Y_Attack;
    public Attack X_Attack;
    public Attack A_Attack;
    public Attack B_Attack;
    public List<Combo> combos;
    public float comboLeeway = 0.2f;

    [Header("Components")]
    public Animator ani;
    public InputManager im;

    Attack curAttack = null;
    ComboInput lastInput = null;
    List<int> currentCombos = new List<int>();
    float timer = 0f;
    float leeway = 0f;
    bool skip = false;
    void Start()
    {
        if (ani == null) ani = GetComponent<Animator>();
        PrimeCombos();
    }
    void PrimeCombos()
    {
        for (int i = 0; i < combos.Count; i++)
        {
            Combo c = combos[i];
            c.onInputted.AddListener(() =>
            {
                skip = true;
                Attack(c.comboAttack);
                ResetCombos();
            });
        }
    }
    void Update()
    {
        if (curAttack != null)
        {
            if (timer > 0f) timer -= Time.deltaTime;
            else curAttack = null;
            ani.SetBool("Attacking", true);
            return;
        }else ani.SetBool("Attacking", false);

        if (currentCombos.Count > 0)
        {
            leeway += Time.deltaTime;
            if (leeway >= comboLeeway)
            {
                if (lastInput != null)
                {
                    Attack(GetAttackFromType(lastInput.type));
                    lastInput = null;
                }
                ResetCombos();
            }
        }
        else leeway = 0f;

        ComboInput input = null;
        if (im.Y) input = new ComboInput(AttackType.Y);
        if (im.X) input = new ComboInput(AttackType.X);
        if (im.A) input = new ComboInput(AttackType.A);
        if (im.B) input = new ComboInput(AttackType.B);

        if (input == null) return;
        lastInput = input;

        List<int> remove = new List<int>();
        for (int i = 0; i < currentCombos.Count; i++)
        {
            if (combos[currentCombos[i]].continueCombo(input)) leeway = 0f;
            else remove.Add(i);
        }

        if (skip)
        {
            skip = false;
            return;
        }

        for (int i = 0; i < combos.Count; i++)
        {
            if (currentCombos.Contains(i)) continue;
            if (combos[i].continueCombo(input))
            {
                currentCombos.Add(i);
                leeway = 0f;
            }
        }

        foreach (int i in remove)
            currentCombos.RemoveAt(i);

        if (currentCombos.Count <= 0) Attack(GetAttackFromType(input.type));
    }

    void ResetCombos()
    {
        leeway = 0f;
        for (int i = 0; i < currentCombos.Count; i++)
        {
            combos[currentCombos[i]].ResetCombo();
        }
        currentCombos.Clear();
    }

    void Attack(Attack att)
    {
        curAttack = att;
        timer = att.length;
        ani.Play(att.name, -1, 0);
        LeftHand.CanDamage = true;
        RightHand.CanDamage = true;
    }

    Attack GetAttackFromType(AttackType t)
    {
        if (t == AttackType.Y) return Y_Attack;
        if (t == AttackType.X) return X_Attack;
        if (t == AttackType.A) return A_Attack;
        if (t == AttackType.B) return B_Attack;
        return null;
    }
}
[System.Serializable]
public class Attack
{
    public string name;
    public float length;
}
[System.Serializable]
public class ComboInput
{
    public AttackType type;

    public ComboInput(AttackType t)
    {
        type = t;
    }

    public bool isSameAs(ComboInput test)
    {
        return (type == test.type);
    }
}
[System.Serializable]
public class Combo
{
    public string name;
    public List<ComboInput> inputs;
    public Attack comboAttack;
    public UnityEvent onInputted;
    int curInput = 0;

    public bool continueCombo(ComboInput i)
    {
        if (inputs[curInput].isSameAs(i))
        {
            curInput++;
            if (curInput >= inputs.Count)
            {
                onInputted.Invoke();
                curInput = 0;
            }
            return true;
        }
        else
        {
            curInput = 0;
            return false;
        }
    }

    public ComboInput CurrentComboInput()
    {
        if (curInput >= inputs.Count) return null;
        return inputs[curInput];
    }

    public void ResetCombo()
    {
        curInput = 0;
    }
}
