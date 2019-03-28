using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final_Dialog : MonoBehaviour
{

    public bool isTriggered;
    public bool startingDialog;
    public Dialog convo;
    
    public DialogManager dialogManager;
    public float convoTimer;

    // Start is called before the first frame update
    void Start()
    {
        dialogManager = FindObjectOfType<DialogManager>().GetComponent<DialogManager>();
        startingDialog = false;
        convoTimer = 5;
    }

    // Update is called once per frame
    void Update()
    {

        if (startingDialog)
        {
           
                convoTimer -= Time.deltaTime;
                
                if (isTriggered == true)
                {
                    TriggerDialog();
                    isTriggered = false;

                }

                     
                if (convoTimer <= 0)
                {
                    ContinueDialogue();
                    convoTimer = 5;    
                }

        }




    }

    private void OnTriggerEnter(Collider other)
    {
     
            TriggerDialog();
            startingDialog = true;
          


    }

    public void TriggerDialog()
    {
        dialogManager.StartDialog(convo);
    }
    
    public void ContinueDialogue()
    {
        dialogManager.DisplayNextSentence();
    }

}
