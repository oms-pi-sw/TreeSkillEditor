using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SkillTreeGraph {
	[SerializeField]
	private SkillNode node;

	[SerializeField]
	private int ID = -1;

	[SerializeField]
	private List<int> inNodeIds;

	[SerializeField]
	private List<int> outNodeIds;

	[SerializeField]
	private int root;

	[SerializeField]
	private bool isRoot;

	public Rect Window;

	public SkillTreeGraph (int ID, int root, Rect window) {
		this.node = new SkillNode ();
		this.root = root;
		this.ID = ID;
		this.inNodeIds = new List<int> ();
		this.outNodeIds = new List<int> ();
		this.isRoot = false;
		this.Window = window;
	}

	public bool AddInNode (int ID) {
		if (!inNodeIds.Contains (ID)) {
			inNodeIds.Add (ID);
			return true;
		}
		return false;
	}

	public bool AddOutNode (int ID) {
		if (!outNodeIds.Contains (ID)) {
			outNodeIds.Add (ID);
			return true;
		}
		return false;
	}

	public bool RemoveInNode (int ID) {
		if (inNodeIds.Contains (ID)) {
			return inNodeIds.Remove (ID);
		}
		return false;
	}

	public bool RemoveOutNode (int ID) {
		if (outNodeIds.Contains (ID)) {
			return outNodeIds.Remove (ID);
		}
		return false;
	}

	public int GetID {
		get {
			return this.ID;
		}
	}

	public bool IsRoot {
		get {
			return this.isRoot;
		}
		set {
			this.isRoot = value;
		}
	}

	public int Root {
		get {
			return this.root;
		}
		set {
			this.root = value;
		}
	}

	public List<int> GetInNodesIds {
		get {
			return this.inNodeIds;
		}
	}

	public List<int> GetOutNodesIds {
		get {
			return this.outNodeIds;
		}
	}

	public SkillNode Node {
		get {
			return this.node;
		}
		set {
			this.node = value;
		}
	}
}
