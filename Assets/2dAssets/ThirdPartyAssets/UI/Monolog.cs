using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Monolog : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI monologText;

    public void SetupMonolog(string text, float duration)
    {
        monologText.text = text;

        StartCoroutine(StartMonologDestruction(duration));
    }

    private IEnumerator StartMonologDestruction(float duration)
    {
        yield return new WaitForSeconds(duration);

        Destroy(gameObject);
    }
}
