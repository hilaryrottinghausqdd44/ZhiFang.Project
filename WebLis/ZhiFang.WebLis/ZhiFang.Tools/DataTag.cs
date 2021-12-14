using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;

namespace ZhiFang.Tools
{
    public class DataTag
    {

        /// <summary>
        /// 定义数据源类型
        /// </summary>
        public enum DatabaseType
        {
            Oracle,
            SQLServer,
            Sybase,
            Access,
            Excel,
            DB2,
            OTHER,
            XML
        }


        /// <summary>
        /// 定义字段类型
        /// </summary>
        public enum FieldType
        {
            IDENTITY, //系统自动加1的整型
            Integer,  //整型
            Long,     //长整型
            Number,   //数字型
            Float,    //符点型
            Money,    //货币型
            GUID,     //自动生成的唯一标识，36位的字符型
            Varchar,  //可变字符
            News,     //新闻
            File,     //文件
            Char,     //字符
            Date,     //日期型，本类型一般就包括时间
            Time,     //时间型
            DateTime, //日期时间型
            BLOB,     //大二进制字段，如图象
            CLOB,     //大文本字段
            NONE      //未定义
        }



        /// <summary>
        /// 数据库的操作方法
        /// </summary>
        public enum OPID
        {
            INSERT,
            UPDATE,
            DELETE,
            SELECT,
            EXECUTE,//运行，比如只运行某个SQL脚本
            VIEW,//数据视图：和修改差不多
            NONE
        }



        /// <summary>
        /// 权限角色类型
        /// </summary>
        public enum PowerType
        {
            department,//部门
            post,      //岗位
            duty,      //职位
            employee,  //员工
            NONE
        }


        /// <summary>
        /// 权限具体的操作方法，即具有什么功能
        /// </summary>
        public enum PowerOPID
        {
            BatchINSERT,//批量增加
            INSERT,//增加
            DELETE,//审处
            COPY,  //复制
            UPDATE,//修改
            SAVE,  //保存 
            CANCEL,//取消
            OutIn, //导入导出
            NONE
        }


        /// <summary>
        /// 权限具有的操作方法，即具有什么功能
        /// </summary>
        public enum HasPowerOPID
        {
            HasBatchINSERT,//批量增加
            HasINSERT,//增加
            HasDELETE,//审处
            HasCOPY,  //复制
            HasUPDATE,//修改
            HasSAVE,  //保存 
            HasCANCEL,//取消
            HasOutIn, //导入导出
            HasNONE
        }



        /// <summary>
        /// 统计图的类型
        /// </summary>
        public enum ChartTypeSTAT
        {
            Combo,//直方图：垂直直方图，分组(默认)
            ComboHorizontal,//直方图：水平直方图，分组
            ComboSideBySide,//直方图：水平直方图，并排
            Pies,//饼图
            PiesSplit,//饼图(拆分的饼图)
            PiesMUL,//多饼图
            PiesSplitMUL,//多饼图(拆分的多饼图)
            Spline, //折线图
            Line, //直线图
            AreaLine, //二维面积图
            Area, //三维面积图
            Ring,//环型图
            Column,//柱型图
            NONE //未定义
        }


        /// <summary>
        /// 统计图阴影效果的分类
        /// </summary>
        public enum ChartShadingEffectSTAT
        {
            One,
            Two,
            Three,//立体的，默认值
            Four,
            Five
        }


        /// <summary>
        /// 统计项的统计类别，如：记录数、和等
        /// </summary>
        public enum DataSeriesType
        {
            Count, //个数，即记录数
            Sum//求和(默认)
        }


        /// <summary>
        /// 统计图可输出的图象格式
        /// </summary>
        public enum ImageFormatSTAT
        {
            Bmp,
            Jpg,
            Pdf,//默认
            Png,
            Tif
        }



        /// <summary>
        /// 统计函数的类别,如日期型可以按年\月进行统计等
        /// </summary>
        public enum GroupByType
        {
            Year,
            Quarter,
            Month,
            Week,
            Day,
            YearMonth,
            NONE
        }


