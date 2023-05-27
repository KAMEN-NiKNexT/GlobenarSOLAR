using ComputerQuiz;
using Globenar;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kamen.DataSave
{
    [Serializable] public class Data
    {
        #region Variables

        public Action OnDataChanged;
        [SerializeField] private Profile _userProfile;
        [SerializeField] private List<QuizResultData> _quizResults = new List<QuizResultData>();

        #endregion

        #region Properties

        public Profile UserProfile 
        { 
            get => _userProfile; 
            set { if (value != null) _userProfile = value; }
        }
        public List<QuizResultData> QuizResults { get => _quizResults; }

        #endregion

        #region Methods


        #endregion
    }
}