using TMPro.SpriteAssetUtilities;
using UnityEngine;

public class JamReturnPlace : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out JamInteractable jam) && !jam.eqipped)
        {
            jam.transform.position = this.transform.position;
                    Debug.Log("YOO");

            //task complete
            //idk prolly add sum particles
        }
    }
}
