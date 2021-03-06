﻿using UnityEngine;
using System.Collections;

public class CheckInput : StateMachineBehaviour {

    public Inputs inputToCheck;

    [HideInInspector]
    public InputManager inputManager;

    private bool isLanding = false; 

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (this.inputManager == null)
        {
            this.inputManager = animator.gameObject.GetComponent<InputManager>();
        }	
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (this.inputManager == null)
        {
            Debug.LogWarning("InputManager not found for " + animator.gameObject);
            return;
        }

        /*AnimatorStateInfo animatorInfo = animator.GetCurrentAnimatorStateInfo(0);
        isLanding = animatorInfo.IsName("Fall-End-Hard") || animatorInfo.IsName("Fall-End");*/
        
        animator.SetInteger(string.Format("{0}_Input", inputToCheck), (int)this.inputManager.GetInput(inputToCheck));
     
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
