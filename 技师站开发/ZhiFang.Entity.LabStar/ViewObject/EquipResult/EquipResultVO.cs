using System;

namespace ZhiFang.Entity.LabStar
{
    /// <summary>
    /// 通讯客户端信息类
    /// </summary>
    public class ClientComputerInfo
    {
        /// <summary>
        /// 计算机名称
        /// </summary>
        public string ComputerName { get; set; }
        /// <summary>
        /// 网卡地址
        /// </summary>
        public string MacAddress { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string IPAddress { get; set; }
        /// <summary>
        /// 检验结果文件名
        /// </summary>
        public string ComFileName { get; set; }
        /// <summary>
        /// 检验结果文件路径--预留
        /// </summary>
        public string ComFilePath { get; set; }

        public long? SComFileID { get; set; }
        public long? SEquipID { get; set; }
        public string SEquipName { get; set; }
        public long? SSectionID { get; set; }
        public string SSectionName { get; set; }
    }

    public class EquipResultBase
    {
        protected string _sampleNo;

        public int EquipNo { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public int SectionNo { get; set; }
        public int TestTypeNo { get; set; }
        public string ClientComputer { get; set; }
        public string ClientMac { get; set; }
        public string ClientIP { get; set; }

        public string SampleNo
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_sampleNo))
                    return "";
                else
                    return _sampleNo;
            }
            set { _sampleNo = value; }
        }


    }

    public class EquipResult : EquipResultBase
    {
        protected string _serialNo;
        protected string _serialField;

        public int ItemNo { get; set; }
        public string OriginalValue { get; set; }
        public string OriginalDesc { get; set; }
        public DateTime? ItemDate { get; set; }
        public DateTime? ItemTime { get; set; }
        public string SerialNo
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_serialNo))
                    return "";
                else
                    return _serialNo;
            }
            set { _serialNo = value; }
        }
        public string SerialField
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_serialField))
                    return "";
                else
                    return _serialField;
            }
            set { _serialField = value; }
        }

    }

    public class EquipGraphResult : EquipResultBase
    {
        protected string _serialNo;
        protected string _serialField;
        protected string _graphName;
        protected string _graphType;

        public string SerialNo
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_serialNo))
                    return "";
                else
                    return _serialNo;
            }
            set { _serialNo = value; }
        }
        public string SerialField
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_serialField))
                    return "";
                else
                    return _serialField;
            }
            set { _serialField = value; }
        }
        public string GraphBase64 { get; set; }
        public string GraphName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_graphName))
                    return "";
                else
                    return _graphName;
            }
            set { _graphName = value; }
        }
        public string GraphType
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_graphType))
                    return "";
                else
                    return _graphType;
            }
            set { _graphType = value; }
        }

    }

    public class EquipMemoResult : EquipResultBase
    {
        protected string _serialNo;
        protected string _serialField;
        protected string _formComment;

        public string FormComment
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_formComment))
                    return "";
                else
                    return _formComment;
            }
            set { _formComment = value; }
        }
        public string SerialNo
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_serialNo))
                    return "";
                else
                    return _serialNo;
            }
            set { _serialNo = value; }
        }
        public string SerialField
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_serialField))
                    return "";
                else
                    return _serialField;
            }
            set { _serialField = value; }
        }

    }

    public class EquipQCResult
    {
        protected string _userValue;
        protected string _originalValue;
        protected string _originalDesc;

        public int QCItemNo { get; set; }
        public DateTime ReceiveDate { get; set; }
        public DateTime ReceiveTime { get; set; }
        public int? QCDataType { get; set; }
        public int? RuleIndex { get; set; }
        public int? IfUse { get; set; }
        public string UserValue
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_userValue))
                    return "";
                else
                    return _userValue;
            }
            set { _userValue = value; }
        }
        public string OriginalValue
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_originalValue))
                    return "";
                else
                    return _originalValue;
            }
            set { _originalValue = value; }
        }

        public string OriginalDesc
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_originalDesc))
                    return "";
                else
                    return _originalDesc;
            }
            set { _originalDesc = value; }
        }

    }

    public class EquipResultGroupBy
    {
        public int EquipNo { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public int SectionNo { get; set; }
        public int TestTypeNo { get; set; }
        public string SampleNo { get; set; }
        public string SerialNo { get; set; }
        public long LongGUID { get; set; }
    }
}
