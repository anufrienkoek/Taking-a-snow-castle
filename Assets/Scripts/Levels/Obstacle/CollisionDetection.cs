using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CollisionDetection : MonoBehaviour
{
    [SerializeField] private ParticleSystem CharacterParticle;
    [SerializeField] private AudioSource _followerAudioSource;

    public void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Following"))
        {
            _followerAudioSource.Play();
            var particle = Instantiate(CharacterParticle , other.transform.position, other.transform.rotation);
            Destroy(particle,0.1f);
            Destroy(other.gameObject,0.1f);
        }
        
        if(other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.Lose();
        }
    }
}