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

    public float PopupGroupYOffset, PopupGroupMaxY;
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
        Vector3 newPosition = Camera.main.WorldToScreenPoint(patient.transform.position);
        bool stackVertical = false;

        newPosition = new Vector3(newPosition.x, Mathf.Clamp(newPosition.y, 0, PopupGroupMaxY));

        int popupAmount = Popups.Count;

        if (newPosition.x < 0 || newPosition.x > Screen.width)
        {
            stackVertical = true;
            newPosition = new Vector3(Mathf.Clamp(newPosition.x, 0, Screen.width), Mathf.Clamp(newPosition.y, 0, PopupGroupMaxY - ((float)popupAmount / 2) * PopupOffset), newPosition.z);
        }

        transform.position = newPosition;

        for (int i = 0; i < popupAmount; i++)
        {
            if (stackVertical)
            {
                Popups[i].Destination = new Vector3(0,(i - ((float)popupAmount / 2)) * PopupOffset + PopupGroupYOffset, 0);
            }
            else
            {
                Popups[i].Destination = new Vector3((i - ((float)popupAmount / 2)) * PopupOffset, PopupGroupYOffset, 0);
            }
        }
    }

    public void AddPopup(Illness illness)
    {
        BedPopup newPopup = Instantiate(BedPopupPrefab, transform).GetComponent<BedPopup>();
        newPopup.GetComponent<SpriteRenderer>().sprite = illness.PopupSprite;
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