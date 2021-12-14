Ext.QuickTips.init();

Ext.Loader.setConfig({
    enabled:true
});

Ext.Loader.setPath('Ext.ux', getRootPath() + '/ui/extjs/ux');

Ext.define('OutSideItems', {
    extend:'Ext.zhifangux.DdItems',
    alias:'widget.OutSideItems',
    title:'列表',
    width:680,
    height:280,
    leftMatchField:'MEPTSampleSendItem_ItemAllItem_Id',
    leftFilterField:'MEPTSampleSendItem_BLaboratory_Id',
    leftFilterValue:'',
    rightFieldsetTitle:'选择项目',
    leftFieldsetTitle:'外送项目',
    leftInternalWhere:'',
    rightInternalWhere:'',
    rightObjectName:'ItemAllItem',
    leftObjectName:'MEPTSampleSendItem',
    relationObjectName:'BLaboratory',    //MEPTSampleSendItem
    leftPrimaryKey:'MEPTSampleSendItem_Id',
    rightPrimaryKey:'ItemAllItem_Id',
    leftExternalPrimaryKey:'',
    leftExternalWhere:'',
    rightExternalWhere:'',
    autoSelect:true,
    deleteIndex:-1,
    autoScroll:true,
    leftServerUrl:getRootPath() + '/' + 'MEPTService.svc/MEPT_UDTO_SearchMEPTSampleSendItemByHQL',
    rightServerUrl:getRootPath() + '/' + 'SingleTableService.svc/ST_UDTO_SearchItemAllItemByHQL',
    saveServerUrl:getRootPath() + '/' + 'MEPTService.svc/MEPT_UDTO_AddMEPTSampleSendItem',
    delServerUrl:getRootPath() + '/' + 'MEPTService.svc/MEPT_UDTO_DelMEPTSampleSendItem',
    selectType:true,
    leftField:[],
    rightField:[],
    valueLeftField:[ {
        text:'项目名称',
        dataIndex:'MEPTSampleSendItem_ItemAllItem_CName',
        width:100,
        sortable:false,
        hidden:false,
        hideable:true
    }, {
        text:'报告天数',
        dataIndex:'MEPTSampleSendItem_ReportDays',
        width:100,
        sortable:false,
        hidden:false,
        hideable:true
    }, {
        text:'名称',
        dataIndex:'MEPTSampleSendItem_BLaboratory_CName',
        width:65,
        sortable:false,
        hidden:true,
        hideable:true
    }, {
        text:'简称',
        dataIndex:'MEPTSampleSendItem_BLaboratory_ShortCode',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true
    }, {
        text:'主键ID',
        dataIndex:'MEPTSampleSendItem_BLaboratory_Id',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true
    }, {
        text:'时间戳',
        dataIndex:'MEPTSampleSendItem_BLaboratory_DataTimeStamp',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true
    }, {
        text:'英文名称',
        dataIndex:'MEPTSampleSendItem_ItemAllItem_EName',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true
    }, {
        text:'项目简称',
        dataIndex:'MEPTSampleSendItem_ItemAllItem_SName',
        width:100,
        sortable:false,
        hidden:false,
        hideable:true
    }, {
        text:'结果单位',
        dataIndex:'MEPTSampleSendItem_ItemAllItem_Unit',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true
    }, {
        text:'默认参考范围',
        dataIndex:'MEPTSampleSendItem_ItemAllItem_RefRange',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true
    }, {
        text:'结果类型',
        dataIndex:'MEPTSampleSendItem_ItemAllItem_ValueType',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true
    }, {
        text:'采样要求',
        dataIndex:'MEPTSampleSendItem_ItemAllItem_SamplingRequire',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true
    }, {
        text:'项目价格',
        dataIndex:'MEPTSampleSendItem_ItemAllItem_ItemCharge',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true
    }, {
        text:'主键ID',
        dataIndex:'MEPTSampleSendItem_ItemAllItem_Id',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true
    }, {
        text:'时间戳',
        dataIndex:'MEPTSampleSendItem_ItemAllItem_DataTimeStamp',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true
    }, {
        text:'是否使用',
        dataIndex:'MEPTSampleSendItem_IsUse',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true
    }, {
        text:'主键ID',
        dataIndex:'MEPTSampleSendItem_Id',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true
    }, {
        text:'时间戳',
        dataIndex:'MEPTSampleSendItem_DataTimeStamp',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true
    } ],
    valueRightField:[ {
        text:'项目名称',
        dataIndex:'ItemAllItem_CName',
        width:100,
        sortable:false,
        hidden:false,
        hideable:true,
        align:'undefined'
    }, {
        text:'英文名称',
        dataIndex:'ItemAllItem_EName',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true,
        align:'undefined'
    }, {
        text:'项目简称',
        dataIndex:'ItemAllItem_SName',
        width:100,
        sortable:false,
        hidden:false,
        hideable:true,
        align:'undefined'
    }, {
        text:'项目类型',
        dataIndex:'ItemAllItem_ItemType',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true,
        align:'undefined'
    }, {
        text:'结果单位',
        dataIndex:'ItemAllItem_Unit',
        width:100,
        sortable:false,
        hidden:false,
        hideable:true,
        align:'undefined'
    }, {
        text:'默认参考范围',
        dataIndex:'ItemAllItem_RefRange',
        width:100,
        sortable:false,
        hidden:false,
        hideable:true,
        align:'undefined'
    }, {
        text:'结果类型',
        dataIndex:'ItemAllItem_ValueType',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true,
        align:'undefined'
    }, {
        text:'采样要求',
        dataIndex:'ItemAllItem_SamplingRequire',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true,
        align:'undefined'
    }, {
        text:'临床意义',
        dataIndex:'ItemAllItem_ClinicalSignificance',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true,
        align:'undefined'
    }, {
        text:'收费类型',
        dataIndex:'ItemAllItem_ChargeType',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true,
        align:'undefined'
    }, {
        text:'项目价格',
        dataIndex:'ItemAllItem_ItemCharge',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true,
        align:'undefined'
    }, {
        text:'描述',
        dataIndex:'ItemAllItem_Comment',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true,
        align:'undefined'
    }, {
        text:'是否使用',
        dataIndex:'ItemAllItem_IsUse',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true,
        align:'undefined'
    }, {
        text:'显示次序',
        dataIndex:'ItemAllItem_DispOrder',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true,
        align:'undefined'
    }, {
        text:'代码',
        dataIndex:'ItemAllItem_UseCode',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true,
        align:'undefined'
    }, {
        text:'快捷码',
        dataIndex:'ItemAllItem_Shortcode',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true,
        align:'undefined'
    }, {
        text:'汉字拼音字头',
        dataIndex:'ItemAllItem_PinYinZiTou',
        width:100,
        sortable:false,
        hidden:false,
        hideable:true,
        align:'undefined'
    }, {
        text:'主键ID',
        dataIndex:'ItemAllItem_Id',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true,
        align:'undefined'
    }, {
        text:'时间戳',
        dataIndex:'ItemAllItem_DataTimeStamp',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true,
        align:'undefined'
    } ],
    btnLeft:false,
    btnAllLeft:true,
    btnRight:false,
    btnAllRight:true,
    btnHidden:true,
    filterLeft:true,
    filterRight:false,
    fieldSetLeft:true,
    fieldSetRight:true,
    afterRender:function() {
        var me = this;
        me.callParent(arguments);
        if (Ext.typeOf(me.callback) == 'function') {
            me.callback(me);
        }
    },
    initComponent:function() {
        var me = this;
        this.callParent(arguments);
    }
});