using UnityEngine;
using System.Collections;

public class NeighborsCheckHolder {

	public DungeonTile leftTile = null;
	public DungeonTile rightTile = null;
	public DungeonTile upTile = null;
	public DungeonTile downTile = null;

	public DungeonTile leftUpTile = null;
	public DungeonTile leftDownTile = null;
	public DungeonTile rightUpTile = null;
	public DungeonTile rightDownTile = null;


	public void Reset(){
		leftTile = null;
		rightTile = null;
		upTile = null;
		downTile = null;
		
		leftUpTile = null;
		leftDownTile = null;
		rightUpTile = null;
		rightDownTile = null;
	}

	public bool HorizontalAllFilled(){
		if (leftTile && rightTile && upTile && downTile) {
			return true;
		} else {
			return false;
		}
	}
	public bool DiagnalAllFilled(){
		if (leftUpTile && rightUpTile && leftDownTile && rightDownTile) {
			return true;
		} else {
			return false;
		}
	}

	public bool AllTilesFilled(){
		if (DiagnalAllFilled () && HorizontalAllFilled ()) {
			return true;
		} else {
			return false;
		}
	}
}
