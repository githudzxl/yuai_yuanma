using System;

// Token: 0x02000003 RID: 3
public static class IL2CPPHelper
{
	// Token: 0x06000004 RID: 4 RVA: 0x0000209D File Offset: 0x0000029D
	public static bool VirtualProtect(IntPtr lpAddress, uint dwSize, IL2CPPHelper.Protection flNewProtect, out uint lpflOldProtect)
	{
		lpflOldProtect = 0U;
		return false;
	}

	// Token: 0x02000022 RID: 34
	public enum Protection
	{
		// Token: 0x04000090 RID: 144
		PAGE_NOACCESS = 1,
		// Token: 0x04000091 RID: 145
		PAGE_READONLY,
		// Token: 0x04000092 RID: 146
		PAGE_READWRITE = 4,
		// Token: 0x04000093 RID: 147
		PAGE_WRITECOPY = 8,
		// Token: 0x04000094 RID: 148
		PAGE_EXECUTE = 16,
		// Token: 0x04000095 RID: 149
		PAGE_EXECUTE_READ = 32,
		// Token: 0x04000096 RID: 150
		PAGE_EXECUTE_READWRITE = 64,
		// Token: 0x04000097 RID: 151
		PAGE_EXECUTE_WRITECOPY = 128,
		// Token: 0x04000098 RID: 152
		PAGE_GUARD = 256,
		// Token: 0x04000099 RID: 153
		PAGE_NOCACHE = 512,
		// Token: 0x0400009A RID: 154
		PAGE_WRITECOMBINE = 1024
	}
}
