using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string m_sceneName;
    
    public void LoadSceneHandle()
    {
        SceneManager.LoadScene(m_sceneName, LoadSceneMode.Single);
    }
}
