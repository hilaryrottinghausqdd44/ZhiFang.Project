/**
 * 报表明细基础列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.report2.ReportInfoGrid',{
    extend:'Shell.ux.grid.PostPanel',
    title:'报表明细基础列表',
    
    width:1600,
    height:400,
    
    /**获取数据服务路径*/
    selectUrl:'/StatService.svc/Stat_UDTO_ReportReconciliationDetail',
    
    /**默认加载*/
	defaultLoad:false,
	/**后台排序*/
	remoteSort:false,
	/**带分页栏*/
	hasPagingtoolbar:false,
	/**是否启用序号列*/
	hasRownumberer:true,
	/**带功能按钮栏*/
	hasButtontoolbar:false,
	
	/**报表类型*/
	reportType:'1',
	
	initComponent:function(){
		var me = this;
		
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		//销售=经销商=送检单位=科室=合作分级=项目名称=开票方类型=开票方（付款单位）=
		//合同价=是否有阶梯价=样本预制条码=实验室条码=病人名=采样时间=录入时间=
		//核收时间=报告时间=状态=是否免单=免单类型=免单价格=终端价=阶梯价=应收价
		
		//项目名称=销售=经销商=送检单位=科室=合作分级=
		//开票方类型=开票方（付款单位）=合同价=是否有阶梯价=
		//样本预制条码=实验室条码=病人名=采样时间=录入时间=核收时间=
		//报告时间=状态=是否免单=免单类型=免单价格=终端价=阶梯价=应收价
		var columns = [{
			dataIndex: 'DStatDetailClass_SellerName',
			text: '销售',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatDetailClass_DealerName',
			text: '经销商',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatDetailClass_NFClientName',
			text: '送检单位',
			defaultRenderer: true
		},{
			dataIndex: 'DStatDetailClass_NFDeptName',
			text: '科室',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatDetailClass_CoopLevel',
			align:'center',
			text: '合作分级',
			width: 60,
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.CoopLevel['E' + value] || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'DStatDetailClass_ItemName',
			text: '项目名称',
			defaultRenderer: true
		},{
			dataIndex: 'DStatDetailClass_BillingUnitType',
			align:'center',
			text: '开票方类型',
			width: 75,
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.UnitType['E' + value] || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'DStatDetailClass_BillingUnitName',
			text: '开票方(付款单位)',
			defaultRenderer: true
		},{
			dataIndex: 'DStatDetailClass_ItemContPrice',
			text: '合同价',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatDetailClass_IsStepPrice',
			text: '是否有阶梯价',
			width:90,
			align:'center',
			isBool:true,
			type:'bool'
		}, {
			dataIndex: 'DStatDetailClass_SerialNo',
			text: '样本预制条码',
			defaultRenderer: true
		},{
			dataIndex: 'DStatDetailClass_BarCode',
			text: '实验室条码',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatDetailClass_CName',
			text: '病人名',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatDetailClass_CollectDate',
			text: '采样时间',
			width:130,isDate:true,hasTime:true
		},{
			dataIndex: 'DStatDetailClass_OperDate',
			text: '录入时间',
			width:130,isDate:true,hasTime:true
		}, {
			dataIndex: 'DStatDetailClass_ReceiveDate',
			text: '核收时间',
			width:130,isDate:true,hasTime:true
		},{
			dataIndex: 'DStatDetailClass_CheckDate',
			text: '报告时间',
			width:130,isDate:true,hasTime:true
		}, {
			dataIndex: 'DStatDetailClass_SampleStatus',
			align:'center',
			text: '状态',
			width: 60,
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.IsLocked['E' + value] || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'DStatDetailClass_IsFree',
			text: '是否免单',
			width:60,
			align:'center',
			isBool:true,
			type:'bool'
		},{
			dataIndex: 'DStatDetailClass_IsFreeType',
			text: '免单类型',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatDetailClass_ItemFreePrice',
			text: '免单价格',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatDetailClass_ItemEditPrice',
			text: '终端价',
			defaultRenderer: true
		},{
			dataIndex: 'DStatDetailClass_ItemStepPrice',
			text: '阶梯价',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatDetailClass_ItemPrice',
			text: '应收价',
			defaultRenderer: true
		}];
		
		return columns;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		me.postParams.fields = me.getStoreFields(true).join(',');
		return me.callParent(arguments);
	}
});