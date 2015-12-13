using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Player : Character {

	//Player Stats
	private LevelingSystem _levelSystem = new LevelingSystem();
	private GridAttack testAttack = new GridAttack ();

	//Bij start van een nieuwe dungeon (niet floor maar daadwerkelijk een gehele dungeon) dan start weer op level 1. (bedenk er een leuke story bij)

	protected override void Start(){
		base.Start ();
		_discoverTiles = true;
		_seeEffectOnTiles = true;
		_attributes = new Attributes(5,3,4,4,2,1,0,8);
	}

	// Update is called once per frame
	void Update () {
		if (_turnUser.inTurn) {
			if (Input.GetKey (KeyCode.LeftArrow)) {
				_faceDir = Vector2.left;
				_movement.Move (Vector2.left);
			}
			if (Input.GetKey (KeyCode.RightArrow)) {
				_faceDir = Vector2.right;
				_movement.Move (Vector2.right);
			}
			if (Input.GetKey(KeyCode.UpArrow)) {
				_faceDir = Vector2.up;
				_movement.Move (Vector2.up);
			}
			if (Input.GetKey (KeyCode.DownArrow)) {
				_faceDir = Vector2.down;
				_movement.Move (Vector2.down);
			}

			if (Input.GetKeyDown (KeyCode.W)) {
				_combat.Attack(currentTileOn,testAttack,_faceDir);
			}
		}
	}
}
