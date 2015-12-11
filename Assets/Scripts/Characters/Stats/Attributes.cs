using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Attributes {

	private int _pointsToSpend = 0;

	//private var _allStats : Array = [];
	// * = (inc chance to successfully perform) 
	private BaseAttribute _strength; 	//This will be effecting damage done with melee weapons. Also throw damage and distance.
	private BaseAttribute _endurence; 	//This will be effecting health and resistance against debufs and sickness.
	private BaseAttribute _accuracy; 	//This will be effecting the chance of hitting a target with a melee and ranged attacks(higher effect then luck) *
	private BaseAttribute _dexterity; 	//This will be effecting Evation, Blocking chance after the effect or the reflex stat. *
	private BaseAttribute _slightOfHand;//This will be effecting Stealing success and disarming enemies in a surprise attack or reflex. *
	private BaseAttribute _reflex; 		//This will be effecting the chance you have on seeing an incomming attack and get the option to evade,counter, block etc. * 
	private BaseAttribute _luck; 		//This will be effecting every positive chance. This includes: Reflex effect, Hitting enemy, finding items, evading and blocking incomming attacks etc. *
	private BaseAttribute _eyeSight;    //This will be effecting how far the character can see.

	public Attributes(int baseStr = 0,int baseEnd = 0,int baseAcc = 0,int baseDex = 0,int baseSoH = 0, int baseReflex = 0, int baseLuck = 0, int baseSight = 5){
	
		_strength 			 = new BaseAttribute ("Strength", baseStr);
		_endurence 			= new BaseAttribute ("Endurence", baseEnd);
		_accuracy 			 = new BaseAttribute ("Accuracy", baseAcc);
		_dexterity 			= new BaseAttribute ("Dexterity", baseDex);
		_slightOfHand  = new BaseAttribute ("Slight of Hand", baseSoH);
		_reflex 			= new BaseAttribute ("Reflex", baseReflex);
		_luck 					= new BaseAttribute ("Luck", baseLuck);
		_eyeSight 			  = new BaseAttribute ("Sight", baseSight);
	}

	// Getters

	public BaseAttribute strength{
		get{return _strength;}
	}
	public BaseAttribute endurence{
		get{return _endurence;}
	}
	public BaseAttribute accuracy{
		get{return _accuracy;}
	}
	public BaseAttribute dexterity{
		get{return _dexterity;}
	}
	public BaseAttribute slightOfHand{
		get{return _slightOfHand;}
	}
	public BaseAttribute reflex{
		get{return _reflex;}
	}
	public BaseAttribute luck{
		get{return _luck;}
	}
	public BaseAttribute sight{
		get{return _eyeSight;}
	}
}
