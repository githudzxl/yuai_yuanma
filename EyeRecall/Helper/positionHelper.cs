using System;
using Assets.Sources.Utils.Weapon;
using EyeRecall.Helper.Player;
using share;
using UnityEngine;

namespace EyeRecall.Helper
{
	// Token: 0x0200000E RID: 14
	internal class positionHelper
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00003F24 File Offset: 0x00002124
		public static Vector3 GetPosition(PlayerEntity Player)
		{
			return new Vector3((float)Player.GetX(), (float)Player.GetY(), (float)Player.GetZ());
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003F40 File Offset: 0x00002140
		public static Vector3 EntityPos2World(Vector3 position)
		{
			return new Vector3(0f - position.y, position.z, position.x);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003F5F File Offset: 0x0000215F
		public static Vector3 World2EntityPos(Vector3 position)
		{
			return new Vector3(position.z, -position.x, position.y);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003F7C File Offset: 0x0000217C
		public static bool WallcanAttack(PlayerEntity Player)
		{
			Vector3 normalized = (positionHelper.GetPosition(Player) - positionHelper.GetPosition(updateData.own)).normalized;
			return FireUtility.BulletTrace(Contexts.sharedInstance.battleRoom.pyEngine.PyEngine, updateData.own, Contexts.sharedInstance.player, 100000f, new Vector3D((double)normalized.x, (double)normalized.y, (double)normalized.z), new float[3], new float[3], false).EntityId == Player.GetId();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00004008 File Offset: 0x00002208
		public positionHelper()
		{
		}
	}
}
