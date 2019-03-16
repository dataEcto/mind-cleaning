using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

//This is the general Game Controller, all located
//On the Progress Bar
//There is some code from another game I was attempting 
//to make during the summer, but I never finished it.
//This means some of the wording does get weird, but I will still
//Attempt to explain it
public class EndingProgressBar : MonoBehaviour
{
    /// <summary>
    /// Set Up Variables
    /// </summary>
    public float currentProgress { get; set; }

    public float maxProgress { get; set; }


    public Animator animator;

    /// <summary>
    /// In Game used Variables
    /// </summary>
    public Slider progressBar;
    public Graphic fillbar;
   

    public GameObject player;
    public bool resetBar;
    


    //Properties relating to Bushy, both the NPC and the Final Cleanable object
    public GameObject bushyModel;
    public GameObject bushyDoodle;
    public bool bushyStart;
    public bool bushyDone;
  



    //The properties that involve the DealDamage method
    bool dealingDamage;
    private bool dealingDamageAgain;
    Color damageColor;


    bool shouldFill;


    public GameObject plane;


    void Start()
    {
        //can be any value of course
        maxProgress = 100f;

        //Start at 0 progress for each item.
        currentProgress = 0;

        //Get the value of the slider 
        //set it to calculate progress
        progressBar.value = CalculateProgress();
        resetBar = false;
        
       

        bushyStart = false;
        bushyDone = false;
     

        dealingDamage = false;
        damageColor = Color.blue;



    }


    void Update()
    {


            //Disable the Bushy that is inside the house
            bushyDoodle.GetComponent<SpriteRenderer>().enabled = false;
            bushyDoodle.GetComponent<AudioSource>().enabled = false;


            //let da spooky begin
            bushyModel.GetComponent<AudioSource>().enabled = true;
            
            Debug.Log("The wall should be gone now");


            if (bushyStart)
            {
                
                animator.SetBool("shouldAppear",true);
                shouldFill = true;
                DealDamage(5);

                if (resetBar == false)
                {
                    currentProgress = 0;
                    resetBar = true;
                }
                
            }
            
            CleanBushy();

        

    }

    float CalculateProgress()
    {
        return currentProgress / maxProgress;
    }

    void addProgress(float progressGained)
    {
        Debug.Log("Adding: " + progressGained );
      
        currentProgress += progressGained;
        progressBar.value = CalculateProgress();

        //Prevent the player from restoring past full health
        if (currentProgress >= maxProgress)
        {
            currentProgress -= 1;
            //Debug.Log("Progress is full. Will no longer add more");
        }

    }

    void DealDamage(float damageValue)
    {

        //Deal damage to the progress bar
        currentProgress -= damageValue * Time.deltaTime;
        //Same as from start
        progressBar.value = CalculateProgress();
        Debug.Log(("Damage Done: " + damageValue));


    }

  
    
    void CleanBushy()
    {
            
        if (Input.GetKeyDown(KeyCode.Space) && shouldFill && bushyDone == false)
        {
            Debug.Log("Refill Less");
            addProgress(20);

        }
        else if (Input.GetKeyDown(KeyCode.Space) && shouldFill == false)
        {
            Debug.Log("Don't Fill");
        }

        if (currentProgress >= 99f && bushyDone == false)
        {
            bushyDone = true;
            currentProgress = 0;
            animator.SetBool("shouldAppear", false);
        


        }
        else if (bushyDone)
        {
            animator.SetBool("shouldAppear", false);
            //Get the player animating to do...something
            plane.GetComponent<MeshRenderer>().enabled = false;
            plane.GetComponent<MeshCollider>().enabled = false;
            plane.GetComponent<BoxCollider>().enabled = false;

        }
            
    }


}





