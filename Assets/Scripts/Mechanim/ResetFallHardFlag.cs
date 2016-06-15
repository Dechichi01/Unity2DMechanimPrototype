using UnityEngine;

public class ResetFallHardFlag : StateMachineBehaviour
{

    [HideInInspector]
    public Player player;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (this.player == null)
        {
            this.player = animator.gameObject.GetComponent<Player>();
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (this.player == null)
        {
            Debug.LogWarning("Player not found for " + animator.gameObject);
            return;
        }
        animator.SetBool("FallHardFlag", false);
    }

}
