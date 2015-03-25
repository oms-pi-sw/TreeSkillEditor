using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum AttackType {
	Base, Balistic, Area, Mixed
}

[Serializable]
public class SkillNode {
	[SerializeField]
	private string name = "";

	[SerializeField]
	private AttackType attackType = AttackType.Base;

	[SerializeField]
	private string comment = "";

	[SerializeField]
	private float elementalDamagePerSecond = 0;

	[SerializeField]
	private float radius = 0;

	[SerializeField]
	private float attackDamage = 0;

	[SerializeField]
	private float attackDamagePerSecond = 0;

	[SerializeField]
	private float elementalDamageProb = 0;

	[SerializeField]
	private float towerLife = 0;

	[SerializeField]
	private float cooldown = 0;

	[SerializeField]
	private float projectileSpeed = 0;

	[SerializeField]
	private float criticalHitProb = 0;

	[SerializeField]
	private float damageMultiplier = 1;

	[SerializeField]
	private float criticalHitDamageMultiplier = 0;

	[SerializeField]
	private int animatorState = 0;

	[SerializeField]
	private int perkLevel = 0;

	[SerializeField]
	private Texture2D perkLogo = null;

	[SerializeField]
	private bool selected = false;

	public SkillNode () {
		this.name = "";
	}

	public SkillNode (string name) {
		this.name = name;
	}

	public string Name {
		get {
			return this.name;
		}
		set {
			this.name = value;
		}
	}

	public AttackType Attack {
		get {
			return this.attackType;
		}
		set {
			this.attackType = value;
		}
	}

	public float AttackDamage {
		get {
			return this.attackDamage;
		}
		set {
			this.attackDamage = value;
		}
	}

	public float Radius {
		get {
			return this.radius;
		}
		set {
			this.radius = value;
		}
	}

	public float ElementalDamageProb {
		get {
			return this.elementalDamageProb;
		}
		set {
			this.elementalDamageProb = value;
		}
	}

	public float ElementalDamagePerSecond {
		get {
			return this.elementalDamagePerSecond;
		}
		set {
			this.elementalDamagePerSecond = value;
		}
	}

	public float TowerLife {
		get {
			return this.towerLife;
		}
		set {
			this.towerLife = value;
		}
	}

	public string Comment {
		get {
			return this.comment;
		}
		set {
			this.comment = value;
		}
	}

	public float Cooldown {
		get {
			return this.cooldown;
		}
		set {
			this.cooldown = value;
		}
	}

	public float ProjectileSpeed {
		get {
			return this.projectileSpeed;
		}
		set {
			this.projectileSpeed = value;
		}
	}

	public float CriticalHitProb {
		get {
			return this.criticalHitProb;
		}
		set {
			this.criticalHitProb = value;
		}
	}

	public float DamageMultiplier {
		get {
			return this.damageMultiplier;
		}
		set {
			this.damageMultiplier = value;
		}
	}

	public float CriticalHitDamageMultiplier {
		get {
			return this.criticalHitDamageMultiplier;
		}
		set {
			this.criticalHitDamageMultiplier = value;
		}
	}

	public float AttackDamagePerSecond {
		get {
			return this.attackDamagePerSecond;
		}
		set {
			this.attackDamagePerSecond = value;
		}
	}

	public int AnimatorState {
		get {
			return this.animatorState;
		}
		set {
			this.animatorState = value;
		}
	}

	public int PerkLevel {
		get {
			return this.perkLevel;
		}
		set {
			this.perkLevel = value;
		}
	}

	public Texture2D PerkLogo {
		get {
			return this.perkLogo;
		}
		set {
			this.perkLogo = value;
		}
	}

	public bool Selected {
		get {
			return this.selected;
		}
		set {
			this.selected = value;
		}
	}
}