        /// <summary>
        /// 存储过程参数的输入输出方向
        /// </summary>
        public enum ParamDirection
        {
            NONE,         //参数为空，未定义
            Input,        //参数是输入方向
            InputOutput,  //参数是输出参数
            Output,       //参数既能输入，也能输出
            ReturnValue   //参数表示诸如存储过程、内置函数或用户定义函数之类的操作的返回值
        }


        #region 自定义的数据类

        /// <summary>
        /// 定义一个统计字段的参数,一般结合STATData的GroupBy使用
        /// </summary>
        public class GroupByParam
        {
            public GroupByType Type;//统计函数的类别(类型为GroupByType),默认为NONE,即直接按字段进行统计
            public long From;//是日期型时要转换成长整型,默认为-1
            public long To;//是日期型时要转换成长整型,默认为-1

            public GroupByParam()
            {
            }

        }




        /// <summary>
        /// 统计图数据类
        /// </summary>
        public class STATData
        {
            //数据库信息
            public DataBaseLoginInfo dataBaseLoginInfo;
            //表基本信息
            public TableConfigInfo tableConfigInfo;
            //统计表所在的数据库名称
            public string DatabaseName;
            //统计表
            public string TableName;
            //数据系列:即统计项,如统计个数、总和等
            public System.Collections.Hashtable DataSeries;//数据存放格式为:统计字段名称,字段描述
            public System.Collections.Hashtable DataSeriesType;//数据存放格式为:统计字段,统计的类型DataSeriesType
            //统计分类项：即根据什么来统计等，如“关注程度”、“性别”(现在只有一个分类)
            public System.Collections.Hashtable GroupBy;//存放格式为:字段名称,字段描述
            public System.Collections.Hashtable groupByParam;//存放格式为:字段名称,字段参数(类型为GroupByParam)
            public System.Collections.Queue GroupByQueue;//存放统计分类字段的字段名称次序（先进先出）
            //可查询字段
            public System.Collections.Hashtable QueryField;//数据存放格式为:字段名称,字段描述
            public System.Collections.Hashtable QueryFieldType;//数据存放格式为:字段字段,字段类型FieldType
            //统计图的类型
            public ChartTypeSTAT chartType;
            public string ChartTypeXML;//在XML上显示的类型
            //统计图的效果（阴影）
            public ChartShadingEffectSTAT chartShadingEffect;
            public ImageFormatSTAT imageFormat;//图象的输出格式
            //统计图的名称
            public string Name;//该统计图保存的名称,本名称在同一个配置文件里是唯一的
            //统计图名称下的分组名称
            public string StatGroupName;//用来在显示统计图的标题时在标题后面加上作为标识
            //统计图分类：用来区分某个统计图的默认分类，如果该属性没有定义或其内容为“”，则命名为“未分类”
            public string ChartSort;
            //统计图的标题
            public string Title;
            //统计图的坐标（X,Y）标题
            public string XAxisTitle;//横坐标标题
            public string YAxisTitle;//纵坐标标题
            //是否显示结果
            public bool ShowValue;
            //维度变换:是否把第一分类项和第二分类项进行交换
            public bool ConvertXY;
            //图的尺寸
            public int Width;//宽：像素
            public int Height;//高：像素
            public string Where;//查询条件
            public System.Data.DataSet StatResult;//从数据库中统计出来的结果

            public STATData()
            {
            }
        }



        /// <summary>
        /// 存储过程类
        /// </summary>
        public class StoredProc
        {
            public string StoredProcName;//存储过程名称
            public OPID OPID;//运行方式，默认为执行，即不是查询数据
            public Hashtable StoredProcParam;//存储过程参数内容的哈希表【参数名称，参数值】，如果参数没有值传null
            public Hashtable StoredProcParamType;//存储过程参数类型的哈希表【参数名称，参数类型（取字段类型枚举FieldType）】，如果参数没有值传null
            public Hashtable StoredProcParamDirection;//存储过程参数方向的哈希表【参数名称，存储过程参数的输入输出方向枚举类型ParamDirection】，如果没有指定方向(传null值)，则默认为输入
            public Object StoredProcReturnValue;//存储过程返回值，返回对象；
            public Hashtable StoredProcParamValue;//存储过程参数内容的哈希表【参数名称，参数的返回值】，参数的返回值类型为object类型

