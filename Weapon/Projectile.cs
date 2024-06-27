using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AirSimulator
{    
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        Rigidbody _rb;
        [SerializeField] float _speed;
        [SerializeField] float _lifetime;
        float _startTime;
        void Start()
        {
            _startTime = 0f;
            _rb = GetComponent<Rigidbody>();
        }
        void FixedUpdate()
        {
            Vector3 velocity = _rb.velocity;
            _rb.AddRelativeForce(Vector3.up * _speed, ForceMode.Impulse);
        }
        void Update()
        {
            _startTime += Time.deltaTime;
          
            if(_startTime > _lifetime)
            {
                _startTime = 0f;
                _rb.velocity = Vector3.zero;
                this.gameObject.SetActive(false);
            }
        }
    }
}
