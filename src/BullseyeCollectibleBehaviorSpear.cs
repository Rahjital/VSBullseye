﻿using System;
using System.Text;
using System.Collections.Generic;
using Vintagestory.API.Server;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.Config;
using Vintagestory.API.MathTools;
using Vintagestory.API.Util;
using Vintagestory.GameContent;

namespace Bullseye
{
	public class BullseyeCollectibleBehaviorSpear : BullseyeCollectibleBehaviorThrowable
	{
		public BullseyeCollectibleBehaviorSpear(CollectibleObject collObj) : base(collObj) {}

		public override float GetProjectileDamage(EntityAgent byEntity, ItemSlot weaponSlot, ItemSlot ammoSlot)
		{
			return base.GetProjectileDamage(byEntity, weaponSlot, ammoSlot) * ConfigSystem.GetSyncedConfig().SpearDamage;
		}

		public override float GetProjectileWeight(EntityAgent byEntity, ItemSlot weaponSlot, ItemSlot ammoSlot) => 0.3f;
		public override int GetProjectileDurabilityCost(EntityAgent byEntity, ItemSlot weaponSlot, ItemSlot ammoSlot) => 3;
		public override float GetProjectileDropChance(EntityAgent byEntity, ItemSlot weaponSlot, ItemSlot ammoSlot) => 1.1f;

		public override EntityProperties GetProjectileEntityType(EntityAgent byEntity, ItemSlot weaponSlot, ItemSlot ammoSlot)
		{
			// Accept either Bullseye projectileEntityCode, or vanilla spearEntityCode
			string entityCode = collObj.Attributes["projectileEntityCode"].AsString() ?? collObj.Attributes["spearEntityCode"].AsString();

			return byEntity.World.GetEntityType(new AssetLocation(entityCode));
		}

		public override void OnShot(ItemSlot slot, Entity projectileEntity, EntityAgent byEntity) 
		{
			//if (byEntity is EntityPlayer) collObj.RefillSlotIfEmpty(slot, byEntity, (itemstack) => itemstack.Collectible is BullseyeItemSpear);

			base.OnShot(slot, projectileEntity, byEntity);
		}

		public override void GetHeldItemInfo(ItemSlot inSlot, StringBuilder dsc, IWorldAccessor world, bool withDebugInfo)
		{
			if (inSlot.Itemstack.Collectible.Attributes == null) return;

			float damage = 0f;

			if (inSlot.Itemstack.Collectible.Attributes != null)
			{
				damage = inSlot.Itemstack.Collectible.Attributes["damage"].AsFloat(0) * ConfigSystem.GetSyncedConfig().SpearDamage;
			}

			dsc.AppendLine(damage + Lang.Get("piercing-damage-thrown"));
		}
	}
}
