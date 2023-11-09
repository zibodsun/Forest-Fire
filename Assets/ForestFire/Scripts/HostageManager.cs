using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

/*
 * The three levels of difficulty that the game can be played in
 */
public enum Difficulty 
{ 
    Easy,
    Medium,
    Hard 

};
public class HostageManager : MonoBehaviour
{
    public Difficulty difficulty;
    public ForestFire3D forestFire;     // reference to the ForestFire3D script
    public GameObject hostagePrefab;
    public GameObject deathImage;

    public int numberOfHostages;        // determined at Start by the difficulty picked
    private int currentHostages;        // hostages that have been rescued


    private void Awake()
    {
        difficulty = DifficultySetting.staticDifficulty;

        // difficulty affects the number of hostages to rescue and the starting number of lit trees
        switch (difficulty)
        {
            case Difficulty.Easy:
                numberOfHostages = 2;
                forestFire.nlight = 2;
                break;
            case Difficulty.Medium:
                numberOfHostages = 4;
                forestFire.nlight = 4;
                break;
            case Difficulty.Hard:
                numberOfHostages = 7;
                forestFire.nlight = 6;
                break;
        }
    }

    private void Start()
    {
        Debug.Log("Start HostageManager");
        currentHostages = 0;           // initialise the hostage count
    }

    /*
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

    /*
     * Returns true if the dictionary hostageList contains the cell coordinates provided in the parameters
     */
    public bool ContainsHostage(int xCount, int yCount, int[,] hostageList) {
        for (int i = 0; i < hostageList.GetLength(0); i++) {
            if (hostageList[i,0] == xCount && hostageList[i,1] == yCount) {
                return true;
            }
        }
        return false;
    }

    /*
     * Prints all the generated hostages. Only for debugging
     */
    public void PrintHostageList(int[,] hostageList) {
        for (int i = 0; i < hostageList.GetLength(0); i++) { 
            Debug.Log(hostageList[i,0] + " " + hostageList[i,1]);
        }
    }

    /*
     * Returns the number of active hostages
     */
    public int getCurrHostages() {
        return currentHostages;
    }

    /*
     * Call this function when a hostage is rescued to increase the score
     */
    public void hostageRescued() {
        currentHostages++;
    }
}
