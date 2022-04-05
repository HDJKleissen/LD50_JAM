using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WebGLMusicChecker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (FMODUnity.RuntimeManager.HasBankLoaded("Master")
            && FMODUnity.RuntimeManager.HasBankLoaded("Master.strings")
            )
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            StartCoroutine(CoroutineHelper.Chain(
                CoroutineHelper.WaitUntil(() => FMODUnity.RuntimeManager.HasBankLoaded("Master")),
                CoroutineHelper.WaitUntil(() => FMODUnity.RuntimeManager.HasBankLoaded("Master.strings")),
                CoroutineHelper.Do(() => SceneManager.LoadScene("MainMenu"))
            ));
        }
    }
}