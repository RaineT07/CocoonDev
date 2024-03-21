using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loadingscreenbar : MonoBehaviour
{
    public Image progressBar;
    void Start()
    {
        StartCoroutine(LoadAsyncOperation());
        
        
    }

    IEnumerator LoadAsyncOperation() {

        AsyncOperation levelProgress = SceneManager.LoadSceneAsync(1);

        while (levelProgress.progress<1 ) {
           progressBar.fillAmount=levelProgress.progress;
            yield return new WaitForEndOfFrame();
        
        }
        yield return new WaitForEndOfFrame();
    }
}
