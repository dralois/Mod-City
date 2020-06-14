using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New Mod", menuName = "Mods/Hendrik Mod")]
public class Mod_Hendrik : IModObject
{

	protected override void AwakeInternal()
	{
	}

	protected override void OnEnableInternal()
	{
        SpriteResolver[] r = (Modable as PlayerBehaviour).GetComponentsInChildren<SpriteResolver>();
        foreach (SpriteResolver s in r)
            s.SetCategoryAndLabel(s.GetCategory(), "Hendrik");
	}

	protected override void UpdateInternal()
	{

	}

	protected override void OnDisableInternal()
	{

	}

	protected override void DestroyInternal()
	{
	}
}
