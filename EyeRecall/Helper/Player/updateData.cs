using System;
using System.Collections.Generic;
using Entitas;
using EyeRecall.Function.Hack;
using UnityEngine;

namespace EyeRecall.Helper.Player
{
	// Token: 0x02000011 RID: 17
	internal class updateData
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00004474 File Offset: 0x00002674
		private static void updateEntity()
		{
			updateData.EntityList = Contexts.sharedInstance.player.GetEntities();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000448A File Offset: 0x0000268A
		private static void updateCamera()
		{
			updateData.mainCamera = Contexts.sharedInstance.worldCamera.unityObjects.mainCamera;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000044A8 File Offset: 0x000026A8
		private static void updatePlayers()
		{
			try
			{
				if (rage_aimbot.Locking && updateData.aimTarget != null)
				{
					if (!updateData.aimTarget.IsDead())
					{
						return;
					}
					updateData.aimTarget = null;
					updateData.lastDistance = 10000;
				}
				updateData.lastDistance = 10000;
				foreach (IEntity entity in updateData.EntityList)
				{
					PlayerEntity playerEntity = (PlayerEntity)entity;
					if (playerEntity.IsMySelf())
					{
						updateData.own = playerEntity;
					}
					if (playerEntity.GetTeam() != updateData.own.GetTeam() && !playerEntity.IsDead())
					{
						Vector3 vector = updateData.mainCamera.WorldToScreenPoint(positionHelper.EntityPos2World(positionHelper.GetPosition(playerEntity)) + new Vector3(0f, 150f, 0f));
						int num = (int)(Math.Abs(vector.x - (float)Screen.width / 2f) + Math.Abs(vector.y - (float)Screen.height / 2f));
						if (num < updateData.lastDistance)
						{
							updateData.aimTarget = playerEntity;
							updateData.lastDistance = num;
						}
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004600 File Offset: 0x00002800
		public static void Update()
		{
			try
			{
				updateData.updateCamera();
				updateData.updateEntity();
				updateData.updatePlayers();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004634 File Offset: 0x00002834
		public updateData()
		{
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000463C File Offset: 0x0000283C
		// Note: this type is marked as 'beforefieldinit'.
		static updateData()
		{
		}

		// Token: 0x04000047 RID: 71
		public static List<IEntity> EntityList;

		// Token: 0x04000048 RID: 72
		public static int lastDistance = 10000;

		// Token: 0x04000049 RID: 73
		public static PlayerEntity aimTarget;

		// Token: 0x0400004A RID: 74
		public static Camera mainCamera;

		// Token: 0x0400004B RID: 75
		public static PlayerEntity own;

		// Token: 0x0400004C RID: 76
		public static Vector3 startVector3;

		// Token: 0x0400004D RID: 77
		internal static List<PlayerEntity> tempList = new List<PlayerEntity>();
	}
}
