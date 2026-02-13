using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace EyeRecall.Helper
{
	// Token: 0x02000009 RID: 9
	public class config
	{
		// Token: 0x0600002C RID: 44 RVA: 0x000031B0 File Offset: 0x000013B0
		internal static void WriteConfig()
		{
			string text = config.settingPath;
			string text2 = text + "\\SETTING.cfg";
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			if (config._config.Length != 0)
			{
				text2 = config._config[config._configKey] ?? "";
			}
			string text3 = JsonConvert.SerializeObject(new config.AllConfig
			{
				esp_config = CheatThread._Visual,
				aimbot_config = CheatThread._aimBot,
				antiAim_config = CheatThread._antiAim,
				misc_config = CheatThread._misc,
				rageBot_config = CheatThread._rageBot
			}, Formatting.Indented);
			try
			{
				FileStream fileStream = new FileStream(text2, FileMode.Create, FileAccess.Write);
				StreamWriter streamWriter = new StreamWriter(fileStream);
				streamWriter.Write(text3);
				streamWriter.Close();
				fileStream.Close();
			}
			catch (Exception ex)
			{
				config.configTitle = "写入失败,异常:" + ex.Message;
			}
			finally
			{
				config.configTitle = "写入成功";
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000032B0 File Offset: 0x000014B0
		internal static void LoadConfig()
		{
			if (Directory.Exists(config.settingPath))
			{
				string text = config.settingPath;
				string text2 = config._config[config._configKey] ?? "";
				if (!File.Exists(text2))
				{
					config.configTitle = "参数文件不存在";
					return;
				}
				try
				{
					FileStream fileStream = new FileStream(text2, FileMode.Open, FileAccess.Read);
					StreamReader streamReader = new StreamReader(fileStream);
					string text3 = streamReader.ReadToEnd();
					streamReader.Close();
					fileStream.Close();
					config.AllConfig allConfig = JsonConvert.DeserializeObject<config.AllConfig>(text3);
					CheatThread._Visual = allConfig.esp_config;
					CheatThread._aimBot = allConfig.aimbot_config;
					CheatThread._antiAim = allConfig.antiAim_config;
					CheatThread._misc = allConfig.misc_config;
					CheatThread._rageBot = allConfig.rageBot_config;
					config.configTitle = "读取成功";
				}
				catch (Exception ex)
				{
					config.configTitle = "读取失败,异常:" + ex.Message;
				}
				finally
				{
					config.configTitle = "读取成功";
				}
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000033A4 File Offset: 0x000015A4
		public config()
		{
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000033AC File Offset: 0x000015AC
		// Note: this type is marked as 'beforefieldinit'.
		static config()
		{
		}

		// Token: 0x0400002E RID: 46
		internal static string[] aimPosText = new string[] { "头部", "脖子", "胸部", "左脚", "右脚" };

		// Token: 0x0400002F RID: 47
		internal static string[] rageBotPosText = new string[]
		{
			"头部", "脖子", "左上臂", "右上臂", "左前臂", "右前臂", "左手", "右手", "腹部", "臀部",
			"左大腿", "右大腿", "左小腿", "右小腿", "左脚", "右脚"
		};

		// Token: 0x04000030 RID: 48
		internal static string[] AntiAimModeText = new string[] { "假头", "真头", "随机真假", "磕头" };

		// Token: 0x04000031 RID: 49
		internal static string[] AntiAimPitchModeText = new string[] { "向上", "平视", "向下" };

		// Token: 0x04000032 RID: 50
		internal static string[] bhopModeText = new string[] { "长按空格", "单按X键" };

		// Token: 0x04000033 RID: 51
		internal static string[] AntiAimYawModeText = new string[] { "手动", "旋转", "抖动", "侧对" };

		// Token: 0x04000034 RID: 52
		internal static string[] ResolverModeText = new string[] { "自动解析", "按键手动", "概率解析" };

		// Token: 0x04000035 RID: 53
		internal static string[] glowColorOptions = new string[] { "红色", "黄色", "蓝色", "绿色", "彩虹" };

		// Token: 0x04000036 RID: 54
		internal static string settingPath = "C:\\XXX";

		// Token: 0x04000037 RID: 55
		internal static string[] _config;

		// Token: 0x04000038 RID: 56
		internal static string[] _settings;

		// Token: 0x04000039 RID: 57
		internal static int _configKey;

		// Token: 0x0400003A RID: 58
		internal static string configTitle = "未加载配置";

		// Token: 0x0400003B RID: 59
		internal static bool first_attack = false;

		// Token: 0x0400003C RID: 60
		internal static PlayerEntity own;

		// Token: 0x0400003D RID: 61
		internal static bool is_forward = true;

		// Token: 0x0400003E RID: 62
		internal static double speed;

		// Token: 0x02000025 RID: 37
		public struct AllConfig
		{
			// Token: 0x040000A7 RID: 167
			public config.RageBot rageBot_config;

			// Token: 0x040000A8 RID: 168
			public config.aimBot aimbot_config;

			// Token: 0x040000A9 RID: 169
			public config.esp esp_config;

			// Token: 0x040000AA RID: 170
			public config.Misc misc_config;

			// Token: 0x040000AB RID: 171
			public config.AntiAim antiAim_config;
		}

		// Token: 0x02000026 RID: 38
		public struct Menu
		{
			// Token: 0x040000AC RID: 172
			public Texture2D menuBackground;

			// Token: 0x040000AD RID: 173
			public Texture2D menuForeground;

			// Token: 0x040000AE RID: 174
			public Texture2D menuNormal;

			// Token: 0x040000AF RID: 175
			public Texture2D menuHover;

			// Token: 0x040000B0 RID: 176
			public Texture2D boxColor;
		}

		// Token: 0x02000027 RID: 39
		public struct esp
		{
			// Token: 0x040000B1 RID: 177
			public bool glow;

			// Token: 0x040000B2 RID: 178
			public int glowColorMode;

			// Token: 0x040000B3 RID: 179
			public bool Enable;

			// Token: 0x040000B4 RID: 180
			public bool showBox;

			// Token: 0x040000B5 RID: 181
			public bool showName;

			// Token: 0x040000B6 RID: 182
			public bool showHp;

			// Token: 0x040000B7 RID: 183
			public bool showWeapon;

			// Token: 0x040000B8 RID: 184
			public bool showC4;

			// Token: 0x040000B9 RID: 185
			public bool showAngle;

			// Token: 0x040000BA RID: 186
			public bool showAimRange;

			// Token: 0x040000BB RID: 187
			public bool showRadar;

			// Token: 0x040000BC RID: 188
			public bool showScreenCollimation;

			// Token: 0x040000BD RID: 189
			public float nameEspRGB;

			// Token: 0x040000BE RID: 190
			public float weaponEspRGB;

			// Token: 0x040000BF RID: 191
			public float c4EspRGB;

			// Token: 0x040000C0 RID: 192
			public float rangeEspRGB;

			// Token: 0x040000C1 RID: 193
			public float screenCollimationEspRGB;

			// Token: 0x040000C2 RID: 194
			public float glowEspRGB;
		}

		// Token: 0x02000028 RID: 40
		public struct RageBot
		{
			// Token: 0x040000C3 RID: 195
			public float Accurary;

			// Token: 0x040000C4 RID: 196
			public bool Multiplehitbox;

			// Token: 0x040000C5 RID: 197
			public bool Enable;

			// Token: 0x040000C6 RID: 198
			public bool Resolver;

			// Token: 0x040000C7 RID: 199
			public bool ManualTiggerbot;

			// Token: 0x040000C8 RID: 200
			public List<bool> aimPos;

			// Token: 0x040000C9 RID: 201
			public int a_resolovertype;
		}

		// Token: 0x02000029 RID: 41
		public struct aimBot
		{
			// Token: 0x040000CA RID: 202
			public float LegitSpeed;

			// Token: 0x040000CB RID: 203
			public string aimbotKey;

			// Token: 0x040000CC RID: 204
			public bool Enable;

			// Token: 0x040000CD RID: 205
			public bool InvincibleNoAim;

			// Token: 0x040000CE RID: 206
			public bool PassWallAim;

			// Token: 0x040000CF RID: 207
			public bool LegitAimbot;

			// Token: 0x040000D0 RID: 208
			public int aimKey;

			// Token: 0x040000D1 RID: 209
			public int aimRange;

			// Token: 0x040000D2 RID: 210
			public int AimPosMode;
		}

		// Token: 0x0200002A RID: 42
		public struct AntiAim
		{
			// Token: 0x040000D3 RID: 211
			public bool Enable;

			// Token: 0x040000D4 RID: 212
			public int fakeYaw;

			// Token: 0x040000D5 RID: 213
			public int JitterMin;

			// Token: 0x040000D6 RID: 214
			public int JitterMax;

			// Token: 0x040000D7 RID: 215
			public int AntiAimMode;

			// Token: 0x040000D8 RID: 216
			public int RotateSpeed;

			// Token: 0x040000D9 RID: 217
			public int AntiAimYawMode;

			// Token: 0x040000DA RID: 218
			public int AntiAimPitchMode;
		}

		// Token: 0x0200002B RID: 43
		public struct Misc
		{
			// Token: 0x040000DB RID: 219
			public bool fakeFov;

			// Token: 0x040000DC RID: 220
			public bool fakeLag;

			// Token: 0x040000DD RID: 221
			public bool bhop;

			// Token: 0x040000DE RID: 222
			public bool noRecoil;

			// Token: 0x040000DF RID: 223
			public bool InstanceSniper;

			// Token: 0x040000E0 RID: 224
			public bool tiggerBot;

			// Token: 0x040000E1 RID: 225
			public bool thirdPerson;

			// Token: 0x040000E2 RID: 226
			public bool shieldbayonet;

			// Token: 0x040000E3 RID: 227
			public string thirdPersonKey;

			// Token: 0x040000E4 RID: 228
			public int fovValue;

			// Token: 0x040000E5 RID: 229
			public int fakeLagTick;

			// Token: 0x040000E6 RID: 230
			public int tiggerBotDelayedTime;

			// Token: 0x040000E7 RID: 231
			public bool leftGun;

			// Token: 0x040000E8 RID: 232
			public int bhoptype;

			// Token: 0x040000E9 RID: 233
			public bool gHost;

			// Token: 0x040000EA RID: 234
			public bool ReplaceWeapon;

			// Token: 0x040000EB RID: 235
			public bool autoChatEnable;

			// Token: 0x040000EC RID: 236
			public int autoChatDelay;

			// Token: 0x040000ED RID: 237
			public string autoChatMessage;
		}
	}
}
