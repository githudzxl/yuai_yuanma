using System;
using EyeRecall.Helper;
using SSJJUserCmd;
using UnityEngine;

namespace EyeRecall.Function.Hack
{
	// Token: 0x02000017 RID: 23
	internal class misc_bhop
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00006C14 File Offset: 0x00004E14
		public static bool Judge_forward(double camera, double a)
		{
			int num = (int)(camera + 90.0);
			int num2 = (int)(camera - 90.0);
			bool flag;
			if (camera > 270.0)
			{
				flag = a > (double)num2 || a < (double)(num - 360);
			}
			else if (camera < 90.0)
			{
				flag = a < (double)num || a > (double)(num2 + 360);
			}
			else
			{
				flag = a > (double)num2 && a < (double)num;
			}
			return flag;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00006C90 File Offset: 0x00004E90
		public static void StateMove()
		{
			if (EyeRecall.Helper.config.own == null)
			{
				return;
			}
			double num = (double)EyeRecall.Helper.config.own.move.Velocity.x;
			double num2 = (double)EyeRecall.Helper.config.own.move.Velocity.y;
			double num3 = Math.Atan(num2 / num);
			if (num < 0.0 && num2 < 0.0)
			{
				num3 -= 3.141592653589793;
			}
			if (num < 0.0 && num2 > 0.0)
			{
				num3 -= 3.141592653589793;
			}
			double num4 = num3 / 0.017453292519943295;
			if (num4 < -180.0)
			{
				num4 += 360.0;
			}
			num4 = Math.Round(num4) + 180.0;
			EyeRecall.Helper.config.is_forward = misc_bhop.Judge_forward(Math.Round((double)EyeRecall.Helper.config.own.GetViewYaw()) + 180.0, num4);
			EyeRecall.Helper.config.speed = Math.Sqrt(num * num + num2 * num2);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00006D90 File Offset: 0x00004F90
		internal static void Execute(PlayerEntity playerEntity, ref UserCmd userCmd)
		{
			try
			{
				if (CheatThread._misc.bhop && !playerEntity.OnGround())
				{
					if (userCmd.IsJump)
					{
						userCmd.Buttons &= -33;
					}
					if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
					{
						float axisX = userCmd.AxisX;
						if (Math.Abs(axisX) > 0.1f)
						{
							if (axisX > 0f)
							{
								userCmd.MoveRight = (	EyeRecall.Helper.config.is_forward ? 127f : (-127f));
							}
							else
							{
								userCmd.MoveRight = (EyeRecall.Helper.config.is_forward ? (-127f) : 127f);
							}
						}
						else
						{
							userCmd.MoveRight = 0f;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogError("[misc_bhop.Execute] An error occurred: " + ex.Message);
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00006E70 File Offset: 0x00005070
		public misc_bhop()
		{
		}
	}
}
