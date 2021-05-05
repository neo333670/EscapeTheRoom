using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeGame {

    public delegate void EscapeMessageEvent(string message);
    public delegate void EScapeGameEvent(EscapeGame game);
    public delegate void EscapeGameEntityEvent(Entity entity);

    public event EscapeMessageEvent OnMessageAdded = (m) => { };

    public event EscapeGameEntityEvent OnEntitySelected = (e) => { };
    public event EscapeGameEntityEvent OnEntityDeselected = (e) => { };
    public event EscapeGameEntityEvent OnEntityInspected = (e) => { };
    public event EscapeGameEntityEvent OnEntityInteracted = (e) => { };
    public event EscapeGameEntityEvent OnEntityTaken = (e) => { };
    public event EscapeGameEntityEvent OnEntityReleased = (e) => { };

    public event EScapeGameEvent OnGameStarted = (g) => { };
    public event EScapeGameEvent OnGameOver = (g) => { };
    public event EScapeGameEvent OnGameFinished = (g) => { };

	List<Entity> m_Entities = new List<Entity>();
	public List<Entity> Entities { get { return m_Entities; } }

	int m_SelectedIndex = -1;
	Entity m_SelectedEntity = null;

	Entity m_TakenEntity = null;
	public Entity TakenEntity  { get { return m_TakenEntity; } }

	public EscapeGame () {
	
		//MakeGame ();

		Debug.Log ("You are in a locked room. Do something to escape!");
		Debug.Log ("Press 'N' to select item; " +
			"'R' to putback taken item; " +
			"'Space' to inspect selected item; " +
			"'Enter' to interact with the selected item.");
	}

	void Finish () {

		Debug.Log ("Thanks for playing the game!");
		UnityEditor.EditorApplication.isPlaying = false;
	}

	public void MakeGame () {
	
		m_Entities.Add (new Entity (this, "Basketball", new Vector3(1, 0, 0)));
		m_Entities.Add (new Entity (this, "Chair", new Vector3(3, 0, 1)));
		m_Entities.Add (new Entity (this, "Cup", new Vector3(5, 0, 2)));
		m_Entities.Add (new KeyEntity (this, "Key A", "123", new Vector3(7, 0, 4)));
		m_Entities.Add (new KeyEntity (this, "Key B", "124", new Vector3(9, 0, 5)));
		m_Entities.Add (new DoorEntity (this, "Door A", null, new Vector3(-1, 0, 4)));
		m_Entities.Add (new DoorEntity (this, "Door B", null, new Vector3(-2, 0, 6)));
		m_Entities.Add (new MonsterDoorEntity (this, "Door C", "123", new Vector3(-3, 0, 0)));
		m_Entities.Add (new ExitDoorEntity (this, "Door D", "124", new Vector3(-4, 3, 0)));
		m_Entities.Add (new BoxEntity (this, "Box A", null, null, new Vector3(-5, 4, 0)));
		m_Entities.Add (new BoxEntity (this, "Box B", new KeyEntity (this, "Key C", "125", new Vector3(6, 0, 0)), null, new Vector3(6, 0, 0)));
		m_Entities.Add (new PaperEntity (this, "Paper A", "Find a key to escape the room.", new Vector3(-6, 0, 0)));

		OnGameStarted(this);
	}

	public void Inspect () {
		
		if (m_SelectedEntity != null) {

			Debug.Log (string.Format ("Inspect item <color=white>{0}</color>", m_SelectedEntity.Name));
			m_SelectedEntity.Inspect ();
		
		} else {
		
			Debug.Log ("You have to select a item first.");
		}
	}

	public void Interact () {

		if (m_SelectedEntity != null) {

			Debug.Log (string.Format ("Interact with item <color=white>{0}</color>", m_SelectedEntity.Name));
			m_SelectedEntity.Interact ();
		
		} else {

			Debug.Log ("You have to select a item first.");
		}
	}

	public void SelectNext () {

		if (m_Entities.Count == 0) {

			Deselect ();
			Debug.Log ("There is nothing in this room.");
			return;
		}

		if (++m_SelectedIndex >= m_Entities.Count) {

			m_SelectedIndex = 0;
		}

		m_SelectedEntity = m_Entities [m_SelectedIndex];

		Debug.Log (string.Format ("<color=white>{0}</color> has been selected.", m_SelectedEntity.Name));
	}

	public void Take (Entity entity) {

		if (m_TakenEntity != null) {

			Debug.Log (string.Format ("You already take item <color=white>{0}</color>", m_TakenEntity.Name));

		} else {

			Debug.Log (string.Format ("Take item <color=white>{0}</color>, press 'R' to put back.", entity.Name));

			m_TakenEntity = entity;
			m_Entities.Remove (entity);

			Deselect ();
		}
	}

	public void PutBack () {
	
		if (m_TakenEntity != null) {

			Debug.Log (string.Format ("Put item <color=white>{0}</color> back.", m_TakenEntity.Name));
			m_Entities.Add (m_TakenEntity);

			m_TakenEntity = null;

		} else {
		
			Debug.Log ("You have nothing to put back.");
		}
	}

	void Deselect () {
	
		m_SelectedIndex = -1;
		m_SelectedEntity = null;
	}

	public void Escape () {
	
		Debug.Log ("<color=green>Congrats! You escape the room!</color>");
		Finish ();
	}

	public void Die () {

		Debug.Log ("<color=red>Oops! You died.</color>");
		Finish ();
	}

	public void SelectEntity(Entity entity) {
		if (m_SelectedEntity == entity) {
			return;
		}
		if (m_SelectedEntity != null) {
			m_SelectedEntity.DeSelect();
			OnEntityDeselected(m_SelectedEntity);

			m_SelectedEntity = null;
		}
		if (entity != null) {
			m_SelectedEntity = entity;
			m_SelectedEntity.Select();

			OnEntitySelected(m_SelectedEntity);
		}
	}
}
