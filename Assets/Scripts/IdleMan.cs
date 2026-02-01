using TMPro;
using UnityEngine;

public class IdleMan : MonoBehaviour
{
    [SerializeField]
    private GameObject mask;

    [SerializeField] private GameObject Backstory; 

    [SerializeField]
    private GameObject head;
    public void ShowBackStory()
    {
        head.SetActive(true);
        mask.SetActive(false);

        Backstory.SetActive(true);

    }
}
