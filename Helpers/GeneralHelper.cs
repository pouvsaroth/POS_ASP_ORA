using Oracle.ManagedDataAccess.Types;
using POS_ASP_ORA.Models;

namespace POS_ASP_ORA.Helpers
{
    public class GeneralHelper
    {
        public static int GetOracleInt(object value)
        {
            if (value == null || value == DBNull.Value)
                return 0;

            var oracleValue = (OracleDecimal)value;

            if (oracleValue.IsNull)
                return 0;

            return oracleValue.ToInt32();
        }

        public static List<MenuModel> BuildMenuTree(List<MenuModel> flatList)
        {
            var lookup = flatList.ToDictionary(x => x.Id);

            foreach (var item in flatList)
            {
                if (!string.IsNullOrEmpty(item.ParentId) && lookup.ContainsKey(item.ParentId))
                {
                    lookup[item.ParentId].Children.Add(item);
                }
            }

            return flatList.Where(x => string.IsNullOrEmpty(x.ParentId)).ToList();
        }

        public static byte[] StringToByteArray(string hex)
        {
            if (string.IsNullOrEmpty(hex))
                throw new ArgumentException("Input string cannot be null or empty.", nameof(hex));

            int length = hex.Length;
            byte[] bytes = new byte[length / 2];
            for (int i = 0; i < length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }
    }
}
