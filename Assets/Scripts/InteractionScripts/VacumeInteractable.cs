using System;
using UnityEngine;

public class VacumeInteractable : MonoBehaviour, I_Interactable
{
    public bool eqipped = false;

    public static Action equipVacuum;
    public static Action unequipVacuum;

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

            equipVacuum?.Invoke();
        }
        else
        {
            transform.SetParent(null, true);  

            Player.Instance.isHoldingSomething = false;

            eqipped = false; 

            unequipVacuum?.Invoke();
        }
        
    }
    
}