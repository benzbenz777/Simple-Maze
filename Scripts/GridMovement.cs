using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridMovement : MonoBehaviour
{
  [SerializeField] private AudioClip footstepSoundClip;
  [SerializeField] private AudioClip wallHitSoundClip;
  private AudioSource footstepAudioSource;
  private AudioSource wallHitAudioSource;
  [SerializeField] private bool isRepeatedMovement = false;
  [SerializeField] private float moveDuration = 0.1f;
  [SerializeField] private float gridSize = 1f;
  [SerializeField] private LayerMask whatStopMovement;
  public static int ckwall = 0;
  public static int stepup = 0;
  public static int stepdown = 0;
  public static int stepleft = 0;
  public static int stepright = 0;
  public static int whits = 0;
  public static int stepupttl = 0;
  public static int stepdownttl = 0;
  public static int stepleftttl = 0;
  public static int steprightttl = 0;
  public static int whitsttl = 0;
  private bool bbcheck = true;
  private bool isMoving = false;
  public static bool flwall = false;


  private void Start()
  {
    // สร้าง AudioSource สำหรับ footstep และ WallHits
    footstepAudioSource = gameObject.AddComponent<AudioSource>();
    wallHitAudioSource = gameObject.AddComponent<AudioSource>();

    // โหลดเสียง footstep และ WallHits ลงใน AudioSource
    footstepAudioSource.clip = footstepSoundClip;
    wallHitAudioSource.clip = wallHitSoundClip;

    // ตั้งค่าระดับเสียงตามต้องการ
    footstepAudioSource.volume = 0.5f; // เสียง footstep 50%
    wallHitAudioSource.volume = 0.6f; // เสียง WallHits 60%
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

  public void Checkbtn(bool bcheck)
    {
        bbcheck = bcheck;
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
      if (bbcheck == true)
      {
        if (inputFunction(KeyCode.UpArrow) || CheckUp())
        {
          StartCoroutine(Move(Vector2.up));
          stepup++;
          stepupttl++;

        }
        else if (inputFunction(KeyCode.DownArrow) || CheckDown())
        {
          StartCoroutine(Move(Vector2.down));
          stepdown++;
          stepdownttl++;

        }
        else if (inputFunction(KeyCode.LeftArrow) || CheckLeft())
        {
          StartCoroutine(Move(Vector2.left));
          stepleft++;
          stepleftttl++;

        }
        else if (inputFunction(KeyCode.RightArrow) || CheckRight())
        {
          StartCoroutine(Move(Vector2.right));
          stepright++;
          steprightttl++;
        }
      }else
      {
        
      }
      stepcount stepCountScript = FindObjectOfType<stepcount>();
      if (stepCountScript != null)
      {
        stepCountScript.ReceiveStepCounts(stepup, stepdown, stepleft, stepright, whits);
      }

      Goal ReceiveStt = FindObjectOfType<Goal>();
      if (ReceiveStt != null)
      {
        ReceiveStt.ReceiveAllstep(stepup, stepdown, stepleft, stepright, whits);
      }

      //ส่ง ค่าสถิติ ไป TotalScore
      TotalScore SendStep = FindObjectOfType<TotalScore>();
      if (SendStep != null)
      {
        SendStep.RecieveSteps(stepupttl, stepdownttl, stepleftttl, steprightttl, whitsttl);
      }

      totalui totalsendhits = FindObjectOfType<totalui>();
      if (totalsendhits != null)
      {
        totalsendhits.ReceiveHits(whits);
      }

    }
  }
  // ทำให้เดิน สมูต และ เช็ค Hitbox กำแพง เพื่อให้หยุด
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
      whits++;
      whitsttl++;
      ckwall = 1;
      flwall = true;
      Direchits ReceiveCK = FindObjectOfType<Direchits>();
      if (ReceiveCK != null)
      {
        ReceiveCK.ReceiveCKWall(ckwall);
      }

      Showdirect ReceiveFKW = FindObjectOfType<Showdirect>();
      if (ReceiveFKW != null)
      {
        ReceiveFKW.ReceiveFKWall(flwall);
      }
      flwall = false;
      yield break; // ออกจาก coroutine ทันที
    }
  
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

  public void resetstep(int restep)
  {
    stepup = restep;
    stepdown = restep;
    stepleft = restep;
    stepright = restep;
    whits = restep;
  }
}

