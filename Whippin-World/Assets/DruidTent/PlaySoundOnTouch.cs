using UnityEngine;

public class PlaySoundOnTouch : MonoBehaviour
{
    // R�f�rence � l'AudioSource ajout� pr�c�demment
    private AudioSource audioSource;

    void Start()
    {
        // R�cup�rer le composant AudioSource du GameObject
        audioSource = GetComponent<AudioSource>();
    }

    // Cette fonction est appel�e chaque fois que ce GameObject entre en collision avec un autre GameObject
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSource.Play();
        }

    }
}

