using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SS.View;

public class GAMEController : Controller
{
    public const string GAME_SCENE_NAME = "GAME";
    public static GAMEController instance;

    const string LEVEL_TEXT = "LEVEL {0}";

    //[SerializeField] GameObject m_PlaySoundIcon;
    //[SerializeField] GameObject m_MuteSoundIcon;

    [SerializeField] TextMeshProUGUI levetext;

    //bool isPlaySound;

    public override string SceneName()
    {
        return GAME_SCENE_NAME;
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        levetext.text = string.Format(LEVEL_TEXT, GameManager.isCompleteLevel ? GameManager.indexStageComplete : PlayerPrefs.GetInt("SaveIdStage", 1));

        //isPlaySound = PlayerPrefs.GetInt("Sound", 1) == 1;
        //AudioListener.volume = PlayerPrefs.GetInt("Sound", 1);
        //m_PlaySoundIcon.SetActive(isPlaySound);
        //m_MuteSoundIcon.SetActive(!isPlaySound);
    }

    public void ReplayGame()
    {
        GameManager.countDieShowSkip += 1;
        if (!GameManager.isCompleteLevel)
        {
            PlayerPrefs.SetInt("SaveIdStage", GameManager.instance.indexStage);
        }
        Manager.Load(GAME_SCENE_NAME);
    }

    // ------  button Test Game  ------------
    public void TestResetGame()
    {
        PlayerPrefs.DeleteAll();
        Manager.Load(GAME_SCENE_NAME);
    }

    public void TestNextGame()
    {
        if (!GameManager.isCompleteLevel)
        {
            GameManager.instance.indexStage += 1;
            PlayerPrefs.SetInt("SaveIdStage", GameManager.instance.indexStage);
        }
        else
        {
            GameManager.indexStageComplete += 1;
        }

        Manager.Load(GAME_SCENE_NAME);
    }

    public void TestGoBackLevel()
    {
        if (!GameManager.isCompleteLevel)
        {
            GameManager.instance.indexStage -= 1;
            PlayerPrefs.SetInt("SaveIdStage", GameManager.instance.indexStage);
        }
        else if (GameManager.indexStageComplete >= 1)
        {
            GameManager.indexStageComplete -= 1;
        }
        Manager.Load(GAME_SCENE_NAME);
    }

    // --------------------------------------
    //public void SetSound()
    //{
    //    if (!isPlaySound)
    //    {
    //        AudioListener.volume = 1;
    //        PlayerPrefs.SetInt("Sound", 1);
    //        isPlaySound = true;
    //    }
    //    else
    //    {
    //        AudioListener.volume = 0;
    //        PlayerPrefs.SetInt("Sound", 0);
    //        isPlaySound = false;
    //    }

    //    m_PlaySoundIcon.SetActive(isPlaySound);
    //    m_MuteSoundIcon.SetActive(!isPlaySound);
    //    SoundManager.instance.audioSound.PlayOneShot(SoundManager.instance.buttonSound);
    //    PlayerPrefs.Save();
    //}
}