using System;
using System.Collections;
using System.Xml;
using System.Data;

namespace ZhiFang.WebLisService.clsCommon
{


    #region �Զ��������ö������




    /// <summary>
    /// ��������Դ����
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
    /// �����ֶ�����
    /// </summary>
    public enum FieldType
    {
        IDENTITY, //ϵͳ�Զ���1������
        Integer,  //����
        Long,     //������
        Number,   //������
        Float,    //������
        Money,    //������
        GUID,     //�Զ����ɵ�Ψһ��ʶ��36λ���ַ���
        Varchar,  //�ɱ��ַ�
        News,     //����
        File,     //�ļ�
        Char,     //�ַ�
        Date,     //�����ͣ�������һ��Ͱ���ʱ��
        Time,     //ʱ����
        DateTime, //����ʱ����
        BLOB,     //��������ֶΣ���ͼ��
        CLOB,     //���ı��ֶ�
        NONE      //δ����
    }



    /// <summary>
    /// ���ݿ�Ĳ�������
    /// </summary>
    public enum OPID
    {
        INSERT,
        UPDATE,
        DELETE,
        SELECT,
        EXECUTE,//���У�����ֻ����ĳ��SQL�ű�
        VIEW,//������ͼ�����޸Ĳ��
        NONE
    }



    /// <summary>
    /// Ȩ�޽�ɫ����
    /// </summary>
    public enum PowerType
    {
        department,//����
        post,      //��λ
        duty,      //ְλ
        employee,  //Ա��
        NONE
    }


    /// <summary>
    /// Ȩ�޾���Ĳ���������������ʲô����
    /// </summary>
    public enum PowerOPID
    {
        BatchINSERT,//��������
        INSERT,//����
        DELETE,//��
        COPY,  //����
        UPDATE,//�޸�
        SAVE,  //���� 
        CANCEL,//ȡ��
        OutIn, //���뵼��
        NONE
    }


    /// <summary>
    /// Ȩ�޾��еĲ���������������ʲô����
    /// </summary>
    public enum HasPowerOPID
    {
        HasBatchINSERT,//��������
        HasINSERT,//����
        HasDELETE,//��
        HasCOPY,  //����
        HasUPDATE,//�޸�
        HasSAVE,  //���� 
        HasCANCEL,//ȡ��
        HasOutIn, //���뵼��
        HasNONE
    }



    /// <summary>
    /// ͳ��ͼ������
    /// </summary>
    public enum ChartTypeSTAT
    {
        Combo,//ֱ��ͼ����ֱֱ��ͼ������(Ĭ��)
        ComboHorizontal,//ֱ��ͼ��ˮƽֱ��ͼ������
        ComboSideBySide,//ֱ��ͼ��ˮƽֱ��ͼ������
        Pies,//��ͼ
        PiesSplit,//��ͼ(��ֵı�ͼ)
        PiesMUL,//���ͼ
        PiesSplitMUL,//���ͼ(��ֵĶ��ͼ)
        Spline, //����ͼ
        Line, //ֱ��ͼ
        AreaLine, //��ά���ͼ
        Area, //��ά���ͼ
        Ring,//����ͼ
        Column,//����ͼ
        NONE //δ����
    }


    /// <summary>
    /// ͳ��ͼ��ӰЧ���ķ���
    /// </summary>
    public enum ChartShadingEffectSTAT
    {
        One,
        Two,
        Three,//����ģ�Ĭ��ֵ
        Four,
        Five
    }


    /// <summary>
    /// ͳ�����ͳ������磺��¼�����͵�
    /// </summary>
    public enum DataSeriesType
    {
        Count, //����������¼��
        Sum//���(Ĭ��)
    }


    /// <summary>
    /// ͳ��ͼ�������ͼ���ʽ
    /// </summary>
    public enum ImageFormatSTAT
    {
        Bmp,
        Jpg,
        Pdf,//Ĭ��
        Png,
        Tif
    }



    /// <summary>
    /// ͳ�ƺ��������,�������Ϳ��԰���\�½���ͳ�Ƶ�
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
    /// �洢���̲����������������
    /// </summary>
    public enum ParamDirection
    {
        NONE,         //����Ϊ�գ�δ����
        Input,        //���������뷽��
        InputOutput,  //�������������
        Output,       //�����������룬Ҳ�����
        ReturnValue   //������ʾ����洢���̡����ú������û����庯��֮��Ĳ����ķ���ֵ
    }


    #endregion �Զ��������ö������



    #region �Զ����������






    /// <summary>
    /// ����һ��ͳ���ֶεĲ���,һ����STATData��GroupByʹ��
    /// </summary>
    public class GroupByParam
    {
        public GroupByType Type;//ͳ�ƺ��������(����ΪGroupByType),Ĭ��ΪNONE,��ֱ�Ӱ��ֶν���ͳ��
        public long From;//��������ʱҪת���ɳ�����,Ĭ��Ϊ-1
        public long To;//��������ʱҪת���ɳ�����,Ĭ��Ϊ-1

        public GroupByParam()
        {
        }

    }




