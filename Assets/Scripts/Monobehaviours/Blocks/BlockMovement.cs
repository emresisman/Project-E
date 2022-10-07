using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectE
{
    public class BlockMovement : MonoBehaviour
    {
        private Rigidbody myRigidbody;

        private void Awake()
        {
            myRigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            myRigidbody.velocity = Vector3.zero;
        }
    }
}