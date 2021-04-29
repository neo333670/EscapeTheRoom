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

		Debug.Log ("There is something on the paper.");
	}

	public override void Interact () {

		Debug.Log (string.Format ("Read the paper:<color=white>{0}</color>", m_Content));
	}
}
