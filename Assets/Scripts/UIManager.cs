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
    Canvas canvas = GetComponentInParent<Canvas>();
    canvas.renderMode = RenderMode.ScreenSpaceCamera;
    canvas.worldCamera = Camera.main;

    // Configure input fields for touch devices
    emailInput.inputType = TMP_InputField.InputType.Standard;
    emailInput.keyboardType = TouchScreenKeyboardType.EmailAddress;
    emailInput.shouldHideMobileInput = false;
    emailInput.contentType = TMP_InputField.ContentType.EmailAddress;
    

    passwordInput.inputType = TMP_InputField.InputType.Password;
    passwordInput.keyboardType = TouchScreenKeyboardType.Default;
    passwordInput.shouldHideMobileInput = false;
    passwordInput.contentType = TMP_InputField.ContentType.Password;

    emailInput.onEndEdit.AddListener((value) => {
        emailInput.text = value;
    });

    passwordInput.onEndEdit.AddListener((value) => {
        passwordInput.text = value;
    });
    
        ShowLoginPanel();
        ClearInputFields();
    }
    
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

    public void OnSignUpButtonClick()
    {
        if (ValidateInput())
        {
            authManager.SignUp(emailInput.text, passwordInput.text);
            statusText.text = "Signing up...";
        }
    }

    public void OnSignInButtonClick()
    {
        if (ValidateInput())
        {
            authManager.SignIn(emailInput.text, passwordInput.text);
            statusText.text = "Signing in...";
        }
    }

    public void OnSignOutButtonClick()
    {
        authManager.SignOut();
        ShowLoginPanel();
        ClearInputFields();
    }

    private bool ValidateInput()
    {
        if (string.IsNullOrEmpty(emailInput.text))
        {
            statusText.text = "Please enter an email";
            return false;
        }

        if (string.IsNullOrEmpty(passwordInput.text))
        {
            statusText.text = "Please enter a password";
            return false;
        }

        if (passwordInput.text.Length < 6)
        {
            statusText.text = "Password must be at least 6 characters";
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
        welcomeText.text = $"Welcome, {data.email}!";
        ShowGamePanel();
        ClearInputFields();
    }

    // Called from AuthManager when authentication fails
    public void OnAuthenticationError(string error)
    {
        statusText.text = error;
    }
}
