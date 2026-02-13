using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Assets.Sources.Modules.Player.HitBox;
using Assets.Sources.Utils.Weapon;
using Entitas;
using EyeRecall.Helper;
using EyeRecall.Helper.Player;
using share;
using UnityEngine;

namespace EyeRecall.Function.Hack
{
    // Token: 0x0200001D RID: 29
    internal class rage_silentbot
    {
        // Token: 0x0600008F RID: 143 RVA: 0x00007AD4 File Offset: 0x00005CD4
        // 获取目标指定骨骼的位置
        private static Vector3 GetHitBox(PlayerEntity target, int LeakType)
        {
            Vector3 vector = Vector3.zero;
            switch (LeakType)
            {
                case 0:
                    vector = target.GetHitBox("Bip01_Head").position + new Vector3(0f, 10f, 0f);
                    break;
                case 1:
                    vector = target.GetHitBox("Bip01_Neck").position;
                    break;
                case 2:
                    vector = target.GetHitBox("Bip01_L_UpperArm").position;
                    break;
                case 3:
                    vector = target.GetHitBox("Bip01_R_UpperArm").position;
                    break;
                case 4:
                    vector = target.GetHitBox("Bip01_L_Forearm").position;
                    break;
                case 5:
                    vector = target.GetHitBox("Bip01_R_Forearm").position;
                    break;
                case 6:
                    vector = target.GetHitBox("Bip01_L_Hand").position;
                    break;
                case 7:
                    vector = target.GetHitBox("Bip01_R_Hand").position;
                    break;
                case 8:
                    vector = target.GetHitBox("Bip01_Spine").position;
                    break;
                case 9:
                    vector = target.GetHitBox("Bip01_Pelvis").position;
                    break;
                case 10:
                    vector = target.GetHitBox("Bip01_L_Thigh").position;
                    break;
                case 11:
                    vector = target.GetHitBox("Bip01_R_Thigh").position;
                    break;
                case 12:
                    vector = target.GetHitBox("Bip01_L_Calf").position;
                    break;
                case 13:
                    vector = target.GetHitBox("Bip01_R_Calf").position;
                    break;
                case 14:
                    vector = target.GetHitBox("Bip01_L_Foot").position;
                    break;
                case 15:
                    vector = target.GetHitBox("Bip01_R_Foot").position;
                    break;
                default:
                    vector = Vector3.zero;
                    break;
            }
            return vector;
        }

        // Token: 0x06000090 RID: 144 RVA: 0x00007CA0 File Offset: 0x00005EA0
        // 第一次角度钳制
        public static Vector3 firstClampAngle(Vector3 vector3_2, Vector3 vector3_3)
        {
            Vector3 normalized = (vector3_3 - vector3_2).normalized;
            float num = Mathf.Atan2(normalized.z, normalized.x) * 57.29578f - 90f;
            if (num < -180f)
            {
                num += 360f;
            }
            if (num > 180f)
            {
                num -= 360f;
            }
            float num2 = Mathf.Asin(normalized.y / normalized.magnitude) * 57.29578f;
            num = Mathf.Clamp(num, -180f, 180f);
            num2 = Mathf.Clamp(num2, -89f, 89f);
            return new Vector3(num2, num, 0f);
        }

        // Token: 0x06000091 RID: 145 RVA: 0x00007D44 File Offset: 0x00005F44
        // 第二次角度钳制
        public static float secondClampAngle(float angle, float Mini, float Max)
        {
            float num;
            if (angle > Mini)
            {
                if (angle < Max)
                {
                    num = angle;
                }
                else
                {
                    num = Max;
                }
            }
            else
            {
                num = Mini;
            }
            return num;
        }

        // Token: 0x06000092 RID: 146 RVA: 0x00007D64 File Offset: 0x00005F64
        // 第三次角度钳制
        public static Vector3 thirdClampAngle(Vector3 angle)
        {
            angle.x = rage_silentbot.secondClampAngle(angle.x, -89f, 89f);
            angle.y = rage_silentbot.secondClampAngle(angle.y, -180f, 180f);
            angle.z = 0f;
            return angle;
        }

