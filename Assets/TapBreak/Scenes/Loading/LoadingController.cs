using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SS.View;

public class LoadingController : Controller
{
    public const string LOADING_SCENE_NAME = "Loading";

    [SerializeField] Transform m_LoadingIcon;

    public override string SceneName()
    {
        return LOADING_SCENE_NAME;
    }

    void Update()
    {
        m_LoadingIcon.Rotate(new Vector3(0, 0, 300 * Time.deltaTime));
    }
}