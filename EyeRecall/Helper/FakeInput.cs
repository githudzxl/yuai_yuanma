using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Assets.Scripts.Input;
using UnityEngine;

namespace EyeRecall.Helper
{
    // Token: 0x0200000A RID: 10
    internal class FakeInput : IDeviceInput
    {
        // Token: 0x06000030 RID: 48 RVA: 0x0000358C File Offset: 0x0000178C
        public bool AnyKey()
        {
            if (FakeInput.preInput != null)
            {
                FakeInput.preInput();
            }
            if (!FakeInput.forceKey.Any((KeyValuePair<int, int> a) => a.Value != 0))
            {
                if (!FakeInput.forceMouse.Any((KeyValuePair<int, int> a) => a.Value != 0))
                {
                    return Input.anyKey;
                }
            }
            return true;
        }

        // Token: 0x06000031 RID: 49 RVA: 0x00003607 File Offset: 0x00001807
        public static void ForceMouse(int mouseButton, FakeInput.InputST st)
        {
            FakeInput.forceMouse[mouseButton] = (int)st;
        }

        // Token: 0x06000032 RID: 50 RVA: 0x00003615 File Offset: 0x00001815
        public static void ForceKey(KeyCode keyCode, FakeInput.InputST st)
        {
            FakeInput.forceKey[(int)keyCode] = (int)st;
        }

        // Token: 0x06000033 RID: 51 RVA: 0x00003623 File Offset: 0x00001823
        public bool AnyKeyDown()
        {
            return FakeInput.forceKey.Any((KeyValuePair<int, int> a) => a.Value == 2) || Input.anyKeyDown;
        }

        // Token: 0x06000034 RID: 52 RVA: 0x00003657 File Offset: 0x00001857
        public bool GetMouseButtonUp(int button)
        {
            return Input.GetMouseButtonUp(button);
        }

        // Token: 0x06000035 RID: 53 RVA: 0x00003660 File Offset: 0x00001860
        public float GetAxis(string axis)
        {
            if (axis == "Mouse X")
            {
                float x = FakeInput.forceAxisOnce.x;
                FakeInput.forceAxisOnce.x = 0f;
                return Input.GetAxis(axis) + x;
            }
            if (axis == "Mouse Y")
            {
                float y = FakeInput.forceAxisOnce.y;
                FakeInput.forceAxisOnce.y = 0f;
                return Input.GetAxis(axis) + y;
            }
            return Input.GetAxis(axis);
        }

        // Token: 0x06000036 RID: 54 RVA: 0x000036D4 File Offset: 0x000018D4
        // 获取按键状态
        public bool GetKey(KeyCode keyCode)
        {
            int num;
            if (FakeInput.forceKey.TryGetValue((int)keyCode, out num) && num > 0)
            {
                switch (num)
                {
                    case 1:
                        return true;
                    case 2:
                        FakeInput.forceKey[(int)keyCode] = 0;
                        return true;
                    case 3:
                        return false;
                    case 4:
                        FakeInput.forceKey[(int)keyCode] = 0;
                        return false;
                }
            }
            return Input.GetKey(keyCode);
        }

        // Token: 0x06000037 RID: 55 RVA: 0x00003734 File Offset: 0x00001934
        public bool GetKeyDown(KeyCode keyCode)
        {
            int num;
            if (FakeInput.forceKey.TryGetValue((int)keyCode, out num) && num > 0)
            {
                if (num == 2)
                {
                    return true;
                }
                if (num == 4)
                {
                    return false;
                }
            }
            return Input.GetKeyDown(keyCode);
        }

