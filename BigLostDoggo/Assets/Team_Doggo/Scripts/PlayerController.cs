using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{

  public int playerIndex = 0;
  public float maxSpeed = 2f;
  public float moveForce = 5f;
  public AnimationCurve accelerationMod;
  public AnimationCurve decelerationMod;

  CharacterController2D1 _controller;
  bool controls_frozen = false;
  public bool controls_frozen_perm = false;


  public GameObject EquippedWeapon;
  GameObject CurrentWep;

  // Use this for initialization
  void Start()
  {
    Camera.main.SendMessage("AddObject", gameObject);
    _controller = GetComponent<CharacterController2D1>();

    EquipWeapon(EquippedWeapon);
  }

  void EquipWeapon(GameObject eqi)
  {
    if (CurrentWep != null && CurrentWep.GetComponent<Weapon>().GetAttacking()) return;

    if (CurrentWep != null) Destroy(CurrentWep);
    EquippedWeapon = eqi;
    CurrentWep = Instantiate(EquippedWeapon);

    var wep = CurrentWep.GetComponent<Weapon>();
    wep.user = gameObject;
    wep._pindex = playerIndex;
  }

  // Will call bulk functions
  void Update()
  {
    CombatInput();
    MovementInput();
  }

  List<KeyCode> keyQueue = new List<KeyCode>();

  // Checks for combat inputs
  void CombatInput()
  {
    // Used to prevent some weird "key priority" cases
    bool dirlock = false;
    Vector2 aimDir = new Vector2(0, 0);

    // Queue the down arrow
    if (Input.GetKeyDown(KeyCode.DownArrow))
    {
      dirlock = true;

      keyQueue.Insert(0, KeyCode.DownArrow);
    }
    if (Input.GetKeyUp(KeyCode.DownArrow)) keyQueue.Remove(KeyCode.DownArrow);

    // Queue the up Arrow
    if (!dirlock && Input.GetKeyDown(KeyCode.UpArrow))
    {
      dirlock = true;

      keyQueue.Insert(0, KeyCode.UpArrow);
    }
    if (Input.GetKeyUp(KeyCode.UpArrow)) keyQueue.Remove(KeyCode.UpArrow);

    // Queue the right arrow
    if (!dirlock && Input.GetKeyDown(KeyCode.RightArrow))
    {
      dirlock = true;

      keyQueue.Insert(0, KeyCode.RightArrow);
    }
    if (Input.GetKeyUp(KeyCode.RightArrow)) keyQueue.Remove(KeyCode.RightArrow);

    // Queue the down arrow
    if (!dirlock && Input.GetKeyDown(KeyCode.LeftArrow))
    {
      dirlock = true;

      keyQueue.Insert(0, KeyCode.LeftArrow);
    }
    if (Input.GetKeyUp(KeyCode.LeftArrow)) keyQueue.Remove(KeyCode.LeftArrow);

    // Attack
    if (Input.GetKeyDown(KeyCode.Space))
    {
      switch (keyQueue[0])
      {
        case KeyCode.DownArrow:
          {
            aimDir = new Vector2(0, -1);
            break;
          }
        case KeyCode.UpArrow:
          {
            aimDir = new Vector2(0, 1);
            break;
          }
        case KeyCode.RightArrow:
          {
            aimDir = new Vector2(1, 0);
            break;
          }
        case KeyCode.LeftArrow:
          {
            aimDir = new Vector2(-1, 0);
            break;
          }
        default:
          {
            aimDir = new Vector2(0, 0);
            break;
          }
      }
      
      CurrentWep.SendMessage("Attack", (Vector3)aimDir, SendMessageOptions.RequireReceiver);
    }
  }

  //Does movement input
  void MovementInput()
  {
    var velocity = _controller.velocity;

    velocity = DoKnockbackChanges(velocity);
    velocity = DoInputChanges(velocity);

    if (Input.GetKeyDown(KeyCode.M))
    {
      Knockback(3, new Vector3(0, 0, 0));
    }

    _controller.move(velocity * Time.deltaTime);
  }

  // Change move vector based on inputs
  Vector3 DoInputChanges(Vector3 velocity)
  {
    if (controls_frozen || controls_frozen_perm) return velocity;

    Vector2 movex = new Vector2(0, 0);
    Vector2 movey = new Vector2(0, 0);

    bool moving = false;
    bool movingx = false;
    bool movingy = false;

    if (Input.GetKey(KeyCode.W))
    {
      movey += new Vector2(0, 1);
      moving = true;
      movingy = true;
    }
    if (Input.GetKey(KeyCode.A))
    {
      movex += new Vector2(-1, 0);
      moving = true;
      movingx = true;
    }
    if (Input.GetKey(KeyCode.S))
    {
      movey += new Vector2(0, -1);
      moving = true;
      movingy = true;
    }
    if (Input.GetKey(KeyCode.D))
    {
      movex += new Vector2(1, 0);
      moving = true;
      movingx = true;
    }

    // If we're moving
    if (moving)
    {
      if (movingx) velocity.x = AccelerateX(new Vector2(movex.x, movey.y)).x * moveForce;
      if (movingy) velocity.y = AccelerateY(new Vector2(movex.x, movey.y)).y * moveForce;
    }

    // DECELERATE
    if (!movingx)
    {
      movex = new Vector2(Input.GetAxis("Horizontal"), 0);
      movey = new Vector2(0, Input.GetAxis("Vertical"));

      float decelx = decelerationMod.Evaluate(Input.GetAxis("Horizontal"));

      velocity.x = Vector3.Normalize(new Vector3(Vector3.Normalize(movex).x, Vector3.Normalize(movey).y)).x * decelx * moveForce;
    }

    if (!movingy)
    {
      movex = new Vector2(Input.GetAxis("Horizontal"), 0);
      movey = new Vector2(0, Input.GetAxis("Vertical"));

      float decely = decelerationMod.Evaluate(Input.GetAxis("Vertical"));

      velocity.y = Vector3.Normalize(new Vector3(Vector3.Normalize(movex).x, Vector3.Normalize(movey).y)).y * decely * moveForce;
    }

    if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
    {
      // Animation: Idle
    }

    return velocity;
  }

  Vector3 AccelerateX(Vector3 vec)
  {
    float accelx = accelerationMod.Evaluate(Input.GetAxis("Horizontal"));

    return new Vector3((Vector3.Normalize(new Vector3(Vector3.Normalize(vec).x, Vector3.Normalize(vec).y))).x * accelx,
                       (Vector3.Normalize(new Vector3(Vector3.Normalize(vec).x, Vector3.Normalize(vec).y))).y);
  }

  Vector3 AccelerateY(Vector3 vec)
  {
    float accely = accelerationMod.Evaluate(Input.GetAxis("Vertical"));

    return new Vector3((Vector3.Normalize(new Vector3(Vector3.Normalize(vec).x, Vector3.Normalize(vec).y))).x,
                       (Vector3.Normalize(new Vector3(Vector3.Normalize(vec).x, Vector3.Normalize(vec).y))).y * accely);
  }

  bool Knocked = false;
  // Change move vector based on knockback
  Vector3 DoKnockbackChanges(Vector3 velocity)
  {
    if (Knocked)
    {
      Knocked = false;
      velocity = StoredKnockback;
    }
    else
    {
      velocity -= StoredKnockback * (Time.deltaTime * 10);
    }

    StoredKnockback.x -= Time.deltaTime * 10;
    StoredKnockback.y -= Time.deltaTime * 10;

    if (StoredKnockback.x <= 0) StoredKnockback.x = 0;
    if (StoredKnockback.y <= 0) StoredKnockback.y = 0;

    return velocity;
  }


  // Store a knockback upon being hit
  Vector3 StoredKnockback;
  void Knockback(float force, Vector3 source)
  {
    Knocked = true;
    IEnumerator ko = freezeControls(0.3f);
    StartCoroutine(ko);

    source = gameObject.transform.position - source;

    source.Normalize();
    source *= force;

    StoredKnockback = source;
  }

  void OnTakeKnockback(DamagePacket pack)
  {
    print("IMAFUKBOI");
    Knockback(pack._knockback_value, pack._source.transform.position);
  }

  IEnumerator freezeControls(float time)
  {
    controls_frozen = true;
    yield return new WaitForSeconds(time);
    controls_frozen = false;
  }
}
