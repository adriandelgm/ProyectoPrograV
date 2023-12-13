using Firebase.Auth.Providers;
using Firebase.Auth;
using Microsoft.AspNetCore.DataProtection;

namespace ProyectoProgra5.FirebaseAuth
{
    public static class FirebaseAuthHelper
    {
        public const string firebaseAppId = "libreriaintenacional";
        public const string firebaseApiKey = "AIzaSyBaUUXBcC0FOjEl4l626wcOJXRwD5p_IAE";

        public static FirebaseAuthClient setFirebaseAuthClient()
        {
            var response = new FirebaseAuthClient(new FirebaseAuthConfig
            {
                ApiKey = firebaseApiKey,
                AuthDomain = $"{firebaseAppId}.firebaseapp.com",
                Providers = new FirebaseAuthProvider[]
                    {
                        new EmailProvider()
                    }
            });

            return response;
        }
    }
}
