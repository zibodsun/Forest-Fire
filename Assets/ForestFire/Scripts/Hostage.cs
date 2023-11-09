using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class Hostage : MonoBehaviour
{
    public HostageManager hostageManager;
    public ForestFireCell cell;
    public Slider healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        XRSimpleInteractable interactable = GetComponent<XRSimpleInteractable>();
        
        hostageManager = GameObject.FindObjectOfType<HostageManager>();
        healthSlider = transform.Find("Canvas").Find("HealthBar").gameObject.GetComponent<Slider>(); // get reference to this cell's health bar
        healthSlider.maxValue = cell.cellFuel; // set the maximum health to the current fuel of the cell
        //interactable.activated.AddListener(Rescue);
        //interactable.hoverEntered.AddListener(Rescue);
    }

    // Update is called once per frame
    void Update()
    {
        // rescue all hostages (For Debugging Only)
        if (Keyboard.current.kKey.wasPressedThisFrame) {
            Rescue();
        }

        if (cell.cellFuel <= 0)
        {
            cell.SetBurnt();
            this.gameObject.SetActive(false);
        }else {
            healthSlider.value = cell.cellFuel;  // the health of the hostage is the same as the level of fuel of the cell
        }

    }

    public void Rescue() {
        hostageManager.hostageRescued();
        Debug.Log(hostageManager.getCurrHostages() + " " + hostageManager.numberOfHostages + "==========");
        Destroy(this.gameObject);
    }
}
