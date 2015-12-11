using UnityEngine;
using System.Collections;

public class EnemyFactory {

	//Pirates
	public const string PIRATE_MELEE_STRONG = "StrongMeleePriate";
	public const string PIRATE_RANGED_RIFLE = "RangedRiflePirate";

	public GameObject GetEnemy(string enemyString){
		GameObject enemy = Resources.Load<GameObject> ("Prefabs/Object");
		Sprite spriteEnemy = null;
		//enemy.AddComponent<BaseEnemy> ();

		switch (enemyString) {
			case PIRATE_RANGED_RIFLE:
				spriteEnemy = Resources.Load<Sprite>("Art/jp");//TODO add sprite of enemy to);
				enemy.AddComponent<MeleeEnemy>();//TODO Add melee combat ai and other components to this enemy>
			break;
			case PIRATE_MELEE_STRONG:
				spriteEnemy = Resources.Load<Sprite>("Art/jp");
				enemy.AddComponent<RangedEnemy>();
			break;
		}

		enemy.GetComponent<SpriteRenderer>().sprite = spriteEnemy;

		return enemy;
	}
}
