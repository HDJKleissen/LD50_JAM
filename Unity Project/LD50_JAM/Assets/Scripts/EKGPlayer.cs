using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EKGPlayer : MonoBehaviour
{
    FMOD.Studio.EventInstance EKG;
    private Patient Patient;

    public bool Dying, Dead;

    void Start()
    {
        Patient = gameObject.GetComponent<Patient>();
        if (Patient.HasPatient)
        {
            //Invoke("StartPlayingSound", Random.Range(0f, 0.5f)); Could use this to avoid all bein in sync, but not sure if that's better
            StartPlayingSound();
        }
    }

    private void StartPlayingSound()
    {
        EKG = FMODUnity.RuntimeManager.CreateInstance("event:/EKG");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(EKG, transform);
        EKG.start();
        EKG.release();
        SetDead(Dead);
        SetDying(Dying);
    }

    private void Update()
    {
        if (Patient.HasPatient)
        {
            SetDead(Dead);
            SetDying(Dying);
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
        EKG.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
