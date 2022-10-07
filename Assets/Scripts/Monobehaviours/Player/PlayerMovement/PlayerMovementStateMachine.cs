using System.Collections;
using UnityEngine;

namespace ProjectE.PlayerScope.PlayerMovement
{
    public class PlayerMovementStateMachine
    {
        public PlayerMovementState CurrentState { get; private set; }

        public void Initialize(PlayerMovementState startingState)
        {
            CurrentState = startingState;
            startingState.Enter();
        }

        public void ChangeState(PlayerMovementState newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            newState.Enter();
        }

        public bool IsState(PlayerMovementState state)
        {
            return state == CurrentState;
        }
    }
}