using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class RateEventHandler:MonoBehaviour
    {
         private WINRTInterfaceHandler _winrtHandler;

        void Start()
        {
            _winrtHandler = (WINRTInterfaceHandler)(GameObject.Find("Managers").GetComponent("WINRTInterfaceHandler"));
        }
        public void Close()
        {
            this.gameObject.SetActive(false);
        }

        public void ShowRate()
        {
            _winrtHandler.SendRequest(5, "rate", requestCallback);
        }

        private void requestCallback(int mirequestid, string strrequesteddata, string result)
        {
            this.gameObject.SetActive(false);
        }
    }

