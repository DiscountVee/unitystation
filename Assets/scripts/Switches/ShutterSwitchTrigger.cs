﻿using InputControl;
using PlayGroup;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShutterSwitchTrigger: InputTrigger
{

    public ShutterController[] shutters;

    [SyncVar(hook = "SyncShutters")]
	public bool IsClosed;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

		if (IsClosed) {
			CloseShutters();
		}
    }
		
    public override void Interact()
    {
		Debug.Log("INTERACT!");
        if (!this.animator.GetCurrentAnimatorStateInfo(0).IsName("Switches_ShuttersUP"))
        {
            PlayerManager.LocalPlayerScript.playerNetworkActions.CmdToggleShutters(gameObject);
        }
    }

    void SyncShutters(bool isClosed)
    {
        if (!isClosed)
        {
            OpenShutters();
        }
        else
        {
            CloseShutters();
       
        }
    }

    void OpenShutters()
    {
        foreach (var s in shutters)
        {
            s.Open();
        }
        animator.SetTrigger("activated");
    }

    void CloseShutters()
    {
        foreach (var s in shutters)
        {
            s.Close();
        }
        animator.SetTrigger("activated");
    }
}
