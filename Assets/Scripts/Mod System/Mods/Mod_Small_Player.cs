using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Mod", menuName = "Mods/Smaller Character")]
public class Mod_Small_Player : IModObject
{
	protected override void AwakeInternal()
	{
		PlayerBehaviour player = (Modable as PlayerBehaviour);
		player.transform.localScale = new Vector3(0.05f, 0.05f, 0.5f);
		player.transform.position += new Vector3(0.0f, 2.0f, 0.0f);
		player.speed -= 3.0f;
	}

	protected override void DestroyInternal()
	{
	}

	protected override void OnDisableInternal()
	{
	}

	protected override void OnEnableInternal()
	{
	}

	protected override void UpdateInternal()
	{
	}
}
