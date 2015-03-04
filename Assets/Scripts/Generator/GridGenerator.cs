using Assets.Scripts;
using Assets.Scripts.Grid;
using Assets.Scripts.Levels;
using Assets.Scripts.Models;
using UnityEngine;
using System.Collections;

public class GridGenerator:MonoBehaviour
{
	public GameObject WorkingElement;
	public GameObject Buldozer;
	public GameObject Stone;
	public GameObject Target;
	public GameObject FencyElement;
	public GameObject NotWorkingElement;

    private GameObject _backgroundPlane;

	private  int columnNumber = 16;
	private  int rowNumber = 11;
    private static GridGenerator instance;
    

	private Grid _grid;
	
	public void Start()
	{
	   
	}

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Refresh()
    {
        var scroInfo=GameObject.FindObjectOfType<ScoreInfo>();
        DeleteAllDynamic();
        var settingsObject = GameObject.Find("Managers");
        var settingsProvider = (SettingsProvider)(settingsObject.GetComponent("SettingsProvider"));
        var levelManager = (settingsObject.GetComponent<LevelManager>());
        var level = levelManager.GetCurrentLevel();
        _backgroundPlane = GameObject.FindGameObjectWithTag("BackgroundPlane");
        GetColumnAndRowsNumber(level);
        _grid = new Grid(level.LevelContent, columnNumber, rowNumber, scroInfo, settingsProvider, levelManager);

        _backgroundPlane.transform.localScale = new Vector3(columnNumber, rowNumber, 1);
        GenerateContentPrefabsForGridContent();
    }

    private void DeleteAllDynamic()
    {
        var dynamicOldData = GameObject.FindGameObjectsWithTag("Refresh");
        foreach (var old in dynamicOldData)
        {
            Destroy(old, 0f);
        }
    }

    private void GetColumnAndRowsNumber(Level level)
    {
        rowNumber = level.ColumnRows;
        columnNumber = level.ColumnNumbers;
        if ((rowNumber%2) == 0)
        {
            _backgroundPlane.transform.position = new Vector3(-0.5f, 0.5f, 2);
            Debug.LogWarning("log");
        }

    }
	public void GenerateContentPrefabsForGridContent()
	{
		for (int i = 0; i < rowNumber; i++)
		{
			for (int j = 0; j < columnNumber; j++)
			{
				var cell=_grid.GetCell(j,i);
				switch(cell.GetContent()){
					case CellContent.WorkingElement:
					//CreatePrefab(WorkingElement,cell,1);
					break;

				case CellContent.Buldozer:
					CreateBuldozer(Buldozer,cell,-2);
					break;

				case CellContent.Stone:
					CreateKamen(Stone,cell,0);
					break;

				case CellContent.Target:
					CreatePrefab(Target,cell,-1);
					break;

				case CellContent.FencyElement:
					CreatePrefab(FencyElement,cell,0);
					break;

				case CellContent.NotWorkingElement:
					CreatePrefab(NotWorkingElement,cell,0);
					break;

                case CellContent.StoneWithTarget:
                    CreatePrefab(Target, cell, -1);
                    CreateKamen(Stone, cell, 0);
                    break;
				}
			}
		}


	}
	public void CreatePrefab(GameObject prefab,GridCell cell,float z){
		var gameobject=Instantiate(prefab,new Vector3(cell.Position.x,cell.Position.y,z),new Quaternion(0,0,0,0)) as GameObject;
        gameobject.tag = "Refresh";
	}
    public void CreateKamen(GameObject prefab, GridCell cell, float z)
    {
        var kamen = Instantiate(prefab, new Vector3(cell.Position.x, cell.Position.y, z), new Quaternion(0, 0, 0, 0)) as GameObject;
        kamen.tag = "Refresh";
        cell.SetKamen(kamen);
    }
    public void CreateBuldozer(GameObject prefab, GridCell cell, float z)
    {
        var buldozer = (GameObject)Instantiate(prefab, new Vector3(cell.Position.x, cell.Position.y, z), new Quaternion(0, 0, 0, 0));
        buldozer.tag = "Refresh";
		var buldozerController= buldozer.GetComponent<BuldozerController> ();

		buldozerController.SetGrid (_grid);
        buldozerController.SetStartCell(cell);
    }


}









