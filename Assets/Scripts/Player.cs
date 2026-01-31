using System.Runtime.InteropServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2.5f;

    [SerializeField] 
    private Rigidbody2D rb; 


    void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        rb.linearVelocity = GameInput.Instance.GetMovementVectorNormalized() * moveSpeed;
    }

}
