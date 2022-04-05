using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpToPosition : MonoBehaviour
{
    public bool Local, DestroyAtDestination;

    [SerializeField] UpdateMoment updateMoment;
    public Vector3 Destination;
    public Transform DestinationTransform;
    public float LerpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (updateMoment == UpdateMoment.Update)
        {
            if (Local)
            {
                if (DestinationTransform != null)
                {
                    Destination = DestinationTransform.localPosition;
                }
                transform.localPosition = Vector3.Lerp(transform.localPosition, Destination, LerpSpeed * Time.deltaTime);
            }
            else
            {
                if (DestinationTransform != null)
                {
                    Destination = DestinationTransform.position;
                }
                transform.position = Vector3.Lerp(transform.position, Destination, LerpSpeed * Time.deltaTime);
            }
        }
    }
    void FixedUpdate()
    {
        if (updateMoment == UpdateMoment.FixedUpdate)
        {
            if (Local)
            {
                if (DestinationTransform != null)
                {
                    Destination = DestinationTransform.localPosition;
                }
                transform.localPosition = Vector3.Lerp(transform.localPosition, Destination, LerpSpeed * Time.fixedDeltaTime);
            }
            else
            {
                if (DestinationTransform != null)
                {
                    Destination = DestinationTransform.position;
                }
                transform.position = Vector3.Lerp(transform.position, Destination, LerpSpeed * Time.fixedDeltaTime);
            }
        }
    }
    void LateUpdate()
    {
        if (updateMoment == UpdateMoment.LateUpdate)
        {
            if (Local)
            {
                if (DestinationTransform != null)
                {
                    Destination = DestinationTransform.localPosition;
                }
                transform.localPosition = Vector3.Lerp(transform.localPosition, Destination, LerpSpeed * Time.deltaTime);
            }
            else
            {
                if (DestinationTransform != null)
                {
                    Destination = DestinationTransform.position;
                }
                transform.position = Vector3.Lerp(transform.position, Destination, LerpSpeed * Time.deltaTime);
            }
        }
    }

    enum UpdateMoment
    {
        Update,
        FixedUpdate,
        LateUpdate
    }
}
