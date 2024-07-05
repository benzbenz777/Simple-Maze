using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceButtonSound : MonoBehaviour
{
    public List<AudioClip> gunshotSounds;
    private AudioSource audioSource;
    private int ckfinish = 0;
    private int cksound = 0;
    private bool ckbut = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    bool CheckDisSound()
    {
        return Input.GetButtonDown("PS4L1");
    }

    public void ReceiveCKSuv(int cknodis)
    {
        ckfinish = cknodis;
        Debug.Log("ckfinish :" + ckfinish);
    }

    public void PlaySound(int soundIndex)
    {
        Debug.Log("AudioClip is null at index " + soundIndex);
        if (soundIndex >= 0 && soundIndex < gunshotSounds.Count)
        {
            if (gunshotSounds[soundIndex] != null)
            {
                audioSource.PlayOneShot(gunshotSounds[soundIndex]);
            }
            else
            {
                Debug.LogError("AudioClip is null at index " + soundIndex);
            }
        }
        else
        {
            Debug.LogError("Invalid sound index: " + soundIndex);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("SoundRange"))
        {
            cksound = 1;
        }
        else if (other.CompareTag("SoundRange2"))
        {
            cksound = 2;
        }
        else if (other.CompareTag("SoundRange3"))
        {
            cksound = 3;
        }
        else
        {
            cksound = 0;
        }
    }

    IEnumerator Range4(int checksound)
    {
        if (!audioSource.isPlaying) // ตรวจสอบว่าเสียงกำลังไม่เล่นอยู่
        {
            if (checksound == 0)
            {
                PlaySound(0);
            }
            else if (checksound == 1)
            {
                PlaySound(1);
            }
            else if (checksound == 2)
            {
                PlaySound(2);
            }
            else if (checksound == 3)
            {
                PlaySound(3);
            }
            // รอเสียงเล่นจบ
            while (audioSource.isPlaying)
            {
                yield return null;
            }
     
        }
        // ถ้าเสียงกำลังเล่น ไม่ต้องทำอะไร
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && ckfinish == 0 || CheckDisSound() && ckfinish == 0)
        {
            // เมื่อกดปุ่ม Q หรือปุ่ม PS4L1 และ ckfinish เป็น 0
            ckbut = !ckbut; // สลับค่าของ ckbut ระหว่าง true และ false
        }
       
        if (ckfinish == 1)
        {
           audioSource.Stop(); //สั่งหยุด
        }

        if (ckbut)
        {
            // ถ้าปุ่มถูกกดอยู่
            if (cksound == 0)
            {
                StartCoroutine(Range4(cksound));
            }
            else if (cksound == 1)
            {
                StartCoroutine(Range4(cksound));
            }
            else if (cksound == 2)
            {
                StartCoroutine(Range4(cksound));
            }
            else if (cksound == 3)
            {
                StartCoroutine(Range4(cksound));
            }
        }
    }
}
