using UnityEngine;
using System.Collections;

public class TurnSystem : MonoBehaviour {

	private TurnSystemHandeler _turnSystemHandeler = new TurnSystemHandeler();

	public TurnSystemHandeler turnSystemHandeler{
		get{return _turnSystemHandeler;}
	}
}
