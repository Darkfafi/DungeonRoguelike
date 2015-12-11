using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnSystemHandeler{

	public delegate void PassUserInfo(TurnUser user);
	
	public event PassUserInfo UserPassedTurn;
	public event PassUserInfo UserGainedTurn;
	public event PassUserInfo UserSignedInForTurn;
	public event PassUserInfo UserSignedOutForTurn;

	// all objects qeued for a turn in a list. If turn ends, then give turn to next object that wil end is turn after his doing...
	//(Look for all objects with turn script on it. then in that script they have the power to pass a turn and more.)
	List<TurnUser> _usersToGiveTurnTo = new List<TurnUser>();
	int passedTurns = 0;
	TurnUser _currentTurnUser;

	public void CurrentUserDoneWithTurn(TurnUser user){
		if (user == _currentTurnUser) {
			PassTurnToFirstUser ();
		} else {
			Debug.LogError("Given user is not in his turn. Please give current TurnUser");
		}
	}
	public void SignInForTurn(TurnUser user){
		_usersToGiveTurnTo.Add (user);
		SortListOnValue ();
		if (UserSignedInForTurn != null) {
			UserSignedInForTurn(user);
		}

		if (_usersToGiveTurnTo.Count == 1 && _currentTurnUser == null) {
			PassTurnToFirstUser();
		}
	}
	public void SignOutForTurn(TurnUser user){
		_usersToGiveTurnTo.Remove (user);
		if (UserSignedOutForTurn != null) {
			UserSignedOutForTurn(user);
		}
	}
	public void PassTurnToFirstUser(){
		if (UserPassedTurn != null) {
			UserPassedTurn(_currentTurnUser);
		}

		_currentTurnUser = null;

		passedTurns ++;
		if (_usersToGiveTurnTo.Count > 0) {
			TurnUser firstUserInQueue = _usersToGiveTurnTo [0];
			firstUserInQueue.GainTurn ();
			_currentTurnUser = firstUserInQueue;
			firstUserInQueue.SignOutForTurn ();

			if (UserGainedTurn != null) {
				UserGainedTurn (firstUserInQueue);
			}
		}
	}

	private void SortListOnValue(){
		List<TurnUser> newList = new List<TurnUser>();
		for (int i = 0; i < _usersToGiveTurnTo.Count; i++) {
			if(newList.Count == 0){
				newList.Add(_usersToGiveTurnTo[i]); 
			}else if(newList[0].value < _usersToGiveTurnTo[i].value && newList[0].turnsInQueue < _usersToGiveTurnTo[i].turnsInQueue){
				newList.Insert(0,_usersToGiveTurnTo[i]);
			}else{
				newList.Add(_usersToGiveTurnTo[i]);
			}
		}
		_usersToGiveTurnTo = newList;
	}
}
