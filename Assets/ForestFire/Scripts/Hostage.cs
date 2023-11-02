using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Hostage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        XRSimpleInteractable interactable = GetComponent<XRSimpleInteractable>();
        //interactable.activated.AddListener(Rescue);
        //interactable.hoverEntered.AddListener(Rescue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Rescue() {
        Debug.Log("========Thank you==========");
        Destroy(this.gameObject);
    }
}
