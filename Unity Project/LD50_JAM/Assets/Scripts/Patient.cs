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
    public static event Action<Patient> OnPatientDeath;

    public BedGenerator bedGenerator;
    public Slider PatientProgressSlider;
    public float ProgressPerIllnessPerSecond;
    public float IllnessRecoveryPerSecond;
    float progressValue;

    float patientTimer;

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
    }

    private void Update()
    {
        int illnessAmount = Illnesses.Count;
        if(illnessAmount == 0)
        {
            progressValue -= IllnessRecoveryPerSecond * Time.deltaTime;
        }
        else
        {
            progressValue += ProgressPerIllnessPerSecond * illnessAmount * Time.deltaTime;
        }
        progressValue = Mathf.Clamp(progressValue, 0, 1.01f);
        
        if(progressValue == 0)
        {
            PatientProgressSlider.gameObject.SetActive(false);
        }
        else
        {
            PatientProgressSlider.gameObject.SetActive(true);
        }

        PatientProgressSlider.value = progressValue;

        if(progressValue >= 1)
        {
            progressValue = 0;
            HasPatient = false;
            OnPatientDeath?.Invoke(this);
            Illnesses.Clear();
            bedGenerator.IsDone = true;
        }
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
