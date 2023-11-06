using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class Hostage : MonoBehaviour
{
    public TMP_Text scoreText;
    public HostageManager hostageManager;

    // Start is called before the first frame update
    void Start()
    {
        XRSimpleInteractable interactable = GetComponent<XRSimpleInteractable>();
        scoreText = GameObject.FindGameObjectWithTag("ScoreBoard").GetComponent<TMP_Text>();
        hostageManager = GameObject.FindObjectOfType<HostageManager>();
        //interactable.activated.AddListener(Rescue);
        //interactable.hoverEntered.AddListener(Rescue);
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "SCORE: " + hostageManager.getCurrHostages() + "/" + hostageManager.numberOfHostages;
    }

    public void Rescue() {
        Debug.Log("========Thank you==========");
        hostageManager.hostageRescued();
        Destroy(this.gameObject);
    }
}
