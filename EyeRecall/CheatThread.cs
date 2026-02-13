using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Assets.Sources.Info.Weapon;
using Entitas;
using EyeRecall.Function;
using EyeRecall.Function.Hack;
using EyeRecall.Helper;
using EyeRecall.Helper.Player;
using UnityEngine;

namespace EyeRecall
{
    // Token: 0x02000007 RID: 7
    [Obfuscation(Exclude = true)]
    internal class CheatThread : MonoBehaviour
    {
        // Token: 0x0600001F RID: 31 RVA: 0x00002A3C File Offset: 0x00000C3C
        // 初始化配置和默认值
        private void Start()
        {
            global_Hooker.global_Hooker_init();
            drawMenu.showMenu = true;
            CheatThread._aimBot.aimbotKey = KeyCode.LeftAlt.ToString();
            CheatThread._misc.thirdPersonKey = KeyCode.LeftAlt.ToString();
            CheatThread._rageBot.aimPos = new List<bool>(new bool[EyeRecall.Helper.config.rageBotPosText.Length]);
            CheatThread._Visual.showRadar = false;
            CheatThread._Visual.showScreenCollimation = false;
            CheatThread._Visual.nameEspRGB = 0.5f;
            CheatThread._Visual.weaponEspRGB = 0.5f;
            CheatThread._Visual.c4EspRGB = 0.5f;
            CheatThread._Visual.rangeEspRGB = 0.5f;
            CheatThread._Visual.screenCollimationEspRGB = 0.5f;
            CheatThread._Visual.glowEspRGB = 0.5f;
            CheatThread._misc.autoChatEnable = false;
            CheatThread._misc.autoChatDelay = 3100;
            CheatThread._misc.autoChatMessage = "刘晓倩（难逃情深愿）的蓝白色条纹内库居然被众人观赏，带透明的丝液，需要刘晓倩卡泡的语音以及各种视频的，某站搜超绝牢大私信就给！";
            if (Directory.Exists(EyeRecall.Helper.config.settingPath))
            {
                EyeRecall.Helper.config._config = Directory.GetFiles(EyeRecall.Helper.config.settingPath, "*.cfg");
                return;
            }
            Directory.CreateDirectory(EyeRecall.Helper.config.settingPath);
        }

        // Token: 0x06000020 RID: 32 RVA: 0x00002B70 File Offset: 0x00000D70
        // 左手持枪功能
        private void LeftGun()
        {
            if (CheatThread._misc.leftGun)
            {
                Vector3 localPosition = updateData.own.firstPersonUnityObjects.HandGameObject.transform.localPosition;
                updateData.own.firstPersonUnityObjects.HandGameObject.transform.localPosition = new Vector3(0f, 0f, -10f);
                return;
            }
            if (!CheatThread._misc.leftGun)
            {
                updateData.own.firstPersonUnityObjects.HandGameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
            }
        }

