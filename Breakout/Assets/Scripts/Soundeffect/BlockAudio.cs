using UnityEngine;

public class BlockAudio : MonoBehaviour
{
    [SerializeField] private AudioClip breakSound;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void PlayBreakSound()
    {
        if (breakSound == null)
        {
            return;
        }

        GameObject temp = new GameObject("BlockBreakSound");
        temp.transform.position = transform.position;
        AudioSource tempSource = temp.AddComponent<AudioSource>();
        tempSource.clip = breakSound;
        tempSource.volume = .3f;
        tempSource.Play();
        Destroy(temp, breakSound.length);
    }
}