using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Start_Dialog : MonoBehaviour
{

    public bool isTriggered;
    public bool startingDialog;
    public Dialog convo;
    
    public DialogManager dialogManager;
    public ProgressBar progressScript;
    public GameObject startWall;
    public float convoTimer;

    // Start is called before the first frame update
    void Start()
    {
        dialogManager = FindObjectOfType<DialogManager>().GetComponent<DialogManager>();
        progressScript = FindObjectOfType<ProgressBar>().GetComponent<ProgressBar>();
        startWall = GameObject.Find("Block");
        startingDialog = false;
        convoTimer = 4;
    }

    // Update is called once per frame
    void Update()
    {

        if (startingDialog)
        {
            if (progressScript.oneStart == false)
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
            if (Input.GetKeyDown(KeyCode.Mouse0) || convoTimer <= -2)
            {
                ContinueDialogue();
                convoTimer = 7;
            }
        }

        //Reset to 3 for every object to clean
        if (progressScript.oneStart && progressScript.oneDone == false)
        {
            convoTimer = 4;
        }

        //Reset convoTimer back to 3
        //Then count down again once the object is clean
        //This prevents dialog being skipped due to convoTimer going down when
        //coincidently, the object is done being clean.
        if (progressScript.oneDone && progressScript.twoStart == false)
        {
            convoTimer -= Time.deltaTime;
            Debug.Log("Countdown Object 1");
            if (convoTimer <= 0 && progressScript.repeatOneDone)
            {
                convoTimer = 4;
                ContinueDialogue();
                Debug.Log("dialog 1 continue");
            }
        }
        
        //Object 2
        if (progressScript.twoStart && progressScript.twoDone == false)
        {
            Debug.Log("Object 2");
            convoTimer = 7;
        }

        
        if (progressScript.twoDone && progressScript.threeStart == false)
        {
            convoTimer -= Time.deltaTime;

            if (convoTimer <= 0)
            {
                convoTimer = 7;
                ContinueDialogue();
            }
        }
        
        //Object 3
        if (progressScript.threeStart && progressScript.threeDone == false)
        {
            Debug.Log("Object 3");
            convoTimer = 7;
        }

        
        if (progressScript.threeDone && progressScript.fourStart == false)
        {
            convoTimer -= Time.deltaTime;

            if (convoTimer <= 0)
            {
                convoTimer = 7;
                ContinueDialogue();
            }
        }
        
        //Object 4
        if (progressScript.fourStart && progressScript.fourDone == false)
        {
            Debug.Log("Object 4");
            convoTimer = 7;
        }

        
        if (progressScript.fourDone && progressScript.repeatOne == false)
        {
            convoTimer -= Time.deltaTime;

            if (convoTimer <= 0)
            {
                convoTimer = 7;
                ContinueDialogue();
            }
        }
        
        //Object 5
        if (progressScript.repeatOne && progressScript.repeatOneDone == false)
        {
            Debug.Log("Object 5");
            convoTimer = 7;
        }

        
        if (progressScript.repeatOneDone && progressScript.repeatTwo == false)
        {
            convoTimer -= Time.deltaTime;

            if (convoTimer <= 0)
            {
                convoTimer = 7;
                ContinueDialogue();
            }
        }
        
        //Object 6
        if (progressScript.repeatTwo && progressScript.repeatTwoDone == false)
        {
            Debug.Log("Object 6");
            convoTimer = 7;
        }
         
                 
        if (progressScript.repeatTwoDone && progressScript.repeatThree == false)
        {
            convoTimer -= Time.deltaTime;
         
            if (convoTimer <= 0)
            {
                convoTimer = 7;
                ContinueDialogue();
            }
        }
        
        //Object 7
        if (progressScript.repeatThree && progressScript.repeatThreeDone == false)
        {
            Debug.Log("Object 7");
            convoTimer = 7;
        }
         
                 
        if (progressScript.repeatThreeDone && progressScript.repeatFour == false)
        {
            convoTimer -= Time.deltaTime;
         
            if (convoTimer <= 0)
            {
                convoTimer = 7;
                ContinueDialogue();
            }
        }
        
        //Object 8
        if (progressScript.repeatFour && progressScript.repeatFourDone == false)
        {
            Debug.Log("Object 7");
            convoTimer = 7;
        }
         
                 
        if (progressScript.repeatFourDone && progressScript.bushyStart == false)
        {
            convoTimer -= Time.deltaTime;
         
            if (convoTimer <= 0)
            {
                convoTimer = 7;
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
