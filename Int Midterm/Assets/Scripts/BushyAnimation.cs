using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushyAnimation : MonoBehaviour
{
    public Animator bushyAnim;
    public ProgressBar progressScript;
    public Camera mainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        progressScript = FindObjectOfType<ProgressBar>().GetComponent<ProgressBar>();
    }

    // Update is called once per frame
    void Update()
    {
     
        transform.LookAt(mainCamera.transform.position, Vector3.up);
        if (progressScript.oneDone)
        {
             //Make Bushy move nearby
             bushyAnim.SetBool("byCouch",true);
            
        }

        if (progressScript.twoDone)
        {
            bushyAnim.SetBool("byCouch",false);
            bushyAnim.SetBool("bySwitch",true);
        }

        
      
        if (progressScript.threeDone) //This also accounts for when fourDone is true
        {
            bushyAnim.SetBool("bySwitch",false);
            bushyAnim.SetBool("byCoffeeBowl",true);
        }

        if (progressScript.repeatOneDone)
        {
            bushyAnim.SetBool("byCoffeeBowl",false);
            bushyAnim.SetBool("byCouch",true);
        }

        if (progressScript.repeatTwoDone)
        {
            bushyAnim.SetBool("byCoffeeBowl",true);
            bushyAnim.SetBool("byCouch",false);
        }

        if (progressScript.repeatThreeDone)
        {
            bushyAnim.SetBool("bySwitch",true);
            bushyAnim.SetBool("byCoffeeBowl",false);
        }
    }

    public void Billboard()
    {
        transform.LookAt(mainCamera.transform.position, Vector3.up);
        
    }
}
