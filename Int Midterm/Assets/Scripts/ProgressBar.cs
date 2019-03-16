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
public class ProgressBar : MonoBehaviour
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
    
    //Properties relating to the First Object to clean, The Couch
    public GameObject objectOne;
    public GameObject couchModel;
    public GameObject couchDoodle;
    public bool oneDone;
    public bool oneStart;
    public bool repeatOne;
    public bool repeatOneDone; 
    
    //Properties relating to the Second Object to clean, The Nintendo Switch
    public GameObject objectTwo;
    public GameObject switchModel;
    public GameObject switchDoodle;
    public bool twoDone;
    public bool twoStart;
    public bool repeatThree;
    public bool repeatThreeDone;
    
    
    //Properties relating to the Third Object to clean, The Coffee Cup
    public GameObject objectThree;
    public GameObject coffeeModel;
    public GameObject coffeeDoodle;
    public bool threeDone;
    public bool threeStart;
    public bool repeatTwo;
    public bool repeatTwoDone;

    //Properties relating to the Fourth Object to clean, the Bowl
    public GameObject objectFour;
    public GameObject bowlModel;
    public GameObject bowlDoodle;
    public bool fourDone;
    public bool fourStart;
    public bool repeatFour;
    public bool repeatFourDone;


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

   
    public GameObject wall;


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
        repeatOne = false;
        repeatOneDone = false;
        
        twoDone = false;
        twoStart = false;
        switchModel = GameObject.Find("Switch");
        repeatThree = false;
        repeatThreeDone = false;
        
        threeDone = false;
        threeStart = false;
        coffeeModel = GameObject.Find("CoffeeCup");
        repeatTwo = false;
        repeatTwoDone = false;
        
        fourDone = false;
        fourStart = false;
        repeatFour = false;
        repeatFourDone = false;

        bushyStart = false;
        bushyDone = false;
     
        
        //Set all other doodle objects to be invisible at first.
        switchDoodle.GetComponent<SpriteRenderer>().enabled = false;
        coffeeDoodle.GetComponent<SpriteRenderer>().enabled = false;
        bowlDoodle.GetComponent<SpriteRenderer>().enabled = false;
        

        dealingDamage = false;
        damageColor = Color.blue;
        
        wall = GameObject.Find("Ending Wall");


    }


    void Update()
    {

       
        //Object One
        //The Couch
        //This oneStart bool (and other variants) comes from the Raycast script
        if (oneStart)
        {
            shouldFill = true;
            animator.SetBool("shouldAppear", true);
            CleanObjectOne();
            oneStart = false;
     

        }


        //Object Two
        //The Nintendo Switch
        if (oneDone)
        {
            //Disable the previous Sprite
            //then change it to its new model
            //I could have set this up before in start, but
            //but by the time I realized that, it was...too late
            couchDoodle.GetComponent<SpriteRenderer>().enabled = false;
            couchModel.GetComponent<MeshRenderer>().enabled = true;

            //Enabled the next object to appear
            switchDoodle.GetComponent<SpriteRenderer>().enabled = true;
            
           
            
            
            if (twoStart)
            {
                shouldFill = true;  
                CleanObjectTwo();
                animator.SetBool("shouldAppear", true);
            }

        }
        
        
        //Object Three
        //The Coffee Cup
        if (twoDone == true)
        {
            //Disable the previous Sprite
            //then change it to its new model
            switchDoodle.GetComponent<SpriteRenderer>().enabled = false;
            switchModel.GetComponent<MeshRenderer>().enabled = true;

            coffeeDoodle.GetComponent<SpriteRenderer>().enabled = true;
            
            
           
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
                DealDamage(10);
            }
            
            if (currentProgress <= 0)
            {
                currentProgress = 1;
            }

        }


        
        //Object 4
        //The Bowl
        if (threeDone == true)
        {
            //Disable the previous Sprite
            //then change it to its new model
            coffeeDoodle.GetComponent<SpriteRenderer>().enabled = false;
            coffeeModel.GetComponent<MeshRenderer>().enabled = true;
         
            bowlDoodle.GetComponent<SpriteRenderer>().enabled = true;
           
            animator.SetBool("shouldAppear", false);

         
            fillbar.color = damageColor;

            if (currentProgress <= 0)
            {
                currentProgress = 1;
            }
        

            if (fourStart)
            {
                
                if (dealingDamage == false)
                {
                    currentProgress = 50;
                    dealingDamage = true;
                }
                shouldFill = true;
                animator.SetBool("shouldAppear", true);
                CleanObjectFour();
                DealDamage(10);
            }

        }

        //Object "5"
        //The Couch Again
        if (fourDone == true)
        {
            //Disable certain object 4 stuff as per usual
            bowlDoodle.GetComponent<SpriteRenderer>().enabled = false;
            bowlModel.GetComponent<MeshRenderer>().enabled = true;
            
            animator.SetBool("shouldAppear", false);
            
            couchDoodle.GetComponent<SpriteRenderer>().enabled = true;
            couchModel.GetComponent<MeshRenderer>().enabled = false;

            if (repeatOne)
            {
                animator.SetBool("shouldAppear", true);
                shouldFill = true;
                DealDamage(20);
                
                if (resetBar == false)
                {
                    currentProgress = 0;
                    resetBar = true;
                }
               
                CleanObjectOne();
                
                
            }
        }

        //Object "6"
        //The Cup Again
        if (repeatOneDone)
        {

            couchDoodle.GetComponent<SpriteRenderer>().enabled = false;
            couchModel.GetComponent<MeshRenderer>().enabled = true;
            
            animator.SetBool("shouldAppear", false);

            coffeeDoodle.GetComponent<SpriteRenderer>().enabled = true;
            coffeeModel.GetComponent<MeshRenderer>().enabled = false;

            if (repeatTwo)
            {

                animator.SetBool("shouldAppear", true);
                shouldFill = true;
                DealDamage(30);
                
                if (resetBar == false)
                {
                    currentProgress = 0;
                    resetBar = true;
                }
               
                CleanObjectThree();

            }


        }
        
        //Object "7"
        //The Nintendo Switch Again
        if (repeatTwoDone)
        {
            coffeeDoodle.GetComponent<SpriteRenderer>().enabled = false;
            coffeeModel.GetComponent<MeshRenderer>().enabled = true;
            
            animator.SetBool("shouldAppear",false);

            switchDoodle.GetComponent<SpriteRenderer>().enabled = true;
            switchModel.GetComponent<MeshRenderer>().enabled = false;
            
            if (repeatThree)
            {
                Debug.Log("bar should appear for this");
                animator.SetBool("shouldAppear", true);
                shouldFill = true;
                DealDamage(40);
                
                if (resetBar == false)
                {
                    currentProgress = 0;
                    resetBar = true;
                }
               
                CleanObjectTwo();

            }

        }

        if (repeatThreeDone)
        {
            switchDoodle.GetComponent<SpriteRenderer>().enabled = false;
            switchModel.GetComponent<MeshRenderer>().enabled = true;
            
            animator.SetBool("shouldAppear",false);

            bowlDoodle.GetComponent<SpriteRenderer>().enabled = true;
            bowlModel.GetComponent<MeshRenderer>().enabled = false;

            if (repeatFour)
            {
                
                animator.SetBool("shouldAppear",true);
                shouldFill = true;
                DealDamage(40);

                if (resetBar == false)
                {
                    currentProgress = 0;
                    resetBar = true;
                }


              
            }
            
            CleanObjectFour();


        }

        if (repeatFourDone)
        {
            wall.GetComponent<MeshRenderer>().enabled = false;
            wall.GetComponent<BoxCollider>().enabled = false;

            bushyDoodle.GetComponent<AudioSource>().enabled = true;

            couchModel.GetComponent<MeshRenderer>().enabled = false;
            switchModel.GetComponent<MeshRenderer>().enabled = false;
            coffeeModel.GetComponent<MeshRenderer>().enabled = false;
            bowlDoodle.GetComponent<SpriteRenderer>().enabled = false;
            
            
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

    void CleanObjectOne()
    {
       
        if (Input.GetKeyDown(KeyCode.Space) && shouldFill && oneDone == false )
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
        else if (oneDone == true && repeatOne == false)
        {
            
            currentProgress = 0;
            animator.SetBool("shouldAppear", false);
  
        }
        
        //For when it goes back to the couch
        if (Input.GetKeyDown(KeyCode.Space) && shouldFill && repeatOne && repeatOneDone == false)
        {
     
            addProgress(5);
        }
        else if (shouldFill == false) 
        {
            Debug.Log("Do not fill the repeat couch 1");
        }

     
         if (currentProgress >= 99f && repeatOneDone == false)
         {
                repeatOneDone = true;
                shouldFill = false;
                currentProgress = 0;
                objectOne.GetComponent<Animator>().SetBool("isClean", true);
         }
         else if (repeatOneDone)
         {
             Debug.Log("Bar should go away");
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
        else if (twoDone == true && repeatThree == false)
        {
            animator.SetBool("shouldAppear", false);

        }
        
        //For when it goes back to the Switch
        if (Input.GetKeyDown(KeyCode.Space) && shouldFill && repeatThree && repeatThreeDone == false)
        {
            addProgress(20);
        }

        if (currentProgress >= 99f && repeatThreeDone == false)
        {
            repeatThreeDone = true;
            shouldFill = false;
            currentProgress = 0;
            objectOne.GetComponent<Animator>().SetBool("isClean", true);
        }
        else if (repeatThreeDone)
        {
            repeatThreeDone = true;
            
        }

    }
    
    void CleanObjectThree()
    {
        if (Input.GetKeyDown(KeyCode.Space) && shouldFill && dealingDamage && threeDone == false)
        {
            Debug.Log("Refill Less");
            addProgress(15);

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
        else if (threeDone == true && repeatTwo == false)
        {
            animator.SetBool("shouldAppear", false);
        
        }
        
        //For when it goes back to the Cup
        if (Input.GetKeyDown(KeyCode.Space) && shouldFill && repeatTwo && repeatTwoDone == false)
        {

            addProgress(10);
        }
        else if (shouldFill == false) 
        {
            Debug.Log("Do not fill the repeat couch 1");
        }

     
        if (currentProgress >= 99f && repeatTwoDone == false)
        {
            repeatTwoDone = true;
            shouldFill = false;
            currentProgress = 0;
            objectOne.GetComponent<Animator>().SetBool("isClean", true);
        }
        else if (repeatTwoDone)
        {
            Debug.Log("Do everything in here");
            shouldFill = false;
            repeatTwoDone = true;
            animator.SetBool("shouldAppear", false);
        }

    }
    
    void CleanObjectFour()
    {
        if (Input.GetKeyDown(KeyCode.Space) && shouldFill && dealingDamage && fourDone == false)
        {
            Debug.Log("Refill Less");
            addProgress(5);

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
        else if (fourDone == true && repeatFour == false)
        {
            animator.SetBool("shouldAppear", false);

        }
        
        //The final object!
        if (Input.GetKeyDown(KeyCode.Space) && shouldFill && repeatFour && repeatFourDone == false)
        {

            addProgress(15);
        }
        else if (shouldFill == false) 
        {
            Debug.Log("Do not fill bowl again");
        }

     
        if (currentProgress >= 99f && repeatFourDone == false)
        {
            repeatFourDone = true;
            shouldFill = false;
            currentProgress = 0;
            objectOne.GetComponent<Animator>().SetBool("isClean", true);
        }
        else if (repeatFourDone)
        {
        
            shouldFill = false;
            repeatFourDone = true;
            animator.SetBool("shouldAppear",false);
     
        }

      
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
            SceneManager.LoadScene(1);

        }
            
    }


}





