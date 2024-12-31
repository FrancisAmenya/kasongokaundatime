using UnityEngine;
using System.Runtime.InteropServices;

public static class FirebasePlugin
{
    [DllImport("__Internal")]
    public static extern void InitializeFirebase();

    [DllImport("__Internal")]
    public static extern void SignInNative(string email, string password);

    [DllImport("__Internal")]
    public static extern void SignUpNative(string email, string password);

    [DllImport("__Internal")]
    public static extern void SignOutNative();
}
