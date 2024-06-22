using System.Collections;
using TMPro;
using UnityEngine;

public class LoadingTextScript : MonoBehaviour
{
    public TMP_Text loadingText;

    IEnumerator LoadingCoroutine()
    {
        while (true)
        {
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(1f);
                loadingText.text += ".";
            }
            yield return new WaitForSeconds(1f);
            loadingText.text = "Loading";
        }
    }

    void Start()
    {
        loadingText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(LoadingCoroutine());
    }
}