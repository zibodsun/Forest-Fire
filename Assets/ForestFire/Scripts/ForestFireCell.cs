﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.UI;

// class that represents a single cell in the cellular automaton
// contains data about the cell and methods to change its visual appearance
public class ForestFireCell : MonoBehaviour
{
    public int numberOfAliveNeighbours; // integer to store the number of alive neighbour cells
    public State cellState; // this variable stores the state of the cell as an enum defined below 
    // public Slider healthSlider;
    public enum State
    {
        None,
        Tree,
        Grass,
        Alight,
        Rock,
        Burnt,
    }
    public bool containsHostage = false;
    public int cellFuel; // integer to store the amount of fuel in the cell

    // references to materials used for visual appearance
    public Material groundMaterialBurnt;
    public Material groundMaterialGrass;
    public Material groundMaterialRock;
    public Material groundMaterialTree;
    private MeshRenderer groundMeshRenderer; // reference to this cell's mesh renderer, used when changing material

    public GameObject treeObject; // reference to tree visual object
    public GameObject leaves; // reference to leaves visual object
    public GameObject rockObject; // reference to rock visual object

    public GameObject treeFireFVX; // reference to tree fire vfx
    public GameObject grassFireFVX; // reference to grass fire vfx

    public GameObject currentFire; // if the cell is on fire, the reference to the fire vfx is stored here
    public GameObject playerCamera; // reference to player camera
    public float fireVFXDistance; // float to set the maximum distance a fire vfx will be rendered at. this is used to improve rendering performance. 

    private VisualEffect _fireVisualEffect; // reference to the fire vfx on the current fire object.

    // Awake is a built-in Unity function that is only called once, before the Start function
    private void Awake()
    {
        groundMeshRenderer = GetComponent<MeshRenderer>(); // get reference to this cell's mesh renderer
        // healthSlider = transform.Find("Canvas").Find("HealthBar").gameObject.GetComponent<Slider>(); // get reference to this cell's health bar
    }

    // reset anything that was turned on by a different cell
    private void ResetCell()
    {
        // turn off the tree and rock objects
        treeObject.SetActive(false);
        rockObject.SetActive(false);

        // destroy the fire effect if it exists
        if (currentFire != null)
        {
            Destroy(currentFire);
        }

    }

    // Update is a built-in Unity function that is called once per frame 
    private void Update()
    {   
        /*
        // update the health slider when it is a hostage cell
        if (containsHostage) {

            healthSlider.value = cellFuel;  // the health of the hostage is the same as the level of fuel of the cell
        } */

        // check whether current fire variable has been assigned 
        if (currentFire != null)
        {
            // enable vfx effect if player is within specified distance
            if (Vector3.Distance(transform.position, playerCamera.transform.position) > fireVFXDistance)
            {
                if (_fireVisualEffect.enabled == true)
                    _fireVisualEffect.enabled = false;
            }
            else
            {
                if (_fireVisualEffect.enabled == false)
                    _fireVisualEffect.enabled = true;
            }
        }
    }

    // change cell state to tree
    // a grass cell will never go back to tree from another state 
    // so we can check to see if the tree has material has been set aleady and skip changing it again to save on performance
    public void SetTree()
    {
        if (groundMeshRenderer.sharedMaterial == groundMaterialTree)
            return;

        // this code below wont run once the tree material has been set
        ResetCell();
        cellState = State.Tree;
        groundMeshRenderer.material = groundMaterialTree;
        leaves.SetActive(true);
        treeObject.SetActive(true);
    }

    // change cell state to grass    
    // a grass cell will never go back to grass from another state 
    // so we can check to see if the grass has material has been set aleady and skip changing it again to save on performance
    public void SetGrass()
    {
        if (groundMeshRenderer.sharedMaterial == groundMaterialGrass)
            return;

        // this code below wont run once the grass material has been set
        ResetCell();
        cellState = State.Grass;
        groundMeshRenderer.material = groundMaterialGrass;
    }

    // change cell state to rock
    // once a cell is set to rock state will never change
    // so we can check to see if the rock has material has been set aleady and never alter it again to save on performance
    public void SetRock()
    {
        if (groundMeshRenderer.sharedMaterial == groundMaterialRock)
            return;

        // this code below wont run once the rock material has been set
        ResetCell();
        cellState = State.Rock;
        cellFuel = 0;
        groundMeshRenderer.material = groundMaterialRock; // sets the cell material to rock
        rockObject.SetActive(true); 
    }

    // set cell alight
    public void SetAlight()
    {
        cellState = State.Alight;

        // check if a fire has assigned 
        if (currentFire == null)
        {
            // check whether tree object is active on this cell. if so assign the fire vfx as the current fire object
            if (treeObject.activeInHierarchy)
            {
                currentFire = Instantiate(treeFireFVX);
                currentFire.transform.SetParent(gameObject.transform, true);
                currentFire.transform.localPosition = Vector3.zero;
            }
            else // if the tree is not active, assign the grass vfx as the current fire object
            {
                currentFire = Instantiate(grassFireFVX);
                currentFire.transform.SetParent(gameObject.transform, true);
                currentFire.transform.localPosition = Vector3.zero;
            }
            // get a reference to the vfx component on the current fire object
            _fireVisualEffect = currentFire.GetComponent<VisualEffect>();
        }
    }

    // set cell burnt
    public void SetBurnt()
    {
        // if the cell has a fire, destroy it
        if (currentFire != null)
        {
            Destroy(currentFire);
        }

        // if there are leaves active in the hierarchy of this cell, disable them as if they have been burnt 
        if (treeObject.activeInHierarchy)
            leaves.SetActive(false);

        cellState = State.Burnt;
        groundMeshRenderer.material = groundMaterialBurnt;
    }
}