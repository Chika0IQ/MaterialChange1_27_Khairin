using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody playerRb;
    Renderer PlayerRenderer;
    public Material[] PlayerMaterial;


    int jumps = 0; //Count jumps for DoubleJump
    float gravityModifier = 3.0f; // Gravity
    float jumpforce = 10.0f; //How high player will jump
    float speed = 7.0f; // Movement Speed of player
    float boundarylimit = 20.0f; // Limit Border 


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        PlayerRenderer = GetComponent<Renderer>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {

        // WASD Movement Only (No Border)
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * Time.deltaTime * vertical * speed);
        transform.Translate(Vector3.right * Time.deltaTime * horizontal * speed);


        //Front And Back Border
        if (transform.position.z < -boundarylimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -boundarylimit);
            PlayerRenderer.material.color = PlayerMaterial[2].color;
        }
        else if (transform.position.z > boundarylimit)
        {   
            transform.position = new Vector3(transform.position.x, transform.position.y, boundarylimit);
            PlayerRenderer.material.color = PlayerMaterial[3].color;
        }

        // Left And Right Border
        if (transform.position.x < -boundarylimit)
        {
            transform.position = new Vector3(-boundarylimit, transform.position.y, transform.position.z);
            PlayerRenderer.material.color = PlayerMaterial[4].color;
        }
        else if (transform.position.x > boundarylimit)
        {
            transform.position = new Vector3(boundarylimit, transform.position.y, transform.position.z);
            PlayerRenderer.material.color = PlayerMaterial[5].color;

        }

        JumpPlayer();
    }
    private void OnCollisionEnter(Collision collision)  // If On the Ground
    {
        if (collision.gameObject.CompareTag("GamePlane"))
        {
            jumps = 0;
            PlayerRenderer.material.color = PlayerMaterial[0].color; // Change Color
        }
    }

    void JumpPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumps < 2)
        {
            
            playerRb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse); //Jump Height
            jumps++;
            PlayerRenderer.material.color = PlayerMaterial[1].color;
        }
    }
}