            public StoredProc()
            {
            }

        }




        /// <summary>
        /// 操作权限类
        /// </summary>
        public class HasPower
        {
            public bool HasBatchINSERT;//批量增加
            public bool HasINSERT;//增加
            public bool HasDELETE;//审处
            public bool HasCOPY;  //复制
            public bool HasUPDATE;//修改
            public bool HasSAVE;  //保存 
            public bool HasCANCEL;//取消
            public bool HasOutIn; //导入导出
            public bool HasALL;  //具有所有的权限
            public bool HasNONE;  //什么权限都没有

            public HasPower()
            {
            }

        }



        /// <summary>
        /// 针对OA配置的数据查询类
        /// </summary>
        public class SQLSelectData
        {
            //应用系统信息
            public string SystemName;//当前操作的数据库的名称
            //数据库信息
            public string DatabaseName;//当前操作的数据库的名称
            public string TableName;//当前操作的表的名称
            public string ConnectionString;//数据库连接串:如果指定了连接串，直接从连接串中查询
            //查询语句:完整的查询语句,如果设置本语句,则仅仅执行本语句后返回
            public string SelectSQL;//查询语句
            //查询字段语句
            public string SelectFields;//查询字段语句
            //查询条件
            public string Where;//查询的条件语句
            //排序语句:排序字段列表
            public string OrderBy;//排序语句
            //查询信息
            public int PageStart;//返回记录的起始页
            public int PageSize;//返回记录的每页记录数
            //返回的记录信息
            public DataSet RecordsetDataSet;
            public DataTable RecordsetDataTable;
            public string RecordsetXML;

            //构造函数
            public SQLSelectData()
            {
            }

        }




        /// <summary>
        /// 通用的数据查询类
        /// </summary>
        public class SQLData
        {
            //用户信息
            public UserLoginInfo userLoginInfo;
            //数据库信息
            public DataBaseLoginInfo dataBaseLoginInfo;
            //表基本信息
            public TableConfigInfo tableConfigInfo;

            public string ParentName; //父亲结点的名称
            public string ParentGuid; //父亲结点的GUID
            public string ParentPath; //父亲结点的路径
            public string oldCode;	  //该节点修改前的code
            public string newCode;	  //该节点修改后的code
            //
            public string Opid; //当前操作的类别
            public string Action;//当前操作的动作
            public string Id; //当前操作的记录GUID
            public string TableName;//当前操作的表的名称
            public string PrimaryKey;//主关键字段，如果是多个字段，用逗号分隔（最好用单主键）
            public string TitleFieldName;//标题字段的名称
            public string TitleFieldValue;//标题字段的内容，用来做操作审计用
            public string SqlType;//SQL脚本的类型，比如：INSERT、UPDATE、DELETE
            public string SqlModal;    //运行脚本模板
            public string SqlExecute;    //运行的具体的SQL脚本
            public Hashtable SqlParams; //参数表，存放[字段名称，字段内容]
            public Hashtable WhereParams;//条件表，存放[字段名称，字段内容]
            public Hashtable OrderParams;//排序条件表，存放[字段名称，排序方式]
            public Hashtable FieldType;//字段类型表，存放[字段名称，字段类型]
            public DataTable RecordSet; //查询结果
            public string RecordXML; //查询结果XML
            public int RecordNumber; //满足查询结果的记录数，初始化为-100
            public int RecordBegin; //满足查询结果的起始记录号，初始化为-100
            public int RecordEnd; //满足查询结果的截止记录号，初始化为-100
            public int PageNumber; //每页返回的记录数，初始化为20
            public bool IsAudit; //是否要做操作审计，初始化为false
            public string AuditContent; //审计操作内容

            public SQLData()
            {
            }

        }



