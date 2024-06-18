using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterControllerBasic : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField] float _responsiveness;
    [SerializeField] float _throttleIncreaseAmount;//Amount to increase or decrease by
    public Transform rotorBlades;
    [SerializeField] float _rotorRotationModifier;
    float _throttle = 1;
    AudioSource _audio;
    Vector3 _flatForward;
    Vector3 _flatRight;
    float forwardDot;
    float rightDot;
    [SerializeField]
    float autoLevelForce = 2f;

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
        CalculateAngles();
        // AutoLevel(_rb);
        if(Input.GetKey(KeyCode.Z))
        {
            _pitch = 0f;
        }    
    }
    void ReadInput()
    {
        _roll = Input.GetAxis("Roll");
        _pitch = Input.GetAxis("Pitch");
        _yaw = Input.GetAxis("Yaw");

        Debug.Log($"Roll : {_roll}");
        Debug.Log($"Pitch : {_pitch}");
        Debug.Log($"Yaw : {_yaw}");

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
    void CalculateAngles()
    {
        _flatForward = transform.forward;
        //zero the y axis to atraighten the transform
        _flatForward.y = 0f;
        _flatForward = _flatForward.normalized;
        Debug.DrawRay(transform.position, _flatForward, Color.blue );

        _flatRight = transform.right;
        _flatRight.y = 0f;
        _flatRight = _flatRight.normalized;
        Debug.DrawRay(transform.position, _flatRight, Color.red );

        forwardDot = Vector3.Dot(transform.up, _flatForward);
        rightDot = Vector3.Dot(transform.up, _flatRight);
    }
    // void AutoLevel(Rigidbody rb)
    // {
    //     float rightForce = forwardDot * _responsiveness;
    //     float forwardForce = rightDot * _responsiveness;

    //     rb.AddTorque(transform.right * rightForce, ForceMode.Impulse);
    //     rb.AddTorque(transform.forward * forwardForce, ForceMode.Impulse);
    // }
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
