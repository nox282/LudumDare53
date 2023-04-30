using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenGoal : MonoBehaviour
{
	public ScreenStart NextScreenStart;
	public ScreenComponent Screen;
	private TriggerComponent _goal;

	private void Awake()
	{
		_goal = GetComponent<TriggerComponent>();
	}

	private void OnEnable()
	{
		_goal.OnTriggerStayEvent += OnGoalStay;
	}

	private void OnDisable()
	{
		_goal.OnTriggerStayEvent -= OnGoalStay;
	}

	private void OnGoalStay(Collider other)
	{
		Screen.GoalReached(NextScreenStart);
	}
}
