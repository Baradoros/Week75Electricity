using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager manager;      // For other scripts to have access to GameManager methods

    void Start() {

        // Ensure there can only be one GameManager in a scene, prefer the oldest one
        if (manager != null) {
            Destroy(this);
        }
        else {
            manager = this;
            DontDestroyOnLoad(this);
        }
    }

    public void TeleportObject(GameObject obj, Vector3 position) {
        obj.transform.position = position;
    }

    private IEnumerator DelaySceneLoad(string scene, float time) {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(scene);

    }

    public void LoadScene(string scene, float time) {
        StartCoroutine(DelaySceneLoad(scene, time));
    }


    private IEnumerator DelayQuit(float time) {
        yield return new WaitForSeconds(time);
        Quit();
    }

    private void Quit() {
        #if UNITY_EDITOR                                                    // If we're in Unity Editor, stop play mode
            if (UnityEditor.EditorApplication.isPlaying == true)
                UnityEditor.EditorApplication.isPlaying = false;
        #else                                                               // If we are in a built application, quit to desktop
            Application.Quit();
        #endif
    }

    public void QuitToDesktop(float time) {
        StartCoroutine(DelayQuit(time));
    }
}
