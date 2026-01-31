using UnityEngine;

public class TestingInteraction : MonoBehaviour, I_Interactable
{
    public void Interact()
    {
        this.GetComponent<SpriteRenderer>().color = Color.red;
    }
     
}
