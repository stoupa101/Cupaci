using UnityEngine;
using System.Collections;
public class MoveBox : MonoBehaviour {

	void Awake ()
	{
		// Setting up the reference.

	}

	void Update ()
	{
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		PlayerControl player = col.gameObject.GetComponent<PlayerControl>();
		if (player != null)
		{
			// If the colliding gameobject is an Enemy...
			if(col.gameObject.tag == "Player" && player.grounded) {
				rigidbody2D.isKinematic = false;
			} else {
				rigidbody2D.isKinematic = true;
			}
		}
		else
		{
			rigidbody2D.isKinematic = true;
		}
	}
}
