using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public GameObject Player;
	public Transform Land;
	public Transform Drug;
	private int landNumber = 1;

	void LateUpdate ()
	{
		var position = transform.position;
		position.x = Player.transform.position.x;
		transform.position = position;

		if (transform.position.x > landNumber * 10 - 11) {
			newBlock ();
		}
	}

	void newBlock ()
	{
		int n = Random.Range (0, 10);
		if (n > 5)
			Instantiate (Drug, new Vector3 (20 * landNumber + n, 5, 0), new Quaternion ());
		Instantiate (Land, new Vector3 (20 * landNumber, 0, 0), new Quaternion ());
				
		landNumber++;
	}
}
