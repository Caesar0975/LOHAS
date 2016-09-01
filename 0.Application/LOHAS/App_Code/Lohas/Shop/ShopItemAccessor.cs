using Lohas.Round;
using Lohas.Shop;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace Lohas.Shop
{
    public class ShopItemAccessor
    {
        private static List<ShopItem> Get(SqlCommand SqlCommand)
        {
            List<ShopItem> ShopItems = new List<ShopItem>();

            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                using (SqlCommand.Connection = SqlConnection)
                {
                    SqlConnection.Open();

                    SqlDataReader SqlDataReader = SqlCommand.ExecuteReader();

                    while (SqlDataReader.Read())
                    {
                        List<string> SchoolItemIds = SchoolItemStringParse((string)SqlDataReader["SchoolItemIds"]);
                        string Id = (string)SqlDataReader["Id"];
                        string Name = (string)SqlDataReader["Name"];
                        string Phone = (string)SqlDataReader["Phone"];
                        string Address = (string)SqlDataReader["Address"];
                        string Latitude = (string)SqlDataReader["Latitude"];
                        string Longitude = (string)SqlDataReader["Longitude"];
                        List<RoundItem> RoundItems = StringParse2RoundItem((string)SqlDataReader["RoundItems"]);
                        int Sort = (int)SqlDataReader["Sort"];
                        DateTime UpdateTime = (DateTime)SqlDataReader["UpdateTime"];
                        DateTime CreateTime = (DateTime)SqlDataReader["CreateTime"];

                        ShopItem ShopItem = new ShopItem(SchoolItemIds, Id, Name, Phone, Address, Latitude, Longitude, RoundItems, Sort, UpdateTime, CreateTime);
                        ShopItems.Add(ShopItem);
                    }
                }
            }

            return ShopItems;
        }

        internal static List<ShopItem> SelectAll()
        {
            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.CommandText = "SELECT "
                                   + " * "
                                   + "FROM "
                                   + " Shop_ShopItem ";

            return Get(SqlCommand);
        }

        internal static void UpdateInsert(ShopItem ShopItem)
        {
            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                using (SqlCommand SqlCommand = SqlConnection.CreateCommand())
                {
                    SqlCommand.CommandText = "UPDATE "
                                             + " Shop_ShopItem "
                                             + "SET "
                                             + " SchoolItemIds = @SchoolItemIds "
                                             + " ,Name = @Name "
                                             + " ,Phone = @Phone "
                                             + " ,Address = @Address "
                                             + " ,Latitude = @Latitude "
                                             + " ,Longitude = @Longitude "
                                             + " ,RoundItems = @RoundItems "
                                             + " ,Sort = @Sort "
                                             + " ,UpdateTime = @UpdateTime "
                                             + " ,CreateTime = @CreateTime "
                                             + "WHERE "
                                             + " Id = @Id "

                                             + "IF @@ROWCOUNT = 0 "
                                             + "BEGIN "

                                             + "INSERT INTO "
                                             + " Shop_ShopItem "
                                             + "( SchoolItemIds, Id, Name, Phone, Address, Latitude, Longitude, RoundItems, Sort, UpdateTime, CreateTime  ) "
                                             + "VALUES "
                                             + "( @SchoolItemIds, @Id, @Name, @Phone, @Address, @Latitude, @Longitude, @RoundItems, @Sort, @UpdateTime, @CreateTime ) "

                                             + "END ";

                    SqlCommand.Parameters.AddWithValue("SchoolItemIds", ShopItem.SchoolItemIds.Count > 0 ? string.Join("｜", ShopItem.SchoolItemIds.ToArray()) : "");
                    SqlCommand.Parameters.AddWithValue("Id", ShopItem.Id);
                    SqlCommand.Parameters.AddWithValue("Name", ShopItem.Name);
                    SqlCommand.Parameters.AddWithValue("Phone", ShopItem.Phone);
                    SqlCommand.Parameters.AddWithValue("Address", ShopItem.Address);
                    SqlCommand.Parameters.AddWithValue("Latitude", ShopItem.Latitude);
                    SqlCommand.Parameters.AddWithValue("Longitude", ShopItem.Longitude);
                    SqlCommand.Parameters.AddWithValue("RoundItems", RoundItemParse2String(ShopItem.RoundItems));
                    SqlCommand.Parameters.AddWithValue("Sort", ShopItem.Sort);
                    SqlCommand.Parameters.AddWithValue("UpdateTime", ShopItem.UpdateTime);
                    SqlCommand.Parameters.AddWithValue("CreateTime", ShopItem.CreateTime);

                    SqlConnection.Open();
                    SqlCommand.ExecuteNonQuery();
                }
            }
        }

        internal static void Delete(ShopItem ShopItem)
        {
            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                using (SqlCommand SqlCommand = SqlConnection.CreateCommand())
                {
                    SqlCommand.CommandText = "Delete "
                                           + " Shop_ShopItem "
                                           + "WHERE "
                                           + " Id = @Id ";

                    SqlCommand.Parameters.AddWithValue("Id", ShopItem.Id);

                    SqlConnection.Open();
                    SqlCommand.ExecuteNonQuery();
                }
            }
        }

        private static List<string> SchoolItemStringParse(string SchoolItemIds)
        {
            List<string> SchoolItemIdList = new List<string>();
            foreach (string SchoolItemId in SchoolItemIds.Split(new string[] { "｜" }, StringSplitOptions.RemoveEmptyEntries))
            {
                SchoolItemIdList.Add(SchoolItemId);
            }
            return SchoolItemIdList;
        }

        private static string RoundItemParse2String(List<RoundItem> RoundItemList)
        {
            StringBuilder StringBuilder = new StringBuilder();

            foreach (RoundItem RoundItem in RoundItemList)
            {
                string ShopItemId = RoundItem.ShopItemId;
                string Id = RoundItem.Id;
                TimeSpan StartTime = RoundItem.StartTime;
                TimeSpan EndTime = RoundItem.EndTime;
                int LimitPairAmount = RoundItem.LimitPairAmount;

                string RoundString = string.Format("ShopItemId：{0}；Id：{1}；StartTime：{2}；EndTime：{3}；LimitPairAmount：{4}；│",
                     ShopItemId, Id, StartTime, EndTime, LimitPairAmount);

                StringBuilder.Append(RoundString);
            }

            return StringBuilder.ToString();
        }

        private static List<RoundItem> StringParse2RoundItem(string RoundStrings)
        {
            List<RoundItem> RoundItemList = new List<RoundItem>();

            foreach (string RoundString in RoundStrings.Split(new string[] { "│" }, StringSplitOptions.RemoveEmptyEntries))
            {
                Dictionary<string, string> KeyValues = new Dictionary<string, string> { };

                foreach (string KeyValue in RoundString.Split(new string[] { "；" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    KeyValues.Add(KeyValue.Split('：')[0], KeyValue.Split('：')[1]);
                }

                string ShopItemId = KeyValues["ShopItemId"];
                string Id = KeyValues["Id"];
                TimeSpan StartTime = TimeSpan.Parse(KeyValues["StartTime"]);
                TimeSpan EndTime = TimeSpan.Parse(KeyValues["EndTime"]);
                int LimitPairAmount = int.Parse(KeyValues["LimitPairAmount"]);

                RoundItemList.Add(new RoundItem(ShopItemId, Id, StartTime, EndTime, LimitPairAmount));
            }

            RoundItemList = RoundItemList.OrderBy(r => r.StartTime).ToList();

            return RoundItemList;
        }
    }
}