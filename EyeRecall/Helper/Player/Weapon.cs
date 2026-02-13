using System;
using Assets.Sources.Components.Interface.Info.Weapon;
using Assets.Sources.Utils.Player;
using data;
using physics;
using SSJJUserCmd;
using UnityEngine;

namespace EyeRecall.Helper.Player
{
	// Token: 0x02000012 RID: 18
	internal class Weapon
	{
		// Token: 0x0600005E RID: 94 RVA: 0x00004654 File Offset: 0x00002854
		public static float WeaponSpread(UserCmd userCmd_0)
		{
			bool flag = Contexts.sharedInstance.weapon == null || Contexts.sharedInstance.battleRoom == null || Contexts.sharedInstance.weapon.currentWeaponEntity == null || Contexts.sharedInstance.player == null;
			float num;
			if (flag)
			{
				num = 0f;
			}
			else
			{
				IEntitsWeaponInfo info = Contexts.sharedInstance.weapon.currentWeaponEntity.basicInfo.Info;
				IPyEngine pyEngine = Contexts.sharedInstance.battleRoom.pyEngine.PyEngine;
				PlayerEntity myPlayerEntity = Contexts.sharedInstance.player.myPlayerEntity;
				WeaponEntity currentWeaponEntity = Contexts.sharedInstance.weapon.currentWeaponEntity;
				SceneMoveData sceneMoveData = pyEngine.GetWorld().GetSceneMoveData() as SceneMoveData;
				bool flag2 = sceneMoveData != null && sceneMoveData.isWeightlessness;
				bool flag3 = flag2;
				bool flag4 = !userCmd_0.PredicatedOnce && info.AccuracyLogic != null && info.SpreadLogic != null;
				if (flag4)
				{
					info.SpreadLogic.BeforeFire(out currentWeaponEntity.spread.Spread, myPlayerEntity, currentWeaponEntity, userCmd_0, flag3);
					info.AccuracyLogic.BeforeFire(userCmd_0.Seq, myPlayerEntity, currentWeaponEntity, myPlayerEntity.clientTime.ClientTime);
				}
				float num2 = 0f;
				int weaponType = info.WeaponType;
				float num3;
				switch (weaponType)
				{
				case 0:
					num3 = currentWeaponEntity.accuracy.Accuracy * 100f / 92f;
					goto IL_029E;
				case 1:
				case 6:
					goto IL_0253;
				case 2:
				case 3:
				case 4:
					break;
				case 5:
				{
					if (myPlayerEntity.fov.IsZoom())
					{
						num3 = 1f;
					}
					else
					{
						num3 = 0f;
					}
					float num4 = PlayerUtility.PlayerLength2D(myPlayerEntity);
					num2 = 0f;
					if (num4 > 350f)
					{
						num2 = 0.4f;
						goto IL_029E;
					}
					if (num4 > 25f)
					{
						num2 = 0.7f;
						goto IL_029E;
					}
					goto IL_029E;
				}
				default:
					switch (weaponType)
					{
					case 10:
					case 12:
					{
						float num5 = 1f;
						float num6 = (currentWeaponEntity.accuracy.Accuracy - info.AccuracyOffset) * 100f;
						float num7 = info.MaxInaccuracy - info.AccuracyOffset;
						num3 = num5 - num6 / (num7 * 100f);
						num2 = currentWeaponEntity.spread.Spread;
						goto IL_029E;
					}
					case 14:
						goto IL_0253;
					}
					break;
				}
				num3 = 0f;
				num2 = currentWeaponEntity.spread.Spread;
				goto IL_029E;
				IL_0253:
				float num8 = 1f;
				float num9 = (currentWeaponEntity.accuracy.Accuracy - info.DefaultAccuracy) * 100f;
				float num10 = info.MaxInaccuracy - info.DefaultAccuracy;
				num3 = num8 - num9 / (num10 * 100f);
				num2 = currentWeaponEntity.spread.Spread;
				IL_029E:
				num = Mathf.Clamp(num3 - num2, 0f, 1f);
			}
			return num;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00004915 File Offset: 0x00002B15
		public Weapon()
		{
		}
	}
}
