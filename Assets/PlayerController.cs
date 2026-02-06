using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.SceneManagement; //importing SceneManagement Library
using static PlayerController;

public class PlayerController : MonoBehaviour
{
    public Playstates State;

    public enum Playstates
    {
        Move,
        Win,

    }



    //Movement Variables
    Rigidbody2D rb;
    SpriteRenderer Shopping;
    public float jumpForce;
    public float speed;
    public float dash;
    public float score;


    //Animation variables
    Animator anim;
    public bool moving;
    public bool jump;

    //Ground check
    public bool isGrounded;

    public List<Sprite> sprites;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        rb = GetComponent<Rigidbody2D>();
        score = 0;
        Shopping = GetComponent<SpriteRenderer>();


    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = transform.position;

        //variables to mirror the player
        Vector3 newScale = transform.localScale;
        float currentScale = Mathf.Abs(transform.localScale.x); //take absolute value of the current x scale, this is always positive


        rb.linearVelocity = new Vector2(rb.linearVelocity.x * 5f, rb.linearVelocity.y);

        if (State == Playstates.Move)
        {

       

        

        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition.x -= speed;
            newScale.x = -currentScale;
            //is moving
            moving = true;
            Debug.Log("move");
        }

        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition.x += speed;
            newScale.x = +currentScale;
            //is moving
            moving = true;
            Debug.Log("move");
        }

        if (Input.GetKey("a") && Input.GetKey(KeyCode.LeftShift))
        {
            newPosition.x -= speed + dash;
            newScale.x = -currentScale;
            //is dashing
            moving = true;
            Debug.Log("move");

        }

        if (Input.GetKey("d") && Input.GetKey(KeyCode.LeftShift))
        {
            newPosition.x += speed + dash;
            newScale.x = +currentScale;
            //is dashing
            moving = true;
            Debug.Log("move");
        }


        if (Input.GetKey("space") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jump = true;
        }

        if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
        {
            moving = false;
        }

        transform.position = newPosition;
        transform.localScale = newScale;

         }

        if (score == 9)
        {

            SceneManager.LoadScene(1);
            State = Playstates.Win;
        }

        if (State == Playstates.Win)
        {
            if (Input.GetKeyUp("space"))
            {

                SceneManager.LoadScene(0);
                State = Playstates.Move;
            }
        }

    }

   


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            Debug.Log("i hit the ground");

            isGrounded = true;
            jump = false;
        }

        if (collision.gameObject.tag.Equals("death"))
        {
            Debug.Log("death");

            SceneManager.LoadScene(0);
        }

        if (collision.gameObject.tag.Equals("door"))
        {
            Debug.Log("change scene");

            SceneManager.LoadScene(2);
        }

        if (collision.gameObject.tag.Equals("fruit"))
        {

            score++;


        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            isGrounded = false;
        }

    }


}

