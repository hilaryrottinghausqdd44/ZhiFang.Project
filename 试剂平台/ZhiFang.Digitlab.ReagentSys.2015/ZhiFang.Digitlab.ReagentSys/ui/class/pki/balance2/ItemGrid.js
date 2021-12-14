/**
 * 对账列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.balance2.ItemGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '医嘱项目基础列表 ',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/StatService.svc/Stat_UDTO_SearchReconciliationLocking?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/BaseService.svc/ST_UDTO_UpdateNRequestItemByField',
	/**下载EXCEL文件服务地址*/
  	downLoadExcelUrl:'/StatService.svc/Stat_UDTO_ReconciliationToExcel',
  	
  	/**财务报表类型*/
	reportType:null,
  	
	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,
	
	/**默认加载*/
	defaultLoad: true,
	/**默认每页数量*/
	defaultPageSize:200,
	
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**带功能按钮栏*/
	hasButtontoolbar:false,
	/**是否启用序号列*/
	hasRownumberer: true,

	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	
	/**查询栏参数设置*/
	searchToolbarConfig:{},

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.getComponent('filterToolbar').on({
			search:function(p,params){
				me.onSearch();
			}
		});
	},
	initComponent: function() {
		var me = this,
			config = me.searchToolbarConfig || {};
			
		if(me.reportType){
			me.buttonToolbarItems = me.buttonToolbarItems || [];
			if(me.buttonToolbarItems.length > 0){
				me.buttonToolbarItems.push('-');
			}
			me.buttonToolbarItems.push('exp_excel');
		}
			
		//创建挂靠功能栏
		me.dockedItems = me.dockedItems || [Ext.create('Shell.class.pki.balance2.SearchToolbar',Ext.apply(config,{
			itemId:'filterToolbar',
			dock:'top',
			isLocked: true,
			height:105
		}))];
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			dataIndex: 'NRequestItem_IsLocked',
			align:'center',
			text: '状态',
			width: 60,
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.IsLocked['E' + value] || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		},{
			dataIndex: 'NRequestItem_BSeller_AreaIn',
			text: '销售区域',
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_BSeller_Name',
			text: '销售',
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_BDealer_Name',
			text: '经销商',
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_NRequestForm_BLaboratory_CName',
			text: '送检单位',
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_NRequestForm_BDept_CName',
			text: '科室',
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_CoopLevel',
			align:'center',
			text: '合作分级',
			width: 60,
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.CoopLevel['E' + value] || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		},{
			dataIndex: 'NRequestItem_BTestItem_CName',
			text: '项目名称',
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_BillingUnitType',
			align:'center',
			text: '开票方类型',
			width: 75,
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.UnitType['E' + value] || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		},{
			dataIndex: 'NRequestItem_BBillingUnit_Name',
			text: '开票方(付款单位)',
			width:105,
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_BillingUnitInfo',
			text: '个人开票信息',
			width:85,
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_IsStepPrice',
			text: '是否有阶梯价',width:90,
			align:'center',
			isBool:true,
			type:'bool'
		},{
			dataIndex: 'NRequestItem_NRequestForm_SerialNo',
			text: '样本预制条码',
			width:90,
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_BarCode',
			text: '实验室条码',
			width:90,
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_NRequestForm_CName',
			text: '病人名',
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_CollectDate',
			text: '采样时间',width:130,isDate:true,hasTime:true
		},{
			dataIndex: 'NRequestItem_OperDate',
			text: '录入时间',width:130,isDate:true,hasTime:true
		},{
			dataIndex: 'NRequestItem_ReceiveDate',
			text: '核收时间',width:130,isDate:true,hasTime:true
		},{
			dataIndex: 'NRequestItem_SenderTime2',
			text: '报告时间',width:130,isDate:true,hasTime:true
		},{
			dataIndex: 'NRequestItem_IsFree',
			text: '是否免单',width:60,
			align:'center',
			isBool:true,
			type:'bool'
		},{
			dataIndex: 'NRequestItem_IsFreeType',
			text: '免单类型',width:60,
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_ItemPriceType',
			text: '价格类型',
			width: 60,
			align:'center',
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.ItemPriceType['E' + value] || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		},{
			dataIndex: 'NRequestItem_ItemFreePrice',
			text: '免单价格',width:60,
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_ItemEditPrice',
			text: '终端价',width:60,
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_ItemStepPrice',
			text: '阶梯价',width:60,
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_ItemContPrice',
			text: '合同价',width:60,
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_ItemPrice',
			text: '应收价',
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_IsSpread',
			text: '已返差价（仅财务状态查询界面）',
			width:60,align:'center',
			isBool:true,type:'bool'
		},{
			dataIndex: 'NRequestItem_SpreadMemo',
			text: '返差价说明（仅财务状态查询界面）',
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_FirstLocker',
			text: '对账人',width:80,
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_FirstLockedDate',
			text: '对账时间',width:130,
			isDate:true,hasTime:true
		},{
			dataIndex: 'NRequestItem_IsLocker',
			text: '财务锁定人',width:80,
			defaultRenderer: true
		},{
			dataIndex: 'NRequestItem_IsLockedDate',
			text: '财务锁定时间',width:130,
			isDate:true,hasTime:true
		},{
			dataIndex: 'NRequestItem_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];
		
		return columns;
	},
	
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			params = [];

		me.doFilterParams();

		var arr = [];
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;

		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true);

		//默认条件
		if (me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if (me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if (me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}
		var where = arr.join(") and (");
		if (where) where = "(" + where + ")";

		if (where) {
			url += '&strWhere=' + JShell.String.encode(where);
		}
		
		//做处理
		if (me.params.DateType) params.push("&dateType=" + me.params.DateType);
		if (me.params.StartDate) params.push("&startDate=" + me.params.StartDate);
		if (me.params.EndDate) params.push("&endDate=" + me.params.EndDate);
		if (me.params.Laboratory_Id) params.push("&labID=" + me.params.Laboratory_Id);
		if (me.params.TestItem_Id) params.push("&itemID=" + me.params.TestItem_Id);
		//if (me.parmas.deptID) params.push("&deptID=" + me.deptID);
		if (me.params.Dealer_Id) params.push("&dealerID=" + me.params.Dealer_Id);
		if (me.params.BillingUnit_Id) params.push("&billingUnitID=" + me.params.BillingUnit_Id);
		if (me.params.IsLocked) params.push("&sampleStatus=" + me.params.IsLocked);

		url += params.join("");

		return url;
	},
	/**@overwrite 条件处理*/
	doFilterParams: function() {
		var me = this,
			params = me.getComponent('filterToolbar').getParams();
		
		me.params = params;
		
		//内部数据条件
		var where = [];
		
		if (params.BillingUnitType) {
			where.push("(nrequestitem.BillingUnitType='" + params.BillingUnitType + "')");
		}
		if (params.ItemPriceType) {
			where.push("(nrequestitem.ItemPriceType='" + params.ItemPriceType + "')");
		}
		if (params.IsLocked) {
			where.push("(nrequestitem.IsLocked='" + params.IsLocked + "')");
		}
		if (params.IsSpread != null) {
			where.push("(nrequestitem.IsSpread='" + (params.IsSpread == true ? "1" : "0") + "')");
		}
		if (params.IsStepPrice != null) {
			where.push("(nrequestitem.IsStepPrice='" + (params.IsStepPrice == true ? "1" : "0") + "')");
		}
		if (params.IsFree != null) {
			where.push("(nrequestitem.IsFree='" + (params.IsFree == true ? "1" : "0") + "')");
		}
		
		if (params.Seller_AreaIn) {
			where.push("nrequestitem.BSeller.AreaIn like '%" + params.Seller_AreaIn + "%'");
		}
		if (params.NRequestForm_CName) {
			where.push("nrequestitem.NRequestForm.CName like '%" + params.NRequestForm_CName + "%'");
		}
		if (params.SerialNo) {
			where.push("nrequestitem.SerialNo='" + params.SerialNo + "'");
		}
		if (params.BarCode) {
			where.push("nrequestitem.BarCode='" + params.BarCode + "'");
		}
		
		if (params.Seller_Id) {
			where.push("nrequestitem.BSeller.Id='" + params.Seller_Id + "'");
		}
		
		me.internalWhere = where.join(" and ");
	},
	/**修改数据*/
	updateOneByParams: function(id, params) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;

		var params = Ext.JSON.encode(params);

		JShell.Server.post(url, params, function(data) {
			var record = me.store.findRecord(me.PKField, id);
			if (data.success) {
				if (record) {
					record.set(me.DelField, true);
					record.commit();
				}
				me.saveCount++;
			} else {
				me.saveErrorCount++;
				if (record) {
					record.set(me.DelField, false);
					record.commit();
				}
			}
			if (me.saveCount + me.saveErrorCount == me.saveLength) {
				me.hideMask(); //隐藏遮罩层
				if (me.saveErrorCount == 0) me.onSearch();
			}
		}, false);
	},
	/**导出EXCEL文件*/
	onExpExcelClick:function(){
		var me = this;
			
		me.doActionClick = true;
		
//		dateType={DATETYPE}&startDate={STARTDATE}&endDate={ENDDATE}&
//		labID={LABID}&itemID={ITEMID}&deptID={DEPTID}&dealerID={DEALERID}&
//		billingUnitID={BILLINGUNITID}&sampleStatus={SAMPLESTATUS}&
//		strWhere={STRWHERE}&reportType={REPORTTYPE}&operateType={OPERATETYPE}&
//		page={PAGE}&limit={LIMIT}&sort={SORT}
		
		var url = me.getExcelUrl();
		
		window.open(url);
	},
	getExcelUrl:function(){
		var me = this,
			operateType = '0',
			params = [];

		me.doFilterParams();

		var arr = [];
		var url = (me.downLoadExcelUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.downLoadExcelUrl;

		url += "?page=" + me.store.currentPage + "&limit=" + me.store.pageSize;
		url += "&reportType=" + me.reportType + "&operateType=" + operateType;

		//默认条件
		if (me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if (me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if (me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}
		var where = arr.join(") and (");
		if (where) where = "(" + where + ")";

		if (where) {
			url += '&strWhere=' + JShell.String.encode(where);
		}
		
		//做处理
		if (me.params.DateType) params.push("&dateType=" + me.params.DateType);
		if (me.params.StartDate) params.push("&startDate=" + me.params.StartDate);
		if (me.params.EndDate) params.push("&endDate=" + me.params.EndDate);
		if (me.params.Laboratory_Id) params.push("&labID=" + me.params.Laboratory_Id);
		if (me.params.TestItem_Id) params.push("&itemID=" + me.params.TestItem_Id);
		//if (me.parmas.deptID) params.push("&deptID=" + me.deptID);
		if (me.params.Dealer_Id) params.push("&dealerID=" + me.params.Dealer_Id);
		if (me.params.BillingUnit_Id) params.push("&billingUnitID=" + me.params.BillingUnit_Id);
		if (me.params.IsLocked) params.push("&sampleStatus=" + me.params.IsLocked);

		url += params.join("");
		
		return url;
	}
});