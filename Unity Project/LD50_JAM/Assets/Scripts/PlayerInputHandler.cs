using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerCure playerCure;

    public event Action OnPlayerInteract;

    public event Action<Vector2> OnPlayerMove;

    [SerializeField] Transform closestInteractable;

    public GameObject TutorialUI;

    // Start is called before the first frame update
    void Start()
    {
        if (playerMovement == null)
        {
            playerMovement = GetComponent<PlayerMovement>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
            MusicPlayer.Instance?.SetMenu(true);
            MusicPlayer.Instance?.SetLose(false);
            MusicPlayer.Instance?.SetPanic(false);
        }
        if (Input.GetButtonDown("Interact"))
        {
            if (closestInteractable != null)
            {
                closestInteractable.GetComponent<IInteractable>().Interact(this);
                Patient pat = closestInteractable.GetComponent<Patient>();
                if (pat != null)
                {
                    if(!pat.HasPatient)
                    {
                        return;
                    }
                }

                OnPlayerInteract?.Invoke();
            }
        }
    }

    public void SetHeldCure(CureType cure)
    {
        playerCure.SetHeldCure(cure);
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Pickup", gameObject);
    }

    public CureType GetHeldCure()
    {
        return playerCure.GetHeldCure();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        IInteractable interactable = collision.GetComponent<IInteractable>();
        if (interactable != null)
        {
            Transform interactableTransform = collision.GetComponent<Transform>();

            if (closestInteractable == null)
            {
                closestInteractable = interactableTransform;
            }
            else if (interactableTransform != closestInteractable)
            {
                if (Vector3.Distance(transform.position, interactableTransform.position) < Vector3.Distance(transform.position, closestInteractable.transform.position))
                {
                    closestInteractable = interactableTransform;
                }
            }
        }
        //Debug.Log("Entered trigger " + collision.name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ShowTutorial>() != null)
        {
            TutorialUI.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<IInteractable>() != null)
        {
            closestInteractable = null;
        }
        else if(collision.GetComponent<ShowTutorial>() != null)
        {
            TutorialUI.gameObject.SetActive(false);
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