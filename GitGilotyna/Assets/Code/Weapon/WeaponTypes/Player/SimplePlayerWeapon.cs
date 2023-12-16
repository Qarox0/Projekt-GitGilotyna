﻿using System;
using Code.Mobs;
using Code.Weapon.WeaponData;
using Unity.Mathematics;
using UnityEngine;

namespace Code.Weapon.WeaponTypes.Player
{
    public class SimplePlayerWeapon : PlayerWeapon
    {
        
        public override void Attack(Target target)
        {
            var weaponData = data as RangedWeaponData;
            if (weaponData == null) throw new Exception("Invalid Data Type For Weapon!");
            var position = player.transform.position;
            var gameObject =
                GameObject.Instantiate(weaponData.bulletPrefab, position, quaternion.identity);
            var rb = gameObject.GetComponent<Rigidbody2D>();
            float modifier = 1.0f;
            if (PlayerPrefs.HasKey(SkillType.ATTACK.ToString()))
            {
                modifier += 0.01f * PlayerPrefs.GetInt(SkillType.ATTACK.ToString());
            }
            gameObject.GetComponent<Bullet>().Initialize("Enemy", (int)(weaponData.attackDamage* modifier));
            var force = (target.transform.position - position).normalized * (Time.fixedDeltaTime * weaponData.speed);
            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }
}