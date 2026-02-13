using System;
using System.Reflection;

namespace EyeRecall.Helper
{
	// Token: 0x0200000F RID: 15
	internal static class ReflectExt
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00004010 File Offset: 0x00002210
		public static T GetField<T>(this object _object, string name)
		{
			return (T)((object)_object.GetType().GetField(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).GetValue(_object));
		}
	}
}
