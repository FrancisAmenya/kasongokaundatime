using UnityEngine;
using System.Runtime.InteropServices;

public class AuthManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    
    [DllImport("__Internal")]
    private static extern void InitializeFirebase();

    [DllImport("__Internal")]
    private static extern void SignUpNative(string email, string password);
    
    [DllImport("__Internal")]
    private static extern void SignInNative(string email, string password);
    
    [DllImport("__Internal")]
    private static extern void SignOutNative();

    private static UserData currentUser;
    private static bool isAuthenticated = false;

    private void Start()
    {
        if (isAuthenticated && currentUser != null)
        {
            string userData = JsonUtility.ToJson(currentUser);
            uiManager.OnAuthenticationSuccess(userData);
        }
    }


    // Unity message handlers with specific string parameters
    public void OnAuthSuccess(string userData)
    {
        Debug.Log($"Authentication successful. User data: {userData}");
        currentUser = JsonUtility.FromJson<UserData>(userData);
        isAuthenticated = true;
        uiManager.OnAuthenticationSuccess(userData);
    }

    public void OnAuthFailure(string error)
    {
        Debug.LogError($"Authentication error: {error}");
        isAuthenticated = false;
        currentUser = null;
        uiManager.OnAuthenticationError(error);
    }

    // Callback methods must match exactly with jslib function calls
    public void OnSignUpSuccess(string userId)
    {
        Debug.Log($"Sign up successful. User ID: {userId}");
        isAuthenticated = true;
        currentUser = JsonUtility.FromJson<UserData>(userId);
        uiManager.OnAuthenticationSuccess(userId);
    }

    public void OnSignInSuccess(string userId)
    {
        Debug.Log($"Sign in successful. User ID: {userId}");
        isAuthenticated = true;
        currentUser = JsonUtility.FromJson<UserData>(userId);
        uiManager.OnAuthenticationSuccess(userId);
    }

    public void OnSignOutSuccess()
    {
        Debug.Log("Sign out successful");
        isAuthenticated = false;
        currentUser = null;
        uiManager.ShowLoginPanel();
    }

    public void OnAuthError(string errorMessage)
    {
        Debug.LogError($"Auth error: {errorMessage}");
        isAuthenticated = false;
        currentUser = null;
        uiManager.OnAuthenticationError(errorMessage);
    }

    // Public methods called from UI
    public void SignUp(string email, string password)
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
            SignUpNative(email, password);
        #endif
    }

    public void SignIn(string email, string password)
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
            SignInNative(email, password);
        #endif
    }

    public void SignOut()
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
            SignOutNative();
            isAuthenticated = false;
            currentUser = null;
            uiManager.ShowLoginPanel();
        #endif
    }

    // Helper method to check authentication status
    public bool IsUserAuthenticated()
    {
        return isAuthenticated && currentUser != null;
    }
}
