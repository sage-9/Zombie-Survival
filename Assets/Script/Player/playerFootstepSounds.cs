using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerFootstepSound : MonoBehaviour
{
    public AudioClip[] footstepClips;
    public float stepInterval = 0.5f;
    public float minVelocity = 0.1f;

    private AudioSource audioSource;
    private CharacterController controller;
    private float stepTimer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller == null || !controller.isGrounded)
            return;

        bool isMoving = controller.velocity.magnitude > minVelocity;

        if (isMoving)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                PlayFootstep();
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }

    void PlayFootstep()
    {
        if (footstepClips.Length == 0) return;

        int index = Random.Range(0, footstepClips.Length);
        audioSource.PlayOneShot(footstepClips[index]);
    }
}

