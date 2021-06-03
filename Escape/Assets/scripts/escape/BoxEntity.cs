using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxEntity : OpenableEntity {

	Entity m_Content;
	bool m_Closed = true;

	public BoxEntity (EscapeGame game, string name, Entity content, string keyIdentifier, Vector3 pos) :
		base (game, name, keyIdentifier, pos) {

		m_Content = content;
	}

	public override void Inspect () { 

		if (m_Closed) {
			m_Game.Showmsg("A closed box.");

		} else {
			if (m_Content == null) {
				m_Game.Showmsg("An empty box.");

			} else {
				m_Game.Showmsg("Something inside the box:\n");
				m_Content.Inspect ();
			}
		}
	}

	public override void Interact (Entity entity = null) {

		if (m_Closed) {

			m_Closed = false;
			m_Game.Showmsg("The box is opened.");

		} else {

			if (m_Content == null) {

				base.Interact (); 

			} else {

				m_Game.Showmsg("Something inside the box, interact with it:\n");
				m_Content.Interact ();
			}
		}
	}
}