using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_Dialog : MonoBehaviour
{

    public bool isTriggered;
    public bool startingDialog;
    public Dialog convo;
    
    public DialogManager dialogManager;
    public ProgressBar progressScript;
    public GameObject startWall; 
    

    // Start is called before the first frame update
    void Start()
    {
        dialogManager = FindObjectOfType<DialogManager>().GetComponent<DialogManager>();
        progressScript = FindObjectOfType<ProgressBar>().GetComponent<ProgressBar>();
        startWall = GameObject.Find("Block");
        startingDialog = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (startingDialog)
        {
            if (progressScript.oneStart == false)
            {
                if (isTriggered == true)
                {
                    TriggerDialog();


                    isTriggered = false;

                }

            }

        }

        if (isTriggered == false)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                ContinueDialogue();
            }
        }



    }

    private void OnTriggerEnter(Collider other)
    {
     
            TriggerDialog();
            startingDialog = true;
            startWall.GetComponent<MeshRenderer>().enabled = true;
            startWall.GetComponent<BoxCollider>().enabled = true;
 


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
