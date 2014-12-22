using Assets.Scripts.Grid;
using Assets.Scripts.Models;
using UnityEngine;
using System.Collections;

public class BuldozerController : MonoBehaviour
{
    public Sprite Top;
    public Sprite Left;
    public Sprite Right;
    public Sprite Bottom;
	

    private BulldozerState _state;
    private Grid _grid;

    private CurrentCellPosition _cureCellPosition;

    public bool IsTouch = false;

    // Use this for initialization
	void Start () {
	    _state=BulldozerState.Top;
        
	}

    private void OnEnable()
    {
        FingerGestures.OnFingerSwipe += FingerGestures_OnFingerSwipe;
    }

    void OnDisable()
    {
        // unregister finger gesture events
        FingerGestures.OnFingerSwipe -= FingerGestures_OnFingerSwipe;
     
    }

    private void FingerGestures_OnFingerSwipe(int fingerindex, Vector2 startpos, FingerGestures.SwipeDirection direction, float velocity)
    {
        if (IsTouch)
        {
            if (direction==FingerGestures.SwipeDirection.Up)
            {
                if (_state != BulldozerState.Top)
                {
                    this.transform.rotation = Quaternion.Euler(0, 0, 360);
                    _state = BulldozerState.Top;
                    ChangePosition(BulldozerState.Top);
                }
                else
                {
                    ChangePosition(BulldozerState.Top);
                }
            }

            if (direction == FingerGestures.SwipeDirection.Left)
            {
                if (_state != BulldozerState.Left)
                {
                    this.transform.rotation = Quaternion.Euler(0, 0, 90);
                    _state = BulldozerState.Left;
                    ChangePosition(BulldozerState.Left);
                }
                else
                {
                    ChangePosition(BulldozerState.Left);
                }
            }

            if (direction == FingerGestures.SwipeDirection.Right)
            {
                if (_state != BulldozerState.Right)
                {
                    _state = BulldozerState.Right;
                    this.transform.rotation = Quaternion.Euler(0, 0, 270);
                    ChangePosition(BulldozerState.Right);
                }
                else
                {
                    ChangePosition(BulldozerState.Right);
                }
            }

            if (direction == FingerGestures.SwipeDirection.Down)
            {
                if (_state != BulldozerState.Bottom)
                {
                    _state = BulldozerState.Bottom;
                    this.transform.rotation = Quaternion.Euler(0, 0, 180);
                    ChangePosition(BulldozerState.Bottom);
                }
                else
                {
                    ChangePosition(BulldozerState.Bottom);
                }
            }
            Debug.LogWarning("Свайп " + direction);
        }
        
    }

    public void SetGrid(Grid grid)
    {
        _grid = grid;
        _cureCellPosition=new CurrentCellPosition(grid.ColumnNumber,grid.RowNumber,_grid);
        Debug.LogWarning("Grid is set");
    }

    public void SetStartCell(GridCell cell)
    {
        _cureCellPosition.SetCurrentCell(cell);
        
    }
    

	public BulldozerState GetState(){
		return _state;
	}

	

	// Update is called once per frame
	void Update ()
	{
	    if (!IsTouch)
	    {
	        KeyInputHandler();
	    }

	    else
	    {
	        TouchInputHandler();
	    }
	   
	}

    private void TouchInputHandler()
    {
		if (Input.GetMouseButton (0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo ))
			{
				Vector3 worldSpaceHitPoint = hitInfo.point;
			   
			    
			}
		
		}


    }

    private void KeyInputHandler()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (_state != BulldozerState.Top)
            {
                this.transform.rotation = Quaternion.Euler(0, 0, 360);
                _state = BulldozerState.Top;
                ChangePosition(BulldozerState.Top);
            }
            else
            {
                ChangePosition(BulldozerState.Top);
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (_state != BulldozerState.Left)
            {
                this.transform.rotation = Quaternion.Euler(0, 0, 90);
                _state = BulldozerState.Left;
                ChangePosition(BulldozerState.Left);
            }
            else
            {
                ChangePosition(BulldozerState.Left);
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (_state != BulldozerState.Right)
            {
                _state = BulldozerState.Right;
                this.transform.rotation = Quaternion.Euler(0, 0, 270);
                ChangePosition(BulldozerState.Right);
            }
            else
            {
                ChangePosition(BulldozerState.Right);
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (_state != BulldozerState.Bottom)
            {
                _state = BulldozerState.Bottom;
                this.transform.rotation = Quaternion.Euler(0, 0, 180);
                ChangePosition(BulldozerState.Bottom);
            }
            else
            {
                ChangePosition(BulldozerState.Bottom);
            }
        }
    }

    private void ChangePosition(BulldozerState state)
    {
       
		var test=_cureCellPosition.NavigateTo (state);
		if (!test)
						return;
        switch (state)
        {
		
                case BulldozerState.Top:
		     	Move (new Vector3(0,1,0));

                
                break;

                case BulldozerState.Bottom:
			Move (new Vector3(0,-1,0));

                break;

                case BulldozerState.Left  :
			Move (new Vector3(-1,0,0));
		     	
                
                break;

                case BulldozerState.Right:
			Move (new Vector3(1,0,0));

                break;
        }
    
    }


    private void Move(Vector3 t){
		this.transform.position+=t;
	}

  
}







