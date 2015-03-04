using UnityEngine;
using System.Collections;

public class OnMusicToggleLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var settingsProvider = (SettingsProvider)(GameObject.Find("Managers").GetComponent("SettingsProvider"));
		var toggle = this.GetComponent<UIToggle> ();
	    toggle.value = settingsProvider.IsMusic;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
