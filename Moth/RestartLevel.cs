using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetLevelFunction();
        }
    }

    void ResetLevelFunction()
    {
       
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        
        SceneManager.LoadScene(currentSceneIndex);
    }
}