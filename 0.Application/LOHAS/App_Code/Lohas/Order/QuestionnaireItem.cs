using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lohas.Order
{
    public enum OptionType { 單選, 多選, 單行文字方塊 }

    public class QuestionnaireItem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ShortTitle { get; set; }
        public OptionType OptionType { get; set; }
        public List<string> Options { get; set; }
        public int Sort { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime CreateTime { get; set; }

        public QuestionnaireItem(string Title, string ShortTitle, OptionType OptionType)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Title = Title;
            this.ShortTitle = ShortTitle;
            this.OptionType = OptionType;
            this.Options = new List<string>();
            this.Sort = QuestionnaireItemManager.GetNewSort();
            this.UpdateTime = DateTime.Now;
            this.CreateTime = DateTime.Now;
        }

        internal QuestionnaireItem(string Id, string Title, string ShortTitle, OptionType OptionType, List<string> Options, int Sort, DateTime UpdateTime, DateTime CreateTime)
        {
            this.Id = Id;
            this.Title = Title;
            this.ShortTitle = ShortTitle;
            this.OptionType = OptionType;
            this.Options = Options;
            this.Sort = Sort;
            this.UpdateTime = UpdateTime;
            this.CreateTime = CreateTime;
        }
    }
}