using UnityEngine;
using System.Collections;

public class GridMovement : MonoBehaviour {

	public delegate void IntInfo(int x, int y);
	public event IntInfo MoveSetPosition;


	public GridHolder gridHolder;
	public float movementSpeed = 12.5f;

	private Vector2 _currentTilePos = new Vector2 ();
	private DungeonTile _currentTile;
	private DungeonTile _tileToMoveTo;
	private Vector2 _movementVec = new Vector2 ();

	public void Move(Vector2 dir){
		if (_tileToMoveTo == null) {
			DungeonTile tileToMoveTo = gridHolder.GetTile ((int)(_currentTilePos.x + dir.x), (int)(_currentTilePos.y + dir.y));
			if (tileToMoveTo != null && !tileToMoveTo.isWall) {
				_tileToMoveTo = tileToMoveTo;
			}
		}
	}

	void Update(){
		if (_tileToMoveTo != null) {
			transform.Translate((_tileToMoveTo.gameObject.transform.position - transform.position) * Time.deltaTime * movementSpeed);
			if((_tileToMoveTo.gameObject.transform.position - transform.position).magnitude < 0.1f){
				SetGridPosition((int)_tileToMoveTo.gridPosition.x,(int)_tileToMoveTo.gridPosition.y);
				if(_currentTile != null){
					_currentTile.RemoveGameObjectOnTile(this.gameObject);
				}
				_currentTile = _tileToMoveTo;
				_currentTile.AddGameObjectOnTile(this.gameObject);
				_tileToMoveTo = null;
			}
		}
	}

	public void SetGridPosition(int x, int y){
		_currentTilePos = new Vector2 (x, y);
		transform.position = gridHolder.GetTile (x, y).gameObject.transform.position;
		if (MoveSetPosition != null) {
			MoveSetPosition(x,y);
		}
	}
}
