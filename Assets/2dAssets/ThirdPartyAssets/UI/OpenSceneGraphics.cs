using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpenSceneGraphics : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button goToIntroButton;
    [SerializeField] private Transform introduction;

    private void Awake()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked);
        goToIntroButton.onClick.AddListener(OnGoToIntroButtonClicked);
    }

    private void OnPlayButtonClicked()
    {
        SceneManager.LoadScene(GameConstants.GameSceneName);
    }
    
    private void OnGoToIntroButtonClicked()
    {
        introduction.gameObject.SetActive(true);
    }
}