        /// <summary>
        /// 用户登录信息类
        /// </summary>
        public class UserLoginInfo
        {
            public string UserName;//用户名，即ID
            public string PassWord; //密码
            public string Token; //token
            public string Roles; //该用户所属的角色组，如果有多个，用逗号分隔
            public string ResGuid; //资源的GUID
            public string ResType; //资源的类型
            public string ResPowerValue; //用户对该资源权限
            public DateTime LoginTime;//登录时间
            public UserLoginInfo()
            { }

        }


        /// <summary>
        /// 数据库连接的基本信息类
        /// </summary>
        public class DataBaseLoginInfo
        {
            public DatabaseType DatabaseType;//数据库类型
            public string IP; //IP地址
            public string TCP; //端口号
            public string DatabaseName; //数据库名称
            public string UserName;//用户名，即ID（数据库的owner）
            public string PassWord; //密码
            public string DefaultTableSpace; //默认的表空间
            public string ConnectionString;//连接串
            public string ConfigXML;//配置信息的XML
            public string SelectTableXML;//选择的表及其结构的XML
            public DataBaseLoginInfo()
            {
            }
        }


        /// <summary>
        /// 表结构的基本信息类，目的：完成对表的增加、修改、删除等操作
        /// 记录表及其父表的基本情况，一个表和设置多个用来做为主关键字的主键字段PrimaryKeyTable，
        /// 表定义的主键字段PrimaryKeyTable和该表的外键ForeignKeyTable联合作为该表的主键PrimaryKey
        /// 一个表必须定义其主键字段，一个表默认只有一个主表
        /// 所有存放到表中的字段类型，必须都转换为枚举类型FieldType
        /// 定义的主键字段和外键字段，都是从TablesRelation中进行提取
        /// </summary>
        public class TableConfigInfo
        {
            //操作方法，如：INSERT、UPDATE、DELETE、SELECT
            public OPID OPID;
            //数据库信息
            public DataBaseLoginInfo dataBaseLoginInfo;
            //相关联表的信息：即该表的主表名称
            public string PrimaryTable;//当前表相关联的主表名称
            //表信息：当前表的信息
            public string TableName;//用户操作的主表名称
            public string TableDesc;//用户操作的主表的描述名称
            //外键字段信息
            public Hashtable ForeignKey;//外来关键字表：存放为“外来关键字名称”、“外来关键字值”，即字段名称、字段内容
            public Hashtable ForeignKeyType;//外来关键字类型表：存放为“外来关键字名称”、“外来关键字类型”，即字段名称、字段类型
            public Hashtable ForeignKeyRelation;//外来关键字表：存放为“外来关键字名称”、“主表的字段名称”，即字段名称、主表的字段名称
            public System.Collections.Stack ForeignKeyStack;//外来关键字堆栈表：存放为“外来关键字名称”的先后次序，先进去的在后面
            //主键字段信息
            public Hashtable PrimaryKey;//主键表：存放为“主关键字名称”、“主关键字内容”，即字段名称、字段内容
            public Hashtable PrimaryKeyType;//主键表：存放为“主关键字名称”、“主关键字类型”，即字段名称、字段类型
            //该表所有定义的字段信息
            public Hashtable FieldNameDesc;//主表的字段名称（字段名称，字段描述）
            public Hashtable FieldNameType;//主表的字段名称（字段名称，字段类型类型为我们定义的枚举类型FieldType）
            public Hashtable FieldNameDictionary;//主表的字段名称（字段名称，该字段定义的数据字典(在配置文件里定义)）
            public Hashtable FieldNameValue;//主表的字段名称（字段名称，字段内容）
            //标题
            public string TitleField;//从XML取到的显示到Tree的字段，用来设置ListView 的Item内容
            public string Title; //从XML取到的显示到Tree的字段对应的标题，用来设置ListView 的column标题

        }


