using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ComputerQuiz
{
    [CreateAssetMenu(fileName = "Quiz", menuName = "ComputerQuiz/Objects/Quiz", order = 1)]
    public class Quiz : ScriptableObject
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private string _name;
        [SerializeField] private QuizPageInfo[] _pages;

        #endregion

        #region Properties

        public string Name { get => _name; }
        public QuizPageInfo[] Pages { get => _pages; }

        #endregion
    }
}