    /// <summary>
    /// ͳ��ͼ������
    /// </summary>
    public class STATData
    {
        //���ݿ���Ϣ
        public DataBaseLoginInfo dataBaseLoginInfo;
        //�������Ϣ
        public TableConfigInfo tableConfigInfo;
        //ͳ�Ʊ����ڵ����ݿ�����
        public string DatabaseName;
        //ͳ�Ʊ�
        public string TableName;
        //����ϵ��:��ͳ����,��ͳ�Ƹ������ܺ͵�
        public System.Collections.Hashtable DataSeries;//���ݴ�Ÿ�ʽΪ:ͳ���ֶ�����,�ֶ�����
        public System.Collections.Hashtable DataSeriesType;//���ݴ�Ÿ�ʽΪ:ͳ���ֶ�,ͳ�Ƶ�����DataSeriesType
        //ͳ�Ʒ����������ʲô��ͳ�Ƶȣ��硰��ע�̶ȡ������Ա�(����ֻ��һ������)
        public System.Collections.Hashtable GroupBy;//��Ÿ�ʽΪ:�ֶ�����,�ֶ�����
        public System.Collections.Hashtable groupByParam;//��Ÿ�ʽΪ:�ֶ�����,�ֶβ���(����ΪGroupByParam)
        public System.Collections.Queue GroupByQueue;//���ͳ�Ʒ����ֶε��ֶ����ƴ����Ƚ��ȳ���
        //�ɲ�ѯ�ֶ�
        public System.Collections.Hashtable QueryField;//���ݴ�Ÿ�ʽΪ:�ֶ�����,�ֶ�����
        public System.Collections.Hashtable QueryFieldType;//���ݴ�Ÿ�ʽΪ:�ֶ��ֶ�,�ֶ�����FieldType
        //ͳ��ͼ������
        public ChartTypeSTAT chartType;
        public string ChartTypeXML;//��XML����ʾ������
        //ͳ��ͼ��Ч������Ӱ��
        public ChartShadingEffectSTAT chartShadingEffect;
        public ImageFormatSTAT imageFormat;//ͼ��������ʽ
        //ͳ��ͼ������
        public string Name;//��ͳ��ͼ���������,��������ͬһ�������ļ�����Ψһ��
        //ͳ��ͼ�����µķ�������
        public string StatGroupName;//��������ʾͳ��ͼ�ı���ʱ�ڱ�����������Ϊ��ʶ
        //ͳ��ͼ���ࣺ��������ĳ��ͳ��ͼ��Ĭ�Ϸ��࣬���������û�ж����������Ϊ������������Ϊ��δ���ࡱ
        public string ChartSort;
        //ͳ��ͼ�ı���
        public string Title;
        //ͳ��ͼ�����꣨X,Y������
        public string XAxisTitle;//���������
        public string YAxisTitle;//���������
        //�Ƿ���ʾ���
        public bool ShowValue;
        //ά�ȱ任:�Ƿ�ѵ�һ������͵ڶ���������н���
        public bool ConvertXY;
        //ͼ�ĳߴ�
        public int Width;//�������
        public int Height;//�ߣ�����
        public string Where;//��ѯ����
        public System.Data.DataSet StatResult;//�����ݿ���ͳ�Ƴ����Ľ��

        public STATData()
        {
        }
    }



    /// <summary>
    /// �洢������
    /// </summary>
    public class StoredProc
    {
        public string StoredProcName;//�洢��������
        public OPID OPID;//���з�ʽ��Ĭ��Ϊִ�У������ǲ�ѯ����
        public Hashtable StoredProcParam;//�洢���̲������ݵĹ�ϣ����������ƣ�����ֵ�����������û��ֵ��null
        public Hashtable StoredProcParamType;//�洢���̲������͵Ĺ�ϣ����������ƣ��������ͣ�ȡ�ֶ�����ö��FieldType�������������û��ֵ��null
        public Hashtable StoredProcParamDirection;//�洢���̲�������Ĺ�ϣ����������ƣ��洢���̲����������������ö������ParamDirection�������û��ָ������(��nullֵ)����Ĭ��Ϊ����
        public Object StoredProcReturnValue;//�洢���̷���ֵ�����ض���
        public Hashtable StoredProcParamValue;//�洢���̲������ݵĹ�ϣ����������ƣ������ķ���ֵ���������ķ���ֵ����Ϊobject����

        public StoredProc()
        {
        }

    }




    /// <summary>
    /// ����Ȩ����
    /// </summary>
    public class HasPower
    {
        public bool HasBatchINSERT;//��������
        public bool HasINSERT;//����
        public bool HasDELETE;//��
        public bool HasCOPY;  //����
        public bool HasUPDATE;//�޸�
        public bool HasSAVE;  //���� 
        public bool HasCANCEL;//ȡ��
        public bool HasOutIn; //���뵼��
        public bool HasALL;  //�������е�Ȩ��
        public bool HasNONE;  //ʲôȨ�޶�û��

        public HasPower()
        {
        }

    }



    /// <summary>
    /// ���OA���õ����ݲ�ѯ��
    /// </summary>
    public class SQLSelectData
    {
        //Ӧ��ϵͳ��Ϣ
        public string SystemName;//��ǰ���������ݿ������
        //���ݿ���Ϣ
        public string DatabaseName;//��ǰ���������ݿ������
        public string TableName;//��ǰ�����ı������
        public string ConnectionString;//���ݿ����Ӵ�:���ָ�������Ӵ���ֱ�Ӵ����Ӵ��в�ѯ
        //��ѯ���:�����Ĳ�ѯ���,������ñ����,�����ִ�б����󷵻�
        public string SelectSQL;//��ѯ���
        //��ѯ�ֶ����
        public string SelectFields;//��ѯ�ֶ����
        //��ѯ����
        public string Where;//��ѯ���������
        //�������:�����ֶ��б�
        public string OrderBy;//�������
        //��ѯ��Ϣ
        public int PageStart;//���ؼ�¼����ʼҳ
        public int PageSize;//���ؼ�¼��ÿҳ��¼��
        //���صļ�¼��Ϣ
        public DataSet RecordsetDataSet;
        public DataTable RecordsetDataTable;
        public string RecordsetXML;

        //���캯��
        public SQLSelectData()
        {
        }

    }




    /// <summary>
    /// ͨ�õ����ݲ�ѯ��
    /// </summary>
    public class SQLData
    {
        //�û���Ϣ
        public UserLoginInfo userLoginInfo;
        //���ݿ���Ϣ
        public DataBaseLoginInfo dataBaseLoginInfo;
        //�������Ϣ
        public TableConfigInfo tableConfigInfo;

        public string ParentName; //���׽�������
        public string ParentGuid; //���׽���GUID
        public string ParentPath; //���׽���·��
        public string oldCode;	  //�ýڵ��޸�ǰ��code
        public string newCode;	  //�ýڵ��޸ĺ��code
        //
        public string Opid; //��ǰ���������
        public string Action;//��ǰ�����Ķ���
        public string Id; //��ǰ�����ļ�¼GUID
        public string TableName;//��ǰ�����ı������
        public string PrimaryKey;//���ؼ��ֶΣ�����Ƕ���ֶΣ��ö��ŷָ������õ�������
        public string TitleFieldName;//�����ֶε�����
        public string TitleFieldValue;//�����ֶε����ݣ����������������
        public string SqlType;//SQL�ű������ͣ����磺INSERT��UPDATE��DELETE
        public string SqlModal;    //���нű�ģ��
        public string SqlExecute;    //���еľ����SQL�ű�
        public Hashtable SqlParams; //����������[�ֶ����ƣ��ֶ�����]
        public Hashtable WhereParams;//����������[�ֶ����ƣ��ֶ�����]
        public Hashtable OrderParams;//��������������[�ֶ����ƣ�����ʽ]
        public Hashtable FieldType;//�ֶ����ͱ�����[�ֶ����ƣ��ֶ�����]
        public DataTable RecordSet; //��ѯ���
        public string RecordXML; //��ѯ���XML
        public int RecordNumber; //�����ѯ����ļ�¼������ʼ��Ϊ-100
        public int RecordBegin; //�����ѯ�������ʼ��¼�ţ���ʼ��Ϊ-100
        public int RecordEnd; //�����ѯ����Ľ�ֹ��¼�ţ���ʼ��Ϊ-100
        public int PageNumber; //ÿҳ���صļ�¼������ʼ��Ϊ20
        public bool IsAudit; //�Ƿ�Ҫ��������ƣ���ʼ��Ϊfalse
        public string AuditContent; //��Ʋ�������

