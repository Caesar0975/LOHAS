using LeftHand.MemberShip;
using Lohas.Shop;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Lohas.Order
{
    public class OrderItemManager
    {
        private static decimal _DayOrderCounter;
        public static void Initial()
        {
            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.CommandText = "SELECT "
                                   + " ISNULL(MAX(Id),0)"
                                   + "FROM "
                                   + " Order_OrderItem ";

            decimal LastId = (decimal)GetByScalar(SqlCommand);
            decimal DateStartId = decimal.Parse(DateTime.Now.ToString("yyyyMMdd") + "000");

            _DayOrderCounter = (DateStartId > LastId) ? DateStartId : LastId;
        }

        private static List<OrderItem> GetByDataReader(SqlCommand SqlCommand)
        {
            List<OrderItem> OrderItems = new List<OrderItem>();

            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                using (SqlCommand)
                {
                    SqlCommand.Connection = SqlConnection;
                    SqlConnection.Open();

                    SqlDataReader SqlDataReader = SqlCommand.ExecuteReader();

                    while (SqlDataReader.Read())
                    {
                        decimal Id = (decimal)SqlDataReader["Id"];

                        string MemberAccount = (string)SqlDataReader["MemberAccount"];
                        string MemberName = (string)SqlDataReader["MemberName"];
                        string MemberPhone = (string)SqlDataReader["MemberPhone"];

                        string MemberArea = (string)SqlDataReader["MemberArea"];
                        string MemberSchool = (string)SqlDataReader["MemberSchool"];

                        string ShopId = (string)SqlDataReader["ShopId"];
                        string ShopName = (string)SqlDataReader["ShopName"];
                        string ShopPhone = (string)SqlDataReader["ShopPhone"];
                        string ShopAddress = (string)SqlDataReader["ShopAddress"];
                        string Shoplatitude = (string)SqlDataReader["Shoplatitude"];
                        string Shoplongitude = (string)SqlDataReader["Shoplongitude"];

                        DateTime OrderDate = (DateTime)SqlDataReader["OrderDate"];
                        string OrderRound = (string)SqlDataReader["OrderRound"];
                        StateType State = (StateType)Enum.Parse(typeof(StateType), (string)SqlDataReader["State"]);
                        string QuestionResult = (string)SqlDataReader["QuestionResult"];
                        string SyatemRemark = (string)SqlDataReader["SyatemRemark"];

                        DateTime UpdateTime = (DateTime)SqlDataReader["UpdateTime"];
                        DateTime CreateTime = (DateTime)SqlDataReader["CreateTime"];

                        OrderItem OrderItem = new OrderItem(Id, MemberAccount, MemberName, MemberPhone, MemberArea, MemberSchool,
                            ShopId, ShopName, ShopPhone, ShopAddress, Shoplatitude, Shoplongitude, OrderDate, OrderRound,
                            State, QuestionResult, SyatemRemark,
                            UpdateTime, CreateTime);

                        OrderItems.Add(OrderItem);
                    }
                }
            }

            return OrderItems;
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

        public static OrderItem GetById(decimal Id)
        {
            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.CommandText = "SELECT "
                                   + " * "
                                   + "FROM "
                                   + " Order_OrderItem "
                                   + "WHERE "
                                   + " Id = @Id ";

            SqlCommand.Parameters.AddWithValue("Id", Id);

            return GetByDataReader(SqlCommand).FirstOrDefault();
        }

        public static int GetTotalAmount()
        {
            SqlCommand SqlCommand = new SqlCommand();

            SqlCommand.CommandText = "SELECT COUNT(*) FROM Order_OrderItem";

            return (int)GetByScalar(SqlCommand);
        }

        public static List<OrderItem> GetByPageIndex(int StartIndex, int EndIndex)
        {
            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.CommandText = "WITH a AS "
                                           + "( "
                                           + "SELECT "
                                           + " ROW_NUMBER() OVER( ORDER BY MemberName DESC ) AS RowNumber , * "
                                           + "FROM "
                                           + " Order_OrderItem "
                                           + ") "

                                           + "SELECT "
                                           + " * "
                                           + "FROM "
                                           + " a "
                                           + "WHERE "
                                           + " RowNumber BETWEEN " + StartIndex + " AND " + EndIndex;

            return GetByDataReader(SqlCommand);
        }

        public static List<OrderItem> GetBySearch(List<StateType> StateTypes, DateTime? StartDate, DateTime? EndDate)
        {
            if (StateTypes.Count == 0) { return new List<OrderItem>(); }

            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.CommandText = "WITH a AS "
                                           + "( "
                                           + "SELECT "
                                           + " ROW_NUMBER() OVER( ORDER BY MemberName DESC ) AS RowNumber , * "
                                           + "FROM "
                                           + " Order_OrderItem "
                                           + ") "

                                           + "SELECT "
                                           + " * "
                                           + "FROM "
                                           + " a "
                                           + "WHERE "
                                           + " State IN ('" + string.Join("','", StateTypes.Select(s => s.ToString())) + "') "
                                           + "AND "
                                           + " OrderDate >= @StartDate "
                                           + "AND "
                                           + " OrderDate <= @EndDate ";

            SqlCommand.Parameters.AddWithValue("StartDate", StartDate);
            SqlCommand.Parameters.AddWithValue("EndDate", EndDate);

            return GetByDataReader(SqlCommand);
        }

        public static List<OrderItem> GetByCreateTime(ShopItem Shop, DateTime StartTime, DateTime EndTime)
        {
            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.CommandText = "SELECT "
                                   + " * "
                                   + "FROM "
                                   + " Order_OrderItem "
                                   + "WHERE "
                                   + " ShopId = @ShopId "
                                   + "AND "
                                   + " CreateTime >= @StartDate "
                                   + "AND "
                                   + " CreateTime <= @EndDate";

            SqlCommand.Parameters.AddWithValue("ShopId", Shop.Id);
            SqlCommand.Parameters.AddWithValue("StartDate", StartTime);
            SqlCommand.Parameters.AddWithValue("EndDate", EndTime);

            return GetByDataReader(SqlCommand);
        }

        public static List<OrderItem> GetByOrderDate(string ShopId, DateTime StartTime, DateTime EndTime)
        {
            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.CommandText = "SELECT "
                                   + " * "
                                   + "FROM "
                                   + " Order_OrderItem "
                                   + "WHERE "
                                   + " ShopId = @ShopId "
                                   + "AND "
                                   + " OrderDate >= @StartTime "
                                   + "AND "
                                   + " OrderDate <= @EndTime";

            SqlCommand.Parameters.AddWithValue("ShopId", ShopId);
            SqlCommand.Parameters.AddWithValue("StartTime", StartTime);
            SqlCommand.Parameters.AddWithValue("EndTime", EndTime);

            return GetByDataReader(SqlCommand);
        }

        public static int GetRoundCount(string ShopId, DateTime OrderDate, string OrderRound)
        {
            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.CommandText = "SELECT "
                                   + " Count(*) "
                                   + "FROM "
                                   + " Order_OrderItem "
                                   + "WHERE "
                                   + " ShopId = @ShopId "
                                   + "AND "
                                   + " OrderDate = @OrderDate "
                                   + "AND "
                                   + " OrderRound = @OrderRound";

            SqlCommand.Parameters.AddWithValue("ShopId", ShopId);
            SqlCommand.Parameters.AddWithValue("OrderDate", OrderDate);
            SqlCommand.Parameters.AddWithValue("OrderRound", OrderRound);

            return (int)GetByScalar(SqlCommand);
        }

        public static Dictionary<string, int> GetRoundCount(string ShopId, DateTime OrderDate)
        {
            Dictionary<string, int> RoundCount = new Dictionary<string, int>();

            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                using (SqlCommand SqlCommand = SqlConnection.CreateCommand())
                {
                    SqlCommand.CommandText = "SELECT "
                                           + " OrderRound , Count(*) as Counter "

                                           + "FROM "
                                           + " Order_OrderItem "

                                           + "WHERE "
                                           + " ShopId = @ShopId "
                                           + "AND "
                                           + " OrderDate = @OrderDate "

                                           + "GROUP BY "
                                           + " OrderRound ";

                    SqlCommand.Parameters.AddWithValue("ShopId", ShopId);
                    SqlCommand.Parameters.AddWithValue("OrderDate", OrderDate);

                    SqlConnection.Open();

                    SqlDataReader SqlDataReader = SqlCommand.ExecuteReader();
                    while (SqlDataReader.Read())
                    {
                        string OrderRound = (string)SqlDataReader["OrderRound"];
                        int Counter = (int)SqlDataReader["Counter"];

                        RoundCount.Add(OrderRound, Counter);
                    }

                    return RoundCount;
                }
            }
        }

        public static OrderItem Get(string IdCarNumber, string Phone)
        {
            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.CommandText = "SELECT "
                                   + " * "
                                   + "FROM "
                                   + " Order_OrderItem "
                                   + "WHERE "
                                   + " MemberAccount = @MemberAccount "
                                   + "AND "
                                   + " MemberPhone = @MemberPhone ";

            SqlCommand.Parameters.AddWithValue("MemberAccount", IdCarNumber);
            SqlCommand.Parameters.AddWithValue("MemberPhone", Phone);

            return GetByDataReader(SqlCommand).FirstOrDefault();
        }

        public static void SaveTemp(OrderItem OrderItem)
        {
            HttpContext.Current.Session["Order_Form_TempOrderItem"] = OrderItem;
        }

        public static OrderItem GetTemp()
        {
            return (OrderItem)HttpContext.Current.Session["Order_Form_TempOrderItem"];
        }

        public static void Save(OrderItem OrderItem)
        {
            if (OrderItem.Id == -1) { OrderItem.Id = GetNewId(); }

            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                using (SqlCommand SqlCommand = SqlConnection.CreateCommand())
                {
                    SqlCommand.CommandText = "UPDATE "
                                             + " Order_OrderItem "
                                             + "SET "
                                             + " MemberAccount = @MemberAccount "
                                             + " ,MemberName = @MemberName "
                                             + " ,MemberPhone = @MemberPhone "

                                             + " ,MemberArea = @MemberArea "
                                             + " ,MemberSchool = @MemberSchool "

                                             + " ,ShopId = @ShopId "
                                             + " ,ShopName = @ShopName "
                                             + " ,ShopPhone = @ShopPhone "
                                             + " ,ShopAddress = @ShopAddress "
                                             + " ,Shoplatitude = @Shoplatitude "
                                             + " ,Shoplongitude = @Shoplongitude "

                                             + " ,OrderDate = @OrderDate "
                                             + " ,OrderRound = @OrderRound "

                                             + " ,State = @State "
                                             + " ,SyatemRemark = @SyatemRemark "

                                             + " ,QuestionResult = @QuestionResult "

                                             + " ,UpdateTime = @UpdateTime "
                                             + "WHERE "
                                             + " Id = @Id "

                                             + "IF @@ROWCOUNT = 0 "
                                             + "BEGIN "

                                             + "INSERT INTO "
                                             + " Order_OrderItem "
                                             + "( Id, MemberAccount, MemberName, MemberPhone, MemberArea, MemberSchool, ShopId, ShopName, ShopPhone, ShopAddress, Shoplatitude, Shoplongitude,"
                                             + " OrderDate, OrderRound, State, QuestionResult, SyatemRemark, UpdateTime, CreateTime) "
                                             + "VALUES "
                                             + "( @Id, @MemberAccount, @MemberName, @MemberPhone, @MemberArea, @MemberSchool, @ShopId, @ShopName, @ShopPhone, @ShopAddress, @Shoplatitude, @Shoplongitude,"
                                             + " @OrderDate, @OrderRound, @State, @QuestionResult, @SyatemRemark, @UpdateTime, @CreateTime) "

                                             + "END ";

                    SqlCommand.Parameters.AddWithValue("Id", OrderItem.Id);
                    SqlCommand.Parameters.AddWithValue("MemberAccount", OrderItem.MemberAccount);
                    SqlCommand.Parameters.AddWithValue("MemberName", OrderItem.MemberName);
                    SqlCommand.Parameters.AddWithValue("MemberPhone", OrderItem.MemberPhone);

                    SqlCommand.Parameters.AddWithValue("MemberArea", OrderItem.MemberArea);
                    SqlCommand.Parameters.AddWithValue("MemberSchool", OrderItem.MemberSchool);

                    SqlCommand.Parameters.AddWithValue("ShopId", OrderItem.ShopId);
                    SqlCommand.Parameters.AddWithValue("ShopName", OrderItem.ShopName);
                    SqlCommand.Parameters.AddWithValue("ShopPhone", OrderItem.ShopPhone);
                    SqlCommand.Parameters.AddWithValue("ShopAddress", OrderItem.ShopAddress);
                    SqlCommand.Parameters.AddWithValue("Shoplatitude", OrderItem.Shoplatitude);
                    SqlCommand.Parameters.AddWithValue("Shoplongitude", OrderItem.Shoplongitude);

                    SqlCommand.Parameters.AddWithValue("OrderDate", OrderItem.OrderDate);
                    SqlCommand.Parameters.AddWithValue("OrderRound", OrderItem.OrderRound);

                    SqlCommand.Parameters.AddWithValue("State", OrderItem.State.ToString());
                    SqlCommand.Parameters.AddWithValue("QuestionResult", OrderItem.QuestionResult);
                    SqlCommand.Parameters.AddWithValue("SyatemRemark", OrderItem.SyatemRemark);

                    SqlCommand.Parameters.AddWithValue("UpdateTime", OrderItem.UpdateTime);
                    SqlCommand.Parameters.AddWithValue("CreateTime", OrderItem.CreateTime);

                    SqlConnection.Open();
                    SqlCommand.ExecuteNonQuery();
                }
            }
        }

        public static void Remove(OrderItem OrderItem)
        {
            using (SqlConnection SqlConnection = ConnectionManager.GetConnection())
            {
                using (SqlCommand SqlCommand = SqlConnection.CreateCommand())
                {
                    SqlCommand.CommandText = "Delete "
                                           + " Order_OrderItem "
                                           + "WHERE "
                                           + " Id = @Id ";

                    SqlCommand.Parameters.AddWithValue("Id", OrderItem.Id);

                    SqlConnection.Open();
                    SqlCommand.ExecuteNonQuery();
                }
            }
        }

        static object LockNewIdObj = new object();
        private static decimal GetNewId()
        {
            lock (LockNewIdObj)
            {
                return (_DayOrderCounter += 1);
            }
        }
    }
}
