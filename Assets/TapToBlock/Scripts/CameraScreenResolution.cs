using UnityEngine;
using System.Collections;

public class CameraScreenResolution : MonoBehaviour
{
    public bool maintainWidth = true;
    [Range(-1, 1)]
    public int adaptPosition;
    float defaultWidth;
    float defaultHeight;
    Vector3 CameraPos;

    public enum CamState { still, shaking };
    CamState camState = CamState.still;
    [SerializeField] float shakeMargin = 1f;
    [SerializeField] float shakeTime = 1f;
    float timerShake;
    Vector3 shakeDirection;

    // Use this for initialization
    void Start()
    {

        CameraPos = Camera.main.transform.position;

        defaultHeight = Camera.main.orthographicSize;
        defaultWidth = Camera.main.orthographicSize * (9f / 16f); // Camera.main.aspect;
        //.Log(string.Format("width: {0}, height: {1}", defaultWidth, defaultHeight));
        //.Log("adapt: " + adaptPosition * (defaultWidth - Camera.main.orthographicSize * Camera.main.aspect));
    }

    public void ShakeCam(Vector3 initialDirection)
    {
        camState = CamState.shaking;
        shakeDirection = initialDirection;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCam();
    }

    void UpdateCam()
    {
        switch (camState)
        {
            case CamState.shaking:
                timerShake += Time.deltaTime;
                float t_shakeValue = shakeMargin * (shakeTime - timerShake) * Mathf.Sin(Mathf.PI * (10f * timerShake + 0.5f));
                //.Log(t_shakeValue);
                Vector3 defaultCamPos = new Vector3(CameraPos.x, CameraPos.y + adaptPosition * (defaultHeight - Camera.main.orthographicSize), CameraPos.z); //base on still camera
                transform.position = defaultCamPos + shakeDirection * t_shakeValue; // * UnityEngine.Random.value;
                if (timerShake >= shakeTime)
                {
                    timerShake = 0f;
                    camState = CamState.still;
                }
                break;
            case CamState.still:
                if (maintainWidth)
                {
                    Camera.main.orthographicSize = defaultWidth / Camera.main.aspect;
                    //CameraPos.y was added in case camera in case camera's y is not in 0
                    Camera.main.transform.position = new Vector3(CameraPos.x, CameraPos.y + adaptPosition * (defaultHeight - Camera.main.orthographicSize), CameraPos.z);
                }
                else
                {
                    //CameraPos.x was added in case camera in case camera's x is not in 0
                    Camera.main.transform.position = new Vector3(CameraPos.x + adaptPosition * (defaultWidth - Camera.main.orthographicSize * Camera.main.aspect), CameraPos.y, CameraPos.z);
                }
                break;
        }
    }
}