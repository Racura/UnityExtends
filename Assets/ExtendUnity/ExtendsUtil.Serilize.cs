using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;

public static partial class ExtendsUtil {

	public static string GetPath(string filename)
	{
		Init();
		return Path.Combine(Application.persistentDataPath, filename);
	}
	
	
	static bool _init;
	public static void Init()
	{
		if(_init) return;
		
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			System.Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
		} 
		
		
		_init = true;
	}
	
	
	public static T BinaryDeserialize<T>(string path){
		
		Init();
		using (FileStream fs = new FileStream (path, FileMode.Open)) {
			
			BinaryFormatter formatter = new BinaryFormatter ();
			return (T)formatter.Deserialize (fs);
		}
	}
	
	public static void BinarySerialize<T>(T value, string path){
		
		Init();
		using (FileStream fs = new FileStream (path, FileMode.Create)) {
			
			BinaryFormatter formatter = new BinaryFormatter ();
			formatter.Serialize (fs, value);
		}
	}
}