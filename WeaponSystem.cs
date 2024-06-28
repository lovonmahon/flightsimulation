using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AirSimulator
{
    public class WeaponSystem : MonoBehaviour, IWeapons
    {
        [SerializeField] AudioSource _audioSource;
        [SerializeField] Transform _muzzle;
        [SerializeField] int _projectileMaxAmount;
        int _ammoCount = 8;

        void Start()
        {
            _ammoCount = 8;
        }
        void Update()
        {
            Reload();
        }
        public virtual void Fire()
        {
            
        }
        protected void FireWeapon()
        {
            if(_ammoCount > 0 )
            {
                GameObject weaponFired = WeaponProjectilePool.Instance.ProjectileAvailable();

                if(weaponFired != null)
                {
                    weaponFired.SetActive(true);
                    weaponFired.transform.position = _muzzle.position;
                    weaponFired.transform.rotation = _muzzle.rotation;
                    
                    _ammoCount--;
                }
                else
                {
                    Debug.Log("No projectile available");
                }
            }
            _ammoCount = Mathf.Clamp(_ammoCount, 0, _projectileMaxAmount - 1);
        }
        public virtual void Reload()
        {
            if(_ammoCount <= 0)
            {
                ResetAmmo();
            }
        }
        protected void ResetAmmo()
        {
            _ammoCount = _projectileMaxAmount - 1;
        }
    }
}
