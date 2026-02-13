using System;
using System.Reflection;
using Assets.Scripts.Input;
using EyeRecall.Function;
using EyeRecall.Helper;
using UnityEngine;

namespace EyeRecall
{
	// Token: 0x02000008 RID: 8
	[Obfuscation(Exclude = true)]
	public class Loader
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00003144 File Offset: 0x00001344
		[Obfuscation(Exclude = true)]//入口点
		public static void HackLoad()
		{
			try
			{
				LogManager.WriteLog("Inject Success!");
				InputCollector.Instance.SetDeviceInput(new FakeInput());
				GameObject gameObject = new GameObject("XZ");
				gameObject.AddComponent<CheatThread>();
				gameObject.AddComponent<MenuThread>();
				UnityEngine.Object.DontDestroyOnLoad(gameObject);
			}
			catch (Exception ex)
			{
				LogManager.WriteLog(ex.ToString());
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000031A8 File Offset: 0x000013A8
		public Loader()
		{
		}
	}
}
