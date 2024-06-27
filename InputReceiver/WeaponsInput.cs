using System;
using System.Collections.Generic;
using UnityEngine;

namespace AirSimulator
{    
    public class WeaponsInput : MonoBehaviour
    {
        public static Action fireAction;
        [SerializeField] float _inputCooldown;
        float _timer;
        void Update()
        {
            _timer += Time.deltaTime;

            if(Input.GetMouseButtonDown(0))
            {
                if(_timer >= _inputCooldown)
                {
                    fireAction?.Invoke();
                    _timer = 0f;
                }
            }
        }
    }
}
