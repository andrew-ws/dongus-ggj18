using GG18.Minions;
using GG18.Missiles;
using Rewired;
using System;
using UnityEngine;

public class Firewall : MonoBehaviour {

	[SerializeField] int playerId;
	public Player player;
	float height;
	public float max_height = 3f;
	public float move_speed = 3f;
	bool going_up = false;
	bool going_down = false;
	Vector3 initpos;
	public float cooldown_length;
	float cooldown_timer;
	public float minion_dps;
	public float top_hold_time = 1f;
	float top_hold_timer;


	// Use this for initialization
	void Start () {
		player = ReInput.players.GetPlayer(playerId);
		initpos = this.transform.position;
		cooldown_timer = -1f;
		top_hold_timer = -1f;
	}
	
	// Update is called once per frame
	void Update () {
		if (player.GetButtonDown("Firewall")) {
			Up();
		}

		if (top_hold_timer < 0) {
			if (going_up) {
				height += move_speed * Time.deltaTime;
				if (height > max_height) {
					height = max_height;
					going_up = false;
					going_down = true;
					top_hold_timer = top_hold_time;
				}
			}
			else if (going_down) {
				height -= move_speed * Time.deltaTime;
				if (height < 0) {
					height = 0;
					going_up = false;
					going_down = false;
				}
			}
		}
		transform.position = initpos + Vector3.up * height;
		cooldown_timer -= Time.deltaTime;
		top_hold_timer -= Time.deltaTime;
	}

	public void Up () {
		if (cooldown_timer > 0) {
			return;
		}
		going_up = true;
		going_down = false;
	}

	private void OnCollisionEnter(Collision collision)
	{
		GameObject otherGO = collision.gameObject;
		if (otherGO.tag == "minion") {
			Minion otherMinion = otherGO.GetComponent<Minion>();
			if (otherMinion.player != player) {
				otherMinion.TakeDamage(minion_dps);
			}
		}
		if (otherGO.tag == "missile") {
			Missile otherMissile = otherGO.GetComponent<Missile>();
			if (otherMissile.player != player) {
				// TODO: is this gonna always go before the terminal collision?
				Destroy(otherGO);
			}
		}
	}
}
