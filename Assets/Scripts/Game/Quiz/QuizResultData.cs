using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ComputerQuiz
{
    [Serializable] public class QuizResultData
    {
        #region Variables

        [SerializeField] private string _quizName;
        [SerializeField] private Vector2Int _quizIndex;
        [SerializeField] private float _percent;
        [SerializeField] private int _ratingForQuiz;

        #endregion

        #region Properties

        public string QuizName { get => _quizName; }
        public Vector2Int QuizIndex { get => _quizIndex; }
        public float Percent { get => _percent; }
        public int RatingForQuiz { get => _ratingForQuiz; }

        #endregion

        #region Constructors

        public QuizResultData(string quizName, Vector2Int quizIndex, float percent, int ratingForQuiz)
        {
            _quizName = quizName;
            _quizIndex = quizIndex;
            _percent = percent;
            _ratingForQuiz = ratingForQuiz;
        }

        #endregion
    }
}