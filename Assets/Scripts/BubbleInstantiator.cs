using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BubbleInstantiator : MonoBehaviour {

	public Transform bubblePrefab;

	public RuntimeAnimatorController[] animationClips;
	// ...instaed of a predefined array, could a read from resource folder, like suggested in
	//	http://answers.unity3d.com/questions/271842/adding-a-clip-to-a-animation-component-via-script.html

	private List<GameObject> indefiniteChips;

	void Awake() {


	}

	// Use this for initialization
	void Start () {

		indefiniteChips = new List<GameObject>( Resources.LoadAll<GameObject>("Prefabs/nouns") );
	
		SpawnNewNounBubble();

		Physics.gravity = new Vector3(0, -0.8F, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void SpawnNewNounBubble() {


		GameObject someRandomChipPrefab = GetAndRemoveRandomGameObjectFromList( indefiniteChips );

		if( null != someRandomChipPrefab ) {

			GameObject someRandomChipInstance = 
				Instantiate( someRandomChipPrefab, transform.position, transform.rotation ) as GameObject;

			Transform bubbleInstance = Instantiate( bubblePrefab, transform.position, transform.rotation) as Transform;
			
			someRandomChipInstance.transform.parent = bubbleInstance;
			someRandomChipInstance.transform.position = bubbleInstance.transform.position;
			
			// for some reason the chips are upside down, let's rotate
			someRandomChipInstance.transform.Rotate( new Vector3(180, 180, 0) );
			
			ScaleObjectToBeWithinItsParent( someRandomChipInstance );

			// choose and start a random animation for the parent bubble
			Animator instanceAnim = bubbleInstance.GetComponent<Animator>();
			if( instanceAnim != null ) {
				
				instanceAnim.runtimeAnimatorController = animationClips[Random.Range(0, animationClips.Length)];
				instanceAnim.enabled = true;
			}
			
		} else {
			// we're out of chips, end the game...
			GameOver();
		}
	}


	private void GameOver() {

		// TODO: show highscore
	}

	private GameObject GetAndRemoveRandomGameObjectFromList( List<GameObject> objectList ) {
		GameObject objectToRemove = null;

		if( objectList.Count > 0 ) {

			int listIndex = Random.Range(0, objectList.Count);
			
			objectToRemove = objectList[listIndex];
			objectList.RemoveAt( listIndex );
		}
		return objectToRemove;
	}

	private void ScaleObjectToBeWithinItsParent( GameObject child ) {

		float parentChildRatio = child.transform.parent.localScale.x / child.transform.localScale.x;

		// let's increase the scale by some trial and error value
		child.transform.localScale *= parentChildRatio * 1.5f;
	}

}
