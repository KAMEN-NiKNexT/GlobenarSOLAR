using DG.Tweening;
using Kamen.UI;
using Kamen;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using TMPro;

namespace ComputerQuiz
{
    [RequireComponent(typeof(CanvasGroup))]
    public class QuizPage : MonoBehaviour
    {
        #region Variables

        [Header("Prefabs")]
        [SerializeField] private QuizButton _answerButtonPrefab;

        [Header("Objects")]
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Image _quastionImage;
        [SerializeField] private TextMeshProUGUI _quastionText;
        [Space]
        [SerializeField] private Transform _answerButtonHolder;

        [Header("Settings")]
        [SerializeField] private float _delayBeforeShowRightAnswer;
        [SerializeField] private float _delayBeforeNextPage; 

        [Header("Variables")]
        private List<QuizButton> _answerButtons = new List<QuizButton>();
        private QuizPageInfo _info;
        private bool _isAnswerChoosen;

        #endregion

        #region Control Methods

        public void SetUp(QuizPageInfo info)
        {
            _info = info;
            _answerButtons.Clear();

            _quastionImage.sprite = _info.HelpImage;
            _quastionImage.SetNativeSize();
            _quastionImage.gameObject.SetActive(false);

            _quastionText.text = _info.Quastion;

            for (int i = 0; i < _info.Answers.Length; i++)
            {
                QuizButton answerButton = Instantiate(_answerButtonPrefab, _answerButtonHolder);
                answerButton.SetUpText(_info.Answers[i]);
                int number = i;
                answerButton.ClickButton.onClick.AddListener(() => ClickOnButton(number));

                _answerButtons.Add(answerButton);
            }
        }
        public IEnumerator ImageAppear(float duration)
        {
            _quastionImage.color = new Color(_quastionImage.color.r, _quastionImage.color.g, _quastionImage.color.b, 0);
            yield return new WaitForSeconds(duration);
            _quastionImage.gameObject.SetActive(true);
            Color newFade = new Color(_quastionImage.color.r, _quastionImage.color.g, _quastionImage.color.b, 1);
            _quastionImage.DOColor(newFade, duration);
        }

        public void HideCanvasGroup() => _canvasGroup.alpha = 0f;
        public void VerticalSwipe(bool isShow, float duration, Ease curve, MyCurve myCurve)
        {
            float screenEdge = CanvasSettings.Instance.GetCurrentScreenSize().y;

            Vector3 startPosition = isShow ? new Vector3(0f, -screenEdge, 0f) : Vector3.zero;
            Vector3 endPosition = isShow ? Vector3.zero : new Vector3(0f, screenEdge, 0f);

            transform.localPosition = startPosition;
            if (myCurve == null) transform.DOLocalMove(endPosition, duration).SetEase(curve);
            else transform.DOLocalMove(endPosition, duration).SetEase(myCurve.Value);
        }
        private void ClickOnButton(int index)
        {
            if (_isAnswerChoosen) return;
            _isAnswerChoosen = true;

            StartCoroutine(PaintingCoroutine(index));
        }
        private IEnumerator PaintingCoroutine(int index)
        {
            _answerButtons[index].ChoosePainting();

            yield return new WaitForSeconds(_answerButtons[0].PaintingDuration + _delayBeforeShowRightAnswer);

            if (!_info.NumbersRightAnswers.Contains(index)) _answerButtons[index].AnswerPainting(false);
            else QuizzesManager.Instance.IncreaseAmountRightAnswers();

            for (int i = 0; i < _info.NumbersRightAnswers.Length; i++)
            {
                _answerButtons[_info.NumbersRightAnswers[i]].AnswerPainting(true);
            }
            yield return new WaitForSeconds(_answerButtons[0].PaintingDuration + _delayBeforeNextPage);
            QuizzesManager.Instance.TransitionTo();
        }

        #endregion
    }
}