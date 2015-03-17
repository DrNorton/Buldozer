﻿using Assets.Scripts;
using Assets.Scripts.Grid;
using Assets.Scripts.Models;
using UnityEngine;
using System.Collections;

public class BuldozerController : MonoBehaviour
{
    private BulldozerState _state;
    private Grid _grid;
    private bool _canPinch = true;
    private CurrentCellPosition _cureCellPosition;

    public bool IsTouch = false;
   
    private SwipeGestureRecognizer _swipeGesture;
    private PinchGestureRecognizer _gesture;

    // Use this for initialization
	void Start () {
	    _state=BulldozerState.Top;
        _dpad= VCDPadBase.GetInstance("dpad");
	}

    private void OnEnable()
    {
        //_swipeGesture = this.GetComponent<SwipeGestureRecognizer>();
        //_swipeGesture.OnSwipe+=BuldozerController_OnSwipe;
        //_gesture = this.GetComponent<PinchGestureRecognizer>();
        //_gesture.OnPinchBegin+=_gesture_OnPinchBegin;
        //_gesture.OnPinchEnd+=_gesture_OnPinchEnd;
    }

    private void _gesture_OnPinchEnd(PinchGestureRecognizer source)
    {
        IsTouch = true;
    }

    private void _gesture_OnPinchBegin(PinchGestureRecognizer source)
    {
        IsTouch = false;
    }

    private void BuldozerController_OnSwipe(SwipeGestureRecognizer source)
    {
        FingerGestures_OnFingerSwipe(0, new Vector2(), source.Direction, 0);
    }

    



    void OnDisable()
    {
        // unregister finger gesture events
		_swipeGesture.OnSwipe-=BuldozerController_OnSwipe;
		
       
    }

    private void FingerGestures_OnFingerSwipe(int fingerindex, Vector2 startpos, FingerGestures.SwipeDirection direction, float velocity)
    {
		Debug.LogWarning(FingerGestures.Touches.Count);     
        if (IsTouch)
        {
            if (direction==FingerGestures.SwipeDirection.Up)
            {
                GoToTop();
            }

            if (direction == FingerGestures.SwipeDirection.Left)
            {
                GoLeft();
            }

            if (direction == FingerGestures.SwipeDirection.Right)
            {
                GoRight();
            }

            if (direction == FingerGestures.SwipeDirection.Down)
            {
                GoDown();
            }
            Debug.LogWarning("Свайп " + direction);
        }
        
    }

    public void GoDown()
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

    public void GoRight()
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

    public void GoLeft()
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

    public void GoToTop()
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


    private int c = 0;
    private VCDPadBase _dpad;
    // Update is called once per frame
    void FixedUpdate()
	{
	    
          
        //if (!IsTouch)
        //{
        //    KeyInputHandler();
        //}

        //else
        //{
        //    TouchInputHandler();
        //}

        //// Use the DPad to move the cube container

        // try and get the dpad


        // if we got one, perform movement
        if (_dpad)
        {

            if (_dpad.Left)
                    GoLeft();
            if (_dpad.Right)
                 GoRight();
            if (_dpad.Up)
                   GoToTop();
            if (_dpad.Down)
            {
                c++;
                Debug.LogWarning(c);
                GoDown();
            }
                   
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







