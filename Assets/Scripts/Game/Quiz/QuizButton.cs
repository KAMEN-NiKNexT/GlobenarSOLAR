using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kamen.DataSave;
using DG.Tweening;
using TMPro;

namespace ComputerQuiz
{
    [RequireComponent(typeof(Button))]
    public class QuizButton : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        public Button ClickButton;
        [SerializeField] private Image _background;
        [SerializeField] private TextMeshProUGUI _answerText;

        [Header("Settigns")]
        [SerializeField] private float _paintingDuration;
        [SerializeField] private Ease _paintingEase;

        #endregion

        #region Properties

        public float PaintingDuration { get => _paintingDuration; }

        #endregion

        #region Control Methods

        public void SetUpText(string text) => _answerText.text = text;
        
        public void ChoosePainting()
        {
            _background.DOColor(Color.grey, _paintingDuration).SetEase(_paintingEase);
        }
        public void AnswerPainting(bool isRight)
        {
            Color32 newColor = isRight ? Color.green : Color.red;
            _background.DOColor(newColor, _paintingDuration).SetEase(_paintingEase);
        }

        #endregion
    }
}