using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace Powerless_Deodorizers
{
    public class Patches
    {
        [HarmonyPatch(typeof(AirFilterConfig))]
        public class AirFilterConfig_Patch
        {
            [HarmonyPatch(typeof(AirFilterConfig), "CreateBuildingDef")]
			[HarmonyPatch(nameof(AirFilterConfig.CreateBuildingDef))]
			public static class AirFilterConfig_CreateBuildingDef_Patch
            {
				public static void Postfix(ref BuildingDef __result)
				{
                    __result.RequiresPowerInput = false;
					__result.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 0));

                }
			}
            [HarmonyPatch(typeof(AirFilterConfig), "DoPostConfigureComplete")]
            [HarmonyPatch(nameof(AirFilterConfig.DoPostConfigureComplete))]
            public static class AirFilterConfig_DoPostConfigureComplete_Patch
            {
                public static void Postfix(ref GameObject go)
                {
                    go.AddOrGet<LogicOperationalController>();
                    go.AddOrGetDef<PoweredActiveController.Def>();
                    SymbolOverrideControllerUtil.AddToPrefab(go);
                }
            }
        }
    }
}
