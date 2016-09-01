using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;

public enum ConfigKey { SeoTitle, SeoDescription, AnalyticsCode, 壹元簡訊帳號, 壹元簡訊密碼, 活動說明, 活動說明2, 報名成功說明, 黑名單, FB分享圖, FB分享Name, FB分享Caption, FB分享Description, 計數器, 問卷概述 }

public static partial class ConfigManager
{
    private static Dictionary<ConfigKey, string> Configs;

    //上傳路徑
    public static string UploadPath { get { return "/_Upload/Config/"; } }
    public static string PhysicalUploadPath { get { return HttpContext.Current.Server.MapPath(UploadPath); } }

    //初始化
    public static void Initial()
    {
        Configs = ((ConfigKey[])Enum.GetValues(typeof(ConfigKey))).ToDictionary(k => k, v => "");

        using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
        {

            SqlCommand SqlCommand = SqlConnection.CreateCommand();
            SqlCommand.CommandText = "SELECT "
                                  + " * "
                                  + "FROM "
                                  + " Config ";

            SqlConnection.Open();
            SqlDataReader SqlDataReader = SqlCommand.ExecuteReader();

            while (SqlDataReader.Read())
            {
                string KeyString = (string)SqlDataReader["ConfigKey"];
                string ValueString = (string)SqlDataReader["Value"];

                ConfigKey Key;
                if (Enum.TryParse(KeyString, out Key) == false) { continue; }

                Configs[Key] = ValueString;
            }
        }
    }

    //取得所有的Configs
    public static Dictionary<ConfigKey, string> GetAll()
    {
        return Configs;
    }

    public static string GetByConfigKey(ConfigKey ConfigKey)
    {
        return Configs[ConfigKey];
    }

    //儲存Configs
    public static void Save(Dictionary<ConfigKey, string> Configs)
    {
        using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
        {
            SqlCommand SqlCommand = SqlConnection.CreateCommand();

            StringBuilder Commandtext = new StringBuilder();
            Commandtext.Append("DELETE FROM Config WHERE 1 = 1 ;");

            foreach (KeyValuePair<ConfigKey, string> Item in Configs)
            {
                Commandtext.Append("INSERT INTO "
                                  + " Config "
                                  + "( ConfigKey ,Value ) "
                                  + "VALUES "
                                  + " ( @ConfigKey" + Item.Key + " ,@Value" + Item.Key + " ) ;"
                                  );

                SqlCommand.Parameters.AddWithValue("ConfigKey" + Item.Key, Item.Key.ToString());
                SqlCommand.Parameters.AddWithValue("Value" + Item.Key, Item.Value);
            }

            SqlCommand.CommandText = Commandtext.ToString();

            SqlConnection.Open();
            SqlCommand.ExecuteNonQuery();
        }
    }
}
