using SS.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FirebaseManager
{
    public static Firebase.FirebaseApp app;
    public static bool FirebaseReady = false;
    public static void CheckGooglePlayService()
    {
        Firebase.FirebaseApp.CheckDependenciesAsync().ContinueWith(taskCheck =>
        {
            var dependencyStatus = taskCheck.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                //   app = Firebase.FirebaseApp.DefaultInstance;
                FirebaseManager.app = Firebase.FirebaseApp.DefaultInstance;
                //Firebase.InstanceId.FirebaseInstanceId.DefaultInstance.GetTokenAsync().ContinueWith(
                //    task =>
                //    {
                //        if (!(task.IsCanceled || task.IsFaulted) && task.IsCompleted)
                //        {
                //            UnityEngine.Debug.Log(System.String.Format("Instance ID Token {0}", task.Result));
                //        }
                //    });
                // Set a flag here to indicate whether Firebase is ready to use by your app.
                FirebaseManager.FirebaseReady = true;
                //FirebaseConfigManager.fetch(null);
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
                //Manager.Add(PopupController.POPUP_SCENE_NAME, new PopupData(System.String.Format(
                //  "Could not resolve all Firebase dependencies: {0}", dependencyStatus), PopupType.OK));
                FirebaseManager.FirebaseReady = false;
            }
        });
    }
}
