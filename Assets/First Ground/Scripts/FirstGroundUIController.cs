﻿// First Ground version: 1.3.1
// Author: Gold Experience Team (http://www.ge-team.com/)
// Support: geteamdev@gmail.com
// Please direct any bugs/comments/suggestions to geteamdev@gmail.com

#region Namespaces

using UnityEngine;
using System.Collections;

using UnityEngine.UI;

#endregion //Namespaces

/***************
* FirstGroundUIController class
* This class handles user key inputs.
**************/

public class FirstGroundUIController : MonoBehaviour {
	
	#region Variables

		// Canvas
		public Canvas		m_Canvas									= null;
	
		// SelectDemo
		public Button		m_SelectDemo_Button							= null;
		public GameObject	m_SelectDemo_Window							= null;
	
		// Help
		public Button		m_Help_Button								= null;
		public GameObject	m_Help_Window								= null;

		// HowTo
		public GameObject	m_HowTo										= null;
	
	#endregion
	
	// ######################################################################
	// MonoBehaviour Functions
	// ######################################################################

	#region MonoBehaviour

	// Use this for initialization
	void Awake () {
		//////////////////////////////////////////////////////////////////////
		// If GUIAnimSystemFREE.Instance.m_AutoAnimation is false, all GEAnim elements in the scene will be controlled by scripts.
		// Otherwise, they will be animated automatically.
		//////////////////////////////////////////////////////////////////////
		if (enabled)
		{
			GUIAnimSystemFREE.Instance.m_GUISpeed = 4.0f;
			GUIAnimSystemFREE.Instance.m_AutoAnimation = false;
		}
	}

	// Use this for initialization
	void Start () {

		// Play UI move-in animations
		StartCoroutine(ShowUIs());
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	#endregion //MonoBehaviour
	
	// ######################################################################
	// Functions Functions
	// ######################################################################

	#region Functions

	// Play UI move-in animations
	IEnumerator ShowUIs()
	{
		GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(m_Canvas, false);

		yield return new WaitForSeconds(0.5f);
		
		GUIAnimSystemFREE.Instance.MoveIn(m_SelectDemo_Button.transform, true);

		yield return new WaitForSeconds(0.25f);

		GUIAnimSystemFREE.Instance.MoveIn(m_Help_Button.transform, true);

		yield return new WaitForSeconds(0.25f);

		GUIAnimSystemFREE.Instance.MoveIn(m_HowTo.transform, true);

		GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(m_Canvas, true);
	}
		
	#endregion //Functions
	
	// ######################################################################
	// UI Respond Functions
	// ######################################################################

	#region UI Respond Functions

		public void Button_SelectDemo()
		{
			GUIAnimSystemFREE.Instance.MoveOut(m_SelectDemo_Button.transform, true);
			GUIAnimSystemFREE.Instance.MoveIn(m_SelectDemo_Window.transform, true);
		}

		public void Button_SelectDemo_Minimize()
		{
			GUIAnimSystemFREE.Instance.MoveIn(m_SelectDemo_Button.transform, true);
			GUIAnimSystemFREE.Instance.MoveOut(m_SelectDemo_Window.transform, true);
		}

		public void Button_SelectDemo_Arctic()
		{
			GUIAnimSystemFREE.Instance.LoadLevel("Arctic",0.1f);
		}

		public void Button_SelectDemo_Grassland()
		{
			GUIAnimSystemFREE.Instance.LoadLevel("Grassland",0.1f);
		}

		public void Button_SelectDemo_Volcano()
		{
			GUIAnimSystemFREE.Instance.LoadLevel("Volcano",0.1f);
		}

		public void Button_SelectDemo_Wasteland()
		{
			GUIAnimSystemFREE.Instance.LoadLevel("Wasteland",0.1f);
		}

		public void Button_Help()
		{ 
			GUIAnimSystemFREE.Instance.MoveOut(m_Help_Button.transform, true);
			GUIAnimSystemFREE.Instance.MoveIn(m_Help_Window.transform, true);
		}

		public void Button_Help_Minimize()
		{
			GUIAnimSystemFREE.Instance.MoveIn(m_Help_Button.transform, true);
			GUIAnimSystemFREE.Instance.MoveOut(m_Help_Window.transform, true);
		}

		public void Button_Help_Support()
		{ 
			Application.OpenURL("mailto:geteamdev@gmail.com");
		}

		public void Button_Help_Products()
		{
			Application.ExternalEval("window.open('http://ge-team.com/pages/unity-3d/','GOLD EXPERIENCE TEAM')");
		}
		
	#endregion //UI Respond Functions
}
