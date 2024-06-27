using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AirSimulator
{    
    public class WeaponProjectilePool : MonoBehaviour
    {
        public static WeaponProjectilePool Instance;
        [SerializeField] GameObject _projectile;
        [SerializeField] int _numProjectile;
        List<GameObject> _projectileList = new List<GameObject>();
        
        void Awake()
        {
            if( Instance == null) Instance = this;
        }
        void Start()
        {
            for(int i = 0; i < _numProjectile; i++)
            {
                GameObject GO = Instantiate(_projectile, transform.position, Quaternion.identity);
                GO.SetActive(false);
                _projectileList.Add(GO);
            }
        }

        public GameObject ProjectileAvailable()
        {
            for(int i = 0; i < _projectileList.Count; i++)
            {
               if(!_projectileList[i].activeInHierarchy)
               {
                    return _projectileList[i];
               }
            }
            return null;
        }
    }
}
