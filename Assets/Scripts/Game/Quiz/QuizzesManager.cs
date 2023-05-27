using DG.Tweening;
using Kamen;
using Kamen.DataSave;
using Kamen.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using TMPro;

namespace ComputerQuiz
{
    public class QuizzesManager : SingletonComponent<QuizzesManager>
    {
        #region Classes

        [Serializable]
        private class TransitionInfo
        {
            #region TransitionInfo Variables

            [SerializeField] private float _duration;
            [SerializeField] private Ease _curve;
            [SerializeField] private MyCurve _myCurve;

            #endregion

            #region TransitionInfo Properties

            public float Duration { get => _duration; }
            public Ease Curve { get => _curve; }
            public MyCurve GetMyCurve { get => _myCurve; }

            #endregion
        }

        #endregion

        #region Variables

        [Header("Prefabs")]
        [SerializeField] private QuizPage _quizPagePrefab;

        [Header("Objects")]
        [SerializeField] private TextMeshProUGUI _progressPagesText;
        [SerializeField] private Slider _progressBar;

        [Header("Settigns")]
        [SerializeField] private TransitionInfo _transitionInfo;
        [Space]
        [SerializeField] private Quiz[] _quizzes;
        [SerializeField] private Quiz _testQuiz;
        [SerializeField] private Transform _quizzesScreen;

        [Header("Variables")]
        private Quiz _currentQuiz;
        private Vector2Int _currentIndex;
        private readonly List<QuizPage> _pages = new List<QuizPage>();
        private int _currentPageNumber;
        private int _amountRightAnswers;
        private bool _isOld;

        [Header("Scripts")]
        [SerializeField] private QuizResultPopup _resultPopup;

        #endregion

        #region Control Methods

        public void StartQuiz()
        {
            _isOld = false;
            _currentIndex = RatingManager.Instance.GetQuizId();
            if (_currentIndex == new Vector2Int(-1, -1))
            {
                PopupManager.Instance.Show("Quizzes Over");
            }
            else
            {
                CreateQuiz(RatingManager.Instance.GetQuiz(_currentIndex));
                UseRandomHumanIcon();
                SetUpProgressBar();
                ScreenManager.Instance.TransitionTo("Quiz");
            }
        }
        public void StartOldQuiz(Quiz oldQuiz, Vector2Int index)
        {
            _isOld = true;
            _currentIndex = index;
            CreateQuiz(oldQuiz);
            UseRandomHumanIcon();
            SetUpProgressBar();
            ScreenManager.Instance.TransitionTo("Quiz");
        }

        private void CreateQuiz(Quiz quiz)
        {
            _currentPageNumber = -1;
            _currentQuiz = quiz;
            _amountRightAnswers = 0;
            DestoyPages();

            for (int i = 0; i < _currentQuiz.Pages.Length; i++)
            {
                QuizPage page = Instantiate(_quizPagePrefab, _quizzesScreen.transform);
                page.SetUp(_currentQuiz.Pages[i]);
                page.gameObject.SetActive(false);
                _pages.Add(page);
            }

            TransitionTo(true);
        }
        public void WaitToOpenMenu() => StartCoroutine(WaitToOpenMenuCoroutine());
        public IEnumerator WaitToOpenMenuCoroutine()
        {
            yield return new WaitForSeconds(0.5f);
            ScreenManager.Instance.TransitionTo("Space");
        }

        public void TransitionTo(bool isFast = false)
        {
            QuizPage oldPage = _currentPageNumber >= 0 && _currentPageNumber < _pages.Count - 1 ? _pages[_currentPageNumber] : null;
            _currentPageNumber++;
            QuizPage newPage = _currentPageNumber < _pages.Count ? _pages[_currentPageNumber] : null;

            if (oldPage != null) 
            {
                oldPage.VerticalSwipe(false, !isFast ? _transitionInfo.Duration : 0, _transitionInfo.Curve, _transitionInfo.GetMyCurve);
                StartCoroutine(WaitToTransitionEnd(oldPage));
            }

            if (newPage != null)
            {
                newPage.gameObject.SetActive(true);
                newPage.VerticalSwipe(true, !isFast ? _transitionInfo.Duration : 0, _transitionInfo.Curve, _transitionInfo.GetMyCurve);
                StartCoroutine(newPage.ImageAppear(_transitionInfo.Duration));
            }
            else 
            {
                QuizResultData resultData = null;
                if (!_isOld)
                {
                    string name = $" виз о компьютерах є{DataSaveManager.Instance.MyData.QuizResults.Count + 1}";
                    resultData = new QuizResultData(name, _currentIndex, _amountRightAnswers / (float)_currentQuiz.Pages.Length, RatingManager.Instance.CalculateRatingForQuiz(_amountRightAnswers, _currentQuiz.Pages.Length));
                    DataSaveManager.Instance.MyData.QuizResults.Add(resultData);
                    DataSaveManager.Instance.SaveData();
                }
                else
                {
                    resultData = RatingManager.Instance.GetQuizResultByIndex(_currentIndex, out int number);
                    resultData = new QuizResultData(resultData.QuizName, _currentIndex, _amountRightAnswers / (float)_currentQuiz.Pages.Length, RatingManager.Instance.CalculateRatingForQuiz(_amountRightAnswers, _currentQuiz.Pages.Length));

                    DataSaveManager.Instance.MyData.QuizResults[number] = resultData;
                    DataSaveManager.Instance.SaveData();
                }


                PopupManager.Instance.Show("QuizResult");
                _resultPopup.SetUpPopup(resultData);
            }

            UpdateProgressBar();
        }
        private IEnumerator WaitToTransitionEnd(QuizPage oldScreen)
        {
            yield return new WaitForSeconds(_transitionInfo.Duration);
            oldScreen.HideCanvasGroup();
            oldScreen.gameObject.SetActive(false);
        }
        private void UseRandomHumanIcon()
        {

        }
        private void SetUpProgressBar()
        {
            _progressBar.value = 1;
            _progressBar.maxValue = _pages.Count;
            _progressPagesText.text = $"{1} из {_pages.Count}";
        }
        private void UpdateProgressBar()
        {
            int value = _currentPageNumber + 1;
            if (value > _pages.Count) value = _pages.Count;
            _progressBar.value = value;
            _progressPagesText.text = $"{value} из {_pages.Count}";
        }
        public void IncreaseAmountRightAnswers() => _amountRightAnswers++;
        private void DestoyPages()
        {
            for (int i = 0; i < _pages.Count; i++)
            {
                Destroy(_pages[i].gameObject);
            }
            _pages.Clear();
        }
        private void GetQuizForUser()
        {

        }

        #endregion
    }
}