using System;
using System.Reflection;
using EyeRecall.Helper;
using UnityEngine;

namespace EyeRecall.Function
{
	// Token: 0x02000015 RID: 21
	[Obfuscation(Exclude = true)]
	internal class MenuThread : MonoBehaviour
	{
		// Token: 0x06000075 RID: 117 RVA: 0x000069D4 File Offset: 0x00004BD4
		[Obfuscation(Exclude = true)]
		private void Start()
		{
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000069D8 File Offset: 0x00004BD8
		[Obfuscation(Exclude = true)]
		private void OnGUI()
		{
			this.menuId = 777;
			this.menuRect = drawMenu.menuRectGlobal;
			if (drawMenu.showMenu)
			{
				float num = 10f;
				Rect rect = new Rect(this.menuRect.x - num / 2f, this.menuRect.y + num / 2f, this.menuRect.width + num, this.menuRect.height + num);
				GUI.color = new Color(0f, 0f, 0f, 0.4f);
				GUI.DrawTexture(rect, Texture2D.whiteTexture);
				GUI.color = Color.white;
				this.menuRect = GUILayout.Window(this.menuId, this.menuRect, new GUI.WindowFunction(drawMenu.run), "", new GUILayoutOption[0]);
			}
			if (CheatThread._Visual.Enable && CheatThread._Visual.showAimRange)
			{
				GUI.color = Color.white;
				GizmosProGraph.DrawScreenEllipse(new Vector2((float)Screen.width / 2f, (float)Screen.height / 2f), (float)CheatThread._aimBot.aimRange, (float)CheatThread._aimBot.aimRange, Color.white, 50);
				GUI.color = Color.white;
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00006B1B File Offset: 0x00004D1B
		public MenuThread()
		{
		}

		// Token: 0x0400006F RID: 111
		public int menuId;

		// Token: 0x04000070 RID: 112
		public Rect menuRect;
	}
}
