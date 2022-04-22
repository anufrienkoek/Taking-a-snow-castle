using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public bool mouseDown;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
}
