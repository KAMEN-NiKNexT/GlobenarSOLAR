using Kamen;
using Kamen.DataSave;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

namespace ComputerQuiz
{
    public class RatingManager : SingletonComponent<RatingManager>
    {
        #region Classes 

        [Serializable] private class DifficultySettings
        {
            #region DifficultySettings Variables

            [SerializeField] private string _name;
            [SerializeField] private int _minRating;
            [SerializeField] private int _maxRating;
            [SerializeField] private Quiz[] _quizzes;

            #endregion

            #region DifficultySettings Properties

            public string Name { get => _name; }
            public int MinRating { get => _minRating; }
            public int MaxRating { get => _maxRating; }
            public Quiz[] Quizzes { get => _quizzes; }

            #endregion
        }

        #endregion

        #region Variables

        [Header("Settings")]
        [SerializeField] private DifficultySettings[] _difficultySettings;
        [SerializeField] private int _ratingForRightAnswer;
        [SerializeField] private int _ratingForWrongAnswer;

        #endregion

        #region Control Methods

        public Vector2Int GetQuizId()
        {
            Vector2Int index = Vector2Int.zero;
            DifficultySettings settings = GetDifficultySettingsByRating(CalculateCurrentRating(), out int indexX);
            index.x = indexX;
            
            for (int i = 0; i < DataSaveManager.Instance.MyData.QuizResults.Count; i++)
            {
                if (DataSaveManager.Instance.MyData.QuizResults[i].QuizIndex.x == index.x) index.y = DataSaveManager.Instance.MyData.QuizResults[i].QuizIndex.y + 1;
            }

            if (index.y >= settings.Quizzes.Length) return new Vector2Int(-1, -1);
            return index;
        }
        public Quiz GetQuiz(Vector2Int index)
        {
            return _difficultySettings[index.x].Quizzes[index.y];
        }

        #endregion

        #region Calculate Methods

        public QuizResultData GetQuizResultByIndex(Vector2 index, out int number)
        {
            for (int i = 0; i < DataSaveManager.Instance.MyData.QuizResults.Count; i++)
            {
                if (index == DataSaveManager.Instance.MyData.QuizResults[i].QuizIndex)
                {
                    number = i;
                    return DataSaveManager.Instance.MyData.QuizResults[i];
                }
            }
            Debug.LogError($"[RatingManager] - Результата с идексом - {index} не существует");
            number = 0;
            return null;
        }
        public int CalculateRatingForQuiz(int amountRightAnswer, int amountQuastion)
        {
            return amountRightAnswer * _ratingForRightAnswer + (amountQuastion - amountRightAnswer) * _ratingForWrongAnswer;
        }
        private int CalculateCurrentRating()
        {
            int rating = 0;
            for (int i = 0; i < DataSaveManager.Instance.MyData.QuizResults.Count; i++)
            {
                rating += DataSaveManager.Instance.MyData.QuizResults[i].RatingForQuiz;
            }
            return rating;
        }
        private DifficultySettings GetDifficultySettingsByRating(int rating, out int index )
        {
            for (int i = 0; i < _difficultySettings.Length; i++)
            {
                if (rating >= _difficultySettings[i].MinRating && rating < _difficultySettings[i].MaxRating)
                {
                    index = i;
                    return _difficultySettings[i];
                }
            }
            Debug.LogError($"[RatingManager] - Сложности с рейтингом - {rating} ну существует");
            index = 0;
            return null;
        }

        #endregion
    }
}