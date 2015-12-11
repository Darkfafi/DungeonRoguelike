using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Sight : MonoBehaviour {

	public static Vector2 SIGHT_UP = new Vector2 (0, 1);
	public static Vector2 SIGHT_UP_RIGHT = new Vector2 (1, 1);
	public static Vector2 SIGHT_UP_LEFT = new Vector2 (-1, 1);

	public static Vector2 SIGHT_DOWN = new Vector2 (0, -1);
	public static Vector2 SIGHT_DOWN_RIGHT = new Vector2 (1, -1);
	public static Vector2 SIGHT_DOWN_LEFT = new Vector2 (-1, -1);

	public static Vector2 SIGHT_LEFT = new Vector2(-1,0);
	public static Vector2 SIGHT_RIGHT = new Vector2 (1, 0);

	private List<Vector2> _viewDirections = new List<Vector2>();//..........
	private List<DungeonTile> _tilesInView = new List<DungeonTile> ();
	// if wall. see wall then break loop
	//Also directions?...

	public void See(int x, int y,GridHolder gridHolder,List<Vector2> viewDirections,int viewDistance = 4,bool clearLastSee = true,bool discoverTile = true, bool seeTileEffect = true){
		DungeonTile tile;
		if (clearLastSee) {
			for (int i = _tilesInView.Count - 1; i >= 0; i--) {
				_tilesInView [i].fogTile.SetBeingSeen (false);
			}
			_tilesInView = new List<DungeonTile> ();
		}
		for (int i = 0; i < viewDirections.Count; i++) {
			for(int j = 0; j <= viewDistance; j++){
				tile = gridHolder.GetTile((int)(x + (viewDirections[i].x * j)),(int)(y + (viewDirections[i].y * j)));
				if(tile != null){
					tile.fogTile.SetBeingSeen(seeTileEffect);
					_tilesInView.Add(tile);
					if(discoverTile){
						tile.fogTile.SetDiscovered(true,tile);
					}
					if(tile.isWall){
						break;
					}
				}else{
					break;
				}
			}
		}
	}

	public void SeeCone(int x, int y,GridHolder gridHolder,Vector2 viewDirection,int viewDistance = 4,int viewWidth = 0,bool clearLastSee = true,bool discoverTile = true,bool seeTileEffect = true){
		SeeConeHalf (x, y, gridHolder, viewDirection, 1, viewDistance, viewWidth, clearLastSee, discoverTile,seeTileEffect);
		SeeConeHalf (x, y, gridHolder, viewDirection, -1, viewDistance, viewWidth, false, discoverTile,seeTileEffect);
	}

	private void SeeConeHalf(int x, int y,GridHolder gridHolder,Vector2 viewDirection,int coneHalfDirection,int viewDistance = 4,int viewWidth = 0,bool clearLastSee = true,bool discoverTile = true,bool seeTileEffect = true){
		List<Vector2> dir = new List<Vector2>(){};
		DungeonTile startTileCheck;
		Vector2 tileFormula = new Vector2 ();
		Vector2 tileFormulaWallSolution = new Vector2 ();
		int widthSet = 0;
		int wallsHit = 0;
		int set = 0;
		bool hitWall = false;
		dir.Add(viewDirection);

		if (clearLastSee) {
			See(x,y,gridHolder,dir,0); //TODO SEE ST
		}

		for(int i = 0; i < viewDistance + viewWidth; i++){
			tileFormula = new Vector2();
			widthSet = i - viewWidth;

			if(widthSet < 0){
				widthSet = 0;
			}

			if(wallsHit > 0){
				set = 1;
			}

			if(viewDirection.y != 0){
				tileFormula = new Vector2(x + ((i - (wallsHit)) * coneHalfDirection),y+ (int)(widthSet * viewDirection.y));
			}
			else if(viewDirection.x != 0){
				tileFormula = new Vector2(x + (int)(widthSet * viewDirection.x),y + ((i - (wallsHit)) * coneHalfDirection));
			}
			
			startTileCheck = gridHolder.GetTile((int)tileFormula.x,(int)tileFormula.y);

			if(startTileCheck != null){

				if(!startTileCheck.isWall && hitWall){
					hitWall = false;
				}

				if(startTileCheck.isWall && hitWall){
					break;
				}
					
				See((int)startTileCheck.gridPosition.x,(int)startTileCheck.gridPosition.y,gridHolder,dir,viewDistance - i,false,discoverTile,seeTileEffect);
				if(startTileCheck.isWall){
					hitWall = true;
					wallsHit++;
					i -= 1;
				}
			}else {break;}
		}
	}

	public List<DungeonTile> tilesInView{
		get{return _tilesInView;}
	}
}
