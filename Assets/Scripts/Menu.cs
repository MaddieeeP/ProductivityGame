using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static Menu Instance;
    public GameObject menuButtons;
    public UIElementAnimator circle;

    public void Awake()
    {
        Instance = this;
        menuButtons.SetActive(true);
    }

    public static void crow()
    {
        PlayerPrefs.SetInt("Score", 0);
        SceneManager.LoadScene("AngryCrow");
    }
    public static void edit()
    {
        SceneManager.LoadSceneAsync("QuestionEditor", LoadSceneMode.Additive);
        Instance.menuButtons.SetActive(false);
        Instance.circle.NextState(new Vector3(-400f, 300f, 0f), new Vector3(1000f, 1000f, 1000f), Color.red, AnimationCurve.EaseInOut(0f, 0f, 1f, 1f), 10f, 100f, AnimationCurve.EaseInOut(0f, 0f, 1f, 1f), 0.5f, 3f);
    }
}
