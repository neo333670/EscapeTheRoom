using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyEntity : Entity {

	string m_KeyIdentifier;

	public string Identifier { get { return m_KeyIdentifier; } }

	public KeyEntity (EscapeGame game, string name, string keyIdentifier, Vector3 pos) : 
		base (game, name, pos) {

		m_KeyIdentifier = keyIdentifier;
	}

	public override void Inspect () {

		m_Game.Showmsg("A key for something. Maybe can be used later.");
	}

	public override void Interact (Entity entity = null) {

		Game.Take (this);
	}
}
