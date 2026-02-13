using System;
using SSJJBase.String;
using UnityEngine;

namespace EyeRecall.Helper
{
	// Token: 0x0200000D RID: 13
	internal static class PlayerHelper
	{
		// Token: 0x0600004A RID: 74 RVA: 0x00003EB4 File Offset: 0x000020B4
		internal static Transform GetHitBox(this PlayerEntity target, string BoneName)
		{
			int hashCode = new IgnoreCaseString(BoneName).GetHashCode();
			Transform transform;
			if (target.hitBox.BonetTransform.TryGetValue(hashCode, out transform))
			{
				return transform;
			}
			return null;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003EEE File Offset: 0x000020EE
		internal static bool GetIsUnmatched(PlayerEntity Player)
		{
			return !(Player.basicInfo.CareerInfo.Name == "bossjy6001") && (Player.HasState(1) || Player.GetPlayerInfo().InFrantic);
		}
	}
}
