using System;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    public bool IsPaused;

    float _gameTimer = 0;

    [SerializeField] List<(Illness[], CureType, bool)> cureAttempts = new List<(Illness[], CureType, bool)>();

    int cureSuccesses = 0;
    int cureFailures = 0;

    public static Action<float> OnTimerChange;
    public static Action<int> OnCureSuccessesChange, OnCureFailuresChange;


    public void Awake()
    {
        Patient.OnCureSuccess += RegisterCureSuccess;
        Patient.OnCureFailure += RegisterCureFailure;
    }

    public void OnDestroy()
    {
        Patient.OnCureSuccess -= RegisterCureSuccess;
        Patient.OnCureFailure += RegisterCureFailure;
    }

    void RegisterCureSuccess(Patient patient, Illness illness, CureType cureType)
    {
        cureAttempts.Add((new Illness[] { illness }, cureType, true));
        cureSuccesses++;
        OnCureSuccessesChange?.Invoke(cureSuccesses);
    }

    void RegisterCureFailure(Patient patient, Illness[] illnesses, CureType cureType)
    {
        cureAttempts.Add((illnesses, cureType, false));
        cureFailures++;
        OnCureFailuresChange?.Invoke(cureFailures);

    }

    public void Update()
    {
        _gameTimer += Time.deltaTime;
        OnTimerChange?.Invoke(_gameTimer);
    }
}