using UnityEngine;
using System.Collections;

public class FSMTest : MonoBehaviour 
{

	public FSM fsm ;

	void Start()
	{
		fsm = new FSM();
	}

	void Update() 
	{
		if(Input.GetKeyUp(KeyCode.J)){
			fsm.HandleInput(TRAN_INPUT.BUTTON_A);
			StopAllCoroutines();
			StartCoroutine(autoTimeOut(2));
		}
		else if(Input.GetKeyUp(KeyCode.Space)){
			fsm.HandleInput(TRAN_INPUT.BUTTON_B);
			StopAllCoroutines();
			StartCoroutine(autoTimeOut(1));
		}
	}

	IEnumerator autoTimeOut(float sec){
		yield return new WaitForSeconds(sec);
		fsm.HandleInput(TRAN_INPUT.TIME_OUT);
	}
}

public enum TRAN_INPUT
{
	BUTTON_A,
	BUTTON_B,
	TIME_OUT,
}

public interface State
{
	State HandleInput(TRAN_INPUT input);
	void EnterState();
}

public class IdleState: State
{
	public State HandleInput(TRAN_INPUT input)
	{
		if(input == TRAN_INPUT.BUTTON_A)
		{
			return new SkillA();
		}
		else if (input == TRAN_INPUT.BUTTON_B)
		{
			return new DodgeState();
		}
		return null;
	}
	public void EnterState()
	{
		Debug.Log("To Idle State");
	}
}

public class DodgeState: State
{
	public State HandleInput(TRAN_INPUT input)
	{
		if(input == TRAN_INPUT.TIME_OUT)
		{
			return new IdleState();
		}
		return null;
	}
	public void EnterState()
	{
		Debug.Log("To Dodge State");
	}
}

public class SkillA: State
{
	public State HandleInput(TRAN_INPUT input)
	{
		if(input == TRAN_INPUT.BUTTON_A)
		{
			return new SkillB();
		}
		else if(input == TRAN_INPUT.TIME_OUT)
		{
			return new IdleState();
		}

		return null;
	}

	public void EnterState()
	{
		Debug.Log("To SkillA State");
	}
}

public class SkillB: State
{
	public State HandleInput(TRAN_INPUT input)
	{
		if(input == TRAN_INPUT.TIME_OUT)
		{
			return new IdleState();
		}
		return null;
	}

	public void EnterState()
	{
		Debug.Log("To SkillB State");
	}
}

public class FSM
{
	State CurrentState;

	public FSM (){
		CurrentState = new IdleState();
		CurrentState.EnterState();
	}

	public void HandleInput(TRAN_INPUT input)
	{
		State newState = CurrentState.HandleInput(input);
		if(newState != null)
		{
			CurrentState = newState;
			CurrentState.EnterState();
		}
	}
}