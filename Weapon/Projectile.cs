using System;
using System.Collections.Generic;
using UnityEngine;

namespace AirSimulator
{    
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        public static Action PlayExplosion;
        Rigidbody _rb;
        [SerializeField] float _speed;
        [SerializeField] float _lifetime;
        float _startTime;
        RaycastHit hit;
        
        void Start()
        {
            _startTime = 0f;
            _rb = GetComponent<Rigidbody>();
        }
        void FixedUpdate()
        {
            // _rb.AddRelativeForce(Vector3.up * _speed, ForceMode.Impulse);
            _rb.velocity = transform.forward * _speed;
            TargetHitCheck();
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
        void TargetHitCheck()
        {
            Ray ray = new Ray(transform.position, transform.forward * 2f);
            Debug.DrawRay(transform.position, transform.forward * 2f, Color.red);
            //Store hit value for use in OnTriggerEnter
            Physics.Raycast(ray, out hit);
        }
        void OnTriggerEnter(Collider col)
        {
            if(col.GetComponent<ImpactObject>())
            {
                Debug.Log($"Hitting ship @ {Time.time}");
                GameObject vfx = VFXPool.Instance.IsVFXAvailable();
                if(vfx != null)
                {
                    vfx.transform.position = hit.point;
                    vfx.SetActive(true);
                    PlayExplosion?.Invoke();
                    this.gameObject.SetActive(false);
                }
            }
        }
        // void OnTriggerEnter(Collider col)
        // {
        //     if(col.GetComponent<ImpactObject>())
        //     {
        //         Debug.Log("Hitting ship");
        //         RaycastHit hit;
        //         if(Physics.Raycast(transform.position, transform.TransformDirection(transform.forward), out hit))
        //         {
        //             //play explosion at hit point then disable go
        //             GameObject vfx = VFXPool.Instance.IsVFXAvailable();
        //             if(vfx != null)
        //             {
        //                 vfx.transform.position = hit.point;
        //                 vfx.SetActive(true);
        //                 this.gameObject.SetActive(false);
        //             }
        //         }
        //     }
        // }
    }
}
