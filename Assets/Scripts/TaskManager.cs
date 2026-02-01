using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance {get; private set;}

    public enum TaskType
    {
        DeadBody, 
        Jam,
        BloodBowl         
    }


    public event EventHandler OnTasksComplete;

    public int deadBodyTasks;

    public int jamTasks;

    public int bloodBowlTasks;


    void Awake()
    {
        Instance = this; 

        deadBodyTasks = 0;
        jamTasks = 0;
        bloodBowlTasks = 0;
    }


    public void AddTask(TaskType type)
    {
        if(type == TaskType.DeadBody)
        {
            deadBodyTasks++;
        } else if(type == TaskType.BloodBowl)
        {
            bloodBowlTasks++;
        } else
        {
            jamTasks++;
        }
    }


    public void CompleteTask(TaskType type)
    {
        if(type == TaskType.DeadBody)
        {
            deadBodyTasks--;
        } else if(type == TaskType.BloodBowl)
        {
            bloodBowlTasks--;
        } else
        {
            jamTasks--;
        }


        if(deadBodyTasks + bloodBowlTasks + jamTasks <= 0)
        {
            //Listen to this, fires when tasks are all done
            OnTasksComplete?.Invoke(this, EventArgs.Empty);
            Debug.Log("TASKS COMPLETE");
        
        }
    }


    void Update()
    {
        // Debug.Log("Dead Bodies that need cleaning with vacum: " + deadBodyTasks);
        // Debug.Log("Blood bowls that need to be refilled: " + bloodBowlTasks);
        // Debug.Log("Jam that needs to be put back in kitchen: " + jamTasks);
    }

}