using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehav : MonoBehaviour
{
    MeshRenderer m_Render;

    Entity m_Entity;
    public Entity Entity { get { return m_Entity; } }

    // Start is called before the first frame update
    void Start()
    {
        m_Render = this.GetComponent<MeshRenderer>();
    }

    public void UpdateEntity(Entity entity) {
        m_Entity = entity;
        m_Entity.OnSelected += HandleOnSelected;
        m_Entity.OnDeselected += HandleDeselected;
        m_Entity.OnTaken += HandleOnTaken;
    }

    private void OnDestroy()
    {
        if (m_Entity != null) {
            m_Entity.OnSelected -= HandleOnSelected;
            m_Entity.OnDeselected -= HandleDeselected;
            m_Entity.OnTaken -= HandleOnTaken;
        }
    }

    #region Event Handlers
    void HandleOnSelected(Entity e) { m_Render.material.color = Color.yellow; }
    void HandleDeselected(Entity e) { m_Render.material.color = Color.white; }
    void HandleOnTaken(Entity e) { Destroy(this.gameObject); }
    #endregion
}
