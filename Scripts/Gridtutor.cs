using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gridtutor : MonoBehaviour
{
    [SerializeField]
    private AudioClip footstepSoundClip;

    [SerializeField]
    private AudioClip wallHitSoundClip;
    private AudioSource footstepAudioSource;
    private AudioSource wallHitAudioSource;

    [SerializeField]
    private AudioClip midtu1Clip;

    [SerializeField]
    private AudioClip midtu2Clip;

    [SerializeField]
    private AudioClip midtu3Clip;

    [SerializeField]
    private AudioClip midtu4Clip;

    [SerializeField]
    private AudioClip midtu5Clip;

    [SerializeField]
    private AudioClip midtu6Clip;

    [SerializeField]
    private AudioClip midtu7Clip;

    [SerializeField]
    private AudioClip midtu8Clip;

    [SerializeField]
    private AudioClip midtu9Clip;

    [SerializeField]
    private AudioClip midtu10Clip;

    [SerializeField]
    private AudioClip midtu11Clip;

    [SerializeField]
    private AudioClip midtu12Clip;

    [SerializeField]
    private AudioClip midtu13Clip;

    [SerializeField]
    private AudioClip midtu14Clip;

    [SerializeField]
    private AudioClip midtu15Clip;
    private AudioSource midtu1AudioSource;
    private AudioSource midtu2AudioSource;
    private AudioSource midtu3AudioSource;
    private AudioSource midtu4AudioSource;
    private AudioSource midtu5AudioSource;
    private AudioSource midtu6AudioSource;
    private AudioSource midtu7AudioSource;
    private AudioSource midtu8AudioSource;
    private AudioSource midtu9AudioSource;
    private AudioSource midtu10AudioSource;
    private AudioSource midtu11AudioSource;
    private AudioSource midtu12AudioSource;
    private AudioSource midtu13AudioSource;
    private AudioSource midtu14AudioSource;
    private AudioSource midtu15AudioSource;

    [SerializeField]
    private bool isRepeatedMovement = false;

    [SerializeField]
    private float moveDuration = 0.1f;

    [SerializeField]
    private float gridSize = 1f;

    [SerializeField]
    private LayerMask whatStopMovement;
    public static int whits = 0;
    public static int ckwalk = 0;
    public static int ckl1 = 0;
    private bool ckwalkb = false;
    private bool ckhitb = false;
    public bool l1ck = false;
    public bool afl1ck = false;
    public bool aflfin = false;
    public bool ckfwall = false;
    private bool isMoving = false;

    private void Start()
    {
        resetall();
        // สร้าง AudioSource สำหรับ footstep และ WallHits
        footstepAudioSource = gameObject.AddComponent<AudioSource>();
        wallHitAudioSource = gameObject.AddComponent<AudioSource>();

        // โหลดเสียง footstep และ WallHits ลงใน AudioSource
        footstepAudioSource.clip = footstepSoundClip;
        wallHitAudioSource.clip = wallHitSoundClip;

        // ตั้งค่าระดับเสียงตามต้องการ
        footstepAudioSource.volume = 0.5f; // เสียง footstep 50%
        wallHitAudioSource.volume = 0.6f; // เสียง WallHits 60%

        midtu1AudioSource = gameObject.AddComponent<AudioSource>();
        midtu2AudioSource = gameObject.AddComponent<AudioSource>();
        midtu3AudioSource = gameObject.AddComponent<AudioSource>();
        midtu4AudioSource = gameObject.AddComponent<AudioSource>();
        midtu5AudioSource = gameObject.AddComponent<AudioSource>();
        midtu6AudioSource = gameObject.AddComponent<AudioSource>();
        midtu7AudioSource = gameObject.AddComponent<AudioSource>();
        midtu8AudioSource = gameObject.AddComponent<AudioSource>();
        midtu9AudioSource = gameObject.AddComponent<AudioSource>();
        midtu10AudioSource = gameObject.AddComponent<AudioSource>();
        midtu11AudioSource = gameObject.AddComponent<AudioSource>();
        midtu12AudioSource = gameObject.AddComponent<AudioSource>();
        midtu13AudioSource = gameObject.AddComponent<AudioSource>();
        midtu14AudioSource = gameObject.AddComponent<AudioSource>();
        midtu15AudioSource = gameObject.AddComponent<AudioSource>();

        midtu1AudioSource.clip = midtu1Clip;
        midtu2AudioSource.clip = midtu2Clip;
        midtu3AudioSource.clip = midtu3Clip;
        midtu4AudioSource.clip = midtu4Clip;
        midtu5AudioSource.clip = midtu5Clip;
        midtu6AudioSource.clip = midtu6Clip;
        midtu7AudioSource.clip = midtu7Clip;
        midtu8AudioSource.clip = midtu8Clip;
        midtu9AudioSource.clip = midtu9Clip;
        midtu10AudioSource.clip = midtu10Clip;
        midtu11AudioSource.clip = midtu11Clip;
        midtu12AudioSource.clip = midtu12Clip;
        midtu13AudioSource.clip = midtu13Clip;
        midtu14AudioSource.clip = midtu14Clip;
        midtu15AudioSource.clip = midtu15Clip;

        midtu1AudioSource.volume = 0.5f;
        midtu2AudioSource.volume = 0.5f;
        midtu3AudioSource.volume = 0.5f;
        midtu4AudioSource.volume = 0.5f;
        midtu5AudioSource.volume = 0.5f;
        midtu6AudioSource.volume = 0.5f;
        midtu7AudioSource.volume = 0.5f;
        midtu8AudioSource.volume = 0.5f;
        midtu9AudioSource.volume = 0.5f;
        midtu10AudioSource.volume = 0.5f;
        midtu11AudioSource.volume = 0.5f;
        midtu12AudioSource.volume = 0.5f;
        midtu13AudioSource.volume = 0.5f;
        midtu14AudioSource.volume = 0.5f;
        midtu15AudioSource.volume = 0.5f;

        Time.timeScale = 0f;
        StartCoroutine(PlaySoundsSequence());
    }

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

    bool L1()
    {
        return Input.GetButtonDown("PS4L1");
    }

    private void Update()
    {
        // Only process on move at a time.
        if (!isMoving)
        {
            // Accomodate two different types of moving.
            System.Func<KeyCode, bool> inputFunction;
            if (isRepeatedMovement)
            {
                // GetKey repeatedly fires.
                inputFunction = Input.GetKey;
            }
            else
            {
                // GetKeyDown fires once per keypress
                inputFunction = Input.GetKeyDown;
            }

            // If the input function is active, move in the appropriate direction.
            if (inputFunction(KeyCode.UpArrow) || CheckUp())
            {
                StartCoroutine(Move(Vector2.up));
                ckwalk++;
            }
            else if (inputFunction(KeyCode.DownArrow) || CheckDown())
            {
                StartCoroutine(Move(Vector2.down));
                ckwalk++;
            }
            else if (inputFunction(KeyCode.LeftArrow) || CheckLeft())
            {
                StartCoroutine(Move(Vector2.left));
                ckwalk++;
            }
            else if (inputFunction(KeyCode.RightArrow) || CheckRight())
            {
                StartCoroutine(Move(Vector2.right));
                ckwalk++;
            }

            if (ckwalk == 7)
            {
                ckwalkb = true;
                if (ckwalkb == true)
                {
                    StartCoroutine(PlaySoundsSequence2());
                    ckwalkb = false;
                    ckwalk++;
                    l1ck = true;
                    Dissoundtutor Checkbtn = FindObjectOfType<Dissoundtutor>();
                    if (Checkbtn != null)
                    {
                        Checkbtn.Checkbtn(l1ck);
                    }
                }
            }

            if (whits == 1)
            {
                ckhitb = true;
                if (ckhitb = true)
                {
                    StartCoroutine(PlaySoundsSequence3());
                    ckhitb = false;
                    whits++;
                }
            }

            if (l1ck == true && Input.GetKeyDown(KeyCode.Q) || l1ck == true && L1())
            {
                StartCoroutine(PlaySoundsSequence4());
                l1ck = false;
                afl1ck = true;
            }

            if (afl1ck == true)
            {
                StartCoroutine(PlaySoundsSequence9());
                afl1ck = false;
                aflfin = true;
            }
            if (aflfin == true)
            {
                StartCoroutine(PlaySoundsSequence5());
                StartCoroutine(PlaySoundsSequence6());
                StartCoroutine(PlaySoundsSequence7());
                aflfin = false;
            }
        }
    }

    // ทำให้เดิน สมูต และ เช็ค Hitbox กำแพง เพื่อให้หยุดห
    private IEnumerator Move(Vector2 direction)
    {
        isMoving = true;
        Vector2 startPosition = transform.position;
        Vector2 endPosition = startPosition + (direction * gridSize);
        Collider2D obstacle = Physics2D.OverlapCircle(endPosition, gridSize / 2f, whatStopMovement);

        if (obstacle != null)
        {
            wallHitAudioSource.Play();
            isMoving = false;
            //========================================================//
            whits++;
            ckfwall = true;
            Showdirecttr Checkwll = FindObjectOfType<Showdirecttr>();
            if (Checkwll != null)
            {
                Checkwll.ReceiveTTWall(ckfwall);
            }
            ckfwall = false;
            yield break; // ออกจาก coroutine ทันที
        }

        // Smoothly move in the desired direction taking the required time.
        float elapsedTime = 0;
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float percent = elapsedTime / moveDuration;
            transform.position = Vector2.Lerp(startPosition, endPosition, percent);
            yield return null;
        }

        // ทำให้แน่ใจว่า Player อยู่ที่ตำแหน่ง ที่เดินไปล่าสุด
        transform.position = endPosition;

        // เล่นเสียงเท้า
        footstepAudioSource.Play();

        // ไม่ให้เดิน เพื่อรอ input
        isMoving = false;
    }

    private IEnumerator PlaySoundsSequence()
    {
        midtu1AudioSource.Play(); // เล่นเสียง midtu1Clip
        while (midtu1AudioSource.isPlaying) // รอให้เสียง finish1Clip เล่นจบ
        {
            yield return null;
        }

        midtu2AudioSource.Play();
        while (midtu2AudioSource.isPlaying) // รอให้เสียง finish2Clip เล่นจบ
        {
            yield return null;
        } // เล่นเสียง finish2Clip

        midtu3AudioSource.Play();
        while (midtu3AudioSource.isPlaying) // รอให้เสียง finish3Clip เล่นจบ
        {
            yield return null;
        }

        midtu4AudioSource.Play();
        while (midtu4AudioSource.isPlaying)
        {
            yield return null;
        }
        midtu5AudioSource.Play(); // เล่นเสียง midtu1Clip
        while (midtu5AudioSource.isPlaying) // รอให้เสียง finish1Clip เล่นจบ
        {
            yield return null;
        }
        Time.timeScale = 1f;
    }

    private IEnumerator PlaySoundsSequence2()
    {
        yield return new WaitForSeconds(1.0f);
        Time.timeScale = 0f;
        midtu6AudioSource.Play();
        while (midtu6AudioSource.isPlaying) // รอให้เสียง finish2Clip เล่นจบ
        {
            yield return null;
        }
        Time.timeScale = 1f;
    }

    private IEnumerator PlaySoundsSequence3()
    {
        yield return new WaitForSeconds(1.0f);
        Time.timeScale = 0f;
        midtu7AudioSource.Play();
        while (midtu7AudioSource.isPlaying) // รอให้เสียง finish3Clip เล่นจบ
        {
            yield return null;
        }
        Time.timeScale = 1f;
    }

    private IEnumerator PlaySoundsSequence4()
    {
        yield return new WaitForSeconds(1.0f);
        Time.timeScale = 0f;
        midtu8AudioSource.Play();
        while (midtu8AudioSource.isPlaying)
        {
            yield return null;
        }
        Time.timeScale = 1f;
    }

    private IEnumerator PlaySoundsSequence5()
    {
        yield return new WaitForSeconds(2.75f);
        Time.timeScale = 0f;
        midtu10AudioSource.Play();
        while (midtu10AudioSource.isPlaying)
        {
            yield return null;
        }
        Time.timeScale = 1f;
    }

    private IEnumerator PlaySoundsSequence6()
    {
        yield return new WaitForSeconds(3.25f);
        Time.timeScale = 0f;
        midtu11AudioSource.Play();
        while (midtu11AudioSource.isPlaying)
        {
            yield return null;
        }
        Time.timeScale = 1f;
    }

    private IEnumerator PlaySoundsSequence7()
    {
        yield return new WaitForSeconds(3.75f);
        Time.timeScale = 0f;
        midtu12AudioSource.Play();
        while (midtu12AudioSource.isPlaying)
        {
            yield return null;
        }
        Time.timeScale = 1f;
    }

    private IEnumerator PlaySoundsSequence8()
    {
        yield return new WaitForSeconds(4.25f);
        Time.timeScale = 0f;
        midtu13AudioSource.Play();
        while (midtu13AudioSource.isPlaying)
        {
            yield return null;
        }
        Time.timeScale = 1f;
    }

    private IEnumerator PlaySoundsSequence9()
    {
        yield return new WaitForSeconds(2.25f);
        Time.timeScale = 0f;
        midtu9AudioSource.Play();
        while (midtu9AudioSource.isPlaying)
        {
            yield return null;
        }
        Time.timeScale = 1f;
    }

    private IEnumerator PlaySoundsSequence10()
    {
        yield return new WaitForSeconds(1.0f);
        Time.timeScale = 0f;
        midtu14AudioSource.Play();
        while (midtu14AudioSource.isPlaying)
        {
            yield return null;
        }
        midtu15AudioSource.Play();
        while (midtu15AudioSource.isPlaying)
        {
            yield return null;
        }
        Time.timeScale = 1f;
    }

    void resetall(){
    whits = 0;
    ckwalk = 0;
    ckl1 = 0;
    ckwalkb = false;
    ckhitb = false;
    l1ck = false;
    afl1ck = false;
    aflfin = false;
    ckfwall = false;
    isMoving = false;
    }
}
