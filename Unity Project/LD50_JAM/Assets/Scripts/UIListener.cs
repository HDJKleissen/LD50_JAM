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
        GameSession.OnCureSuccessesChange += SetCureSuccessesUI;
        GameSession.OnCureFailuresChange += SetCureFailuresUI;
    }

    private void OnDestroy()
    { 
        PlayerCure.OnCureChange -= SetHeldCureUI;
        GameSession.OnTimerChange -= SetTimerUI;
        GameSession.OnCureSuccessesChange -= SetCureSuccessesUI;
        GameSession.OnCureFailuresChange -= SetCureFailuresUI;
    }

    void SetHeldCureUI(CureType cureType)
    {
        HeldCureText.SetText(cureType.ToString());
    }

    void SetTimerUI(float timer)
    {
        TimerText.SetText(Util.FormatTime(timer));
    }

    void SetCureSuccessesUI(int amount)
    {
        CureSuccessesAmountText.SetText(amount.ToString());
    }
    void SetCureFailuresUI(int amount)
    {
        CureFailuresAmountText.SetText(amount.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