        // Token: 0x06000093 RID: 147 RVA: 0x00007DB8 File Offset: 0x00005FB8
        // 第四次角度钳制
        public static Vector3 forthClampAngle(Vector3 angle)
        {
            if (angle.x > 89f)
            {
                angle.x -= 180f;
            }
            if (angle.x < -89f)
            {
                angle.x += 180f;
            }
            angle.y %= 360f;
            if (angle.y > 180f)
            {
                angle.y -= 360f;
            }
            return angle;
        }

        // Token: 0x06000094 RID: 148 RVA: 0x00007E30 File Offset: 0x00006030
        // 消除后坐力
        public static void noRecoil(PlayerEntity ownEntity, ref Vector3 vector3_0)
        {
            if (ownEntity != null)
            {
                float punchPitch = ownEntity.GetPunchPitch();
                float punchYaw = ownEntity.GetPunchYaw();
                vector3_0.x -= 2f * punchPitch;
                float y = vector3_0.y;
                vector3_0.y = y - 2f * punchYaw;
                rage_silentbot._tempPunchpitch = punchPitch;
                rage_silentbot._tempPunchyaw = punchYaw;
            }
        }

        // Token: 0x06000095 RID: 149 RVA: 0x00007E84 File Offset: 0x00006084
        // 检查是否可以瞄准目标
        private static bool canAim(PlayerEntity playerEntity_0, PlayerEntity playerEntity_1, Vector3 vector3_2, Vector3 vector3_3)
        {
            Vector3 vector = positionHelper.World2EntityPos((vector3_3 - vector3_2).normalized);
            new Vector3D((double)vector.x, (double)vector.y, (double)vector.z);
            return FireUtility.BulletTraceNormal(Contexts.sharedInstance.battleRoom.pyEngine.PyEngine, playerEntity_0, 10000000f, new Vector3(vector.x, vector.y, vector.z), new float[3], new float[3]).EntityId == playerEntity_1.GetId();
        }

        // Token: 0x06000096 RID: 150 RVA: 0x00007F14 File Offset: 0x00006114
        // 修正瞄准位置
        internal static Vector3 FixPos(PlayerEntity playerEntity_0, PlayerEntity playerEntity_1, Vector3 vector3_2, Vector3 vector3_3)
        {
            Vector3 vector = rage_silentbot.firstClampAngle(vector3_2 + positionHelper.EntityPos2World(playerEntity_0.move.Velocity) * ((float)Contexts.sharedInstance.userCommand.commands.CommandToSendList.Last.Value.FrameInterval * 0.001f), vector3_3 + positionHelper.EntityPos2World(playerEntity_1.move.Velocity) * ((float)Contexts.sharedInstance.userCommand.commands.CommandToSendList.Last.Value.FrameInterval * 0.001f));
            vector = rage_silentbot.forthClampAngle(vector);
            rage_silentbot.thirdClampAngle(vector);
            rage_silentbot.noRecoil(playerEntity_0, ref vector);
            vector = rage_silentbot.forthClampAngle(vector);
            rage_silentbot.thirdClampAngle(vector);
            return vector;
        }

        // Token: 0x06000097 RID: 151 RVA: 0x00007FD8 File Offset: 0x000061D8
        // 获取屏幕距离
        private float GetScreenDistance(IEntity entity, Vector2 screenCenter)
        {
            Vector3 vector = positionHelper.EntityPos2World(positionHelper.GetPosition((PlayerEntity)entity));
            Vector3 vector2 = updateData.mainCamera.WorldToScreenPoint(vector);
            return Vector2.Distance(new Vector2(vector2.x, vector2.y), screenCenter);
        }

