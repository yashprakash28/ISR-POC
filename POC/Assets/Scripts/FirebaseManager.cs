using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;
using Firebase.Database;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager instance;

    //Firebase variables
    [Header("Firebase")]
    // public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;    
    public FirebaseUser User;
    public DatabaseReference DBreference;

    [Header("Data Load From Database")]
    public string _emailtext;
    public string _usernametext;


    //Login variables
    [Header("Login")]
    [SerializeField]
    private TMP_InputField loginEmail;
    [SerializeField]
    private TMP_InputField loginPassword;
    [SerializeField]
    private TMP_Text loginOutputText;
    [Space(5f)]

    //Register variables
    [Header("Register")]
    [SerializeField]
    public TMP_InputField registerUsername;
    [SerializeField]
    private TMP_InputField registerEail;
    [SerializeField]
    private TMP_InputField registerPassword;
    [SerializeField]
    private TMP_InputField registerConfirmPassword;
    [SerializeField]
    private TMP_Text registerOutputText;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(instance.gameObject);
            instance = this;
        }
    }

    private void Start()
    {
        StartCoroutine(CheckAndFixDependancies());
    }

    private IEnumerator CheckAndFixDependancies()
    {
        var checkAndFixDependanciesTask = FirebaseApp.CheckAndFixDependenciesAsync();

        yield return new WaitUntil(predicate: () => checkAndFixDependanciesTask.IsCompleted);

        var dependancyResult = checkAndFixDependanciesTask.Result;

        if (dependancyResult == DependencyStatus.Available)
        {
            //If they are avalible Initialize Firebase
            InitializeFirebase();
        }
        else
        {
            Debug.LogError("Could not resolve all Firebase dependencies: " + dependancyResult);
        }
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        StartCoroutine(checkAutoLogin());
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);

        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        Debug.Log(DBreference);
    }

    private IEnumerator checkAutoLogin()
    {
        yield return new WaitForEndOfFrame();
        if (User != null)
        {
            var reloadUserTask = User.ReloadAsync();
            yield return new WaitUntil(predicate: () => reloadUserTask.IsCompleted);

            AutoLogin();
        }
        else
        {
            AuthUIManager.instance.LoginScreen();
        }
    }

    private void AutoLogin()
    {
        if (User != null)
        {
            // Email Varification
            if (User.IsEmailVerified)
            {
                StartCoroutine(LoadUserData());
                GameManager.instance.ChangeScene(1);
            }
            else
            {
                StartCoroutine(SendVerificationEmail());
            }
        }
        else
        {
            AuthUIManager.instance.LoginScreen();
        }
    }
    private void AuthStateChanged (object sender, System.EventArgs eventArgs)
    {
        if(auth.CurrentUser != User)
        {
            bool signedIn = User != auth.CurrentUser && auth.CurrentUser != null;
            if(!signedIn && User != null)
            {
               Debug.Log("Signed Out");   
            }

            User = auth.CurrentUser;

            if(signedIn)
            {
                Debug.Log($"Signed In : {User.DisplayName}");
            }
        }
    }

    public void ClearOutputs()
    {
        loginOutputText.text = " ";
        registerOutputText.text = " ";
    }

    //Function for the login button
    public void LoginButton()
    {
        //Call the login coroutine passing the email and password
        StartCoroutine(Login(loginEmail.text, loginPassword.text));
    }

    //Function for the register button
    public void RegisterButton()
    {
        //Call the register coroutine passing the email, password, and username
        StartCoroutine(Register(registerEail.text, registerPassword.text, registerUsername.text ,registerConfirmPassword.text));
    }

    public void SaveDataButton()
    {
        StartCoroutine(UpdateUsernameAuth(registerUsername.text));
        StartCoroutine(UpdateUsernameDatabase(registerUsername.text));
        
        StartCoroutine(UpdateEmailId(registerEail.text));
        StartCoroutine(UpdatePassword(registerPassword.text));
    }

    private IEnumerator Login(string _email, string _password)
    {
        Credential credential = EmailAuthProvider.GetCredential(_email, _password);
        //Call the Firebase auth signin function passing the email and password
        var loginTask = auth.SignInWithCredentialAsync(credential);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => loginTask.IsCompleted);

        if (loginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {loginTask.Exception}");
            FirebaseException firebaseEx = (FirebaseException)loginTask.Exception.GetBaseException();
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Unkown Error, Please Try Again";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            loginOutputText.text = message;
            // yield return new WaitForSeconds(2);
            // loginOutputText.text = " ";

        }
        else
        {
            if(User.IsEmailVerified)
            {
                
                yield return new WaitForSeconds(1f);
                GameManager.instance.ChangeScene(1);
                StartCoroutine(LoadUserData());
            }
            else
            {
                //send verification email
                StartCoroutine(SendVerificationEmail());
            }
            
            //User is now logged in
            //Now get the result
            // User = loginTask.Result;
            // Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            // loginOutputText.text = "";
            // confirmLoginText.text = "Logged In";
            // yield return new WaitForSeconds(2);
            // confirmLoginText.text = " ";
        }
    }

    private IEnumerator Register(string _email, string _password, string _username, string _confirmPassword)
    {
        if (_username == "")
        {
            //If the username field is blank show a warning
            registerOutputText.text = "Please Enter A UserName";
            // yield return new WaitForSeconds(2);
            // registerOutputText.text = " ";
        }
        else if(_password != _confirmPassword)
        {
            //If the password does not match show a warning
            registerOutputText.text = "Password Does Not Match!";
            // yield return new WaitForSeconds(2);
            // registerOutputText.text = " ";
        }
        else 
        {
            //Call the Firebase auth signin function passing the email and password
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = (FirebaseException)RegisterTask.Exception.GetBaseException();
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                    case AuthError.InvalidEmail:
                        message = " Invalid Email";
                        break;
                }
                registerOutputText.text = message;
                // yield return new WaitForSeconds(2);
                // registerOutputText.text = " ";
            }
            else
            {
                UserProfile profile = new UserProfile
                {
                    DisplayName = _username,
                };

                var defaultUserTask = User.UpdateUserProfileAsync(profile);

                yield return new WaitUntil(predicate: () => defaultUserTask.IsCompleted);
                //User has now been creatd
                //Now get the result

                if (defaultUserTask.Exception != null)
                {
                    User.DeleteAsync();
                    //If there are errors handle them
                    Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                    FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                    AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                    string message = "Register Failed!";
                    switch (errorCode)
                    {
                        case AuthError.Cancelled:
                            message = "Update User Cancelled";
                            break;
                        case AuthError.SessionExpired:
                            message = "SessionExpired";
                            break;
                        
                    }
                    registerOutputText.text = message;
                    // yield return new WaitForSeconds(2);
                    // registerOutputText.text = " ";
                }
                else
                {
                    Debug.Log($"Firebase User Created Successfully: {User.DisplayName} ({User.UserId})");
                    SaveDataButton();
                    StartCoroutine(SendVerificationEmail());
                }
                // User = RegisterTask.Result;

                // if (User != null)
                // {
                //     //Create a user profile and set the username
                //     // UserProfile profile = new UserProfile{DisplayName = _username};

                //     //Call the Firebase auth update user profile function passing the profile with the username
                //     var ProfileTask = User.UpdateUserProfileAsync(profile);
                //     //Wait until the task completes
                //     yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                //     if (ProfileTask.Exception != null)
                //     {
                //         //If there are errors handle them
                //         Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                //         FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                //         AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                //         registerOutputText.text = "Username Set Failed!";
                //         yield return new WaitForSeconds(2);
                //         registerOutputText.text = " ";
                //     }
                //     else
                //     {
                //         //Username is now set
                //         //Now return to login screen
                //         AuthUIManager.instance.LoginScreen();
                //         registerOutputText.text = "";
                //         yield return new WaitForSeconds(2);
                //         registerOutputText.text = " ";
                //     }
            }
        }
    }

    private IEnumerator SendVerificationEmail()
    {
        if(User != null)
        {
            var emailTask = User.SendEmailVerificationAsync();

            yield return new WaitUntil(predicate: () => emailTask.IsCompleted);

            if(emailTask.Exception != null)
            {
                FirebaseException firebaseException = (FirebaseException)emailTask.Exception.GetBaseException();

                AuthError error = (AuthError)firebaseException.ErrorCode;

                string output = "Unkown Error, try Again";

                switch(error)
                {
                    case AuthError.Cancelled:
                        output = "Verification Task was Cancelled";
                        break;
                    case AuthError.InvalidRecipientEmail:
                        output = "Invalid";
                        break;
                    case AuthError.TooManyRequests:
                        output = "Too Many Requests";
                        break;
                }

                AuthUIManager.instance.AwaitVerification(false, User.Email, output);
        
            }
            else
            {
                AuthUIManager.instance.AwaitVerification(true, User.Email, null);
                Debug.Log("Email Sent Seccessfully");
            }
        }
    }


    private IEnumerator UpdateUsernameAuth(string _username)
    {
        // Create a user profile and set the username
        UserProfile profile = new UserProfile { DisplayName = _username};
        
        // Call the firebase auth update user profile function passing the profile with the username
        var ProfileTask = User.UpdateUserProfileAsync(profile);
        // wait untill the task completes
        yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

        if (ProfileTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
        }
        else
        {
            // auth username is now updated
        }
    }

    private IEnumerator UpdateUsernameDatabase(string _username)
    {
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("username").SetValueAsync(_username);
        
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            // database username is now updated
        }
    }

    private IEnumerator UpdateEmailId(string _email)
    {
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("email").SetValueAsync(_email);
        
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            // database email is now updated
        }
    }

    private IEnumerator UpdatePassword(string _password)
    {
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("password").SetValueAsync(_password);
        
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            // database password is now updated
        }
    }

    public IEnumerator LoadUserData()
    {

        var DBTask = DBreference.Child("users").Child(User.UserId).GetValueAsync();
        
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            // no data exists
            _usernametext = "No username Exist";
            _emailtext = "No email Exist";
            Debug.Log("0");
            Debug.Log("0");
            Debug.Log("0");
        }
        else
        {
            // data retrieved

            DataSnapshot snapshot = DBTask.Result;
            // LobbyManager.instance._email.text = "Mera email";
            _usernametext = snapshot.Child("username").Value.ToString();
            _emailtext = snapshot.Child("email").Value.ToString();
            Debug.Log(snapshot.Child("username"));
            Debug.Log(snapshot.Child("email"));
            Debug.Log(snapshot.Child("password"));
            Debug.Log("Data retreived");
        }
    }

}