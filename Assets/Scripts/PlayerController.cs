using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	
	Vector3 movement = new Vector3();
	public Transform Land;
	public Transform Drug;
	//public double jumpStrength = 0.1;
	private int landNumber = 1;
	private int lastAct = 1; //0 - последние действие влево, 1 - вправо
	
	public GameObject CMR;
	
	
	public List<KeyCode> upButton;
	public List<KeyCode> downButton;
	public List<KeyCode> leftButton;
	public List<KeyCode> rightButton;
	
	// Use this for initialization
	void Start () {
		//Instantiate(land, new Vector3(10*landNumber,0,0),new Quaternion());
		//rb = GetComponent<Rigidbody2D> ();
		
	}
	
	
	
	// Update is called once per frame
	void Update () {
		
		// Движение по тачу
		if (Input.touchCount > 0) {
			var touch = Input.GetTouch (0);
			if (touch.position.x > this.transform.position.x)
				moveRight();
			else 
				moveLeft();
			
		}
		//Движение по нажатию клавиш
		movement += MoveIfPressed (leftButton, moveLeft (), 0);
		movement += MoveIfPressed (rightButton, moveRight(), 1);
		movement += MoveIfPressed (upButton, moveUp(), 2);
		//moveUp ();
		
		
		movement.Normalize();
		
		if (movement.magnitude > 0)
		{
			transform.Translate (movement);
			movement = new Vector3();
		}
		
		
		
		//Создание новых платформ
		if (transform.position.x > landNumber * 10 - 11) {
			newBlock ();
		}
		
		
		
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "drugs") {
			ChangeScaleM(1.2f,1.2f,1f);
			Destroy(coll.gameObject);
		}
		
		
	}
	
	Vector3 moveRight() {
		
		return Vector3.right;
		
	}
	Vector3 moveLeft() {
		
		return Vector3.left;
		
	}
	Vector3 moveUp() {
		transform.position.Set (transform.position.x, 200, transform.position.z);
		return Vector3.up * 1000000;
		
	}
	
	void newBlock() {
		int n = Random.Range (0, 10);
		if (n > 5)
			Instantiate (Drug, new Vector3 (20 * landNumber + n, 5, 0), new Quaternion());
		Instantiate (Land, new Vector3 (20 * landNumber, 0, 0), new Quaternion());
		
		landNumber++;
	}
	
	void ChangeScaleM(float x, float y, float z){
		Vector3 newScale = new Vector3 ();
		newScale = transform.localScale;
		newScale.x *= x;
		newScale.y *= y;
		newScale.z *= z;
		
		//Camera cam = CMR.GetComponent<Camera>();
		//cam.orthographicSize++;
		
		transform.localScale = newScale;
	}
	
	
	Vector3 MoveIfPressed(List<KeyCode> keyList, Vector3 Movement, int moveType) {
		// Проверяем кнопки из списка
		foreach (KeyCode element in keyList)
		{
			if(Input.GetKey (element))
			{
				//Отрабатываем поворот
				if (moveType < 2){
					if (moveType != lastAct) {
						ChangeScaleM (-1, 1, 1);
					}
					
					lastAct = moveType;
					return Movement;
				}
			}
			if(Input.GetKeyDown (element)){
				if (moveType == 2) return Movement;
			}
		}
		// Если кнопки не нажаты, то не двигаемся
		return Vector3.zero;
	}
	
	
}