using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class RelayIntegerToUDPTargetsMono : MonoBehaviour
{

    public TargetIPV4 m_mouseMoveTarget = 
        new TargetIPV4() {
            m_ip = "127.0.0.1",
            m_actionType2020=15,
            m_port= new uint []{7015 }
        };
    public List<TargetIPV4> m_targets = new List<TargetIPV4>();

    [System.Serializable]
    public class TargetIPV4
    {
        public int m_actionType2020=0;
        public string m_ip = "127.0.0.1";
        public uint[] m_port = new uint[] { 7000, 7001, 7002, 7004 };
    }

    public STRUCT_CursorPositionNetworkPlayerAction m_lastPushed;
    public int m_lastActionType;
    public int m_lastInteger;

    public void Push(STRUCT_CursorPositionNetworkPlayerAction action)
    {
        PushMouseMove(action.m_mousePosition);
        PushIntegerAction(action.m_playerValueAction);
    }

    private void PushMouseMove(STRUCT_CursorPosition2020 mousePosition)
    {
        IntegerToMousePosition2020Utility.ParseMousePosition2020ToInteger(out int integer, mousePosition);
        PushIntegerAction(integer);
    }

    public void PushIntegerAction(int integer)
    {
        int actionType = integer/100000000;
        m_lastActionType = actionType;
        m_lastInteger = integer;
        foreach (TargetIPV4 target in m_targets)
        {
            PushIntegerToTarget(integer, actionType, target);
        }
    }

    private static void PushIntegerToTarget(int integer, int actionType, TargetIPV4 target)
    {
        byte[] bytes = System.BitConverter.GetBytes(integer);
        if (target.m_actionType2020 == actionType)
        {
            UdpClient c = new UdpClient();
            foreach (uint port in target.m_port)
            {
                c.Send(bytes, bytes.Length, target.m_ip, (int)port);
            }
            c.Close();
        }
    }
}
