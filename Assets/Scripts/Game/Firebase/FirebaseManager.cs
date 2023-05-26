using Kamen;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;

namespace Globenar
{
    public class FirebaseManager : SingletonComponent<FirebaseManager>
    {
        FirebaseApp app;
        protected override void Awake()
        {
            base.Awake();
            Initialzie();
        }
        private void Initialzie()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
                var dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                    app = FirebaseApp.DefaultInstance;
                    FirebaseAuthManager.Instance.InitializeFirebase();
                    Debug.Log("Good");
                }
                else
                {
                    Debug.LogError(string.Format(
                      "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                }
            });
        }
    }
}