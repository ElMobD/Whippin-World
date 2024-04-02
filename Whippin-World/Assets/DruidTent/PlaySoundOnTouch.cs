using UnityEngine;

public class PlaySoundOnTouch : MonoBehaviour
{
    // Référence à l'AudioSource ajouté précédemment
    private AudioSource audioSource;

    void Start()
    {
        // Récupérer le composant AudioSource du GameObject
        audioSource = GetComponent<AudioSource>();
    }

    // Cette fonction est appelée chaque fois que ce GameObject entre en collision avec un autre GameObject
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSource.Play();
        }

    }
}

