using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using DotNetDetour;

// Token: 0x02000004 RID: 4
public class MethodHook
{
    // 自动属性的后备字段
    [CompilerGenerated]
    private bool _isHooked;

    // Token: 0x17000001 RID: 1
    // 标识方法是否已被Hook
    public bool isHooked
    {
        [CompilerGenerated]
        get
        {
            return this._isHooked;
        }
        [CompilerGenerated]
        private set
        {
            this._isHooked = value;
        }
    }

    // Token: 0x06000007 RID: 7 RVA: 0x000020B4 File Offset: 0x000002B4
    // 静态构造函数，初始化不同平台的跳转指令缓冲区
    static MethodHook()
    {
        byte[] array = new byte[14];
        array[0] = byte.MaxValue;
        array[1] = 37;
        MethodHook.s_jmpBuff_64 = array;
        MethodHook.s_jmpBuff_arm32_arm = new byte[] { 4, 240, 31, 229, 0, 0, 0, 0 };
        MethodHook.s_jmpBuff_arm32_thumb = new byte[]
        {
            0, 181, 16, 180, 3, 180, 120, 70, 22, 48,
            0, 104, 105, 70, 8, 49, 8, 96, 121, 70,
            14, 49, 142, 70, 1, 188, 2, 188, 0, 189,
            192, 70, 0, 0, 0, 0, 0, 189
        };
        MethodHook.s_jmpBuff_arm64 = new byte[]
        {
            4, 240, 31, 229, 0, 0, 0, 0, 0, 0,
            0, 0
        };

        // 根据不同平台设置跳转缓冲区和地址偏移
        if (LDasm.IsAndroidARM())
        {
            MethodHook.s_addrOffset = 4;
            if (IntPtr.Size == 4)
            {
                MethodHook.s_jmpBuff = MethodHook.s_jmpBuff_arm32_arm;
                return;
            }
            MethodHook.s_jmpBuff = MethodHook.s_jmpBuff_arm64;
            return;
        }
        else
        {
            if (IntPtr.Size == 4)
            {
                MethodHook.s_jmpBuff = MethodHook.s_jmpBuff_32;
                MethodHook.s_addrOffset = 1;
                return;
            }
            MethodHook.s_jmpBuff = MethodHook.s_jmpBuff_64;
            MethodHook.s_addrOffset = 6;
            return;
        }
    }

    // Token: 0x06000008 RID: 8 RVA: 0x0000218C File Offset: 0x0000038C
    // 构造函数，初始化Hook所需的方法和指针
    public MethodHook(MethodBase targetMethod, MethodBase replacementMethod, MethodBase proxyMethod = null)
    {
        this._targetMethod = targetMethod;
        this._replacementMethod = replacementMethod;
        this._proxyMethod = proxyMethod;
        this._targetPtr = this.GetFunctionAddr(this._targetMethod);
        this._replacementPtr = this.GetFunctionAddr(this._replacementMethod);
        if (proxyMethod != null)
        {
            this._proxyPtr = this.GetFunctionAddr(this._proxyMethod);
        }
        this._jmpBuff = new byte[MethodHook.s_jmpBuff.Length];
    }

    // Token: 0x06000009 RID: 9 RVA: 0x000021FF File Offset: 0x000003FF
    // 安装Hook
    public void Install()
    {
        if (LDasm.IsiOS())
        {
            return;
        }
        if (this.isHooked)
        {
            return;
        }
        HookPool.AddHooker(this._targetMethod, this);
        this.InitProxyBuff();
        this.BackupHeader();
        this.PatchTargetMethod();
        this.PatchProxyMethod();
        this.isHooked = true;
    }

    // Token: 0x0600000A RID: 10 RVA: 0x00002240 File Offset: 0x00000440
    // 卸载Hook，恢复原始方法
    public unsafe void Uninstall()
    {
        if (!this.isHooked)
        {
            return;
        }
        byte* ptr = (byte*)this._targetPtr.ToPointer();
        for (int i = 0; i < this._proxyBuff.Length; i++)
        {
            *(ptr++) = this._proxyBuff[i];
        }
        this.isHooked = false;
        HookPool.RemoveHooker(this._targetMethod);
    }

    // Token: 0x0600000B RID: 11 RVA: 0x00002298 File Offset: 0x00000498
    // 初始化代理缓冲区
    private unsafe void InitProxyBuff()
    {
        byte* ptr = (byte*)this._targetPtr.ToPointer();
        uint num = LDasm.SizeofMinNumByte((void*)ptr, MethodHook.s_jmpBuff.Length);
        this._proxyBuff = new byte[num];
        this.EnableAddrModifiable(this._targetPtr, num);
    }

    // Token: 0x0600000C RID: 12 RVA: 0x000022D8 File Offset: 0x000004D8
    // 备份目标方法的原始头部字节
    private unsafe void BackupHeader()
    {
        byte* ptr = (byte*)this._targetPtr.ToPointer();
        for (int i = 0; i < this._proxyBuff.Length; i++)
        {
            this._proxyBuff[i] = *(ptr++);
        }
    }

    // Token: 0x0600000D RID: 13 RVA: 0x00002314 File Offset: 0x00000514
    // 修补目标方法，写入跳转到替换方法的指令
    private unsafe void PatchTargetMethod()
    {
        Array.Copy(MethodHook.s_jmpBuff, this._jmpBuff, this._jmpBuff.Length);
        fixed (byte* ptr = &this._jmpBuff[MethodHook.s_addrOffset])
        {
            byte* ptr2 = ptr;
            if (IntPtr.Size == 4)
            {
                *(int*)ptr2 = this._replacementPtr.ToInt32();
            }
            else
            {
                *(long*)ptr2 = this._replacementPtr.ToInt64();
            }
        }
        byte* ptr3 = (byte*)this._targetPtr.ToPointer();
        if (ptr3 != null)
        {
            int i = 0;
            int num = this._jmpBuff.Length;
            while (i < num)
            {
                *(ptr3++) = this._jmpBuff[i];
                i++;
            }
        }
    }

