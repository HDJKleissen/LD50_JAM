using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Patient : MonoBehaviour, IInteractable
{
    public bool HasPatient = false;

    public List<Illness> Illnesses;
    public Collider2D patientCollider;

    public static event Action<Patient, Illness, CureType> OnCureSuccess;
    public static event Action<Patient, Illness[], CureType> OnCureFailure;
    public static event Action<Patient, Illness> OnIllnessCreate;
    public static event Action<Patient> OnPatientDeath, OnPatientEnterDanger, OnPatientExitDanger;

    public BedGenerator bedGenerator;
    public Slider PatientProgressSlider;
    public float ProgressPerIllnessPerSecond;
    public float IllnessRecoveryPerSecond;
    public float ProgressValue, previousProgressValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (patientCollider == null)
        {
            patientCollider = GetComponent<Collider2D>();
        }
        if(bedGenerator == null)
        {
            bedGenerator = GetComponent<BedGenerator>();
        }
        BedGenerator.OnPatientCompletelyCured += ResetBed;
    }

    void OnDestroy()
    {
        BedGenerator.OnPatientCompletelyCured -= ResetBed;
    }

    private void ResetBed(Patient patient)
    {
        if(patient == this)
        {
            ProgressValue = 0;
            Illnesses.Clear();
        }
    }

    private void Update()
    {
        int illnessAmount = Illnesses.Count;
        if(illnessAmount == 0)
        {
            ProgressValue -= IllnessRecoveryPerSecond * Time.deltaTime;
        }
        else
        {
            ProgressValue += ProgressPerIllnessPerSecond * illnessAmount * Time.deltaTime;
        }
        ProgressValue = Mathf.Clamp(ProgressValue, 0, 1.01f);
        
        if(ProgressValue == 0)
        {
            PatientProgressSlider.gameObject.SetActive(false);
        }
        else
        {
            PatientProgressSlider.gameObject.SetActive(true);
        }

        PatientProgressSlider.value = ProgressValue;

        if(ProgressValue >= 1)
        {
            ProgressValue = 0;
            HasPatient = false;
            OnPatientDeath?.Invoke(this);
            Illnesses.Clear();
            bedGenerator.IsDone = true;
        }

        if (previousProgressValue <= 0.5f && ProgressValue > 0.5f)
        {
            OnPatientEnterDanger?.Invoke(this);
        }
        if (previousProgressValue > 0.5f && ProgressValue <= 0.5f)
        {
            OnPatientExitDanger?.Invoke(this);
        }

        previousProgressValue = ProgressValue;
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
