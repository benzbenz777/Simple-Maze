using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bypass : MonoBehaviour
{
  bool bypassscence()
    {
        return Input.GetButtonDown("PS4L3");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)|| bypassscence()) 
        {
            SceneManager.LoadScene("MainMenu");
            
        }
    }
}
