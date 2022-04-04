using System;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    public bool IsPaused;

    float _gameTimer = 0;

    [SerializeField] List<(Illness[], CureType, bool)> cureAttempts = new List<(Illness[], CureType, bool)>();

    public int maxPatientDeaths;
    int cureSuccesses = 0;
    int cureFailures = 0;
    int patientDeaths = 0;
    int patientFullyCured = 0;

    public static Action<float> OnTimerChange;
    public static Action<int> OnCureSuccessesChange, OnCureFailuresChange, OnPatientsFullyCuredChange;
    public static Action<int, int> OnPatientsDiedChange;
    public static Action<int, int> SetCorrectUIOnGameStart;

    public void Awake()
    {
        Patient.OnCureSuccess += RegisterCureSuccess;
        Patient.OnCureFailure += RegisterCureFailure;
        Patient.OnPatientDeath += RegisterPatientDeath;
        BedGenerator.OnPatientCompletelyCured += RegisterPatientFullyCured;
    }

    private void Start()
    {
        SetCorrectUIOnGameStart?.Invoke(0, maxPatientDeaths);
    }

    public void OnDestroy()
    {
        Patient.OnCureSuccess -= RegisterCureSuccess;
        Patient.OnCureFailure -= RegisterCureFailure;
        Patient.OnPatientDeath -= RegisterPatientDeath;
        BedGenerator.OnPatientCompletelyCured -= RegisterPatientFullyCured;
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

    void RegisterPatientDeath(Patient patient)
    {
        patientDeaths++;
        OnPatientsDiedChange?.Invoke(patientDeaths, maxPatientDeaths);
    }
    void RegisterPatientFullyCured(Patient patient)
    {
        patientFullyCured++;
        OnPatientsFullyCuredChange?.Invoke(patientFullyCured);
    }

    public void Update()
    {
        _gameTimer += Time.deltaTime;
        OnTimerChange?.Invoke(_gameTimer);
    }
}