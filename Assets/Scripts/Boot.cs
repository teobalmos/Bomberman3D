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
        yield return SceneManager.LoadSceneAsync("GameMain", LoadSceneMode.Additive);
        yield return SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Additive);
        yield return SceneManager.LoadSceneAsync("Background", LoadSceneMode.Additive);
        MenuManager.instance.ChangeMenu<TitleMenuScript>();
    }
}