        // Token: 0x06000098 RID: 152 RVA: 0x0000801C File Offset: 0x0000621C
        // 执行RageBot逻辑
        internal static bool Execute(PlayerEntity playerEntity_0, ref float float_0, ref float float_1, int int_1)
        {
            bool flag2;
            try
            {
                new List<IEntity>();
                float num = 1E+09f;
                bool flag;
                if (CheatThread._rageBot.Enable)
                {
                    PlayerEntity playerEntity = null;
                    Vector2 screenCenter = new Vector2((float)Screen.width * 0.5f, (float)Screen.height * 0.5f);
                    Vector3 compenstatePos = playerEntity_0.GetCompenstatePos(playerEntity_0.fpos.Change.GetPosIndex());
                    Vector3 vector = new Vector3(compenstatePos.x, compenstatePos.y, compenstatePos.z + (float)playerEntity_0.move.PyPlayerMove.GetViewHeight());
                    vector = positionHelper.EntityPos2World(vector);
                    Camera main = Camera.main;
                    foreach (IEntity entity in updateData.EntityList.OrderBy(delegate (IEntity _player)
                    {
                        Vector3 vector9 = positionHelper.EntityPos2World(positionHelper.GetPosition((PlayerEntity)_player));
                        Vector3 vector10 = updateData.mainCamera.WorldToScreenPoint(vector9);
                        return Vector2.Distance(new Vector2(vector10.x, vector10.y), screenCenter);
                    }).ToList<IEntity>())
                    {
                        if (entity != null)
                        {
                            PlayerEntity playerEntity2 = (PlayerEntity)entity;
                            if (!playerEntity2.IsMySelf() && playerEntity2.hasHitBox && playerEntity2.hasThirdPersonUnityObjects && playerEntity2.GetTeam() != playerEntity_0.GetTeam() && !playerEntity2.IsDead() && playerEntity2.GetHpPercent() >= 0.0 && !PlayerHelper.GetIsUnmatched(playerEntity2) && Contexts.sharedInstance.weapon.currentWeaponEntity.slot.Slot < 3)
                            {
                                if (playerEntity2.hitBox.HitBoxBrushDirty)
                                {
                                    PlayerHitBoxBrushUtility.UpdatePlayerAllHitBoxBrush(playerEntity2);
                                }
                                if (CheatThread._rageBot.Multiplehitbox)
                                {
                                    for (int i = 0; i < rage_silentbot.AimBone.Length; i++)
                                    {
                                        if (!rage_silentbot.AimBone[i])
                                        {
                                            rage_silentbot.ManualAttack = false;
                                        }
                                        else
                                        {
                                            rage_silentbot.a_silent_target = playerEntity2;
                                            Vector3 hitBox = rage_silentbot.GetHitBox(rage_silentbot.a_silent_target, i);
                                            rage_silentbot.c_aimPos = hitBox;
                                            if (main != null)
                                            {
                                                Vector3 vector2 = main.WorldToScreenPoint(rage_silentbot.c_aimPos);
                                                Vector2 vector3 = new Vector2(vector2.x, (float)Screen.height - vector2.y);
                                                if (vector2.z > 0.01f)
                                                {
                                                    float num2 = Vector2.Distance(screenCenter, vector3);
                                                    if (num2 < num)
                                                    {
                                                        num = num2;
                                                        playerEntity = playerEntity2;
                                                        rage_silentbot.h_silent_target = playerEntity;
                                                        rage_silentbot.h_aimPos = rage_aimbot.GetAimPos(rage_silentbot.h_silent_target, true);
                                                    }
                                                }
                                            }
                                            if (rage_silentbot.canAim(playerEntity_0, playerEntity2, vector, hitBox))
                                            {
                                                Vector3 vector4 = rage_silentbot.FixPos(playerEntity_0, playerEntity2, vector, rage_silentbot.c_aimPos);
                                                float_0 = vector4.y;
                                                float_1 = vector4.x;
                                                flag = true;
                                                rage_silentbot.ManualAttack = false;
                                                rage_silentbot.AutoAttack = true;
                                                return flag;
                                            }
                                            rage_silentbot.ManualAttack = false;
                                        }
                                    }
                                }
                                else
                                {
                                    rage_silentbot.a_silent_target = playerEntity2;
                                    Vector3 hitBox2 = rage_silentbot.GetHitBox(rage_silentbot.a_silent_target, 0);
                                    rage_silentbot.c_aimPos = hitBox2;
                                    if (main != null)
                                    {
                                        Vector3 vector5 = main.WorldToScreenPoint(rage_silentbot.c_aimPos);
                                        Vector2 vector6 = new Vector2(vector5.x, (float)Screen.height - vector5.y);
                                        if (vector5.z > 0.01f)
                                        {
                                            float num3 = Vector2.Distance(screenCenter, vector6);
                                            if (num3 < num)
                                            {
                                                num = num3;
                                                playerEntity = playerEntity2;
                                                rage_silentbot.h_silent_target = playerEntity;
                                                rage_silentbot.h_aimPos = rage_aimbot.GetAimPos(rage_silentbot.h_silent_target, true);
                                            }
                                        }
                                    }
                                    if (rage_silentbot.canAim(playerEntity_0, playerEntity2, vector, hitBox2))
                                    {
                                        Vector3 vector7 = rage_silentbot.FixPos(playerEntity_0, playerEntity2, vector, rage_silentbot.c_aimPos);
                                        float_0 = vector7.y;
                                        float_1 = vector7.x;
                                        flag = true;
                                        rage_silentbot.ManualAttack = false;
                                        rage_silentbot.AutoAttack = true;
                                        return flag;
                                    }
                                    rage_silentbot.ManualAttack = false;
                                }
                            }
                        }
                    }
                    if (Input.GetKey(KeyCode.Mouse2) && CheatThread._rageBot.ManualTiggerbot && playerEntity != null)
                    {
                        Vector3 vector8 = rage_silentbot.FixPos(playerEntity_0, playerEntity, vector, rage_silentbot.h_aimPos);
                        float_0 = vector8.y;
                        float_1 = vector8.x;
                        rage_silentbot.AutoAttack = false;
                        rage_silentbot.ManualAttack = true;
                        flag = true;
                    }
                    else
                    {
                        rage_silentbot.ManualAttack = false;
                        flag = false;
                    }
                }
                else
                {
                    rage_silentbot.ManualAttack = false;
                    rage_silentbot.AutoAttack = false;
                    flag = false;
                }
                flag2 = flag;
            }
            catch
            {
                flag2 = false;
            }
            return flag2;
        }

