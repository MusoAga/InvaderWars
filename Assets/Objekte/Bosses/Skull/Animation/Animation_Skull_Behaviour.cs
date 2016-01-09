using UnityEngine;
using System.Collections;

public class Animation_Skull_Behaviour : StateMachineBehaviour {

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        if (stateInfo.IsName("Skull_Idle"))
            animator.GetComponent<Enemy_Skull>().idle();
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (stateInfo.IsName("Skull_ShootBeam"))
            animator.GetComponent<Enemy_Skull>().shootBeam();
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        if (stateInfo.IsName("Skull_ChargeLaser"))
            animator.GetComponent<Enemy_Skull>().shootLaser();
        if (stateInfo.IsName("Skull_ShootLaser"))
            animator.GetComponent<Enemy_Skull>().idle();


        if (stateInfo.IsName("Break"))
            animator.GetComponent<Enemy_Skull>().onDestroy();
    }

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
