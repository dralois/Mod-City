using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Mod", menuName = "Mods/BOSD Mod")]
public class Mod_BOSD : IModObject
{
    public float waitTime = 4;
    private float waitTick = 1000;

	protected override void AwakeInternal()
	{
	}

	protected override void OnEnableInternal()
	{
        (Modable as MainMenuManager).BOSD.SetActive(true);
        waitTick = waitTime;
    }

	protected override void UpdateInternal()
	{
        waitTick -= Time.deltaTime;
        if (waitTick <= 0)
        {
            (Modable as MainMenuManager).LoadLevelByName("ModSelection");
            waitTick = 1000;
        }
    }

	protected override void OnDisableInternal()
	{
	}

	protected override void DestroyInternal()
	{
	}
}
