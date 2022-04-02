using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerCure playerCure;

    public event Action OnPlayerInteract;
    public event Action<Vector2> OnPlayerMove;

    [SerializeField] Patient closestPatient;
    
    // Start is called before the first frame update
    void Start()
    {
        if(playerMovement == null)
        {
            playerMovement = GetComponent<PlayerMovement>();
        }
    }

    private void Update()
    {
        if (Input.GetButtonUp("Interact"))
        {
            if(closestPatient != null && playerCure.GetHeldCure() != CureType.NONE)
            {
                closestPatient.AttemptCure(playerCure.GetHeldCure());
                OnPlayerInteract?.Invoke();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Patient patient = collision.GetComponent<Patient>();
        if (patient != null)
        {
            if (closestPatient == null)
            {
                closestPatient = patient;
            }
            else if (patient != closestPatient)
            {
                if(Vector3.Distance(transform.position, patient.transform.position) < Vector3.Distance(transform.position, closestPatient.transform.position))
                {
                    closestPatient = patient;
                }
            }
        }
        //Debug.Log("Entered trigger " + collision.name);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<Patient>() != null)
        {
            closestPatient = null;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        playerMovement.Move(movementInput);
        OnPlayerMove?.Invoke(movementInput);
    }
}
