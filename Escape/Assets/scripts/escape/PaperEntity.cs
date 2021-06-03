using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperEntity : Entity {

	string m_Content;

	public PaperEntity (EscapeGame game, string name, string content, Vector3 pos) :
		base (game, name, pos) {
	
		m_Content = content;
	}

	public override void Inspect () {

		m_Game.Showmsg("There is something on the paper.");
	}

	public override void Interact (Entity entity = null) {

		m_Game.Showmsg(string.Format ("Read the paper:<color=white>{0}</color>", m_Content));
	}
}
