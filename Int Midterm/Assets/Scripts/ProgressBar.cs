using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

//This is code from another game I was attempting
//Some of the captilzation is off, so I apologize
public class ProgressBar : MonoBehaviour
{
    /// <summary>
    /// Set Up Variables
    /// </summary>
    public float currentProgress { get; set; }

    public float maxProgress { get; set; }
    //public float ProgressRefillRate;

    //public TextMeshProUGUI progressBarText;
    public Animator animator;

    /// <summary>
    /// In Game used Variables
    /// </summary>
    public Slider progressBar;
    public Graphic fillbar;
   

    public GameObject player;
    public bool resetBar;
    
    
    public GameObject objectOne;
    public GameObject couchModel;
    public GameObject couchDoodle;
    public bool oneDone;
    public bool oneStart;
    
    public GameObject objectTwo;
    public GameObject switchModel;
    public GameObject switchDoodle;
    public bool twoDone;
    public bool twoStart;
    
    public GameObject objectThree;
    public GameObject coffeeModel;
    public GameObject coffeeDoodle;
    public bool threeDone;
    public bool threeStart;

    public GameObject objectFour;
    public GameObject bowlModel;
    public GameObject bowlDoodle;
    public bool fourDone;
    public bool fourStart;

    //Other Minigames to fill up bar trigger
    bool dealingDamage;
    private bool dealingDamageAgain;
    Color damageColor;


    bool shouldFill;

    //Prototype Stuff
    public Dialog convo;
    public DialogManager dialogManager;

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
        
        oneDone = false;
        oneStart = false;
        couchModel = GameObject.Find("Couch");
       
        
        twoDone = false;
        twoStart = false;
        switchModel = GameObject.Find("Switch");
        
        
        threeDone = false;
        twoStart = false;
        coffeeModel = GameObject.Find("CoffeeCup");

        fourDone = false;
        fourStart = false;
        
        
        

