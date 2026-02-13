using System;
using System.IO;

namespace EyeRecall.Helper
{
	// Token: 0x0200000C RID: 12
	internal class LogManager
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00003CCC File Offset: 0x00001ECC
		public static void WriteLog(string log)
		{
			try
			{
				string text = "C:\\";
				if (!Directory.Exists(text + "\\cheat"))
				{
					Directory.CreateDirectory(text + "\\cheat");
				}
				text = string.Concat(new object[]
				{
					text,
					"\\cheat\\",
					DateTime.Now.Month,
					"-",
					DateTime.Now.Day,
					"-",
					DateTime.Now.Hour,
					"-",
					"run.log"
				});
				StreamWriter streamWriter = new StreamWriter(text, true);
				streamWriter.WriteLine(string.Format("[{0}] {1}", DateTime.Now.ToString(), log));
				streamWriter.Close();
			}
			catch
			{
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003DBC File Offset: 0x00001FBC
		public static void WritePassAntiCheatLog(string log)
		{
			try
			{
				string text = "C:\\";
				if (!Directory.Exists(text + "\\passCheat"))
				{
					Directory.CreateDirectory(text + "\\passCheat");
				}
				text = string.Concat(new object[]
				{
					text,
					"\\passCheat\\",
					DateTime.Now.Month,
					"-",
					DateTime.Now.Day,
					"-",
					DateTime.Now.Hour,
					"-",
					"debug.log"
				});
				StreamWriter streamWriter = new StreamWriter(text, true);
				streamWriter.WriteLine(string.Format("[{0}] {1}", DateTime.Now.ToString(), log));
				streamWriter.Close();
			}
			catch
			{
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003EAC File Offset: 0x000020AC
		public LogManager()
		{
		}
	}
}
