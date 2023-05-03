using System;
using UnityEngine;

namespace Globenar
{
    [Serializable] public class Profile
    {
        #region Variables

        [SerializeField] private string _name;
        [SerializeField] private int _age;
        [SerializeField] private string _gender;
        [SerializeField] private string _country;
        [SerializeField] private string _email;

        #endregion

        #region Properties

        public string Name { get => _name; }
        public int Age { get => _age; }
        public string Gender { get => _gender; }
        public string Country { get => _country; }
        public string Email { get => _email; }

        #endregion

        #region Constructors

        public Profile(string name, int age, string gender, string country, string email)
        {
            _name = name;
            _age = age;
            _gender = gender;
            _country = country;
            _email = email;
        }

        #endregion
    }
}