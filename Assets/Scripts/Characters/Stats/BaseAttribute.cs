using UnityEngine;
using System.Collections;

public class BaseAttribute {

	private string _nameAttribute = "";
	private int _pointsInStat = 0;
	//send events for changes in the attribute.
	//Attributes holder contains all the descriptions.

	public BaseAttribute(string name,int startPoints){
		_nameAttribute = name;
		_pointsInStat = startPoints;
	}

	public int statPoints{
		get{return _pointsInStat;}
	}
}
