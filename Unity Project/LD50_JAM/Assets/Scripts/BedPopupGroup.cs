using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedPopupGroup : MonoBehaviour
{
    public Patient patient;
    public GameObject BedPopupPrefab;
    public List<LerpToPosition> Popups = new List<LerpToPosition>();
    Dictionary<Illness, BedPopup> IllnessPopupDict = new Dictionary<Illness, BedPopup>();

    public float PopupGroupYOffset;
    public float PopupOffset;

    // Start is called before the first frame update
    void Start()
    {
        Popups = new List<LerpToPosition>(GetComponentsInChildren<LerpToPosition>());
        Patient.OnIllnessCreate += CreateIllnessPopup;
        Patient.OnCureSuccess += RemoveIllnessPopup;
    }
    void CreateIllnessPopup(Patient creatingPatient, Illness illness)
    {
        if(creatingPatient == patient)
        {
            AddPopup(illness);
        }
    }


    void RemoveIllnessPopup(Patient removingPatient, Illness illness, CureType cureType)
    {
        if (removingPatient == patient)
        {
            RemovePopup(illness);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Camera.main.WorldToScreenPoint(patient.transform.position);

        int popupAmount = Popups.Count;

        for(int i = 0; i < popupAmount; i++)
        {
            Popups[i].Destination = new Vector3((i - ((float)popupAmount / 2)) * PopupOffset, PopupGroupYOffset, 0);
        }
    }

    public void AddPopup(Illness illness)
    {
        BedPopup newPopup = Instantiate(BedPopupPrefab, transform).GetComponent<BedPopup>();
        IllnessPopupDict.Add(illness, newPopup);
        Popups.Add(newPopup.GetComponent<LerpToPosition>());
    }

    public void RemovePopup(Illness illness)
    {
        BedPopup toDelete = IllnessPopupDict[illness];
        Popups.Remove(toDelete.GetComponent<LerpToPosition>());
        IllnessPopupDict.Remove(illness);
        Destroy(toDelete.gameObject);
    }
}