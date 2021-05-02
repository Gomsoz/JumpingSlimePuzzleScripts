using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SlimeBehavior : MonoBehaviour
{
    private static Animator slimeAnimator;

    private static Defines.SlimeState _slimeState;
    public static Defines.SlimeState SlimeState { get { return _slimeState; } }

    private void Start()
    {
        SlimeBehaviorInit();
    }

    private void Update()
    {
        
    }

    public void SlimeBehaviorInit()
    { 
        slimeAnimator = GetComponent<Animator>();
        GameManager.GameMgr.SuccessBlock += SlimeJump;
        _slimeState = Defines.SlimeState.Idle;
    }

    public void SlimeJump(bool evt)
    {
        if(evt)
            slimeAnimator.Play("Jump");
    }
}
