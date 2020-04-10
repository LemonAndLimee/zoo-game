using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManagement : MonoBehaviour
{
    public bool isNew;
    public bool isLoad;

    

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void New()
    {
        isNew = true;
        SceneManager.LoadSceneAsync(1);
    }

    public void Load()
    {
        isLoad = true;
        SceneManager.LoadSceneAsync(1);
    }
}
