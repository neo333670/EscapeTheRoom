using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenableEntity : Entity {
	
	string m_KeyIdentifier;

	public OpenableEntity (EscapeGame game, string name, string keyIdentifier, Vector3 pos) :
		base (game, name, pos) {

		m_KeyIdentifier = keyIdentifier;
	}

	public override void Inspect () {

		if (string.IsNullOrEmpty (m_KeyIdentifier)) {

			base.Inspect ();
			return;
		}

		Debug.Log ("Use the right key to open this.");
	}

	public override void Interact () {

		if (string.IsNullOrEmpty (m_KeyIdentifier)) {

			Open ();
			return;
		}

		KeyEntity key = Game.TakenEntity as KeyEntity;

		if (key != null) {

			if (key.Identifier == m_KeyIdentifier) {

				Open ();

			} else {

				Debug.Log (string.Format ("This item cannot be opened by the key <color=white>{0}</color>", key.Name));
			}

		} else {

			Debug.Log ("You need a key to open it.");
		}
	}

	protected virtual void Open () { 

		Debug.Log ("Succeed to open the item, but nothing happened.");
	}
}
