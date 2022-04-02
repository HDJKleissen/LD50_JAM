using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCure : MonoBehaviour
{
    public bool HoldingCure => _heldCure != CureType.NONE;

    public static Action<CureType> OnCureChange;

    [SerializeField]
    CureType _heldCure;

    // Start is called before the first frame update
    void Start()
    {
        SetHeldCure(CureType.NONE);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHeldCure(CureType cureType)
    {
        _heldCure = cureType;
        OnCureChange?.Invoke(_heldCure);
    }

    public CureType GetHeldCure()
    {
        return _heldCure;
    }
}
