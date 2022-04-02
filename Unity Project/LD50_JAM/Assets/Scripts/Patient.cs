using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Patient : MonoBehaviour
{
    public Illness Illness;
    public Collider2D patientCollider;
    public TextMeshProUGUI CuredByText, IsCuredText;

    // TODO: Replace this "local" variable with getting the playercure class from the player object that is interacting with this
    public PlayerCure playerCure;

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
        CuredByText.SetText(Illness.CuredBy.ToString());
    }
    private void OnMouseUpAsButton()
    {
        if (playerCure.HoldingCure)
        {
            Illness.Cure(playerCure.GetHeldCure());
            playerCure.SetHeldCure(CureType.NONE);
            IsCuredText.SetText(Illness.IsCured ? "Cured!" : "Not Cured!");
        }
    }
}
