using UnityEngine;
using System.Collections;
using System.Linq;

public class BubbleAnimationEvents : MonoBehaviour {

	private BubbleInstantiator bubbleInstantiator;

	void Awake() {

		bubbleInstantiator = GameObject.FindGameObjectWithTag("GameController").GetComponent<BubbleInstantiator>();
	}

	public void SpawnNextBubble() {

		bubbleInstantiator.SpawnNewNounBubble();
	}

	public void BurstBubbleAndDrop() {

		// let's get a handle to the bubble's child, the noun chip
		//Transform chip = gameObject.GetComponentInChildren<Transform>();

		var chips = gameObject.transform.Cast<Transform>().Where(c=>c.gameObject.tag == "indefiniteA").ToArray();
		if( chips.Length < 1 ) {
			chips = gameObject.transform.Cast<Transform>().Where(c=>c.gameObject.tag == "indefiniteAn").ToArray();
		}
		// now we trust that something is in chips

		chips[0].parent = null;

		chips[0].rigidbody.useGravity = true;

		Destroy( gameObject );
	}
}
