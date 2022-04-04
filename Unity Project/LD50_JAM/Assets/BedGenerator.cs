using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedGenerator : MonoBehaviour
{
    public BedGeneratorSO BedGeneratorSO;
    [ReadOnly] public bool IsDone = false;

    [SerializeField] [ReadOnly] float betweenTimeTimer = 0;
    [SerializeField] [ReadOnly] float illnessDifficultyTimer = 0;
    [SerializeField] [ReadOnly] int currentMaxDifficulty = 0;
    [SerializeField] [ReadOnly] float currentAverageBetweenIllnessesTime = 0;
    [SerializeField] [ReadOnly] float currentTimeToNextIllness = 0;
    [SerializeField] [ReadOnly] float nextIllnessTimer = 0;

    [SerializeField] Patient patient;
    [SerializeField] [ReadOnly] float currentTimeBetweenPatients = 0;
    [SerializeField] float minTimeBetweenPatients = 0;
    [SerializeField] float maxTimeBetweenPatients = 0;
    [SerializeField] float timeBetweenMaxAndMinTimeBetweenPatients = 0;
    [SerializeField] [ReadOnly] float betweenPatientsTimeTimer = 0;
    [SerializeField] float timeUntilFirstPatient = 0;

    [SerializeField] [ReadOnly] BedGeneratorSO[] illnessChains;
    [SerializeField] [ReadOnly] int illnessChainDifficultyMax = 0;

    public static event Action<Patient> OnPatientCompletelyCured;
    public static event Action<Patient> OnNewPatientInBed;
    // Start is called before the first frame update
    void Start()
    {
        if (patient == null)
        {
            patient = GetComponent<Patient>();
        }

        Patient.OnPatientDeath += GetNewIllnessChain;
        illnessChains = Resources.LoadAll<BedGeneratorSO>("BedGeneratorSO");
        StartCoroutine(GetNewIllnessChainCR(timeUntilFirstPatient));
    }

    private void OnDestroy()
    {
        Patient.OnPatientDeath -= GetNewIllnessChain;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimers();
    }

    void GetNewIllnessChain(Patient deadPatient)
    {
        if (deadPatient == patient)
        {
            StartCoroutine(GetNewIllnessChainCR(currentTimeBetweenPatients));
        }
    }
    IEnumerator GetNewIllnessChainCR(float waitAmount)
    {
        yield return new WaitForSeconds(waitAmount);
        List<BedGeneratorSO> viableChains = new List<BedGeneratorSO>();

        for (int i = 0; i < illnessChains.Length; i++)
        {
            if (illnessChains[i].Difficulty <= illnessChainDifficultyMax)
            {
                viableChains.Add(illnessChains[i]);
            }
        }

        illnessChainDifficultyMax++;
        BedGeneratorSO = viableChains[UnityEngine.Random.Range(0, viableChains.Count)];
        betweenTimeTimer = 0;
        illnessDifficultyTimer = 0;
        currentMaxDifficulty = 0;
        currentAverageBetweenIllnessesTime = 0;
        currentTimeToNextIllness = 0;
        nextIllnessTimer = 0;
        IsDone = false;
        patient.HasPatient = true;
        OnNewPatientInBed?.Invoke(patient);
    }

    Illness ChooseIllness()
    {
        List<Illness> illnessesList = new List<Illness>();

        for (int i = 0; i < BedGeneratorSO.Illnesses.Length; i++)
        {
            if (BedGeneratorSO.Illnesses[i].Difficulty <= currentMaxDifficulty)
            {
                illnessesList.Add(BedGeneratorSO.Illnesses[i]);
            }
        }

        return Instantiate(illnessesList[UnityEngine.Random.Range(0, illnessesList.Count)]);
    }

    private void UpdateTimers()
    {
        if (patient.HasPatient && !IsDone)
        {
            if (betweenTimeTimer < BedGeneratorSO.TimeFromMaxTimeToMinTime)
            {
                betweenTimeTimer += Time.deltaTime;
            }
            if (currentMaxDifficulty <= BedGeneratorSO.IllnessMaxDifficulty)
            {
                if (illnessDifficultyTimer < BedGeneratorSO.TimePerIllnessDifficulty)
                {
                    illnessDifficultyTimer += Time.deltaTime;
                }
                else
                {
                    currentMaxDifficulty++;
                    illnessDifficultyTimer = 0;
                }
            }
            else
            {
                StartCoroutine(GetNewIllnessChainCR(currentTimeBetweenPatients));
                
                patient.HasPatient = false;
                IsDone = true;
                OnPatientCompletelyCured?.Invoke(patient);
                return;
            }

            currentAverageBetweenIllnessesTime = Mathf.Lerp(BedGeneratorSO.MaxTimeBetweenIllness, BedGeneratorSO.MinTimeBetweenIllness, betweenTimeTimer / BedGeneratorSO.TimeFromMaxTimeToMinTime);

            if (nextIllnessTimer < currentTimeToNextIllness)
            {
                nextIllnessTimer += Time.deltaTime;
            }
            else
            {
                // Get Illness and add to patient
                patient.CreateIllness(ChooseIllness());
                nextIllnessTimer = 0;
                currentTimeToNextIllness = currentAverageBetweenIllnessesTime + UnityEngine.Random.Range(-1f, 1f);
            }
        }
        else if (!patient.HasPatient)
        {
            if (betweenPatientsTimeTimer < timeBetweenMaxAndMinTimeBetweenPatients)
            {
                betweenPatientsTimeTimer += Time.deltaTime;
            }
            currentTimeBetweenPatients = Mathf.Lerp(maxTimeBetweenPatients, minTimeBetweenPatients, betweenPatientsTimeTimer / timeBetweenMaxAndMinTimeBetweenPatients);
        }
    }
}