using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direchits : MonoBehaviour
{
    private int ckcwall = 0;
    // Start is called before the first frame update

    public void ReceiveCKWall(int ckwall)
    {
        ckcwall = ckwall;
    }

    private void Update() {
        Debug.Log("Test :" + ckcwall);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (ckcwall == 1 && collision.gameObject.CompareTag("WLeft"))
        {
            Debug.Log("Player hit the left wall. Turn Left.");
            // Set ckwnum or perform other actions as needed
        }
        else if (ckcwall == 1 && collision.gameObject.CompareTag("WRight"))
        {
            Debug.Log("Player hit the right wall. Turn Right.");
            // Set ckwnum or perform other actions as needed
        }
        else if (ckcwall == 1 && collision.gameObject.CompareTag("WForW"))
        {
            Debug.Log("Player hit the forward wall. Move Forward.");
            // Set ckwnum or perform other actions as needed
        }
        else if (ckcwall == 1 && collision.gameObject.CompareTag("WBack"))
        {
            Debug.Log("Player hit the back wall. Move Back.");
            // Set ckwnum or perform other actions as needed
        }

        // Set ckcwall to 0 to wait for the next collision
        ckcwall = 0;


    }
}