        public SQLData()
        {
        }

    }



    /// <summary>
    /// �û���¼��Ϣ��
    /// </summary>
    public class UserLoginInfo
    {
        public string UserName;//�û�������ID
        public string PassWord; //����
        public string Token; //token
        public string Roles; //���û�����Ľ�ɫ�飬����ж�����ö��ŷָ�
        public string ResGuid; //��Դ��GUID
        public string ResType; //��Դ������
        public string ResPowerValue; //�û��Ը���ԴȨ��
        public DateTime LoginTime;//��¼ʱ��
        public UserLoginInfo()
        { }

    }


    /// <summary>
    /// ���ݿ����ӵĻ�����Ϣ��
    /// </summary>
    public class DataBaseLoginInfo
    {
        public DatabaseType DatabaseType;//���ݿ�����
        public string IP; //IP��ַ
        public string TCP; //�˿ں�
        public string DatabaseName; //���ݿ�����
        public string UserName;//�û�������ID�����ݿ��owner��
        public string PassWord; //����
        public string DefaultTableSpace; //Ĭ�ϵı�ռ�
        public string ConnectionString;//���Ӵ�
        public string ConfigXML;//������Ϣ��XML
        public string SelectTableXML;//ѡ��ı����ṹ��XML
        public DataBaseLoginInfo()
        {
        }
    }


    /// <summary>
    /// ��ṹ�Ļ�����Ϣ�࣬Ŀ�ģ���ɶԱ�����ӡ��޸ġ�ɾ���Ȳ���
    /// ��¼����丸��Ļ��������һ��������ö��������Ϊ���ؼ��ֵ������ֶ�PrimaryKeyTable��
    /// �����������ֶ�PrimaryKeyTable�͸ñ�����ForeignKeyTable������Ϊ�ñ������PrimaryKey
    /// һ������붨���������ֶΣ�һ����Ĭ��ֻ��һ������
    /// ���д�ŵ����е��ֶ����ͣ����붼ת��Ϊö������FieldType
    /// ����������ֶκ�����ֶΣ����Ǵ�TablesRelation�н�����ȡ
    /// </summary>
    public class TableConfigInfo
    {
        //�����������磺INSERT��UPDATE��DELETE��SELECT
        public OPID OPID;
        //���ݿ���Ϣ
        public DataBaseLoginInfo dataBaseLoginInfo;
        //����������Ϣ�����ñ����������
        public string PrimaryTable;//��ǰ�����������������
        //����Ϣ����ǰ�����Ϣ
        public string TableName;//�û���������������
        public string TableDesc;//�û��������������������
        //����ֶ���Ϣ
        public Hashtable ForeignKey;//�����ؼ��ֱ�����Ϊ�������ؼ������ơ����������ؼ���ֵ�������ֶ����ơ��ֶ�����
        public Hashtable ForeignKeyType;//�����ؼ������ͱ�����Ϊ�������ؼ������ơ����������ؼ������͡������ֶ����ơ��ֶ�����
        public Hashtable ForeignKeyRelation;//�����ؼ��ֱ�����Ϊ�������ؼ������ơ�����������ֶ����ơ������ֶ����ơ�������ֶ�����
        public System.Collections.Stack ForeignKeyStack;//�����ؼ��ֶ�ջ������Ϊ�������ؼ������ơ����Ⱥ�����Ƚ�ȥ���ں���
        //�����ֶ���Ϣ
        public Hashtable PrimaryKey;//����������Ϊ�����ؼ������ơ��������ؼ������ݡ������ֶ����ơ��ֶ�����
        public Hashtable PrimaryKeyType;//����������Ϊ�����ؼ������ơ��������ؼ������͡������ֶ����ơ��ֶ�����
        //�ñ����ж�����ֶ���Ϣ
        public Hashtable FieldNameDesc;//������ֶ����ƣ��ֶ����ƣ��ֶ�������
        public Hashtable FieldNameType;//������ֶ����ƣ��ֶ����ƣ��ֶ���������Ϊ���Ƕ����ö������FieldType��
        public Hashtable FieldNameDictionary;//������ֶ����ƣ��ֶ����ƣ����ֶζ���������ֵ�(�������ļ��ﶨ��)��
        public Hashtable FieldNameValue;//������ֶ����ƣ��ֶ����ƣ��ֶ����ݣ�
        //����
        public string TitleField;//��XMLȡ������ʾ��Tree���ֶΣ���������ListView ��Item����
        public string Title; //��XMLȡ������ʾ��Tree���ֶζ�Ӧ�ı��⣬��������ListView ��column����

        public TableConfigInfo()
        {
            //�����������磺INSERT��UPDATE��DELETE��SELECT
            OPID = OPID.NONE;
            //���ݿ���Ϣ
            dataBaseLoginInfo = DataTag.initDataBaseLoginInfo();
            //�������Ϣ��������Ϣ��
            PrimaryTable = "";
            //����Ϣ����ǰ�����Ϣ
            TableName = "";
            TableDesc = "";
            //����ֶ���Ϣ
            ForeignKey = new Hashtable();
            ForeignKeyType = new Hashtable();
            ForeignKeyRelation = new Hashtable();
            ForeignKeyStack = new Stack();
            //�����ֶ���Ϣ
            PrimaryKey = new Hashtable();
            PrimaryKeyType = new Hashtable();
            //�ñ����ж�����ֶ���Ϣ
            FieldNameDesc = new Hashtable();
            FieldNameType = new Hashtable();
            FieldNameDictionary = new Hashtable();
            FieldNameValue = new Hashtable();
            //�����ֶ�
            TitleField = "";
            Title = "";
        }

    }


