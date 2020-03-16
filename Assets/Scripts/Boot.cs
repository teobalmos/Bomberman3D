using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boot : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(bootCoroutine());
        
        Debug.Log($"instance: {MenuManager.instance}");
    }

    private IEnumerator bootCoroutine()
    {
        SceneManager.LoadScene("GameMain", LoadSceneMode.Additive);
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Additive);
        yield return null;
        MenuManager.instance.ChangeMenu<TitleMenuScript>();
    }
}
