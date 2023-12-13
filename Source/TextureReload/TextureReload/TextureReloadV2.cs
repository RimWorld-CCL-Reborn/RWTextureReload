using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace TextureReload
{
    /*
    //  [StaticConstructorOnStartup]
    // This class does not work yet...
    public static class TextureReloadV2
    {
        static List<WeakReference> graphics;
        static List<WeakReference> graphicData;

        static TextureReloadV2()
        {
            graphics = new List<WeakReference>();
            graphicData = new List<WeakReference>();

            HarmonyInstance harmony = HarmonyInstance.Create("rimworld.erdelf.TextureReload");
            harmony.Patch(AccessTools.Method(typeof(Dialog_DebugActionsMenu), "DoListingItems"), null, new HarmonyMethod(typeof(TextureReloadV2), nameof(ReloadTextures)));
            typeof(Graphic).AllSubclassesNonAbstract().Do(t => harmony.Patch(AccessTools.Method(t, "DrawWorker"), null, new HarmonyMethod(typeof(TextureReloadV2), nameof(AddGraphic))));
            harmony.Patch(AccessTools.Method(typeof(GraphicData), "Init"), null, new HarmonyMethod(typeof(TextureReloadV2), nameof(AddGraphic)));
        }

        static void AddGraphic(object __instance)
        {
            Log.Message(__instance.GetType().FullName);
            if (__instance.GetType().IsSubclassOf(typeof(Graphic)))
            {
                if (!graphics.Any(wr => wr.Target == __instance))
                    graphics.Add(new WeakReference(__instance));
            } else if(__instance.GetType() == typeof(GraphicData))
                    if (!graphicData.Any(wr => wr.Target == __instance))
                        graphicData.Add(new WeakReference(__instance));
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
                });
                //Find.Maps.SelectMany(m => m.spawnedThings).Do(t => Traverse.Create(t).Field("graphicInt").SetValue(null));
                Log.Message(graphics.Count + " " + graphicData.Count);
                Log.Message(graphicData.RemoveAll(wr => !wr.IsAlive).ToString());
                graphicData.ForEach(wr => Traverse.Create(wr.Target).Method("Init").GetValue());
                graphics.RemoveAll(wr => !wr.IsAlive);
                graphics.ForEach(wr =>
                {
                    Graphic graphic = wr.Target as Graphic;
                    Log.Message((graphic != null).ToString() + " | " + (graphic.data != null).ToString());

                    if (AccessTools.Method(graphic.GetType(), nameof(Graphic.Init)).DeclaringType != typeof(Graphic))
                    {
                        Log.Message("hey");
                        graphic.Init(new GraphicRequest(graphic.GetType(), graphic.data?.texPath ?? graphic.path, graphic.data != null ? graphic.data.shaderType.Shader : graphic.Shader,
                            graphic.data?.drawSize ?? graphic.drawSize, graphic.data?.color ?? graphic.color, graphic.data?.colorTwo ?? graphic.colorTwo, graphic.data, 0, graphic.data?.shaderParameters));
                    }
                });
                Log.Message(graphics.Count + " " + graphicData.Count);
            })).GetValue();
    }*/
}