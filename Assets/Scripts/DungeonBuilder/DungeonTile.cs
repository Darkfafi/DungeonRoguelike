using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonTile : BaseTileComponent {

	public DungeonTile parentTile;

	public SightTile fogTile;

	public bool startTile = false;

	[SerializeField]private bool _empty = true;
	[SerializeField]private bool _isWall = false;
	[SerializeField]private bool _isGround = false;
	[SerializeField]private bool _isDoor = false;

	public bool isClosed = false;

	public int groundWorth = 0;

	private List<GameObject> _objectsOnTile = new List<GameObject>();

	public bool isWall{
		set{
			_isWall = value;
			if(_isGround && _isWall){
				_isGround = false;
			}
			if(_isDoor && _isWall){
				isDoor = false;
			}
			//gameObject.GetComponent<SpriteRenderer>().color = Color.red;
			if(!_isGround && !isDoor){
				_empty = !value;
			}
		}
		get{
			return _isWall;
		}
	}
	public bool isGround{
		set{
			_isGround = value;
			if(_isGround && _isWall){
				_isWall = false;
			}
			if(_isDoor && _isGround){
				isDoor = false;
			}
			//gameObject.GetComponent<SpriteRenderer>().color = Color.green;
			if(!_isWall && !isDoor){
				_empty = !value;
			}
		}
		get{
			return _isGround;
		}
	}
	public bool isDoor {
		set{
			_isDoor = value;
			if(_isDoor && _isWall){
				_isWall = false;
			}
			if(_isDoor && _isGround){
				_isGround = false;
			}
			//gameObject.GetComponent<SpriteRenderer> ().color = Color.yellow;
			if(_isDoor){
				gameObject.GetComponent<SpriteRenderer> ().sortingOrder = 1;
				fogTile.gameObject.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer> ().sortingOrder + 2;
			}else{
				gameObject.GetComponent<SpriteRenderer> ().sortingOrder = 0;
			}
			if(!_isGround && !_isWall){
				_empty = !value;
			}
		}
		get{return _isDoor;}
	}

	public bool empty{
		get{return _empty;}
	}
	public List<GameObject> objectsOnTile{
		get{return _objectsOnTile;}
	}
	public Vector2 AddGameObjectOnTile(GameObject go){
		_objectsOnTile.Add (go);
		return gridPosition;
	}
	public Vector2 RemoveGameObjectOnTile(GameObject go){
		_objectsOnTile.Remove (go);
		return gridPosition;
	}
}
