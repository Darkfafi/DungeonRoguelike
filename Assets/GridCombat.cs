using UnityEngine;
using System.Collections;

public class GridCombat : MonoBehaviour {

	public GridHolder gridHolder; //TODO must replace all of these with a static grid that is changed on level change

	public void Attack(DungeonTile casterTile,GridAttack attack, Vector2 direction){ // Give attackClass, & direction (AttackClass has all info about area effect,distance etc...STILL A CONCEPT)
		//Attack(attack,tile(dir.x + range, dir.y + range));
		DungeonTile targetTile = null;
		for (int i = attack.range; i >= 0; i--) {
			targetTile = gridHolder.GetTile ((int)casterTile.gridPosition.x +(int)direction.x * i,(int)casterTile.gridPosition.y + (int)direction.y * i); 
			if(targetTile != null){
				break;
			}
		}
		Attack (casterTile,attack, targetTile);
	}
	public void Attack(DungeonTile casterTile,GridAttack attack, DungeonTile targetTile){
		//dir = targetPos - attackerPos;
		//startTile = Tile(casterTile.x + 	dir.x, casterTile.y + dir.y)
		Vector2 dif = targetTile.gridPosition - casterTile.gridPosition;
		Vector2 dir = new Vector2 (Mathf.Abs (dif.x) / dif.x, Mathf.Abs (dif.y) / dif.y);
		DungeonTile startTile = gridHolder.GetTile ((int)casterTile.gridPosition.x + (int)dir.x, (int)casterTile.gridPosition.y + (int)dir.y);
		//startTile.GetComponent<SpriteRenderer> ().color = Color.red;
		attack.Use (startTile, targetTile, gridHolder);
	}
}
