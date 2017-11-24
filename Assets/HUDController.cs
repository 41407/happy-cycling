using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HUDController : MonoBehaviour
{

    private static HUDController instance;
    private Animator anim;
    public enum State
    {
        Gameplay,
        Cutscene,
    }

    public static State CurrentState
    {
        set
        {
            instance.anim.SetTrigger(value == State.Gameplay ? "Gameplay" : "Cutscene");
        }
    }

    void Awake()
    {
        instance = Component.FindObjectOfType<HUDController>();
        anim = GetComponent<Animator>();
    }
}
