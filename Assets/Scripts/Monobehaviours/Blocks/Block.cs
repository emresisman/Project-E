using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectE
{
    public class Block : MonoBehaviour
    {
        public float StunTime { get => stunTime; }
        private float stunTime = 2f;

        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            StartCoroutine(DestroyAfterOneSecond()); 
        }

        IEnumerator DestroyAfterOneSecond()
        {
            yield return new WaitForSeconds(0.5f);
            BlockManager.Instance.DestroyBlock(this);
        }

        public void ResetVelocity()
        {
            rb.velocity = Vector3.zero;
        }
    }
}