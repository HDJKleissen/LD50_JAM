using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BedGenerator", menuName = "AdjAni/BedGenerator")]
public class BedGeneratorSO : ScriptableObject
{
    public int Difficulty;

    public Illness[] Illnesses;
    public float MinTimeBetweenIllness, MaxTimeBetweenIllness;
    public float TimeFromMaxTimeToMinTime;

    public int IllnessMaxDifficulty {
        get {
            int highestDifficulty = -1;

            for(int i = 0; i < Illnesses.Length; i++)
            {
                if(Illnesses[i].Difficulty > highestDifficulty)
                {
                    highestDifficulty = Illnesses[i].Difficulty;
                }
            }

            return highestDifficulty;
    }
}
    public float TimePerIllnessDifficulty;
}
