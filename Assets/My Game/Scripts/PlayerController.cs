using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    [SerializeField] private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Variável para guardar o objeto com o qual podemos interagir
    private Interactable currentInteractable;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Input de movimento (WASD ou Setas)
        movement.x = Input.GetAxisRaw("Horizontal");
        if (movement.x > 0)
        {
            animator.SetBool("isWalkingSides", true);
            spriteRenderer.flipX = true;
        }
        else if (movement.x < 0)
        {
            animator.SetBool("isWalkingSides", true);
            spriteRenderer.flipX = false;


        }
        else
        {
            animator.SetBool("isWalkingSides", false);
            spriteRenderer.flipX = false;
        }
            movement.y = Input.GetAxisRaw("Vertical");
        if (movement.y < 0 )
        {
            animator.SetBool("isWalkingDown", true);
            animator.SetBool("isWalkingUp", false);
        }
        else if (movement.y > 0 )
        {
            animator.SetBool("isWalkingUp", true);
            animator.SetBool("isWalkingDown", false);
        }
        else
        {
            animator.SetBool("isWalkingDown", false);
            animator.SetBool("isWalkingUp", false);
        }
        // Input de Interação
        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    void FixedUpdate()
    {
        // Aplica o movimento físico
        rb.velocity = movement.normalized * moveSpeed;
    }

    // Detecta quando chega perto de algo interativo
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            currentInteractable = collision.GetComponent<Interactable>();
        }
    }

    // Detecta quando se afasta
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            if (currentInteractable == collision.GetComponent<Interactable>())
            {
                currentInteractable = null;
            }
        }
    }
}