using UnityEngine;
using System.Collections;

public class Stats {

	//no lose life things here. This is only a holder for the info. Other components and all can read it for condition purposes.

	public int health; //Dit is je max health <3

	public int baseMeleeDmg;// Damage done by melee weapons
	public int baseThrowRange; // Throw range 
	
	public int meleeHitChance;
	public int rangedHitChance;
	

	// All Reflex moment variables.
	public int evasionChance;
	public int blockChance;

	public int stealingChance;
	public int disarmingChance;


	public int reflex; // will give a value. This value is then changed by the enemy his stats and then used.

	public int luck; // inc calculations of the other chances.
}
