using Lohas.Area;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Lohas.School
{
    public class SchoolItemManager
    {
        private static List<SchoolItem> _SchoolItemCache;

        public static void Initial()
        {
            _SchoolItemCache = SchoolItemAccessor.SelectAll()
                                .OrderByDescending(c => c.Sort)
                                .ThenByDescending(c=>c.UpdateTime)
                                .ToList();
        }

        public static int GetSort()
        {
            int Sort = 0;

            if (_SchoolItemCache.Count() != 0)
            { Sort = _SchoolItemCache.Max(s => s.Sort); }

            return Sort + 1;
        }

        public static List<SchoolItem> GetAll()
        {
            return _SchoolItemCache.ToList();
        }

        public static SchoolItem Get(string Id)
        {
            return _SchoolItemCache.FirstOrDefault(c => c.Id == Id);
        }

        public static SchoolItem GetByName(string Name)
        {
            return _SchoolItemCache.FirstOrDefault(c => c.Name == Name);
        }

        public static List<SchoolItem> GetByArea(AreaItem Area)
        {
            if (Area == null) { return new List<SchoolItem>(); }

            return _SchoolItemCache.Where(s => s.AreaItemId == Area.Id).ToList();
        }

        static object SaveObject = new object();
        public static void Save(SchoolItem SchoolItem)
        {
            lock (SaveObject)
            {
                if (SchoolItem.Id == "-1") { SchoolItem.Id = Guid.NewGuid().ToString(); }

                SchoolItem.UpdateTime = DateTime.Now;

                //更新資料庫
                SchoolItemAccessor.UpdateInsert(SchoolItem);

                //更新記憶體
                _SchoolItemCache.Remove(SchoolItem);
                _SchoolItemCache.Add(SchoolItem);

                _SchoolItemCache.OrderBy(b => b.Id)
                                .ThenByDescending(c => c.CreateTime)
                                .ToList();
            }
        }

        public static SchoolItem NameCheck(string Name)
        {
            return _SchoolItemCache.FirstOrDefault(s => s.Name == Name);
        }


        public static void Remove(SchoolItem SchoolItem)
        {
            //更新資料庫
            SchoolItemAccessor.Delete(SchoolItem);

            //更新記憶体
            _SchoolItemCache.Remove(SchoolItem);

        }

    }
}