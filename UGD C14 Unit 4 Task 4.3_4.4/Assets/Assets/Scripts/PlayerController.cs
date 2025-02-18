using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private float speed = 5.0f;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 lookDirection;
    public Animator animator;
    private Rigidbody playerRB;
    private bool isOnPlatform = true;

    private bool isAttackThrown = false;
    private GameObject projectileInstance;
    public GameObject projectilePrefab;
    private Rigidbody projectileRB;
    private GameObject enemyInstance;
    private Vector3 enemyPos;
    public GameObject explosionEffectPrefab;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
        OutofBounds();
        checkPunch();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(SpawnProjectile());
        }
    }

    void checkPunch()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("isPunching", true);
            StartCoroutine(ResetPunch());
        }
    }

    IEnumerator ResetPunch()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.SetBool("isPunching", false);
    }


    void movePlayer()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        lookDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (lookDirection != Vector3.zero)
        {
            animator.SetBool("isWalking", true);

            transform.Translate(lookDirection * speed * Time.deltaTime, Space.World);


            transform.rotation = Quaternion.LookRotation(lookDirection);
        }

        else
        {
            animator.SetBool("isWalking", false);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("isRunning", true);
            speed = 15.0f;
        }
        else
        {
            animator.SetBool("isRunning", false);
            speed = 5.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isOnPlatform)
        {
            isOnPlatform = false;
            animator.SetBool("isJumping", true);
            playerRB.AddForce(Vector3.up * 60.0f, ForceMode.Impulse);
        }
    }

    void OutofBounds()
    {
        if (transform.position.y < -3)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isOnPlatform = true;
            animator.SetBool("isJumping", false);
        }
    }

    IEnumerator SpawnProjectile()
    {
        if (isAttackThrown)
        {
            yield return new WaitForSeconds(0.5f);
            isAttackThrown = false;
        }

        if (!isAttackThrown)
        {
            projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            projectileRB = projectileInstance.GetComponent<Rigidbody>();

            enemyInstance = GameObject.FindWithTag("Enemy");

            enemyPos = enemyInstance.transform.position;

            print("enemy Position:" + enemyPos);

            Vector3 direction = transform.forward.normalized;

            print("Projectile Position:" + direction);

            projectileRB.AddForce(direction * 8.0f, ForceMode.Impulse);
            projectileRB.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);

            isAttackThrown = true;

            yield return new WaitForSeconds(2);


            if (projectileInstance!=null)
            {
                Instantiate(explosionEffectPrefab, projectileInstance.transform.position, Quaternion.identity);

                Destroy(projectileInstance);
            }

        }
    }
}