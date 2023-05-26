using Kamen;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using System.Net.Mail;

namespace Globenar
{
    public class FirebaseAuthManager : SingletonComponent<FirebaseAuthManager>
    {
        FirebaseAuth auth;
        FirebaseUser user;

        public void SignUp(string email, string password)
        {
                auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    return;
                }

                // Firebase user has been created.
                Firebase.Auth.AuthResult result = task.Result;
                Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                    result.User.DisplayName, result.User.UserId);
            });
        }
        public void LogIn(string email, string password)
        {
            auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    return;
                }

                Firebase.Auth.AuthResult result = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                    result.User.DisplayName, result.User.UserId);
            });
        }

        public void InitializeFirebase()
        {
            auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
            auth.StateChanged += AuthStateChanged;
            AuthStateChanged(this, null);
        }

        void AuthStateChanged(object sender, System.EventArgs eventArgs)
        {
            if (auth.CurrentUser != user)
            {
                bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null
                    && auth.CurrentUser.IsValid();
                if (!signedIn && user != null)
                {
                    Debug.Log("Signed out " + user.UserId);
                }
                user = auth.CurrentUser;
                if (signedIn)
                {
                    Debug.Log("Signed in " + user.UserId);
                    //displayName = user.DisplayName ?? "";
                   // emailAddress = user.Email ?? "";
                    //photoUrl = user.PhotoUrl ?? "";
                }
            }
        }

        void OnDestroy()
        {
            auth.StateChanged -= AuthStateChanged;
            auth = null;
        }
    }
}