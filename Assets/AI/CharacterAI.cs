using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterAI : Character {

	public bool attacksPlayer = true;
	public bool attacksOwnKind = false;
	public bool attacksOtherKind = false;

	protected Character _targetCharacter = null;
}
