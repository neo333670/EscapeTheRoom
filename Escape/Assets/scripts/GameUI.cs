using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] EscapeGameScene m_EscapeGameScene;
    [SerializeField] Text m_messageText;
    [SerializeField] GameObject m_Action;
    [SerializeField] Dropdown m_TakenEntityDropdown;

    List<string> m_Messagelist;
    int m_NowMessage;

    private void Start()
    {
        m_NowMessage = 0;
        m_Messagelist = new List<string> ();
        UpdateDropdownList();
    }

    public void ShowMessage(string msg) {
        m_Messagelist.Add(msg);
        m_messageText.text = "<b>Me:</b>" + msg;
        m_NowMessage = m_Messagelist.Count - 1;
    }

    public void LookUpperMessage() {
        if (m_NowMessage > 0) {
            m_NowMessage--;
            m_messageText.text = "<b>Me:</b>" + m_Messagelist[m_NowMessage];
        }
    }

    public void LookNextMessage()
    {
        if (m_NowMessage < m_Messagelist.Count - 1){
            m_NowMessage++;
            m_messageText.text = "<b>Me:</b>" + m_Messagelist[m_NowMessage];
        }
    }

    public void SetActionVisible(bool visible) { m_Action.SetActive(visible); }

    public void Inspect() {
        m_EscapeGameScene.Game.Inspect(); }

    public void Interact() { 
        m_EscapeGameScene.Game.Interact(); }

    public void UseEntity() {
        var entity = m_EscapeGameScene.Game.TakenEntities[m_TakenEntityDropdown.value];
        m_EscapeGameScene.Game.Interact(entity);
    }

    public void PutBack() {
        var entity = m_EscapeGameScene.Game.TakenEntities[m_TakenEntityDropdown.value];
        m_EscapeGameScene.Game.PutBack(entity);
    }

    public void UpdateDropdownList() {
        m_TakenEntityDropdown.ClearOptions();
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();

        foreach (var e in m_EscapeGameScene.Game.TakenEntities) {
            options.Add(new Dropdown.OptionData() { text = e.Name });
        }

        m_TakenEntityDropdown.options = options;
        m_TakenEntityDropdown.gameObject.SetActive(m_TakenEntityDropdown.options.Count > 0);
    }
}
