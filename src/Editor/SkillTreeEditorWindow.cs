using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

public class SkillTreeEditorWindow : EditorWindow {
	Vector2 scrollPos;

	SkillTreeDatabase database = ScriptableObject.CreateInstance<SkillTreeDatabase> ();

	int inID = -1, outID = -1;
	int removeInID = -1, removeOutID = -1;

	[MenuItem("Skill Tree Editor/Editor Window")]
	static void ShowEditor() {
		SkillTreeEditorWindow editor = EditorWindow.GetWindow<SkillTreeEditorWindow>("SkillTree Editor");
		editor.Init ();
	}

	public void Init () {
		int id = database.AddNode (IDIncrementMode.Min, new Rect (50, 50, 300, 425));
		database.ChangeRootNode (id);
	}

	void OnGUI () {
		try {
			if (this.inID != this.outID && this.inID >= 0 && this.outID >= 0) {
				database.AddConnection (this.outID, this.inID);
				this.outID = -1;
				this.inID = -1;
			}

			if (this.removeInID != this.removeOutID && this.removeInID >= 0 && this.removeOutID >= 0) {
				database.RemoveConnection (this.removeOutID, this.removeInID);
				this.removeOutID = -1;
				this.removeInID = -1;
			}

			GUILayout.BeginHorizontal ();

			if (GUILayout.Button("Save Database", GUILayout.Width (100), GUILayout.Height (20))) {
				string path = EditorUtility.SaveFilePanelInProject ("Save Tree", "Tree", "asset", "Save your tree");
				if (path != null && path != "") {
					if (!path.EndsWith (".asset"))
						path += ".asset";
					EditorUtility.SetDirty (database);
					if (!AssetDatabase.Contains(database))
						AssetDatabase.CreateAsset (database, path);
					AssetDatabase.SaveAssets ();
					AssetDatabase.Refresh ();
				}
			}
			if (GUILayout.Button("Load Database", GUILayout.Width (100), GUILayout.Height (20))) {
				string path = EditorUtility.OpenFilePanel ("Open Tree", "Asset", "asset");
				if (path.StartsWith(Application.dataPath)) {
					path = "Assets" + path.Substring(Application.dataPath.Length);
				}
				if (path != null && path != "") {
					database = (SkillTreeDatabase)AssetDatabase.LoadAssetAtPath(path, typeof(SkillTreeDatabase));
				}
			}

			if (GUILayout.Button("Add New Node", GUILayout.Width (100), GUILayout.Height (20))) {
				database.AddNode (IDIncrementMode.Min, new Rect (50, 50, 300, 425));
			}

			GUILayout.EndHorizontal ();

			database.Element = (TreeElement)EditorGUILayout.EnumPopup (database.Element, GUILayout.Width (500));

			scrollPos = GUI.BeginScrollView (new Rect (0, 0, position.width, position.height), scrollPos, new Rect (0, 0, 10000, 10000));
			BeginWindows ();

			foreach (SkillTreeGraph tree in database.GetSkillTree) {
				tree.Window = GUI.Window ((int)tree.GetID, tree.Window, DrawWindow, "Node ID " + tree.GetID.ToString ());
				foreach (int ids in database[tree.GetID].GetOutNodesIds)
					DrawNodeCurve (tree.Window, database[ids].Window);
			}
			
			EndWindows ();
			GUI.EndScrollView ();
		} catch (Exception e) {
			Debug.Log (e.Message + " " + e.Source);
		}
	}

