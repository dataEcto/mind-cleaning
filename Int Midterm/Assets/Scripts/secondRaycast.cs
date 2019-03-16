

using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class secondRaycast : MonoBehaviour
{

    public float maxDistance;
    public EndingProgressBar endProgressBar;

  

    private void Start()
    {

        endProgressBar = FindObjectOfType<EndingProgressBar>().GetComponent<EndingProgressBar>();

    }

    void Update()
    {

 
        Ray playerRay = new Ray(this.transform.position, this.transform.forward);

        Debug.DrawRay(playerRay.origin, playerRay.direction * maxDistance, Color.green);

        RaycastHit hit;

        if (Physics.Raycast(playerRay.origin, playerRay.direction, out hit, maxDistance))
        {
           

            if (hit.transform.gameObject.tag == "The End")
            {

                Debug.Log("Final");
                endProgressBar.bushyStart = true;

            }
            
        }

      
    }
}
