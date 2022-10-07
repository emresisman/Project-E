using System.Collections;
using UnityEngine;
using ProjectE.InputScope;

namespace ProjectE.PlayerScope.PlayerMovement
{
    public class MovingState : PlayerMovementState
    {
        public MovingState(Player player, PlayerMovementStateMachine stateMachine, Rigidbody playerRigidbody, MonoBehaviour monoBehaviour) : 
            base(player, stateMachine, playerRigidbody, monoBehaviour) { }

        public override void Enter()
        {
            base.Enter();
            player.SetAnimationBool(runParam, true);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            Inputs.GetTouch(0, out Vector2 fingerPos, out TouchPhase touchPhase);
            ConvertScreenPointToWorldPosition(fingerPos, player.GroundLayer, out Vector3 worldPos, out bool isHit);
            var targetVector = worldPos - player.transform.position;
            if (IsFingerPhaseHolding(touchPhase) && isHit)
            {
                MoveFingerPoint(targetVector);
                LookAtMovingDirection(targetVector);
            }
            else ResetVelocity();
        }

        public override void HandleInput()
        {
            base.HandleInput();
            if (Inputs.GetTouchCount() == 0) stateMachine.ChangeState(player.idleS);
        }

        public override void Exit()
        {
            base.Exit();
            player.SetAnimationBool(runParam, false);
        }

        private void MoveFingerPoint(Vector3 fingerPoint)
        {
            playerRigidbody.velocity = fingerPoint.normalized * player.Speed;
        }

        private void LookAtMovingDirection(Vector3 targetVector)
        {
            player.transform.rotation = Quaternion.Euler(0f, Mathf.Atan2(targetVector.x, targetVector.z) * Mathf.Rad2Deg, 0f);
        }

        private void ConvertScreenPointToWorldPosition(Vector2 screenPos, LayerMask layer, out Vector3 hitPoint, out bool hitted)
        {
            RaycastHit hit;
            Ray fingerRay = player.PlayerCam.ScreenPointToRay(screenPos);
            if (Physics.Raycast(ray: fingerRay, hitInfo: out hit, maxDistance: 50f, layerMask: layer))
            {
                hitted = true;
                hitPoint = hit.point;
            }
            else
            {
                hitted = false;
                hitPoint = Vector3.zero;
            }
        }

        private bool IsFingerPhaseHolding(TouchPhase phase)
        {
            return (phase == TouchPhase.Moved || phase == TouchPhase.Stationary) ? true : false;
        }
    }
}