using System;
using UnityEngine;

namespace Globenar
{
    [Serializable] public class Profile
    {
        #region Variables

        [SerializeField] private string _name;
        [SerializeField] private string _birthdayDate;
        [SerializeField] private string _gender;
        [SerializeField] private string _email;
        [SerializeField] private string _password;

        #endregion

        #region Properties

        public string Name { get => _name; }
        public string BirthdayDate { get => _birthdayDate; }
        public string Gender { get => _gender; }
        public string Email { get => _email; }
        public string Password { get => _password; }

        #endregion

        #region Constructors

        public Profile(string name, string birthdayDate, string gender, string email, string password)
        {
            _name = name;
            _birthdayDate = birthdayDate;
            _gender = gender;
            _email = email;
            _password = password;
        }

        #endregion
    }
}