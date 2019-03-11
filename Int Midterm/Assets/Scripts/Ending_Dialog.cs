using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending_Dialog : MonoBehaviour
{
    private bool isTriggered;
    public Dialog convo;
    public DialogManager dialogManager;
  
    

    // Start is called before the first frame update
    void Start()
    {
        isTriggered = true;
        dialogManager = FindObjectOfType<DialogManager>().GetComponent<DialogManager>();
        TriggerDialog();
     
   
    }

    // Update is called once per frame
    void Update()
    {
     
          
          

      
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isTriggered == true)
        {
            TriggerDialog();
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                ContinueDialogue();
    
            }

            isTriggered = false;
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
