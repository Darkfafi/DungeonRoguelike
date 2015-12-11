using UnityEngine;
using System.Collections;

public class StatCalculator : MonoBehaviour {
	
	public static void CalculateStats(Attributes attr, Stats stats){
		stats.health = attr.endurence.statPoints;

		stats.baseMeleeDmg = 2 + attr.strength.statPoints;
		stats.baseThrowRange = 2 + (int)(attr.strength.statPoints * 0.3f);

		stats.meleeHitChance = attr.accuracy.statPoints;
		stats.rangedHitChance = attr.accuracy.statPoints;

		stats.blockChance = attr.dexterity.statPoints;
		stats.evasionChance = attr.dexterity.statPoints;

		stats.disarmingChance = (int)((attr.dexterity.statPoints * 0.1f) + (attr.slightOfHand.statPoints * 0.1f));
		stats.stealingChance = attr.slightOfHand.statPoints;

		stats.reflex = attr.reflex.statPoints;
		stats.luck = attr.luck.statPoints;
	}
}
