using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultySetting : MonoBehaviour
{
    public static Difficulty staticDifficulty;

    public void setEasy()
    {
        staticDifficulty = Difficulty.Easy;
    }
    public void setMedium()
    {
        staticDifficulty = Difficulty.Medium;
    }
    public void setHard()
    {
        staticDifficulty = Difficulty.Hard;
    }
    public void LoadForestFire()
    {
        SceneManager.LoadScene(1);
    }
}
