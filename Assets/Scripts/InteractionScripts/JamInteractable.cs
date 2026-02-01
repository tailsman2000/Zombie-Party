using UnityEngine;

public class JamInteractable : MonoBehaviour, I_Interactable
{
    public bool eqipped = false;

    [SerializeField] private GameObject arrowIndicator;

     public void Interact()
    {
        if(!eqipped)
        {
            Player player = Player.Instance;

            this.transform.SetParent(player.interactAttachPoint);

            player.isHoldingSomething = true;

            transform.localPosition = Vector3.zero;

            player.Unmask();
            
            eqipped = true;            
        }
        else
        {
            transform.SetParent(null, true);  

            Player.Instance.isHoldingSomething = false;

            eqipped = false; 
        }


        arrowIndicator.SetActive(!eqipped);
        
    }
    
}
