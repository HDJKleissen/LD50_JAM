using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cure : MonoBehaviour
{
    public CureType cureType;
    public Collider2D cureCollider;

    // TODO: Replace this "local" variable with getting the playercure class from the player object that is interacting with this
    public PlayerCure playerCure;

    public void Start()
    {
        if (cureCollider == null)
        {
            cureCollider = GetComponent<Collider2D>();
        }

        if (playerCure == null)
        {
            playerCure = FindObjectOfType<PlayerCure>();
        }
    }

    public void OnMouseUpAsButton()
    {
        playerCure.SetHeldCure(cureType);
    }
}

public enum CureType
{
    NONE,
    SYRINGE,
    CRASH_CART,
    BANDAGES
}
