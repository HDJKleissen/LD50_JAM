using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBarkPlayer : MonoBehaviour
{
    [SerializeField] float ShortestTimeBetweenBarks = 12f;
    [SerializeField] float LongestTimeBetweenBarks = 22f;

    private Patient patient;

    // Start is called before the first frame update
    void Start()
    {
        patient = gameObject.GetComponent<Patient>();
        Invoke("PlayBark", Random.Range(ShortestTimeBetweenBarks, LongestTimeBetweenBarks));
    }

    void PlayBark()
    {
        if (patient.HasPatient)
        {
            //Debug.Log("I should be barking");
            FMODUnity.RuntimeManager.PlayOneShotAttached("event:/PatientVO", gameObject);
        }
        Invoke("PlayBark", Random.Range(ShortestTimeBetweenBarks, LongestTimeBetweenBarks));
    }
}