    /// <summary>
    /// ListView������:
    /// ���FieldDefine.XML������ֶκͷ���������
    /// ��ListView��ʾ���ֶΣ�ListView�ϵ������Ĳ˵���ͬʱ��ʾ��ToolsBar�У�
    /// ��ѡ��ListView��ĳ����¼ʱ���������ݸ���(ֵ)��������������
    /// </summary>
    public class ListViewData
    {
        //�����ͽṹ�йص�����
        public string ParentName; //���׽�������
        public string ParentGuid; //���׽���GUID
        public string ParentPath; //���׽���·��code
        public string ParentPathName; //���׽��·������
        //���ڱ������
        public string Opid;  //��Ӧ�Ĳ������ͱ�ʶ
        public string TableName;//��Ӧ�ı�����
        public string TableDesc;//��Ӧ�ı�����
        public string PrimaryKey;//���ؼ��ֶΣ�����Ƕ���ֶΣ��ö��ŷָ�
        public string TitleFieldName;//�����ֶε�����
        public string ParentFieldName;//�����ֶε����ƣ�������ѯ�ӽ����
        public string ParentFieldValue;//�����ֶε�ֵ����ӡ��޸ġ�ɾ������ʱ��
        public string SQL;//���е�SQL���
        //�������ݵ�����
        public string TitleFieldValue;//�����ֶε�����
        public string ContentXML;//���浱Ȼ���������ݶ�Ӧ��XML
        public Hashtable ContentTable;//���浱Ȼ���������ݣ��ù�ϣ���ţ��ֶ����ƣ��ֶ����ݣ�
        public string Id;  //�û��������������ݵ�ID
        public int FieldNum;//�ֶεĸ���
        public int MenuNum;//�˵��ĸ���
        //public int RightMenuNum;//�ұ߲˵��ĸ���
        public int LabelLength;//��ǩ����󳤶ȣ����ֶ�������ʾ�������
        //��������������
        public string Action;  //�û������Ĳ���,ȡname�� ֵ
        public string ActionDesc;  //�û���������������,ȡdescription�� ֵ
        public bool IsRightAction;//�û�ѡ��Ĳ˵��Ƿ����ұߣ�ListView���ģ�Ĭ��Ϊ�ұ�
        //�ֶε�����
        public object[] DictObject; //�ֵ��ֶΣ��ֶε���ʾ���ݺ����ݿ����ŵ���������[ShowField,SaveField]
        public bool[] IsShowInListView; //�Ƿ���ʾ��ListView
        public bool[] IsShowInForm; //�Ƿ���ʾ��Form������Ϊ¼���ֶ�
        public bool[] IsReadOnly; //ֻ��������ʾ��Form�е������޸�
        public string[] FieldDesc;//�ֶα���
        public string[] FieldName;//�ֶ�����
        public string[] FieldType;//�ֶ�����
        public int[] FormLength;//�ֶ���ʾ��FORM���ȣ���λ������
        public int[] ListViewLength;//�ֶ���ʾ��ListView�ĳ��ȣ���λ������
        public string[] FieldContent;//�ֶ�����
        public Hashtable FieldContentTable;//�ֶ����ݣ��ù�ϣ���ţ��ֶ����ƣ��ֶ����ݣ�
        public string[] FieldStyle;//�ֶε���ʾ��񣬼���ʲô�ؼ���ʾ���ֱ�Ϊmemo,select�ȵ�
        public string[] FieldRegex;//������ʽ
        public string[] FieldAnchor;//�ֶε���ê������=right�������һ�����ұߣ����������һ��������
        public bool[] FieldMustInput;//�Ƿ��Ǳ����ֶ�
        //�˵�������
        public string[] MenuName;//��actions����Ĳ˵��һ����ΪListView�������Ĳ˵�
        public string[] MenuDesc;//��actions����Ĳ˵�������
        public string[] MenuIcon;//��actions����Ĳ˵���ͼ��

        public ListViewData()
        {
        }

    }


    /// <summary>
    /// �����������:
    /// TreeView������ӵ�һЩ���ԣ���ŵ����Node��TAG��
    /// </summary>
    public class TreeNodeData
    {
        //��ǰ���������Ϣ
        public TableConfigInfo tableConfigInfo;
        //����������
        public string ParentOpid;//�û���������Ŀ�ĸ�OPID
        public string ParentPath;//�ý��ĸ��׽���·��path
        //�����Ϣ
        public string OPID;//��������
        public string ID;//�ý��ID
        public string NodeName;//�ý������name
        public string NodePath;//�ý��·��path
        //��չ��Ϣ
        public int OpenIcon;//�ý���ڴ�״̬�µ�ͼ�������ֵ
        public int CloseIcon;//�ý���ڹر�״̬�µ�ͼ�������ֵ
        public int Depth;//������ȣ���Ϊ1��������Ƶ��Ϊ2����������
        public bool IsTree;//�ý���Ƿ��ǿ���
        public bool IsExpand;//�ýӵ��Ƿ��Ѿ�չ����Ĭ��Ϊfalse,�����û������+����ʱ
        public bool IsLeaf;//�ж��Ƿ�����һ��������У���="false"������Ϊ"true"

        public TreeNodeData()
        {
        }

    }


