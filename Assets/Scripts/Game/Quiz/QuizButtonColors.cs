using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ComputerQuiz
{
    [CreateAssetMenu(fileName = "QuizButtonColors", menuName = "ComputerQuiz/Settings/QuizButtonColors", order = 1)]
    public class QuizButtonColors : ScriptableObject
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private Color32 _chooseColor;
        [SerializeField] private Color32 _wrongColor;
        [SerializeField] private Color32 _rightColor;

        #endregion

        #region Properties

        public Color32 ChooseColor { get => _chooseColor; }
        public Color32 WrongColor { get => _wrongColor; }
        public Color32 RightColor { get => _rightColor; }

        #endregion
    }
}