using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientCuredHeart : MonoBehaviour
{
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        GetComponent<LerpToPosition>().DestinationTransform = player;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) < 1f)
        {
            Destroy(gameObject);
        }   
    }
}