    /// <summary>
    /// ComboBox������
    /// </summary>
    public class ComboBoxData
    {
        public int ItemNum;//����
        public string[] SaveValue;//�����ֵ
        public string[] ShowValue;//��ʾ��ֵ
        public bool[] IsDefault;  //�Ƿ���Ĭ��ֵ
        public System.Collections.Hashtable ShowSaveTable;//[��ʾ��ֵ�������ֵ]
        public System.Collections.Hashtable SaveShowTable;//[�����ֵ����ʾ��ֵ]
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
    /// ѡ���ֶ���Ϣ��
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
        public string type; //�ֶ�����
        public int order;  //����
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
    /// Ӧ��ϵͳ������
    /// </summary>
    public class AppSystemData
    {
        //��������
        public OPID OPID;
        //���õĲ���
        public string SystemName;//Ӧ��ϵͳ����
        public string DataBaseName;//��Ӧ�����ݿ�����
        public string TableName;//��Ӧ�����ݿ��(ģ�������)
        public string SelectFields;//ѡ����ֶ�(�������б�����ʾ���ֶ�)
        public string PrimaryKeySQL;//��ǰ�޸ĵļ�¼��������ѯ���ʽ������������֮���ô�д�� �� AND ���ָ�
        public string ForeignKeySQL;//�����������ѯ���ʽ������������֮���ô�д�� �� AND ���ָ�
        public string WhereSQL;//�̶��Ĳ�ѯ����
        public string SQL;//�����Ĳ�ѯ���(��������˱����,��ֻ���б����,����������ѯ�йصĲ�������������)
        //�����������
        public string ModalName;//ģ������
        public string ModalID;//ģ���ʶ(������ܵ�id��name
        public string PrimaryFieldName;//�����ֶζ�Ӧ��������ֶ����� 
        public string RelationModalName;//��������ģ������
        public string RelationFieldName;//�������ֶ�����
        public string RelationFieldValue;//�������ֶ�����
        //ҳ�����ݲ���
        public int PageStart;//��ʼҳ
        public int PageMode;//��ҳ��ʽ(-1 ����ҳ,0 ȫ��ҳ,1 ֻҪ��һҳ,2\3)
        public int PageSize;//ÿҳ��ʾ�ļ�¼��(Ĭ��Ϊ20)
        public int RecordCount;//���������ļ�¼��
        public int PageCount;//��ҳ��
        public DataSet DS;//���صĽ����
        //�����ֶ�SQL
        public string OrderBySQL;
        //��ʽ�ļ�
        public string CSSFile;

        public AppSystemData()
        {
        }
    }




    #endregion �Զ����������



    #region ��ʼ���Զ����������


    /// <summary>
    /// �Զ�����������һЩ��ʼ���ķ���
    /// </summary>
    public class DataTag
    {
        public DataTag()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }


        /// <summary>
        /// ��ʼ��һ���洢������
        /// </summary>
        /// <returns></returns>
        public static StoredProc initStoredProc()
        {
            StoredProc storedProc = new StoredProc();
            storedProc.StoredProcName = "";
            storedProc.OPID = OPID.EXECUTE;//ֻ���У����������ݼ���ֻ������Ӱ����е�
            storedProc.StoredProcParam = new Hashtable();
            storedProc.StoredProcParamType = new Hashtable();
            storedProc.StoredProcParamDirection = new Hashtable();
            storedProc.StoredProcReturnValue = new object();
            storedProc.StoredProcParamValue = new Hashtable();
            return storedProc;
        }





        /// <summary>
        /// ��ʼ��һ�����ݿ��¼��DataBaseLoginInfo������
        /// </summary>
        /// <returns></returns>
        public static DataBaseLoginInfo initDataBaseLoginInfo()
        {
            DataBaseLoginInfo tagDataBaseLoginInfo = new DataBaseLoginInfo();
            tagDataBaseLoginInfo.DatabaseType = DatabaseType.SQLServer;
            tagDataBaseLoginInfo.DatabaseName = "";
            tagDataBaseLoginInfo.ConnectionString = "";
            tagDataBaseLoginInfo.DefaultTableSpace = "";
            tagDataBaseLoginInfo.IP = "";
            tagDataBaseLoginInfo.UserName = "";
            tagDataBaseLoginInfo.PassWord = "";
            tagDataBaseLoginInfo.TCP = "";
            return tagDataBaseLoginInfo;
        }


        /// <summary>
        /// ����һ�����ݿ��¼��DataBaseLoginInfo������
        /// </summary>
        /// <returns></returns>
        public static DataBaseLoginInfo initDataBaseLoginInfo(DataBaseLoginInfo oldData)
        {
            DataBaseLoginInfo tagDataBaseLoginInfo = new DataBaseLoginInfo();
            tagDataBaseLoginInfo.DatabaseType = oldData.DatabaseType;
            tagDataBaseLoginInfo.DatabaseName = oldData.DatabaseName;
            tagDataBaseLoginInfo.ConnectionString = oldData.ConnectionString;
            tagDataBaseLoginInfo.DefaultTableSpace = oldData.DefaultTableSpace;
            tagDataBaseLoginInfo.IP = oldData.IP;
            tagDataBaseLoginInfo.UserName = oldData.UserName;
            tagDataBaseLoginInfo.PassWord = oldData.PassWord;
            tagDataBaseLoginInfo.TCP = oldData.TCP;
            return tagDataBaseLoginInfo;
        }


        /// <summary>
        /// ��ʼ��TableConfigInfo��������һ��TableConfigInfo��
        /// </summary>
        /// <returns></returns>
        public static TableConfigInfo initTableConfigInfo()
        {
            TableConfigInfo tagTableConfigInfo = new TableConfigInfo();
            //�����������磺INSERT��UPDATE��DELETE��SELECT
            tagTableConfigInfo.OPID = OPID.NONE;
            //���ݿ���Ϣ
            tagTableConfigInfo.dataBaseLoginInfo = DataTag.initDataBaseLoginInfo();
            //�������Ϣ��������Ϣ��
            tagTableConfigInfo.PrimaryTable = "";
            //����Ϣ����ǰ�����Ϣ
            tagTableConfigInfo.TableName = "";
            tagTableConfigInfo.TableDesc = "";
            //����ֶ���Ϣ
            tagTableConfigInfo.ForeignKey = new Hashtable();
            tagTableConfigInfo.ForeignKeyType = new Hashtable();
            tagTableConfigInfo.ForeignKeyRelation = new Hashtable();
            tagTableConfigInfo.ForeignKeyStack = new Stack();
            //�����ֶ���Ϣ
            tagTableConfigInfo.PrimaryKey = new Hashtable();
            tagTableConfigInfo.PrimaryKeyType = new Hashtable();
            //�ñ����ж�����ֶ���Ϣ
            tagTableConfigInfo.FieldNameDesc = new Hashtable();
            tagTableConfigInfo.FieldNameType = new Hashtable();
            tagTableConfigInfo.FieldNameDictionary = new Hashtable();
            tagTableConfigInfo.FieldNameValue = new Hashtable();
            //�����ֶ�
            tagTableConfigInfo.TitleField = "";
            tagTableConfigInfo.Title = "";
            return tagTableConfigInfo;
        }



