using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeGameScene : MonoBehaviour {

	EscapeGame m_Game;
	#region Message Event Handlers
	void HandleOnMessageAdded(string message) { }
    #endregion

    #region Entity Event Handlers
	void HandleOnEntitySelected(Entity entity) { }
	void HandleOnEntityDeselected(Entity entity) { }
	void HandleOnEntityInspected(Entity entity) { }
	void HandleOnEntityInteracted(Entity entity) { }
	void HandleOnEntityTaken(Entity entity) { }
	void HandleOnEntityReleased(Entity entity) { }
	#endregion

	#region Game Event Handlers
	void HandleOnGameStrated(EscapeGame game) {
		foreach (var e in m_Game.Entities) { 
		}
	}
	void HandleOnGameFinished(EscapeGame game) { }
	void HandleOnGameOver(EscapeGame game) { }
    #endregion
    private void Awake()
    {
		m_Game = new EscapeGame();
		m_Game.OnGameStarted += HandleOnGameFinished;
		m_Game.OnGameFinished += HandleOnGameFinished;
		m_Game.OnGameOver += HandleOnGameOver;

		m_Game.OnMessageAdded += HandleOnMessageAdded;

		m_Game.OnEntitySelected += HandleOnEntitySelected;
		m_Game.OnEntityDeselected += HandleOnEntityDeselected;
		m_Game.OnEntityInspected += HandleOnEntityInspected;
		m_Game.OnEntityInteracted += HandleOnEntityInteracted;
		m_Game.OnEntityTaken += HandleOnEntityTaken;
		m_Game.OnEntityReleased += HandleOnEntityReleased;
    }

    void Update () {

		if (Input.GetKeyDown (KeyCode.N)) {

			m_Game.SelectNext ();

		} else if (Input.GetKeyDown (KeyCode.Space)) {

			m_Game.Inspect ();

		} else if (Input.GetKeyDown (KeyCode.Return)) {

			m_Game.Interact ();

		} else if (Input.GetKeyDown (KeyCode.R)) {

			m_Game.PutBack ();
		}
	}
}
