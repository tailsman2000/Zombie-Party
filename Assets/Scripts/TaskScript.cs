using UnityEngine;

public class TaskScript : MonoBehaviour
{
    private bool interactable;
    [SerializeField] private TaskManager.TaskType taskType;
    [SerializeField] private GameObject yellowAura;
    [SerializeField] private bool needsVacuum;
    [SerializeField] private bool needsJam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactable = true;
        yellowAura.SetActive(interactable);
        EnableInteract();
        if(needsJam)
        {
            DisableInteract();
            JamInteractable.equipJam += EnableInteract;
            JamInteractable.unequipJam += DisableInteract;
        }
        if(needsVacuum)
        {
            DisableInteract();
            VacumeInteractable.equipVacuum += EnableInteract;
            VacumeInteractable.unequipVacuum += DisableInteract;
        }
    }

    public void EnableInteract()
    {
        interactable = true;
        yellowAura.SetActive(interactable);
    }

    public void DisableInteract()
    {
        interactable = false;
        yellowAura.SetActive(interactable);
    }

    public void TaskComplete()
    {
        TaskManager.instance.TaskProgress(taskType);
        DisableInteract();
        this.enabled = false;
    }
}
