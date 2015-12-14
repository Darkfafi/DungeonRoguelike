using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridAttack {

	public delegate void TileReturnDelegate(DungeonTile tile);

	public event TileReturnDelegate TileInTrailEffect;  //When Trail effect tile is effected
	public event TileReturnDelegate TileInAreaEffect; 	//When Area effect tile is effected (Can be used for lighting the tiles up, flame effects left on tiles etc)
	public event TileReturnDelegate TileInTarget; 		//When Target tile is effected

	public int damage = 1;	//Damage of attack
	public int pushBack = 0; 	//Tiles the target is pushed back after being hit.
	public int range = 5;		//Range of attack
	public bool trailEffect = true; //All tiles between target and caster are also being hit.
	public bool canPassWalls = false; //If it hits a wall then it will continue its path or not.
	public bool canPassObjects = false; // If it hits an object (or enemy or anything else thats not a wall but has an attack catcher)
	public int areaEffect = 0; // tiles around hit tile are effected with dmg

	public int dmgTrailEffectMod = 0; //Mod in dmg if hit by trail effect
	public int dmgAreaEffectMod = 0; //Mod in dmg if hit by area effect

	public void Use(DungeonTile startTile, DungeonTile targetTileGiven,GridHolder gridHolder){
		DungeonTile currentTile = null;
		DungeonTile targetTile = null;
		DungeonTile preTile = null;

		int difX = (int)targetTileGiven.gridPosition.x - (int)startTile.gridPosition.x;
		int difY = (int)targetTileGiven.gridPosition.y - (int)startTile.gridPosition.y;

		if (difX > range) {
			difX = range;
		}
		if (difY > range) {
			difY = range;
		}

		targetTile = gridHolder.GetTile((int)startTile.gridPosition.x + difX,(int)startTile.gridPosition.y + difY);

		int dirX = 0;
		int dirY = 0;

		if (difX != 0) {
			dirX = Mathf.Abs(difX) / difX;
		}
		if (difY != 0) {
			dirY = Mathf.Abs(difY) / difY;
		}

		for (int yRow = 0; yRow < Mathf.Abs(difY) + 1; yRow ++) {
			for(int xRow = 0; xRow < Mathf.Abs(difX) + 1; xRow ++){
				currentTile = gridHolder.GetTile((int)startTile.gridPosition.x +  (xRow * dirX),(int)startTile.gridPosition.y + (yRow * dirY));
				if(currentTile != null){
					if(currentTile != targetTile && (!currentTile.isWall || canPassWalls)){ // TODO If it hits an object and canPassObjects is false then also end attack with red tile
						if(trailEffect){
							// dmg = damage + dmgTrailEffectMod 
							//TODO Check for Attack Catcher on all objects on tile
							AttackObjectsOnTile(GetModGridAttack(dmgTrailEffectMod),currentTile,new Vector2(dirX,dirY));
							currentTile.GetComponent<SpriteRenderer>().color = Color.blue;
							if(TileInTrailEffect != null){
								TileInTrailEffect(currentTile);
							}
						}
					}else{
						//TODO Check for Attack Catcher on all objects on tile
						AttackObjectsOnTile(this,currentTile,new Vector2(dirX,dirY));
						currentTile.GetComponent<SpriteRenderer>().color = Color.red;
						if(TileInTarget != null){
							TileInTarget(currentTile);
						}
						break;
					}
				}
				preTile = currentTile; // can be used if you hit a wall that you calculate the area effect from the pretile and not the currentTile. (STILL AN IDEA!)
			}
		}

		//TODO area effect. Loop from target tile X - range to X + range && Y - Range to Y + range where range == areaEffect variable
	}

	private void AttackObjectsOnTile(GridAttack attack, DungeonTile tile, Vector2 dir){
		List<GameObject> allObjects = tile.objectsOnTile;
		GameObject currentObj;
		GridAttackCatcher attCatcher;
		int l = allObjects.Count;
		for (int i = l - 1; i >= 0; i--) {
			currentObj = allObjects[i];
			attCatcher = currentObj.GetComponent<GridAttackCatcher>();
			if(attCatcher != null){
				attCatcher.CatchAttack(attack,dir);
			}
		}
	}

	private GridAttack GetModGridAttack(int modDmg, int modPushBack = 0){
		GridAttack gridAttack = new GridAttack ();
		gridAttack.damage = damage + modDmg;
		gridAttack.pushBack = pushBack + modPushBack; 	//Tiles the target is pushed back after being hit.
		gridAttack.range = range;		//Range of attack
		gridAttack.trailEffect = trailEffect; //All tiles between target and caster are also being hit.
		gridAttack.canPassWalls = canPassWalls; //If it hits a wall then it will continue its path or not.
		gridAttack.canPassObjects = canPassObjects; // If it hits an object (or enemy or anything else thats not a wall but has an attack catcher)
		gridAttack.areaEffect = areaEffect; // tiles around hit tile are effected with dmg
		
		gridAttack.dmgTrailEffectMod = dmgTrailEffectMod; //Mod in dmg if hit by trail effect
		gridAttack.dmgAreaEffectMod = dmgAreaEffectMod; //Mod in dmg if hit by area effect
		return gridAttack;
	}
}
