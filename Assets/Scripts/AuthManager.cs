using UnityEngine;
using System.Runtime.InteropServices;

public class AuthManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    // Declare the external JavaScript functions with the correct attribute
    [DllImport("__Internal")]
    private static extern void SignUpNative(string email, string password);
    
    [DllImport("__Internal")]
    private static extern void SignInNative(string email, string password);
    
    [DllImport("__Internal")]
    private static extern void SignOutNative();

    
    // Declare the external JavaScript functions with the correct attribute
    //[System.Runtime.InteropServices.DllImport("__Internal", EntryPoint = "signUp")]
    //private static extern void SignUpExternal(string email, string password);
    //
    //[System.Runtime.InteropServices.DllImport("__Internal", EntryPoint = "signIn")]
    //private static extern void SignInExternal(string email, string password);
    //
    //[System.Runtime.InteropServices.DllImport("__Internal", EntryPoint = "signOut")]
    //private static extern void SignOutExternal();
    //


    void Start()
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
            FirebasePlugin.InitializeFirebase();
        #endif
    }


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
        #endif
    }

    //public void SignUp(string email, string password)
    //{
    //    #if UNITY_WEBGL && !UNITY_EDITOR
    //        SignUpExternal(email, password);
    //    #endif
    //}

    //public void SignIn(string email, string password)
    //{
    //    #if UNITY_WEBGL && !UNITY_EDITOR
    //        SignInExternal(email, password);
    //    #endif
    //}

    //public void SignOut()
    //{
    //    #if UNITY_WEBGL && !UNITY_EDITOR
     //       SignOutExternal();
     //   #endif
    //}
    
    // Callback methods called from JavaScript
    public void OnSignUpSuccess(string userId)
    {
        Debug.Log($"Sign up successful. User ID: {userId}");
        // Handle successful sign up
        uiManager.OnAuthenticationSuccess(userId);
    }

    public void OnSignInSuccess(string userId)
    {
        Debug.Log($"Sign in successful. User ID: {userId}");
        // Handle successful sign in
        uiManager.OnAuthenticationSuccess(userId);
    }

    public void OnSignOutSuccess(string message)
    {
        Debug.Log("Sign out successful");
        // Handle successful sign out
        uiManager.ShowLoginPanel();
    }

    public void OnAuthError(string errorMessage)
    {
        Debug.LogError($"Auth error: {errorMessage}");
        // Handle authentication error
        uiManager.OnAuthenticationError(errorMessage);
    }

    public void OnAuthStateChanged(string userId)
    {
        if (!string.IsNullOrEmpty(userId))
        {
            Debug.Log($"User is signed in: {userId}");
            uiManager.ShowGamePanel();
        }
        else
        {
            Debug.Log("User is signed out");
            uiManager.ShowGamePanel();
        }
    }
}
