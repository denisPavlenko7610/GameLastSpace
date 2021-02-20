using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadthZone : MonoBehaviour
{
    private Animator _animator;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _animator = other.GetComponent<Animator>();
            _animator.SetBool("IsDie", true);

            StartCoroutine(Wait());
        }

    }
    
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(1);
    }
}