        /// <summary>
        /// ListView数据类:
        /// 存放FieldDefine.XML定义的字段和方法，包括
        /// 在ListView显示的字段，ListView上的上下文菜单（同时显示到ToolsBar中）
        /// 当选中ListView的某条记录时，将其内容更新(值)：即保存其内容
        /// </summary>
        public class ListViewData
        {
            //与树型结构有关的属性
            public string ParentName; //父亲结点的名称
            public string ParentGuid; //父亲结点的GUID
            public string ParentPath; //父亲结点的路径code
            public string ParentPathName; //父亲结点路径名称
            //关于表的声明
            public string Opid;  //对应的操作类型标识
            public string TableName;//对应的表名称
            public string TableDesc;//对应的表描述
            public string PrimaryKey;//主关键字段，如果是多个字段，用逗号分隔
            public string TitleFieldName;//标题字段的名称
            public string ParentFieldName;//父亲字段的名称，用来查询子结点用
            public string ParentFieldValue;//父亲字段的值，添加、修改、删除子类时用
            public string SQL;//运行的SQL语句
            //操作内容的声明
            public string TitleFieldValue;//标题字段的内容
            public string ContentXML;//保存当然操作的内容对应的XML
            public Hashtable ContentTable;//保存当然操作的内容：用哈希表存放（字段名称，字段内容）
            public string Id;  //用户所做操作的内容的ID
            public int FieldNum;//字段的个数
            public int MenuNum;//菜单的个数
            //public int RightMenuNum;//右边菜单的个数
            public int LabelLength;//标签的最大长度，即字段输入提示的最大宽度
            //操作动作的声明
            public string Action;  //用户所做的操作,取name的 值
            public string ActionDesc;  //用户所做操作的描述,取description的 值
            public bool IsRightAction;//用户选择的菜单是否是右边（ListView）的，默认为右边
            //字段的声明
            public object[] DictObject; //字典字段，字段的显示内容和数据库里存放的内容数组[ShowField,SaveField]
            public bool[] IsShowInListView; //是否显示到ListView
            public bool[] IsShowInForm; //是否显示到Form，即作为录入字段
            public bool[] IsReadOnly; //只读，即显示到Form中到不能修改
            public string[] FieldDesc;//字段标题
            public string[] FieldName;//字段名称
            public string[] FieldType;//字段类型
            public int[] FormLength;//字段显示到FORM长度，单位是象素
            public int[] ListViewLength;//字段显示到ListView的长度，单位是象素
            public string[] FieldContent;//字段内容
            public Hashtable FieldContentTable;//字段内容：用哈希表存放（字段名称，字段内容）
            public string[] FieldStyle;//字段的显示风格，即用什么控件显示，分别为memo,select等等
            public string[] FieldRegex;//正则表达式
            public string[] FieldAnchor;//字段的抛锚方法，=right则放在上一个的右边，否则放在上一个的下面
            public bool[] FieldMustInput;//是否是必填字段
            //菜单的声明
            public string[] MenuName;//由actions定义的菜单项，一般作为ListView的上下文菜单
            public string[] MenuDesc;//由actions定义的菜单的描述
            public string[] MenuIcon;//由actions定义的菜单的图标

            public ListViewData()
            {
            }

        }


        /// <summary>
        /// 树结点数据类:
        /// TreeView结点增加的一些属性，存放到结点Node的TAG中
        /// </summary>
        public class TreeNodeData
        {
            //当前操作表的信息
            public TableConfigInfo tableConfigInfo;
            //父结点的属性
            public string ParentOpid;//用户操作的项目的父OPID
            public string ParentPath;//该结点的父亲结点的路径path
            //结点信息
            public string OPID;//操作方法
            public string ID;//该结点ID
            public string NodeName;//该结点名称name
            public string NodePath;//该结点路径path
            //扩展信息
            public int OpenIcon;//该结点在打开状态下的图标的索引值
            public int CloseIcon;//该结点在关闭状态下的图标的索引值
            public int Depth;//树的深度，根为1，根的子频道为2，依次类推
            public bool IsTree;//该结点是否是棵树
            public bool IsExpand;//该接点是否已经展开，默认为false,用在用户点击“+”号时
            public bool IsLeaf;//判断是否由下一级，如果有，则="false"，否则为"true"
            public TreeNodeData()
            {
            }

        }
        /// <summary>
        /// ComboBox数据类
        /// </summary>
        public class ComboBoxData
        {
            public int ItemNum;//个数
            public string[] SaveValue;//保存的值
            public string[] ShowValue;//显示的值
            public bool[] IsDefault;  //是否是默认值
            public System.Collections.Hashtable ShowSaveTable;//[显示的值，保存的值]
            public System.Collections.Hashtable SaveShowTable;//[保存的值，显示的值]
            public ComboBoxData()
            { }
            public ComboBoxData(int itemNum, string[] saveValue, string[] showValue, bool[] isDefault, Hashtable showSaveTable, Hashtable saveShowTable)
            {
                ItemNum = itemNum;
                SaveValue = saveValue;
                ShowValue = showValue;
                IsDefault = isDefault;
                ShowSaveTable = showSaveTable;
                SaveShowTable = saveShowTable;
            }
        }
        /// <summary>
        /// 选择字段信息类
        /// </summary>
        public class SelectFieldInfo
        {
            public string fieldName;
            public bool isCheck;
            public bool isKey;
            public bool isIndex;
            public string fieldType;
            public string fieldEName;

