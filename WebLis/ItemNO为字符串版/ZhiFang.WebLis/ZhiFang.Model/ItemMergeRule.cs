using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Model
{
    	//ItemMerge
    public class ItemMergeRule
    {
        public ItemMergeRule() { }

        /// <summary>
        /// ItemNo
        /// </summary>		
        private string _itemno;
        public string SuperGroupNoLikeKey { get; set; }
        public string ItemNo
        {
            get { return _itemno; }
            set { _itemno = value; }
        }
        /// <summary>
        /// Id
        /// </summary>		
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// SectionNo
        /// </summary>		
        private int? _sectionno;
        public int? SectionNo
        {
            get { return _sectionno; }
            set { _sectionno = value; }
        }
        /// <summary>
        /// SectionNo
        /// </summary>		
        private string _mergerulename;
        public string MergeRuleName
        {
            get { return _mergerulename; }
            set { _mergerulename = value; }
        }
        public bool SelectedFlag { get; set; }
        public string  itemMergeCName { get; set; }
    }
		   
}
