using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
	[SerializeField]
	private float _groundHeight = 1;
	[SerializeField]
	private float _jumpSpeed = 4;
	[SerializeField]
	private float _gravity = 1;
	[SerializeField]
	private int _maxJumps = 2;

	private float mCurrYVelocity;
	private int mJumpCount = 0;

	// Use this for initialization
	void Start () 
	{
		mCurrYVelocity = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButtonDown(0) && mJumpCount < _maxJumps)
		{
			mCurrYVelocity = _jumpSpeed;
			mJumpCount++;
		}
		UpdateMovement();
	}

	private void UpdateMovement()
	{
		Vector3 pos = transform.position;
		pos += new Vector3( 0, mCurrYVelocity * Time.deltaTime, 0 );
		if(pos.y <= _groundHeight)
		{
			pos.y = _groundHeight;
			mCurrYVelocity = 0;
			mJumpCount = 0;
		}
		transform.position = pos;
		mCurrYVelocity -= _gravity * _gravity * Time.deltaTime;
	}

	public void OnDrawGizmos()
	{
	}
}
