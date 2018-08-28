using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpLine : MonoBehaviour {
    //Hello World~ welcome to the Pick Up Line script, made by your friend, KingdomCross~ Watch my lines as I get players to pick, me, up~
    public float spinTime;
    public float spinSpeed;
    private float saveSpinTime;
    private bool spinBool;

	// Use this for initialization
	void Start ()
	{
	    saveSpinTime = spinTime;
	    spinBool = true;
	}
	
	// Update is called once per frame
	void Update () {
	    SpinObject();
	}

    void SpinObject()
    {
        if (spinTime >= 0 && spinBool)
        {
            transform.Rotate(new Vector3(0, 0, spinSpeed));
            spinTime -= Time.deltaTime;
        }
        else if (spinTime <= 0 && spinBool)
        {
            spinTime = saveSpinTime;
            spinBool = false;
        }
        else if (spinTime >= 0 && spinBool == false)
        {
            transform.Rotate(new Vector3(0, 0, -spinSpeed));
            spinTime -= Time.deltaTime;
        }
        else if (spinTime <= 0 && spinBool == false)
        {
            spinTime = saveSpinTime;
            spinBool = true;
        }
    }
}
