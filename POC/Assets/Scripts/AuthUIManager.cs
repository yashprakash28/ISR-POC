using TMPro;
using UnityEngine;

public class AuthUIManager : MonoBehaviour
{
    // Start is called before the first frame update
   public static AuthUIManager instance;

    [Header("References")]
    [SerializeField]
    private GameObject chechkingForAccountUI;
    [SerializeField]
    private GameObject registerUI;
    [SerializeField]
    private GameObject verifyEmailUI;
    [SerializeField]
    private GameObject loginUI;
    [SerializeField]
    private TMP_Text verifyEmailText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(gameObject);
        }
    }
    
    private void ClearUI()
    {
        FirebaseManager.instance.ClearOutputs();
        loginUI.SetActive(false);
        registerUI.SetActive(false);
        verifyEmailUI.SetActive(false);
        // checkingForAccountUI.SetActive(false);
    }
    //Functions to change the login screen UI
    public void LoginScreen() //Back button
    {
        ClearUI();
        loginUI.SetActive(true);
        // registerUI.SetActive(false);
    }
    public void RegisterScreen() // Regester button
    {
        ClearUI();
        // loginUI.SetActive(false);
        registerUI.SetActive(true);
    }

    public void AwaitVerification(bool _emailSent , string _email , string _output)
    {
        ClearUI();
        verifyEmailUI.SetActive(true);
        if(_emailSent)
        {
            verifyEmailText.text = $"sent Email \n please Verify{_email}";
        }
        else
        {
            verifyEmailText.text = $"Email Not Sent: {_output}\nPleseVerify{_email}";
        }
    }
}
