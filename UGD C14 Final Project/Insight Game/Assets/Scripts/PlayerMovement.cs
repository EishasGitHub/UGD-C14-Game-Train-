using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float baseSpeed = 6f;
    public Animator animator;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 lookDirection;
    private Rigidbody playerRB;

    public AudioSource walkSound;
    public AudioSource runSound;

    private bool goingUp = false;

    public GameObject Image;
    public GameObject firstScroll;
    public GameObject secondScroll;
    public GameObject thirdScroll;
    public GameObject fourthScroll;
    public GameObject fifthScroll;
    public GameObject starting;
    public GameObject endingImage;
    public GameObject Continue;

    public GameObject scroll2;
    public GameObject scroll3;
    public GameObject scroll4;
    public GameObject scroll5;

    private bool isPaused = false;
    public bool GameStarted = false;

    public bool gameEnded = false;

    public int health = 3;

    public TextMeshProUGUI healthText;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        UpdateHealthUI();
    }

    void Update()
    {
        if (GameStarted)
        {
            movePlayer();
        }
        
        else
        {
            StartPage();
        }
    }

    void movePlayer()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        lookDirection = new Vector3(horizontalInput, 0, verticalInput);
        lookDirection = transform.TransformDirection(lookDirection).normalized;


        if (lookDirection != Vector3.zero)
        {
            if (!goingUp)
            {
                animator.SetBool("walkFlag", true);
                playerRB.linearVelocity = lookDirection * baseSpeed;
                
                if (!walkSound.isPlaying)
                {
                    walkSound.Play();
                }
            }

            else
            {
                animator.SetBool("walkFlag", true);
                playerRB.linearVelocity = lookDirection * 30;

                if (!walkSound.isPlaying)
                {
                    walkSound.Play();
                }
            }
        }

        else
        {
            animator.SetBool("walkFlag", false);
            playerRB.linearVelocity = Vector3.zero;
            walkSound.Stop();
        }


        if (Input.GetKey(KeyCode.LeftControl))
        {
            animator.SetBool("sneakFlag", true);
            baseSpeed = 4f;
            walkSound.volume = 0.5f;
        }

        else if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("runFlag", true);
            baseSpeed = 10f;

            if (!runSound.isPlaying)
            {
                walkSound.Stop();
                runSound.Play();
            }
        }

        else
        {
            animator.SetBool("runFlag", false);
            animator.SetBool("sneakFlag", false);
            baseSpeed = 6;

            if (runSound.isPlaying)
                runSound.Stop();

            walkSound.volume = 1;
        }

        if (isPaused && Input.GetKeyDown(KeyCode.Return))
        {
            Time.timeScale = 1;
            isPaused = false;
            Image.SetActive(false);
            firstScroll.SetActive(false);
            secondScroll.SetActive(false);
            thirdScroll.SetActive(false);
            fourthScroll.SetActive(false);
            fifthScroll.SetActive(false);
            Continue.SetActive(false);
        }

        if (gameEnded && Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ramp"))
        {
            goingUp = true;
        }

        if (collision.gameObject.CompareTag("Scroll1"))
        {
            Destroy(collision.gameObject);
            scrollObtained();
            firstScroll.SetActive(true);
            Continue.SetActive(true);
            scroll2.SetActive(true);
        }

        if (collision.gameObject.CompareTag("Scroll2"))
        {
            Destroy (collision.gameObject);
            scrollObtained();
            secondScroll.SetActive(true);
            Continue.SetActive(true);
            scroll3.SetActive(true);
        }

        if (collision.gameObject.CompareTag("Scroll3"))
        {
            Destroy (collision.gameObject);
            scrollObtained();
            thirdScroll.SetActive(true);
            Continue.SetActive (true);
            scroll4.SetActive(true);
        }

        if (collision.gameObject.CompareTag("Scroll4"))
        {
            Destroy(collision.gameObject);
            scrollObtained();
            fourthScroll.SetActive(true);
            Continue.SetActive(true);
            scroll5.SetActive(true);
        }

        if (collision.gameObject.CompareTag("Scroll5"))
        {
            Destroy(collision.gameObject);
            endingImage.SetActive(true);
            fifthScroll.SetActive(true);
            gameEnded = true;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            health--;
            UpdateHealthUI();

            if (health <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ramp"))
        {
            goingUp = false;
        }
    }

    void StartPage()
    {
        Image.SetActive(true);
        starting.SetActive(true);
            
        if (Input.GetKey(KeyCode.Space))
        {
            Image.SetActive(false);
            starting.SetActive(false);
            GameStarted = true;
        }
    }

    void scrollObtained()
    {
        Time.timeScale = 0;
        isPaused = true;
        Image.SetActive(true);
    }

    void UpdateHealthUI()
    {
        healthText.text = "Health: " + health; // Update UI Text
    }
}
