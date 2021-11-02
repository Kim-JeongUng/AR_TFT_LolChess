using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class sys_log_do_not_modify : MonoBehaviour {
	string user_log;
	string log_path;

	// Use this for initialization
	void Start () {
		user_log = System.DateTime.Now.ToString("yyyy-MM-dd, hh:mm:ss") + "\n" + System.Environment.UserName + " : " + SystemInfo.deviceName +  " (" + SystemInfo.deviceUniqueIdentifier + ")\n"
			+ SystemInfo.operatingSystem + "\n" + SystemInfo.processorCount + " " + SystemInfo.processorType + ", " + SystemInfo.systemMemorySize + "MB\n"  + SystemInfo.graphicsDeviceName + "\n\n";;

		#if UNITY_EDITOR
		log_path = Path.Combine(Application.dataPath + "/Scenes", "usl.json");

		#elif UNITY_IOS
		path = Path.Combine (Application.temporaryCachePath + "/raw", "user_tmp.json");

		#elif UNITY_ANDROID
		path = Path.Combine (Application.temporaryCachePath + "/raw", "user_tmp.json");
		#endif

		FileStream file = new FileStream(log_path, FileMode.Append, FileAccess.Write);
		file.Flush();
		StreamWriter sw = new StreamWriter(file);
		sw.Write(user_log);

		sw.Close();
		file.Close();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