        /// <summary>
        /// ��ʼ��һ��TableConfigInfo�����ݣ�����ԭ���������ิ�Ƶ��½�������
        /// </summary>
        /// <param name="oldData"></param>
        /// <returns></returns>
        public static TableConfigInfo initTableConfigInfo(TableConfigInfo oldData)
        {
            TableConfigInfo tagTableConfigInfo = new TableConfigInfo();
            //�����������磺INSERT��UPDATE��DELETE��SELECT
            tagTableConfigInfo.OPID = oldData.OPID;
            //���ݿ���Ϣ
            tagTableConfigInfo.dataBaseLoginInfo = DataTag.initDataBaseLoginInfo(oldData.dataBaseLoginInfo);
            //�������Ϣ��������Ϣ��
            tagTableConfigInfo.PrimaryTable = oldData.PrimaryTable;
            //����Ϣ����ǰ�����Ϣ
            tagTableConfigInfo.TableName = oldData.TableName;
            tagTableConfigInfo.TableDesc = oldData.TableDesc;
            //����ֶ���Ϣ
            tagTableConfigInfo.ForeignKey = (Hashtable)oldData.ForeignKey.Clone();
            tagTableConfigInfo.ForeignKeyType = (Hashtable)oldData.ForeignKeyType.Clone();
            tagTableConfigInfo.ForeignKeyRelation = (Hashtable)oldData.ForeignKeyRelation.Clone();
            //�����ֶ���Ϣ
            tagTableConfigInfo.PrimaryKey = (Hashtable)oldData.PrimaryKey.Clone();
            tagTableConfigInfo.PrimaryKeyType = (Hashtable)oldData.PrimaryKeyType.Clone();
            //�ñ����ж�����ֶ���Ϣ
            tagTableConfigInfo.FieldNameDesc = (Hashtable)oldData.FieldNameDesc.Clone();
            tagTableConfigInfo.FieldNameType = (Hashtable)oldData.FieldNameType.Clone();
            tagTableConfigInfo.FieldNameDictionary = (Hashtable)oldData.FieldNameDictionary.Clone();
            tagTableConfigInfo.FieldNameValue = (Hashtable)oldData.FieldNameValue.Clone();
            //�����ֶ�
            tagTableConfigInfo.TitleField = oldData.TitleField;
            tagTableConfigInfo.Title = oldData.Title;
            return tagTableConfigInfo;
        }



        /// <summary>
        /// ��ʼ��SQLData��
        /// </summary>
        /// <returns></returns>
        public static SQLData initSQLData()
        {
            SQLData sqlData = new SQLData();
            sqlData.Opid = "";
            sqlData.TableName = "";
            sqlData.SqlModal = "";
            sqlData.SqlExecute = "";
            sqlData.SqlType = "";
            sqlData.SqlParams = new Hashtable();
            sqlData.WhereParams = new Hashtable();
            sqlData.OrderParams = new Hashtable();
            sqlData.FieldType = new Hashtable();
            sqlData.RecordSet = new DataTable();
            sqlData.RecordXML = "";
            sqlData.RecordNumber = -100;
            sqlData.RecordBegin = -100;
            sqlData.RecordEnd = -100;
            sqlData.PageNumber = 20;
            sqlData.IsAudit = false;
            sqlData.TitleFieldName = "";
            sqlData.TitleFieldValue = "";
            sqlData.dataBaseLoginInfo = DataTag.initDataBaseLoginInfo();
            sqlData.tableConfigInfo = DataTag.initTableConfigInfo();
            return sqlData;
        }



        /// <summary>
        /// ��ʼ��һ��TreeNode��������
        /// </summary>
        /// <returns></returns>
        public static TreeNodeData initTreeNodeData()
        {
            TreeNodeData tagTreeNodeData = new TreeNodeData();
            //����Ϣ
            tagTreeNodeData.tableConfigInfo = DataTag.initTableConfigInfo();
            //����������
            tagTreeNodeData.ParentOpid = "";
            tagTreeNodeData.ParentPath = "";
            //�����Ϣ
            tagTreeNodeData.OPID = "";
            tagTreeNodeData.ID = "";
            tagTreeNodeData.NodeName = "";
            tagTreeNodeData.NodePath = "";
            //��չ����
            tagTreeNodeData.OpenIcon = 0;
            tagTreeNodeData.CloseIcon = 0;
            tagTreeNodeData.Depth = 0;
            tagTreeNodeData.IsTree = false;
            tagTreeNodeData.IsExpand = false;
            tagTreeNodeData.IsLeaf = false;
            return tagTreeNodeData;
        }


        /// <summary>
        /// ��һ��ListViewData�ิ�Ƴ���һ��ListViewData��
        /// </summary>
        /// <param name="oldData">ԭ������</param>
        /// <returns></returns>
        public static ListViewData initListViewData(ListViewData oldData)
        {
            ListViewData listItemData = new ListViewData();
            listItemData.ParentName = oldData.ParentName;
            listItemData.ParentGuid = oldData.ParentGuid;
            listItemData.ParentPath = oldData.ParentPath;
            listItemData.ParentPathName = oldData.ParentPathName;
            listItemData.Opid = oldData.Opid;
            listItemData.TableName = oldData.TableName;
            listItemData.TableDesc = oldData.TableDesc;
            listItemData.PrimaryKey = oldData.PrimaryKey;
            listItemData.SQL = oldData.SQL;
            listItemData.TitleFieldName = oldData.TitleFieldName;
            listItemData.TitleFieldValue = oldData.TitleFieldValue;
            listItemData.ParentFieldName = oldData.ParentFieldName;
            listItemData.ParentFieldValue = oldData.ParentFieldValue;
            listItemData.ContentXML = oldData.ContentXML;
            listItemData.ContentTable = oldData.ContentTable;
            listItemData.Id = oldData.Id;
            listItemData.FieldNum = oldData.FieldNum;
            listItemData.LabelLength = oldData.LabelLength;
            listItemData.Action = oldData.Action;
            listItemData.ActionDesc = oldData.ActionDesc;
            listItemData.IsRightAction = oldData.IsRightAction;
            listItemData.DictObject = oldData.DictObject;
            listItemData.IsShowInListView = oldData.IsShowInListView;
            listItemData.IsShowInForm = oldData.IsShowInForm;
            listItemData.IsReadOnly = oldData.IsReadOnly;
            listItemData.FieldDesc = oldData.FieldDesc;
            listItemData.FieldName = oldData.FieldName;
            listItemData.FieldType = oldData.FieldType;
            listItemData.FormLength = oldData.FormLength;
            listItemData.ListViewLength = oldData.ListViewLength;
            listItemData.FieldContent = oldData.FieldContent;
            listItemData.FieldContentTable = oldData.FieldContentTable;
            listItemData.FieldStyle = oldData.FieldStyle;
            listItemData.FieldRegex = oldData.FieldRegex;
            listItemData.FieldAnchor = oldData.FieldAnchor;
            listItemData.FieldMustInput = oldData.FieldMustInput;
            listItemData.MenuNum = oldData.MenuNum;
            listItemData.MenuName = oldData.MenuName;
            listItemData.MenuDesc = oldData.MenuDesc;
            listItemData.MenuIcon = oldData.MenuIcon;
            return listItemData;
        }



