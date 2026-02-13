using System;
using System.Text;
using Assets.Sources.Constant;
using cakeslice;
using EyeRecall.Helper;
using EyeRecall.Helper.Player;
using UnityEngine;

namespace EyeRecall.Function.Hack
{
	// Token: 0x02000020 RID: 32
	internal class visual_esp
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x00008724 File Offset: 0x00006924
		private static GUIStyle GetTextStyle()
		{
			if (visual_esp._textStyle == null)
			{
				visual_esp._textStyle = new GUIStyle(GUI.skin.label);
				visual_esp._textStyle.fontStyle = FontStyle.Bold;
				visual_esp._textStyle.alignment = TextAnchor.MiddleCenter;
				visual_esp._textStyle.normal.background = null;
				visual_esp._textStyle.padding = new RectOffset(0, 0, 0, 0);
			}
			return visual_esp._textStyle;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000878C File Offset: 0x0000698C
		private static void DrawStyledText(Vector2 screenPosition, string text, Color textColor)
		{
			Color color = GUI.color;
			GUIStyle textStyle = visual_esp.GetTextStyle();
			textStyle.normal.textColor = textColor;
			GUIContent guicontent = new GUIContent(text);
			Vector2 vector = textStyle.CalcSize(guicontent);
			Rect rect = new Rect(screenPosition.x - vector.x / 2f, screenPosition.y, vector.x, vector.y);
			Rect rect2 = new Rect(rect.x + 1f, rect.y + 1f, rect.width, rect.height);
			GUIStyle guistyle = new GUIStyle(textStyle);
			guistyle.normal.textColor = Color.black;
			GUI.color = Color.black;
			GUI.Label(rect2, guicontent, guistyle);
			GUI.color = Color.white;
			GUI.Label(rect, guicontent, textStyle);
			GUI.color = color;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000885C File Offset: 0x00006A5C
		private static void DrawHealth(Rect position, float height, float hpPercent)
		{
			float num = 5f;
			float num2 = 1f;
			float num3 = 2f;
			float num4 = 1f;
			height *= num4;
			Rect rect = new Rect(position.x - num - num3, position.y, num, height);
			Rect rect2 = new Rect(rect.x, rect.y + height * (1f - hpPercent), num, height * hpPercent);
			Rect rect3 = new Rect(rect.x - num2, rect.y - num2, num + 2f * num2, height + 2f * num2);
			Color color = GUI.color;
			GUI.color = Color.black;
			GUI.DrawTexture(rect3, Texture2D.whiteTexture);
			GUI.DrawTexture(rect, Texture2D.whiteTexture);
			GUI.color = ((hpPercent > 0.65f) ? Color.green : ((hpPercent > 0.3f) ? Color.yellow : Color.red));
			GUI.DrawTexture(rect2, Texture2D.whiteTexture);
			GUI.color = color;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000894C File Offset: 0x00006B4C
		internal static void Execute(PlayerEntity targetPlayer)
		{
			try
			{
				if (targetPlayer.GetTeam() != updateData.own.GetTeam())
				{
					resolver.run(targetPlayer);
					if (CheatThread._Visual.glow)
					{
						Camera mainCamera = updateData.mainCamera;
						OutlineEffect outlineEffect = mainCamera.gameObject.GetComponent<OutlineEffect>() ?? mainCamera.gameObject.AddComponent<OutlineEffect>();
						Color color;
						switch (CheatThread._Visual.glowColorMode)
						{
						case 0:
							color = Color.red;
							break;
						case 1:
							color = Color.yellow;
							break;
						case 2:
							color = Color.blue;
							break;
						case 3:
							color = Color.green;
							break;
						case 4:
							color = Color.HSVToRGB(Mathf.Repeat(Time.time * 0.5f, 1f), 1f, 1f);
							break;
						default:
							color = Color.red;
							break;
						}
						outlineEffect.lineColor0 = color;
						outlineEffect.lineColor1 = Color.white;
						outlineEffect.lineColor2 = Color.white;
						outlineEffect.additiveRendering = true;
						SkinnedMeshRenderer[] array = ((!RuleUtilty.EnableAvater()) ? targetPlayer.thirdPersonUnityObjects.CareerSkins.ToArray() : targetPlayer.thirdPersonUnityObjects.ThirdTran.BodyTransform.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>());
						foreach (SkinnedMeshRenderer skinnedMeshRenderer in array)
						{
							if (skinnedMeshRenderer.GetComponent<Outline>() == null)
							{
								skinnedMeshRenderer.gameObject.AddComponent<Outline>();
							}
						}
						if (targetPlayer.IsDead())
						{
							SkinnedMeshRenderer[] array2 = array;
							for (int i = 0; i < array2.Length; i++)
							{
								Outline component = array2[i].GetComponent<Outline>();
								if (component != null)
								{
									UnityEngine.Object.Destroy(component);
								}
							}
						}
					}
					if (!CheatThread._Visual.glow || !CheatThread._Visual.Enable)
					{
						SkinnedMeshRenderer[] array2 = ((!RuleUtilty.EnableAvater()) ? targetPlayer.thirdPersonUnityObjects.CareerSkins.ToArray() : targetPlayer.thirdPersonUnityObjects.ThirdTran.BodyTransform.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>());
						for (int i = 0; i < array2.Length; i++)
						{
							Outline component2 = array2[i].GetComponent<Outline>();
							if (component2 != null)
							{
								UnityEngine.Object.Destroy(component2);
							}
						}
					}
					Contexts.sharedInstance.battleRoom.roomData.IsObserver = CheatThread._Visual.showRadar && CheatThread._Visual.Enable;
					if (targetPlayer != null && targetPlayer.hasBasicInfo && !targetPlayer.IsDead() && CheatThread._Visual.Enable)
					{
						Vector3 vector = Contexts.sharedInstance.worldCamera.unityObjects.mainCamera.WorldToScreenPoint(updateData.own.GetHitBox("Bip01_Head").position);
						Vector3 vector2 = positionHelper.EntityPos2World(positionHelper.GetPosition(targetPlayer));
						Vector3 vector3 = Camera.main.WorldToScreenPoint(vector2);
						Vector2 vector4 = new Vector2(vector.x, (float)Screen.height - vector.y);
						if (vector3.z >= 0f)
						{
							Vector3 vector5 = Camera.main.WorldToScreenPoint(vector2 + new Vector3(0f, 180f, 0f));
							float num = Mathf.Abs(vector3.y - vector5.y);
							float num2 = num * 0.51542f;
							Rect rect = new Rect(vector5.x - num2 / 2f, (float)Screen.height - vector5.y, num2, num);
							float num3 = (float)targetPlayer.GetHpPercent();
							float magnitude = (positionHelper.GetPosition(targetPlayer) - positionHelper.GetPosition(updateData.own)).magnitude;
							if (targetPlayer.orientation.Pitch == 88.8f || targetPlayer.orientation.Pitch == -88.8f)
							{
								GizmosProGraph.DrawTextWithShadow(new Vector2(vector5.x, rect.yMax + 25f), "(低|抬)头解析中(概率误判),真头则空枪", Color.red);
							}
							if (CheatThread._Visual.showBox)
							{
								GUI.color = Color.black;
								GizmosProGraph.DrawBox(new Vector2(rect.x, rect.y), new Vector2(rect.width, rect.height), 1f);
							}
							if (CheatThread._Visual.showWeapon)
							{
								visual_esp.DrawStyledText(new Vector2(vector5.x, rect.yMax + 6f), targetPlayer.currentWeapon.WeaponInfo.StringName, Color.red);
							}
							if (CheatThread._Visual.showAngle)
							{
								string text = visual_esp.CreateAngleString(targetPlayer.orientation.Pitch, targetPlayer.orientation.Yaw);
								visual_esp.DrawStyledText(new Vector2(vector5.x, rect.yMax + 18f), text, Color.white);
							}
							if (CheatThread._Visual.showName)
							{
								visual_esp.DrawStyledText(new Vector2(vector5.x, (float)Screen.height - vector5.y - 18f), string.Format("[{0}]{1}", targetPlayer.GetId(), targetPlayer.basicInfo.PlayerName), Color.red);
							}
							if (CheatThread._Visual.showC4 && targetPlayer.basicInfo.Current.HasC4)
							{
								GUI.color = new Color(0.8f, 0f, 0f, 0.8f);
								Vector3 vector6 = Contexts.sharedInstance.worldCamera.unityObjects.mainCamera.WorldToScreenPoint(targetPlayer.GetHitBox("Bip01_Head").position);
								GizmosProGraph.DrawDot(new Vector2(vector6.x, (float)Screen.height - vector6.y));
							}
							if (rage_silentbot.flag5 && rage_silentbot.AutoAttack)
							{
								Vector3 vector7 = Contexts.sharedInstance.worldCamera.unityObjects.mainCamera.WorldToScreenPoint(rage_silentbot.c_aimPos);
								Vector2 vector8 = new Vector2(vector7.x, (float)Screen.height - vector7.y);
								if (vector7.z > 0f)
								{
									GUI.color = Color.red;
									GizmosProGraph.DrawLine(vector4, vector8, 1.5f);
								}
							}
							if (rage_silentbot.flag5 && rage_silentbot.ManualAttack)
							{
								Vector3 vector9 = Contexts.sharedInstance.worldCamera.unityObjects.mainCamera.WorldToScreenPoint(rage_silentbot.h_aimPos);
								Vector2 vector10 = new Vector2(vector9.x, (float)Screen.height - vector9.y);
								if (vector9.z > 0f)
								{
									GUI.color = Color.green;
									GizmosProGraph.DrawLine(vector4, vector10, 1f);
								}
							}
							if (CheatThread._Visual.showHp && num3 != 0f)
							{
								visual_esp.DrawHealth(rect, num, num3);
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00008FE8 File Offset: 0x000071E8
		private static string CreateAngleString(float p, float y)
		{
			p = p * 1.00001f / 1.00001f;
			y = y + 1.00001f - 1.00001f;
			int num = (int)(p * 10f);
			int num2 = (int)(y * 10f);
			StringBuilder stringBuilder = new StringBuilder(16);
			stringBuilder.Append('[');
			stringBuilder.Append((float)num / 10f);
			stringBuilder.Append(" | ");
			stringBuilder.Append((float)num2 / 10f);
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000906D File Offset: 0x0000726D
		public visual_esp()
		{
		}

		// Token: 0x04000089 RID: 137
		private static GUIStyle _textStyle;
	}
}
