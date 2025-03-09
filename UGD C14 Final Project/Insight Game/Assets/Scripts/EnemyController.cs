using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public NavMeshAgent agent;
    public Transform target;
    public float walkHearingRange = 20f;
    public float runHearingRange = 50f;
    private PlayerMovement pm;
    public Animator animator;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            pm = playerObj.GetComponent<PlayerMovement>();
            target = playerObj.transform; 
        }

        else
        {
            Debug.LogError("Player GameObject not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (target != null)
        //{
        //    agent.SetDestination(target.position);
        //}
        if (pm.GameStarted)
        {
            DetectFootsteps();

            if (transform.position.z >= target.position.z - 5f && transform.position.z <= target.position.z + 0.5f)
            {
                animator.SetBool("inRange", true);
            }

            else
            {
                animator.SetBool("inRange", false);
            }
        }

    }

    public void DetectFootsteps()
    {
        if (pm != null)
        {

            float soundVolume = 0f;
            float currentHearingRange = 0f;

            if (pm.walkSound.isPlaying)
            {
                soundVolume = pm.walkSound.volume;
                currentHearingRange = walkHearingRange;
            }

            // Check if running sound is playing & has higher volume
            if (pm.runSound.isPlaying && pm.runSound.volume > soundVolume)
            {
                soundVolume = pm.runSound.volume;
                currentHearingRange = runHearingRange;
            }

            // If volume is 1 or more & player is in respective hearing range
            if (soundVolume >= 1f && Vector3.Distance(transform.position, target.position) <= currentHearingRange)
            {
                animator.SetBool("playerVisible", true);
                agent.SetDestination(target.position);  // Move toward player
            }
            else
            {
                animator.SetBool("playerVisible", false);
                agent.SetDestination(transform.position); // Stop moving
            }
        }
    }
}
