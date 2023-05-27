using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ComputerQuiz
{
    [RequireComponent(typeof(Button))]
    public class QuizProfilePanel : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private Text _nameText;
        [SerializeField] private Text _quastionAmountText;
        [SerializeField] private Image _percentIcon;
        [SerializeField] private Text _percentText;

        [Header("Variables")]
        private Button _button;
        private QuizResultData _result;

        #endregion

        #region Control Methods

        public void Initialize(QuizResultData result)
        {
            _result = result;
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => Click());

            _nameText.text = result.QuizName;
            int quastionAmount = RatingManager.Instance.GetQuiz(_result.QuizIndex).Pages.Length;

            string quastionText = TextFormater.Instance.GetWordForm(quastionAmount, "вопрос", "вопроса", "вопросов");
            _quastionAmountText.text = $"{quastionAmount} {quastionText}";

            _percentIcon.fillAmount = _result.Percent;
            _percentText.text = $"{(int)Math.Round(_result.Percent * 100f)} %";
        }
        private void Click()
        {
            QuizzesManager.Instance.StartOldQuiz(RatingManager.Instance.GetQuiz(_result.QuizIndex), _result.QuizIndex);
        }

        #endregion
    }
}