            public SelectFieldInfo()
            { }

            public SelectFieldInfo(string fieldName, bool isCheck, bool isKey, bool isIndex, string fieldType, string fieldEName)
            {
                this.fieldName = fieldName;
                this.isCheck = isCheck;
                this.isKey = isKey;
                this.isIndex = isIndex;
                this.fieldType = fieldType;
                this.fieldEName = fieldEName;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public class ColumnSelectedInfo
        {
            public string titleName;//
            public string height;
            public string inputFunction;
            public string type; //字段类型
            public int order;  //排序
            //public string onDblClick;
            public ColumnSelectedInfo()
            {
            }

            public ColumnSelectedInfo(string titleName, string height, string inputFunction, string type, int order)
            {
                this.titleName = titleName;
                this.height = height;
                this.inputFunction = inputFunction;
                this.type = type;
                this.order = order;


            }

            public class OrderComparer : IComparer
            {
                int IComparer.Compare(Object x, Object y)
                {
                    if (((ColumnSelectedInfo)x).order < ((ColumnSelectedInfo)y).order)
                        return -1;

                    if (((ColumnSelectedInfo)x).order == ((ColumnSelectedInfo)y).order)
                        return 0;
                    else
                        return 1;
                }
            }//end OrderComparer
        }
        /// <summary>
        /// 应用系统数据类
        /// </summary>
        public class AppSystemData
        {
            //操作方法
            public OPID OPID;
            //配置的参数
            public string SystemName;//应用系统名称
            public string DataBaseName;//对应的数据库名称
            public string TableName;//对应的数据库表(模块的主表)
            public string SelectFields;//选择的字段(在数据列表中显示的字段)
            public string PrimaryKeySQL;//当前修改的记录的主键查询表达式。条件与条件之间用大写的 “ AND ”分隔
            public string ForeignKeySQL;//关联的外键查询表达式。条件与条件之间用大写的 “ AND ”分隔
            public string WhereSQL;//固定的查询条件
            public string SQL;//完整的查询语句(如果定义了本语句,则只运行本语句,别的设置与查询有关的参数都不起作用)
            //界面关联参数
            public string ModalName;//模块名称
            public string ModalID;//模块标识(用做框架的id和name
            public string PrimaryFieldName;//关联字段对应的主表的字段名称 
            public string RelationModalName;//关联的主模块名称
            public string RelationFieldName;//关联的字段名称
            public string RelationFieldValue;//关联的字段内容
            //页面数据参数
            public int PageStart;//起始页
            public int PageMode;//分页方式(-1 不分页,0 全部页,1 只要第一页,2\3)
            public int PageSize;//每页显示的记录数(默认为20)
            public int RecordCount;//满足条件的记录数
            public int PageCount;//总页数
            public DataSet DS;//返回的结果集
            //排序字段SQL
            public string OrderBySQL;
            //样式文件
            public string CSSFile;
            public AppSystemData()
            {
            }
        }
        #endregion 自定义的数据类
    }
}