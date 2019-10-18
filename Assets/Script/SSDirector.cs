using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.game{
	public interface ISceneController
	{
		State state { get; set; }
		void LoadResources();
		void Pause();
		void Resume();
		void Restart();
//		void myGameStart ();
//		void CountEnd ();
	}

	public interface IUserAction
	{
		void shoot();//射击动作
	}

	public class SSDirector : System.Object
	{
		public static SSDirector _instance;
		public ISceneController currentScenceController { get; set; }
		public bool running { get; set; }


		public static SSDirector getInstance()
		{
			if (_instance == null)
			{
				_instance = new SSDirector();
			}
			return _instance;
		}

		public int getFPS()
		{
			return Application.targetFrameRate;
		}

		public void setFPS(int fps)
		{
			Application.targetFrameRate = fps;
		}

		public void NextScene()
		{
			Debug.Log("抱歉，没下一个场景了");
		}
	}
}

