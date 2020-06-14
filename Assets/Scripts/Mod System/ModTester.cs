public class ModTester : IModable
{
	protected override void AwakeInternal()
	{
	}

	protected override void OnDestroyInternal()
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

	private void Start()
	{
		foreach (var mod in _mods)
		{
			mod.ModLoad();
			mod.ModActivate();
			mod.ModAwake(this);
		}
	}
}
