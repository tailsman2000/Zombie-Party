using System;
using UnityEngine;
using UnityEngine.UI;

public class DeadbodyCleanup : MonoBehaviour
{
    [SerializeField]
    private Image currentCleanUpProgressImage; 

    [SerializeField] private GameObject background;

    [SerializeField] 
    private float cleanUpSpeed;

    [SerializeField] private GameObject arrowIndicator;

    void Start()
    {
        TaskManager.Instance.AddTask(TaskManager.TaskType.DeadBody);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out VacumeInteractable vacum) && vacum.eqipped)
        {
            currentCleanUpProgressImage.gameObject.SetActive(true);
            background.SetActive(true);

            arrowIndicator.SetActive(false);
            //Just the background child 
        }
       
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
            TaskManager.Instance.CompleteTask(TaskManager.TaskType.DeadBody);

            //get rid of body 

            Debug.Log("DESTROY ME");
            Destroy(this.gameObject);
        }
    }


}
