using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Lohas.Area
{
    public class AreaItemAccessor
    {
        private static List<AreaItem> Get(SqlCommand SqlCommand)
        {
            List<AreaItem> AreaItems = new List<AreaItem>();

            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                using (SqlCommand.Connection = SqlConnection)
                {
                    SqlConnection.Open();

                    SqlDataReader SqlDataReader = SqlCommand.ExecuteReader();

                    while (SqlDataReader.Read())
                    {
                        string Id = (string)SqlDataReader["Id"];
                        string Name = (string)SqlDataReader["Name"];
                        int Sort = (int)SqlDataReader["Sort"];
                        DateTime UpdateTime = (DateTime)SqlDataReader["UpdateTime"];
                        DateTime CreateTime = (DateTime)SqlDataReader["CreateTime"];

                        AreaItem AreaItem = new AreaItem(Id, Name, Sort, UpdateTime, CreateTime);
                        AreaItems.Add(AreaItem);
                    }
                }
            }

            return AreaItems;
        }

        internal static List<AreaItem> SelectAll()
        {
            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.CommandText = "SELECT "
                                   + " * "
                                   + "FROM "
                                   + " School_AreaItem ";

            return Get(SqlCommand);
        }

        internal static void UpdateInsert(AreaItem AreaItem)
        {
            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                using (SqlCommand SqlCommand = SqlConnection.CreateCommand())
                {
                    SqlCommand.CommandText = "UPDATE "
                                             + " School_AreaItem "
                                             + "SET "
                                             + " Name = @Name "
                                             + " ,Sort = @Sort "
                                             + " ,UpdateTime = @UpdateTime "
                                             + " ,CreateTime = @CreateTime "
                                             + "WHERE "
                                             + " Id = @Id "

                                             + "IF @@ROWCOUNT = 0 "
                                             + "BEGIN "

                                             + "INSERT INTO "
                                             + " School_AreaItem "
                                             + "( Id, Name, Sort, UpdateTime, CreateTime  ) "
                                             + "VALUES "
                                             + "( @Id, @Name, @Sort, @UpdateTime, @CreateTime ) "

                                             + "END ";

                    SqlCommand.Parameters.AddWithValue("Id", AreaItem.Id);
                    SqlCommand.Parameters.AddWithValue("Name", AreaItem.Name);
                    SqlCommand.Parameters.AddWithValue("Sort", AreaItem.Sort);
                    SqlCommand.Parameters.AddWithValue("UpdateTime", AreaItem.UpdateTime);
                    SqlCommand.Parameters.AddWithValue("CreateTime", AreaItem.CreateTime);

                    SqlConnection.Open();
                    SqlCommand.ExecuteNonQuery();
                }
            }
        }

        internal static void Delete(AreaItem AreaItem)
        {
            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                using (SqlCommand SqlCommand = SqlConnection.CreateCommand())
                {
                    SqlCommand.CommandText = "Delete "
                                           + " School_AreaItem "
                                           + "WHERE "
                                           + " Id = @Id ";

                    SqlCommand.Parameters.AddWithValue("Id", AreaItem.Id);

                    SqlConnection.Open();
                    SqlCommand.ExecuteNonQuery();
                }
            }
        }


    }
}