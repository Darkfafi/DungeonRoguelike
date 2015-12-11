using UnityEngine;
using System.Collections;

public class BaseTileComponent : MonoBehaviour {

	private Vector2 _gridPosition;

	public void SetTile(int x, int y){
		_gridPosition = new Vector2 (x, y);
	}
	
	public Vector2 gridPosition{
		get{return _gridPosition;}
	}
}
