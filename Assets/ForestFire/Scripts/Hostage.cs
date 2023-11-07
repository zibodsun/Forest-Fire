using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Hostage : MonoBehaviour
{
    
    public HostageManager hostageManager;

    // Start is called before the first frame update
    void Start()
    {
        XRSimpleInteractable interactable = GetComponent<XRSimpleInteractable>();
        
        hostageManager = GameObject.FindObjectOfType<HostageManager>();
        //interactable.activated.AddListener(Rescue);
        //interactable.hoverEntered.AddListener(Rescue);
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.kKey.wasPressedThisFrame) {
            Rescue();
        }
    }

    public void Rescue() {
        hostageManager.hostageRescued();
        Debug.Log(hostageManager.getCurrHostages() + " " + hostageManager.numberOfHostages + "==========");
        Destroy(this.gameObject);
    }
}
