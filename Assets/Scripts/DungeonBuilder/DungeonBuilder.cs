using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonBuilder{

	//eventuele seed meegeven.
	private DungeonPainter _painter;
	private List<DungeonRoom> _allRooms = new List<DungeonRoom>();

	private DungeonTile _coreTile;
	private DungeonTile _endTile;
	// idee,  linear gebruiken voor boss rooms


	public void GenerateDungeon(GridHolder gridHolder,int amountOfRooms,bool linear = false, int roomSizeRangeMin = 15, int roomSizeRangeMax = 70){
		int startTileChooser = 0;
		int maxForLinear = 40;
		DungeonTile startTile = gridHolder.GetTile(Random.Range(1,gridHolder.grid.Count - 1),Random.Range(1,gridHolder.grid[0].Count - 1));
		DungeonRoom dungenRoom = null;

		//end results
		_coreTile = startTile;
		_endTile = null;

		for (int i = 0; i < amountOfRooms; i++) {

			dungenRoom = CreateRoom (gridHolder, startTile, Random.Range(roomSizeRangeMin,roomSizeRangeMax));
			dungenRoom.startTile = startTile;

			startTileChooser = Random.Range(0,100);

			if(startTileChooser <= maxForLinear || linear){
				startTile = CreateDoorForRoom(dungenRoom,gridHolder);
			}
			if(!linear){
				if(dungenRoom.GetDoorTiles().Count == 0 || (startTileChooser > maxForLinear)){
					startTile = null;
					for(int j = 0; j < _allRooms.Count; j++){
						startTile = CreateDoorForRoom(_allRooms[j],gridHolder);
						if(startTile != null){
							break;
						}
					}
				}
			}else{
				if(dungenRoom.GetDoorTiles().Count == 0){
					for(int j = _allRooms.Count - 1; j >= 0; j--){
						startTile = CreateDoorForRoom(_allRooms[j],gridHolder);
						if(startTile != null){
							break;
						}
					}
				}
			}

			_endTile = startTile;

			if(startTile != null){
				startTile = GetStartTileFromDoor(gridHolder,startTile); 
			}

			if(startTile == null){
				break;
			}
		}
	}

	private DungeonTile GetStartTileFromDoor(GridHolder gridHolder, DungeonTile doorTile){
		List<DungeonTile> tilesDoorNeighbors = new List<DungeonTile> ();
		List<DungeonTile> tilesDoorNeighborsNeighbors = new List<DungeonTile> ();
		int wallCounter = 0;
		tilesDoorNeighbors = gridHolder.GetNeighbors(doorTile);

		DungeonTile startTile = null;
		for(int i = 0; i < tilesDoorNeighbors.Count; i++){
			if(tilesDoorNeighbors[i].empty){
				wallCounter = 0;
				tilesDoorNeighborsNeighbors = gridHolder.GetNeighbors(tilesDoorNeighbors[i]);
				for(int j = 0; j < tilesDoorNeighborsNeighbors.Count; j++){
					if(tilesDoorNeighborsNeighbors[j].isWall){
						wallCounter ++;
					}
				}
				if(wallCounter < 3){
					startTile = tilesDoorNeighbors[i];
				}
			}
			

		}
		return startTile;
	}

	private DungeonRoom CreateRoom(GridHolder gridHolder,DungeonTile startTile,int roomMaxSize){

		DungeonRoom dungeonRoom = new DungeonRoom ();

		int roomTilesSet = 0; //tiles placed
		int groundCost = 0; // reduce worth of groundworth neighbors
		List<DungeonTile> roomTiles = new List<DungeonTile>();

		DungeonTile currentTile = startTile;
		startTile.startTile = true;
		startTile.isGround = true;
		//startTile.gameObject.GetComponent<SpriteRenderer> ().color = Color.blue;
		List<DungeonTile> currentTileNeighbors = gridHolder.GetNeighbors (currentTile); // current Tile His neighbors (van de beste zijn buren checken tot daar een beste uit komt)
		DungeonTile bestForGroundNeighbor = null;


		DungeonTile currentNeightbor; //current Neighbor
		List<DungeonTile> currentNeighborTileNeighbors; // current Neighbor his neighbors


		roomTiles.Add (startTile); // start tile is first tile of the room

		// while not all roomtiles placed
		while (roomTilesSet < roomMaxSize) {
			// Check neighbors if they already have a ground around them

			for(int i = 0; i < currentTileNeighbors.Count; i++){
				currentNeightbor = currentTileNeighbors[i];
				currentNeighborTileNeighbors = gridHolder.GetNeighbors(currentNeightbor);
				for(int j = 0; j < currentNeighborTileNeighbors.Count; j++){
					if(currentNeighborTileNeighbors.Count == 4){
						if(currentNeighborTileNeighbors[j].isGround){
							currentNeightbor.groundWorth++; //groundWorth - groundCost; if groundWorth < 0; groundWorth = 0; (daarmee maakt het na 4 tiles niet uit of de volgende neighbor ground tiles heeft).
						}
					}else{
						currentNeightbor.isClosed = true;
					}
				}
			}

			bestForGroundNeighbor = null;

			for(int j = 0; j < currentTileNeighbors.Count; j++){

				currentTileNeighbors[j].groundWorth -= groundCost;

				if(currentTileNeighbors[j].groundWorth < 0){
					currentTileNeighbors[j].groundWorth = 0;
				}
				if(!currentTileNeighbors[j].isClosed){
					if(bestForGroundNeighbor == null){
						bestForGroundNeighbor = currentTileNeighbors[j];
					}else{
						if(currentTile == startTile && bestForGroundNeighbor.groundWorth > currentTileNeighbors[j].groundWorth){
							bestForGroundNeighbor = currentTileNeighbors[j];

						} else if(bestForGroundNeighbor.groundWorth < currentTileNeighbors[j].groundWorth){
							bestForGroundNeighbor = currentTileNeighbors[j];
						}else if(bestForGroundNeighbor.groundWorth == currentTileNeighbors[j].groundWorth){
							if(Random.Range(0,2) < 1){
								bestForGroundNeighbor = currentTileNeighbors[j];
							}
						}                                                                             
					}
				}
			}
			if(bestForGroundNeighbor != null){
				roomTiles.Add(bestForGroundNeighbor);
				bestForGroundNeighbor.parentTile = currentTile;
				currentTile.isClosed = true;
				currentTile.isGround = true;
				currentTile = bestForGroundNeighbor;
				currentTile.isGround = true;
				groundCost ++;
			}else{
				currentTile = startTile;
				groundCost = 0;
			}
			for(int i = 0; i < currentTileNeighbors.Count; i++){
				currentTileNeighbors[i].groundWorth = 0;
			}


			currentTileNeighbors = gridHolder.GetNeighbors (currentTile);
		
			//reset neighborts  
			if(groundCost > 1){ // how solid the rooms are
				groundCost = 0;
			}
			roomTilesSet ++;
		}

		dungeonRoom.SetGroundTiles(roomTiles);
		CreateWallsAroundRoom (gridHolder, roomTiles, dungeonRoom);
		return dungeonRoom;//CreateWallsAroundRoom (gridHolder,roomTiles,dungeonRoom); // geef kamer terug en maak dan een deur daar

	}

	private void CreateWallsAroundRoom(GridHolder gridHolder,List<DungeonTile> roomTiles,DungeonRoom dungeonRoom){

		List<DungeonTile> currentTileNeighbors = new List<DungeonTile> ();
		List<DungeonTile> allWalls = new List<DungeonTile> ();
		for (int i = 0; i < roomTiles.Count; i++) {
			currentTileNeighbors = gridHolder.GetNeighbors(roomTiles[i],true);
			for(int j = 0; j < currentTileNeighbors.Count; j++){ 
				if(!currentTileNeighbors[j].isGround && (!currentTileNeighbors[j].isClosed || gridHolder.GetNeighbors(currentTileNeighbors[j]).Count < 4)){
					currentTileNeighbors[j].isWall = true;
					allWalls.Add(currentTileNeighbors[j]);
				}
			}

		}

		dungeonRoom.SetWallTiles(allWalls);
		dungeonRoom.LockRoom ();
		//doorTile = CreateDoorForRoom(dungeonRoom,gridHolder);
		_allRooms.Add (dungeonRoom);
	}

	private DungeonTile CreateDoorForRoom(DungeonRoom dungeonRoom, GridHolder gridHolder){
		bool useEmpty = false;
		bool useGround = false;
		bool skipTile = false;
		DungeonTile doorTile;

		int wallHorCount = 0;
		int wallVerCount = 0;
		Vector2 dirToWall = new Vector2 ();

		List<DungeonTile> allWalls = dungeonRoom.GetWallTiles();
		List<DungeonTile> currentTileNeighbors = new List<DungeonTile> ();
		List<DungeonTile> doorPlaceAbleTiles = new List<DungeonTile> ();

		for (int i = 0; i < allWalls.Count; i++) {
			currentTileNeighbors = gridHolder.GetNeighbors(allWalls[i]);

			useEmpty = false;
			useGround = false;
			wallHorCount = 0;
			wallVerCount = 0;
			skipTile = false;
			if(allWalls[i].isDoor == false){
				for(int j = 0; j < currentTileNeighbors.Count; j++){

					if(currentTileNeighbors[j].isDoor){
						skipTile = true;
					}
					if(currentTileNeighbors[j].isWall){
						dirToWall = gridHolder.GetDirectionToTile(allWalls[i],currentTileNeighbors[j]);
						if(dirToWall == Vector2.right || dirToWall == Vector2.left){
							wallHorCount ++;
						}else if(dirToWall == Vector2.up || dirToWall == Vector2.down){
							wallVerCount ++;
						}
					}

					if(wallHorCount > 0 && wallVerCount > 0){
						skipTile = true;
					}

					if(!skipTile){
						if(currentTileNeighbors[j].empty){
							useEmpty = true;
						}
						if(currentTileNeighbors[j].isGround){
							useGround = true;
						}
						if(j == 3 && useEmpty && useGround){
							doorPlaceAbleTiles.Add(allWalls[i]);
						}
					}
				}
			}
		}
		if (doorPlaceAbleTiles.Count != 0) {
			doorTile = doorPlaceAbleTiles [Random.Range (0, doorPlaceAbleTiles.Count)]; 
			doorTile.isDoor = true;
			dungeonRoom.AddDoorTile (doorTile);
		} else {
			doorTile = null;
		}
		dungeonRoom.LockRoom ();
		return doorTile; 
	}

	public List<DungeonRoom> allDungeonRooms{
		get{return _allRooms;}
	}

	public List<DungeonTile> GetEntireDungeonInTiles(){
		List<DungeonTile> allTiles = new List<DungeonTile> ();
		List<DungeonTile> allTilesRoom = new List<DungeonTile> ();
		for (int i = 0; i < _allRooms.Count; i++) {
			allTilesRoom = _allRooms[i].GetTotalRoom();
			for(int j = 0; j < allTilesRoom.Count; j++){
				allTiles.Add(allTilesRoom[j]);
			}
		}

		return allTiles;
	}

	public DungeonTile coreTile{
		get{return _coreTile;}
	}
	public DungeonTile endTile{
		get{return _endTile;}
	}
}
