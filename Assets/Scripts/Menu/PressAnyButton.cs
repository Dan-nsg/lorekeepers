using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class PressAnyButton : MonoBehaviour
{
    public string nextSceneName;
    private bool inputDetected = false;

    public TextMeshProUGUI textToBlink;
    public float blinkInterval = 0.5f;
    private bool isTextColor1 = true;

    public Color color1 = Color.white;
    public Color color2 = Color.yellow;

    private void Start()
    {
        StartCoroutine(BlinkRoutine());
    }

    private void Update()
    {

        if (inputDetected)
            return;

        if (Input.anyKeyDown)
        {
            inputDetected = true;
            LoadNextScene();
        }
    }

    private IEnumerator BlinkRoutine()
    {
        while (true)
        {
            textToBlink.color = isTextColor1 ? color1 : color2;

            isTextColor1 = !isTextColor1;

            yield return new WaitForSeconds(blinkInterval);
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
