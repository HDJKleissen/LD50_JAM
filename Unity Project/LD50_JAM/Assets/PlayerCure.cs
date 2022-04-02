using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCure : MonoBehaviour
{
    [SerializeField]
    CureType _heldCure = CureType.NONE;

    public bool HoldingCure => _heldCure != CureType.NONE;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHeldCure(CureType cureType)
    {
        _heldCure = cureType;
    }

    public CureType GetHeldCure()
    {
        return _heldCure;
    }
}
