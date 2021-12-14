/**
 * 报表查询基础列表
 * @author longfc
 * @version 2017-01-12
 */
Ext.define('Shell.class.pki.report2.details.Grid',{
    extend:'Shell.ux.grid.Panel',
    title:'报表查询基础列表',

    /**获取数据服务路径Stat_UDTO_ReportReconciliation*/
    selectUrl:'/StatService.svc/Stat_UDTO_ReportReconciliationDetail',

    /**默认加载*/
	defaultLoad:false,
	/**后台排序*/
	remoteSort:false,
	/**带分页栏*/
	hasPagingtoolbar:true,
	/**是否启用序号列*/
	hasRownumberer:true,
	/**带功能按钮栏*/
	hasButtontoolbar:false,
	/**是否显示导出Excel操作列*/
	isHiddenEXCELActioncolumn:false,
	/**报表类型*/
	reportType:'1',
	/**查询参数*/
	params:null,

	/**是否处于点击功能按钮状态*/
	doActionClick:false,

	initComponent:function(){
		var me = this;

		me.addEvents('download');
		//数据列
		me.columns = me.createGridColumns();

		me.columns.push({
			xtype: 'actioncolumn',
			sortable:false,
			text: '导出',
			align: 'center',
			width: 40,
			style:'font-weight:bold;color:white;background:orange;',
			hidden: me.isHiddenEXCELActioncolumn,
			hideable: false,
			items: [{
				iconCls:'file-excel hand',
				tooltip:'导出EXCEL文件',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.doActionClick = true;
					me.fireEvent('download',me,rec);
				}
			}]
		});

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		return [];
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		me.doFilterParams();
		me.postParams.fields = me.getStoreFields(true).join(',');
		var url = (me.selectUrl.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.selectUrl;
		
		url += '?isPlanish=' + me.postParams.isPlanish + 
			'&reportType=' + me.postParams.reportType + 
			'&fields=' + me.postParams.fields + 
			'&entityJson=' + Ext.JSON.encode(me.postParams.entity);
		
		return url;
	},
	/**@overwrite 条件处理*/
	doFilterParams: function() {
		var me = this,
			params = me.params || {},
			ParaClass = {};

		if(params.DateType){ParaClass.DateType = params.DateType;}
		if(params.StartDate){ParaClass.BeginDate = params.StartDate;}
		if(params.EndDate){ParaClass.EndDate = params.EndDate;}

		if(params.NRequestForm_CName){ParaClass.PatName = params.NRequestForm_CName;}
		if(params.IsFree){ParaClass.IsFree = params.IsFree;}
		if(params.IsLocked){ParaClass.SampleStatus = params.IsLocked;}

		if(params.Seller_Id){ParaClass.SellerID = params.Seller_Id;}
		if(params.Dealer_Id){ParaClass.DealerID = params.Dealer_Id;}
		if(params.Laboratory_Id){ParaClass.SLabID = params.Laboratory_Id;}

		if(params.TestItem_Id){ParaClass.ItemID = params.TestItem_Id;}
		if(params.BillingUnitType){ParaClass.BillingUnitType = params.BillingUnitType;}
		if(params.BillingUnit_Id){ParaClass.BillingUnitID = params.BillingUnit_Id;}

		if(params.SerialNo){ParaClass.SerialNo = params.SerialNo;}
		if(params.BarCode){ParaClass.BarCode = params.BarCode;}

		me.postParams = {
			entity:ParaClass,
			reportType:me.reportType,
			isPlanish:true,
			fields:me.getStoreFields(true).join(',')
		};
	}
});