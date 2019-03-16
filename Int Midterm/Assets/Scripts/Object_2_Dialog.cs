using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_2_Dialog : MonoBehaviour
{
    private bool isTriggered;
    public Dialog convo;
    public DialogManager dialogManager;
    public ProgressBar progressScript;
    public float convoTimer;

    // Start is called before the first frame update
    void Start()
    {
        isTriggered = true;
        dialogManager = FindObjectOfType<DialogManager>().GetComponent<DialogManager>();
        progressScript = FindObjectOfType<ProgressBar>().GetComponent<ProgressBar>();
        convoTimer = 3;

    }

    // Update is called once per frame
    void Update()
    {
        if (progressScript.twoDone == true)
        {
            if (isTriggered == true)
            {
                TriggerDialog();
                
                if (Input.GetKeyDown(KeyCode.Mouse0) || convoTimer <= 0)
                {
                    ContinueDialogue();
                    convoTimer = 3;
                }

                isTriggered = false;
            }
          

         
        }
        
        
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
