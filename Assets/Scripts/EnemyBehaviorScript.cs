using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviorScript : MonoBehaviour
{
    [SerializeField] private GameObject angleFodder;
    [SerializeField] private EnemyLosScript enemyLosScript;
    [SerializeField] private Animator animator;

    [Header("Movement")]
    private Coroutine moveRoutine;
    [SerializeField] private float minWaitTime = 2f;
    [SerializeField] private float maxWaitTime = 5f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float aggroSpeed = 2f;

    [Header("Player detection")]
    private float sus;
    private bool aggroed = false;
    private GameObject player => Player.Instance.gameObject;
    [SerializeField] private float susChargeSpeed;
    [SerializeField] private float unsussyDelay;
    [SerializeField] private GameObject questionMarkObject;
    [SerializeField] private Image questionMarkIcon;
    [SerializeField] private GameObject exclamationMarkObject;
    [SerializeField] private GameObject losCone;
    [SerializeField] private GameObject losCircle;
    [SerializeField] private LayerMask obstacleLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveRoutine = StartCoroutine(RandomMovement(minWaitTime, maxWaitTime));
        //Canvas canvas = angleFodder.GetComponent<Canvas>();
        //canvas.worldCamera = Camera.main;
        //canvas.planeDistance = 1;
    }

    private IEnumerator RandomMovement(float min, float max)
    {
        while(true) {
            int angle;
            int attempt = 0;
            do {
                attempt++;
                angle = Random.Range(0, 360);
            } while(Physics2D.Linecast(transform.position, player.transform.position, obstacleLayer).collider != null && attempt < 5);
            
            yield return StartCoroutine(LookTowards(0.15f, angle));
            yield return StartCoroutine(MoveForDuration(Random.Range(0.5f, 3)));

            yield return new WaitForSeconds(Random.Range(min, max));
        }
    }

    private IEnumerator MoveForDuration(float duration)
    {
        animator.SetBool("walking", true);
        float elapsed = 0f;
        while (elapsed < duration)
        {
            transform.position += angleFodder.transform.right * moveSpeed * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
        animator.SetBool("walking", false);
    }

    private IEnumerator LookTowards(float duration, float target)
    {
        float startDirection = angleFodder.transform.rotation.eulerAngles.z;
        float elapsed = 0f;
        float t;

        if(target - startDirection > 180f)
        {
            startDirection += 360f;
        }
        else if(target - startDirection < -180f)
        {
            target += 360f;
        }

        while (elapsed < duration)
        {
            t = elapsed / duration;
            float angle = t * t * (3f - 2f * t);
            angleFodder.transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(startDirection, target, angle));
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator Sussy()
    {
        if(!aggroed) {
            if(moveRoutine != null) {
                StopCoroutine(moveRoutine);
                moveRoutine = null;
            }
            StartCoroutine(LookTowards(0.1f, Mathf.Atan2((player.transform.position - transform.position).y, (player.transform.position - transform.position).x) * Mathf.Rad2Deg));
            questionMarkObject.SetActive(true);
            while(enemyLosScript.playerInSight) {
                //Things get slower sussed out when player is masked
                bool isPlayerMasked = Player.Instance.isMasked; 

                sus += (isPlayerMasked ? susChargeSpeed / 5f : susChargeSpeed) * Time.deltaTime;

                if(isPlayerMasked)
                {
                    //idk if i used your code right there, feel free to optimize ig lollllll
                    StartCoroutine(LookTowards(0.1f, Mathf.Atan2((player.transform.position - transform.position).y, (player.transform.position - transform.position).x) * Mathf.Rad2Deg));
                }

                questionMarkIcon.fillAmount = sus / 100f;

                if(sus >= 100)
                {
                    Aggro();
                    break;
                }
                yield return null;
            }
        }
    }

    public IEnumerator Unsussy()
    {
        yield return new WaitForSeconds(unsussyDelay);
        ResetSusWithMovement();
    }

    private void ResetSus()
    {
        sus = 0;
        exclamationMarkObject.SetActive(false);
        questionMarkIcon.fillAmount = 0f;
        questionMarkObject.SetActive(false);
    }

    private void ResetSusWithMovement()
    {
        sus = 0;
        exclamationMarkObject.SetActive(false);
        questionMarkIcon.fillAmount = 0f;
        questionMarkObject.SetActive(false);

        moveRoutine = StartCoroutine(RandomMovement(minWaitTime, maxWaitTime));
    }

    public void Aggro()
    {
        ResetSus();
        exclamationMarkObject.SetActive(true);
        StartCoroutine(AggroMovement());
    }

    private IEnumerator AggroMovement()
    {
        aggroed = true;
        animator.SetBool("aggro", true);
        animator.SetBool("walking", false);
        losCircle.SetActive(true);
        if(moveRoutine != null) {
            StopCoroutine(moveRoutine);
            moveRoutine = null;
        }
        yield return null;
        while(Physics2D.Linecast(transform.position, player.transform.position, obstacleLayer).collider == null)
        {
            Vector3 dir = (player.transform.position - transform.position).normalized;
            transform.position += dir * aggroSpeed * Time.deltaTime;
            StartCoroutine(LookTowards(1/60f, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg));
            yield return null;
        }
        ResetSus();
        losCircle.SetActive(false);
        yield return null;
        moveRoutine = StartCoroutine(RandomMovement(minWaitTime, maxWaitTime));
        animator.SetBool("aggro", false);
        aggroed = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Shriek" && !aggroed)
        {
            Aggro();
        }
    }
    public bool IsAggroed()
    {
        return aggroed;
    }
}