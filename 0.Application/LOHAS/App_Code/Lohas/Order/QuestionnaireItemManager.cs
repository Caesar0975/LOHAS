using Lohas.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lohas.Order
{
    public class QuestionnaireItemManager
    {
        private static List<QuestionnaireItem> _QuestionnaireItemCache;

        public static void Initial()
        {
            _QuestionnaireItemCache = QuestionnaireItemAccessor.SelectAll()
                                .OrderBy(b => b.Sort)
                                .ThenByDescending(c => c.CreateTime)
                                .ToList();
        }

        public static List<QuestionnaireItem> GetAll()
        {
            return _QuestionnaireItemCache.ToList();
        }

        public static QuestionnaireItem GetById(string Id)
        {
            return _QuestionnaireItemCache.FirstOrDefault(s => s.Id == Id);
        }

        public static void Save(QuestionnaireItem QuestionnaireItem)
        {
            Save(new List<QuestionnaireItem>() { QuestionnaireItem });
        }
        public static void Save(List<QuestionnaireItem> QuestionnaireItems)
        {
            foreach (QuestionnaireItem QuestionnaireItem in QuestionnaireItems)
            {
                QuestionnaireItem.UpdateTime = DateTime.Now;
            }

            //更新資料庫
            QuestionnaireItemAccessor.UpdateInsert(QuestionnaireItems);

            //更新快取
            _QuestionnaireItemCache = _QuestionnaireItemCache.Union(QuestionnaireItems).ToList();
        }

        public static void Remove(QuestionnaireItem QuestionnaireItem)
        {
            //更新資料庫
            QuestionnaireItemAccessor.Delete(QuestionnaireItem);

            //更新記憶体
            _QuestionnaireItemCache.Remove(QuestionnaireItem);

        }

        public static int GetNewSort()
        {
            return (_QuestionnaireItemCache.Count == 0) ? 1 : _QuestionnaireItemCache.Max(s => s.Sort) + 1;
        }
    }
}
