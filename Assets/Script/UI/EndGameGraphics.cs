using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EndGameGraphics : MonoBehaviour
{
    [SerializeField] private Button goToMainMenuButton;
    [SerializeField] private Image bgImage;
    [SerializeField] private TextMeshProUGUI endGameText;

    private static Sprite staticBgImage;
    private static string staticEndGameText;

    public static void Open(Sprite bgImage, string endGameText)
    {
        SceneManager.LoadScene(GameConstants.EndGameScene);
        staticBgImage = bgImage;
        staticEndGameText = endGameText;
    }

    private void Awake()
    {
        bgImage.sprite = staticBgImage;
        endGameText.text = staticEndGameText;
        goToMainMenuButton.onClick.AddListener(OnMenuButtonClicked);
    }

    private void OnMenuButtonClicked()
    {
        SceneManager.LoadScene(GameConstants.OpenSceneName);
    }
}
