using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladesRotorController : MonoBehaviour
{
    public enum Axis {x, y, z}
    public Axis rotationAxis;
    float _bladeSpeed;
    public float BladeSpeed
    {
        get
        {
            return _bladeSpeed;
        }
        set
        {
            _bladeSpeed = Mathf.Clamp(value, 0, 3000);
        }
    }

    public bool inverseRotation = false;
    Vector3 _Rotation;
    float _rotateDegree;

    void Update()
    {
        HandlePropellerRotation();
    }
    void HandlePropellerRotation()
    {
        if(inverseRotation)
        {
            _rotateDegree -= _bladeSpeed * Time.deltaTime;
        }
        else
        {
            _rotateDegree += _bladeSpeed * Time.deltaTime;
        }
        //Fractionate by 1 degree intervals
        _rotateDegree = _rotateDegree%360;

        switch(rotationAxis)
        {
            case Axis.y:
                transform.localRotation = Quaternion.Euler(_Rotation.x, _rotateDegree, _Rotation.z);
                break;
            case Axis.x:
                transform.localRotation = Quaternion.Euler(_rotateDegree, _Rotation.y, _Rotation.z);
                break;
            case Axis.z:
                transform.localRotation = Quaternion.Euler(_Rotation.x, _Rotation.y, _rotateDegree);
                break;

        }
    }
}
