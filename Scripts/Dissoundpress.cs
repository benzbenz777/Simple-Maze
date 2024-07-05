using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissoundpress : MonoBehaviour
{
    public List<AudioClip> gunshotSounds;
    private AudioSource audioSource;
    private int ckfinish = 0;
    private int cksound = 0;

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
        yield return null;
    }

    private void Update()
    {
        if (
            cksound == 0 && Input.GetKeyDown(KeyCode.Q) && ckfinish == 0
            || cksound == 0 && CheckDisSound() && ckfinish == 0
        )
        {
            // Play the gunshot sound
            StartCoroutine(Range4(cksound));
        }
        else if (
            cksound == 1 && Input.GetKeyDown(KeyCode.Q) && ckfinish == 0
            || cksound == 1 && CheckDisSound() && ckfinish == 0
        )
        {
            StartCoroutine(Range4(cksound));
        }
        else if (
            cksound == 2 && Input.GetKeyDown(KeyCode.Q) && ckfinish == 0
            || cksound == 2 && CheckDisSound() && ckfinish == 0
        )
        {
            StartCoroutine(Range4(cksound));
        }
        else if (
            cksound == 3 && Input.GetKeyDown(KeyCode.Q) && ckfinish == 0
            || cksound == 3 && CheckDisSound() && ckfinish == 0
        )
        {
            StartCoroutine(Range4(cksound));
        }

        // Additional logic for other updates if needed
    }
}
