using TMPro.SpriteAssetUtilities;
using UnityEngine;

public class JamReturnPlace : MonoBehaviour
{
    void Start()
    {
        TaskManager.Instance.AddTask(TaskManager.TaskType.Jam);

    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out JamInteractable jam) && !jam.eqipped)
        {
            jam.transform.position = this.transform.position;
            //task complete
            //idk prolly add sum particles
            TaskManager.Instance.CompleteTask(TaskManager.TaskType.Jam);
        }
    }
}
