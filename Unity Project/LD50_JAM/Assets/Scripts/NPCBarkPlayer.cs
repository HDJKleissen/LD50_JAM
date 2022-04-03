using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBarkPlayer : MonoBehaviour
{
    [SerializeField] float ShortestTimeBetweenBarks = 15f;
    [SerializeField] float LongestTimeBetweenBarks = 25f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("PlayBark", Random.Range(ShortestTimeBetweenBarks, LongestTimeBetweenBarks));
    }

    void PlayBark()
    {
        Debug.Log("I should be barking");
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/PatientVO", gameObject);
        Invoke("PlayBark", Random.Range(ShortestTimeBetweenBarks, LongestTimeBetweenBarks));
    }
}


