using System;
using System.Collections.Generic;
using System.Reflection;

// Token: 0x02000002 RID: 2
public static class HookPool
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002048 File Offset: 0x00000248
	public static void AddHooker(MethodBase method, MethodHook hooker)
	{
		MethodHook methodHook;
		if (HookPool._hookers.TryGetValue(method, out methodHook))
		{
			methodHook.Uninstall();
			HookPool._hookers[method] = hooker;
			return;
		}
		HookPool._hookers.Add(method, hooker);
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00002083 File Offset: 0x00000283
	public static void RemoveHooker(MethodBase method)
	{
		HookPool._hookers.Remove(method);
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002091 File Offset: 0x00000291
	// Note: this type is marked as 'beforefieldinit'.
	static HookPool()
	{
	}

	// Token: 0x04000001 RID: 1
	private static Dictionary<MethodBase, MethodHook> _hookers = new Dictionary<MethodBase, MethodHook>();
}
