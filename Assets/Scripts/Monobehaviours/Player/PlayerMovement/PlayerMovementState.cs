using System.Collections;
using UnityEngine;
using ProjectE.PlayerScope;

namespace ProjectE.PlayerScope.PlayerMovement
{
    public abstract class PlayerMovementState
    {
        protected Player player;
        protected PlayerMovementStateMachine stateMachine;
        protected Rigidbody playerRigidbody;
        protected MonoBehaviour monoBehaviour;

        protected int idleParam = Animator.StringToHash("idle");
        protected int runParam = Animator.StringToHash("run");
        protected int stunParam = Animator.StringToHash("stun");

        protected PlayerMovementState(Player player, PlayerMovementStateMachine stateMachine, Rigidbody playerRigidbody, MonoBehaviour monoBehaviour)
        {
            this.player = player;
            this.stateMachine = stateMachine;
            this.playerRigidbody = playerRigidbody;
            this.monoBehaviour = monoBehaviour;
        }

        public virtual void Enter()
        {
            ResetVelocity();
        }

        public virtual void LogicUpdate()
        {

        }

        public virtual void PhysicsUpdate()
        {
            player.SetBlockAreaPosition();
        }

        public virtual void HandleInput()
        {

        }

        public virtual void Exit()
        {
            ResetVelocity();
        }

        public virtual void ResetVelocity()
        {
            playerRigidbody.velocity = Vector3.zero;
        }
    }
}