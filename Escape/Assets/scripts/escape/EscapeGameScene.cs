using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeGameScene : MonoBehaviour {
	[SerializeField] GameUI m_GameUI;

	EscapeGame m_Game;
	public EscapeGame Game { get { return m_Game; } }

	public const float SELECT_RANGE = 10f;

	#region Message Event Handlers
	void HandleOnMessageAdded(string message) {
		m_GameUI.ShowMessage(message);
	}
    #endregion

    #region Entity Event Handlers
	void HandleOnEntitySelected(Entity entity) {
		m_GameUI.SetActionVisible(true);
	}
	void HandleOnEntityDeselected(Entity entity) {
		m_GameUI.SetActionVisible(false);
	}
	void HandleOnEntityInspected(Entity entity) { }
	void HandleOnEntityInteracted(Entity entity) { }

	void HandleOnEntityTaken(Entity entity) {
		m_GameUI.SetActionVisible(false);
		m_GameUI.UpdateDropdownList();
	}
	void HandleOnEntityReleased(Entity entity) {
		m_GameUI.UpdateDropdownList();

		CreateEntityBehav(entity);
	}
	#endregion

	#region Game Event Handlers
	void HandleOnGameStrated(EscapeGame game) {
		foreach (var e in m_Game.Entities) {
			//create Entities GameObject here
			CreateEntityBehav(e);
		}
	}
	void HandleOnGameFinished(EscapeGame game) { }
	void HandleOnGameOver(EscapeGame game) { }
	#endregion

	void CreateEntityBehav(Entity e) {
		var entityBehav = GameObject.Instantiate(
		Resources.Load<EntityBehav>("Prefabs/" + e.Prefabs));
		entityBehav.transform.position = e.Position;
		entityBehav.UpdateEntity(e);
	}
    private void Awake()
    {
		m_Game = new EscapeGame();
		m_Game.OnGameStarted += HandleOnGameStrated;
		m_Game.OnGameFinished += HandleOnGameFinished;
		m_Game.OnGameOver += HandleOnGameOver;

		m_Game.OnMessageAdded += HandleOnMessageAdded;

		m_Game.OnEntitySelected += HandleOnEntitySelected;
		m_Game.OnEntityDeselected += HandleOnEntityDeselected;
		m_Game.OnEntityInspected += HandleOnEntityInspected;
		m_Game.OnEntityInteracted += HandleOnEntityInteracted;
		m_Game.OnEntityTaken += HandleOnEntityTaken;
		m_Game.OnEntityReleased += HandleOnEntityReleased;

		m_Game.MakeGame();
    }

    void Update () {

		if (Input.GetKeyDown(KeyCode.P)) {
			DetectByplayer();
		}

		if (Input.GetKeyDown (KeyCode.N)) {

			m_Game.SelectNext ();

		} else if (Input.GetKeyDown (KeyCode.Space)) {

			m_Game.Inspect ();

		} else if (Input.GetKeyDown (KeyCode.Return)) {

			m_Game.Interact ();

		} else if (Input.GetKeyDown (KeyCode.R)) {

			m_Game.PutBack ();
		}
		if (Input.GetMouseButtonDown(0)) {
			DetectByMouse();
		}	
	}

	void DetectByplayer()
	{
		Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
		bool detectEntity = false;

		RaycastHit raycastHitResult;

		if (Physics.Raycast(ray, out raycastHitResult))
		{
			if (raycastHitResult.distance < SELECT_RANGE)
			{
				var entityBehav = raycastHitResult.collider.GetComponent<EntityBehav>();

				if (entityBehav != null)
				{
					detectEntity = true;
					m_Game.SelectEntity(entityBehav.Entity);
				}
			}
		}
		if (!detectEntity)
		{
			SelectNothing();
		}
	}

	void DetectByMouse() {

        bool detectEntity = false;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit))
		{
			var entityBehav = hit.collider.GetComponent<EntityBehav>();

			if (entityBehav != null)
			{
				detectEntity = true;
				m_Game.SelectEntity(entityBehav.Entity);
			}
		}
		if (!detectEntity) { SelectNothing(); }

    }

	void SelectNothing() {
		m_Game.SelectEntity(null);
	}
}
