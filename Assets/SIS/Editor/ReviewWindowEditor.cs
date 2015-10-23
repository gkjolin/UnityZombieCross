﻿using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace SIS
{
    [InitializeOnLoad]
    public class ReviewWindowEditor : EditorWindow
    {
        private static ReviewConfig currentConfig;
		private static Texture2D reviewWindowImage;
		private static string fileName = "RatingSetup";
		private static string imagePath = "/EditorFiles/Asset_smallLogo.png";

		
        static ReviewWindowEditor()
        {
			EditorApplication.update += Startup;
		}
		
		
		static void Startup()
		{
			EditorApplication.update -= Startup;
			
			if (ReviewWindowEditor.Current == null)
                return;
			
			Count();
		}


        [MenuItem("Window/Simple IAP System/Review")]
        static void Init()
        {
            EditorWindow.GetWindowWithRect(typeof(ReviewWindowEditor), new Rect(0, 0, 256, 260), false, "Review Window");
        }


        void OnGUI()
        {		
			if(reviewWindowImage == null)
			{
				var script = MonoScript.FromScriptableObject(this);
				string path = Path.GetDirectoryName(AssetDatabase.GetAssetPath(script));
				reviewWindowImage = AssetDatabase.LoadAssetAtPath(path + imagePath, typeof(Texture2D)) as Texture2D;
			}

            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(30);
            EditorGUILayout.BeginVertical();
			GUILayout.Label(reviewWindowImage);
            EditorGUILayout.LabelField("Review Simple IAP System");
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.Space();
            EditorGUILayout.LabelField("Please consider giving us a rating on the");
            EditorGUILayout.LabelField("Unity Asset Store. Your support helps us");
			EditorGUILayout.LabelField("to improve this product. Thank you!");

			GUILayout.Space(20);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Rate now!"))
            {
                Help.BrowseURL("https://www.assetstore.unity3d.com/#!/content/22628");
                DisableRating();
            }
            if (GUILayout.Button("Later"))
            {
				ReviewWindowEditor.Current.counter = 5;
				this.Close();
            }
			if (GUILayout.Button("Never"))
            {
                DisableRating();
            }
            EditorGUILayout.EndHorizontal();
        }
		
		
		static void Count()
		{
			if(!ReviewWindowEditor.Current.active) return;
			
			double time = GenerateUnixTime();
			double diff = time - ReviewWindowEditor.Current.lastCheck;
			if(diff < 20) return;
			
			ReviewWindowEditor.Current.lastCheck = time;
			ReviewWindowEditor.Current.counter++;
			
			if(ReviewWindowEditor.Current.counter > 10)
				Init();
			
            ReviewWindowEditor.Save();
		}
		
		
        static double GenerateUnixTime()
        {
            var epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
            return (System.DateTime.UtcNow - epochStart).TotalHours;
        }
	

        void DisableRating()
        {
            ReviewWindowEditor.Current.active = false;
			ReviewWindowEditor.Current.counter = 0;
            ReviewWindowEditor.Save();
			this.Close();
        }


        public static ReviewConfig Current
        {
            get
            {
                if (currentConfig == null)
                {
                    currentConfig = Resources.Load(fileName, typeof(ReviewConfig)) as ReviewConfig;
                }

                return currentConfig;
            }
            set
            {
                currentConfig = value;
            }
        }


        public static void Save()
        {
            EditorUtility.SetDirty(ReviewWindowEditor.Current);
        }
    }
}