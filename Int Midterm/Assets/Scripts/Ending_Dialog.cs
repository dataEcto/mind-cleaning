using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Ending_Dialog : MonoBehaviour
{

    public bool isTriggered;
    public bool startingDialog;
    public Dialog convo;
    
    public DialogManager dialogManager;
    public ProgressBar progressScript;
    public Start_Dialog startDialog;
    public float convoTimer;

    // Start is called before the first frame update
    void Start()
    {
        dialogManager = FindObjectOfType<DialogManager>().GetComponent<DialogManager>();
        progressScript = FindObjectOfType<ProgressBar>().GetComponent<ProgressBar>();
        startDialog = FindObjectOfType<Start_Dialog>().GetComponent<Start_Dialog>();
        startingDialog = false;
        convoTimer = 6;
    }

    // Update is called once per frame
    void Update()
    {

        if (startingDialog)
        {
            if (progressScript.bushyStart == false)
            {
                convoTimer -= Time.deltaTime;
                if (isTriggered == true)
                {
                    TriggerDialog();
                    isTriggered = false;

                }
     
            }
      

        }

       //Starting Dialog Continue
        if (isTriggered ==  false && progressScript.oneStart == false)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) || convoTimer <= 0)
            {
                ContinueDialogue();
                convoTimer = 6;
                startDialog.enabled = false;
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
