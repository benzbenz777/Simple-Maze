using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Showdirect : MonoBehaviour
{
    public List<GameObject> imagesToShow;
    public Image walk;
    private int currentImageIndex = 0;
    private bool flkwall = false;

    bool CheckUp()
    {
        return Input.GetButtonDown("PS4Tri");
    }

    bool CheckDown()
    {
        return Input.GetButtonDown("PS4X");
    }

    bool CheckLeft()
    {
        return Input.GetButtonDown("PS4Squ");
    }

    bool CheckRight()
    {
        return Input.GetButtonDown("PS4O");
    }

    public void ReceiveFKWall(bool flwall)
    {
        flkwall = flwall;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || CheckUp())
        {
            ShowImage(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || CheckLeft())
        {
            ShowImage(2);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || CheckDown())
        {
            ShowImage(3);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || CheckRight())
        {
            ShowImage(4);
        }

        if (flkwall == true)
        {
            StartCoroutine(ChangeColorCoroutine()); // เริ่ม Coroutine เปลี่ยนสี
            flkwall = false; // กำหนด flkwall เป็น false หลังจากเริ่ม Coroutine
        }
    }

    IEnumerator ChangeColorCoroutine()
    {
        walk.color = Color.red; // เปลี่ยนสีเป็นแดง
        yield return new WaitForSeconds(0.2f); // รอเป็นเวลา 0.2 วินาที
        walk.color = Color.white; // เปลี่ยนสีเป็นขาว
    }

    void ShowImage(int index)
    {
        if (index >= 0 && index < imagesToShow.Count)
        {
            imagesToShow[currentImageIndex].SetActive(false);
            currentImageIndex = index;
            imagesToShow[currentImageIndex].SetActive(true);
        }
        else
        {
            Debug.LogWarning("Invalid image index.");
        }
    }
}
