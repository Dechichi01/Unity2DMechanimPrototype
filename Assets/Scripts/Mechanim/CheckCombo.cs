using UnityEngine;
using System.Collections;

public class CheckCombo : StateMachineBehaviour {

    public Inputs input;
    public InputActions onAction;
    public int setValue;

    [HideInInspector]
    public InputManager inputManager;
    [HideInInspector]
    public string paramName = "ComboCounter";

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
        if (animator.GetInteger(paramName) == 0)
        {
            InputActions inputAction = this.inputManager.GetInput(input);
            int nextValue = inputAction == onAction ? setValue : 0;
            animator.SetInteger(paramName, nextValue);
        }
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
