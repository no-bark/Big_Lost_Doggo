using UnityEngine;
using System.Collections;

public class DamagePacket
{
  public DamagePacket(GameObject source = null, GameObject inflictor = null, float damage_value = 0, float knockback_value = 0)
  {
    _source = source;
    _inflictor = inflictor;

    _damage_value = damage_value;
    _knockback_value = knockback_value;
  }

  public GameObject _source; //The user doing it
  public GameObject _inflictor; //The thing doing it

  public float _damage_value;
  public float _knockback_value;
}

public class MeleeDetector : MonoBehaviour
{
  public GameObject _user;
  public MeleeWeapon _weapon;
  public int _pindex;

  void OnTriggerEnter2D(Collider2D col)
  {
    if (col.gameObject.GetComponent<PlayerController>() != null)
    {
      if (col.gameObject.GetComponent<PlayerController>().playerIndex == _pindex) return;
    }

    print(col.gameObject.name);
    col.gameObject.SendMessage("TakeDamage", new DamagePacket(_user, gameObject, _weapon.Damage, _weapon.Knockback));

      //Check if enemy
      //Send message to the enemy
      //Send message to the player in case they want it for something?
  }
}
