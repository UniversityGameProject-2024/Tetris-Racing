using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
/**
*This component moves its object when the player clicks the arrow keys
*/

public class PlayerControl : MonoBehaviour
{
    [Tooltip("Speed of movement, in meters per second")]
    [SerializeField] float speed = 10f;
    private bool disableMoveRight = false;
    private bool disableMoveLeft = false;

    [SerializeField] 
    public int lives;
    

    [SerializeField] 
    public TextMeshProUGUI textLives;


    [SerializeField] 
    InputAction move = new InputAction(type: InputActionType.Value, expectedControlType: nameof(Vector2));




    void OnEnable()
    {
        move.Enable();
    }

    void OnDisable()
    {
        move.Disable();
    }

    void Start()
    {
        textLives.text = $"Lives: {lives}";
    }


    void Update()
    {
        Vector2 moveDirection = move.ReadValue<Vector2>();
        Vector3 movementVector = new Vector3(moveDirection.x, moveDirection.y, 0) * speed * Time.deltaTime;

        // If user tries moving to the right and disableMovingRight is true then 
        // we update the movementVector.x to zero. 
        if (disableMoveRight && moveDirection.x > 0)
        {
            // Disable moving to the right
            movementVector = new Vector3(0, moveDirection.y, 0) * speed * Time.deltaTime;
        }

        if (disableMoveLeft && moveDirection.x < 0)
        {
            // Disable moving to the left
            movementVector = new Vector3(0, moveDirection.y, 0) * speed * Time.deltaTime;
        }


        transform.position += movementVector;
        //transform.Translate(movementVector);
        // NOTE: "Translate(movementVector)" uses relative coordinates - 
        //       it moves the object in the coordinate system of the object itself.
        // In contrast, "transform.position += movementVector" would use absolute coordinates -
        //       it moves the object in the coordinate system of the world.
        // It makes a difference only if the object is rotated.
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Palyer collided with any other collider");
        if (other.gameObject.name == "Road Border Right")
        {
            Debug.Log("Palyer collided with Road Border Right");
            disableMoveRight = true;
        }
        
        if (other.gameObject.name == "Road Border Left")
        {
            Debug.Log("Palyer collided with Road Border Left");
            disableMoveLeft = true;
        }

        if (other.gameObject.name == "Enemy(Clone)")
        {
            Debug.Log("Palyer collided with Enemy");
            lives--;
            textLives.text = $"Lives: {lives}";
            gameObject.SetActive(false);
            if (lives > 0)
            {
                Invoke("ActivatePlayer", 4f);
            }
            else
            {
                textLives.text = $"Lives: 0  -  Game Over!";
            }
        }

    }


    void ActivatePlayer()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Road Border Right")
        {
            disableMoveRight = false;
        }

        if (other.gameObject.name == "Road Border Left")
        {
            disableMoveLeft = false;
        }

    }
}
