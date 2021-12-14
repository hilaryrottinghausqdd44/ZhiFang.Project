/**
 * 统计-试剂消耗
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.statistics.ConsumePanel', {
	extend: 'Shell.class.rea.statistics.ConsumeLineChart',
	requires: ['Shell.ux.form.field.CheckTrigger'],
	
	title: '试剂消耗统计',
	width:1200,
	height:800,
	
	/**获取数据服务路径*/
	selectUrl: '/ReagentService.svc/ST_UDTO_StatReagentConsume',
	/**默认条件*/
	defaultWhere:'',
	
	/**机构类型*/
    ORGTYPE:null,
    
	LabId:null,
	CompId:null,
	ProdId:null,
	GoodsId:null,
	StartDate:null,
	EndDate:null,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.load();
	},
	initComponent: function() {
		var me = this;
		//me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	
	load:function(){
		var me = this,
			url = JShell.System.Path.ROOT + me.selectUrl;
			
		//试剂名称,库存数量,总计金额
		var fields = [
			'CenQtyDtlTempHistory_GoodsName','CenQtyDtlTempHistory_GoodsQty','CenQtyDtlTempHistory_SumTotal'
		];
//		declare @LabID varchar(200)--实验室
//	  	declare @CompID varchar(200)--供应商
//	  	declare @GoodsID varchar(200)--产品
//	  	declare @ProdID varchar(200)--厂商
//	  	declare @StartDate  varchar(20)--开始时间
//	  	declare @EndDate  varchar(20)--结束时间
		var strPara = [];
		strPara.push(me.LabId);
		strPara.push(me.CompId);
		strPara.push(me.ProdId);
		strPara.push('');
		strPara.push(me.StartDate);
		strPara.push(me.EndDate);
		
		//(string strPara, int groupByType, string fields, bool isPlanish)
		var params = {
			isPlanish:true,
			strPara:strPara.join(','),
			groupByType:0,
			fields:fields.join(',')
		};
		params = Ext.JSON.encode(params);
		
		JShell.Server.post(url,params,function(data){
			var obj = {list:data.value.list};
			me.changeData(obj);
		});
	}
});