using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ComputerQuiz
{
    [CreateAssetMenu(fileName = "QuizPageInfo", menuName = "ComputerQuiz/Objects/QuizPageInfo", order = 1)]
    public class QuizPageInfo : ScriptableObject
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private Sprite _helpImage;
        [SerializeField] private string _quastion;
        [SerializeField] private string[] _answers;
        [SerializeField] private int[] _numbersRightAnswers;

        #endregion

        #region Properties

        public Sprite HelpImage { get => _helpImage; }
        public string Quastion { get => _quastion; }
        public string[] Answers { get => _answers;}
        public int[] NumbersRightAnswers { get => _numbersRightAnswers; }

        #endregion
    }
}