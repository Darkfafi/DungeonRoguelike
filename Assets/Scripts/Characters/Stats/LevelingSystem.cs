using UnityEngine;
using System.Collections;

public class LevelingSystem {

	// send events around on level up, current level etc. So UI, Attributes and all can react to it. Attributes will generate points to spend  etc

	private int _currentLevel; // What level is the holder
	private int _maxLevel; // What level is the max level the level holder can be.

	private int _experiencePoints; //how much experience points does the holder have
	private int _nextLevelExperiencePoints; //how much exp does he need to reach his next level.
	private int _onLevelUpExpMultiplier; // With how much does the next level experiencePoints multiply on level up. (also a parameter if this number needs to change on level up)
}
