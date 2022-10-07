using System.Collections;
using UnityEngine;

namespace ProjectE.PlayerScope.PlayerMovement
{
    public class StunState : PlayerMovementState
    {
        private float stunTime;
        private float currentTime;

        public StunState(Player player, PlayerMovementStateMachine stateMachine, Rigidbody playerRigidbody, MonoBehaviour monoBehaviour) :
            base(player, stateMachine, playerRigidbody, monoBehaviour) { }

        public override void Enter()
        {
            base.Enter();
            currentTime = 0f;
            player.SetAnimationBool(stunParam, true);
            stunTime = player.CalculatedStunTime;
            OpenStunPanel();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            StunTimerScreen.instance.SetTimerText(stunTime - currentTime);
            if (currentTime >= stunTime) stateMachine.ChangeState(player.idleS);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            currentTime += Time.fixedDeltaTime;
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void Exit()
        {
            base.Exit();
            player.SetAnimationBool(stunParam, false);
            CloseStunPanel();
        }

        public void AddStunTime(float time)
        {
            stunTime += time;
        }

        public void OpenStunPanel()
        {
            UIManager.instance.ShowScreen(StunTimerScreen.instance);
        }

        public void CloseStunPanel()
        {
            UIManager.instance.HideScreen(StunTimerScreen.instance);
        }
    }
}