using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Lohas.SlideShow
{
    public class SlideShowItemAccessor
    {
        private static List<SlideShowItem> Get(SqlCommand SqlCommand)
        {
            List<SlideShowItem> SlideShowItems = new List<SlideShowItem>();

            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                using (SqlCommand.Connection = SqlConnection)
                {
                    SqlConnection.Open();

                    SqlDataReader SqlDataReader = SqlCommand.ExecuteReader();

                    while (SqlDataReader.Read())
                    {
                        string Id = (string)SqlDataReader["Id"];
                        string Image = (string)SqlDataReader["Image"];
                        bool Enable = (bool)SqlDataReader["Enable"];
                        string Url = (string)SqlDataReader["Url"];
                        int Sort = (int)SqlDataReader["Sort"];
                        DateTime UpdateTime = (DateTime)SqlDataReader["UpdateTime"];
                        DateTime CreateTime = (DateTime)SqlDataReader["CreateTime"];

                        SlideShowItem SlideShowItem = new SlideShowItem(Id, Image, Enable, Url, Sort,  UpdateTime, CreateTime);
                        SlideShowItems.Add(SlideShowItem);
                    }
                }
            }

            return SlideShowItems;
        }

        internal static List<SlideShowItem> SelectAll()
        {
            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.CommandText = "SELECT "
                                   + " * "
                                   + "FROM "
                                   + " SlideShow_SlideShowItem ";

            return Get(SqlCommand);
        }

        internal static void UpdateInsert(SlideShowItem SlideShowItem)
        {
            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                using (SqlCommand SqlCommand = SqlConnection.CreateCommand())
                {
                    SqlCommand.CommandText = "UPDATE "
                                             + " SlideShow_SlideShowItem "
                                             + "SET "
                                             + " Image = @Image "
                                             + " ,Enable = @Enable "
                                             + " ,Url = @Url "
                                             + " ,Sort = @Sort "
                                             + " ,UpdateTime = @UpdateTime "
                                             + " ,CreateTime = @CreateTime "
                                             + "WHERE "
                                             + " Id = @Id "

                                             + "IF @@ROWCOUNT = 0 "
                                             + "BEGIN "

                                             + "INSERT INTO "
                                             + " SlideShow_SlideShowItem "
                                             + "( Id, Image, Enable, Url, Sort, UpdateTime, CreateTime  ) "
                                             + "VALUES "
                                             + "( @Id, @Image, @Enable, @Url, @Sort, @UpdateTime, @CreateTime ) "

                                             + "END ";
                    
                    SqlCommand.Parameters.AddWithValue("Id", SlideShowItem.Id);
                    SqlCommand.Parameters.AddWithValue("Image", SlideShowItem.Image);
                    SqlCommand.Parameters.AddWithValue("Enable", SlideShowItem.Enable);
                    SqlCommand.Parameters.AddWithValue("Url", SlideShowItem.Url);
                    SqlCommand.Parameters.AddWithValue("Sort", SlideShowItem.Sort);
                    SqlCommand.Parameters.AddWithValue("UpdateTime", SlideShowItem.UpdateTime);
                    SqlCommand.Parameters.AddWithValue("CreateTime", SlideShowItem.CreateTime);

                    SqlConnection.Open();
                    SqlCommand.ExecuteNonQuery();
                }
            }
        }

        internal static void Delete(SlideShowItem SlideShowItem)
        {
            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                using (SqlCommand SqlCommand = SqlConnection.CreateCommand())
                {
                    SqlCommand.CommandText = "Delete "
                                           + " SlideShow_SlideShowItem "
                                           + "WHERE "
                                           + " Id = @Id ";

                    SqlCommand.Parameters.AddWithValue("Id", SlideShowItem.Id);

                    SqlConnection.Open();
                    SqlCommand.ExecuteNonQuery();
                }
            }
        }
    }
}