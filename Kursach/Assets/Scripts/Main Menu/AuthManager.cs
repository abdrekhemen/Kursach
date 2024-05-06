using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField LoginInput;
    [SerializeField] private TMP_InputField PasswordInput;
    [SerializeField] private GameObject LoginWindow;
    [SerializeField] private GameObject MainMenu;

    private bool isSearchInProgress;

    private void Awake()
    {
        Process.Start(@"C:\Program Files\MongoDB\Server\6.0\bin\mongod.exe");
    }
        
    public async void Enter()
    {
        if (!isSearchInProgress)
        {
            isSearchInProgress = true;
            User user = await CRUD.GetUserWithLoginAsync(LoginInput.text);
            if (user != null)
            {
                if (user.Password == PasswordInput.text)
                {
                    LoggedIn(user);
                }
            }
            isSearchInProgress = false;
        }
    }

    private void LoggedIn(User user)
    {
        EndScreenManager.user = user;
        LoginWindow.SetActive(false);
        MainMenu.SetActive(true);
    }

    public async void Register()
    {
        if (!isSearchInProgress )
        {
            isSearchInProgress = true;
            User user = await CRUD.GetUserWithLoginAsync(LoginInput.text);
            if (user == null)
            {
                user = new User()
                {
                    Login = LoginInput.text,
                    Password = PasswordInput.text,
                };
                CRUD.CreateUser(user);
                LoggedIn(user);
            }
            isSearchInProgress = false;
        }
    }
}
