using UnityEngine;
using System.Collections;

public class JumpOnInput : StateMachineBehaviour {

    [HideInInspector]
    public InputManager inputManager;

    [HideInInspector]
    public Player player;

    public Inputs input = Inputs.JUMP;
    public bool applyForceOnStart;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (this.player == null)
        {
            this.player = animator.gameObject.GetComponent<Player>();
        }
	    if (this.inputManager == null)
        {
            this.inputManager = animator.gameObject.GetComponent<InputManager>();
        }

        if (this.applyForceOnStart)
        {
            this.Jump(animator, InputActions.PRESSED);
        }
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	    if (this.inputManager == null)
        {
            Debug.LogWarning("InputManager not found for" + animator.gameObject);
            return;
        }

        this.Jump(animator, this.inputManager.GetInput(input));
	}

    private void Jump(Animator animator, InputActions action)
    {
        if (this.player == null)
        {
            Debug.LogWarning("Player not found for " + animator.gameObject);
            return;
        }

        this.player.Jump(action);
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
