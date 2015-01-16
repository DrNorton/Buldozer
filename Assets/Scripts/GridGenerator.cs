using Assets.Scripts;
using Assets.Scripts.Grid;
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
	public GameObject BackgroundPlane;
    

	private  int columnNumber = 16;
	private  int rowNumber = 11;

	private Grid _grid;
	
	public void Start()
	{
        var settingsProvider =(SettingsProvider)(GameObject.Find("SettingsObject").GetComponent("SettingsProvider"));
	    var level = settingsProvider.GetCurrentLevel();
	    GetColumnAndRowsNumber(level);
		_grid = new Grid(level.LevelContent,columnNumber,rowNumber,this.GetComponent<ScoreInfo>(),settingsProvider);
        BackgroundPlane.transform.localScale = new Vector3(columnNumber, rowNumber, 1);
		GenerateContentPrefabsForGridContent ();
	}

   

    private void GetColumnAndRowsNumber(Level level)
    {
        rowNumber = level.ColumnRows;
        columnNumber = level.ColumnNumbers;
        if ((rowNumber%2) == 0)
        {
            BackgroundPlane.transform.position = new Vector3(-0.5f,0.5f,2);
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
		var gameobject=Instantiate(prefab,new Vector3(cell.Position.x,cell.Position.y,z),new Quaternion(0,0,0,0));
	
	}

    public void CreateKamen(GameObject prefab, GridCell cell, float z)
    {
        var kamen = Instantiate(prefab, new Vector3(cell.Position.x, cell.Position.y, z), new Quaternion(0, 0, 0, 0));
        cell.SetKamen(kamen as GameObject);
    }
    public void CreateBuldozer(GameObject prefab, GridCell cell, float z)
    {
        var buldozer = (GameObject)Instantiate(prefab, new Vector3(cell.Position.x, cell.Position.y, z), new Quaternion(0, 0, 0, 0));
		var buldozerController= buldozer.GetComponent<BuldozerController> ();
		buldozerController.SetGrid (_grid);
        buldozerController.SetStartCell(cell);
    }


}









