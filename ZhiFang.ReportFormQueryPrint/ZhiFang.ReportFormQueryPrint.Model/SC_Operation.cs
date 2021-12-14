using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.ReportFormQueryPrint.Model
{
    #region ReportFormFull
    public class SC_Operation
    {
        #region Member Variables
        protected long _SCOperationID;
        protected long _bobjectID;
        protected long _type;
        protected string _memo;
        protected int _dispOrder;
        protected bool _isUse;
        protected long _creatorID;
        protected string _creatorName;
        protected DateTime? _DataAddTime;
        protected DateTime? _dataUpdateTime;
        protected string _typeName;
        protected string _businessModuleCode;


        #endregion

       

        #region Public Properties

        public virtual long BobjectID
        {
            get { return _bobjectID; }
            set { _bobjectID = value; }
        }
        public virtual long SCOperationID
        {
            get { return _SCOperationID; }
            set { _SCOperationID = value; }
        }

        public virtual long Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public virtual string Memo
        {
            get { return _memo; }
            set
            {
                _memo = value;
            }
        }

        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        public virtual bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
        }

        public virtual long CreatorID
        {
            get { return _creatorID; }
            set { _creatorID = value; }
        }

        public virtual string CreatorName
        {
            get { return _creatorName; }
            set
            {
                _creatorName = value;
            }
        }
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }



        public virtual DateTime? DataAddTime
        {
            get { return _DataAddTime; }
            set { _DataAddTime = value; }
        }

        public virtual string TypeName
        {
            get { return _typeName; }
            set
            {
                _typeName = value;
            }
        }

        public virtual string BusinessModuleCode
        {
            get { return _businessModuleCode; }
            set
            {
                _businessModuleCode = value;
            }
        }


        #endregion
    }
    #endregion
}
