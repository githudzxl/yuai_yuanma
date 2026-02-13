using System;
using System.IO;
using System.Linq;
using EyeRecall.Helper;
using UnityEngine;

namespace EyeRecall.Function
{
	// Token: 0x02000013 RID: 19
	internal class drawMenu
	{
		// Token: 0x06000060 RID: 96 RVA: 0x00004920 File Offset: 0x00002B20
		private static void UIInit()
		{
			try
			{
				GUISkin guiskin = ScriptableObject.CreateInstance<GUISkin>();
				Font font = Font.CreateDynamicFontFromOSFont(new string[] { "PingFang SC", "Microsoft YaHei", "Arial" }, 14);
				if (font == null)
				{
					Debug.LogError("未能加载任何可用字体。");
				}
				guiskin.font = font;
				Color color = new Color(0.1f, 0.1f, 0.12f, 1f);
				Color color2 = new Color(0.18f, 0.18f, 0.22f, 1f);
				Color color3 = new Color(0.25f, 0.25f, 0.35f, 1f);
				Color color4 = new Color(0.35f, 0.35f, 0.45f, 1f);
				Color color5 = new Color(0.9f, 0.9f, 0.95f, 1f);
				Color cyan = Color.cyan;
				Color color6 = new Color(0.15f, 0.15f, 0.25f, 1f);
				Color color7 = new Color(0.25f, 0.25f, 0.45f, 1f);
				guiskin.window.normal.background = drawMenu.MakeTex(2, 2, color);
				guiskin.window.padding = new RectOffset(15, 15, 15, 15);
				guiskin.button.normal.background = drawMenu.MakeTex(2, 2, color3);
				guiskin.button.hover.background = drawMenu.MakeTex(2, 2, color4);
				guiskin.button.active.background = drawMenu.MakeTex(2, 2, color4);
				guiskin.button.normal.textColor = color5;
				guiskin.button.hover.textColor = cyan;
				guiskin.button.padding = new RectOffset(10, 10, 8, 8);
				guiskin.button.alignment = TextAnchor.MiddleCenter;
				guiskin.button.fontSize = 14;
				guiskin.label.normal.textColor = color5;
				guiskin.label.fontSize = 14;
				guiskin.textArea.normal.background = drawMenu.MakeTex(2, 2, color3);
				guiskin.textArea.normal.textColor = color5;
				guiskin.textArea.padding = new RectOffset(5, 5, 5, 5);
				guiskin.textArea.fontSize = 14;
				drawMenu.toggleStyle = new GUIStyle
				{
					normal = 
					{
						background = drawMenu.MakeTex(44, 20, color3)
					},
					onNormal = 
					{
						background = drawMenu.MakeTex(44, 20, cyan)
					},
					fixedWidth = 44f,
					fixedHeight = 20f,
					border = new RectOffset(2, 2, 2, 2)
				};
				drawMenu.toggleKnobStyle = new GUIStyle
				{
					normal = 
					{
						background = drawMenu.MakeTex(16, 16, Color.white)
					},
					fixedWidth = 16f,
					fixedHeight = 16f
				};
				guiskin.horizontalSlider.normal.background = drawMenu.MakeTex(2, 2, color2);
				guiskin.horizontalSlider.fixedHeight = 6f;
				guiskin.horizontalSliderThumb.normal.background = drawMenu.MakeTex(18, 18, cyan);
				guiskin.horizontalSliderThumb.hover.background = drawMenu.MakeTex(18, 18, cyan * 1.1f);
				guiskin.horizontalSliderThumb.fixedWidth = 18f;
				guiskin.horizontalSliderThumb.fixedHeight = 18f;
				guiskin.horizontalSliderThumb.margin = new RectOffset(0, 0, -6, 0);
				drawMenu.sliderLabelStyle = new GUIStyle(guiskin.label)
				{
					alignment = TextAnchor.MiddleRight,
					fontSize = 14
				};
				drawMenu.toolbarStyle = new GUIStyle(guiskin.button)
				{
					normal = 
					{
						background = drawMenu.MakeTex(2, 2, color6),
						textColor = color5
					},
					hover = 
					{
						background = drawMenu.MakeTex(2, 2, color7),
						textColor = cyan
					},
					onNormal = 
					{
						background = drawMenu.MakeTex(2, 2, color7),
						textColor = cyan
					},
					fontSize = 14,
					alignment = TextAnchor.MiddleCenter,
					margin = new RectOffset(0, 0, 0, 0),
					padding = new RectOffset(15, 15, 10, 10),
					stretchWidth = true
				};
				drawMenu.titleStyle = new GUIStyle(guiskin.label)
				{
					normal = 
					{
						textColor = cyan
					},
					fontSize = 24,
					fontStyle = FontStyle.Bold,
					alignment = TextAnchor.MiddleCenter,
					padding = new RectOffset(0, 0, 10, 15)
				};
				drawMenu.sectionBoxStyle = new GUIStyle(guiskin.box)
				{
					normal = 
					{
						background = drawMenu.MakeTex(2, 2, color2)
					},
					padding = new RectOffset(15, 15, 15, 15),
					margin = new RectOffset(5, 5, 10, 10)
				};
				drawMenu.sectionTitleStyle = new GUIStyle(guiskin.label)
				{
					normal = 
					{
						textColor = cyan
					},
					fontSize = 18,
					fontStyle = FontStyle.Bold,
					alignment = TextAnchor.MiddleLeft,
					padding = new RectOffset(0, 0, 8, 12)
				};
				GUI.skin = guiskin;
				drawMenu.labelStyle = GUI.skin.label;
				drawMenu.buttonStyle = GUI.skin.button;
				drawMenu.sliderStyle = GUI.skin.horizontalSliderThumb;
				if (drawMenu.s_gradientBackgroundTex != null)
				{
					UnityEngine.Object.Destroy(drawMenu.s_gradientBackgroundTex);
				}
				drawMenu.s_gradientBackgroundTex = new Texture2D(1, (int)drawMenu.menuRectGlobal.height);
				Color color8 = new Color(0.12f, 0.12f, 0.22f, 1f);
				Color color9 = new Color(0.08f, 0.08f, 0.18f, 1f);
				for (int i = 0; i < drawMenu.s_gradientBackgroundTex.height; i++)
				{
					drawMenu.s_gradientBackgroundTex.SetPixel(0, i, Color.Lerp(color8, color9, (float)i / (float)drawMenu.s_gradientBackgroundTex.height));
				}
				drawMenu.s_gradientBackgroundTex.Apply();
				drawMenu.s_gradientBackgroundTex.hideFlags = HideFlags.HideAndDontSave;
				drawMenu.isInitialized = true;
			}
			catch (Exception ex)
			{
				Debug.LogError("UI初始化异常: " + ex.Message);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00004F64 File Offset: 0x00003164
		internal static void run(int id)
		{
			try
			{
				if (!drawMenu.isInitialized)
				{
					drawMenu.UIInit();
				}
				drawMenu.DrawGradientBackground(drawMenu.menuRectGlobal);
				drawMenu.DrawNeonBorder(drawMenu.menuRectGlobal);
				GUI.BeginGroup(drawMenu.menuRectGlobal);
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				GUILayout.FlexibleSpace();
				GUILayout.Label("雨爱", drawMenu.titleStyle, new GUILayoutOption[0]);
				GUILayout.FlexibleSpace();
				GUILayout.Label(DateTime.Now.ToString("HH:mm:ss"), drawMenu.labelStyle, new GUILayoutOption[] { GUILayout.Width(80f) });
				GUILayout.EndHorizontal();
				GUILayout.Space(10f);
				string[] array = new string[] { "普通自瞄", "暴力自瞄", "视觉", "杂项", "反自瞄" };
				drawMenu.ToolBarValue = GUILayout.Toolbar(drawMenu.ToolBarValue, array, drawMenu.toolbarStyle, new GUILayoutOption[] { GUILayout.Height(35f) });
				GUILayout.Space(15f);
				drawMenu.mainScrollPosition = GUILayout.BeginScrollView(drawMenu.mainScrollPosition, false, false, new GUILayoutOption[0]);
				switch (drawMenu.ToolBarValue)
				{
				case 0:
					drawMenu.DrawLegitAimbotTab();
					break;
				case 1:
					drawMenu.DrawRageAimbotTab();
					break;
				case 2:
					drawMenu.DrawVisualsTab();
					break;
				case 3:
					drawMenu.DrawMiscTab();
					break;
				case 4:
					drawMenu.DrawAntiAimTab();
					break;
				}
				GUILayout.EndScrollView();
				GUI.EndGroup();
				GUI.DragWindow();
			}
			catch (Exception ex)
			{
				Debug.LogError("菜单渲染错误: " + ex.Message);
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00005104 File Offset: 0x00003304
		private static void DrawLegitAimbotTab()
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.BeginVertical(drawMenu.sectionBoxStyle, new GUILayoutOption[] { GUILayout.Width(370f) });
			GUILayout.Label("核心设置", drawMenu.sectionTitleStyle, new GUILayoutOption[0]);
			CheatThread._aimBot.Enable = drawMenu.UiToggle(CheatThread._aimBot.Enable, "启用普通自瞄");
			if (CheatThread._aimBot.Enable)
			{
				CheatThread._aimBot.LegitAimbot = drawMenu.UiToggle(CheatThread._aimBot.LegitAimbot, "平滑模式");
				CheatThread._aimBot.InvincibleNoAim = drawMenu.UiToggle(CheatThread._aimBot.InvincibleNoAim, "无敌不锁");
				CheatThread._aimBot.PassWallAim = drawMenu.UiToggle(CheatThread._aimBot.PassWallAim, "墙后不锁");
			}
			GUILayout.EndVertical();
			GUILayout.BeginVertical(drawMenu.sectionBoxStyle, new GUILayoutOption[0]);
			GUILayout.Label("参数调整", drawMenu.sectionTitleStyle, new GUILayoutOption[0]);
			if (CheatThread._aimBot.Enable)
			{
				if (CheatThread._aimBot.LegitAimbot)
				{
					CheatThread._aimBot.LegitSpeed = drawMenu.UiHorizontalSlider(CheatThread._aimBot.LegitSpeed, "平滑速度", 1f, 600f);
				}
				CheatThread._aimBot.aimRange = drawMenu.UiHorizontalSlider(CheatThread._aimBot.aimRange, "自瞄范围", 1, 500);
				GUILayout.Space(10f);
				CheatThread._aimBot.AimPosMode = drawMenu.DrawDropdown("瞄准部位", CheatThread._aimBot.AimPosMode, EyeRecall.Helper.config.aimPosText, ref drawMenu.aimPosDropdown);
				GUILayout.Space(10f);
				CheatThread._aimBot.aimbotKey = drawMenu.DrawKeybindButton("自瞄热键", CheatThread._aimBot.aimbotKey, ref drawMenu.isWaitingForAimKey);
			}
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000052CC File Offset: 0x000034CC
		private static void DrawRageAimbotTab()
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.BeginVertical(drawMenu.sectionBoxStyle, new GUILayoutOption[] { GUILayout.Width(370f) });
			GUILayout.Label("核心设置", drawMenu.sectionTitleStyle, new GUILayoutOption[0]);
			CheatThread._rageBot.Enable = drawMenu.UiToggle(CheatThread._rageBot.Enable, "启用暴力自瞄");
			if (CheatThread._rageBot.Enable)
			{
				CheatThread._rageBot.Resolver = drawMenu.UiToggle(CheatThread._rageBot.Resolver, "启用解析器");
				CheatThread._rageBot.ManualTiggerbot = drawMenu.UiToggle(CheatThread._rageBot.ManualTiggerbot, "中键手动开火");
				CheatThread._rageBot.Multiplehitbox = drawMenu.UiToggle(CheatThread._rageBot.Multiplehitbox, "多部位漏打补偿");
			}
			GUILayout.EndVertical();
			GUILayout.BeginVertical(drawMenu.sectionBoxStyle, new GUILayoutOption[0]);
			GUILayout.Label("参数调整", drawMenu.sectionTitleStyle, new GUILayoutOption[0]);
			if (CheatThread._rageBot.Enable)
			{
				CheatThread._rageBot.Accurary = drawMenu.UiHorizontalSlider(CheatThread._rageBot.Accurary, "命中率", 0f, 80f);
				CheatThread._rageBot.a_resolovertype = drawMenu.DrawDropdown("解析类型", CheatThread._rageBot.a_resolovertype, EyeRecall.Helper.config.ResolverModeText, ref drawMenu.resolverType);
				if (CheatThread._rageBot.a_resolovertype == 1)
				{
					GUILayout.Label("提示: 侧键解析抬头/低头", drawMenu.labelStyle, new GUILayoutOption[0]);
				}
				if (CheatThread._rageBot.a_resolovertype == 2)
				{
					GUILayout.Label("提示: 50%概率无视真假头", drawMenu.labelStyle, new GUILayoutOption[0]);
				}
				GUILayout.Space(10f);
				if (GUILayout.Button("选择命中部位", drawMenu.buttonStyle, new GUILayoutOption[0]))
				{
					drawMenu.rageBotPosDropdown = !drawMenu.rageBotPosDropdown;
				}
				if (drawMenu.rageBotPosDropdown)
				{
					drawMenu.rageBotPosScrollPosition = GUILayout.BeginScrollView(drawMenu.rageBotPosScrollPosition, new GUILayoutOption[] { GUILayout.Height(150f) });
					for (int i = 0; i < EyeRecall.Helper.config.rageBotPosText.Length; i++)
					{
						CheatThread._rageBot.aimPos[i] = drawMenu.UiToggle(CheatThread._rageBot.aimPos[i], EyeRecall.Helper.config.rageBotPosText[i]);
					}
					GUILayout.EndScrollView();
				}
			}
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000550C File Offset: 0x0000370C
		private static void DrawVisualsTab()
		{
			CheatThread._Visual.Enable = drawMenu.UiToggle(CheatThread._Visual.Enable, "启用视觉辅助");
			if (!CheatThread._Visual.Enable)
			{
				return;
			}
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.BeginVertical(drawMenu.sectionBoxStyle, new GUILayoutOption[] { GUILayout.Width(370f) });
			GUILayout.Label("玩家透视", drawMenu.sectionTitleStyle, new GUILayoutOption[0]);
			CheatThread._Visual.showBox = drawMenu.UiToggle(CheatThread._Visual.showBox, "方框");
			CheatThread._Visual.showName = drawMenu.UiToggle(CheatThread._Visual.showName, "昵称");
			CheatThread._Visual.showHp = drawMenu.UiToggle(CheatThread._Visual.showHp, "血条");
			CheatThread._Visual.showWeapon = drawMenu.UiToggle(CheatThread._Visual.showWeapon, "武器");
			CheatThread._Visual.showC4 = drawMenu.UiToggle(CheatThread._Visual.showC4, "C4携带者");
			CheatThread._Visual.showAngle = drawMenu.UiToggle(CheatThread._Visual.showAngle, "显示角度");
			GUILayout.EndVertical();
			GUILayout.BeginVertical(drawMenu.sectionBoxStyle, new GUILayoutOption[0]);
			GUILayout.Label("其他视觉", drawMenu.sectionTitleStyle, new GUILayoutOption[0]);
			CheatThread._Visual.showAimRange = drawMenu.UiToggle(CheatThread._Visual.showAimRange, "自瞄范围圈");
			CheatThread._Visual.glow = drawMenu.UiToggle(CheatThread._Visual.glow, "人物发光");
			if (CheatThread._Visual.glow)
			{
				CheatThread._Visual.glowColorMode = drawMenu.DrawDropdown("  发光颜色", CheatThread._Visual.glowColorMode, EyeRecall.Helper.config.glowColorOptions, ref drawMenu.glowColorDropdown);
			}
			CheatThread._Visual.showRadar = drawMenu.UiToggle(CheatThread._Visual.showRadar, "雷达");
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000056F8 File Offset: 0x000038F8
		private static void DrawMiscTab()
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.BeginVertical(drawMenu.sectionBoxStyle, new GUILayoutOption[] { GUILayout.Width(370f) });
			GUILayout.Label("功能", drawMenu.sectionTitleStyle, new GUILayoutOption[0]);
			CheatThread._misc.noRecoil = drawMenu.UiToggle(CheatThread._misc.noRecoil, "智能压枪");
			CheatThread._misc.bhop = drawMenu.UiToggle(CheatThread._misc.bhop, "自动连跳");
			if (CheatThread._misc.bhop)
			{
				CheatThread._misc.bhoptype = drawMenu.DrawDropdown("  连跳类型", CheatThread._misc.bhoptype, EyeRecall.Helper.config.bhopModeText, ref drawMenu.bhopModeDropdown);
			}
			CheatThread._misc.InstanceSniper = drawMenu.UiToggle(CheatThread._misc.InstanceSniper, "右键瞬狙");
			CheatThread._misc.tiggerBot = drawMenu.UiToggle(CheatThread._misc.tiggerBot, "自动扳机");
			if (CheatThread._misc.tiggerBot)
			{
				CheatThread._misc.tiggerBotDelayedTime = drawMenu.UiHorizontalSlider(CheatThread._misc.tiggerBotDelayedTime, "  扳机延迟", 1, 250);
			}
			CheatThread._misc.gHost = drawMenu.UiToggle(CheatThread._misc.gHost, "滑步");
			CheatThread._misc.shieldbayonet = drawMenu.UiToggle(CheatThread._misc.shieldbayonet, "屏蔽刺刀");
			CheatThread._misc.thirdPerson = drawMenu.UiToggle(CheatThread._misc.thirdPerson, "第三人称");
			if (CheatThread._misc.thirdPerson)
			{
				CheatThread._misc.thirdPersonKey = drawMenu.DrawKeybindButton("  切换热键", CheatThread._misc.thirdPersonKey, ref drawMenu.isWaitingForThirdPressKey);
			}
			CheatThread._misc.fakeFov = drawMenu.UiToggle(CheatThread._misc.fakeFov, "修改视野");
			if (CheatThread._misc.fakeFov)
			{
				CheatThread._misc.fovValue = drawMenu.UiHorizontalSlider(CheatThread._misc.fovValue, "  视野值", 30, 175);
			}
			CheatThread._misc.fakeLag = drawMenu.UiToggle(CheatThread._misc.fakeLag, "假性延迟");
			if (CheatThread._misc.fakeLag)
			{
				CheatThread._misc.fakeLagTick = drawMenu.UiHorizontalSlider(CheatThread._misc.fakeLagTick, "  延迟量", 0, 300);
			}
			GUILayout.EndVertical();
			GUILayout.BeginVertical(drawMenu.sectionBoxStyle, new GUILayoutOption[0]);
			GUILayout.Label("魔法攻击", drawMenu.sectionTitleStyle, new GUILayoutOption[0]);
			CheatThread._misc.autoChatEnable = drawMenu.UiToggle(CheatThread._misc.autoChatEnable, "启用自动喊话");
			if (CheatThread._misc.autoChatEnable)
			{
				CheatThread._misc.autoChatDelay = drawMenu.UiHorizontalSlider(CheatThread._misc.autoChatDelay, "喊话间隔 (毫秒)", 100, 10000);
				GUILayout.Label("喊话内容:", new GUILayoutOption[0]);
				drawMenu.autoChatMessageScrollPosition = GUILayout.BeginScrollView(drawMenu.autoChatMessageScrollPosition, new GUILayoutOption[] { GUILayout.Height(60f) });
				CheatThread._misc.autoChatMessage = GUILayout.TextArea(CheatThread._misc.autoChatMessage, new GUILayoutOption[] { GUILayout.ExpandHeight(true) });
				GUILayout.EndScrollView();
			}
			GUILayout.Space(20f);
			GUILayout.Label("配置管理", drawMenu.sectionTitleStyle, new GUILayoutOption[0]);
			if (EyeRecall.Helper.config._config != null && EyeRecall.Helper.config._config.Length != 0)
			{
				EyeRecall.Helper.config._configKey = drawMenu.DrawDropdown("选择配置", EyeRecall.Helper.config._configKey, EyeRecall.Helper.config._config.Select(new Func<string, string>(Path.GetFileName)).ToArray<string>(), ref drawMenu.configDropdown);
				GUILayout.Label("当前: " + EyeRecall.Helper.config.configTitle, drawMenu.labelStyle, new GUILayoutOption[0]);
			}
			else
			{
				GUILayout.Label("未找到配置文件。", drawMenu.labelStyle, new GUILayoutOption[0]);
			}
			GUILayout.FlexibleSpace();
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			if (GUILayout.Button("加载", drawMenu.buttonStyle, new GUILayoutOption[0]))
			{
				EyeRecall.Helper.config.LoadConfig();
			}
			GUILayout.Space(10f);
			if (GUILayout.Button("保存", drawMenu.buttonStyle, new GUILayoutOption[0]))
			{
				EyeRecall.Helper.config.WriteConfig();
			}
			GUILayout.EndHorizontal();
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00005B18 File Offset: 0x00003D18
		private static void DrawAntiAimTab()
		{
			CheatThread._antiAim.Enable = drawMenu.UiToggle(CheatThread._antiAim.Enable, "启用反自瞄");
			if (!CheatThread._antiAim.Enable)
			{
				return;
			}
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.BeginVertical(drawMenu.sectionBoxStyle, new GUILayoutOption[] { GUILayout.Width(370f) });
			GUILayout.Label("模式选择", drawMenu.sectionTitleStyle, new GUILayoutOption[0]);
			CheatThread._antiAim.AntiAimMode = drawMenu.DrawDropdown("基础类型", CheatThread._antiAim.AntiAimMode, EyeRecall.Helper.config.AntiAimModeText, ref drawMenu.antiAimModeDropdown);
			CheatThread._antiAim.AntiAimPitchMode = drawMenu.DrawDropdown("俯仰轴", CheatThread._antiAim.AntiAimPitchMode, EyeRecall.Helper.config.AntiAimPitchModeText, ref drawMenu.antiAimPitchModeDropdown);
			CheatThread._antiAim.AntiAimYawMode = drawMenu.DrawDropdown("偏航轴", CheatThread._antiAim.AntiAimYawMode, EyeRecall.Helper.config.AntiAimYawModeText, ref drawMenu.antiAimYawModeDropdown);
			GUILayout.EndVertical();
			GUILayout.BeginVertical(drawMenu.sectionBoxStyle, new GUILayoutOption[0]);
			GUILayout.Label("偏航轴参数", drawMenu.sectionTitleStyle, new GUILayoutOption[0]);
			switch (CheatThread._antiAim.AntiAimYawMode)
			{
			case 0:
			case 2:
				CheatThread._antiAim.fakeYaw = drawMenu.UiHorizontalSlider(CheatThread._antiAim.fakeYaw, "偏航角度", -180, 180);
				if (CheatThread._antiAim.AntiAimYawMode == 2)
				{
					CheatThread._antiAim.JitterMax = drawMenu.UiHorizontalSlider(CheatThread._antiAim.JitterMax, "抖动最大值", -180, 180);
					CheatThread._antiAim.JitterMin = drawMenu.UiHorizontalSlider(CheatThread._antiAim.JitterMin, "抖动最小值", -180, 180);
				}
				break;
			case 1:
				CheatThread._antiAim.RotateSpeed = drawMenu.UiHorizontalSlider(CheatThread._antiAim.RotateSpeed, "旋转速度", 1, 1000);
				break;
			}
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00005D06 File Offset: 0x00003F06
		private static bool CheckMouse()
		{
			return Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.Mouse2) || Input.GetKeyDown(KeyCode.Mouse3) || Input.GetKeyDown(KeyCode.Mouse4);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00005D44 File Offset: 0x00003F44
		private static void DrawGradientBackground(Rect rect)
		{
			if (drawMenu.s_gradientBackgroundTex != null)
			{
				GUI.DrawTexture(rect, drawMenu.s_gradientBackgroundTex);
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00005D60 File Offset: 0x00003F60
		private static void DrawNeonBorder(Rect rect)
		{
			drawMenu.borderColorTime += Time.deltaTime * 0.75f;
			Color color = Color.HSVToRGB(Mathf.Repeat(drawMenu.borderColorTime, 1f), 0.9f, 1f);
			color.a = 0.9f + 0.1f * ((Mathf.Sin(Time.time * 4f) + 1f) / 2f);
			GUI.color = color;
			float num = 2f;
			GUI.DrawTexture(new Rect(rect.x, rect.y, rect.width, num), Texture2D.whiteTexture);
			GUI.DrawTexture(new Rect(rect.x, rect.y + rect.height - num, rect.width, num), Texture2D.whiteTexture);
			GUI.DrawTexture(new Rect(rect.x, rect.y, num, rect.height), Texture2D.whiteTexture);
			GUI.DrawTexture(new Rect(rect.x + rect.width - num, rect.y, num, rect.height), Texture2D.whiteTexture);
			GUI.color = Color.white;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00005E90 File Offset: 0x00004090
		private static Texture2D MakeTex(int width, int height, Color col)
		{
			Color[] array = new Color[width * height];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = col;
			}
			Texture2D texture2D = new Texture2D(width, height);
			texture2D.SetPixels(array);
			texture2D.Apply();
			texture2D.hideFlags = HideFlags.HideAndDontSave;
			return texture2D;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00005ED8 File Offset: 0x000040D8
		private static bool UiToggle(bool value, string label)
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			Rect rect = GUILayoutUtility.GetRect(drawMenu.toggleStyle.fixedWidth, drawMenu.toggleStyle.fixedHeight);
			bool flag = GUI.Toggle(rect, value, GUIContent.none, drawMenu.toggleStyle);
			GUI.Box(new Rect(rect.x + (flag ? (rect.width - drawMenu.toggleKnobStyle.fixedWidth - 2f) : 2f), rect.y + (rect.height - drawMenu.toggleKnobStyle.fixedHeight) / 2f, drawMenu.toggleKnobStyle.fixedWidth, drawMenu.toggleKnobStyle.fixedHeight), GUIContent.none, drawMenu.toggleKnobStyle);
			GUILayout.Space(10f);
			if (GUILayout.Button(label, new GUIStyle(GUI.skin.label)
			{
				alignment = TextAnchor.MiddleLeft
			}, new GUILayoutOption[0]))
			{
				flag = !flag;
			}
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.Space(8f);
			return flag;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00005FD8 File Offset: 0x000041D8
		private static int UiHorizontalSlider(int value, string label, int min, int max)
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label(label, drawMenu.labelStyle, new GUILayoutOption[] { GUILayout.Width(120f) });
			int num = (int)GUILayout.HorizontalSlider((float)value, (float)min, (float)max, new GUILayoutOption[] { GUILayout.ExpandWidth(true) });
			GUILayout.Label(num.ToString(), drawMenu.sliderLabelStyle, new GUILayoutOption[] { GUILayout.Width(40f) });
			GUILayout.EndHorizontal();
			GUILayout.Space(8f);
			return num;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00006060 File Offset: 0x00004260
		private static float UiHorizontalSlider(float value, string label, float min, float max)
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label(label, drawMenu.labelStyle, new GUILayoutOption[] { GUILayout.Width(120f) });
			float num = GUILayout.HorizontalSlider(value, min, max, new GUILayoutOption[] { GUILayout.ExpandWidth(true) });
			GUILayout.Label(string.Format("{0:F1}", num), drawMenu.sliderLabelStyle, new GUILayoutOption[] { GUILayout.Width(40f) });
			GUILayout.EndHorizontal();
			GUILayout.Space(8f);
			return num;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000060EC File Offset: 0x000042EC
		private static string DrawKeybindButton(string label, string currentKey, ref bool isWaiting)
		{
			string text = currentKey;
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label(label, drawMenu.labelStyle, new GUILayoutOption[] { GUILayout.Width(120f) });
			if (GUILayout.Button(isWaiting ? drawMenu.waitingText : text, drawMenu.buttonStyle, new GUILayoutOption[] { GUILayout.ExpandWidth(true) }))
			{
				isWaiting = !isWaiting;
			}
			if (isWaiting)
			{
				if (Event.current.isKey && Event.current.keyCode != KeyCode.None)
				{
					text = Event.current.keyCode.ToString();
					isWaiting = false;
				}
				else if (drawMenu.CheckMouse())
				{
					if (Input.GetKeyDown(KeyCode.Mouse0))
					{
						text = "Mouse0";
					}
					else if (Input.GetKeyDown(KeyCode.Mouse1))
					{
						text = "Mouse1";
					}
					else if (Input.GetKeyDown(KeyCode.Mouse2))
					{
						text = "Mouse2";
					}
					else if (Input.GetKeyDown(KeyCode.Mouse3))
					{
						text = "Mouse3";
					}
					else if (Input.GetKeyDown(KeyCode.Mouse4))
					{
						text = "Mouse4";
					}
					isWaiting = false;
				}
			}
			GUILayout.EndHorizontal();
			GUILayout.Space(8f);
			return text;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000620C File Offset: 0x0000440C
		private static int DrawDropdown(string label, int selectedIndex, string[] options, ref bool isOpen)
		{
			int num = selectedIndex;
			GUILayout.BeginVertical(new GUILayoutOption[0]);
			string text = ((num >= 0 && num < options.Length) ? options[num] : "无");
			if (GUILayout.Button(label + ": " + text, drawMenu.buttonStyle, new GUILayoutOption[0]))
			{
				isOpen = !isOpen;
			}
			if (isOpen)
			{
				GUILayout.BeginVertical(drawMenu.sectionBoxStyle, new GUILayoutOption[0]);
				for (int i = 0; i < options.Length; i++)
				{
					if (GUILayout.Button(options[i], drawMenu.buttonStyle, new GUILayoutOption[0]))
					{
						num = i;
						isOpen = false;
					}
				}
				GUILayout.EndVertical();
			}
			GUILayout.EndVertical();
			return num;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000062A7 File Offset: 0x000044A7
		public drawMenu()
		{
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000062AF File Offset: 0x000044AF
		// Note: this type is marked as 'beforefieldinit'.
		static drawMenu()
		{
		}

		// Token: 0x0400004E RID: 78
		private static GUIStyle buttonStyle;

		// Token: 0x0400004F RID: 79
		private static GUIStyle labelStyle;

		// Token: 0x04000050 RID: 80
		private static GUIStyle toggleStyle;

		// Token: 0x04000051 RID: 81
		private static GUIStyle toggleKnobStyle;

		// Token: 0x04000052 RID: 82
		private static GUIStyle sliderStyle;

		// Token: 0x04000053 RID: 83
		private static GUIStyle toolbarStyle;

		// Token: 0x04000054 RID: 84
		private static GUIStyle titleStyle;

		// Token: 0x04000055 RID: 85
		private static GUIStyle sectionBoxStyle;

		// Token: 0x04000056 RID: 86
		private static GUIStyle sectionTitleStyle;

		// Token: 0x04000057 RID: 87
		private static GUIStyle sliderLabelStyle;

		// Token: 0x04000058 RID: 88
		private static int ToolBarValue;

		// Token: 0x04000059 RID: 89
		private static bool rageBotPosDropdown;

		// Token: 0x0400005A RID: 90
		private static bool bhopModeDropdown;

		// Token: 0x0400005B RID: 91
		private static bool antiAimModeDropdown;

		// Token: 0x0400005C RID: 92
		private static bool antiAimPitchModeDropdown;

		// Token: 0x0400005D RID: 93
		private static bool antiAimYawModeDropdown;

		// Token: 0x0400005E RID: 94
		private static bool resolverType;

		// Token: 0x0400005F RID: 95
		private static bool aimPosDropdown;

		// Token: 0x04000060 RID: 96
		private static bool configDropdown;

		// Token: 0x04000061 RID: 97
		private static bool glowColorDropdown;

		// Token: 0x04000062 RID: 98
		private static bool isWaitingForAimKey;

		// Token: 0x04000063 RID: 99
		private static bool isWaitingForThirdPressKey;

		// Token: 0x04000064 RID: 100
		private static readonly string waitingText = "请按键...";

		// Token: 0x04000065 RID: 101
		internal static bool showMenu;

		// Token: 0x04000066 RID: 102
		private static Vector2 mainScrollPosition;

		// Token: 0x04000067 RID: 103
		private static Vector2 rageBotPosScrollPosition;

		// Token: 0x04000068 RID: 104
		private static Vector2 configScrollPosition;

		// Token: 0x04000069 RID: 105
		private static Vector2 autoChatMessageScrollPosition;

		// Token: 0x0400006A RID: 106
		private static float borderColorTime = 0f;

		// Token: 0x0400006B RID: 107
		private static bool isInitialized = false;

		// Token: 0x0400006C RID: 108
		private static Texture2D s_gradientBackgroundTex;

		// Token: 0x0400006D RID: 109
		internal static Rect menuRectGlobal = new Rect(10f, 10f, 800f, 650f);
	}
}
