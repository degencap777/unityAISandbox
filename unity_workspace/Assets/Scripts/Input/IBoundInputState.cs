using UnityEngine;

[SerializeField]
public interface IBoundInputState
{

	void Update();
	bool ConditionsMet();

}
