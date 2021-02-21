using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadthZone : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    private Animator _animator;

    private void Start()
    {
        gameOverScreen.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _animator = other.GetComponent<Animator>();
            _animator.SetBool("IsDie", true);

            gameOverScreen.SetActive(true);
        }

    }
    
    
}
