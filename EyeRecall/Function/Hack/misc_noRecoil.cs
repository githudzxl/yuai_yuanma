using System;
using Assets.Sources.Components.UserComand;
using Assets.Sources.Free.Data;
using EyeRecall.Helper.Player;

namespace EyeRecall.Function.Hack
{
	// Token: 0x02000019 RID: 25
	internal class misc_noRecoil
	{
		// Token: 0x06000080 RID: 128 RVA: 0x00006E8C File Offset: 0x0000508C
		internal static void Execute()
		{
			try
			{
				if (CheatThread._misc.noRecoil)
				{
					float punchPitch = updateData.own.GetPunchPitch();
					float punchYaw = updateData.own.GetPunchYaw();
					InputComponent input = Contexts.sharedInstance.userCommand.commandsEntity.input;
					input.Pitch -= 2f * (punchPitch - misc_noRecoil.oldPunchPitch);
					input.Yaw -= 2f * (punchYaw - misc_noRecoil.oldPunchYaw);
					Contexts.sharedInstance.worldCamera.unityObjects.mainCamera.transform.Rotate(0f - misc_noRecoil.oldPunchPitch - GameModelLocator.GetInstance().GameModel.ShakeAngleOffect.y, 0f - misc_noRecoil.oldPunchYaw - GameModelLocator.GetInstance().GameModel.ShakeAngleOffect.x, 0f);
					misc_noRecoil.oldPunchPitch = punchPitch;
					misc_noRecoil.oldPunchYaw = punchYaw;
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00006F8C File Offset: 0x0000518C
		public misc_noRecoil()
		{
		}

		// Token: 0x04000074 RID: 116
		private static float oldPunchPitch;

		// Token: 0x04000075 RID: 117
		private static float oldPunchYaw;
	}
}
