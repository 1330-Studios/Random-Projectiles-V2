namespace Random_Projectiles_V2 {
    public static class ThreadSafeRandom {
        public static T GetRandomObjectFromList<T>(this System.Collections.Generic.List<T> list) {
            return list[MelonMod_Main.__randomInstance.GetNextInt(list.Count)];
        }

        public static void Shuffle<T>(this System.Collections.Generic.List<T> list) {
            int n = list.Count;
            while (n > 1) {
                n--;
                int k = MelonMod_Main.__randomInstance.GetNextInt(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
