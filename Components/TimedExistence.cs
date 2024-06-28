using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedExistence : MonoBehaviour
{
    float _lifetime;
    float _startTime;
    void Start()
    {
        _lifetime = 3f;
    }

    void Update()
    {
        _startTime += Time.deltaTime;
        if(_startTime >= _lifetime)
        {
            _startTime = 0f;
            gameObject.SetActive(false);
        }
    }
}
