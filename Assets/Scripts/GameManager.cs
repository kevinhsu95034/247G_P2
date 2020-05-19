using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public string currentScene;
    public Image fade;
    public Transform playerTransform;

    private List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    private void Awake()
    {
        instance = this;
        SceneManager.LoadSceneAsync(currentScene, LoadSceneMode.Additive);
        fade.color = Color.clear;
    }

    public void LoadScene(string to)
    {
        StartCoroutine(TransitionToScene(to));
    }

    public void StaticLoadScene(string to) {
        GameManager.instance.LoadScene(to);
    }

    public IEnumerator TransitionToScene(string to)
    {
        float a = 0;
        while (a < 1) {
            a += Time.deltaTime;
            fade.color = Color.Lerp(Color.clear, Color.black, a);
            yield return null;
        }

        if(currentScene == "Boat1") { 
            
        }

        scenesLoading.Add(SceneManager.UnloadSceneAsync(currentScene));
        scenesLoading.Add(SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive));

        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                yield return null;
            }
        }
        currentScene = to;
        while (a > 0)
        {
            a -= Time.deltaTime;
            fade.color = Color.Lerp(Color.clear, Color.black, a);
            yield return null;
        }
    }
}
