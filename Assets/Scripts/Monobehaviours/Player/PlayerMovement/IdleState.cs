using System.Collections;
using UnityEngine;
using ProjectE.InputScope;

namespace ProjectE.PlayerScope.PlayerMovement
{
    public class IdleState : PlayerMovementState
    {
        public IdleState(Player player, PlayerMovementStateMachine stateMachine, Rigidbody playerRigidbody, MonoBehaviour monoBehaviour) : 
            base(player, stateMachine, playerRigidbody, monoBehaviour) { }

        public override void Enter()
        {
            base.Enter();
            player.SetAnimationBool(idleParam, true);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void HandleInput()
        {
            base.HandleInput();
            if (Inputs.GetTouchCount() > 0) stateMachine.ChangeState(player.movingS);
        }

        public override void Exit()
        {
            base.Exit();
            player.SetAnimationBool(idleParam, false);
        }
    }
}