using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        Patient.OnIllnessCreate += CreateIllnessPopup;
        Patient.OnCureSuccess += RemoveIllnessPopup;
        Patient.OnPatientDeath += DestroyAllPopups;
    }
    void OnDestroy()
    {
        Patient.OnIllnessCreate -= CreateIllnessPopup;
        Patient.OnCureSuccess -= RemoveIllnessPopup;
        Patient.OnPatientDeath -= DestroyAllPopups;
    }
    void CreateIllnessPopup(Patient creatingPatient, Illness illness)
    {
        if(creatingPatient == patient)
        {
            AddPopup(illness);
        }
    }

    void DestroyAllPopups(Patient deadPatient)
    {
        if(patient == deadPatient)
        {
            IllnessPopupDict.Clear();
            foreach(LerpToPosition ltp in Popups)
            {
                Destroy(ltp.gameObject);
            }

            Popups.Clear();
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

        //newPosition = new Vector3(newPosition.x, Mathf.Clamp(newPosition.y, 0, PopupGroupMaxY));

        int popupAmount = Popups.Count;

        if (newPosition.x < 0 || newPosition.x > Screen.width)
        {
            stackVertical = true;
            newPosition = new Vector3(
                Mathf.Clamp(newPosition.x, 0, Screen.width),
                newPosition.y, //Mathf.Clamp(newPosition.y, 0, PopupGroupMaxY - ((float)popupAmount / 2) * PopupOffset),
                newPosition.z
            );
        }
        if (newPosition.y < 0 || newPosition.y > Screen.height)
        {
            newPosition = new Vector3(
                newPosition.x,
                Mathf.Clamp(newPosition.y, 0, Screen.height), //Mathf.Clamp(newPosition.y, 0, PopupGroupMaxY - ((float)popupAmount / 2) * PopupOffset),
                newPosition.z
            );
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
        newPopup.GetComponent<Image>().sprite = illness.PopupSprite;
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