        // Token: 0x06000099 RID: 153 RVA: 0x0000845C File Offset: 0x0000665C
        public rage_silentbot()
        {
        }

        // Token: 0x0600009A RID: 154 RVA: 0x00008464 File Offset: 0x00006664
        // Note: this type is marked as 'beforefieldinit'.
        static rage_silentbot()
        {
        }

        // Token: 0x0400007D RID: 125
        public static bool[] AimBone = new bool[16];

        // Token: 0x0400007E RID: 126
        public static PlayerEntity a_silent_target;

        // Token: 0x0400007F RID: 127
        public static PlayerEntity h_silent_target;

        // Token: 0x04000080 RID: 128
        public static Vector3 c_aimPos;

        // Token: 0x04000081 RID: 129
        public static Vector3 h_aimPos;

        // Token: 0x04000082 RID: 130
        private static float _tempPunchpitch;

        // Token: 0x04000083 RID: 131
        private static float _tempPunchyaw;

        // Token: 0x04000084 RID: 132
        public static bool AutoAttack;

        // Token: 0x04000085 RID: 133
        public static bool flag5;

        // Token: 0x04000086 RID: 134
        public static bool ManualAttack;

        // Token: 0x02000041 RID: 65
        // 编译器生成的辅助类，用于Lambda表达式
        [CompilerGenerated]
        private sealed class ExecuteDisplayClass
        {
            // Token: 0x060000F3 RID: 243 RVA: 0x0000A024 File Offset: 0x00008224
            public ExecuteDisplayClass()
            {
            }

            // Token: 0x060000F4 RID: 244 RVA: 0x0000A02C File Offset: 0x0000822C
            internal float Execute_Lambda1(IEntity _player)
            {
                Vector3 vector = positionHelper.EntityPos2World(positionHelper.GetPosition((PlayerEntity)_player));
                Vector3 vector2 = updateData.mainCamera.WorldToScreenPoint(vector);
                return Vector2.Distance(new Vector2(vector2.x, vector2.y), this.screenCenter);
            }

            // Token: 0x040000FF RID: 255
            public Vector2 screenCenter;
        }
    }
}

