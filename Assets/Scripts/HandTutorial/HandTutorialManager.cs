using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace TMKOC.SYMMETRY
{
    public class HandTutorialManager : MonoBehaviour
    {
        [SerializeField] private GameObject tutorialHand;
        [SerializeField] private Transform dropT;
        private LeafHandler correctLeaf;
        private Vector3 ogPosi;


        private void Start()
        {
            ogPosi = tutorialHand.transform.position;
        }
        public void SetCorrectLeaf(LeafHandler l)
        {
            correctLeaf = l;
        }
        public void PlayTutorialOnLevelOne()=>StartCoroutine(PlayTutorialAfterDelay());
        private IEnumerator PlayTutorialAfterDelay()
        {
            yield return new WaitForSeconds(2f);
            tutorialHand.SetActive(true);
            tutorialHand.transform.DOMove(correctLeaf.gameObject.transform.position, 1f).OnComplete(() =>
            {
                tutorialHand.transform.DOPunchScale(new Vector3(0.3f, 0.3f, 0.3f), 0.5f, 1).OnComplete(() =>
                {
                    tutorialHand.transform.DOMove(dropT.position, 1f).OnComplete(() =>
                    {
                        tutorialHand.transform.DOPunchScale(new Vector3(0.3f, 0.3f, 0.3f), 0.5f, 1).OnComplete(() =>
                        {
                            tutorialHand.SetActive(false);
                            tutorialHand.transform.localPosition = ogPosi;
                        }
                        );
                    }
                    );
                }
                );
            }
            );
        }
    }
}
