using UnityEngine;

public class ResetAnimatorInteger : StateMachineBehaviour
{
    public string parameter;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger(parameter, 0);
        Debug.Log("RESET " + parameter + " " + animator.GetInteger(parameter));
    }

}