        dealingDamage = false;
        dealingDamageAgain = false;
        damageColor = Color.blue;
    }


    void Update()
    {

        
        //Object One
        if (oneStart)
        {
            shouldFill = true;
            animator.SetBool("shouldAppear", true);
            Debug.Log("Run function");
            CleanObjectOne();
            oneStart = false;
        }


        //Object Two
        if (oneDone)
        {
            //Disable the previous Sprite
            //then change it to its new model
            couchDoodle.GetComponent<SpriteRenderer>().enabled = false;
            couchModel.GetComponent<MeshRenderer>().enabled = true;

            if (twoStart)
            {
             
                shouldFill = true;
                
                if (resetBar == false)
                {
                    currentProgress = 0;
                    resetBar = true;
                    Debug.Log("Reset please");
                }
               
                CleanObjectTwo();
                animator.SetBool("shouldAppear", true);
            }

        }
        
        
        //Object Three
        if (twoDone == true)
        {
            //Disable the previous Sprite
            //then change it to its new model
            switchDoodle.GetComponent<SpriteRenderer>().enabled = false;
            switchModel.GetComponent<MeshRenderer>().enabled = true;
                
            animator.SetBool("shouldAppear", false);
            
            fillbar.color = damageColor;

            if (dealingDamage == false)
            {
                currentProgress = 50;
                dealingDamage = true;
            }

            if (threeStart)
            {
                shouldFill = true;
                animator.SetBool("shouldAppear", true);
                CleanObjectThree();
                DealDamage(15);
            }

        }

        
        //Object 4
        if (threeDone == true)
        {
            //Disable the previous Sprite
            //then change it to its new model
            coffeeDoodle.GetComponent<SpriteRenderer>().enabled = false;
            coffeeModel.GetComponent<MeshRenderer>().enabled = true;
           
            animator.SetBool("shouldAppear", false);
            DealDamage(0);
         
            
            fillbar.color = damageColor;

            if (dealingDamageAgain == false)
            {
                currentProgress = 50;
                dealingDamageAgain = true;
            }

            if (fourStart)
            {
                shouldFill = true;
                animator.SetBool("shouldAppear", true);
                CleanObjectFour();
                DealDamage(25);
            }

        }

        if (fourDone == true)
        {
            animator.SetBool("shouldAppear", false);
            shouldFill = false;
            Debug.Log("Change to model");
        }


    }

    float CalculateProgress()
    {
        return currentProgress / maxProgress;
    }

    void addProgress(float progressGained)
    {
        Debug.Log("Adding Progress");
      
        currentProgress += progressGained;
        progressBar.value = CalculateProgress();

        //Prevent the player from restoring past full health
        if (currentProgress >= maxProgress)
        {
            currentProgress -= 1;
            Debug.Log("Progress is full. Will no longer add more");
        }

    }

    void DealDamage(float damageValue)
    {

        //Deal damage to the progress bar
        currentProgress -= damageValue * Time.deltaTime;
        //Same as from start
        progressBar.value = CalculateProgress();


    }

    void CleanObjectOne()
    {
       
        if (Input.GetKeyDown(KeyCode.Space) && shouldFill && oneDone == false)
        {
            addProgress(10);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && shouldFill == false)
        {
            Debug.Log("Don't Fill");
        }

        if (currentProgress >= 99f && oneDone == false)
        {
            oneDone = true;
            shouldFill = false;
            currentProgress = 0;
            animator.SetBool("shouldAppear", false);
            objectOne.GetComponent<Animator>().SetBool("isClean", true);
            
        }
        else if (oneDone == true)
        {
            currentProgress = 0;
            animator.SetBool("shouldAppear", false);
            

        }
    }

    void CleanObjectTwo()
    {
           if (Input.GetKeyDown(KeyCode.Space) && shouldFill && twoDone == false)
           {
                        addProgress(10);
           }
           else if (Input.GetKeyDown(KeyCode.Space) && shouldFill == false)
           {
                Debug.Log("Don't Fill");
           }
        
            if (currentProgress >= 95f && twoDone == false)
            {
                        twoDone = true;
                        currentProgress = 0;
                        animator.SetBool("shouldAppear", false);
                        objectTwo.GetComponent<Animator>().SetBool("isCleanTwo", true);
            }
            else if (twoDone == true)
            {    
                        animator.SetBool("shouldAppear", false);
                        Debug.Log("Go away!");
            }
    }

    void CleanObjectThree()
    {
        if (Input.GetKeyDown(KeyCode.Space) && shouldFill && dealingDamage && threeDone == false)
        {
            Debug.Log("Refill Less");
            addProgress(6);
            
        }
        else if (Input.GetKeyDown(KeyCode.Space) && shouldFill == false)
        {
            Debug.Log("Don't Fill");
        }

        if (currentProgress >= 99f && threeDone == false)
        {
            threeDone = true;
            currentProgress = 0;
            animator.SetBool("shouldAppear", false);
            objectThree.GetComponent<Animator>().SetBool("isCleanThree", true);
          

        }
        else if (threeDone == true)
        {
            animator.SetBool("shouldAppear", false);
            DealDamage(0);
        }
        
    }
    
    void CleanObjectFour()
    {
        if (Input.GetKeyDown(KeyCode.Space) && shouldFill && dealingDamage && fourDone == false)
        {
            Debug.Log("Refill Less");
            addProgress(9);
            
        }
        else if (Input.GetKeyDown(KeyCode.Space) && shouldFill == false)
        {
            Debug.Log("Don't Fill");
        }

        if (currentProgress >= 99f && fourDone == false)
        {
            fourDone = true;
            currentProgress = 0;
            animator.SetBool("shouldAppear", false);
            objectThree.GetComponent<Animator>().SetBool("isCleanThree", true);
       

        }
        else if (fourDone == true)
        {
            animator.SetBool("shouldAppear", false);
            DealDamage(0);
        }
        
    }


  

   
}





