using Oracle.ManagedDataAccess.Types;

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
    }
}
