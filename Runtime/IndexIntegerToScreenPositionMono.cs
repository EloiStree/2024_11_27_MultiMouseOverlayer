using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndexIntegerToScreenPositionMono : MonoBehaviour
{

    public Dictionary<int, IndexIntegerPlayerScreenPosition> m_cursors = new Dictionary<int, IndexIntegerPlayerScreenPosition>();
    public List<IndexIntegerPlayerScreenPosition> m_cursorsDebugList= new List<IndexIntegerPlayerScreenPosition>();
    public int m_playerCount;

    public void RefreshDebugList()
    {
        m_cursorsDebugList.Clear();
        foreach (KeyValuePair<int, IndexIntegerPlayerScreenPosition> kvp in m_cursors)
        {
            m_cursorsDebugList.Add(kvp.Value);
        }
    }

    public void Push(int player, int value) { 
    
        if(m_cursors.ContainsKey(player))
        {
            m_cursors[player].PushIn( value);
        }
        else
        {
            IndexIntegerPlayerScreenPosition newPlayer = new IndexIntegerPlayerScreenPosition();
            newPlayer.PushIn(player, value);
            m_cursors.Add(player, newPlayer);
            RefreshDebugList();
            m_playerCount= m_cursorsDebugList.Count;
        }
    }
}
[System.Serializable]
public class IndexIntegerPlayerScreenPosition
{

    public int m_playerIndex;
    public int m_lastValideValue;
    public int m_percentXAs1to9999LR;
    public int m_percentYAs1to9999DT;
    [Range(0, 1)]
    public float m_percentXLR;
    [Range(0, 1)]
    public float m_percentYDT;


    public void PushIn(int value) {

        m_lastValideValue = value;
        m_percentXAs1to9999LR = value % 9999;
        m_percentYAs1to9999DT = value / 1000 % 9999;
        m_percentXLR = m_percentXAs1to9999LR / 9999;
        m_percentYDT = m_percentYAs1to9999DT / 9999;
    }

    public void PushIn(int index, int value) { 
        m_playerIndex = index;
        PushIn(value);

    }
}