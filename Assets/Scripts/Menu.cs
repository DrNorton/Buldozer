using System;
using System.Linq;
using Assets.Scripts.Models;
using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour { 
	
	public int window;
    private SettingsProvider _settingsProvider;
    public Texture2D texture;
    GUIStyle style;
    public  float _ambianceLevel = 0.0f; //Start muted.
    public Vector2 scrollPosition = Vector2.zero;
    private bool isMusic = true;
    public GUIStyle buttonStyle;

    void Start () {
        _settingsProvider = (SettingsProvider)(GameObject.Find("SettingsObject").GetComponent("SettingsProvider"));
        window = 1;
        style = new GUIStyle();
       
        _ambianceLevel = _settingsProvider.GetVolume();
    }



    void OnGUI2()
    {
        // An absolute-positioned example: We make a scrollview that has a really large client
        // rect and put it in a small rect on the screen.
        scrollPosition = GUI.BeginScrollView(new Rect(0, 0, 100, 100),
            scrollPosition, new Rect(0, 0, 220,1900));

        // Make four buttons - one in each corner. The coordinate system is defined
        // by the last parameter to BeginScrollView.
        GUI.Button(new Rect(0, 0, 100, 20), "Top-left");
        GUI.Button(new Rect(0, 180, 100, 20), "Top-right");
        GUI.Button(new Rect(0, 360, 100, 20), "Bottom-left");
        GUI.Button(new Rect(0, 400, 100, 20), "Bottom-right");

        // End the scroll view that we began above.
        GUI.EndScrollView();
    }
	void OnGUI () {
        style.normal.background = texture;
	    var buttonWidth = 180;
	    var buttonHeight = 40;
	    
       
		if(window == 1) 
		{
            GUI.BeginGroup(new Rect(Screen.width / 2 - Screen.width / 4, 10, 400, 440), style);
            if (GUI.Button(new Rect(200-buttonWidth/2, 120, buttonWidth, buttonHeight), "Play",buttonStyle)) 
			{ 
				window = 2;   
			}
            if (GUI.Button(new Rect(200 - buttonWidth / 2, 170, 180, 40), "Settings", buttonStyle)) 
			{ 
				window = 3; 
			}
            if (GUI.Button(new Rect(200 - buttonWidth / 2, 220, 180, 40), "About", buttonStyle)) 
			{ 
				window = 4; 
			}
            if (GUI.Button(new Rect(200 - buttonWidth / 2, 270, 180, 40), "Exit", buttonStyle)) 
			{
                Application.Quit(); 
			}
            GUI.EndGroup(); 
		} 
		
		if(window == 2)
		{
			
            GUI.BeginGroup(new Rect(Screen.width / 2 - Screen.width / 4, 10, 400,440),style);
            GUILayout.BeginArea(new Rect(110,120,400,440));
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false,true,GUILayout.Width(260), GUILayout.Height(290));
            
           

            GUILayout.BeginVertical();
            if (GUILayout.Button("Back", buttonStyle,GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)))
            {
                window = 1;
            }
            GUILayout.Space(10);
            foreach (var level in Enumerable.Reverse(_settingsProvider.Levels))
            {
                SetButton(buttonWidth, buttonHeight, level.Number);
              

            }
            GUILayout.EndVertical();
            
            GUILayout.EndScrollView();
            GUILayout.EndArea(); 
            GUI.EndGroup();
		    
           
		}

	    if (window == 3)
	    {
            GUI.BeginGroup(new Rect(Screen.width / 2 - Screen.width / 4, 10, 400, 440), style);
            GUIStyle labelStyle = GUI.skin.GetStyle("label");
            labelStyle.fontSize = 18;
	      
            labelStyle.normal.textColor = new Color32(0, 54, 186, 255);
            GUI.Label(new Rect(170, 70, 70, 60), "Settings", labelStyle);
            GUI.Label(new Rect(40, 120,70,100), "Volume", labelStyle);
            _ambianceLevel = GUI.HorizontalSlider(new Rect(110, 128, 200, 50), _ambianceLevel, 0.0f, 1.0f);
            _settingsProvider.SetVolume(_ambianceLevel);
            GUI.Label(new Rect(320, 120, 270, 300), ((int)(_ambianceLevel * 100)).ToString() + "%");

            var toggleStyle = GUI.skin.GetStyle("toggle");
	        toggleStyle.normal.textColor = new Color32(0, 54, 186, 255);
            toggleStyle.active.textColor=new Color32(0, 54, 186, 255);
			toggleStyle.hover.textColor=new Color32(0, 54, 186, 255);
			toggleStyle.focused.textColor=new Color32(0, 54, 186, 255);
			toggleStyle.onActive.textColor=new Color32(0, 54, 186, 255);
			toggleStyle.onNormal.textColor=new Color32(0, 54, 186, 255);
			toggleStyle.onActive.textColor=new Color32(0, 54, 186, 255);
			toggleStyle.onFocused.textColor=new Color32(0, 54, 186, 255);
			toggleStyle.onHover.textColor=new Color32(0, 54, 186, 255);
            isMusic = GUI.Toggle(new Rect(40, 150, 110, 50), isMusic, "MUSIC ON/OFF", toggleStyle);
	        _settingsProvider.ChangeBackgroundMusic(isMusic);
            if (GUI.Button(new Rect(200 - 60, 370, 120, 30), "Back", buttonStyle))
            {
                window = 1; 
            }
            GUI.EndGroup(); 
	    }
		
		
		

	}

    private void SetButton(int buttonWidth, int buttonHeight,int buttonNumber)
    {
        if (GUILayout.Button("Level " + buttonNumber, buttonStyle,GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)))
        {
            _settingsProvider.SetCurrentLevelIndex(buttonNumber);
            Application.LoadLevel("Level1");
        }
        GUILayout.Space(10);
    }
} 
