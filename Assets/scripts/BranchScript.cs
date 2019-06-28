using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BranchScript : MonoBehaviour
{
    [SerializeField] GameObject[] gameObjects;
    [SerializeField] private string nextScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
            for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i] != null)
            {
                return;
            }
        }
        SceneManager.LoadScene(nextScene);
    }
}
