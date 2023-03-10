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
	public class BullseyeCollectibleBehaviorArrow : BullseyeCollectibleBehaviorAmmunition
	{
		public BullseyeCollectibleBehaviorArrow(CollectibleObject collObj) : base(collObj) {}

		protected BullseyeSystemConfig ConfigSystem {get; private set;}

		public override void OnLoaded(ICoreAPI api)
		{
			base.OnLoaded(api);

			ConfigSystem = api.ModLoader.GetModSystem<BullseyeSystemConfig>();
		}

		public override float GetDamage(ItemSlot inSlot, IWorldAccessor world)
		{
			return inSlot.Itemstack.Collectible.Attributes["damage"].AsFloat(0) * ConfigSystem.GetSyncedConfig()?.ArrowDamage ?? 1f;
		}
	}
}