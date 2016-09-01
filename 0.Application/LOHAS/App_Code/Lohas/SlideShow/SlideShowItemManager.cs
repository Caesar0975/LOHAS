using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lohas.SlideShow
{
    public class SlideShowItemManager
    {
        private static string UploadPath = "/_Upload/SlideShow/";
        private static string PhysicalUploadPath = HttpContext.Current.Server.MapPath(UploadPath);

        public static string GetUploadPath()
        {
            return UploadPath;
        }

        public static string GetPhysicalUploadPath()
        {
            if (System.IO.Directory.Exists(PhysicalUploadPath) == false) { System.IO.Directory.CreateDirectory(PhysicalUploadPath); }

            return PhysicalUploadPath;
        }

        private static List<SlideShowItem> SlideShowItemCache;

        public static void Initial()
        {
            SlideShowItemCache = SlideShowItemAccessor.SelectAll()
                                .OrderBy(b => b.Sort)
                                .ThenByDescending(c => c.CreateTime)
                                .ToList();
        }

        internal static int GetSort()
        {
            int Sort = 0;

            if (SlideShowItemCache.Count() != 0)
            { Sort = SlideShowItemCache.Max(s => s.Sort); }

            return Sort + 1;
        }

        public static List<SlideShowItem> GetAll()
        {
            return SlideShowItemCache;
        }

        public static SlideShowItem Get(string Id)
        {
            return SlideShowItemCache.FirstOrDefault(c => c.Id == Id);
        }

        static object SaveObject = new object();
        public static void Save(SlideShowItem SlideShowItem)
        {
            SlideShowItem.UpdateTime = DateTime.Now;

            SlideShowItemCache.Remove(SlideShowItem);
            SlideShowItemCache.Add(SlideShowItem);
            SlideShowItemCache = SlideShowItemCache.OrderBy(b => b.Sort).ThenByDescending(c => c.CreateTime).ToList();

            //更新資料庫
            SlideShowItemAccessor.UpdateInsert(SlideShowItem);
        }

        public static void Remove(SlideShowItem SlideShowItem)
        {
            //更新資料庫
            SlideShowItemAccessor.Delete(SlideShowItem);

            //更新記憶体
            SlideShowItemCache.Remove(SlideShowItem);

            //刪除圖片
            if (string.IsNullOrWhiteSpace(SlideShowItem.Image) == false)
            {
                System.IO.File.Delete(SlideShowItemManager.GetPhysicalUploadPath() + SlideShowItem.Image);
            }
        }
    }
}
