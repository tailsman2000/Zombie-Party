using System;
using UnityEngine;
using UnityEngine.UI;

public class DeadbodyCleanup : MonoBehaviour
{
    [SerializeField]
    private Image currentCleanUpProgressImage; 

    [SerializeField] 
    private float cleanUpSpeed;


    void OnTriggerEnter2D(Collider2D collision)
    {
        currentCleanUpProgressImage.gameObject.SetActive(true);

        //Just the background child 
        transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
    }

    void OnTriggerStay2D(Collider2D collision)
    {

        if(collision.TryGetComponent(out VacumeInteractable vacum) && vacum.eqipped)
        {
            currentCleanUpProgressImage.fillAmount += cleanUpSpeed * Time.deltaTime;    
        }

        if(currentCleanUpProgressImage.fillAmount >= 1f)
        {
            //Cleaning done, task done
        }
    }


}
