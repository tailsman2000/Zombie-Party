using UnityEngine;
using TMPro;
public class TaskManager : MonoBehaviour
{
    public static TaskManager instance { get; private set; }
    public enum TaskType
    {
        SANDWICH = 0,
        VACUUM = 1,
        BLOOD = 2,
        SAMPLE = 3,
        BRAIN = 4
    }
    private int[] progress = new int[5];
    [SerializeField] private int[] required = {1, 5, 1, 5, 1};
    [SerializeField] private TextMeshProUGUI[] taskTexts;
    private string[] taskDescriptions;
    [SerializeField] private Color completedColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        for(int i = 0; i < taskTexts.Length; i++)
        {
            taskDescriptions[i] = taskTexts[i].text;
        }
    }
    private void SetTaskText(int index)
    {
        taskTexts[index].text = taskDescriptions[index] + " (" + progress[index] + "/" + required[index] + ")";
    }
    public void TaskProgress(TaskType type)
    {
        progress[(int)type]++;
        taskTexts[(int)type].text = taskDescriptions[(int)type] + " (" + progress[(int)type] + "/" + required[(int)type] + ")";
        if(progress[(int)type] >= required[(int)type])
        {
            TaskCompleted(type);
        }
    }
    private void TaskCompleted(TaskType type)
    {
        taskTexts[(int)type].color = completedColor;
    }
}
