using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Illness : MonoBehaviour
{
    public bool IsCured = false;

    public CureType CuredBy = CureType.NONE;

    public static event Action<Illness, CureType> OnCureSuccess, OnCureFailure;

    public void Cure(CureType cure)
    {
        if (CuredBy == cure)
        {
            IsCured = true;
            OnCureSuccess?.Invoke(this, cure);
        }
        else
        {
            OnCureFailure?.Invoke(this, cure);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}