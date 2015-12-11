using UnityEngine;
using System.Collections;

public class SightTile : BaseTileComponent {

	// seeAble, undiscovered. discovered...
	//private Vector2 _gridPosition = new Vector2();// aan de hand van zijn grid position wordt weet hij welke tile onder hem zit.  
	private bool _discovered = false;
	private bool _beingSeen = false;

	private SpriteRenderer sprRend;
	private Color _setColor = new Color(1,1,1,1);


	void Awake(){
		sprRend = gameObject.GetComponent<SpriteRenderer> ();
		sprRend.sortingOrder = 3;
	}

	public void SetDiscovered(bool value,BaseTileComponent tileUnder){
		if (value) {
			sprRend.sprite = tileUnder.gameObject.GetComponent<SpriteRenderer>().sprite;
			_setColor = new Color(0.75f,0.75f,0.75f,1);
		} else {
			sprRend.sprite = Resources.Load<Sprite>("Art/Tiles/tiles-15");
			_setColor = new Color(1,1,1,1);
		}
		if (!_beingSeen) {
			sprRend.color = _setColor;
		}
		_discovered = value;
	}

	// iets kan gezien worden maar niet discovered zijn door een 'spel' die tijdelijk dngen ziet ofzo. Anywho klopt de logica
	public void SetBeingSeen(bool value){
		if (value) {
			sprRend.color = new Color(1,1,1,0);
		} else {
			sprRend.color = _setColor; // discovered zet de image op wat het was
		}
		_beingSeen = value;
	}

	public bool discovered{
		get{return _discovered;}
	}
	public bool beingSeen{
		get{return _beingSeen;}
	}
}
