using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Digitlab.Entity
{
    public class TargetModel
    {
        private double? dTarget;
        private double? dSD;
        private double? dCV;

        /// <summary>
        /// 计算靶值
        /// </summary>
        public double? Target
        {
            get { return dTarget; }
            set { dTarget = value; }
        }

        /// <summary>
        /// 计算SD
        /// </summary>
        public double? SD
        {
            get { return dSD; }
            set { dSD = value; }
        }

        /// <summary>
        /// 计算CV
        /// </summary>
        public double? CV
        {
            get { return dCV; }
            set { dCV = value; }
        }

    }
}
