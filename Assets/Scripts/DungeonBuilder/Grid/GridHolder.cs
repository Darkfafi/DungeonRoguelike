using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridHolder{

	public static Vector2 UP_LEFT = new Vector2 (-1, 1);
	public static Vector2 DOWN_LEFT = new Vector2 (-1, -1);
	public static Vector2 UP_RIGHT = new Vector2 (1, 1);
	public static Vector2 DOWN_RIGHT = new Vector2 (1, -1);

	private List<List<GameObject>> _grid;

	public GridHolder(List<List<GameObject>> grid){
		_grid = grid;
	}
	public DungeonTile GetTile(int x, int y){
		DungeonTile result = null;

		if (x >= 0 && y >= 0 && x < _grid.Count && y <_grid [x].Count) {
			result = _grid[x][y].GetComponent<DungeonTile>();
		}

		return result;
	}

	public List<List<GameObject>> grid{
		get{return _grid;}
	}

	public List<DungeonTile> GetNeighbors(DungeonTile tile,bool diagonal = false){
		List<DungeonTile> neighborsToReturn = new List<DungeonTile> ();
		
		int x = (int)tile.gridPosition.x;
		int y = (int)tile.gridPosition.y;
		
		if (GetTile (x - 1, y) != null) {
			neighborsToReturn.Add(GetTile (x - 1, y));
		}
		if (GetTile (x + 1, y) != null) {
			neighborsToReturn.Add(GetTile (x + 1, y));
		}
		if (GetTile (x, y - 1) != null) {
			neighborsToReturn.Add(GetTile (x, y - 1));
		}
		if (GetTile (x, y + 1) != null) {
			neighborsToReturn.Add(GetTile (x, y + 1));
		}
		if (diagonal) {
			if (GetTile (x - 1, y - 1) != null) {
				neighborsToReturn.Add(GetTile (x - 1, y - 1));
			}
			if (GetTile (x + 1, y + 1) != null) {
				neighborsToReturn.Add(GetTile (x + 1, y + 1));
			}
			if (GetTile (x + 1, y - 1) != null) {
				neighborsToReturn.Add(GetTile (x + 1, y - 1));
			}
			if (GetTile (x - 1, y + 1) != null) {
				neighborsToReturn.Add(GetTile (x - 1, y + 1));
			}
		}
		return neighborsToReturn;
	}

	public NeighborsCheckHolder GetNeighborsCheckOnTile(DungeonTile tile, bool wallCheckOnly = false){
		
		List<DungeonTile> currentTileNeighbors = GetNeighbors (tile, true);
		Vector2 dirWall = new Vector2 ();
		NeighborsCheckHolder nCheckHold = new NeighborsCheckHolder ();
			for (int j = 0; j < currentTileNeighbors.Count; j++) {
				if(!wallCheckOnly || (wallCheckOnly && currentTileNeighbors[j].isWall)){
				dirWall = GetDirectionToTile (tile, currentTileNeighbors [j]);
				if (dirWall == Vector2.right) {
					nCheckHold.rightTile = currentTileNeighbors [j];
				}
				if (dirWall == Vector2.left) {
					nCheckHold.leftTile = currentTileNeighbors [j];
				}
				if (dirWall == Vector2.up) {
					nCheckHold.upTile = currentTileNeighbors [j];
				}
				if (dirWall == Vector2.down) {
					nCheckHold.downTile = currentTileNeighbors [j];
				}
				
				if (dirWall == GridHolder.DOWN_LEFT) {
					nCheckHold.leftDownTile = currentTileNeighbors [j];
				}
				if (dirWall == GridHolder.DOWN_RIGHT) {
					nCheckHold.rightDownTile = currentTileNeighbors [j];
				}
				if (dirWall == GridHolder.UP_LEFT) {
					nCheckHold.leftUpTile = currentTileNeighbors [j];
					
				}
				if (dirWall == GridHolder.UP_RIGHT) {
					nCheckHold.rightUpTile = currentTileNeighbors [j];
				}
			}
		}
		return nCheckHold;
	}

	public Vector2 GetDirectionToTile(DungeonTile coreTile, DungeonTile otherTile){
		Vector2 dirVec = new Vector2 ();
	
		if (coreTile.gridPosition.y > otherTile.gridPosition.y) {
			dirVec = Vector2.down;
		} else if (coreTile.gridPosition.y < otherTile.gridPosition.y) {
			dirVec = Vector2.up;
		}
		if(coreTile.gridPosition.x < otherTile.gridPosition.x){
			dirVec += Vector2.right;
		}else if(coreTile.gridPosition.x > otherTile.gridPosition.x){
			dirVec += Vector2.left;
		}
		return dirVec;
	}
}
