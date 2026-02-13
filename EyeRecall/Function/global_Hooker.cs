using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Assets.Sources.Components.Camera;
using Assets.Sources.Components.Interface.Info.Camera;
using Assets.Sources.Components.Interface.Info.Weapon;
using Assets.Sources.Components.Player;
using Assets.Sources.Components.Snapshot;
using Assets.Sources.Components.UserComand;
using Assets.Sources.Info.Camera.CameraLogic;
using Assets.Sources.Modules.Player.Orientation;
using Assets.Sources.Modules.Ui.UiEventCondition;
using Assets.Sources.Systems.UserCommand;
using Assets.Sources.Utils;
using Assets.Sources.Utils.Weapon;
using EyeRecall.Function.Hack;
using EyeRecall.Helper;
using EyeRecall.Helper.Player;
using NetData;
using physics;
using physics.entity;
using physics.trace;
using share;
using SSJJBase.Utility;
using SSJJMath;
using SSJJPhysics;
using SSJJUserCmd;
using UnityEngine;
using weapon;
using Debug = UnityEngine.Debug;
using Trace = physics.trace.Trace;

namespace EyeRecall.Function
{
	// Token: 0x02000014 RID: 20
	[Obfuscation(Exclude = true)]
	internal class global_Hooker
	{
		// Token: 0x06000072 RID: 114 RVA: 0x000062EC File Offset: 0x000044EC
		internal static void global_Hooker_init()
		{
			try
			{
				new MethodHook(typeof(AbstractCaptureSnapshot).GetMethod("ScreenNow", global_Hooker.bindingFlags), typeof(global_Hooker.hk_AbstractCaptureSnapshot).GetMethod("hk_ScreenNow", global_Hooker.bindingFlags), typeof(global_Hooker.hk_AbstractCaptureSnapshot).GetMethod("origin_ScreenNow", global_Hooker.bindingFlags)).Install();
				new MethodHook(typeof(ExclusiveCaptureSnapshot).GetMethod("UpdateScreen", global_Hooker.bindingFlags), typeof(global_Hooker.hk_ExclusiveCaptureSnapshot).GetMethod("hk_UpdateScreen", global_Hooker.bindingFlags), typeof(global_Hooker.hk_ExclusiveCaptureSnapshot).GetMethod("origin_UpdateScreen", global_Hooker.bindingFlags)).Install();
				new MethodHook(typeof(WindowCaptureSnapshot).GetMethod("UpdateScreen", global_Hooker.bindingFlags), typeof(global_Hooker.hk_WindowCaptureSnapshot).GetMethod("hk_UpdateScreen", global_Hooker.bindingFlags), typeof(global_Hooker.hk_WindowCaptureSnapshot).GetMethod("origin_UpdateScreen", global_Hooker.bindingFlags)).Install();
				new MethodHook(typeof(WindowHdcCaptureSnapshot).GetMethod("UpdateScreen", global_Hooker.bindingFlags), typeof(global_Hooker.hk_WindowHdcCaptureSnapshot).GetMethod("hk_UpdateScreen", global_Hooker.bindingFlags), typeof(global_Hooker.hk_WindowHdcCaptureSnapshot).GetMethod("origin_UpdateScreen", global_Hooker.bindingFlags)).Install();
				new MethodHook(typeof(CaptureCameraManager).GetMethod("CaptureCamera", global_Hooker.bindingFlags), typeof(global_Hooker.hk_CaptureCameraManager).GetMethod("hk_CaptureCamera", global_Hooker.bindingFlags), typeof(global_Hooker.hk_CaptureCameraManager).GetMethod("origin_CaptureCamera", global_Hooker.bindingFlags)).Install();
				new MethodHook(typeof(PostProcessUserCommandSystem).GetMethod("InterceptNew", global_Hooker.bindingFlags), typeof(global_Hooker.hk_PostProcessUserCommandSystem).GetMethod("hk_InterceptNew", global_Hooker.bindingFlags), typeof(global_Hooker.hk_PostProcessUserCommandSystem).GetMethod("origin_InterceptNew", global_Hooker.bindingFlags)).Install();
				new MethodHook(typeof(NetEaseCloudManager).GetMethod("Send", global_Hooker.bindingFlags), typeof(global_Hooker.hk_NetEaseCloudManager).GetMethod("hk_Send", global_Hooker.bindingFlags), typeof(global_Hooker.hk_NetEaseCloudManager).GetMethod("origin_Send", global_Hooker.bindingFlags)).Install();
				new MethodHook(typeof(SendUserCommandSystem).GetMethod("GetUserCmdBytes", global_Hooker.bindingFlags), typeof(global_Hooker.hk_SendUserCommandSystem).GetMethod("hk_GetUserCmdBytes", global_Hooker.bindingFlags), typeof(global_Hooker.hk_SendUserCommandSystem).GetMethod("origin_GetUserCmdBytes", global_Hooker.bindingFlags)).Install();
				new MethodHook(typeof(TpsCameraLogic).GetMethod("IsActive", global_Hooker.bindingFlags), typeof(global_Hooker.hk_TpsCameraLogic).GetMethod("hk_IsActive", global_Hooker.bindingFlags), typeof(global_Hooker.hk_TpsCameraLogic).GetMethod("origin_IsActive", global_Hooker.bindingFlags)).Install();
				new MethodHook(typeof(TpsCameraLogic).GetMethod("Update", global_Hooker.bindingFlags), typeof(global_Hooker.hk_TpsCameraLogic).GetMethod("hk_Update", global_Hooker.bindingFlags), typeof(global_Hooker.hk_TpsCameraLogic).GetMethod("origin_Update", global_Hooker.bindingFlags)).Install();
				new MethodHook(typeof(CameraFunction).GetMethod("GetCurrentCmdYaw", global_Hooker.bindingFlags), typeof(global_Hooker.hk_CameraFunction).GetMethod("hk_GetCurrentCmdYaw", global_Hooker.bindingFlags), typeof(global_Hooker.hk_CameraFunction).GetMethod("origin_GetCurrentCmdYaw", global_Hooker.bindingFlags)).Install();
				new MethodHook(typeof(CameraFunction).GetMethod("GetCurrentCmdPitch", global_Hooker.bindingFlags), typeof(global_Hooker.hk_CameraFunction).GetMethod("hk_GetCurrentCmdPitch", global_Hooker.bindingFlags), typeof(global_Hooker.hk_CameraFunction).GetMethod("origin_GetCurrentCmdPitch", global_Hooker.bindingFlags)).Install();
				new MethodHook(typeof(UiIEventCondition).GetMethod("Get_ControlEntityData_Yaw", global_Hooker.bindingFlags), typeof(global_Hooker.hk_UiIEventCondition).GetMethod("hk_Get_ControlEntityData_Yaw", global_Hooker.bindingFlags), typeof(global_Hooker.hk_UiIEventCondition).GetMethod("origin_Get_ControlEntityData_Yaw", global_Hooker.bindingFlags)).Install();
				new MethodHook(typeof(UiIEventCondition).GetMethod("Get_cameraOwnerData_Yaw", global_Hooker.bindingFlags), typeof(global_Hooker.hk_UiIEventCondition).GetMethod("hk_Get_cameraOwnerData_Yaw", global_Hooker.bindingFlags), typeof(global_Hooker.hk_UiIEventCondition).GetMethod("origin_Get_cameraOwnerData_Yaw", global_Hooker.bindingFlags)).Install();
				new MethodHook(typeof(PlayerOrientationPredicationSystem).GetMethod("OnPredicate", global_Hooker.bindingFlags), typeof(global_Hooker.hk_PlayerOrientationPredicationSystem).GetMethod("hk_OnPredicate", global_Hooker.bindingFlags), typeof(global_Hooker.hk_PlayerOrientationPredicationSystem).GetMethod("origin_OnPredicate", global_Hooker.bindingFlags)).Install();
				new MethodHook(typeof(PlayerOrientationPlabackSystem).GetMethod("OnPlayback", global_Hooker.bindingFlags), typeof(global_Hooker.hk_PlayerOrientationPlabackSystem).GetMethod("hk_OnPlayback", global_Hooker.bindingFlags), typeof(global_Hooker.hk_PlayerOrientationPlabackSystem).GetMethod("origin_OnPlayback", global_Hooker.bindingFlags)).Install();
				new MethodHook(typeof(PlayerOrientationPredicationSystem).GetMethod("PredictCmdOnCamera", global_Hooker.bindingFlags), typeof(global_Hooker.hk_PlayerOrientationPredicationSystem).GetMethod("hk_PredictCmdOnCamera", global_Hooker.bindingFlags), typeof(global_Hooker.hk_PlayerOrientationPredicationSystem).GetMethod("origin_PredictCmdOnCamera", global_Hooker.bindingFlags)).Install();
				new MethodHook(typeof(CommandsComponent).GetMethod("LastCameraYaw", global_Hooker.bindingFlags), typeof(global_Hooker.hk_CommandsComponent).GetMethod("hk_LastCameraYaw", global_Hooker.bindingFlags), typeof(global_Hooker.hk_CommandsComponent).GetMethod("origin_LastCameraYaw", global_Hooker.bindingFlags)).Install();
				new MethodHook(typeof(CommandsComponent).GetMethod("LastCameraPitch", global_Hooker.bindingFlags), typeof(global_Hooker.hk_CommandsComponent).GetMethod("hk_LastCameraPitch", global_Hooker.bindingFlags), typeof(global_Hooker.hk_CommandsComponent).GetMethod("origin_LastCameraPitch", global_Hooker.bindingFlags)).Install();
				new MethodHook(typeof(PlayerEntity).GetMethod("get_fov", global_Hooker.bindingFlags), typeof(global_Hooker.hk_PlayerEntity).GetMethod("hk_get_fov", global_Hooker.bindingFlags), typeof(global_Hooker.hk_PlayerEntity).GetMethod("origin_get_fov", global_Hooker.bindingFlags)).Install();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000069C0 File Offset: 0x00004BC0
		public global_Hooker()
		{
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000069C8 File Offset: 0x00004BC8
		// Note: this type is marked as 'beforefieldinit'.
		static global_Hooker()
		{
		}

		// Token: 0x0400006E RID: 110
		public static BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.GetProperty;

		// Token: 0x0200002E RID: 46
		[Obfuscation(Exclude = true)]
		public class hk_AbstractCaptureSnapshot
		{
			// Token: 0x060000AD RID: 173 RVA: 0x000090AD File Offset: 0x000072AD
			public static void hk_ScreenNow(AbstractCaptureSnapshot self)
			{
			}

			// Token: 0x060000AE RID: 174 RVA: 0x000090AF File Offset: 0x000072AF
			public static void origin_ScreenNow(AbstractCaptureSnapshot self)
			{
				Debug.Log("proxy");
			}

			// Token: 0x060000AF RID: 175 RVA: 0x000090BB File Offset: 0x000072BB
			public hk_AbstractCaptureSnapshot()
			{
			}
		}

		// Token: 0x0200002F RID: 47
		[Obfuscation(Exclude = true)]
		public class hk_ExclusiveCaptureSnapshot
		{
			// Token: 0x060000B0 RID: 176 RVA: 0x000090C3 File Offset: 0x000072C3
			public static void hk_UpdateScreen(ExclusiveCaptureSnapshot self)
			{
			}

			// Token: 0x060000B1 RID: 177 RVA: 0x000090C5 File Offset: 0x000072C5
			public static void origin_UpdateScreen(ExclusiveCaptureSnapshot self)
			{
				Debug.Log("proxy");
			}

			// Token: 0x060000B2 RID: 178 RVA: 0x000090D1 File Offset: 0x000072D1
			public hk_ExclusiveCaptureSnapshot()
			{
			}
		}

		// Token: 0x02000030 RID: 48
		[Obfuscation(Exclude = true)]
		public class hk_WindowCaptureSnapshot
		{
			// Token: 0x060000B3 RID: 179 RVA: 0x000090D9 File Offset: 0x000072D9
			public static void hk_UpdateScreen(WindowCaptureSnapshot slef)
			{
			}

			// Token: 0x060000B4 RID: 180 RVA: 0x000090DB File Offset: 0x000072DB
			public static void origin_UpdateScreen(WindowCaptureSnapshot self)
			{
				Debug.Log("proxy");
			}

			// Token: 0x060000B5 RID: 181 RVA: 0x000090E7 File Offset: 0x000072E7
			public hk_WindowCaptureSnapshot()
			{
			}
		}

		// Token: 0x02000031 RID: 49
		[Obfuscation(Exclude = true)]
		public class hk_WindowHdcCaptureSnapshot
		{
			// Token: 0x060000B6 RID: 182 RVA: 0x000090EF File Offset: 0x000072EF
			public static void hk_UpdateScreen(WindowHdcCaptureSnapshot self)
			{
			}

			// Token: 0x060000B7 RID: 183 RVA: 0x000090F1 File Offset: 0x000072F1
			public static void origin_UpdateScreen(WindowHdcCaptureSnapshot self)
			{
				Debug.Log("proxy");
			}

			// Token: 0x060000B8 RID: 184 RVA: 0x000090FD File Offset: 0x000072FD
			public hk_WindowHdcCaptureSnapshot()
			{
			}
		}

		// Token: 0x02000032 RID: 50
		[Obfuscation(Exclude = true)]
		public class hk_CaptureCameraManager
		{
			// Token: 0x060000B9 RID: 185 RVA: 0x00009105 File Offset: 0x00007305
			public static IEnumerator hk_CaptureCamera(CaptureCameraManager self)
			{
				return null;
			}

			// Token: 0x060000BA RID: 186 RVA: 0x00009108 File Offset: 0x00007308
			public static IEnumerator origin_CaptureCamera(CaptureCameraManager self)
			{
				Debug.Log("proxy");
				return null;
			}

			// Token: 0x060000BB RID: 187 RVA: 0x00009115 File Offset: 0x00007315
			public hk_CaptureCameraManager()
			{
			}
		}

		// Token: 0x02000033 RID: 51
		[Obfuscation(Exclude = true)]
		public class hk_PostProcessUserCommandSystem
		{
			// Token: 0x060000BC RID: 188 RVA: 0x00009120 File Offset: 0x00007320
			public static void hk_InterceptNew(PostProcessUserCommandSystem self, UserCmd userCommand)
			{
				if (CheatThread._misc.gHost)
				{
					if ((userCommand.Buttons & 1) > 0)
					{
						userCommand.CleanButtonFlag(1);
					}
					if ((userCommand.Buttons & 2) > 0)
					{
						userCommand.CleanButtonFlag(2);
					}
					if ((userCommand.Buttons & 4) > 0)
					{
						userCommand.CleanButtonFlag(4);
					}
					if ((userCommand.Buttons & 8) > 0)
					{
						userCommand.CleanButtonFlag(8);
					}
				}
			}

			// Token: 0x060000BD RID: 189 RVA: 0x00009181 File Offset: 0x00007381
			public hk_PostProcessUserCommandSystem()
			{
			}
		}

		// Token: 0x02000034 RID: 52
		[Obfuscation(Exclude = true)]
		public class hk_SendUserCommandSystem
		{
			// Token: 0x060000BE RID: 190 RVA: 0x0000918C File Offset: 0x0000738C
			public static byte[] hk_GetUserCmdBytes(SendUserCommandSystem self, LinkedList<UserCmd> sendCmdList, SnapshotsComponent snapshots, out int datalen, bool isTcp)
			{
				datalen = 0;
				if (sendCmdList.Count == 0)
				{
					return null;
				}
				try
				{
					LinkedListNode<UserCmd> linkedListNode = sendCmdList.First;
					UserCmd value = linkedListNode.Value;
					global_Hooker.hk_SendUserCommandSystem.seek = value.RandomSeed;
					float num = 0f;
					float num2 = 0f;
					float num3 = 0f;
					float num4 = 0f;
					int num5 = 0;
					int seq = value.Seq;
					bool flag = false;
					object obj = SendUserCommandSystem.Record == null || SendUserCommandSystem.Record.IsSelfMove();
					misc_bhop.Execute(updateData.own, ref value);
					rage_antiaim.Execute(ref rage_antiaim.fakePitch, value, ref num2, ref num, ref num3, ref num4, ref num5, ref flag);
					global_Hooker.hk_SendUserCommandSystem._binaryDataWriter.Reset();
					if (isTcp)
					{
						global_Hooker.hk_SendUserCommandSystem._binaryDataWriter.WriteByte(31);
					}
					int num6 = snapshots.ReceiveSnapshotLatency;
					if (num6 > 255)
					{
						num6 = 255;
					}
					global_Hooker.hk_SendUserCommandSystem._binaryDataWriter.WriteByte((byte)num6);
					global_Hooker.hk_SendUserCommandSystem._binaryDataWriter.WriteInt(value.Seq);
					global_Hooker.hk_SendUserCommandSystem._binaryDataWriter.WriteInt(value.RenderTime);
					global_Hooker.hk_SendUserCommandSystem._binaryDataWriter.WriteInt(snapshots.LatestSnapshotSeqId);
					int num7 = 191;
					global_Hooker.hk_SendUserCommandSystem._binaryDataWriter.WriteByte((byte)num7);
					global_Hooker.hk_SendUserCommandSystem._binaryDataWriter.WriteByte((byte)value.FrameInterval);
					object obj2 = obj;
					int num8 = ((obj2 == null) ? 0 : ((int)num3));
					int num9 = ((obj2 == null) ? 0 : ((int)num4));
					global_Hooker.hk_SendUserCommandSystem._binaryDataWriter.WriteByte((byte)num8);
					global_Hooker.hk_SendUserCommandSystem._binaryDataWriter.WriteByte((byte)num9);
					global_Hooker.hk_SendUserCommandSystem._binaryDataWriter.WriteInt(value.Buttons);
					int num10 = 0 | value.BagId;
					num10 <<= 4;
					num10 |= value.Weapon;
					global_Hooker.hk_SendUserCommandSystem._binaryDataWriter.WriteByte((byte)num10);
					global_Hooker.hk_SendUserCommandSystem._binaryDataWriter.WriteShort((short)(num * 100f));
					global_Hooker.hk_SendUserCommandSystem._binaryDataWriter.WriteShort((short)(num2 * 100f));
					for (linkedListNode = linkedListNode.Next; linkedListNode != null; linkedListNode = linkedListNode.Next)
					{
						num7 = 0;
						UserCmd value2 = linkedListNode.Value;
						global_Hooker.hk_SendUserCommandSystem.seek = value2.RandomSeed;
						misc_bhop.Execute(updateData.own, ref value2);
						rage_antiaim.Execute(ref rage_antiaim.fakePitch, value2, ref num2, ref num, ref num3, ref num4, ref num5, ref flag);
						int position = global_Hooker.hk_SendUserCommandSystem._binaryDataWriter.Position;
						global_Hooker.hk_SendUserCommandSystem._binaryDataWriter.WriteByte((byte)num7);
						global_Hooker.hk_SendUserCommandSystem._binaryDataWriter.WriteByte((byte)value2.FrameInterval);
						num8 = (int)num3;
						num9 = (int)num4;
						global_Hooker.hk_SendUserCommandSystem._binaryDataWriter.WriteByte((byte)num8);
						global_Hooker.hk_SendUserCommandSystem._binaryDataWriter.WriteByte((byte)num9);
						global_Hooker.hk_SendUserCommandSystem._binaryDataWriter.WriteInt(value2.Buttons);
						num10 = 0 | value2.BagId;
						num10 <<= 4;
						num10 |= value2.Weapon;
						global_Hooker.hk_SendUserCommandSystem._binaryDataWriter.WriteByte((byte)num10);
						num7 |= 31;
						global_Hooker.hk_SendUserCommandSystem._binaryDataWriter.WriteShort((short)(num * 100f));
						num7 |= 32;
						global_Hooker.hk_SendUserCommandSystem._binaryDataWriter.WriteShort((short)(num2 * 100f));
						int position2 = global_Hooker.hk_SendUserCommandSystem._binaryDataWriter.Position;
						global_Hooker.hk_SendUserCommandSystem._binaryDataWriter.Position = position;
						global_Hooker.hk_SendUserCommandSystem._binaryDataWriter.WriteByte((byte)num7);
						global_Hooker.hk_SendUserCommandSystem._binaryDataWriter.Position = position2;
					}
					return global_Hooker.hk_SendUserCommandSystem._binaryDataWriter.GetBytes();
				}
				catch
				{
				}
				return null;
			}

			// Token: 0x060000BF RID: 191 RVA: 0x000094B0 File Offset: 0x000076B0
			public static byte[] origin_GetUserCmdBytes(SendUserCommandSystem self, LinkedList<UserCmd> sendCmdList, SnapshotsComponent snapshots, out int datalen, bool isTcp)
			{
				Debug.Log("Proxy");
				datalen = 0;
				return null;
			}

			// Token: 0x060000C0 RID: 192 RVA: 0x000094C0 File Offset: 0x000076C0
			public hk_SendUserCommandSystem()
			{
			}

			// Token: 0x060000C1 RID: 193 RVA: 0x000094C8 File Offset: 0x000076C8
			// Note: this type is marked as 'beforefieldinit'.
			static hk_SendUserCommandSystem()
			{
			}

			// Token: 0x040000F8 RID: 248
			private static int g_TickShift;

			// Token: 0x040000F9 RID: 249
			public static int seek;

			// Token: 0x040000FA RID: 250
			public static BinaryDataWriter _binaryDataWriter = new BinaryDataWriter();
		}

        // Token: 0x02000035 RID: 53
        [Obfuscation(Exclude = true)]
        public class hk_TpsCameraLogic
        {
            // Token: 0x060000C2 RID: 194 RVA: 0x000094D4 File Offset: 0x000076D4
            // Hook函数：判断是否启用第三人称视角
            public static bool hk_IsActive(TpsCameraLogic self)
            {
                global_Hooker.hk_TpsCameraLogic.origin_IsActive(self);
                CameraDataComponent cameraData = Contexts.sharedInstance.worldCamera.cameraData;
                cameraData.Fov = (CheatThread._misc.fakeFov ? CheatThread._misc.fovValue : 90);
                cameraData.CameraYawAddValue = self.GetField<float>("_yaw");
                cameraData.CameraPitchAddValue = self.GetField<float>("_pitch") - 5f;
                cameraData.TransTime = Mathf.Max(230, cameraData.TransTime + Contexts.sharedInstance.time.time.FrameInterval);
                cameraData.IsTps = global_Hooker.hk_TpsCameraLogic.isTps && CheatThread._misc.thirdPerson;
                return global_Hooker.hk_TpsCameraLogic.isTps && CheatThread._misc.thirdPerson;
            }

            // Token: 0x060000C3 RID: 195 RVA: 0x00009598 File Offset: 0x00007798
            // 原始IsActive函数的代理
            public static bool origin_IsActive(TpsCameraLogic self)
            {
                Debug.Log("proxy");
                return false;
            }

            // Token: 0x060000C4 RID: 196 RVA: 0x000095A8 File Offset: 0x000077A8
            // Hook函数：更新摄像机逻辑
            public static void hk_Update(TpsCameraLogic self)
            {
                try
                {
                    global_Hooker.hk_TpsCameraLogic.origin_Update(self);
                    if (updateData.own != null && !updateData.own.IsDead())
                    {
                        CameraDataComponent cameraData = self.GetField<Contexts>("Contexts").worldCamera.cameraData;
                        PlayerEntity myPlayerEntity = self.GetField<Contexts>("Contexts").player.myPlayerEntity;
                        Vector3 field = self.GetField<Vector3>("_viewOrgPosition");
                        Vector3 vector = default(Vector3);
                        if (cameraData.IsTps)
                        {
                            vector = self.GetCalculateCameraEndPos(field, cameraData.CameraYawAddValue, cameraData.CameraPitchAddValue, self.GetField<float>("_distance"), 10f);
                            Vector3D vector3D = new Vector3D();
                            Vector3D vector3D2 = new Vector3D();
                            Vector3D vector3D3 = new Vector3D();
                            AngleUtility.AnglesToVectors2(self.GetField<float>("_yaw"), self.GetField<float>("_pitch"), vector3D, vector3D2, vector3D3);
                            vector3D.Normalize();
                            vector3D2.Normalize();
                            vector3D3.Normalize();
                            vector3D2.ScaleBy(50f);
                            vector = self.GetCalculateCameraEndPos(vector, cameraData.CameraYawAddValue, 0f, 50f, 10f);
                            bool flag = myPlayerEntity != null && myPlayerEntity.fov.Fov != cameraData.Fov;
                            if (flag)
                            {
                                myPlayerEntity.fov.Fov = cameraData.Fov;
                                myPlayerEntity.fov.DelayFov = cameraData.Fov;
                            }
                        }
                        if (cameraData.TransTime != 0)
                        {
                            self.InterpolateCamareDeadEndPos(field, vector, cameraData.TransTime);
                        }
                    }
                }
                catch
                {
                }
            }

            // Token: 0x060000C5 RID: 197 RVA: 0x00009740 File Offset: 0x00007940
            // 原始Update函数的代理
            public static void origin_Update(TpsCameraLogic self)
            {
                Debug.Log("proxy");
            }

            // Token: 0x060000C6 RID: 198 RVA: 0x0000974C File Offset: 0x0000794C
            public hk_TpsCameraLogic()
            {
            }

            // Token: 0x040000FB RID: 251
            public static bool isTps;
        }



        // Token: 0x02000036 RID: 54
        [Obfuscation(Exclude = true)]
		public class hk_CameraFunction
		{
			// Token: 0x060000C7 RID: 199 RVA: 0x00009754 File Offset: 0x00007954
			public static float hk_GetCurrentCmdYaw(ICameraLogic _icameraLogic)
			{
				if (updateData.own == null || updateData.own.IsDead())
				{
					return global_Hooker.hk_CameraFunction.origin_GetCurrentCmdYaw(_icameraLogic);
				}
				return Contexts.sharedInstance.worldCamera.cameraTransform.Yaw;
			}

			// Token: 0x060000C8 RID: 200 RVA: 0x00009784 File Offset: 0x00007984
			public static float origin_GetCurrentCmdYaw(ICameraLogic _icameraLogic)
			{
				Debug.Log("proxy");
				return 0f;
			}

			// Token: 0x060000C9 RID: 201 RVA: 0x00009795 File Offset: 0x00007995
			public static float hk_GetCurrentCmdPitch(ICameraLogic _icameraLogic)
			{
				if (updateData.own == null || updateData.own.IsDead())
				{
					return global_Hooker.hk_CameraFunction.origin_GetCurrentCmdPitch(_icameraLogic);
				}
				return Contexts.sharedInstance.worldCamera.cameraTransform.Pitch;
			}

			// Token: 0x060000CA RID: 202 RVA: 0x000097C5 File Offset: 0x000079C5
			public static float origin_GetCurrentCmdPitch(ICameraLogic _icameraLogic)
			{
				Debug.Log("proxy");
				return 0f;
			}

			// Token: 0x060000CB RID: 203 RVA: 0x000097D6 File Offset: 0x000079D6
			public hk_CameraFunction()
			{
			}
		}

		// Token: 0x02000037 RID: 55
		[Obfuscation(Exclude = true)]
		public class hk_UiIEventCondition
		{
			// Token: 0x060000CC RID: 204 RVA: 0x000097DE File Offset: 0x000079DE
			public static float hk_Get_ControlEntityData_Yaw(UiIEventCondition self)
			{
				if (updateData.own == null || updateData.own.IsDead())
				{
					return global_Hooker.hk_UiIEventCondition.origin_Get_ControlEntityData_Yaw(self);
				}
				return UiIEventCondition.Get_cameraOwnerData_Yaw();
			}

			// Token: 0x060000CD RID: 205 RVA: 0x000097FF File Offset: 0x000079FF
			public static float origin_Get_ControlEntityData_Yaw(UiIEventCondition self)
			{
				Debug.Log("proxy");
				return 0f;
			}

			// Token: 0x060000CE RID: 206 RVA: 0x00009810 File Offset: 0x00007A10
			public static float hk_Get_cameraOwnerData_Yaw(UiIEventCondition self)
			{
				if (updateData.own == null || updateData.own.IsDead())
				{
					return global_Hooker.hk_UiIEventCondition.origin_Get_cameraOwnerData_Yaw(self);
				}
				return Contexts.sharedInstance.worldCamera.cameraTransform.Yaw;
			}

			// Token: 0x060000CF RID: 207 RVA: 0x00009840 File Offset: 0x00007A40
			public static float origin_Get_cameraOwnerData_Yaw(UiIEventCondition self)
			{
				Debug.Log("proxy");
				return 0f;
			}

			// Token: 0x060000D0 RID: 208 RVA: 0x00009851 File Offset: 0x00007A51
			public hk_UiIEventCondition()
			{
			}
		}

		// Token: 0x02000038 RID: 56
		[Obfuscation(Exclude = true)]
		public class hk_PlayerOrientationPredicationSystem
		{
			// Token: 0x060000D1 RID: 209 RVA: 0x0000985C File Offset: 0x00007A5C
			public static void hk_OnPredicate(PlayerOrientationPredicationSystem self, PlayerOrientationPredicationSystem _playerOrientationPredicationSystem, PlayerEntity _playerEntity, IUserCmd _iuserCmd)
			{
				if (Contexts.sharedInstance != null && Contexts.sharedInstance.player != null && Contexts.sharedInstance.player.cameraOwnerEntity != null)
				{
					PlayerEntity cameraOwnerEntity = Contexts.sharedInstance.player.cameraOwnerEntity;
					if (cameraOwnerEntity.orientation != null)
					{
						if (updateData.own != null && !updateData.own.IsDead())
						{
							cameraOwnerEntity.orientation.Pitch = rage_antiaim.sharePitch;
							cameraOwnerEntity.orientation.Yaw = rage_antiaim.shareYaw;
							global_Hooker.hk_PlayerOrientationPredicationSystem.origin_OnPredicate(self, _playerOrientationPredicationSystem, _playerEntity, _iuserCmd);
							return;
						}
						global_Hooker.hk_PlayerOrientationPredicationSystem.origin_OnPredicate(self, _playerOrientationPredicationSystem, _playerEntity, _iuserCmd);
					}
				}
			}

			// Token: 0x060000D2 RID: 210 RVA: 0x000098EB File Offset: 0x00007AEB
			public static void origin_OnPredicate(PlayerOrientationPredicationSystem self, PlayerOrientationPredicationSystem _playerOrientationPredicationSystem, PlayerEntity _playerEntity, IUserCmd _iuserCmd)
			{
				Debug.Log("proxy");
			}

			// Token: 0x060000D3 RID: 211 RVA: 0x000098F7 File Offset: 0x00007AF7
			public static void hk_PredictCmdOnCamera(PlayerOrientationPredicationSystem self, PlayerOrientationPredicationSystem _playerOrientationPredicationSystem, PlayerEntity _playerEntity, IUserCmd _iuserCmd)
			{
				if (updateData.own == null || updateData.own.IsDead())
				{
					global_Hooker.hk_PlayerOrientationPredicationSystem.origin_PredictCmdOnCamera(self, _playerOrientationPredicationSystem, _playerEntity, _iuserCmd);
				}
			}

			// Token: 0x060000D4 RID: 212 RVA: 0x00009915 File Offset: 0x00007B15
			public static void origin_PredictCmdOnCamera(PlayerOrientationPredicationSystem self, PlayerOrientationPredicationSystem _playerOrientationPredicationSystem, PlayerEntity _playerEntity, IUserCmd _iuserCmd)
			{
				Debug.Log("proxy");
			}

			// Token: 0x060000D5 RID: 213 RVA: 0x00009921 File Offset: 0x00007B21
			public hk_PlayerOrientationPredicationSystem()
			{
			}
		}

		// Token: 0x02000039 RID: 57
		[Obfuscation(Exclude = true)]
		public class hk_PlayerOrientationPlabackSystem
		{
			// Token: 0x060000D6 RID: 214 RVA: 0x0000992C File Offset: 0x00007B2C
			public static void hk_OnPlayback(PlayerOrientationPlabackSystem self, PlayerOrientationPlabackSystem _playerOrientationPlabackSystem)
			{
				try
				{
					global_Hooker.hk_PlayerOrientationPlabackSystem.origin_OnPlayback(self, _playerOrientationPlabackSystem);
					if (updateData.own != null && !updateData.own.IsDead() && Contexts.sharedInstance != null && Contexts.sharedInstance.player != null && Contexts.sharedInstance.player.cameraOwnerEntity != null)
					{
						PlayerEntity cameraOwnerEntity = Contexts.sharedInstance.player.cameraOwnerEntity;
						if (cameraOwnerEntity.orientation != null && cameraOwnerEntity.basicInfo != null && cameraOwnerEntity.punchOrientation != null)
						{
							PlayerEntityData current = cameraOwnerEntity.basicInfo.Current;
							PlayerEntityData next = cameraOwnerEntity.basicInfo.Next;
							cameraOwnerEntity.orientation.Pitch = rage_antiaim.sharePitch;
							cameraOwnerEntity.orientation.Yaw = rage_antiaim.shareYaw;
							cameraOwnerEntity.punchOrientation.PunchPitch = next.PunchPitch;
							cameraOwnerEntity.punchOrientation.PunchYaw = next.PunchYaw;
							cameraOwnerEntity.orientation.MoveYaw = rage_antiaim.shareYaw;
							cameraOwnerEntity.orientation.ActThirdMoveInterYaw = rage_antiaim.shareYaw;
						}
					}
				}
				catch
				{
				}
			}

			// Token: 0x060000D7 RID: 215 RVA: 0x00009A44 File Offset: 0x00007C44
			public static void origin_OnPlayback(PlayerOrientationPlabackSystem self, PlayerOrientationPlabackSystem _playerOrientationPlabackSystem)
			{
				Debug.Log("proxy");
			}

			// Token: 0x060000D8 RID: 216 RVA: 0x00009A50 File Offset: 0x00007C50
			public hk_PlayerOrientationPlabackSystem()
			{
			}
		}

		// Token: 0x0200003A RID: 58
		[Obfuscation(Exclude = true)]
		public class hk_CommandsComponent
		{
			// Token: 0x060000D9 RID: 217 RVA: 0x00009A58 File Offset: 0x00007C58
			public static short hk_LastCameraYaw(CommandsComponent self)
			{
				short num;
				if (updateData.own != null && !updateData.own.IsDead())
				{
					num = (short)(Contexts.sharedInstance.worldCamera.cameraTransform.Yaw * 100f);
				}
				else
				{
					num = global_Hooker.hk_CommandsComponent.origin_LastCameraYaw(self);
				}
				return num;
			}

			// Token: 0x060000DA RID: 218 RVA: 0x00009AA4 File Offset: 0x00007CA4
			public static short origin_LastCameraYaw(CommandsComponent self)
			{
				Debug.Log("proxy");
				return 0;
			}

			// Token: 0x060000DB RID: 219 RVA: 0x00009AB4 File Offset: 0x00007CB4
			public static short hk_LastCameraPitch(CommandsComponent self)
			{
				short num;
				if (updateData.own != null && !updateData.own.IsDead())
				{
					num = (short)(Contexts.sharedInstance.worldCamera.cameraTransform.Pitch * 100f);
				}
				else
				{
					num = global_Hooker.hk_CommandsComponent.origin_LastCameraPitch(self);
				}
				return num;
			}

			// Token: 0x060000DC RID: 220 RVA: 0x00009B00 File Offset: 0x00007D00
			public static short origin_LastCameraPitch(CommandsComponent self)
			{
				Debug.Log("proxy");
				return 0;
			}

			// Token: 0x060000DD RID: 221 RVA: 0x00009B0D File Offset: 0x00007D0D
			public hk_CommandsComponent()
			{
			}
		}

		// Token: 0x0200003B RID: 59
		[Obfuscation(Exclude = true)]
		public class hk_NetEaseCloudManager
		{
			// Token: 0x060000DE RID: 222 RVA: 0x00009B18 File Offset: 0x00007D18
			public static void hk_Send(int methodId, byte[] bytes)
			{
				try
				{
					LogManager.WritePassAntiCheatLog("拦截一次来自发往服务器的截图[截图检测]");
					string text = "C:\\PassAntiCheat";
					if (!Directory.Exists(text + "\\gotPicture"))
					{
						Directory.CreateDirectory(text + "\\gotPicture");
					}
					File.WriteAllBytes(string.Concat(new object[]
					{
						text,
						"\\gotPicture\\",
						DateTime.Now.Month,
						"-",
						DateTime.Now.Day,
						"-",
						DateTime.Now.Hour,
						"-",
						DateTime.Now.Minute,
						"-",
						DateTime.Now.Second,
						".jpg"
					}), bytes);
				}
				catch
				{
				}
			}

			// Token: 0x060000DF RID: 223 RVA: 0x00009C20 File Offset: 0x00007E20
			public static void origin_Send(NetEaseCloudManager self, NetEaseCloudManager _abstractCaptureSnapshot, byte[] bytes, int methodId)
			{
				Debug.Log("proxy");
			}

			// Token: 0x060000E0 RID: 224 RVA: 0x00009C2C File Offset: 0x00007E2C
			public hk_NetEaseCloudManager()
			{
			}
		}

		// Token: 0x0200003C RID: 60
		[Obfuscation(Exclude = true)]
		public class hk_PlayerEntityData
		{
			// Token: 0x060000E1 RID: 225 RVA: 0x00009C34 File Offset: 0x00007E34
			private static float smethod_6(float num)
			{
				if (num == 87.65556f)
				{
					return -88.9f;
				}
				if (num == -87.66111f)
				{
					return 90.1f;
				}
				if (num <= -30f && num != -88.9f && num != -87.66111f)
				{
					return 90.313f;
				}
				if (num < 30f || num == 90.1f || num == 87.66111f)
				{
					return num;
				}
				return -89.231f;
			}

			// Token: 0x060000E2 RID: 226 RVA: 0x00009C9A File Offset: 0x00007E9A
			public static float hk_get_ViewPitch(PlayerEntityData self)
			{
				LogManager.WriteLog("123");
				return 90f;
			}

			// Token: 0x060000E3 RID: 227 RVA: 0x00009CAB File Offset: 0x00007EAB
			public static float origin_get_ViewPitch(PlayerEntityData self)
			{
				Debug.Log("proxy");
				return 0f;
			}

			// Token: 0x060000E4 RID: 228 RVA: 0x00009CBC File Offset: 0x00007EBC
			public hk_PlayerEntityData()
			{
			}
		}

		// Token: 0x0200003D RID: 61
		[Obfuscation(Exclude = true)]
		public class hk_PlayerEntity
		{
			// Token: 0x060000E5 RID: 229 RVA: 0x00009CC4 File Offset: 0x00007EC4
			public static TraceResult hk_BulletTraceNormal(IPyEngine pyEngine, Trace results, IPyEntity player, float[] startF3, float[] endF3, float[] mins, float[] maxs)
			{
				Console.WriteLine("call hk\n");
				return default(TraceResult);
			}

			// Token: 0x060000E6 RID: 230 RVA: 0x00009CE4 File Offset: 0x00007EE4
			public static TraceResult origin_BulletTraceNormal(IntPtr self, IPyEngine pyEngine, Trace results, IPyEntity player, float[] startF3, float[] endF3, float[] mins, float[] maxs)
			{
				Debug.logger.Log("proxy");
				return default(TraceResult);
			}

			// Token: 0x060000E7 RID: 231 RVA: 0x00009D0C File Offset: 0x00007F0C
			public static FovComponent hk_get_fov(PlayerEntity self)
			{
				FovComponent fovComponent = global_Hooker.hk_PlayerEntity.origin_get_fov(self);
				fovComponent.Fov = (CheatThread._misc.fakeFov ? CheatThread._misc.fovValue : fovComponent.Fov);
				return fovComponent;
			}

			// Token: 0x060000E8 RID: 232 RVA: 0x00009D45 File Offset: 0x00007F45
			public static FovComponent origin_get_fov(PlayerEntity self)
			{
				Debug.logger.Log("proxy");
				return null;
			}

			// Token: 0x060000E9 RID: 233 RVA: 0x00009D57 File Offset: 0x00007F57
			public hk_PlayerEntity()
			{
			}
		}

		// Token: 0x0200003E RID: 62
		[Obfuscation(Exclude = true)]
		public class hk_FireUtility
		{
			// Token: 0x060000EA RID: 234 RVA: 0x00009D60 File Offset: 0x00007F60
			public static TraceResult hk_Fire(PlayerEntity player, WeaponEntity currentWeapon, int seq, float attackDistance, float[] mins, float[] maxs, bool knife = false)
			{
				IEntitsWeaponInfo info = currentWeapon.basicInfo.Info;
				float num = player.orientation.Yaw + player.GetPunchYaw() * 2f;
				float num2 = player.orientation.Pitch + player.GetPunchPitch() * 2f;
				if (player.move.ActThirdMove)
				{
					num = (float)player.basicInfo.MoveYaw;
				}
				double num3 = FireUtility.CalShotsFiredSpread(info.ShotsFiredSpreadMin, info.ShotsFiredSpreadMax, info.ShotsFiredSpreadTime, currentWeapon.attack.ShotsFired, info.AttackInterval);
				Vector3D vector3D = ShootingDirUtils.CalculateShotingDir(seq, (double)num, (double)num2, (double)currentWeapon.spread.Spread, currentWeapon.spread.SpreadScaleY, num3);
				TraceResult traceResult = FireUtility.BulletTrace(Contexts.sharedInstance.battleRoom.pyEngine.PyEngine, player, Contexts.sharedInstance.player, attackDistance, vector3D, mins, maxs, knife);
				traceResult.EntityId = 3;
				return traceResult;
			}

			// Token: 0x060000EB RID: 235 RVA: 0x00009E4B File Offset: 0x0000804B
			public hk_FireUtility()
			{
			}
		}
	}
}
