using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public abstract class VariableObject<T> : ScriptableObject
{
	public T value;
	
}

public abstract class NumericVariableObject<TNumeric> : VariableObject<TNumeric>
{
	public TNumeric maxValue;
	[SerializeField] bool resetOnPlayModeExit = 	true;

	protected virtual void OnDisable()
	{
		# if (UNITY_EDITOR)
		if (resetOnPlayModeExit)
			value = 								maxValue;
		#endif
	}

	protected virtual void OnEnable()
	{
		# if (UNITY_EDITOR)
		if (resetOnPlayModeExit)
			value = 								maxValue;
		#endif
	}
	
}