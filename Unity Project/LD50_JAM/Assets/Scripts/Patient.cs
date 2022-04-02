using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Patient : MonoBehaviour
{
    public bool IsCured = false;

    // TODO: Change to list
    public Illness Illness;
    public Collider2D patientCollider;
    public TextMeshProUGUI CuredByText, IsCuredText;

    // TODO: Replace this "local" variable with getting the playercure class from the player object that is interacting with this
    public PlayerCure playerCure;

    public static event Action<Patient, CureType> OnCureAttempt;

    // Start is called before the first frame update
    void Start()
    {
        if (playerCure == null)
        {
            playerCure = FindObjectOfType<PlayerCure>();
        }
        if (patientCollider == null)
        {
            patientCollider = GetComponent<Collider2D>();
        }
        CuredByText.SetText(Illness.IllnessName);
    }
    private void OnMouseUpAsButton()
    {
        if (playerCure.HoldingCure)
        {
            Cure(playerCure.GetHeldCure());
            playerCure.SetHeldCure(CureType.NONE);
            IsCuredText.SetText(IsCured ? "Cured!" : "Not Cured!");
        }
    }
    public bool CureIsSuccessful(CureType cureType)
    {
        if (Illness == null)
        {
            return false; 
        }
        return Illness.CuredBy == cureType;
    }

    public void Cure(CureType cure)
    {
        if (CureIsSuccessful(cure))
        {
            IsCured = true;
        }
        OnCureAttempt?.Invoke(this, cure);
    }
}
