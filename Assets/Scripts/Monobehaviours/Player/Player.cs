using System.Collections;
using UnityEngine;
using ProjectE.PlayerScope.PlayerMovement;
using ProjectE.Managers;

namespace ProjectE.PlayerScope
{
    public class Player : Singleton<Player>
    {
        public PlayerMovementStateMachine stateM;
        public MovingState movingS;
        public DashState dashS;
        public StunState stunS;
        public IdleState idleS;

        public float Speed { get => speed; }
        public Camera PlayerCam { get => playerCam; }
        public LayerMask GroundLayer { get => _groundLayer; }
        public float CalculatedStunTime { get => calculatedStunTime; }
        public BoxCollider BlockFallArea { get => blockFallArea; }

        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private Camera playerCam;
        [SerializeField] private BoxCollider blockFallArea;
        
        private float speed = 2f;
        private float stunResist = 0f;
        private float calculatedStunTime;
        private Rigidbody myRigidbody;
        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
            myRigidbody = GetComponent<Rigidbody>();
            stateM = new PlayerMovementStateMachine();

            movingS = new MovingState(this, stateM, myRigidbody, this);
            dashS = new DashState(this, stateM, myRigidbody, this);
            stunS = new StunState(this, stateM, myRigidbody, this);
            idleS = new IdleState(this, stateM, myRigidbody, this);
            stateM.Initialize(idleS);
        }

        private void Update()
        {
            stateM.CurrentState.HandleInput();
            stateM.CurrentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            stateM.CurrentState.PhysicsUpdate();
        }

        private void OnCollisionEnter(Collision collision)
        {
            var col = collision.transform.tag;
            switch (col)
            {
                case "Finish":
                    //collision.gameObject.GetComponent<Enemy>().Attack(this);
                    break;
                case "Block":
                    Debug.Log("Hit the block.");
                    StunPlayer(collision.gameObject.GetComponent<Block>().StunTime);
                    break;
            }
        }

        private void StunPlayer(float time)
        {
            CalculateStunTime(time);
            if (!stateM.IsState(stunS)) stateM.ChangeState(stunS);
            else stunS.AddStunTime(time / 2f);
        }

        public void SetAnimationBool(int anim, bool value)
        {
            animator.SetBool(anim, value);
        }

        private void CalculateStunTime(float stunTime)
        {
            calculatedStunTime = stunTime - stunResist;
        }

        public void SetBlockAreaPosition()
        {
            blockFallArea.center = myRigidbody.velocity;
        }
    }
}