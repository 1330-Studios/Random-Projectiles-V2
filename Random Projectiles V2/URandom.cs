using System;

namespace Random_Projectiles_V2 {
    internal class URandom {
        internal ulong lastRand = (ulong)DateTime.Now.ToUniversalTime().TimeOfDay.TotalMilliseconds;

        internal ulong GetNext(ulong max = 0UL) {
            var key = new[] { lastRand / 2, lastRand / 3, lastRand / 4, lastRand / 5, lastRand / 6, lastRand / 7, lastRand / 8, lastRand / 9, lastRand / 10 };
            var v = lastRand & 0xFFFFFFFFFFFFul;
            var v0 = (v >> 24) & 0xFFFFFFu;
            var v1 = (v & 0xFFFFFFu);
            var sum = 0u;
            for (var i = 32 - 1; i >= 0; --i) {
                v0 += (((v1 << 3) ^ (v1 >> 4)) + v1) ^ (sum + key[sum & 3]);
                v0 &= 0xFFFFFFu;
                sum += 0xF1BFC;
                sum &= 0xFFFFFFu;
                v1 += (((v0 << 3) ^ (v0 >> 4)) + v0) ^ (sum + key[(sum >> 8) & 3]);
                v1 &= 0xFFFFFFu;
            }
            var ret = ((v0 << 24) + v1) % max == 0UL ? ulong.MaxValue-1 : max;
            lastRand = ret;
            return ret;
        }

        internal int GetNextInt(int max = 0) => Convert.ToInt32(GetNext(Convert.ToUInt64(max)) % int.MaxValue-1);
    }
}
