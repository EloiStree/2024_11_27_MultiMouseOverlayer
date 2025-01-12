using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacadeRequestMouseActionMono : MonoBehaviour
{
    public bool m_isInAuthorizedConditionToInteract;

    [ContextMenu("Enable Interaction")]
    public void SetAsEnableInteraction()
    {

        m_isInAuthorizedConditionToInteract = true;
    }
    public void SetAsEnableInteraction(bool isEnableToInteractWith)
    {

        m_isInAuthorizedConditionToInteract = isEnableToInteractWith;
    }
    [ContextMenu("Disable Interaction")]
    public void SetAsDisableInteraction() { 
    
        m_isInAuthorizedConditionToInteract = false;
    }
    public bool IsEnableToInteractWith()
    {

        return m_isInAuthorizedConditionToInteract;
    }
    public bool IsNotEnableToInteractWith()
    {

        return !m_isInAuthorizedConditionToInteract;
    }


    public void RequestMouseMovePercentLeftRightDownTop(float leftRight, float downTop) { 
        if(IsNotEnableToInteractWith()) return;
        STRUCT_MouseMove mouseMove = new STRUCT_MouseMove();
        mouseMove.xLeftRightPercent = leftRight;
        mouseMove.yDownTopPercent = downTop;
        EnqueueAction(mouseMove);
    }
    public void RequestMouseKeyboarPercentLeftRigthDownTop(float leftRight, float downTop, byte windowKeyStrokeId, bool press, bool release) { 
        if(IsNotEnableToInteractWith()) return;
        STRUCT_MouseMoveAction mouseMoveAction = new STRUCT_MouseMoveAction();
        mouseMoveAction.m_mouseMove.xLeftRightPercent = leftRight;
        mouseMoveAction.m_mouseMove.yDownTopPercent = downTop;
        mouseMoveAction.m_windowKeyStrokeId = windowKeyStrokeId;
        mouseMoveAction.m_press = press;
        mouseMoveAction.m_release = release;
        EnqueueAction(mouseMoveAction);
    }


    public Queue<STRUCT_MouseMove> m_mouseMoveQueue = new Queue<STRUCT_MouseMove>();
    public Queue<STRUCT_MouseMoveAction> m_mouseMoveKeyboardQueue = new Queue<STRUCT_MouseMoveAction>();
    public Queue<STRUCT_MouseIntegerAction> m_mouseMoveIntegerQueue = new Queue<STRUCT_MouseIntegerAction>();   
    public Queue<STRUCT_MouseIndexIntegerAction> m_mouseMoveIndexIntegerQueue = new Queue<STRUCT_MouseIndexIntegerAction>();

    public int m_waitingMoveActionCount;
    public int m_waitingMoveKeyboardActionCount;
    public int m_waitingMoveIntegerActionCount;
    public int m_waitingMoveIndexIntegerActionCount;

    public void Update()
    {
        m_waitingMoveActionCount = m_mouseMoveQueue.Count;
        m_waitingMoveKeyboardActionCount = m_mouseMoveKeyboardQueue.Count;
        m_waitingMoveIntegerActionCount = m_mouseMoveIntegerQueue.Count;
        m_waitingMoveIndexIntegerActionCount = m_mouseMoveIndexIntegerQueue.Count;
    }

    

    public void EnqueueAction(STRUCT_MouseMove mouseMove) { 
        m_mouseMoveQueue.Enqueue(mouseMove);
    }
    public void EnqueueAction(STRUCT_MouseMoveAction mouseMoveAction) { 
        m_mouseMoveKeyboardQueue.Enqueue(mouseMoveAction);
    }
    public void EnqueueAction(STRUCT_MouseIntegerAction mouseIntegerAction) { 
        m_mouseMoveIntegerQueue.Enqueue(mouseIntegerAction);
    }
    public void EnqueueAction(STRUCT_MouseIndexIntegerAction mouseIndexIntegerAction) { 
        m_mouseMoveIndexIntegerQueue.Enqueue(mouseIndexIntegerAction);
    }


    public bool IsThereMouseMoveAction() { 
        return m_mouseMoveQueue.Count > 0;
    }   
    public bool IsThereMouseMoveKeyboardAction() { 
        return m_mouseMoveKeyboardQueue.Count > 0;
    }
    public bool IsThereMouseIntegerAction() { 
        return m_mouseMoveIntegerQueue.Count > 0;
    }
    public bool IsThereMouseIndexIntegerAction() { 
        return m_mouseMoveIndexIntegerQueue.Count > 0;
    }
    public void DequeueAction(bool found, out STRUCT_MouseMoveAction mouseMoveAction) { 
    
        found = m_mouseMoveKeyboardQueue.Count > 0;
        if(found) { 
            mouseMoveAction = m_mouseMoveKeyboardQueue.Dequeue();
        } else { 
            mouseMoveAction = new STRUCT_MouseMoveAction();
        }
    }
    public void DequeueAction(bool found, out STRUCT_MouseMove mouseMove) { 
    
        found = m_mouseMoveQueue.Count > 0;
        if(found) { 
            mouseMove = m_mouseMoveQueue.Dequeue();
        } else { 
            mouseMove = new STRUCT_MouseMove();
        }
    }

    public void DequeueAction(bool found, out STRUCT_MouseIntegerAction mouseIntegerAction) { 
    
        found = m_mouseMoveIntegerQueue.Count > 0;
        if(found) { 
            mouseIntegerAction = m_mouseMoveIntegerQueue.Dequeue();
        } else { 
            mouseIntegerAction = new STRUCT_MouseIntegerAction();
        }
    }

    public void DequeueAction(bool found, out STRUCT_MouseIndexIntegerAction mouseIndexIntegerAction) { 
    
        found = m_mouseMoveIndexIntegerQueue.Count > 0;
        if(found) { 
            mouseIndexIntegerAction = m_mouseMoveIndexIntegerQueue.Dequeue();
        } else { 
            mouseIndexIntegerAction = new STRUCT_MouseIndexIntegerAction();
        }
    }
}

[System.Serializable]
public struct STRUCT_MouseMove { 
    public float xLeftRightPercent;
    public float yDownTopPercent;
}
[System.Serializable]
public struct STRUCT_MouseMoveAction { 
    public STRUCT_MouseMove m_mouseMove;
    public byte m_windowKeyStrokeId;
    public bool m_press;
    public bool m_release;
}

[System.Serializable]
public struct STRUCT_MouseIntegerAction
{
    public int m_xLeftRight;
    public int m_yDownTop;
    public int m_integerAction;
}

[System.Serializable]
public struct STRUCT_MouseIndexIntegerAction
{
    public int m_xLeftRight;
    public int m_yDownTop;
    public int m_index;
    public int m_value;
}


