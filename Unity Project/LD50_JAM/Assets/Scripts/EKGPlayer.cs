using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EKGPlayer : MonoBehaviour
{
    FMOD.Studio.EventInstance EKG;
    FMOD.Studio.PLAYBACK_STATE pb;
    private Patient Patient;

    public bool Dying, Dead;
    bool EKGPaused = false; 

    void Start()
    {
        Patient = gameObject.GetComponent<Patient>();
        EKG = FMODUnity.RuntimeManager.CreateInstance("event:/EKG");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(EKG, transform);
        //Invoke("StartPlayingSound", Random.Range(0f, 0.5f)); Could use this to avoid all bein in sync, but not sure if that's better
        //StartPlayingSound();
        SetEKGSoundPaused(!Patient.HasPatient);
        Patient.OnPatientDeath += PlayDeathSound;
        BedGenerator.OnPatientCompletelyCured += ResetEKG;
    }

    private void ResetEKG(Patient obj)
    {
        if(obj == Patient)
        {
            SetDead(false);
            SetDying(false);
            SetEKGSoundPaused(true);
        }
    }

    private void PlayDeathSound(Patient deadPatient)
    {
        if(deadPatient == Patient)
        {
            SetDead(true);
            SetDying(true);
            StartCoroutine(PauseAfterDeath());
        }
    }

    IEnumerator PauseAfterDeath()
    {
        yield return new WaitForSeconds(0.3f);
        SetEKGSoundPaused(true);
        SetDying(false);
        SetDead(false);
    }

    private void StartPlayingSound()
    {
        SetDead(Dead);
        SetDying(Dying);
        EKG.start();
    }

    void SetEKGSoundPaused(bool paused)
    {
        EKGPaused = paused;
        EKG.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    private void Update()
    {
        if (Patient.HasPatient)
        {
            EKG.getPlaybackState(out pb);
            if (pb != FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                StartPlayingSound();
            }
            SetDying(Patient.ProgressValue > 0.5f);
        }
    }

    public void SetDead(bool value)
    {
        EKG.setParameterByName("Dead", value ? 1f : 0f, false);
        Dead = value;
    }

    public void SetDying(bool value)
    {
        EKG.setParameterByName("Dying", value ? 1f : 0f, false);
        Dying = value;
    }

    private void OnDestroy()
    {
        EKG.release();
        EKG.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
