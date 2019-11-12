using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Analytics;

public class FbaLogEvent
{
    static void LogEvent(string name, string paramName, int value)
    {
#if LOG_EVENT
        //if (!AndroidUtil.IsGooglePlayServicesAvailable())
        //    return;
        if (!FirebaseManager.FirebaseReady) return;
        FirebaseAnalytics.LogEvent(name, paramName, value);
        //.Log("firebase log success " + name + paramName + value.ToString());
#endif
    }

    static void LogEvent(string name, string paramName, double value)
    {
#if LOG_EVENT
        if (!FirebaseManager.FirebaseReady) return;
        FirebaseAnalytics.LogEvent(name, paramName, value);
#endif
    }

    static void LogEvent(string name, string paramName, string value)
    {
#if LOG_EVENT
        if (!FirebaseManager.FirebaseReady) return;
        FirebaseAnalytics.LogEvent(name, paramName, value);
#endif
    }

    static void LogEvent(string name)
    {
#if LOG_EVENT
        if (!FirebaseManager.FirebaseReady)
        {
            Debug.Log("firebase not ready");
            return;
        }
        FirebaseAnalytics.LogEvent(name);
        Debug.Log("firebase log successfully " + name);
#endif
    }

    public static void LogSceneOpen(string screenName, string screenClass)
    {
        FirebaseAnalytics.SetCurrentScreen(screenName, screenClass);
    }

    public static void LogGameEvent(string paramName, int value)
    {
        LogEvent("GameEvent", paramName, value);
    }

    public static void LogGameEvent(string paramName, string value)
    {
        LogEvent("GameEvent", paramName, value);
    }

    public static void ChangedSetting(string paramName, double value)
    {
        LogEvent("Setting", paramName, value);
    }
}