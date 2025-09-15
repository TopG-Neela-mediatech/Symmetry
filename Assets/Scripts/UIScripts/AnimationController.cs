using System.Collections;
using UnityEngine;

namespace TMKOC.SYMMETRY
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;


        public void PlayCorrectAnimation() => StartCoroutine(PlayCorrectAnimationSequence());
        public void PlayIncorrectAnimation() => StartCoroutine(PlayIncorrectAnimationSequence());


        private IEnumerator PlayCorrectAnimationSequence()
        {
            animator.SetBool("IsIncorrect", false);
            animator.SetBool("IsCorrect", true);
            yield return new WaitForSeconds(2.25f);
            animator.SetBool("IsCorrect", false);
        }
        private IEnumerator PlayIncorrectAnimationSequence()
        {
            animator.SetBool("IsCorrect", false);
            animator.SetBool("IsIncorrect", true);
            yield return new WaitForSeconds(2.75f);
            animator.SetBool("IsIncorrect", false);
            GameManager.Instance.LevelManager.EnableAllLeafCollider();
        }
    }
}