	void DrawWindow (int id) {
		SkillTreeGraph tree = database[id];
		if (EditorGUILayout.Toggle ("Root Node", database.GetRootID () == id)) {
			database.ChangeRootNode ((int)id);
		}

		//NAME
		GUILayout.BeginHorizontal ();

		GUILayout.Label ("Name: ");
		database[id].Node.Name = EditorGUILayout.TextField (tree.Node.Name, GUILayout.MaxWidth (tree.Window.width / 2));

		GUILayout.EndHorizontal ();

		database[id].Node.Comment = GUILayout.TextArea (tree.Node.Comment, GUILayout.MaxHeight (50), GUILayout.Height (30));

		//ANIMATOR STATE
		GUILayout.BeginHorizontal ();
		
		GUILayout.Label ("Anim state: ");
		database[id].Node.AnimatorState = EditorGUILayout.IntField (tree.Node.AnimatorState, GUILayout.MaxWidth (tree.Window.width / 2));
		
		GUILayout.EndHorizontal ();

		//PERK LEVEL
		GUILayout.BeginHorizontal ();
		
		GUILayout.Label ("Perk level: ");
		database[id].Node.PerkLevel = EditorGUILayout.IntField (tree.Node.PerkLevel, GUILayout.MaxWidth (tree.Window.width / 2));
		
		GUILayout.EndHorizontal ();

		//PERK LOGO
		GUILayout.BeginHorizontal ();
		
		GUILayout.Label ("Perk logo: ");
		database[id].Node.PerkLogo = (Texture2D)EditorGUILayout.ObjectField (tree.Node.PerkLogo, typeof (Texture2D), false, GUILayout.MaxWidth (tree.Window.width / 2));
		
		GUILayout.EndHorizontal ();

		//LIFE
		GUILayout.BeginHorizontal ();
		
		GUILayout.Label ("Attack type: ");
		database[id].Node.Attack = (AttackType)EditorGUILayout.EnumPopup (tree.Node.Attack, GUILayout.MaxWidth (tree.Window.width / 2));
		
		GUILayout.EndHorizontal ();

		//LIFE
		GUILayout.BeginHorizontal ();
		
		GUILayout.Label ("Life: ");
		database[id].Node.TowerLife = EditorGUILayout.FloatField (tree.Node.TowerLife, GUILayout.MaxWidth (tree.Window.width / 2));
		
		GUILayout.EndHorizontal ();

		//ELEMENTAL PROB
		GUILayout.BeginHorizontal ();
		
		GUILayout.Label ("El. Prob.: ");
		database[id].Node.ElementalDamageProb = EditorGUILayout.Slider (tree.Node.ElementalDamageProb, 0, 100, GUILayout.MaxWidth (tree.Window.width / 2));
		
		GUILayout.EndHorizontal ();

		//ELEMENTAL DAMAGE PER SEC
		GUILayout.BeginHorizontal ();
		
		GUILayout.Label ("Elem. Dam. per sec.: ");
		database[id].Node.ElementalDamagePerSecond = EditorGUILayout.FloatField (tree.Node.ElementalDamagePerSecond, GUILayout.MaxWidth (tree.Window.width / 2));
		
		GUILayout.EndHorizontal ();

		//COOLDOWN
		GUILayout.BeginHorizontal ();
		
		GUILayout.Label ("Cooldown: ");
		database[id].Node.Cooldown = EditorGUILayout.FloatField (tree.Node.Cooldown, GUILayout.MaxWidth (tree.Window.width / 2));
		
		GUILayout.EndHorizontal ();

		//DAMAGE MULTIPLIER
		GUILayout.BeginHorizontal ();
		
		GUILayout.Label ("Damage Mult.: ");
		database[id].Node.DamageMultiplier = EditorGUILayout.FloatField (tree.Node.DamageMultiplier, GUILayout.MaxWidth (tree.Window.width / 2));
		
		GUILayout.EndHorizontal ();

		if (database[id].Node.Attack == AttackType.Balistic) {
			DrawBalisticNode (id, tree);
		} else if (database[id].Node.Attack == AttackType.Area) {
			DrawAreaNode (id, tree);
		} else if (database[id].Node.Attack == AttackType.Mixed) {
			DrawMixedNode (id, tree);
		} else {
			DrawBaseNode (id, tree);
		}

		//NODE
		GUILayout.BeginHorizontal ();
		if (GUILayout.Button("In")) {
			this.inID = (int)id;
		}
		GUILayout.Space (20);
		if (GUILayout.Button("Out")) {
			this.outID = (int)id;
		}
		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal ();
		if (GUILayout.Button("Remove In")) {
			this.removeInID = (int)id;
		}
		GUILayout.Space (20);
		if (GUILayout.Button("Remove Out")) {
			this.removeOutID = (int)id;
		}
		GUILayout.EndHorizontal ();
		if (GUILayout.Button("Remove Node")) {
			database.RemoveNode ((int)id);
		}
		GUI.DragWindow();
	}

	void DrawBaseNode (int id, SkillTreeGraph tree) {
		DrawMixedNode (id, tree);
	}

	void DrawAreaNode (int id, SkillTreeGraph tree) {

		//DAMAGE PER SECOND
		GUILayout.BeginHorizontal ();
		
		GUILayout.Label ("Damage per sec.: ");
		database[id].Node.AttackDamagePerSecond = EditorGUILayout.FloatField (tree.Node.AttackDamagePerSecond, GUILayout.MaxWidth (tree.Window.width / 2));
		
		GUILayout.EndHorizontal ();

		//AREA
		GUILayout.BeginHorizontal ();
		
		GUILayout.Label ("Radius of area: ");
		database[id].Node.Radius = EditorGUILayout.FloatField (tree.Node.Radius, GUILayout.MaxWidth (tree.Window.width / 2));
		
		GUILayout.EndHorizontal ();
	}

	void DrawMixedNode (int id, SkillTreeGraph tree) {
		DrawAreaNode (id, tree);
		DrawBalisticNode (id, tree);
	}
	void DrawBalisticNode (int id, SkillTreeGraph tree) {
		GUILayout.BeginHorizontal ();
		
		GUILayout.Label ("Proj. speed: ");
		database[id].Node.ProjectileSpeed = EditorGUILayout.FloatField (tree.Node.ProjectileSpeed, GUILayout.MaxWidth (tree.Window.width / 2));
		
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		
		GUILayout.Label ("Hit damage: ");
		database[id].Node.AttackDamage = EditorGUILayout.FloatField (tree.Node.AttackDamage, GUILayout.MaxWidth (tree.Window.width / 2));
		
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		
		GUILayout.Label ("Critical prob.: ");
		database[id].Node.CriticalHitProb = EditorGUILayout.Slider (tree.Node.CriticalHitProb, 0, 100, GUILayout.MaxWidth (tree.Window.width / 2));
		
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		
		GUILayout.Label ("Critical hit mult.: ");
		database[id].Node.CriticalHitDamageMultiplier = EditorGUILayout.FloatField (tree.Node.CriticalHitDamageMultiplier, GUILayout.MaxWidth (tree.Window.width / 2));

		GUILayout.EndHorizontal ();
	}


	void DrawNodeCurve(Rect start, Rect end) {
		Vector3 startPos = new Vector3(start.x + start.width, start.y + start.height / 2, 0);
		Vector3 endPos = new Vector3(end.x, end.y + end.height / 2, 0);
		Vector3 startTan = startPos + Vector3.right * 50;
		Vector3 endTan = endPos + Vector3.left * 50;
		Color shadowCol = new Color(0, 0, 0, 0.06f);
		
		for (int i = 0; i < 3; i++) {
			Handles.DrawBezier (startPos, endPos, startTan, endTan, shadowCol, null, (i + 1) * 5);
		}
		
		Handles.DrawBezier (startPos, endPos, startTan, endTan, Color.black, null, 1);
	}

}