        /// <summary>
        /// ��ʼ��ListViewData��������һ��ListViewData��
        /// �˵���������Ϊ40�������Ĳ˵���
        /// </summary>
        /// <param name="fieldNum"></param>
        /// <returns></returns>
        public static ListViewData initListViewData(int fieldNum)
        {
            int maxMenu = 40;
            ListViewData listItemData = new ListViewData();
            listItemData.ParentName = "";
            listItemData.ParentPath = "/";
            listItemData.ParentGuid = "";
            listItemData.ParentPathName = "";
            listItemData.Opid = "";
            listItemData.TableName = "";
            listItemData.TableDesc = "";
            listItemData.PrimaryKey = "";
            listItemData.SQL = "";
            listItemData.TitleFieldName = "";
            listItemData.TitleFieldValue = "";
            listItemData.ParentFieldName = "";
            listItemData.ParentFieldValue = "";
            listItemData.ContentXML = "";
            listItemData.ContentTable = new Hashtable();
            listItemData.Id = "";
            listItemData.FieldNum = fieldNum;
            listItemData.MenuNum = 0;
            listItemData.LabelLength = 0;
            listItemData.Action = "";
            listItemData.ActionDesc = "";
            listItemData.IsRightAction = true;
            listItemData.DictObject = new object[fieldNum];
            listItemData.IsShowInListView = new bool[fieldNum];
            listItemData.IsShowInForm = new bool[fieldNum];
            listItemData.IsReadOnly = new bool[fieldNum];
            listItemData.FieldDesc = new string[fieldNum];
            listItemData.FieldName = new string[fieldNum];
            listItemData.FieldType = new string[fieldNum];
            listItemData.FormLength = new int[fieldNum];
            listItemData.ListViewLength = new int[fieldNum];
            listItemData.FieldContent = new string[fieldNum];
            listItemData.FieldContentTable = new Hashtable();
            listItemData.FieldStyle = new string[fieldNum];
            listItemData.FieldRegex = new string[fieldNum];
            listItemData.FieldAnchor = new string[fieldNum];
            listItemData.FieldMustInput = new bool[fieldNum];
            listItemData.MenuName = new string[maxMenu];
            listItemData.MenuDesc = new string[maxMenu];
            listItemData.MenuIcon = new string[maxMenu];
            return listItemData;
        }




        /// <summary>
        /// ��ʼ��һ��ͳ��ͼ��������STATData������
        /// </summary>
        /// <returns></returns>
        public static STATData initSTATData()
        {
            STATData statData = new STATData();
            //���ݿ���Ϣ
            statData.dataBaseLoginInfo = DataTag.initDataBaseLoginInfo(); ;
            //�������Ϣ
            statData.tableConfigInfo = DataTag.initTableConfigInfo(); ;
            //ͳ�Ʊ�
            statData.DatabaseName = "";
            statData.TableName = "";
            //����ϵ��:��ͳ����,��ͳ�Ƹ������ܺ͵�
            statData.DataSeriesType = new Hashtable();//���ݴ�Ÿ�ʽΪ:ͳ���ֶ�,ͳ�Ƶ�����DataSeriesType
            statData.DataSeries = new Hashtable();//���ݴ�Ÿ�ʽΪ:ͳ���ֶ�����,�ֶ�����
            //ͳ�Ʒ����������ʲô��ͳ�Ƶȣ��硰��ע�̶ȡ������Ա�(����ֻ��һ������)
            statData.GroupBy = new Hashtable();//��Ÿ�ʽΪ:�ֶ�����,�ֶ�����
            statData.groupByParam = new Hashtable();//��Ÿ�ʽΪ:�ֶ�����,�ֶβ�������(GroupByParam)
            statData.GroupByQueue = new Queue();
            //��ѯ�ֶ�
            statData.QueryField = new Hashtable();
            statData.QueryFieldType = new Hashtable();
            //ͳ��ͼ������
            statData.chartType = ChartTypeSTAT.Combo;
            statData.ChartTypeXML = "Combo";
            //ͳ��ͼ��Ч������Ӱ��
            statData.chartShadingEffect = ChartShadingEffectSTAT.Three;
            statData.imageFormat = ImageFormatSTAT.Pdf;//ͼ��������ʽ
            //ͳ��ͼ������
            statData.Name = "";
            //��������
            statData.StatGroupName = "";
            //ͳ��ͼ���ࣺ��������ĳ��ͳ��ͼ��Ĭ�Ϸ��࣬���������û�ж����������Ϊ������������Ϊ��δ���ࡱ
            statData.ChartSort = "";
            //ͳ��ͼ�ı���
            statData.Title = "";
            //ͳ��ͼ�����꣨X,Y������
            statData.XAxisTitle = "";//���������
            statData.YAxisTitle = "";//���������
            //�Ƿ���ʾ���
            statData.ShowValue = true;
            //�Ƿ񽻻�ά��
            statData.ConvertXY = false;//Ĭ��Ϊ������
            //ͼ�ĳߴ�
            statData.Width = 0;//�������
            statData.Height = 0;//�ߣ�����
            statData.Where = "";//��ͳ����Ŀ��Ӧ��SQL�ű�
            statData.StatResult = new DataSet();//�����ݿ���ͳ�Ƴ����Ľ��
            return statData;
        }



        /// <summary>
        /// ͳ�Ʒ������:ͳ���ֶεĲ���,�������ֶ�,�����ֶ�
        /// </summary>
        /// <returns></returns>
        public static GroupByParam initGroupByParam()
        {
            GroupByParam groupByParam = new GroupByParam();
            groupByParam.Type = GroupByType.NONE;
            groupByParam.From = -1;
            groupByParam.To = -1;
            return groupByParam;
        }



