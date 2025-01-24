using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class UserData
{
    public string uid;
    public string email;
}

public class UIManager : MonoBehaviour
{
    [Header("Auth References")]
    [SerializeField] private AuthManager authManager;
    
    [Header("Input Fields")]
    [SerializeField] private TMP_InputField emailInput;
    [SerializeField] private TMP_InputField passwordInput;
    
    [Header("UI Panels")]
    [SerializeField] private GameObject loginPanel;
    [SerializeField] private GameObject gamePanel;
    
    [Header("Status Text")]
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private TMP_Text welcomeText;

    private TouchScreenKeyboard keyboard;


    private void Start()
    {

    // Set Canvas to handle touch input properly
    //Canvas canvas = GetComponentInParent<Canvas>();
    //canvas.renderMode = RenderMode.ScreenSpaceCamera;
    //canvas.worldCamera = Camera.main;

    // Configure input fields for touch devices
    emailInput.inputType = TMP_InputField.InputType.Standard;
    emailInput.keyboardType = TouchScreenKeyboardType.EmailAddress;
    emailInput.shouldHideMobileInput = false;
    emailInput.contentType = TMP_InputField.ContentType.EmailAddress;
    emailInput.onValueChanged.AddListener((value) => { emailInput.text = value; });
    

    passwordInput.inputType = TMP_InputField.InputType.Password;
    passwordInput.keyboardType = TouchScreenKeyboardType.Default;
    passwordInput.shouldHideMobileInput = false;
    passwordInput.contentType = TMP_InputField.ContentType.Password;
    passwordInput.onValueChanged.AddListener((value) => { passwordInput.text = value; });

    //emailInput.onEndEdit.AddListener((value) => {
    //    emailInput.text = value;
    //});

    //passwordInput.onEndEdit.AddListener((value) => {
    //    passwordInput.text = value;
    //});

        ShowLoginPanel();
        ClearInputFields();
    }

/*
public void OnEmailInputStartEdit()
{
    keyboard = TouchScreenKeyboard.Open(emailInput.text, TouchScreenKeyboardType.EmailAddress);
}

public void OnPasswordInputStartEdit()
{
    keyboard = TouchScreenKeyboard.Open(passwordInput.text, TouchScreenKeyboardType.Default);
}

public void OnEmailInputEndEdit()
{
    emailInput.text = keyboard.text;
    keyboard = null;
}

public void OnPasswordInputEndEdit()
{
    passwordInput.text = keyboard.text;
    keyboard = null;
}
*/

    public void OnSignUpButtonClick()
    {
        if (ValidateInput())
        {
            authManager.SignUp(emailInput.text, passwordInput.text);
            statusText.text = "Creating your player profile... 🌟";
        }
    }

    public void OnSignInButtonClick()
    {
        if (ValidateInput())
        {
            authManager.SignIn(emailInput.text, passwordInput.text);
            statusText.text = "Getting your game ready... 🎲";
        }
    }

    public void OnSignOutButtonClick()
    {
        authManager.SignOut();
        ShowLoginPanel();
        ClearInputFields();
        statusText.text = "See you next time! 👋";
    }

    private bool ValidateInput()
    {
        if (string.IsNullOrEmpty(emailInput.text))
        {
            statusText.text = "Oops! Don't forget to write your email! 📧";
            return false;
        }

        if (string.IsNullOrEmpty(passwordInput.text))
        {
            statusText.text = "Hey! You need a secret password to play! 🔑";
            return false;
        }

        if (passwordInput.text.Length < 6)
        {
            statusText.text = "Your password needs to be longer - try adding more letters or numbers! ✨";
            return false;
        }

        return true;
    }

    public void ShowLoginPanel()
    {
        loginPanel.SetActive(true);
        gamePanel.SetActive(false);
    }

    public void ShowGamePanel()
    {
        loginPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    private void ClearInputFields()
    {
        emailInput.text = "";
        passwordInput.text = "";
        statusText.text = "";
    }

    // Called from AuthManager when authentication is successful
    public void OnAuthenticationSuccess(string userData)
    {   
        UserData data = JsonUtility.FromJson<UserData>(userData);
        welcomeText.text = $"Yay! Let's play, {data.email}!";
        ShowGamePanel();
        ClearInputFields();
    }

    // Called from AuthManager when authentication fails
    public void OnAuthenticationError(string error)
    {
        statusText.text = "Uh-oh! Something went wrong. Let's try again! 🔄";
    }
}
