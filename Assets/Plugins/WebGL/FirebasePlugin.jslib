var FirebasePlugin = {
    InitializeFirebase: function() {
        if (typeof firebase === 'undefined') {
            var initializeFirebaseDelayed = function() {
                var firebaseConfig = {
                    apiKey: "AIzaSyDPIiLeeLJbgkH8_lMZEk6x0CiHTVGCAys",
                    authDomain: "kasongokaundatime.firebaseapp.com",
                    projectId: "kasongokaundatime",
                    storageBucket: "kasongokaundatime.firebasestorage.app",
                    messagingSenderId: "875549036873",
                    appId: "1:875549036873:web:054d5af48a87227af5ca98",
                    measurementId: "G-XB5NWEQ7MW"
                };
               
                firebase.initializeApp(firebaseConfig);
            };
            setTimeout(initializeFirebaseDelayed, 1000);
        }
    },

    SignUpNative: function(email, password) {
        var emailStr = UTF8ToString(email);
        var passwordStr = UTF8ToString(password);
       
        firebase.auth().createUserWithEmailAndPassword(emailStr, passwordStr)
            .then(function(userCredential) {
                unityInstance.SendMessage('AuthManager', 'OnSignUpSuccess', userCredential.user.uid);
            })
            .catch(function(error) {
                unityInstance.SendMessage('AuthManager', 'OnAuthError', error.message);
            });
    },

    SignInNative: function(email, password) {
        var emailStr = UTF8ToString(email);
        var passwordStr = UTF8ToString(password);
       
        firebase.auth().signInWithEmailAndPassword(emailStr, passwordStr)
            .then(function(userCredential) {
                unityInstance.SendMessage('AuthManager', 'OnSignInSuccess', userCredential.user.uid);
            })
            .catch(function(error) {
                unityInstance.SendMessage('AuthManager', 'OnAuthError', error.message);
            });
    },

    SignOutNative: function() {
        firebase.auth().signOut()
            .then(function() {
                unityInstance.SendMessage('AuthManager', 'OnSignOutSuccess', '');
            })
            .catch(function(error) {
                unityInstance.SendMessage('AuthManager', 'OnAuthError', error.message);
            });
    }
};

mergeInto(LibraryManager.library, FirebasePlugin);
