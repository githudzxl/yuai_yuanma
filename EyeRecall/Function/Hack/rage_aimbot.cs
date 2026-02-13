using System;
using Assets.Sources.Components.UserComand;
using EyeRecall.Helper;
using EyeRecall.Helper.Player;
using UnityEngine;

namespace EyeRecall.Function.Hack
{
	// Token: 0x0200001B RID: 27
	internal class rage_aimbot
	{
		// Token: 0x06000086 RID: 134 RVA: 0x00007128 File Offset: 0x00005328
		internal static Vector3 GetAimPos(PlayerEntity target, bool ragebot)
		{
			Vector3 vector = Vector3.zero;
			if (CheatThread._aimBot.AimPosMode == 0)
			{
				vector = target.GetHitBox("Bip01_Head").position + new Vector3(0f, 10f, 0f);
			}
			if (CheatThread._aimBot.AimPosMode == 1)
			{
				vector = target.GetHitBox("Bip01_Neck").position;
			}
			if (CheatThread._aimBot.AimPosMode == 2)
			{
				vector = target.GetHitBox("Bip01_Spine1").position;
			}
			if (CheatThread._aimBot.AimPosMode == 3)
			{
				vector = target.GetHitBox("Bip01_L_Foot").position;
			}
			if (CheatThread._aimBot.AimPosMode == 4)
			{
				vector = target.GetHitBox("Bip01_R_Foot").position;
			}
			if (vector == Vector3.zero)
			{
				vector = target.GetHitBox("Bip01_Head").position;
			}
			return vector;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00007208 File Offset: 0x00005408
		private static void ViolenceAimBot(Vector3 pos)
		{
			Vector3 eulerAngles = Quaternion.FromToRotation((pos - updateData.mainCamera.transform.position).normalized, new Vector3(0f, 0f, 1f)).eulerAngles;
			if (eulerAngles.x > 180f)
			{
				eulerAngles.x -= 360f;
			}
			if (CheatThread._misc.noRecoil)
			{
				eulerAngles.x -= updateData.own.GetPunchPitch() * 2f;
				eulerAngles.y -= updateData.own.GetPunchYaw() * 2f;
			}
			InputComponent input = Contexts.sharedInstance.userCommand.commandsEntity.input;
			input.Pitch = eulerAngles.x;
			input.Yaw = eulerAngles.y;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000072E0 File Offset: 0x000054E0
		private static void LegitAimBot(Vector3 screenPosition)
		{
			Vector3 eulerAngles = Quaternion.FromToRotation(Contexts.sharedInstance.worldCamera.unityObjects.mainCamera.transform.forward, screenPosition - Contexts.sharedInstance.worldCamera.unityObjects.mainCamera.transform.position).eulerAngles;
			if (Contexts.sharedInstance.worldCamera.unityObjects.mainCamera.transform.forward.x > 0f)
			{
				eulerAngles.z = 360f - eulerAngles.z;
			}
			float num = ((eulerAngles.y > 180f) ? (eulerAngles.y - 360f) : eulerAngles.y);
			float num2 = ((eulerAngles.z > 180f) ? (eulerAngles.z - 360f) : eulerAngles.z);
			InputComponent input = Contexts.sharedInstance.userCommand.commandsEntity.input;
			input.Yaw -= num * CheatThread._aimBot.LegitSpeed / 1000f;
			input.Pitch -= num2 * CheatThread._aimBot.LegitSpeed / 1000f;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00007410 File Offset: 0x00005610
		internal static void Execute()
		{
			try
			{
				if (Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), CheatThread._aimBot.aimbotKey)))
				{
					WeaponEntity currentWeaponEntity = Contexts.sharedInstance.weapon.currentWeaponEntity;
					if (currentWeaponEntity != null && currentWeaponEntity.slot.Slot > 3)
					{
						rage_aimbot.Locking = false;
					}
					else
					{
						Vector3 vector = positionHelper.EntityPos2World(positionHelper.GetPosition(updateData.aimTarget));
						Vector3 vector2 = Camera.main.WorldToScreenPoint(vector);
						if (CheatThread._aimBot.PassWallAim && !positionHelper.WallcanAttack(updateData.aimTarget))
						{
							updateData.aimTarget = null;
							rage_aimbot.Locking = false;
						}
						else if (updateData.lastDistance > CheatThread._aimBot.aimRange)
						{
							rage_aimbot.Locking = false;
						}
						else if (vector2.z < 0f)
						{
							rage_aimbot.Locking = false;
						}
						else if (CheatThread._aimBot.InvincibleNoAim && PlayerHelper.GetIsUnmatched(updateData.aimTarget))
						{
							updateData.aimTarget = null;
							rage_aimbot.Locking = false;
						}
						else
						{
							rage_aimbot.Locking = true;
							Vector3 aimPos = rage_aimbot.GetAimPos(updateData.aimTarget, false);
							if (!CheatThread._aimBot.LegitAimbot)
							{
								rage_aimbot.ViolenceAimBot(aimPos);
							}
							else if (CheatThread._aimBot.LegitAimbot)
							{
								rage_aimbot.LegitAimBot(aimPos);
							}
						}
					}
				}
				else
				{
					rage_aimbot.Locking = false;
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00007570 File Offset: 0x00005770
		public rage_aimbot()
		{
		}

		// Token: 0x04000078 RID: 120
		public static bool Locking;
	}
}
