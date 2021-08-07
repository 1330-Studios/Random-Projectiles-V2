using UnhollowerBaseLib;

namespace Random_Projectiles_V2 {
    public static class ThreadSafeRandom {
        public static T GetRandomObjectFromList<T>(this Il2CppSystem.Collections.Generic.List<T> list) {
            return list[URandom.GetNextInt(list.Count)];
        }
        public static T GetRandomObjectFromList<T>(this System.Collections.Generic.List<T> list) {
            return list[URandom.GetNextInt(list.Count)];
        }
        public static T GetRandomObjectFromList<T>(this Il2CppReferenceArray<T> list) where T : Il2CppObjectBase {
            return list[URandom.GetNextInt(list.Count)];
        }

        public static void Shuffle<T>(this System.Collections.Generic.List<T> list) {
            int n = list.Count;
            while (n > 1) {
                n--;
                int k = URandom.GetNextInt(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static void Shuffle<T>(this Il2CppSystem.Collections.Generic.List<T> list) {
            int n = list.Count;
            while (n > 1) {
                n--;
                int k = URandom.GetNextInt(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
