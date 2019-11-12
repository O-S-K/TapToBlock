using UnityEngine;

#if UNITY_IOS
using UnityEngine.iOS;
#endif

public class NativeReviewRequest {

    public static void RequestReview(string url)
    {
        if (PlayerPrefs.GetInt("NATIVE_REVIEW", 0) == 0)
        {
            if (RequestReview())
            {
                PlayerPrefs.SetInt("NATIVE_REVIEW", 1);
                PlayerPrefs.Save();
            }
            else
            {
                Application.OpenURL(url);
            }
        }
        else
        {
            Application.OpenURL(url);
        }
    }

    static bool RequestReview() {
#if UNITY_IOS && !UNITY_EDITOR
		return Device.RequestStoreReview();
#else
        return false;
#endif
	}
}
