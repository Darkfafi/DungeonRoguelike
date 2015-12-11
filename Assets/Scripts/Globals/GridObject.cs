using UnityEngine;
using System.Collections;

public class GridObject : MonoBehaviour {

	private GridHolder _gridHolder;
	private DungeonTile _tileOn = null;
	private int _gridPositionX = 0;
	private int _gridPositionY = 0;

	public virtual void SetPositionData(int x, int y, bool setAsTileObject = true){
		if (_tileOn != null) {
			_tileOn.RemoveGameObjectOnTile(this.gameObject);
		}
		if (setAsTileObject) {
			_tileOn = _gridHolder.GetTile (x, y);
			transform.position = _tileOn.gameObject.transform.position;
			_tileOn.AddGameObjectOnTile (this.gameObject);
		}
		_gridPositionX = x;
		_gridPositionY = y;
	}

	public virtual void SetToGridPosition(int x, int y){
		if (_tileOn != null) {
			_tileOn.RemoveGameObjectOnTile(this.gameObject);
		}
		SetPositionData (x, y);
	}

	public virtual void SetGridHolder(GridHolder gh){
		_gridHolder = gh;
	}

	public GridHolder gridHolder{
		get{return _gridHolder;}
	}

	public int gridPositionX{
		get{return _gridPositionX;}
	}
	public int gridPositionY{
		get{return _gridPositionY;}
	}
}
