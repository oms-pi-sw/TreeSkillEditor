using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum IDIncrementMode {
	Max, Min
}

public enum TreeElement {
	NotElemental, Fire, Ice, Laser, Lightning, Eearth, Corrosive, Air, Explosive
}

[Serializable]
public class SkillTreeDatabase : ScriptableObject {
	[SerializeField]
	private List<SkillTreeGraph> SkillTree = new List<SkillTreeGraph> ();

	[SerializeField]
	private TreeElement treeElement = TreeElement.NotElemental;

	public int GetMaxID () {
		int maxID = -1;
		foreach (SkillTreeGraph tree in SkillTree)
			if (tree.GetID > maxID)
				maxID = tree.GetID;
		return maxID;
	}
	public int GetNewMaxID () {
		return GetMaxID () + 1;
	}
	public int GetNewMinID () {
		List<int> ids = new List<int> ();
		int newMaxID = GetNewMaxID ();
		for (int i = 0; i <= newMaxID; i++)
			ids.Add (i);
		foreach (SkillTreeGraph tree in SkillTree)
			ids.Remove (tree.GetID);
		ids.Sort ();
		return ids[0];
	}

	public int GetRootID () {
		foreach (SkillTreeGraph tree in SkillTree)
			if (tree.IsRoot)
				return tree.GetID;
		return (int)-1;
	}

	public int AddNode (IDIncrementMode mode, Rect window) {
		if (mode == IDIncrementMode.Max) {
			int maxID = this.GetNewMaxID ();
			if (SkillTree.Find (skg => skg.GetID == maxID) == null) {
				SkillTree.Add (new SkillTreeGraph (maxID, this.GetRootID (), window));
				return maxID;
			}
		} else {
			int minID = this.GetNewMinID ();
			if (SkillTree.Find (skg => skg.GetID == minID) == null) {
				SkillTree.Add (new SkillTreeGraph (minID, this.GetRootID (), window));
				return minID;
			}
		}
		return (int)-1;
	}

	public void RemoveNode (int nodeID) {
		SkillTreeGraph node = SkillTree.Find (skg => skg.GetID == nodeID);
		foreach (int ids in node.GetInNodesIds) {
			SkillTreeGraph inNode = SkillTree.Find (skg => skg.GetID == ids);
			inNode.RemoveOutNode (nodeID);
		}
		SkillTree.Remove (node);
	}

	public void ChangeRootNode (int newRootID) {
		foreach (SkillTreeGraph tree in SkillTree) {
			if (tree.GetID != newRootID) {
				tree.IsRoot = false;
				tree.Node.Selected = false;
			} else {
				tree.IsRoot = true;
				tree.Node.Selected = true;
			}
			tree.Root = newRootID;
		}
	}

	public void AddConnection (int outID, int inID) {
		SkillTreeGraph inTree = this.SkillTree.Find (skg => skg.GetID == inID);
		SkillTreeGraph outTree = this.SkillTree.Find (skg => skg.GetID == outID);

		inTree.AddInNode (outID);
		outTree.AddOutNode (inID);
	}

	public void RemoveConnection (int outID, int inID) {
		SkillTreeGraph inTree = this.SkillTree.Find (skg => skg.GetID == inID);
		SkillTreeGraph outTree = this.SkillTree.Find (skg => skg.GetID == outID);

		inTree.RemoveInNode (outID);
		outTree.RemoveOutNode (inID);
	}

	public void RemoveNode (SkillTreeGraph node) {
		RemoveNode (node.GetID);
	}

	public List<SkillTreeGraph> GetSkillTree {
		get {
			return this.SkillTree;
		}
	}

	public SkillTreeGraph this [int index] {
		get {
			return this.SkillTree.Find (skg => skg.GetID == index);
		}
	}

	public TreeElement Element {
		get {
			return this.treeElement;
		}
		set {
			this.treeElement = value;
		}
	}

	public SkillTreeGraph GetRoot {
		get {
			return this[this.GetRootID ()];
		}
	}
}
