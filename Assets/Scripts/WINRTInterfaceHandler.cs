using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class WINRTInterfaceHandler : MonoBehaviour
{
    public delegate void ReceivedCallBack(int miRequestId, string strRequestedData, string result);
    public ReceivedCallBack OnCallBack;
    string msPrevRequestedData;
    int miPrevRequestedId;

    private bool isCreated = false;
    public event Action<int, string> SendRequestEvent; //Event for handling the call backs

    
    void Awake()
    {
        if (!isCreated)
        {
            DontDestroyOnLoad(this.gameObject); //make sure dont delete at all
            isCreated = true;
        }
        else
            Destroy(this.gameObject);
    }
    public bool SendRequest(int requestId, string requestedData, ReceivedCallBack requestCallback)
    {
        if (requestCallback != null)
        {
            miPrevRequestedId = requestId;
            msPrevRequestedData = requestedData;
            OnCallBack = requestCallback;
        }

        //depends on request id send the request to native code
        if (SendRequestEvent != null)
        {
            SendRequestEvent(requestId, requestedData);
        }

        return true;
    }

    public void SendResponse(string msReuestStatus) //which is directly called by the native code
    {
        if (OnCallBack == null)
        {
            return;
        } //if no call back required then just return

        else
        {
            ReceivedCallBack temp = OnCallBack;
            OnCallBack = null;
            temp(miPrevRequestedId, msPrevRequestedData, msReuestStatus);
        }
    }

}//Class end
