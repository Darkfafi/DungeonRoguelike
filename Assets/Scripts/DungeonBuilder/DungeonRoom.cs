using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonRoom {

	//Maybe also neighboring Rooms in a list..
	//Maybe also root Tile.... core of room.
	private List<DungeonTile> _groundTiles = new List<DungeonTile>();
	private List<DungeonTile> _wallTiles = new List<DungeonTile>();
	private List<DungeonTile> _doorTiles = new List<DungeonTile>();

	public DungeonTile startTile = null;

	public void SetGroundTiles(List<DungeonTile> tiles){
		_groundTiles = tiles;
	}
	public void SetWallTiles(List<DungeonTile> tiles){
		_wallTiles = tiles;
	}
	public void AddDoorTile(DungeonTile doorTile){
		_doorTiles.Add (doorTile);
	}

	public List<DungeonTile> GetGroundTiles(){
		return _groundTiles;
	}
	public List<DungeonTile> GetWallTiles(){
		return _wallTiles;
	}
	public List<DungeonTile> GetDoorTiles(){
		return _doorTiles;
	}

	public List<DungeonTile> GetTotalRoom(){
		List<DungeonTile> totalRoom = new List<DungeonTile> ();

		for (int i = 0; i < _groundTiles.Count; i++) {
			totalRoom.Add(_groundTiles[i]);
		}
		for (int i = 0; i < _wallTiles.Count; i++) {
			totalRoom.Add (_wallTiles [i]);
		}
		for (int i = 0; i < _doorTiles.Count; i++) {
			totalRoom.Add(_doorTiles[i]);
		}

		return totalRoom;
	}

	public void LockRoom(){
		for (int i = 0; i < _groundTiles.Count; i++) {
			_groundTiles[i].isClosed = true;
		}
		for (int i = 0; i < _wallTiles.Count; i++) {
			_wallTiles[i].isClosed = true;
		}
		for (int i = 0; i < _doorTiles.Count; i++) {
			_doorTiles[i].isClosed = true;
		}
	}
}
