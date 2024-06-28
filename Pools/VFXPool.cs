using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXPool : MonoBehaviour
{
    public static VFXPool Instance;
    [SerializeField] GameObject _explosion;
    List<GameObject> _explosionList = new List<GameObject>();
    
    [Header("Amount of Spawned Effects")]
    [SerializeField] int _vfxObjAmount;
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        for(int i = 0; i < _vfxObjAmount; i++)
        {
            GameObject vfx = Instantiate(_explosion, transform.position, Quaternion.identity);
            vfx.SetActive(false);
            _explosionList.Add(vfx);
        }
    }
    public GameObject IsVFXAvailable()
    {
        for(int i = 0; i < _explosionList.Count; i++)
        {
            if(!_explosionList[i].activeInHierarchy)
            {
                return _explosionList[i];
            }
        }
        return null;
    }
}
