using UnityEngine;

public class DeadthZone : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    private Animator _animator;

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