using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerControls : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject Button1Object;
    public GameObject Button2Object;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
        Button1Object.SetActive(false);
        Button2Object.SetActive(false);
    }

    void OnMove(InputValue movementValue) 
    {
      Vector2 movementVector = movementValue.Get<Vector2>();

      movementX = movementVector.x;
      movementY = movementVector.y;
    }
   void SetCountText() 
   {
       countText.text =  "Count: " + count.ToString();
       if(count >= 12) 
        {
            winTextObject.SetActive(true);
            Button1Object.SetActive(true);
            Button2Object.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
   }
    void FixedUpdate() 
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement  *  speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
    if (collision.gameObject.CompareTag("Enemy"))
    {
        // Destroy the current object
        Destroy(gameObject); 
        // Update the winText to display "You Lose!"
        winTextObject.gameObject.SetActive(true);
        winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
    }
    }

 
      void OnTriggerEnter(Collider other)  
      {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count+1;

            SetCountText();
        }
        
      }





}
