using Lohas.Round;
using Lohas.Shop;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace Lohas.Order
{
    public class QuestionnaireItemAccessor
    {
        private static List<QuestionnaireItem> GetByDataReader(SqlCommand SqlCommand)
        {
            List<QuestionnaireItem> QuestionnaireItems = new List<QuestionnaireItem>();

            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                using (SqlCommand.Connection = SqlConnection)
                {
                    SqlConnection.Open();

                    using (SqlDataReader SqlDataReader = SqlCommand.ExecuteReader())
                    {
                        while (SqlDataReader.Read())
                        {
                            string Id = (string)SqlDataReader["Id"];
                            string Title = (string)SqlDataReader["Title"];
                            string ShortTitle = (string)SqlDataReader["ShortTitle"];
                            OptionType OptionType = (OptionType)Enum.Parse(typeof(OptionType), (string)SqlDataReader["OptionType"]);
                            List<string> Options = ((string)SqlDataReader["Options"]).Split(',').ToList();
                            int Sort = (int)SqlDataReader["Sort"];
                            DateTime UpdateTime = (DateTime)SqlDataReader["UpdateTime"];
                            DateTime CreateTime = (DateTime)SqlDataReader["CreateTime"];

                            QuestionnaireItems.Add(new QuestionnaireItem(Id, Title, ShortTitle, OptionType, Options, Sort, UpdateTime, CreateTime));
                        }
                    }
                }
            }

            return QuestionnaireItems;
        }
        private static object GetByScalar(SqlCommand SqlCommand)
        {
            object Result = "";

            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                using (SqlCommand)
                {
                    SqlCommand.Connection = SqlConnection;

                    SqlConnection.Open();

                    Result = SqlCommand.ExecuteScalar();
                }
            }

            return Result;
        }

        internal static List<QuestionnaireItem> SelectAll()
        {
            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.CommandText = "SELECT "
                                   + " * "
                                   + "FROM "
                                   + " Order_QuestionnaireItem ";

            return GetByDataReader(SqlCommand);
        }

        internal static void UpdateInsert(List<QuestionnaireItem> QuestionnaireItems)
        {
            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                SqlConnection.Open();

                for (int i = 0; i < (QuestionnaireItems.Count / 200) + (QuestionnaireItems.Count % 200 == 0 ? 0 : 1); i++)
                {
                    List<QuestionnaireItem> PartCourseItems = QuestionnaireItems.Skip(i * 200).Take(200).ToList();
                    StringBuilder CommandText = new StringBuilder();

                    SqlCommand SqlCommand = SqlConnection.CreateCommand();

                    for (int j = 0; j < PartCourseItems.Count; j++)
                    {
                        CommandText.Append("UPDATE"
                                          + " Order_QuestionnaireItem "
                                          + "SET "
                                          + " Title = @Title" + j
                                          + " ,ShortTitle = @ShortTitle" + j
                                          + " ,OptionType = @OptionType" + j
                                          + " ,Options = @Options" + j
                                          + " ,Sort = @Sort" + j
                                          + " ,UpdateTime = @UpdateTime" + j
                                          + " WHERE "
                                          + " Id = @Id" + j

                                          + " IF @@ROWCOUNT = 0 "
                                          + "BEGIN "

                                          + "INSERT INTO "
                                          + " Order_QuestionnaireItem "
                                          + "(Id, Title, ShortTitle, OptionType, Options, Sort, UpdateTime, CreateTime ) "
                                          + "VALUES "
                                          + " ( "
                                          + " @Id" + j
                                          + " ,@Title" + j
                                          + " ,@ShortTitle" + j
                                          + " ,@OptionType" + j
                                          + " ,@Options" + j
                                          + " ,@Sort" + j
                                          + " ,@UpdateTime" + j
                                          + " ,@CreateTime" + j
                                          + " ) "
                                          + " END "
                                        );

                        QuestionnaireItem QuestionnaireItem = PartCourseItems[j];

                        SqlCommand.Parameters.AddWithValue("Id" + j, QuestionnaireItem.Id);
                        SqlCommand.Parameters.AddWithValue("Title" + j, QuestionnaireItem.Title);
                        SqlCommand.Parameters.AddWithValue("ShortTitle" + j, QuestionnaireItem.ShortTitle);
                        SqlCommand.Parameters.AddWithValue("OptionType" + j, QuestionnaireItem.OptionType.ToString());
                        SqlCommand.Parameters.AddWithValue("Options" + j, string.Join(",", QuestionnaireItem.Options));
                        SqlCommand.Parameters.AddWithValue("Sort" + j, QuestionnaireItem.Sort);
                        SqlCommand.Parameters.AddWithValue("UpdateTime" + j, QuestionnaireItem.UpdateTime);
                        SqlCommand.Parameters.AddWithValue("CreateTime" + j, QuestionnaireItem.CreateTime);
                    }

                    SqlCommand.CommandText = CommandText.ToString();
                    SqlCommand.ExecuteNonQuery();
                }
            }
        }

        internal static void Delete(QuestionnaireItem QuestionnaireItem)
        {
            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                using (SqlCommand SqlCommand = SqlConnection.CreateCommand())
                {
                    SqlCommand.CommandText = "Delete "
                                           + " Order_QuestionnaireItem "
                                           + "WHERE "
                                           + " Id = @Id ";

                    SqlCommand.Parameters.AddWithValue("Id", QuestionnaireItem.Id);

                    SqlConnection.Open();
                    SqlCommand.ExecuteNonQuery();
                }
            }
        }
    }
}