/**
 * 报表查询基础列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.report.BasicGrid',{
    extend:'Shell.ux.grid.PostPanel',
    title:'报表查询基础列表',
    
    /**获取数据服务路径*/
    selectUrl:'/StatService.svc/Stat_UDTO_ReportReconciliation',
    
    /**默认加载*/
	defaultLoad:true,
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
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.getComponent('filterToolbar').on({
			search:function(p,params){
				me.onSearch();
			}
		});
		
		me.on({
			itemdblclick:function(view,record){
				me.showInfo(record);
			}
		});
	},
	initComponent:function(){
		var me = this;
		
		//数据列
		me.columns = me.createGridColumns();
		
		me.columns.push({
			xtype: 'actioncolumn',
			text: '操作',
			align: 'center',
			width: 40,
			hideable: false,
			items: [{
				iconCls: 'button-show hand',
				tooltip: '查看明细',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.showInfo(rec);
				}
			}]
		});
		
		//创建挂靠功能栏
		me.dockedItems = me.dockedItems || [Ext.create('Shell.class.pki.report.SearchToolbar',{
			itemId:'filterToolbar',
			dock:'top'
		})];
		
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
		return me.callParent(arguments);
	},
	/**@overwrite 条件处理*/
	doFilterParams: function() {
		var me = this,
			params = me.getComponent('filterToolbar').getParams(),
			ParaClass = {};
			
		//销售=，经销商=，送检单位=，项目=，开票方类型=，开票方=，样本预制条码=，样本实验室条码=，
		//病人名=，采样时间（阶段），录入时间（阶段），核收时间（阶段），是否免单（价格类型）=，状态(默认财务锁定)=
		
		//参数类ParaClass
		//时间类型=DateType、开始时间=BeginDate、结束时间=EndDate、
		//病人名称=PatName、是否免单=IsFree、状态=SampleStatus、
		//销售ID=SellerID，经销商ID=DealerID，送检单位ID=SLabID，
		//项目ID=ItemID，开票方类型=BillingUnitType，开票方ID=BillingUnitID，
		//样本预制条码=SerialNo，样本实验室条码=BarCode，
		
//		var textList = ['SerialNo','BarCode','NRequestForm_CName'];
//		var comboList = ['BillingUnitType','IsLocked','IsFree'];
//		var checkList = [
//			'Laboratory_Id', 'TestItem_Id', 'Dealer_Id',
//			'BillingUnit_Id', 'Seller_Id'
//		];
		
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
	},
	/**查看明细*/
	showInfo: function(record) {
		//JShell.Msg.overwrite('showInfo方法');
		
		var me = this;
			postParams = me.postParams;
			
		var SellerID = record.get('DStatClass_SellerID');
		
		if(!SellerID){
			JShell.Msg.error('缺少销售信息，不能查看明细！');
			return;
		}
			
		var dArr = record.get('DStatClass_StatDate').split('-');
		
		postParams.entity.SellerID = SellerID;
		postParams.entity.BeginDate = JShell.Date.getMonthFirstDate(dArr[0],dArr[1],true);
		postParams.entity.EndDate = JShell.Date.getMonthLastDate(dArr[0],dArr[1],true);

		JShell.Win.open('Shell.class.pki.report.BasicInfoGrid', {
			resizable: false,
			postParams:postParams,
			defaultLoad:true,
			title:me.title + '-明细'
		}).show();
	}
});