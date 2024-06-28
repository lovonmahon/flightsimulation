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
            //Get direction via raycast? 
            //Currently, projectile's directions only updates when forward key is pressed.
            
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
        void OnTriggerEnter(Collider col)
        {
            if(col.GetComponent<ImpactObject>())
            {
                RaycastHit hit;
                if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
                {
                    //play explosion at hit point then disable go
                    GameObject vfx = VFXPool.Instance.IsVFXAvailable();
                    if(vfx != null)
                    {
                        vfx.transform.position = hit.point;
                        vfx.SetActive(true);
                        Debug.Log($"{vfx.transform.gameObject.name}");
                        this.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
