using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScreenGoal : MonoBehaviour
{
	public bool NeedStamp;
	public UnityEvent OnGoal;

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
		if (NeedStamp && !PlayerCharacter.Get.isStamped)
			return;

		if(NextScreenStart != null)
		{
			Screen.GoalReached(NextScreenStart);
		}

		OnGoal.Invoke();
	}
}
