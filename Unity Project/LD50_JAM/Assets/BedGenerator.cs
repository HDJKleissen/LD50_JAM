using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedGenerator : MonoBehaviour
{
    public BedGeneratorSO BedGeneratorSO;

    [SerializeField] float betweenTimeTimer = 0;
    [SerializeField] float illnessDifficultyTimer = 0;
    [SerializeField] int currentMaxDifficulty = 0;
    [SerializeField] float currentAverageBetweenIllnessesTime = 0;
    [SerializeField] float currentTimeToNextIllness = 0;
    [SerializeField] float nextIllnessTimer = 0;

    [SerializeField] Patient patient;

    // Start is called before the first frame update
    void Start()
    {
        if(patient == null)
        {
            patient = GetComponent<Patient>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimers();
    }

    Illness ChooseIllness()
    {
        List<Illness> illnessesList = new List<Illness>();

        for (int i = 0; i < BedGeneratorSO.Illnesses.Length; i++)
        {
            if(BedGeneratorSO.Illnesses[i].Difficulty <= currentMaxDifficulty)
            {
                illnessesList.Add(BedGeneratorSO.Illnesses[i]);
            }
        }

        return Instantiate(illnessesList[Random.Range(0, illnessesList.Count)]);
    }

    private void UpdateTimers()
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
            // Patient is done being sick, clear bed.
            return;
        }

        currentAverageBetweenIllnessesTime = Mathf.Lerp(BedGeneratorSO.MaxTimeBetweenIllness, BedGeneratorSO.MinTimeBetweenIllness, betweenTimeTimer / BedGeneratorSO.TimeFromMaxTimeToMinTime);

        if(nextIllnessTimer < currentTimeToNextIllness)
        {
            nextIllnessTimer += Time.deltaTime;
        }
        else
        {
            // Get Illness and add to patient
            patient.CreateIllness(ChooseIllness());
            nextIllnessTimer = 0;
            currentTimeToNextIllness = currentAverageBetweenIllnessesTime + Random.Range(-1f, 1f);
        }
    }
}