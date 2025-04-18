using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerAttackGrunt : MonoBehaviour
{
    public AudioClip[] gruntClips;
    private AudioSource audioSource;
    public PlayerAttackGrunt gruntPlayer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayGrunt()
    {
        if (gruntClips.Length == 0) return;

        int index = Random.Range(0, gruntClips.Length);
        audioSource.PlayOneShot(gruntClips[index]);
    }

        void Update()
    {
    if (Input.GetKeyDown(KeyCode.KeypadEnter)) 
    {
        gruntPlayer.PlayGrunt();
        
    }
    }

}

    

