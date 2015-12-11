using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DungeonPainter{

	private GridHolder _gridHolder;
	private DungeonBuilder _dungeonBuilder;

	public DungeonPainter(DungeonBuilder dungeonBuilder, GridHolder gridHolder){
		_dungeonBuilder = dungeonBuilder;
		_gridHolder = gridHolder;
	}

	public void PaintGrid(){
		DungeonTile tile = null;
		for (int i = 0; i < _gridHolder.grid.Count; i++) {
			for (int j = 0; j < _gridHolder.grid[i].Count; j++) {
				tile = _gridHolder.grid[i][j].GetComponent<DungeonTile>();

				if(tile.isWall){
					tile.gameObject.GetComponent<SpriteRenderer>().sprite = GetWallArt(_gridHolder.GetNeighborsCheckOnTile(tile,true),tile);
				}else if(tile.empty){
					tile.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/Tiles/tiles-15");
				}
				DebugSetTile(tile);
			}
		}
	}

	public void PaintDungeon(){
		//TODO geef als parameter string mee die je add bij de tile string. dit maakt het thema van de dungeon. (Of het een stone of wooden dungeon is bijv)
		List<DungeonTile> allTiles = _dungeonBuilder.GetEntireDungeonInTiles ();
		//List<DungeonTile> currentTileNeighbors = new List<DungeonTile> ();

		NeighborsCheckHolder nCheckHold;
		//Vector2 dirWall;

		for (int i = 0; i < allTiles.Count; i++) {
			if(allTiles[i].isWall || allTiles[i].isDoor){

				nCheckHold = _gridHolder.GetNeighborsCheckOnTile(allTiles[i],true);

				if(allTiles[i].isWall){
					allTiles[i].GetComponent<SpriteRenderer>().sprite = GetWallArt(nCheckHold,allTiles[i]);
				}else if(allTiles[i].isDoor){
					allTiles[i].GetComponent<SpriteRenderer>().sprite = GetDoorArt(nCheckHold);
				}

			}else if(allTiles[i].isGround){
				allTiles[i].gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/Tiles/tiles-11");
			}

			_dungeonBuilder.coreTile.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/Tiles/tiles-19");
			_dungeonBuilder.endTile.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/Tiles/tiles-20");

			DebugSetTile(allTiles[i]);
		}
	}

	private Sprite GetWallArt(NeighborsCheckHolder nch, DungeonTile tile){
		string baseTilesPath = "Art/Tiles/";
		string artString = "tiles-02";
		Sprite sprt = Resources.Load<Sprite> (baseTilesPath + "tiles-02");

		if (nch.AllTilesFilled()) {
			artString = "tiles-02";
		} else if (nch.leftTile != null && nch.downTile != null && nch.rightTile != null && nch.leftDownTile == null && nch.rightDownTile == null) {
			//artString = "tiles-02";
			//if(){
				artString = "tiles-05";
			//}
		}else if(nch.leftTile != null && nch.downTile != null && nch.rightTile != null && nch.leftDownTile != null && nch.rightDownTile != null){
			artString = "tiles-02";
		}else if(nch.downTile != null) {
			artString = "tiles-08";
			if(nch.rightTile != null && nch.leftTile != null){
				if (nch.rightDownTile == null) {
					artString = "tiles-17";
				}
				if (nch.leftDownTile == null) {
					artString = "tiles-18";
				}
			}else if (nch.rightTile != null) {
				artString = "tiles-03";
				if (nch.rightDownTile == null) {
					artString = "tiles-10";
				}
			}else if (nch.leftTile != null) {
				artString = "tiles-12";
				if (nch.leftDownTile == null) {
					artString = "tiles-09";
				}
			}
		} else{
			artString = "tiles-04";
			if(nch.leftTile == null && nch.rightTile != null){
				artString = "tiles-06";
			}else if(nch.rightTile == null && nch.leftTile != null){
				artString = "tiles-07";
			}else if(nch.rightTile == null && nch.leftTile == null){
				artString = "tiles-16";
			}

		}
		sprt = Resources.Load<Sprite> (baseTilesPath + artString);
		return sprt;
	}

	private Sprite GetDoorArt(NeighborsCheckHolder nch){
		string baseTilesPath = "Art/Tiles/";
		Sprite sprt = null;
		if (nch.leftTile != null && nch.rightTile != null) {
			sprt = Resources.Load<Sprite> (baseTilesPath + "tiles-14");
		} else if (nch.upTile != null && nch.downTile != null) {
			sprt = Resources.Load<Sprite> (baseTilesPath + "tiles-13");
		}
		
		return sprt;
	}

	public static void DebugSetTile(BaseTileComponent tile){
		//Debug.Log("debugSet");
		Color thisColor;
		thisColor = tile.gameObject.GetComponent<SpriteRenderer>().color;
		tile.gameObject.GetComponent<SpriteRenderer>().color = new Color(thisColor.r,thisColor.g,thisColor.b,1);
		//tile.gameObject.transform.localScale = new Vector3(0.44444444444f * 1.5f,0.44444444444f * 1.5f,1); // TODO Because of placeholder art 
		tile.gameObject.transform.localScale = new Vector3(1.5f,1.5f,1); // TODO Because of placeholder art 
	}
}
