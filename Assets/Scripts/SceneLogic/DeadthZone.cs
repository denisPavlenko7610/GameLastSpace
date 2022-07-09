using UnityEngine;

namespace SceneLogic
{
    public class DeadthZone : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverScreen;
        private Animator _animator;
        private readonly int _IsDie = Animator.StringToHash("IsDie");

        private void OnTriggerEnter2D(Collider2D other)
        {
            var player = "Player";
            if (other.CompareTag(player))
            {
                _animator = other.GetComponent<Animator>();
                _animator.SetBool(_IsDie, true);

                gameOverScreen.SetActive(true);
            }
        }
    }
}