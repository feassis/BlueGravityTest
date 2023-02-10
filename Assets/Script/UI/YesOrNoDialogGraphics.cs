using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class YesOrNoDialogGraphics : MonoBehaviour
{
    [SerializeField] private Button yesButton; 
    [SerializeField] private Button noButton;
    [SerializeField] private TextMeshProUGUI dialogTextMesh;

    private Action yesAction;
    private Action noAction;

    private void Awake()
    {
        yesButton.onClick.AddListener(OnYesButtonClicked);
        noButton.onClick.AddListener(OnNoButtonClicked);
    }

    public void Open(Action onYesAction, Action onNoAction, string dialogText)
    {
        gameObject.SetActive(true);
        yesAction = onYesAction;
        noAction = onNoAction;
        dialogTextMesh.text = dialogText;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    private void OnYesButtonClicked()
    {
        Close();
        yesAction?.Invoke();
    }

    private void OnNoButtonClicked()
    {
        Close();
        noAction?.Invoke();
    }

    private void ResetActions()
    {
        yesAction = null;
        noAction = null;
    }
}
