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

		m_Game.Showmsg("Use the right key to open this.");
	}

	public override void Interact (Entity entity = null) {

		if (string.IsNullOrEmpty (m_KeyIdentifier)) {

			Open ();
			return;
		}

		KeyEntity key = Game.TakenEntities[0] as KeyEntity;

		if (key != null) {

			if (key.Identifier == m_KeyIdentifier) {

				Open ();

			} else {

				m_Game.Showmsg(string.Format ("This item cannot be opened by the key <color=white>{0}</color>", key.Name));
			}

		} else {

			m_Game.Showmsg("You need a key to open it.");
		}
	}

	protected virtual void Open () {

		m_Game.Showmsg("Succeed to open the item, but nothing happened.");
	}
}
