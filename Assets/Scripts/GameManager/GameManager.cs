using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _winObject;
    [SerializeField] private GameObject _loseObject;
    
    [SerializeField] private AudioSource _playerWinAudioSource;
    [SerializeField] private AudioSource _playerLoseAudioSource;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Win()
    {
        _playerWinAudioSource.Play();
        _winObject.SetActive(true);
        
        var currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        Progress.Instance.SetLevel(currentLevelIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Progress.Instance.Level += 1;
        Progress.Instance.Save();
        
    }
    
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Lose()
    {
        _playerLoseAudioSource.Play();
        _loseObject.SetActive(true);
    }
}
