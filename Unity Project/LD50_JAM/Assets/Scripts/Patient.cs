using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Patient : MonoBehaviour, IInteractable
{
    public bool IsCured => Illnesses.Count == 0;
    public bool HasPatient = false;

    // TODO: Change to list
    public List<Illness> Illnesses;
    public Collider2D patientCollider;

    public static event Action<Patient, Illness, CureType> OnCureSuccess;
    public static event Action<Patient, Illness[], CureType> OnCureFailure;
    public static event Action<Patient, Illness> OnIllnessCreate;

    // Start is called before the first frame update
    void Start()
    {
        if (patientCollider == null)
        {
            patientCollider = GetComponent<Collider2D>();
        }
    }

    private void Update()
    {
    }

    public void CreateIllness(Illness illness)
    {
        Illnesses.Add(illness);
        OnIllnessCreate?.Invoke(this, illness);
    }

    public Illness IllnessCuredByCure(CureType cureType)
    {
        for(int i = Illnesses.Count - 1; i >= 0; i--)
        {
            if(Illnesses[i].CuredBy == cureType)
            {
                return Illnesses[i];
            }
        }

        return null;   
    }

    public void AttemptCure(CureType cure)
    {
        if(cure == CureType.NONE)
        {
            return;
        }

        Illness curedIllness = IllnessCuredByCure(cure);

        if (curedIllness != null)
        {
            Illnesses.Remove(curedIllness);
            OnCureSuccess?.Invoke(this, curedIllness, cure);
        }
        else
        {
            OnCureFailure?.Invoke(this, Illnesses.ToArray(), cure);
        }
    }

    public void Interact(PlayerInputHandler player)
    {
        AttemptCure(player.GetHeldCure());
    }
}
