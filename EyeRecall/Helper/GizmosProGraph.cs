using System;
using UnityEngine;

namespace EyeRecall.Helper
{
	// Token: 0x0200000B RID: 11
	internal class GizmosProGraph
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00003858 File Offset: 0x00001A58
		private static void CreateLineMaterial()
		{
			if (!GizmosProGraph.s_material)
			{
				GizmosProGraph.s_material = new Material(Shader.Find("Hidden/Internal-Colored"));
				GizmosProGraph.s_material.hideFlags = HideFlags.HideAndDontSave;
				GizmosProGraph.s_material.SetInt("_SrcBlend", 5);
				GizmosProGraph.s_material.SetInt("_DstBlend", 10);
				GizmosProGraph.s_material.SetInt("_Cull", 0);
				GizmosProGraph.s_material.SetInt("_ZWrite", 0);
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000038D2 File Offset: 0x00001AD2
		public static void Begin(Color color)
		{
			GizmosProGraph.CreateLineMaterial();
			GizmosProGraph.s_material.SetPass(0);
			GL.PushMatrix();
			GL.LoadOrtho();
			GL.Begin(1);
			GL.Color(color);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000038FB File Offset: 0x00001AFB
		public static void DrawDot(Vector2 position)
		{
			GizmosProGraph.DrawScreenRect(position - Vector2.one, Vector2.one * 2f, 1f);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003924 File Offset: 0x00001B24
		public static void DrawTextWithShadow(Vector2 position, string text, Color textColor)
		{
			if (GizmosProGraph.customFont == null)
			{
				Debug.LogError("Custom font not loaded. Text may not display correctly.");
				return;
			}
			Color color = GUI.color;
			GUIStyle guistyle = new GUIStyle(GUI.skin.label)
			{
				font = GizmosProGraph.customFont,
				normal = 
				{
					textColor = textColor
				},
				alignment = TextAnchor.MiddleCenter
			};
			GUIContent guicontent = new GUIContent(text);
			Vector2 vector = guistyle.CalcSize(guicontent);
			Rect rect = new Rect(position.x - vector.x / 2f, position.y, vector.x, vector.y);
			GUIStyle guistyle2 = new GUIStyle(guistyle)
			{
				normal = 
				{
					textColor = new Color(0f, 0f, 0f, 0.2f)
				}
			};
			GUI.Label(new Rect(rect.x + 1f, rect.y + 1f, rect.width, rect.height), text, guistyle2);
			GUI.color = textColor;
			GUI.Label(rect, text, guistyle);
			GUI.color = color;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003A2C File Offset: 0x00001C2C
		internal static void DrawBox(Vector2 position, Vector2 size, float thickness)
		{
			GUI.DrawTexture(new Rect(position.x, position.y, size.x, thickness), Texture2D.whiteTexture);
			GUI.DrawTexture(new Rect(position.x, position.y, thickness, size.y), Texture2D.whiteTexture);
			GUI.DrawTexture(new Rect(position.x + size.x, position.y, thickness, size.y), Texture2D.whiteTexture);
			GUI.DrawTexture(new Rect(position.x, position.y + size.y, size.x + thickness, thickness), Texture2D.whiteTexture);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003AD4 File Offset: 0x00001CD4
		internal static void DrawLine(Vector2 from, Vector2 to, float thickness)
		{
			Vector2 vector = to - from;
			float magnitude = vector.magnitude;
			Vector2 normalized = vector.normalized;
			Vector2 vector2 = new Vector2(-normalized.y, normalized.x) * (thickness / 2f);
			GUI.DrawTexture(new Rect(from - vector2, new Vector2(magnitude, thickness)), Texture2D.whiteTexture);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003B38 File Offset: 0x00001D38
		public static void DrawScreenRect(Vector2 position, Vector2 size, float thickness)
		{
			GUI.DrawTexture(new Rect(position.x, position.y, size.x, thickness), Texture2D.whiteTexture);
			GUI.DrawTexture(new Rect(position.x, position.y, thickness, size.y), Texture2D.whiteTexture);
			GUI.DrawTexture(new Rect(position.x + size.x, position.y, thickness, size.y), Texture2D.whiteTexture);
			GUI.DrawTexture(new Rect(position.x, position.y + size.y, size.x + thickness, thickness), Texture2D.whiteTexture);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003BE0 File Offset: 0x00001DE0
		public static void DrawScreenEllipse(Vector2 center, float xRadius, float yRadius, Color color, int smooth = 50)
		{
			GizmosProGraph.Begin(color);
			for (int i = 0; i < smooth; i++)
			{
				int num = (i + 1) % smooth;
				float num2 = 6.2831855f / (float)smooth * (float)i;
				float num3 = 6.2831855f / (float)smooth * (float)num;
				GL.Vertex3((center.x + xRadius * Mathf.Cos(num2)) / (float)Screen.width, (center.y + yRadius * Mathf.Sin(num2)) / (float)Screen.height, 0f);
				GL.Vertex3((center.x + xRadius * Mathf.Cos(num3)) / (float)Screen.width, (center.y + yRadius * Mathf.Sin(num3)) / (float)Screen.height, 0f);
			}
			GizmosProGraph.End();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003C98 File Offset: 0x00001E98
		private static void End()
		{
			GL.End();
			GL.PopMatrix();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003CA4 File Offset: 0x00001EA4
		public GizmosProGraph()
		{
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003CAC File Offset: 0x00001EAC
		// Note: this type is marked as 'beforefieldinit'.
		static GizmosProGraph()
		{
		}

		// Token: 0x04000044 RID: 68
		private static Material s_material;

		// Token: 0x04000045 RID: 69
		public static readonly Font customFont = Resources.Load<Font>("Fonts/NotoSansCJK") ?? Resources.GetBuiltinResource<Font>("Arial.ttf");
	}
}
