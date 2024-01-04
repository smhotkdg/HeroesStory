#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("egVMCKx7MCTt5rdfl9pgBEHGSdUs1ILR0ZYI09MnmfUjJMaroIStQRudL1LsgiB/HXkI9CptmzCOkxJAP88bex+m8xcL7y1pHteDpjU+8LHnZBB19t6Oo+X8dsrlQgYyUfGGqD+NDi0/AgkGJYlHifgCDg4OCg8MMjejuwKHYeF/ai3PslqDHS2vUY9iFYYhvSIQkH5ttBwFJplZ2o6Rkg5qcxd0lnbJX1pFEQdOeINnU5PWl7/yEebchScGdlGSZQaqFYB5VX0eCl77PIwJJBp8ErOnJLqh4M87Ed1HlHFaewmJj5r7SJUte0Sdju3WjQ4ADz+NDgUNjQ4OD6MkdmKDpnfs5Wd11GyxJ0o2NNgnJCfGYP0n9TbyOOs6L1eZig0MDg8O");
        private static int[] order = new int[] { 2,7,10,11,4,10,10,8,13,9,10,12,13,13,14 };
        private static int key = 15;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
