using UnityEngine;
using System.Collections;

public class PinchZoom : MonoBehaviour {
    public float pinchScaleFactor = 0.02f;
	// Use this for initialization
	void Start () {
	
	}

	void Enabled(){
		this.GetComponent<PinchGestureRecognizer> ().OnPinchMove += OnPinchMoved;
		}

    private void OnPinchMoved(PinchGestureRecognizer source)
    {
        Camera.main.orthographicSize += pinchScaleFactor;
        Debug.LogWarning("PINCh");
    }

    // Update is called once per frame
	void Update () {
	
	}
}
