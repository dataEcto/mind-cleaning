

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastBehavior : MonoBehaviour
{

    public float maxDistance;
    public ProgressBar progressScript;
  

    private void Start()
    {
        progressScript = FindObjectOfType<ProgressBar>().GetComponent<ProgressBar>();
       
    }

    void Update()
    {

 
        Ray playerRay = new Ray(this.transform.position, this.transform.forward);

        Debug.DrawRay(playerRay.origin, playerRay.direction * maxDistance, Color.green);

        RaycastHit hit;

        if (Physics.Raycast(playerRay.origin, playerRay.direction, out hit, maxDistance))
        {
           
            if (hit.transform.gameObject.tag == "Cleaning Object 1")
            {
                progressScript.oneStart = true;
              
                
                
                //This makes the couch the "5th" object
                //We are essentially looping back to the start
                if (progressScript.fourDone)
                {
                    
                    progressScript.repeatOne = true;

                }
            }
            
            if (hit.transform.gameObject.tag == "Cleaning Object 2" && progressScript.oneDone == true)
            {
                progressScript.twoStart = true;
               
                //The switch is the "7th" object now
                if (progressScript.repeatTwoDone)
                {
                    Debug.Log("Begin cleaning the switch again");
                    progressScript.repeatThree = true;
                }
            }

            if (hit.transform.gameObject.tag == "Cleaning Object 3" && progressScript.twoDone == true)
            {
                progressScript.threeStart = true;
                
                //Make the revisit Cup the "6th" object
                if (progressScript.repeatOneDone)
                {
                    
                    progressScript.repeatTwo = true;
                }
           
            }
            
            if (hit.transform.gameObject.tag == "Cleaning Object 4" && progressScript.threeDone == true)
            {
                progressScript.fourStart = true;

                if (progressScript.repeatThreeDone)
                {
                    progressScript.repeatFour = true;
                }
             
            }

            if (hit.transform.gameObject.tag == "Finish")
            {

                Debug.Log("Hit bushy");
                progressScript.bushyStart = true;
                

            }
            
        }

      
    }
}
