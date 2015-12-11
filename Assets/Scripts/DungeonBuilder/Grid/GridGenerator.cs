using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridGenerator : MonoBehaviour{
	
	private GridHolder _gridHolder;

	public void CreateGrid(int xSize, int ySize){
		List<List<GameObject>> grid = new List<List<GameObject>> ();
		GameObject tileObject = (GameObject)Resources.Load("Prefabs/TileObject");
		GameObject tileFogObject;

		DungeonTile dTile;
		SightTile sTile;

		Vector3 sizeTile = tileObject.GetComponent<SpriteRenderer>().bounds.size;
		Vector3 positionTile = new Vector3 (0,0,0);

		for (int xRow = 0; xRow < xSize; xRow++) {
			grid.Add(new List<GameObject>());
			for(int yRow = 0; yRow < ySize; yRow++){
				positionTile = new Vector3(-((xSize * sizeTile.x) / 2) + xRow * sizeTile.x,-((ySize * sizeTile.y) / 2) + yRow * sizeTile.y,1);
				tileObject = Instantiate((GameObject)Resources.Load("Prefabs/TileObject"),positionTile,Quaternion.identity) as GameObject;
				tileFogObject = Instantiate((GameObject)Resources.Load("Prefabs/TileObject"),positionTile,Quaternion.identity) as GameObject;
				sTile = tileFogObject.AddComponent<SightTile>();
				dTile = tileObject.AddComponent<DungeonTile>();
				dTile.SetTile(xRow,yRow);
				dTile.fogTile = sTile;

				sTile.SetDiscovered(false,dTile);
				sTile.SetBeingSeen(false);

				grid[xRow].Add(tileObject);
				tileObject.transform.SetParent(gameObject.transform);
				tileFogObject.transform.SetParent(tileObject.transform);

				if(xRow == 0 || xRow == xSize - 1 || yRow == 0 || yRow == ySize - 1){
					tileObject.GetComponent<DungeonTile>().isWall = true;
				}

			}
		}

		_gridHolder = new GridHolder(grid);
	}

	public GridHolder gridHolder{
		get{return _gridHolder;}
	}
}
