using UnityEngine;
using System.Collections;

public class GroundController : MonoBehaviour {

	private int numberOfTiles;
	private float tileWidth;
	public GameObject[] groundTiles;
	public GameObject[] groundTileStarts;
	public GameObject[] groundTileEnds;
	public GameObject[] groundChongs;
	public GameObject groundFormation;
	public GameObject chong1;

	public int pos;

	// Use this for initialization
	void Start () {
		pos = ItemData.Pos;
		tileWidth = groundTiles[pos].GetComponent<SpriteRenderer>().bounds.size.x;
		numberOfTiles = Random.Range(6, 10);
		SpawnGround();
		SpawnFormation();
	}

	void SpawnGround() {
		// add begin tiles

		int r = Random.Range(2, numberOfTiles - 4);

		for (int i = 1; i <= numberOfTiles; i++) {
			Vector3 offset = new Vector3((i - 1) * tileWidth, 0, 0);
			GameObject groundTileObject;
			if (i == 1) {
				groundTileObject = Instantiate(groundTileStarts[pos], transform.position + offset, Quaternion.identity) as GameObject;
			} else if (i == numberOfTiles) {
				groundTileObject = Instantiate(groundTileEnds[pos], transform.position + offset, Quaternion.identity) as GameObject;
			} else if (i != r) {
				groundTileObject = Instantiate(groundTiles[pos], transform.position + offset, Quaternion.identity) as GameObject;
			} else {
				groundTileObject = Instantiate(groundChongs[pos], transform.position + offset, Quaternion.identity) as GameObject;

			}

			groundTileObject.transform.parent = transform;

		}

		//add end tiles
	}

	void SpawnFormation() {
//		int rand = Random.Range(1, 3);
		float y = 0;
//		if (rand == 1) y = 1;
//		else if (rand == 2) y = -1;

		Vector3 offset = new Vector3((numberOfTiles + 1) * tileWidth - 1.0f, y, 0);
		GameObject groundFormationObject = Instantiate(groundFormation, transform.position + offset, Quaternion.identity) as GameObject;
		groundFormationObject.transform.parent = transform;

		int r = Random.Range(0, 2);
		if (r == 1 && DistanceController.RunDistance > 1800) {
			Instantiate(chong1, 
				new Vector3(transform.position.x + (numberOfTiles + 0.5f)  * tileWidth - 0.5f, -2.0f 
					+ Mathf.Min(transform.position.y, transform.position.y + y), 0), 
				Quaternion.identity);			
		}

	}
}
