using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Lohas.School
{
    public class SchoolItemAccessor
    {
        private static List<SchoolItem> Get(SqlCommand SqlCommand)
        {
            List<SchoolItem> SchoolItems = new List<SchoolItem>();

            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                using (SqlCommand.Connection = SqlConnection)
                {
                    SqlConnection.Open();

                    SqlDataReader SqlDataReader = SqlCommand.ExecuteReader();

                    while (SqlDataReader.Read())
                    {
                        string AreaItemId = (string)SqlDataReader["AreaItemId"];
                        string Id = (string)SqlDataReader["Id"];
                        string Name = (string)SqlDataReader["Name"];
                        int Sort = (int)SqlDataReader["Sort"];
                        DateTime UpdateTime = (DateTime)SqlDataReader["UpdateTime"];
                        DateTime CreateTime = (DateTime)SqlDataReader["CreateTime"];

                        SchoolItem SchoolItem = new SchoolItem(AreaItemId, Id, Name, Sort, UpdateTime, CreateTime);
                        SchoolItems.Add(SchoolItem);
                    }
                }
            }

            return SchoolItems;
        }

        internal static List<SchoolItem> SelectAll()
        {
            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.CommandText = "SELECT "
                                   + " * "
                                   + "FROM "
                                   + " School_SchoolItem ";

            return Get(SqlCommand);
        }

        internal static void UpdateInsert(SchoolItem SchoolItem)
        {
            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                using (SqlCommand SqlCommand = SqlConnection.CreateCommand())
                {
                    SqlCommand.CommandText = "UPDATE "
                                             + " School_SchoolItem "
                                             + "SET "
                                             + " AreaItemId = @AreaItemId "
                                             + " ,Name = @Name "
                                             + " ,Sort = @Sort "
                                             + " ,UpdateTime = @UpdateTime "
                                             + " ,CreateTime = @CreateTime "
                                             + "WHERE "
                                             + " Id = @Id "

                                             + "IF @@ROWCOUNT = 0 "
                                             + "BEGIN "

                                             + "INSERT INTO "
                                             + " School_SchoolItem "
                                             + "( AreaItemId, Id, Name, Sort, UpdateTime, CreateTime  ) "
                                             + "VALUES "
                                             + "( @AreaItemId, @Id, @Name, @Sort, @UpdateTime, @CreateTime ) "

                                             + "END ";

                    SqlCommand.Parameters.AddWithValue("AreaItemId", SchoolItem.AreaItemId);
                    SqlCommand.Parameters.AddWithValue("Id", SchoolItem.Id);
                    SqlCommand.Parameters.AddWithValue("Name", SchoolItem.Name);
                    SqlCommand.Parameters.AddWithValue("Sort", SchoolItem.Sort);
                    SqlCommand.Parameters.AddWithValue("UpdateTime", SchoolItem.UpdateTime);
                    SqlCommand.Parameters.AddWithValue("CreateTime", SchoolItem.CreateTime);

                    SqlConnection.Open();
                    SqlCommand.ExecuteNonQuery();
                }
            }
        }

        internal static void Delete(SchoolItem SchoolItem)
        {
            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                using (SqlCommand SqlCommand = SqlConnection.CreateCommand())
                {
                    SqlCommand.CommandText = "Delete "
                                           + " School_SchoolItem "
                                           + "WHERE "
                                           + " Id = @Id ";

                    SqlCommand.Parameters.AddWithValue("Id", SchoolItem.Id);

                    SqlConnection.Open();
                    SqlCommand.ExecuteNonQuery();
                }
            }
        }


    }
}