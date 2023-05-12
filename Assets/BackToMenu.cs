using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(45);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
