using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed = 1f;
	public float spawnDelay = 1f;

	private bool movingRigth = false;
	private float maxX;
	private float minX;

	// Use this for initialization
	void Start () {
		SpawnInPositions (enemyPrefab);
		FindMinAndMax (ref minX, ref maxX);
	}
	
	// Update is called once per frame
	void Update () {
		MoveFormation (ref movingRigth);

		if (AllMembersDead()) {
			Debug.Log("Empty Formation");
			SpawnUntilFull ();
		}
	}

	public void OnDrawGizmos (){
		Gizmos.DrawWireCube (transform.position, new Vector3 (width, height));
	}

	void Spawn (GameObject prefab, Vector3 onPosition){
		GameObject enemy  = Instantiate (prefab, onPosition, Quaternion.identity) as GameObject;
		enemy.transform.parent = transform;
	}

	void Spawn (GameObject prefab, Vector3 onPosition, Transform parent){
		GameObject enemy  = Instantiate (prefab, onPosition, Quaternion.identity) as GameObject;
		enemy.transform.parent = parent;
	}

	void SpawnInPositions (GameObject prefab){
		int count = 0;
		foreach (Transform child in transform) {
			Spawn(prefab, child.transform.position, child);
			count ++;
			print (count);
			if (count >=10){
				break;
			}
		}
	}

	void SpawnUntilFull(){
		Transform freePosition = NextFreePosition ();
		if (freePosition) {
			Spawn (enemyPrefab, freePosition.position, freePosition);

		}
		if (NextFreePosition()){
		Invoke ("SpawnUntilFull", spawnDelay);
		}
	}

	Transform NextFreePosition(){
		foreach (Transform childPositionGameOnject in transform) {
			if (childPositionGameOnject.childCount == 0){
				return childPositionGameOnject;
			}
		}
		return null;
	}

	void MoveFormation (ref bool movingRigth){
		if (movingRigth) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		} else {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		float rigthEdgeFormation = transform.position.x + (width * 0.5f);
		float leftEdgeFormation = transform.position.x - (width*0.5f);
		if (leftEdgeFormation <= minX) {
			movingRigth = true;
		} else if (rigthEdgeFormation >= maxX) {
			movingRigth = false;
		}
	}

	void FindMinAndMax (ref float min, ref float max){
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, distance));
		min = leftmost.x;
		max = rightmost.x;
	}

	bool AllMembersDead(){
		foreach (Transform childPositionGameOnject in transform) {
			if (childPositionGameOnject.childCount > 0){
				return false;
			}
		}
		return true;
	}
}
