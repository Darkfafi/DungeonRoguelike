using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Character : GridObject {

	protected string _type = "Default"; // TODO must be set to; Human, Orc, Dog etc etc... (This way dogs can choose to attack everything except their own kind (type))

	protected GridMovement _movement;
	protected GridCombat _combat;
	protected TurnUser _turnUser;
	
	protected Sight _sight;
	protected bool _discoverTiles = false;
	protected bool _seeEffectOnTiles = false;
	protected Vector2 _faceDir = Vector2.left;
	
	//Player Stats
	protected Attributes _attributes = new Attributes();
	protected Attributes _modAttributes = new Attributes ();
	protected Stats _stats = new Stats();
	
	//Bij start van een nieuwe dungeon (niet floor maar daadwerkelijk een gehele dungeon) dan start weer op level 1. (bedenk er een leuke story bij)
	
	// Use this for initialization
	void Awake () {
		_sight = gameObject.AddComponent<Sight> ();
		_turnUser = gameObject.AddComponent<TurnUser> ();
		_movement = gameObject.AddComponent<GridMovement> ();
		_combat = gameObject.AddComponent<GridCombat> ();
		_movement.MoveSetPosition += CharacterMoved;
		_turnUser.UserGainedTurn += TurnGained;
	}
	
	protected virtual void Start(){
		_turnUser.autoSignInAfterPass = true;
		_turnUser.totalSubTurns = 2;
		_turnUser.SignInForTurn ();
	}
	
	protected void CharacterMoved(int x, int y){
		SetPositionData (x, y);
		DidAction ();
	}

	private void TurnGained(TurnUser user){
		See ();
	}

	protected void DidAction(int subTurnWeight = 1){
		See ();
		_turnUser.PassSubTurn(subTurnWeight);
	}

	private void See(){
		// TODO maybe if blinded then make sight distance 1 or 0 to simulate sight loss.
		if (gridHolder != null) {
			_sight.SeeCone (gridPositionX, gridPositionY, gridHolder, _faceDir, _attributes.sight.statPoints, 2, true, _discoverTiles, _seeEffectOnTiles);
		}
	}

	public override void SetToGridPosition (int x, int y)
	{
		base.SetToGridPosition (x, y);
		See ();
	}

	public override void SetGridHolder (GridHolder gh)
	{
		base.SetGridHolder (gh);
		_movement.gridHolder = gh;
		_combat.gridHolder = gh;
	}
}
