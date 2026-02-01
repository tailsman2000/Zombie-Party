using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleZombieMovement : MonoBehaviour
{ 
    [SerializeField] private GameObject angleFodder;
    [SerializeField] private Animator animator;

    [Header("Movement")]
    private Coroutine moveRoutine;
    [SerializeField] private float minWaitTime = 2f;
    [SerializeField] private float maxWaitTime = 5f;
    [SerializeField] private float moveSpeed = 2f;

    [SerializeField] private LayerMask obstacleLayer;
   
   void Start()
    {
        moveRoutine = StartCoroutine(RandomMovement(minWaitTime, maxWaitTime));
        
    }
    private IEnumerator RandomMovement(float min, float max)
    {
        while(true) {
            int angle;
            int attempt = 0;
            do {
                attempt++;
                angle = Random.Range(0, 360);
            } while(Physics2D.Linecast(transform.position, transform.position + Vector3.forward, obstacleLayer).collider != null && attempt < 5);
            
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



}
