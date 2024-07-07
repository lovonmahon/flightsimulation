using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Mover : MonoBehaviour
{
    Animator _animator;
    UnityEngine.AI.NavMeshAgent _agent;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        UpdateAnimator();
    }
    void UpdateAnimator()
        {
            //First get global velocity on navmesh agent
            Vector3 velocity = _agent.velocity;
            //convert to local velocity
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            //Which direction of interest for movement
            float speed = localVelocity.z;
            float turning = localVelocity.x;
            //Influence the float parameter on the animator by feding it the speed values from the local velocity.
            _animator.SetFloat("forwardSpeed", speed);
            _animator.SetFloat("turning", turning);
        }
}
