using System.Collections;
using UnityEngine;

namespace ProjectE.PlayerScope.PlayerMovement
{
    public class DashState : PlayerMovementState
    {
        public DashState(Player player, PlayerMovementStateMachine stateMachine, Rigidbody playerRigidbody, MonoBehaviour monoBehaviour) : 
            base(player, stateMachine, playerRigidbody, monoBehaviour) { }

        public override void Enter()
        {
            base.Enter();
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
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}