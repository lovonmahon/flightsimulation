using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AirSimulator
{    
    public class Rocket : WeaponSystem, IWeapons
    {
        [SerializeField] float _fireRate;
        float _lastTimeFired;

        void Start()
        {
            _lastTimeFired = 0f;
        }
        void OnEnable()
        {
            WeaponsInput.fireAction += Fire;
        }
        void OnDisable()
        {
            WeaponsInput.fireAction -= Fire;
        }

        public override void Fire()
        {
            FireWeapon();
            // if(Time.time > (_lastTimeFired + _fireRate))
            // {
            //     //get available projectile from pool
            //     //set and enable projectile at origin
            //     FireWeapon();
                
            //     _lastTimeFired = Time.time;
            // }
        }
    }
}
