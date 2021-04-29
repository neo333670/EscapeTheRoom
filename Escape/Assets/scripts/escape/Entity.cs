using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity {

	public delegate void EntityEvent(Entity e);

	public event EntityEvent OnSelected = (e) => { };
	public event EntityEvent OnDeselected = (e) => { };
	public event EntityEvent OnTaken = (e) => { };

	protected string m_Prefabs = "entity_cube";
	public string Prefabs { get { return m_Prefabs; } }

	Vector3 m_Positon;
	public Vector3 Position { get { return m_Positon; } }

	protected string m_Name;
	public string Name { get { return m_Name; } }

	EscapeGame m_Game;
	public EscapeGame Game { get { return m_Game; } }

	public Entity (EscapeGame game, string name, Vector3 pos) {

		m_Game = game;
		m_Name = name;
		m_Positon = pos;
	}
		
	public virtual void Inspect () { 

		Debug.Log ("Hmm...nothing special.");
	}

	public virtual void Interact () {
	
		Debug.Log ("Nothing happened.");
	}
}