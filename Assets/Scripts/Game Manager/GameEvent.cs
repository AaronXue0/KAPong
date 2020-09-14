using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace GameSystem
{
    public class GameEvent : MonoBehaviour
    {
        [SerializeField]
        public GameObject _gDisplay;

        [SerializeField]
        public Text _sDisplay;

        [SerializeField]
        public Image _aDisplay;
        [SerializeField]
        public Sprite[] _aContent;
        [SerializeField]
        float shuffleDelay = 0.1f;
        [SerializeField]
        int maxShuffle = 7;
        int shuffleCounter = 0;
        int _aRandom;

        /// <summary>
        /// GameOver Effect Handling
        /// </summary>
        public Color whiteT
        {
            get
            {
                Color color = Color.white;
                color.a = 0;
                return color;
            }
        }
        public void GameOver(int score)
        {
            _gDisplay.SetActive(true);
            Image[] images = _gDisplay.GetComponentsInChildren<Image>();
            GameObject.Find("/Evaluate Canvas/Score Text").GetComponent<Text>().text = score.ToString();
            foreach (Image image in images)
            {
                image.color = whiteT;
            }
            ShowGameOverDisplay(score, images);
        }
        public void ShowGameOverDisplay(int score, Image[] images)
        {
            foreach (Image image in images)
            {
                image.color = whiteT;
                image.DOFade(0.8f, 1);
            }
        }

        /// <summary>
        /// Goal Effect Handling
        /// </summary>
        public void GoalHandling(ref int score, float speed, int state)
        {
            int bonus = 0;
            if (state == 1 || state == 4) bonus = 2;
            else bonus = 1;
            score += (int)speed;
            score *= bonus;
            StartCoroutine(ScoreCorountine(score));
        }
        IEnumerator ScoreCorountine(int score)
        {
            int n = int.Parse(_sDisplay.text);
            while (n != score)
            {
                n++;
                _sDisplay.text = n.ToString();
                yield return null;
            }
        }

        /// <summary>
        /// Shuffle Effect Handling
        /// </summary>
        public void Shuffle(System.Action<int> callback)
        {
            shuffleCounter = maxShuffle;
            StartCoroutine(ShuffleCoroutine(Random.Range(0, _aContent.Length), callback));
        }
        IEnumerator ShuffleCoroutine(int rand, System.Action<int> callback)
        {
            yield return new WaitForSeconds(shuffleDelay);
            _aDisplay.sprite = _aContent[rand];
            shuffleCounter--;
            if (shuffleCounter > 0) StartCoroutine(ShuffleCoroutine(Random.Range(0, _aContent.Length), callback));
            else callback(rand);
        }
    }
}