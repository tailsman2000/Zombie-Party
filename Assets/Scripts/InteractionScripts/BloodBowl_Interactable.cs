using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class BloodBowl_Interactable : MonoBehaviour, I_Interactable
{
    
    private const string FILL_UP_BOWL_AMOUNT_ANIMATION_KEY = "FilledUpBowl";

    [SerializeField]
    private Image currentFillAmount;

    [SerializeField]
    private GameObject currentFillAmountBackground;


    [SerializeField]
    private float drainAmount; 

    [SerializeField]
    private Vector2 increaseAmountRanges;

    private Animator animator;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
        TaskManager.Instance.AddTask(TaskManager.TaskType.BloodBowl);
    }
    public void Interact()
    {
        EnableVisual(true);

        currentFillAmount.fillAmount += Random.Range(increaseAmountRanges.x, increaseAmountRanges.y);

        Player.Instance.Unmask();
        
        if(currentFillAmount.fillAmount >= 1f)
        {
            //Task complete 
            EnableVisual(false);  

            animator.SetBool(FILL_UP_BOWL_AMOUNT_ANIMATION_KEY, true);

            TaskManager.Instance.CompleteTask(TaskManager.TaskType.BloodBowl);

            //just so bloodbowl stays in scene
            this.enabled = false; 

        }
    }

    void Update()
    {
        if(currentFillAmount.fillAmount < 1f)
        {
            currentFillAmount.fillAmount -= drainAmount * Time.deltaTime;

            if(currentFillAmount.fillAmount <= 0f)
            {
                EnableVisual(false);
            }
        }

    }


    private void EnableVisual(bool enable)
    {
        currentFillAmount.gameObject.SetActive(enable);
        currentFillAmountBackground.SetActive(enable);
    }
}
