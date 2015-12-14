using UnityEngine;
using System.Collections;

public class GridAttackCatcher : MonoBehaviour {

	public delegate void AttackVec2Delegate(GridAttack attack, Vector2 dir);

	public event AttackVec2Delegate AttackCatched;

	public void CatchAttack(GridAttack attack, Vector2 directionAttackedFrom){
		if (AttackCatched != null) {
			AttackCatched(attack,directionAttackedFrom);
		}
	}
}
