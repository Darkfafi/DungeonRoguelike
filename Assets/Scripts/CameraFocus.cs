using UnityEngine;
using System.Collections;

public class CameraFocus : MonoBehaviour {

	private GameObject _target;

	// Use this for initialization
	void Start () {
		_target = GameObject.FindGameObjectWithTag (Tags.PLAYER);
	}
	
	// Update is called once per frame
	void Update () {
		if (_target != null) {
			transform.position = new Vector3(_target.transform.position.x,_target.transform.position.y,transform.position.z);
		}
	}
}
