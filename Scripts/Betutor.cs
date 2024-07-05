using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Betutor : MonoBehaviour
{

    [SerializeField] private AudioClip Sounds1;
    [SerializeField] private AudioClip Sounds2;
    private AudioSource sounds1AudioSource;
    private AudioSource sounds2AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        sounds1AudioSource = gameObject.AddComponent<AudioSource>();
        sounds2AudioSource = gameObject.AddComponent<AudioSource>();

        sounds1AudioSource.clip = Sounds1;
        sounds2AudioSource.clip = Sounds2;

        sounds1AudioSource.volume = 0.5f;
        sounds2AudioSource.volume = 0.5f;

        StartCoroutine(PlaySounds());
    }

    IEnumerator PlaySounds()
    {
        yield return new WaitForSeconds(1.5f);
        sounds1AudioSource.Play(); // เล่นเสียง Sounds1

        while (sounds1AudioSource.isPlaying) // รอให้เสียง Sounds1 เล่นจบ
        {
            yield return null;
        }
        sounds2AudioSource.Play(); // เล่นเสียง Sounds2
    }

    bool Notutor()
    {
        return Input.GetButtonDown("PS4L1");
    }

    bool Tutor()
    {
        return Input.GetButtonDown("PS4R1");
    }


    // Update is called once per frame
    void Update()
    {
        if (Notutor() || Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene("Stage1");
        }

        if (Tutor() || Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("Tutor");
        }
    }
}
