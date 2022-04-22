using UnityEngine;

public class Audio : MonoBehaviour
{
    [SerializeField] private AudioSource _mainSound;

    private void Start()
    {
        _mainSound = GetComponent<AudioSource>();
        _mainSound.Play();
    }
}