        // Token: 0x06000038 RID: 56 RVA: 0x00003768 File Offset: 0x00001968
        // 获取鼠标按键状态
        public bool GetMouseButton(int button)
        {
            int num;
            if (FakeInput.forceMouse.TryGetValue(button, out num) && num > 0)
            {
                switch (num)
                {
                    case 1:
                        return true;
                    case 2:
                        FakeInput.forceMouse[button] = 0;
                        return true;
                    case 3:
                        return false;
                    case 4:
                        FakeInput.forceMouse[button] = 0;
                        return false;
                }
            }
            // 屏蔽刺刀功能
            if (CheatThread._misc.shieldbayonet && button == 1 && Contexts.sharedInstance.player.myPlayerEntity.currentWeapon.Weapon != 3 && Contexts.sharedInstance.player.myPlayerEntity.currentWeapon.Weapon != 4)
            {
                return button == 0;
            }
            return Input.GetMouseButton(button);
        }

        // Token: 0x06000039 RID: 57 RVA: 0x00003815 File Offset: 0x00001A15
        public bool GetMouseButtonDown(int button)
        {
            return Input.GetMouseButtonDown(button);
        }

        // Token: 0x0600003A RID: 58 RVA: 0x0000381D File Offset: 0x00001A1D
        public FakeInput()
        {
        }

        // Token: 0x0600003B RID: 59 RVA: 0x00003825 File Offset: 0x00001A25
        // Note: this type is marked as 'beforefieldinit'.
        static FakeInput()
        {
        }

        // Token: 0x0400003F RID: 63
        public static Action preInput = null;

        // Token: 0x04000040 RID: 64
        public static Vector2 forceAxisOnce = Vector2.zero;

        // Token: 0x04000041 RID: 65
        private static Dictionary<int, int> forceKey = new Dictionary<int, int>();

        // Token: 0x04000042 RID: 66
        private static Dictionary<int, int> forceMouse = new Dictionary<int, int>();

        // Token: 0x04000043 RID: 67
        public static Vector2 forceAxis = Vector2.zero;

        // Token: 0x0200002C RID: 44
        // 输入状态枚举
        public enum InputST
        {
            // Token: 0x040000EF RID: 239
            None,
            // Token: 0x040000F0 RID: 240
            TrueKeep,
            // Token: 0x040000F1 RID: 241
            TrueOnce,
            // Token: 0x040000F2 RID: 242
            FalseKeep,
            // Token: 0x040000F3 RID: 243
            FalseOnce
        }

        // Token: 0x0200002D RID: 45
        // 编译器生成的辅助类，用于Lambda表达式
        [CompilerGenerated]
        [Serializable]
        private sealed class CompilerGeneratedClass
        {
            // Token: 0x060000A8 RID: 168 RVA: 0x00009075 File Offset: 0x00007275
            // Note: this type is marked as 'beforefieldinit'.
            static CompilerGeneratedClass()
            {
            }

            // Token: 0x060000A9 RID: 169 RVA: 0x00009081 File Offset: 0x00007281
            public CompilerGeneratedClass()
            {
            }

            // Token: 0x060000AA RID: 170 RVA: 0x00009089 File Offset: 0x00007289
            internal bool AnyKey_Lambda1(KeyValuePair<int, int> a)
            {
                return a.Value != 0;
            }

            // Token: 0x060000AB RID: 171 RVA: 0x00009095 File Offset: 0x00007295
            internal bool AnyKey_Lambda2(KeyValuePair<int, int> a)
            {
                return a.Value != 0;
            }

            // Token: 0x060000AC RID: 172 RVA: 0x000090A1 File Offset: 0x000072A1
            internal bool AnyKeyDown_Lambda1(KeyValuePair<int, int> a)
            {
                return a.Value == 2;
            }

            // Token: 0x040000F4 RID: 244
            public static readonly FakeInput.CompilerGeneratedClass Instance = new FakeInput.CompilerGeneratedClass();

            // Token: 0x040000F5 RID: 245
            public static Func<KeyValuePair<int, int>, bool> AnyKey_Func1;

            // Token: 0x040000F6 RID: 246
            public static Func<KeyValuePair<int, int>, bool> AnyKey_Func2;

            // Token: 0x040000F7 RID: 247
            public static Func<KeyValuePair<int, int>, bool> AnyKeyDown_Func1;
        }
    }
}