    // Token: 0x0600000E RID: 14 RVA: 0x000023AC File Offset: 0x000005AC
    // 修补代理方法，使其可以调用原始方法
    private unsafe void PatchProxyMethod()
    {
        if (this._proxyPtr == IntPtr.Zero)
        {
            return;
        }
        this.EnableAddrModifiable(this._proxyPtr, (uint)this._proxyBuff.Length);
        byte* ptr = (byte*)this._proxyPtr.ToPointer();

        // 写入原始方法头部字节
        for (int i = 0; i < this._proxyBuff.Length; i++)
        {
            *(ptr++) = this._proxyBuff[i];
        }

        // 计算跳转地址并写入跳转指令
        fixed (byte* ptr2 = &this._jmpBuff[MethodHook.s_addrOffset])
        {
            byte* ptr3 = ptr2;
            if (IntPtr.Size == 4)
            {
                // 32位平台：使用整数运算计算跳转地址
                int targetAddr = this._targetPtr.ToInt32();
                int offset = this._proxyBuff.Length;
                *(int*)ptr3 = targetAddr + offset;
            }
            else
            {
                // 64位平台：使用长整数运算计算跳转地址
                long targetAddr = this._targetPtr.ToInt64();
                long offset = (long)this._proxyBuff.Length;
                *(long*)ptr3 = targetAddr + offset;
            }
        }

        // 写入跳转指令
        for (int j = 0; j < this._jmpBuff.Length; j++)
        {
            *(ptr++) = this._jmpBuff[j];
        }
    }

    // Token: 0x0600000F RID: 15 RVA: 0x00002484 File Offset: 0x00000684
    // 启用内存地址的可修改权限
    private void EnableAddrModifiable(IntPtr ptr, uint size)
    {
        if (!LDasm.IsIL2CPP())
        {
            return;
        }
        uint num;
        IL2CPPHelper.VirtualProtect(ptr, size, IL2CPPHelper.Protection.PAGE_EXECUTE_READWRITE, out num);
    }

    // Token: 0x06000010 RID: 16 RVA: 0x000024A8 File Offset: 0x000006A8
    // 获取方法的本地函数地址
    private unsafe IntPtr GetFunctionAddr(MethodBase method)
    {
        if (!LDasm.IsIL2CPP())
        {
            return method.MethodHandle.GetFunctionPointer();
        }

        // IL2CPP环境下获取方法地址的特殊处理
        // 创建临时结构体并固定在内存中
        MethodHook.__ForCopy forCopy = new MethodHook.__ForCopy();
        forCopy.method = method;

        long* ptr = &forCopy.__dummy;
        ptr++; // 指向method字段后的位置

        IntPtr zero = IntPtr.Zero;

        if (sizeof(IntPtr) == 8)
        {
            // 64位平台
            long ptrValue = *ptr;
            long* secondPtr = (long*)(ptrValue + sizeof(IntPtr) * 2);
            long finalAddr = *secondPtr;
            zero = new IntPtr(finalAddr);
        }
        else
        {
            // 32位平台
            int ptrValue = *(int*)ptr;
            int* secondPtr = (int*)(ptrValue + sizeof(IntPtr) * 2);
            int finalAddr = *secondPtr;
            zero = new IntPtr(finalAddr);
        }
        return zero;
    }

    // Token: 0x04000003 RID: 3
    private MethodBase _targetMethod;

    // Token: 0x04000004 RID: 4
    private MethodBase _replacementMethod;

    // Token: 0x04000005 RID: 5
    private MethodBase _proxyMethod;

    // Token: 0x04000006 RID: 6
    private IntPtr _targetPtr;

    // Token: 0x04000007 RID: 7
    private IntPtr _replacementPtr;

    // Token: 0x04000008 RID: 8
    private IntPtr _proxyPtr;

    // Token: 0x04000009 RID: 9
    private static readonly byte[] s_jmpBuff;

    // Token: 0x0400000A RID: 10
    // 32位平台的跳转指令模板
    private static readonly byte[] s_jmpBuff_32 = new byte[] { 104, 0, 0, 0, 0, 195 };

    // Token: 0x0400000B RID: 11
    private static readonly byte[] s_jmpBuff_64;

    // Token: 0x0400000C RID: 12
    private static readonly byte[] s_jmpBuff_arm32_arm;

    // Token: 0x0400000D RID: 13
    private static readonly byte[] s_jmpBuff_arm32_thumb;

    // Token: 0x0400000E RID: 14
    private static readonly byte[] s_jmpBuff_arm64;

    // Token: 0x0400000F RID: 15
    private static readonly int s_addrOffset;

    // Token: 0x04000010 RID: 16
    private byte[] _jmpBuff;

    // Token: 0x04000011 RID: 17
    private byte[] _proxyBuff;

    // Token: 0x02000023 RID: 35
    // 用于IL2CPP环境下获取方法地址的辅助结构
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    private struct __ForCopy
    {
        // Token: 0x0400009B RID: 155
        public long __dummy;

        // Token: 0x0400009C RID: 156
        public MethodBase method;
    }
}

