using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Harmony;
using UnityEngine;
using Verse;

namespace TextureReload
{
    [StaticConstructorOnStartup]
    public static class TextureReload
    {
        static TextureReload()
        {
            HarmonyInstance.Create("rimworld.erdelf.TextureReload").Patch(AccessTools.Method(typeof(Dialog_DebugActionsMenu), "DoListingItems"), null, new HarmonyMethod(typeof(TextureReload), nameof(ReloadTextures)));
        }

        static void ReloadTextures(Dialog_DebugActionsMenu __instance) => 
            Traverse.Create(__instance as Dialog_DebugOptionLister).Method("DebugAction", "Reload Textures", new Action(() =>
            {
                GraphicDatabase.Clear();
                LoadedModManager.RunningMods.Do(current =>
                {
                    Traverse textureInfo = Traverse.Create(current).Field("textures");
                    ModContentHolder<Texture2D> textures = textureInfo.GetValue<ModContentHolder<Texture2D>>();
                    textures.contentList.Clear();
                    textures.ReloadAll();
                    textureInfo.SetValue(textures);
                    
                    GenDefDatabase.DefsToGoInDatabase<ThingDef>(current).Do(def =>
                    {
                        if (def.graphicData != null)
                        {
                            Traverse.Create(def.graphicData).Method("Init").GetValue();
                            def.graphic = def.graphicData.Graphic;
                        }
                    });
                });
                if (Current.ProgramState == ProgramState.Playing)
                    Find.Maps.SelectMany(m => m.spawnedThings).Do(t => Traverse.Create(t).Field("graphicInt").SetValue(null));
            })).GetValue();
    }
}