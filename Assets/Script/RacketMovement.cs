using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketMovement : MonoBehaviour {

	private Transform trans = null;

    private void OnEnable()
    {
        //EasyTouch.On_SwipeStart += On_SwapStart;
        //EasyTouch.On_Swipe += On_Swap;
        //EasyTouch.On_SwipeEnd += On_SwapEnd;
    }

    private void OnDisable()
    {
        UnsubscribeEvent();
    }

    private void OnDestroy()
    {
        UnsubscribeEvent();
    }

    void UnsubscribeEvent()
    {
        //EasyTouch.On_SwipeStart -= On_SwapStart;
        //EasyTouch.On_Swipe -= On_Swap;
        //EasyTouch.On_SwipeEnd -= On_SwapEnd;
    }

	void Start() {
		trans = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			trans.position += Vector3.left * 3.2f;
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			trans.position += Vector3.right * 3.2f;
		} 
		 
			
	}

    //private void On_SwapStart(Gesture gesture)
    //{

    //}

    //private void On_Swap(Gesture gesture)
    //{

    //}

    //private void On_SwapEnd(Gesture gesture)
    //{
    //    Debug.Log(gesture.swipeVector.normalized);
    //}
}
