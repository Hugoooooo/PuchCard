using System;

namespace PunchCard
{
    //規則: 欄位名稱(不用資料表前置碼及底線) + "Type"
    /// <summary>
    /// 是否
    /// </summary>
    public class YesNo
    {
        /// <summary>是</summary>
        public static string Yes = "Y";
        /// <summary>否</summary>
        public static string No = "N";

        /// <summary>
        /// 依欄位值取得欄位說明
        /// 
        /// </summary>
        /// <param name="AFieldValue">欄位值</param>
        /// <returns>欄位說明</returns>
        public static string getFieldComment(string AFieldValue)
        {
            switch (AFieldValue)
            {
                case "Y": return "是";
                case "N": return "否";
                default: return "";
            }
        }
    }
    public class TrueFalse
    {
        /// <summary>True</summary>
        public static string True = "1";
        /// <summary>False</summary>
        public static string False = "0";
        /// <summary>
        /// 依欄位值取得欄位說明
        /// </summary>
        /// <param name="AFieldValue">欄位值</param>
        /// <returns>欄位說明</returns>
        public static string getFieldComment(string AFieldValue)
        {
            switch (AFieldValue)
            {
                case "0": return "假";
                case "1": return "真";
                default: return "";
            }
        }
    }
    public enum PunchCard
    {
        ON = 0,
        OFF = 1
    }

}

