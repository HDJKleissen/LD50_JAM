using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : UnitySingleton<MusicPlayer>
{
    FMOD.Studio.EventInstance Music;

    public bool Menu , Panic;

    void Start()
    {
        if (Instance == this)
        {
            Music = FMODUnity.RuntimeManager.CreateInstance("event:/Music");
            Music.start();
            Music.release();
            DontDestroyOnLoad(this);
            SetPanic(Panic);
            SetMenu(true);
        }
    }

    private void Update()
    {
        SetPanic(Panic);
        SetMenu(Menu);
    }

    public void SetPanic(bool value)
    {
        Music.setParameterByName("Panic", value ? 1f : 0f, false);
        Panic = value;
    }

    public void SetMenu(bool value)
    {
        Music.setParameterByName("Menu", value ? 1f : 0f, false);
        Menu = value;
    }

    private void OnDestroy()
    {
        Music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