        // Token: 0x06000021 RID: 33 RVA: 0x00002C0C File Offset: 0x00000E0C
        // 设置Yaw角度
        private void set_yawAngle()
        {
            if (CheatThread._antiAim.Enable && (CheatThread._antiAim.AntiAimYawMode == 0 || CheatThread._antiAim.AntiAimYawMode == 1))
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    CheatThread._antiAim.fakeYaw = -90;
                }
                if (Input.GetKeyDown(KeyCode.X))
                {
                    CheatThread._antiAim.fakeYaw = -180;
                }
                if (Input.GetKeyDown(KeyCode.C))
                {
                    CheatThread._antiAim.fakeYaw = 90;
                }
            }
        }

        // Token: 0x06000022 RID: 34 RVA: 0x00002C80 File Offset: 0x00000E80
        // 按键功能设置
        private void key_setFunction()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                CheatThread._antiAim.Enable = !CheatThread._antiAim.Enable;
            }
            if (Input.GetKeyDown(KeyCode.Home) || Input.GetKeyDown(KeyCode.F2))
            {
                drawMenu.showMenu = !drawMenu.showMenu;
            }
            if (CheatThread._misc.thirdPerson && Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), CheatThread._misc.thirdPersonKey)))
            {
                global_Hooker.hk_TpsCameraLogic.isTps = !global_Hooker.hk_TpsCameraLogic.isTps;
            }
            if (CheatThread._misc.InstanceSniper && Input.GetKeyDown(KeyCode.Mouse1))
            {
                FakeInput.ForceMouse(0, FakeInput.InputST.TrueOnce);
                FakeInput.ForceMouse(1, FakeInput.InputST.TrueOnce);
            }
        }

        // Token: 0x06000023 RID: 35 RVA: 0x00002D39 File Offset: 0x00000F39
        // 替换武器
        private void ReplaceWeapon(PlayerEntity myPlayentity, string weaponContexts)
        {
            myPlayentity.basicInfo.Current.CurrentWeaponName = weaponContexts;
            myPlayentity.currentWeapon.Name = weaponContexts;
            myPlayentity.currentWeapon.WeaponInfo = WeaponInfoFactory.Instance.GetWeaponInfo(weaponContexts);
        }

        // Token: 0x06000024 RID: 36 RVA: 0x00002D70 File Offset: 0x00000F70
        // 连跳功能
        private void onceBhop()
        {
            if (CheatThread._misc.bhop && CheatThread._misc.bhoptype == 1)
            {
                if (Input.GetKeyDown(KeyCode.Mouse3))
                {
                    CheatThread.needbhop = !CheatThread.needbhop;
                }
                if (CheatThread.needbhop && updateData.own.OnGround())
                {
                    FakeInput.ForceKey(KeyCode.Space, FakeInput.InputST.TrueOnce);
                }
            }
        }

        // Token: 0x06000025 RID: 37 RVA: 0x00002DCC File Offset: 0x00000FCC
        // 设置RageBot瞄准位置
        private void set_ragebotPos()
        {
            rage_silentbot.AimBone[0] = CheatThread._rageBot.aimPos[0];
            rage_silentbot.AimBone[1] = CheatThread._rageBot.aimPos[1];
            rage_silentbot.AimBone[2] = CheatThread._rageBot.aimPos[2];
            rage_silentbot.AimBone[3] = CheatThread._rageBot.aimPos[3];
            rage_silentbot.AimBone[4] = CheatThread._rageBot.aimPos[4];
            rage_silentbot.AimBone[5] = CheatThread._rageBot.aimPos[5];
            rage_silentbot.AimBone[6] = CheatThread._rageBot.aimPos[6];
            rage_silentbot.AimBone[7] = CheatThread._rageBot.aimPos[7];
            rage_silentbot.AimBone[8] = CheatThread._rageBot.aimPos[8];
            rage_silentbot.AimBone[9] = CheatThread._rageBot.aimPos[9];
            rage_silentbot.AimBone[10] = CheatThread._rageBot.aimPos[10];
            rage_silentbot.AimBone[11] = CheatThread._rageBot.aimPos[11];
            rage_silentbot.AimBone[12] = CheatThread._rageBot.aimPos[12];
            rage_silentbot.AimBone[13] = CheatThread._rageBot.aimPos[13];
            rage_silentbot.AimBone[14] = CheatThread._rageBot.aimPos[14];
            rage_silentbot.AimBone[15] = CheatThread._rageBot.aimPos[15];
        }

        // Token: 0x06000026 RID: 38 RVA: 0x00002F58 File Offset: 0x00001158
        // 自动发送聊天消息
        private void AutoChat()
        {
            if (CheatThread._misc.autoChatEnable && !string.IsNullOrEmpty(CheatThread._misc.autoChatMessage) && CheatThread._misc.autoChatMessage.Trim().Length > 0)
            {
                CheatThread.autoChatTimer += Time.deltaTime;
                if (CheatThread.autoChatTimer >= (float)CheatThread._misc.autoChatDelay / 1000f)
                {
                    ChatUtility.SendServerMessage(CheatThread._misc.autoChatMessage);
                    CheatThread.autoChatTimer = 0f;
                    return;
                }
            }
            else
            {
                CheatThread.autoChatTimer = 0f;
            }
        }

        // Token: 0x06000027 RID: 39 RVA: 0x00002FE8 File Offset: 0x000011E8
        // 主更新循环
        private void Update()
        {
            try
            {
                if (Directory.Exists(EyeRecall.Helper.config.settingPath))
                {
                    EyeRecall.Helper.config._config = Directory.GetFiles(EyeRecall.Helper.config.settingPath, "*.cfg");
                }
                updateData.Update();
                misc_noRecoil.Execute();
                misc_tiggerbot.Execute();
                this.set_yawAngle();
                this.set_ragebotPos();
                this.key_setFunction();
                this.LeftGun();
                this.onceBhop();
                this.AutoChat();
                if (CheatThread._misc.ReplaceWeapon && updateData.own.currentWeapon.Weapon == 1)
                {
                    this.ReplaceWeapon(updateData.own, "m4a1_legend2");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("CheatThread Update 错误: " + ex.Message);
            }
        }

        // Token: 0x06000028 RID: 40 RVA: 0x000030A0 File Offset: 0x000012A0
        // GUI绘制
        private void OnGUI()
        {
            try
            {
                GUI.depth = 879;
                if (updateData.EntityList != null)
                {
                    foreach (IEntity entity in updateData.EntityList)
                    {
                        visual_esp.Execute((PlayerEntity)entity);
                    }
                }
                if (CheatThread._aimBot.Enable)
                {
                    rage_aimbot.Execute();
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("CheatThread OnGUI 错误: " + ex.Message);
            }
        }

        // Token: 0x06000029 RID: 41 RVA: 0x0000313C File Offset: 0x0000133C
        public CheatThread()
        {
        }

        // Token: 0x04000026 RID: 38
        public static EyeRecall.Helper.config.aimBot _aimBot;

        // Token: 0x04000027 RID: 39
        public static EyeRecall.Helper.config.RageBot _rageBot;

        // Token: 0x04000028 RID: 40
        public static EyeRecall.Helper.config.AntiAim _antiAim;

        // Token: 0x04000029 RID: 41
        public static EyeRecall.Helper.config.esp _Visual;

        // Token: 0x0400002A RID: 42
        public static EyeRecall.Helper.config.Misc _misc;

        // Token: 0x0400002B RID: 43
        public static EyeRecall.Helper.config.Menu _Menu;

        // Token: 0x0400002C RID: 44
        private static bool needbhop;

        // Token: 0x0400002D RID: 45
        private static float autoChatTimer;
    }
}
