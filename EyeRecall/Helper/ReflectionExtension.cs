using System;
using System.Reflection;
using UnityEngine;

namespace EyeRecall.Helper
{
	// Token: 0x02000010 RID: 16
	public static class ReflectionExtension
	{
		// Token: 0x06000052 RID: 82 RVA: 0x0000402C File Offset: 0x0000222C
		public static T ReflectProperty<T>(this object obj, string name)
		{
			T t;
			if (obj == null)
			{
				Debug.LogWarning("ReflectProperty: 对象为空，无法获取属性 '" + name + "'。");
				t = default(T);
				return t;
			}
			PropertyInfo property = obj.GetType().GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (property == null)
			{
				t = default(T);
				return t;
			}
			try
			{
				t = (T)((object)property.GetValue(obj, null));
			}
			catch (InvalidCastException)
			{
				Debug.LogError(string.Concat(new string[]
				{
					"ReflectProperty: 属性 '",
					name,
					"' 类型转换失败。期望 '",
					typeof(T).Name,
					"'，实际为 '",
					property.PropertyType.Name,
					"'。"
				}));
				t = default(T);
			}
			catch (Exception ex)
			{
				Debug.LogError("ReflectProperty: 获取属性 '" + name + "' 时发生异常: " + ex.Message);
				t = default(T);
			}
			return t;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00004134 File Offset: 0x00002334
		public static T ReflectField<T>(this object obj, string name)
		{
			T t;
			if (obj == null)
			{
				t = default(T);
				return t;
			}
			FieldInfo field = obj.GetType().GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (field == null)
			{
				t = default(T);
				return t;
			}
			try
			{
				t = (T)((object)field.GetValue(obj));
			}
			catch (InvalidCastException)
			{
				Debug.LogError(string.Concat(new string[]
				{
					"ReflectField: 字段 '",
					name,
					"' 类型转换失败。期望 '",
					typeof(T).Name,
					"'，实际为 '",
					field.FieldType.Name,
					"'。"
				}));
				t = default(T);
			}
			catch (Exception ex)
			{
				Debug.LogError("ReflectField: 获取字段 '" + name + "' 时发生异常: " + ex.Message);
				t = default(T);
			}
			return t;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00004224 File Offset: 0x00002424
		public static void ReflectSetField(this object obj, string name, object value)
		{
			if (obj == null)
			{
				return;
			}
			FieldInfo field = obj.GetType().GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (field != null)
			{
				try
				{
					field.SetValue(obj, value);
				}
				catch (Exception ex)
				{
					Debug.LogError("ReflectSetField: 设置字段 '" + name + "' 时发生异常: " + ex.Message);
				}
				return;
			}
			PropertyInfo property = obj.GetType().GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (property != null && property.CanWrite)
			{
				try
				{
					property.SetValue(obj, value, null);
				}
				catch (Exception ex2)
				{
					Debug.LogError("ReflectSetField: 设置属性 '" + name + "' 时发生异常: " + ex2.Message);
				}
				return;
			}
			Debug.LogWarning(string.Concat(new string[]
			{
				"ReflectSetField: 在类型 '",
				obj.GetType().Name,
				"' 中未找到可设置的字段或属性 '",
				name,
				"'。"
			}));
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000430C File Offset: 0x0000250C
		public static MethodInfo ReflectMethod(this object obj, string name)
		{
			if (obj == null)
			{
				return null;
			}
			return obj.GetType().GetMethod(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00004324 File Offset: 0x00002524
		public static void ReflectInvokeMethod(this object obj, string name, params object[] parameters)
		{
			if (obj == null)
			{
				return;
			}
			MethodInfo method = obj.GetType().GetMethod(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (method == null)
			{
				return;
			}
			try
			{
				method.Invoke(obj, parameters);
			}
			catch (Exception ex)
			{
				Debug.LogError("ReflectInvokeMethod: 调用方法 '" + name + "' 时发生异常: " + ex.Message);
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00004384 File Offset: 0x00002584
		public static T ReflectInvokeMethod<T>(this object obj, string name, params object[] parameters)
		{
			T t;
			if (obj == null)
			{
				t = default(T);
				return t;
			}
			MethodInfo method = obj.GetType().GetMethod(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (method == null)
			{
				t = default(T);
				return t;
			}
			try
			{
				t = (T)((object)method.Invoke(obj, parameters));
			}
			catch (InvalidCastException)
			{
				Debug.LogError(string.Concat(new string[]
				{
					"ReflectInvokeMethod<T>: 方法 '",
					name,
					"' 返回值类型转换失败。期望 '",
					typeof(T).Name,
					"'，实际为 '",
					method.ReturnType.Name,
					"'."
				}));
				t = default(T);
			}
			catch (Exception ex)
			{
				Debug.LogError("ReflectInvokeMethod<T>: 调用方法 '" + name + "' 时发生异常: " + ex.Message);
				t = default(T);
			}
			return t;
		}

		// Token: 0x04000046 RID: 70
		private const BindingFlags DefaultBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
	}
}
