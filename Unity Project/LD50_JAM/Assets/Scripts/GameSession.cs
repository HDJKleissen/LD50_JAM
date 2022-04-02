using System;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    public bool IsPaused;

    float _gameTimer = 0;

    List<(Patient, CureType)> cureAttempts = new List<(Patient, CureType)>();

    int cureSuccesses = 0;
    int cureFailures = 0;

    public static Action<float> OnTimerChange;
    public static Action<int> OnCureSuccessesChange, OnCureFailuresChange;


    public void Awake()
    {
        Patient.OnCureAttempt += RegisterCureAttempt;
    }

    public void OnDestroy()
    {
        Patient.OnCureAttempt -= RegisterCureAttempt;
    }

    void RegisterCureAttempt(Patient patient, CureType cureType)
    {
        cureAttempts.Add((patient, cureType));
        if(patient.CureIsSuccessful(cureType))
        {
            cureSuccesses++;
            OnCureSuccessesChange?.Invoke(cureSuccesses);
        }
        else
        {
            cureFailures++;
            OnCureFailuresChange?.Invoke(cureFailures);
        }
    }

    public void Update()
    {
        _gameTimer += Time.deltaTime;
        OnTimerChange?.Invoke(_gameTimer);
    }
}

