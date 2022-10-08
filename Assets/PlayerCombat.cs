using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public enum InputAttack { Light, Heavy, Pause}

public class PlayerCombat : MonoBehaviour
{

    [SerializeField]
    private InputActionReference light, heavy;
    [HideInInspector]
    public List<InputAttack> CurrentComboBricks;

    public DamageEnemy Weapon;
    public Text InputText;
    public Animator WeaponAnim;
    public float minPause = 0.6f;
    public float maxPause = 0.9f;
    public SliderTimer timer;

    bool combosStarted = false;
    private int attackNum = 0;
    private float lastAttackTime = 0;
    private Animator anim;
    public List<Combo> Combos;
    public float allowedInputTime = 0.75f;
    //bool isCharging;
    //float chargeSpeed;
    //float chargeTime;
    // Start is called before the first frame update


    void Start()
    {
        Combos = new List<Combo>();

        Combo lightCombo = new Combo(new List<InputAttack> { InputAttack.Light}, 10);
        lightCombo.Name = "Light";
        Combos.Add(lightCombo);
        Combo heavyCombo = new Combo(new List<InputAttack> { InputAttack.Heavy}, 10);
        heavyCombo.Name = "Heavy";
        Combos.Add(heavyCombo);




        Combo combo1 = new Combo(new List<InputAttack> { InputAttack.Light, InputAttack.Light }, 50);
        combo1.Name = "Combo1";
        Combos.Add(combo1);
        Combo combo2 = new Combo(new List<InputAttack> { InputAttack.Heavy, InputAttack.Heavy }, 50);
        combo2.Name = "Combo2";
        Combos.Add(combo2);
        Combo combo3 = new Combo(new List<InputAttack> { InputAttack.Light, InputAttack.Heavy }, 50);
        combo3.Name = "Combo3";
        Combos.Add(combo3);
        Combo combo4 = new Combo(new List<InputAttack> { InputAttack.Heavy, InputAttack.Light }, 50);
        combo4.Name = "Combo4";
        Combos.Add(combo4);

        anim = GetComponent<Animator>();
        CurrentComboBricks = new List<InputAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        float canCancelComboTime = lastAttackTime + maxPause;
        if (Time.time > canCancelComboTime/* && combosStarted*/)
        {
            ClearCurrentCombo();
        }
        if (CurrentComboBricks.Count != 0)
        {
            if (Time.time > lastAttackTime + minPause && Time.time < lastAttackTime + maxPause && CurrentComboBricks.Last() != InputAttack.Pause)
            {
                CurrentComboBricks.Add(InputAttack.Pause);
                UpdateText();
            }
        }

        if (!WeaponAnim.GetCurrentAnimatorStateInfo(0).IsName("AttackIdle") && WeaponAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < WeaponAnim.GetCurrentAnimatorStateInfo(0).length * allowedInputTime)
        {
            return;
        }
 
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            
            CurrentComboBricks.Add(InputAttack.Light);
            UpdateText();
            Attack();
        } 
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            
            CurrentComboBricks.Add(InputAttack.Heavy);
            UpdateText();
            Attack();
        }
        //if (Input.GetKey(KeyCode.Mouse5) && chargeTime < 2)
        //{
        //    isCharging = true;
        //    chargeTime += Time.deltaTime * chargeSpeed;
        //}
    }

    //void ReleaseCharge()
    //{
    //    Attack();
    //    chargeTime = 0;
    //    isCharging = false;
    //}

    public void UpdateText()
    {
        string a = "";
        foreach (InputAttack brick in CurrentComboBricks)
        {
            a += brick.ToString() + "\n";
        }
        InputText.text = a;
    }

    public bool CompareCombos(List<InputAttack> current, List<InputAttack> combo)
    {
        if (current.Count == 0)
        {
            return false;
        }
        if (current.Count > combo.Count)
        {
            return false;
        }
        for (int i = 0; i < current.Count; i++)
        {
            if (current[i] != combo[i])
            {
                return false;
            }
        }
        return true;
    }

    public Combo FindCurrentCombo()
    {
        foreach (Combo combo in Combos)
        {
            if (CompareCombos(CurrentComboBricks, combo.ComboBricks))
            {
                return combo;
            }
        }
        return null;
    }

    public void Attack()
    {
        combosStarted = true;
        
        Combo currCombo = FindCurrentCombo();
        if (currCombo == null)
        {
            Debug.Log("Not a combo");
            InputAttack attack = CurrentComboBricks.Last();
            ClearCurrentCombo();
            CurrentComboBricks.Add(attack);
            currCombo = FindCurrentCombo();
        }

        StartCoroutine(ExecuteAttack(currCombo.Name));
        Debug.Log(currCombo.Name);
        UpdateText();


    }

    public void ClearCurrentCombo()
    {
        CurrentComboBricks.Clear();
        UpdateText();
    }


    IEnumerator ExecuteAttack(string name)
    {
        //WeaponAnim.CrossFade(name, 0, 0);
        WeaponAnim.SetTrigger(name);
        yield return new WaitForEndOfFrame();
        lastAttackTime = Time.time + WeaponAnim.GetCurrentAnimatorStateInfo(0).length;
        StartCoroutine(WaitForAnimationToFinnish(WeaponAnim.GetCurrentAnimatorStateInfo(0).length));
    }
    IEnumerator WaitForAnimationToFinnish(float time)
    {
        yield return new WaitForSeconds(time);
        timer.StartLoading(minPause);
    }


}
