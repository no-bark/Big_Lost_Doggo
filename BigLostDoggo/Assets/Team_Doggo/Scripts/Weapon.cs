using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
  public Sprite wepsprite;
  public GameObject user;

  public float Damage;
  public float Knockback;
  public float Speed;
  public float Cooldown;
  public int _pindex;

  bool Attacking = false;

  public bool GetAttacking() { return Attacking; }

  public void Start()
  {
    if (Speed > Cooldown) Cooldown = Speed;
  }

  public GameObject AttackBasic(Vector3 attackVec)
  {
    //Create weapon
    //Animate weapon
    //Return the weapon in case anything needs it. 

    GameObject obj = new GameObject();

    obj.AddComponent<SpriteRenderer>();
    obj.GetComponent<SpriteRenderer>().sprite = wepsprite;

    obj.transform.parent = user.transform;
    obj.transform.localPosition = new Vector3(0, 0, 0);

    obj.transform.localPosition += attackVec * 0.1f;
    StartCoroutine(NoAnim(obj.transform, attackVec));

    return obj;
  }

  // Impuse weapon in direction.
  IEnumerator NoAnim(Transform target, Vector3 dir)
  {
    Attacking = true;
    print("Attacking.");

    Vector3 diff = dir - target.localPosition;
    diff.Normalize();

    float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
    target.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

    yield return new WaitForSeconds(Speed);

    Destroy(target.gameObject);

    yield return new WaitForSeconds(Cooldown - Speed);

    Attacking = false;
    print("Can Attack Again.");
  }
}
