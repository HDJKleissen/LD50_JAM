using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Patient.OnCureFailure += PlayOnCureFailure;
        Patient.OnCureSuccess += PlayOnCureSuccess;
        Patient.OnIllnessCreate += PlayOnIllnessCreate;
        Patient.OnPatientDeath += PlayOnPatientDeath;
        BedGenerator.OnPatientCompletelyCured += PlayOnPatientsFullyCured;
        BedGenerator.OnNewPatientInBed += PlayOnNewPatientInBed;
    }

    private void PlayOnNewPatientInBed(Patient patient)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/NewPatient", patient.gameObject);
    }

    private void PlayOnPatientsFullyCured(Patient patient)
    {
        // @Pat:  Play Patient fully cured (aka patient leaves bed without dying) sound, can use patient.transform for a location
    }

    private void PlayOnPatientDeath(Patient patient)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Death", patient.gameObject);
    }

    private void PlayOnIllnessCreate(Patient patient, Illness illness)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/NewIllness", patient.gameObject);
    }

    private void PlayOnCureSuccess(Patient patient, Illness illness, CureType cureType)
    {
        switch (cureType)
        {
            case CureType.SYRINGE:
                FMODUnity.RuntimeManager.PlayOneShotAttached("event:/CureSyringe", patient.gameObject);
                break;
            case CureType.CRASH_CART:
                FMODUnity.RuntimeManager.PlayOneShotAttached("event:/CureCrashCart", patient.gameObject);
                break;
            case CureType.BANDAGES:
                FMODUnity.RuntimeManager.PlayOneShotAttached("event:/CureBandages", patient.gameObject);
                break;
        }
        // @Pat:  Play Cure Success sound, can use patient.transform for a location, switch on cureType for different cure sounds
    }

    private void PlayOnCureFailure(Patient patient, Illness[] illnesses, CureType cureType)
    {
        // @Pat:  Play Cure Failure sound, can use patient.transform for a location, cureType is the cure that was attempted to be used
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
