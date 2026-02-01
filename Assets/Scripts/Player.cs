using System;
using System.Runtime;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class Player : MonoBehaviour
{
    private const string HAS_MOVE_INPUT_ANIMATION_KEY = "HasMoveInput";

    private const string PLAYER_MOVE_MULTIPLIER_ANIMATION_KEY = "PlayerMoveMultiplier";

    [Header("Movement")]
    [SerializeField]
    private float moveSpeed = 2.5f;
    
    [SerializeField] 
    private float sprintSpeed = 6f;


    [Header("Interaction")]
    [SerializeField]
    private float interactDistance = 2.5f;

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
    [SerializeField]
    private SpriteRenderer masked;
    bool isMasked=false;

    private Rigidbody2D rb;

    private Animator animator;

    void Awake()
    {
        masked.enabled=false;
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
        isMasked=!isMasked;
        if (!isMasked)
        {
            playerHead.enabled=true;
            masked.enabled=false;
        }
        else
        {
            playerHead.enabled=false;
            masked.enabled=true;   
        }
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
        float speed = moveSpeed;
        float animationMultiplier = 1f;

        if(GameInput.Instance.IsSprinting)
        {
            speed = sprintSpeed;
            animationMultiplier = 1.5f;
        } 

        animator.SetFloat(PLAYER_MOVE_MULTIPLIER_ANIMATION_KEY, animationMultiplier);
        
        Vector2 movementInput = GameInput.Instance.GetMovementVectorNormalized();

        if(movementInput != Vector2.zero && !GameManager.Instance.IsGamePaused)
        {
            animator.SetBool(HAS_MOVE_INPUT_ANIMATION_KEY, true);

            bool flipPlayer = movementInput.x < 0;
            
            playerBody.flipX = flipPlayer;
            if (playerHead.enabled)
            {
                playerHead.flipX = flipPlayer;   
            }
            else
            {
                masked.flipX = flipPlayer;
            }
        } else
        { 
            animator.SetBool(HAS_MOVE_INPUT_ANIMATION_KEY, false);   
        }

        rb.linearVelocity = movementInput * speed;
    }

    private void HandleInteraction()
    {
        Vector2 interactionDirection = GameInput.Instance.GetMovementVectorNormalized();

        if(interactionDirection != Vector2.zero)
        {
            lastInteractionDirection = interactionDirection;
        }

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, lastInteractionDirection, interactDistance, interactionLayerMask);
    
        if(hitInfo)
        {

            if(hitInfo.transform.GetComponent<I_Interactable>() != null)
            {
                Debug.Log(hitInfo.transform.gameObject + " has interactable");

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
