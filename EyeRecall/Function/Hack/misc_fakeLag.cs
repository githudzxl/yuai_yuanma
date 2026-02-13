using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Sources.Networking.Server;
using Assets.Sources.Utils.Player;
using EyeRecall.Helper.Player;
using SSJJNetworking.Packet;
using UnityEngine;

namespace EyeRecall.Function.Hack
{
	// Token: 0x02000018 RID: 24
	internal class misc_fakeLag
	{
		// Token: 0x0600007E RID: 126 RVA: 0x00006E78 File Offset: 0x00005078
		public misc_fakeLag()
		{
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00006E80 File Offset: 0x00005080
		// Note: this type is marked as 'beforefieldinit'.
		static misc_fakeLag()
		{
		}

		// Token: 0x04000071 RID: 113
		public static bool is_silentbot;

		// Token: 0x04000072 RID: 114
		public static bool canSend;

		// Token: 0x04000073 RID: 115
		internal static List<UdpPacket> UdpPackets = new List<UdpPacket>();

		// Token: 0x02000040 RID: 64
		internal class hk_BaseBattleServer
		{
			// Token: 0x060000EF RID: 239 RVA: 0x00009E7C File Offset: 0x0000807C
			public static void hk_SendUdpData(BattleServer self, int methodId, byte[] data = null)
			{
				try
				{
					bool flag = (!CheatThread._misc.fakeLag && !CheatThread._rageBot.Enable) || updateData.own == null || Contexts.sharedInstance.player.cameraOwnerEntity == null || updateData.own.IsDead();
					if (flag)
					{
						misc_fakeLag.hk_BaseBattleServer.origin_SendUdpData(self, methodId, data);
					}
					else
					{
						UdpPacket udpPacket = UdpPacket.CreateUdpPacket(self.ConnectionId, methodId, data);
						misc_fakeLag.UdpPackets.Add(udpPacket);
						bool flag2 = misc_fakeLag.is_silentbot && CheatThread._rageBot.Enable;
						if (flag2)
						{
							misc_fakeLag.canSend = true;
						}
						else
						{
							bool flag3 = misc_fakeLag.UdpPackets.Count<UdpPacket>() >= ((!CheatThread._misc.fakeLag && CheatThread._rageBot.Enable) ? 5 : CheatThread._misc.fakeLagTick) || misc_fakeLag.canSend || PlayerUtility.PlayerLength2D(Contexts.sharedInstance.player.cameraOwnerEntity) <= 0.1f;
							if (flag3)
							{
								foreach (UdpPacket udpPacket2 in misc_fakeLag.UdpPackets)
								{
									self.UdpSocket.Send(udpPacket2.FinalData, udpPacket2.FinalLength);
								}
								misc_fakeLag.canSend = false;
								misc_fakeLag.UdpPackets.Clear();
							}
						}
					}
				}
				catch (Exception)
				{
				}
			}

			// Token: 0x060000F0 RID: 240 RVA: 0x0000A004 File Offset: 0x00008204
			public static void origin_SendUdpData(BattleServer self, int methodId, byte[] data = null)
			{
				Debug.Log("proxy");
			}

			// Token: 0x060000F1 RID: 241 RVA: 0x0000A010 File Offset: 0x00008210
			public hk_BaseBattleServer()
			{
			}

			// Token: 0x060000F2 RID: 242 RVA: 0x0000A018 File Offset: 0x00008218
			// Note: this type is marked as 'beforefieldinit'.
			static hk_BaseBattleServer()
			{
			}

			// Token: 0x040000FE RID: 254
			public static List<UdpPacket> UdpPackQueue = new List<UdpPacket>();
		}
	}
}
