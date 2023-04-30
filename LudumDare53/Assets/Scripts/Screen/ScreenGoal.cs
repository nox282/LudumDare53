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
        if (NeedStamp && !PlayerCharacter.Get.IsStamped())
        {
            foreach (var guardCharacter in Screen.GuardCharacters)
            {
                guardCharacter.PerceptionComponent.lastKnownPosition = PlayerCharacter.Get.transform;
            }

            AlertManager.Get.ALERTEGENERAAAAAAAAAAAAAAAAAALE();
            return;
        }

        if (NextScreenStart != null)
        {
            Screen.GoalReached(NextScreenStart);
        }

        OnGoal.Invoke();
    }
}
