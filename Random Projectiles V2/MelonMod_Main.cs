using MelonLoader;
using Random_Projectiles_V2;
using System.Threading;
using System.Windows.Forms;

[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]
[assembly: MelonInfo(typeof(MelonMod_Main), "Randomized Projectiles", "2.0", "1330 Studios LLC")]

namespace Random_Projectiles_V2 {
    internal class MelonMod_Main : MelonMod {
        internal static Controller __instance__ = new Controller();

        public override void OnApplicationStart() {
            MelonLogger.Msg("Random Projectiles Loaded!");
        }

        [HarmonyLib.HarmonyPatch(typeof(GameModelLoader), nameof(GameModelLoader.Load))]
        internal static class GamePatch {
            [HarmonyLib.HarmonyPostfix]
            internal static void Fix() {
                Thread t = new Thread(() => {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(__instance__);
                });
                t.Start();
            }
        }
    }
}
