using UnityEngine;
using System.Collections;

public class OnProgressBarLoaded : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var settingsProvider = (SettingsProvider)(GameObject.Find("SettingsObject").GetComponent("SettingsProvider"));
	    this.GetComponent<UISlider>().value = settingsProvider.GetVolume();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
