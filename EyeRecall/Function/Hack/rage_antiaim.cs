using System;
using Assets.Sources.Utils.Weapon;
using EyeRecall.Helper;
using EyeRecall.Helper.Player;
using SSJJUserCmd;
using UnityEngine;

namespace EyeRecall.Function.Hack
{
	// Token: 0x0200001C RID: 28
	internal class rage_antiaim
	{
		// Token: 0x0600008B RID: 139 RVA: 0x00007578 File Offset: 0x00005778
		private static void setfakePitch(ref float pitch)
		{
			if (CheatThread._antiAim.Enable)
			{
				if (CheatThread._antiAim.AntiAimMode == 0)
				{
					if (CheatThread._antiAim.AntiAimPitchMode == 0)
					{
						pitch = -271f;
					}
					if (CheatThread._antiAim.AntiAimPitchMode == 2)
					{
						pitch = 271f;
					}
				}
				if (CheatThread._antiAim.AntiAimMode == 1)
				{
					if (CheatThread._antiAim.AntiAimPitchMode == 0)
					{
						pitch = 89f;
					}
					if (CheatThread._antiAim.AntiAimPitchMode == 2)
					{
						pitch = -89f;
					}
				}
				if (CheatThread._antiAim.AntiAimMode == 2)
				{
					if (UnityEngine.Random.Range(1, 3) % 2 != 0)
					{
						if (CheatThread._antiAim.AntiAimPitchMode == 0)
						{
							pitch = -271f;
						}
						if (CheatThread._antiAim.AntiAimPitchMode == 2)
						{
							pitch = 271f;
						}
					}
					else
					{
						if (CheatThread._antiAim.AntiAimPitchMode == 0)
						{
							pitch = 89f;
						}
						if (CheatThread._antiAim.AntiAimPitchMode == 2)
						{
							pitch = -89f;
						}
					}
				}
				if (CheatThread._antiAim.AntiAimMode == 3)
				{
					if (CheatThread._antiAim.AntiAimPitchMode == 0)
					{
						float num = (float)UnityEngine.Random.Range(1, 89);
						pitch = num - 360f;
					}
					if (CheatThread._antiAim.AntiAimPitchMode == 2)
					{
						float num2 = (float)UnityEngine.Random.Range(-89, -1);
						pitch = num2 + 360f;
					}
				}
				if (CheatThread._antiAim.AntiAimPitchMode == 1)
				{
					pitch = 0f;
				}
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000076C4 File Offset: 0x000058C4
		public static void FixMovement(float FixedYaw, float CamYaw, ref float moveforward, ref float moveright)
		{
			float num;
			if (CamYaw >= 0f)
			{
				num = 0f;
			}
			else
			{
				num = 360f;
			}
			float num2 = CamYaw + num;
			float num3;
			if (FixedYaw >= 0f)
			{
				num3 = 0f;
			}
			else
			{
				num3 = 360f;
			}
			float num4 = FixedYaw + num3;
			float num5;
			if (num4 >= num2)
			{
				num5 = 360f - Math.Abs(num4 - num2);
			}
			else
			{
				num5 = Math.Abs(num4 - num2);
			}
			float num6 = num5;
			num6 = 360f - num6;
			float num7 = moveforward;
			float num8 = moveright;
			moveforward = Mathf.Cos(0.01745329f * num6) * num7 + Mathf.Cos(0.01745329f * (num6 + 90f)) * num8;
			moveright = Mathf.Sin(0.01745329f * num6) * num7 + Mathf.Sin(0.01745329f * (num6 + 90f)) * num8;
			moveforward = Mathf.Clamp(moveforward, -100f, 100f);
			moveright = Mathf.Clamp(moveright, -100f, 100f);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000077B4 File Offset: 0x000059B4
		internal static void Execute(ref int pitch, UserCmd userCmd, ref float _pitch, ref float _yaw, ref float _moveforward, ref float _moveright, ref int _buttons, ref bool _silenting)
		{
			try
			{
				PlayerEntity myPlayerEntity = Contexts.sharedInstance.player.myPlayerEntity;
				WeaponEntity currentWeaponEntity = Contexts.sharedInstance.weapon.currentWeaponEntity;
				float num = 0f;
				if (CheatThread._antiAim.AntiAimYawMode == 2 && CheatThread._antiAim.Enable)
				{
					num = (float)UnityEngine.Random.Range(CheatThread._antiAim.JitterMin, CheatThread._antiAim.JitterMax);
				}
				float num2 = (float)userCmd.CameraYaw / 100f;
				float num3 = (CheatThread._antiAim.Enable ? ((180f + num2 - (float)CheatThread._antiAim.fakeYaw + num) % 360f - 180f) : num2);
				float num4 = ((pitch != 0) ? ((float)pitch) : ((float)userCmd.CameraPitch / 100f));
				rage_antiaim.setfakePitch(ref num4);
				if (CheatThread._antiAim.AntiAimYawMode == 1 && CheatThread._antiAim.Enable)
				{
					num3 = (180f + num2 + 180f + (float)(userCmd.Seq * CheatThread._antiAim.RotateSpeed % 360)) % 360f - 180f;
				}
				if (CheatThread._antiAim.AntiAimYawMode == 3 && CheatThread._antiAim.Enable)
				{
					num3 = ((UnityEngine.Random.Range(1, 10) % 2 == 1) ? ((180f + num2 - 90f + num) % 360f - 180f) : ((180f + num2 + 90f + num) % 360f - 180f));
				}
				float num5 = userCmd.MoveForward;
				float num6 = userCmd.MoveRight;
				int num7 = userCmd.Buttons;
				bool flag = false;
				float num8 = Weapon.WeaponSpread(userCmd);
				if (Contexts.sharedInstance != null && Contexts.sharedInstance.weapon != null && Contexts.sharedInstance.weapon.currentWeaponEntity != null)
				{
					bool flag2;
					if (WeaponUtility.CanAttack(Contexts.sharedInstance.weapon.currentWeaponEntity, Contexts.sharedInstance.player.cameraOwnerEntity.GetClientTime() + userCmd.FrameInterval))
					{
						rage_antiaim._weaponSpread = num8;
						flag2 = rage_antiaim._weaponSpread >= CheatThread._rageBot.Accurary / 100f;
					}
					else
					{
						flag2 = false;
					}
					flag = flag2;
				}
				bool flag3 = false;
				if (flag && rage_silentbot.Execute(updateData.own, ref _yaw, ref _pitch, num7))
				{
					if (!userCmd.IsAttackOn)
					{
						userCmd.Buttons |= 64;
						num7 |= 64;
						EyeRecall.Helper.config.first_attack = !EyeRecall.Helper.config.first_attack;
					}
					num3 = _yaw;
					num4 = _pitch;
					flag3 = true;
				}
				rage_antiaim.FixMovement(num3, num2, ref num5, ref num6);
				bool flag4 = !flag3 && flag && (userCmd.IsAttackOn || userCmd.IsSecondaryAttackOn);
				if (updateData.own.currentWeapon.Weapon == 4)
				{
					flag4 = true;
				}
				if (flag4)
				{
					num3 = num2;
					num4 = (float)userCmd.CameraPitch / 100f;
					num5 = userCmd.MoveForward;
					num6 = userCmd.MoveRight;
				}
				rage_antiaim.shareYaw = num3;
				rage_antiaim.sharePitch = num4;
				_pitch = num4;
				_yaw = num3;
				_buttons = num7;
				_moveforward = num5;
				_moveright = num6;
				_silenting = flag3;
				misc_fakeLag.is_silentbot = flag3;
			}
			catch
			{
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00007ACC File Offset: 0x00005CCC
		public rage_antiaim()
		{
		}

		// Token: 0x04000079 RID: 121
		public static int fakePitch;

		// Token: 0x0400007A RID: 122
		public static float shareYaw;

		// Token: 0x0400007B RID: 123
		public static float sharePitch;

		// Token: 0x0400007C RID: 124
		private static float _weaponSpread;
	}
}
