using Kamen.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UI.ProceduralImage;

namespace ComputerQuiz
{
    public class QuizResultPopup : Kamen.UI.Popup
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _quastionAmountText;
        [SerializeField] private ProceduralImage _percentIcon;
        [SerializeField] private TextMeshProUGUI _percentText;

        [Header("Settings")]
        [SerializeField] private float _percentCountDuration;

        [Header("Variables")]
        private QuizResultData _result;
        private bool _isCanBeClosed;

        #endregion

        #region Control Methods

        public void SetUpPopup(QuizResultData result)
        {
            _result = result;

            _nameText.text = result.QuizName;
            int quastionAmount = RatingManager.Instance.GetQuiz(_result.QuizIndex).Pages.Length;

            string quastionText = TextFormater.Instance.GetWordForm(quastionAmount, "вопрос", "вопроса", "вопросов");
            _quastionAmountText.text = $"{quastionAmount} {quastionText}";

            StartCoroutine(PercentCounter());
        }
        private IEnumerator PercentCounter()
        {
            float startValue = 0;
            float endValue = _result.Percent;
            _percentIcon.fillAmount = 0;
            _isCanBeClosed = false;

            yield return new WaitForSeconds(_showAnimation.Duration);
            for (float t = 0; t < _percentCountDuration; t += Time.deltaTime)
            {
                float percent = Mathf.Lerp(startValue, endValue, t / _percentCountDuration);
                _percentIcon.fillAmount = percent;
                _percentText.text = $"{(int)Math.Round(percent * 100f)} %";
                yield return null;
            }
            _percentIcon.fillAmount = endValue;
            _percentText.text = $"{(int)Math.Round(endValue * 100f)} %";
            _isCanBeClosed = true;
        }
        public void ClosePopup()
        {
            if (!_isCanBeClosed) return;
            QuizzesManager.Instance.WaitToOpenMenu();
            PopupManager.Instance.Hide("QuizResult");
        }

        #endregion
    }
}