using UnityEngine;
using System.Collections;

public class MeleeWeapon : Weapon
{
  public Vector2 WeaponSize;

  void Attack(Vector3 attackVec)
  {
    //Don't attack twice
    if (GetAttacking()) return;

    var attack = AttackBasic(attackVec);

    if (attack != null)
    {
      attack.transform.localScale = WeaponSize;

      attack.AddComponent<BoxCollider2D>();
      attack.AddComponent<MeleeDetector>();
      attack.AddComponent<Rigidbody2D>();

      attack.GetComponent<MeleeDetector>()._user = user;
      attack.GetComponent<MeleeDetector>()._pindex = _pindex;
      attack.GetComponent<MeleeDetector>()._weapon = this;
      attack.GetComponent<BoxCollider2D>().isTrigger = true;
      attack.GetComponent<Rigidbody2D>().isKinematic = true;

      attack.layer = LayerMask.NameToLayer("Weapon");
    }
  }
}
