using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase;
using System.Threading.Tasks;
using UnityEngine.Events;
using System.Security.Cryptography;
using TMPro;

public class Authentification : MonoBehaviour
{
    [Header("Log In Text References")]
    [SerializeField] private TMP_InputField li_email;
    [SerializeField] private TMP_InputField li_password;
    [Header("Sign Up Text References")]
    [SerializeField] private TMP_InputField su_email;
    [SerializeField] private TMP_InputField su_password;

    private FirebaseAuth _authReference;

    public UnityEvent OnLogInSuccesful = new UnityEvent();
    public UnityEvent OnSignUpSuccesful = new UnityEvent();

    private void OnEnable()
    {
        OnLogInSuccesful.AddListener(LoadMainMenu);
    }

    private void LoadMainMenu()
    {
        GlobalSceneManager.Instance.LoadNormal("MainMenu");
    }

    private void Awake()
    {
        _authReference = FirebaseAuth.GetAuth(FirebaseApp.DefaultInstance);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LogOut();
        }
    }
    public void LogIn()
    {
        StartCoroutine(SignInWithEmail(li_email.text, li_password.text));
    }
    public void SignUp()
    {
        StartCoroutine(RegisterUser(su_email.text, su_password.text));
    }
    /*
    public void RecoverPassword()
    {
        StartCoroutine(RecoverPassword(email));
    }
    private IEnumerator RecoverPassword(string email)
    {
        Debug.Log("Registering");
        var registerTask = _authReference.SendPasswordResetEmailAsync(email);
        yield return new WaitUntil(() => registerTask.IsCompleted);
    }
    */

    private IEnumerator RegisterUser(string email, string password)
    {
        Debug.Log("Registering");
        var registerTask = _authReference.CreateUserWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(() => registerTask.IsCompleted);

        if(registerTask.Exception != null)
        {
            Debug.LogWarning($"Failed to register task with {registerTask.Exception}");
        }
        else
        {
            Debug.Log($"Succesfully registered user {registerTask.Result.User.Email}");
            OnSignUpSuccesful?.Invoke();
        }
    }

    private IEnumerator SignInWithEmail(string email, string password)
    {
        Debug.Log("Loggin In");

        var loginTask = _authReference.SignInWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(() => loginTask.IsCompleted);

        if (loginTask.Exception != null)
        {
            Debug.LogWarning($"Login failed with {loginTask.Exception}");
        }
        else
        {
            Debug.Log($"Login succeeded with {loginTask.Result.User.Email}");
            OnLogInSuccesful?.Invoke();
        }
    }

    public void LogOut()
    {
        FirebaseAuth.DefaultInstance.SignOut();
        GlobalSceneManager.Instance.LoadNormal("Auth");
    }
}
