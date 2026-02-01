using System;
using UnityEngine;

public class JamInteractable : MonoBehaviour, I_Interactable
{
    public bool eqipped = false;
    public static Action equipJam;
    public static Action unequipJam;

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

            equipJam?.Invoke();
        }
        else
        {
            transform.SetParent(null, true);  

            Player.Instance.isHoldingSomething = false;

            eqipped = false; 

            unequipJam?.Invoke();
        }
        
    }
    
}
