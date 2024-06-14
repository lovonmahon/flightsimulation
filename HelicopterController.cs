using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterController : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField] float _responsiveness;
    [SerializeField] float _throttleIncreaseAmount;//Amount to increase or decrease by
    public Transform rotorBlades;
    [SerializeField] float _rotorRotationModifier;
    float _throttle = 1;
    AudioSource _audio;

    float _roll; 
    float _pitch; 
    float _yaw;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _audio = GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        _rb.AddForce(transform.up * _throttle, ForceMode.Impulse);
        _rb.AddTorque(-transform.right * _pitch * _responsiveness);
        _rb.AddTorque(transform.forward * _roll * _responsiveness);
        _rb.AddTorque(transform.up * _yaw * _responsiveness);
    }
    
    void Update()
    {
        ReadInput();
        RotorRotate();
        HelicopterEngineSound();
    }
    void ReadInput()
    {
        
        _roll = Input.GetAxis("Roll");
        _pitch = Input.GetAxis("Pitch");
        _yaw = Input.GetAxis("Yaw");

        if(Input.GetKey(KeyCode.Space))
        {
            _throttle += Time.deltaTime * _throttleIncreaseAmount;
        }
        else if(Input.GetKey(KeyCode.LeftShift))
        {
            _throttle -= Time.deltaTime * _throttleIncreaseAmount;
        }

        _throttle = Mathf.Clamp(_throttle, 5f, 100f);
    }
    void RotorRotate()
    {
        rotorBlades.Rotate(Vector3.forward * _throttle * _rotorRotationModifier);
    }
    void HelicopterEngineSound()
    {
        _audio.volume = (_throttle * 0.1f);
        _audio.volume = Mathf.Clamp(_audio.volume, 0.1f, 1f);
    }
}
