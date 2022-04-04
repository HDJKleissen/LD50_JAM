using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIListener : MonoBehaviour
{
    public TextMeshProUGUI HeldCureText, CureSuccessesAmountText, CureFailuresAmountText, TimerText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        PlayerCure.OnCureChange += SetHeldCureUI;
        GameSession.OnTimerChange += SetTimerUI;
        GameSession.OnPatientsFullyCuredChange += SetPatientsCuredUI;
        GameSession.OnPatientsDiedChange += SetPatientsDiedUI;
        GameSession.SetCorrectUIOnGameStart += SetPatientsDiedUI;
    }

    private void OnDestroy()
    { 
        PlayerCure.OnCureChange -= SetHeldCureUI;
        GameSession.OnTimerChange -= SetTimerUI;
        GameSession.OnPatientsFullyCuredChange -= SetPatientsCuredUI;
        GameSession.OnPatientsDiedChange -= SetPatientsDiedUI;
        GameSession.SetCorrectUIOnGameStart += SetPatientsDiedUI;
    }

    void SetHeldCureUI(CureType cureType)
    {
        HeldCureText.SetText(cureType.ToString());
    }

    void SetTimerUI(float timer)
    {
        TimerText.SetText(Util.FormatTime(timer));
    }

    void SetPatientsCuredUI(int amount)
    {
        CureSuccessesAmountText.SetText(amount.ToString());
    }
    void SetPatientsDiedUI(int amount, int maxAmount)
    {
        CureFailuresAmountText.SetText(amount + "/" + maxAmount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
