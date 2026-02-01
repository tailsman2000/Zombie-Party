using UnityEngine;

public class EnemyLosScript : MonoBehaviour
{
    [SerializeField] private EnemyBehaviorScript enemyBehaviorScript;
    public bool playerInSight { get; private set; } = false;
    private Coroutine unsussyRoutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(Physics2D.Linecast(transform.position, collision.transform.position, LayerMask.GetMask("Obstacles")).collider == null)
            {
                if(unsussyRoutine != null)
                {
                    StopCoroutine(unsussyRoutine);
                    unsussyRoutine = null;
                }

                playerInSight = true;
                StartCoroutine(enemyBehaviorScript.Sussy());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            unsussyRoutine = StartCoroutine(enemyBehaviorScript.Unsussy());
            playerInSight = false;
        }
    }
}
