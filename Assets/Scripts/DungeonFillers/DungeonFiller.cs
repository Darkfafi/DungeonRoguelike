using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonFiller {

	private DungeonBuilder _dungeonBuilder;
	private GridHolder _gridHolder;

	public DungeonFiller(DungeonBuilder dungeonBuilder,GridHolder gridHolder){
		_dungeonBuilder = dungeonBuilder;
		_gridHolder = gridHolder;
	}

	public void SetAllDungeonRoom(int dangerLevel,int lootLevel){
		//danger and loot level are only indications for another system that spawns the real dungeon items. this only sets the room stats for it.
		//The system wil take the percentage of both loot and danger. Will check the size of the room and determan what to spawn.
		List<DungeonRoom> allRooms = _dungeonBuilder.allDungeonRooms;
		CreatePlayer ();

	}

	private void CreatePlayer(){

		// TODO Maak dit zodat de speler echt word gemaakt. (art, gameobject(prefab), alles!);
		GridMovement mvm;// = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<GridMovement> (); // DEBUGGING
		Player player = GameObject.FindGameObjectWithTag (Tags.PLAYER).GetComponent<Player> ();// DEBUGGING
		mvm = player.GetComponent<GridMovement>();
		player.SetGridHolder(_gridHolder); 
		PlaceObjectOnGrid (player,(int)_dungeonBuilder.coreTile.gridPosition.x,(int)_dungeonBuilder.coreTile.gridPosition.y);
		mvm.SetGridPosition((int)_dungeonBuilder.coreTile.gridPosition.x,(int)_dungeonBuilder.coreTile.gridPosition.y);

	}

	private void PlaceObjectOnGrid(GridObject obj, int tileX, int tileY){
		obj.SetPositionData (tileX, tileY);
	}
}