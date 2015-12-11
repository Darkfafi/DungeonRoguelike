using UnityEngine;
using System.Collections;

public class TurnUser : MonoBehaviour {

	public delegate void PassUserInfo(TurnUser user);

	public event PassUserInfo UserPassedTurn;
	public event PassUserInfo UserPassedSubTurn;
	public event PassUserInfo UserGainedTurn;
	public event PassUserInfo UserSignedInForTurn;
	public event PassUserInfo UserSignedOutForTurn;

	private TurnSystemHandeler _turnHandeler;

	public bool autoSignInAfterPass = false;

	public int turnsInQueue = 0;
	private int _value = 0; // Als dit hoger is dan een ander object dan heeft deze voorang.

	public int totalSubTurns = 1;
	private int _subTurnsPassed = 0;

	private bool _inTurn = false;


	void Awake(){
		turnHandeler = GameObject.FindGameObjectWithTag (Tags.TURN_SYSTEM).GetComponent<TurnSystem>().turnSystemHandeler;
	}

	public void PassSubTurn(int amount = 1){
		if (UserPassedSubTurn != null) {
			UserPassedSubTurn(this);
		}
		_subTurnsPassed += amount;
		if (_subTurnsPassed == totalSubTurns) {
			_subTurnsPassed = 0;
			PassTurn();
		}
	}

	//Dit object heeft zijn beurt gedaan en geeft het door.
	private void PassTurn(){
		_inTurn = false;
		_turnHandeler.CurrentUserDoneWithTurn (this);
		if (UserPassedTurn != null) {
			UserPassedTurn(this);
		}
		if (autoSignInAfterPass) {
			SignInForTurn();
		}
	}
	//Turn system geeft dit object e beurt.
	public void GainTurn(){
		_inTurn = true;
		if (UserGainedTurn != null) {
			UserGainedTurn(this);
		}
	}

	//Vragen voor de vlgende beurt. (in de queue komen)
	public void SignInForTurn(){
		_turnHandeler.SignInForTurn (this);
		if (UserSignedInForTurn != null) {
			UserSignedInForTurn(this);
		}
	}
	//Het verlaten van de beurt queue
	public void SignOutForTurn(){
		_turnHandeler.SignOutForTurn (this);
		turnsInQueue = 0;
		if (UserSignedOutForTurn != null) {
			UserSignedOutForTurn (this);
		}
	}

	public TurnSystemHandeler turnHandeler{
		get{return _turnHandeler;}
		set{
			if(_turnHandeler != null){
				_turnHandeler.UserPassedTurn -= GlobalTurnPassed;
			}
			_turnHandeler = value;
			_turnHandeler.UserPassedTurn += GlobalTurnPassed;
		}
	}
	private void GlobalTurnPassed(TurnUser user){
		turnsInQueue++;
	}

	public int value{
		get{return _value;}
	}
	public bool inTurn{
		get{return _inTurn;}
	}
	public int subTurnsPassed{
		get{return _subTurnsPassed;}
	}
}
