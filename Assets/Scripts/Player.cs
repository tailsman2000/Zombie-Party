using System;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class Player : MonoBehaviour
{
    private const string HAS_MOVE_INPUT_ANIMATION_KEY = "HasMoveInput";

    [Header("Movement")]
    [SerializeField]
    private float moveSpeed = 2.5f;
    
    [SerializeField] 
    private float sprintSpeed = 6f;


    [Header("Interaction")]
    [SerializeField]
    private LayerMask interactionLayerMask;
    
    [SerializeField]
    private GameObject currentInteractorObject;

    private Vector2 lastInteractionDirection;


    [Header("Player Visual")]
    [SerializeField]
    private SpriteRenderer playerBody; 
    
    [SerializeField]
    private SpriteRenderer playerHead;

    private Rigidbody2D rb;

    private Animator animator;

    void Awake()
    {

        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
    }

    void Start()
    {
        GameInput.Instance.OnInteractPerformed += GameInput_InteractPerformed; 

        GameInput.Instance.OnMaskTogglePerformed += ToggleMask;
    }

    private void ToggleMask(object sender, EventArgs e)
    {
        //Toggle on and off mask
    }

    private void GameInput_InteractPerformed(object sender, EventArgs e)
    {
        Debug.Log("interact " + currentInteractorObject);
        if(currentInteractorObject != null)
        {
            //Interact with object
            currentInteractorObject.GetComponent<I_Interactable>().Interact();
        }
    }

    private void Update()
    {
        HandleMovement();

        HandleInteraction();

    }

    private void HandleMovement()
    {
        float speed = GameInput.Instance.IsSprinting ? sprintSpeed : moveSpeed; 
        
        Vector2 movementInput = GameInput.Instance.GetMovementVectorNormalized();

        if(movementInput != Vector2.zero)
        {
            animator.SetBool(HAS_MOVE_INPUT_ANIMATION_KEY, true);

            bool flipPlayer = movementInput.x < 0;

            playerBody.flipX = flipPlayer;
            playerHead.flipX = flipPlayer;
        } else
        { 
            animator.SetBool(HAS_MOVE_INPUT_ANIMATION_KEY, false);   
        }

        rb.linearVelocity = movementInput * speed;
    }

    private void HandleInteraction()
    {
        float castDistance = 1f;

        Vector2 interactionDirection = GameInput.Instance.GetMovementVectorNormalized();

        if(interactionDirection != Vector2.zero)
        {
            lastInteractionDirection = interactionDirection;
        }

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, lastInteractionDirection, castDistance, interactionLayerMask);
    
        if(hitInfo)
        {

            if(hitInfo.transform.GetComponent<I_Interactable>() != null)
            {
                Debug.Log(hitInfo.transform + " has interactable");

                currentInteractorObject = hitInfo.transform.gameObject;
            } else
            {
                currentInteractorObject = null;
            }
        } else
        {
            currentInteractorObject = null;
        }
    }


}
