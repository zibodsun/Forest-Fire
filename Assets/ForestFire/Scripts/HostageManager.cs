using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public enum Difficulty 
{ 
    Easy,
    Medium,
    Hard 

};
public class HostageManager : MonoBehaviour
{
    public Difficulty difficulty;
    public ForestFire3D forestFire;
    public GameObject hostagePrefab;
    
    private int numberOfHostages;

    private void Awake()
    {
        // assign variables associated with difficulty
        switch (difficulty)
        {
            case Difficulty.Easy:
                numberOfHostages = 3;
                break;
            case Difficulty.Medium:
                numberOfHostages = 6;
                break;
            case Difficulty.Hard:
                numberOfHostages = 9;
                break;
        }
    }

    private void Start()
    {
        Debug.Log("Start HostageManager");
    }

    /**
     *  This method returns a Dictionary mapping the coordinates of all hostages. It can be called my other classes to 
     *  generate the hostages within the grid of the ForestFire3D object.
     */
    public int[,] GenerateHostages() {
        int[,] hostageLocations = new int[numberOfHostages, 2];
        
        // Generate hostages in random cells
        for (int i = 0; i < numberOfHostages; i++) {
            int x = Random.Range(0, forestFire.gridSizeX);
            int y = Random.Range(0, forestFire.gridSizeY);
            hostageLocations[i,0] = x;
            hostageLocations[i,1] = y;
        }

        return hostageLocations;
    }

    public bool ContainsHostage(int xCount, int yCount, int[,] hostageList) {
        for (int i = 0; i < hostageList.GetLength(0); i++) {
            if (hostageList[i,0] == xCount && hostageList[i,1] == yCount) {
                return true;
            }
        }
        return false;
    }

    public void PrintHostageList(int[,] hostageList) {
        for (int i = 0; i < hostageList.GetLength(0); i++) { 
            Debug.Log(hostageList[i,0] + " " + hostageList[i,1]);
        }
    }
}
