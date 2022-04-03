using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CureDispenser : MonoBehaviour, IInteractable
{
    public CureType CureDispensed;

    public void Interact(PlayerInputHandler player)
    {
        player.SetHeldCure(CureDispensed);
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Pickup", gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
