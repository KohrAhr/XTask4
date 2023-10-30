using System.Text;

namespace DataProvider.Functions
{
    public static class CoreHelper
    {
        public static void ProceedConfigFile()
        {
            IConfigurationRoot config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            // #1
            string? lValue = config["DbConnString"];

            if (string.IsNullOrEmpty(lValue))
            {
                System.Diagnostics.Trace.Write("Connection string to db cannot be empty");
                throw new Exception("Connection string to db cannot be empty");
            }

            AppData.ConnString = lValue;

            // #2
            int lResult;

            lValue = config["MaxRecordsInOutput"];

            if (!int.TryParse(lValue, out lResult))
            {
                lResult = AppData.CONST_DEF_MAX_RECORDS_IN_OUTPUT;
            }

            AppData.MaxRecordsInOutput = lResult;

            // #3

            lValue = config["MinutesDbTTL"];

            if (!int.TryParse(lValue, out lResult))
            {
                lResult = AppData.CONST_DEF_MAX_RECORDS_IN_OUTPUT;
            }

            AppData.MinutesDbTTL = lResult;
        }

        ///
        /// <summary>
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static uint CalculateCRC32(string input)
        {
            uint crc32 = 0xFFFFFFFF; // Initial CRC32 value

            byte[] bytes = Encoding.UTF8.GetBytes(input);

            foreach (byte b in bytes)
            {
                crc32 ^= (uint)b;

                for (int i = 0; i < 8; i++)
                {
                    if ((crc32 & 1) == 1)
                    {
                        crc32 = (crc32 >> 1) ^ 0xEDB88320; // CRC32 polynomial
                    }
                    else
                    {
                        crc32 >>= 1;
                    }
                }
            }

            crc32 ^= 0xFFFFFFFF; // Invert the final CRC32 value

            return crc32;
        }

        ///
        public static string BuildWhereCondition(int aSupplierId, string aCarType, float aMin, float aMax)
        {
            string result = string.Empty;
            string? value;
            float valueMoney;

            // 

            if (aSupplierId > 0)
            {
                result += $" ([SupplierId] = {aSupplierId}) ";
            }

            // #1

            value = aCarType;

            if (!String.IsNullOrEmpty(value))
            {
                if (!String.IsNullOrEmpty(result))
                {
                    result += " and ";
                }
                result += $" ([CarDesc] like '%{value}%') ";
            }

            // #2

            valueMoney = aMin;

            if (valueMoney > 0)
            {
                if (!String.IsNullOrEmpty(result))
                {
                    result += " and ";
                }
                result += $" ([RentCost] >= {valueMoney}) ";
            }

            // #3

            valueMoney = aMax;

            if (valueMoney > 0)
            {
                if (!String.IsNullOrEmpty(result))
                {
                    result += " and ";
                }
                result += $" ([RentCost] <= {valueMoney}) ";
            }

            // #4. Ok. Perhaps with currency we have a trick here. :)

            if (aMax > 0 || aMin > 0)
            {
                if (!String.IsNullOrEmpty(result))
                {
                    result += " and ";
                }
                result += $" ([RentCurrency] = 'EUR') ";
            }

            //

            // Final
            if (!String.IsNullOrEmpty(result))
            {
                result = $" where {result}";
            }

            return result;
        }


    }
}