        /// <summary>
        /// ��ʼ��һ����ѯ�����
        /// </summary>
        /// <returns></returns>
        public static SQLSelectData initSQLSelectData()
        {
            SQLSelectData sqlSelectData = new SQLSelectData();
            //Ӧ��ϵͳ��Ϣ
            sqlSelectData.SystemName = "";//��ǰ���������ݿ������
            //���ݿ���Ϣ
            sqlSelectData.DatabaseName = "";//��ǰ���������ݿ������
            sqlSelectData.TableName = "";//��ǰ�����ı������
            sqlSelectData.ConnectionString = "";//���ݿ����Ӵ�:���ָ�������Ӵ���ֱ�Ӵ����Ӵ��в�ѯ
            //��ѯ���:�����Ĳ�ѯ���,������ñ����,�����ִ�б����󷵻�
            sqlSelectData.SelectSQL = "";//��ѯ���
            //��ѯ�ֶ����
            sqlSelectData.SelectFields = "";//��ѯ�ֶ����
            //��ѯ����
            sqlSelectData.Where = "";//��ѯ���������
            //�������:�����ֶ��б�
            sqlSelectData.OrderBy = "";//�������
            //��ѯ��Ϣ
            sqlSelectData.PageStart = 1;//���ؼ�¼����ʼҳ
            sqlSelectData.PageSize = 20;//���ؼ�¼��ÿҳ��¼��
            //���صļ�¼��Ϣ
            sqlSelectData.RecordsetDataSet = new DataSet();
            sqlSelectData.RecordsetDataTable = new DataTable();
            sqlSelectData.RecordsetXML = "";
            return sqlSelectData;
        }



        /// <summary>
        /// ��ʼ��AppSystemData
        /// </summary>
        /// <returns></returns>
        public static AppSystemData initAppSystemData()
        {
            AppSystemData appSystemData = new AppSystemData();
            //��������
            appSystemData.OPID = OPID.NONE;
            //���õĲ�����ͨ��������ȡ���ݣ�
            appSystemData.SystemName = "";
            appSystemData.DataBaseName = "";
            appSystemData.TableName = "";
            appSystemData.SelectFields = "";
            appSystemData.PrimaryKeySQL = "";
            appSystemData.ForeignKeySQL = "";
            appSystemData.WhereSQL = "";
            appSystemData.SQL = "";
            //�����������
            appSystemData.ModalName = "";
            appSystemData.ModalID = "";
            appSystemData.PrimaryFieldName = "";
            appSystemData.RelationModalName = "";
            appSystemData.RelationFieldName = "";
            appSystemData.RelationFieldValue = "";
            //ҳ�����ݲ���
            appSystemData.PageMode = 0;
            appSystemData.PageStart = 1;
            appSystemData.PageSize = 20;
            appSystemData.RecordCount = -1;
            appSystemData.PageCount = 0;
            appSystemData.DS = new DataSet();
            //����
            appSystemData.OrderBySQL = "";
            //��ʽ�ļ�
            appSystemData.CSSFile = "";
            return appSystemData;
        }



        /// <summary>
        /// ����һ��AppSystemData
        /// </summary>
        /// <returns></returns>
        public static AppSystemData initAppSystemData(AppSystemData oldAppSystemData)
        {
            AppSystemData appSystemData = initAppSystemData();
            //��������
            appSystemData.OPID = oldAppSystemData.OPID;
            //���õĲ���
            appSystemData.SystemName = oldAppSystemData.SystemName;
            appSystemData.DataBaseName = oldAppSystemData.DataBaseName;
            appSystemData.TableName = oldAppSystemData.TableName;
            appSystemData.SelectFields = oldAppSystemData.SelectFields;
            appSystemData.PrimaryKeySQL = oldAppSystemData.PrimaryKeySQL;
            appSystemData.ForeignKeySQL = oldAppSystemData.ForeignKeySQL;
            appSystemData.WhereSQL = oldAppSystemData.WhereSQL;
            appSystemData.SQL = oldAppSystemData.SQL;
            //�����������
            appSystemData.ModalName = oldAppSystemData.ModalName;
            appSystemData.ModalID = oldAppSystemData.ModalID;
            appSystemData.PrimaryFieldName = oldAppSystemData.PrimaryFieldName;
            appSystemData.RelationModalName = oldAppSystemData.RelationModalName;
            appSystemData.RelationFieldName = oldAppSystemData.RelationFieldName;
            appSystemData.RelationFieldValue = oldAppSystemData.RelationFieldValue;
            //ҳ�����ݲ���
            appSystemData.PageStart = oldAppSystemData.PageStart;
            appSystemData.PageSize = oldAppSystemData.PageSize;
            appSystemData.RecordCount = oldAppSystemData.RecordCount;
            appSystemData.PageCount = oldAppSystemData.PageCount;
            appSystemData.DS = oldAppSystemData.DS.Clone();
            //����
            appSystemData.OrderBySQL = oldAppSystemData.OrderBySQL;
            return appSystemData;
        }


        /// <summary>
        /// ��ʼ��һ������Ĳ���Ȩ����
        /// </summary>
        /// <returns></returns>
        public static HasPower initHasPower()
        {
            HasPower hasPower = new HasPower();
            hasPower.HasBatchINSERT = false;
            hasPower.HasINSERT = false;
            hasPower.HasDELETE = false;
            hasPower.HasCOPY = false;
            hasPower.HasUPDATE = false;
            hasPower.HasSAVE = false;
            hasPower.HasCANCEL = false;
            hasPower.HasOutIn = false;
            hasPower.HasALL = false;
            hasPower.HasNONE = true;
            return hasPower;
        }



        /// <summary>
        /// ��ʼ��һ������Ĳ���Ȩ���ࣺĬ��Ϊָ��Ȩ��
        /// </summary>
        /// <returns></returns>
        public static HasPower initHasPower(bool setPower)
        {
            HasPower hasPower = new HasPower();
            hasPower.HasBatchINSERT = setPower;
            hasPower.HasINSERT = setPower;
            hasPower.HasDELETE = setPower;
            hasPower.HasCOPY = setPower;
            hasPower.HasUPDATE = setPower;
            hasPower.HasSAVE = setPower;
            hasPower.HasCANCEL = setPower;
            hasPower.HasOutIn = setPower;
            hasPower.HasALL = setPower;
            hasPower.HasNONE = !setPower;
            return hasPower;
        }



    }



    #endregion ��ʼ���Զ����������








}
