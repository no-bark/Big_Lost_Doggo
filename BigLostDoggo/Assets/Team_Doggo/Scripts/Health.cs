using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

  void OnTakeDamage(DamagePacket packet)
  {
    gameObject.SendMessage("OnTakeKnockback", packet);

    gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
  }

	// Update is called once per frame
	void Update () {
	
	}
}
