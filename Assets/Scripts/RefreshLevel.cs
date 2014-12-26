using UnityEngine;
using System.Collections;

public class RefreshLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnDisable()
    {
        FingerGestures.OnTap -= OnRefreshTap;
    }

    private void OnEnable()
    {
        FingerGestures.OnTap+=OnRefreshTap;
    }

 

    private void OnRefreshTap(Vector2 fingerpos)
    {
        if (CheckSpawnParticles(fingerpos, this.gameObject))
        {
            var settingsProvider = (SettingsProvider)(GameObject.Find("SettingsObject").GetComponent("SettingsProvider"));
            settingsProvider.RefreshLevel();
        }
        else
        {
            Debug.LogWarning("NOT HIT"); 
        }
    }

    public static GameObject PickObject(Vector2 screenPos)
    {
		Vector2 wp = Camera.main.ScreenToWorldPoint (screenPos);

		var collider= Physics2D.OverlapPoint (wp);
		if (collider != null) {
						return collider.gameObject;
				} else {
			return null;
				}

    }

    bool CheckSpawnParticles(Vector2 fingerPos, GameObject requiredObject)
    {
        GameObject selection = PickObject(fingerPos);

        if (!selection || selection != requiredObject)
            return false;

        return true;
    }
}
