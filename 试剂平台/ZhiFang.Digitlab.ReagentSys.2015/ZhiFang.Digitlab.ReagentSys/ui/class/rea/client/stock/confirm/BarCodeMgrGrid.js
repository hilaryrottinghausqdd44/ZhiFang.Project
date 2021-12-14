/**
 * 盒条码明细
 * @author liangyl
 * @version 2017-09-08
 */
Ext.define('Shell.class.rea.client.stock.confirm.BarCodeMgrGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Ext.ux.CheckColumn'
	],
	title: '盒条码明细',
	width: 280,
	height: 350,
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsByHQL?isPlanish=true',
	/**是否启用刷新按钮*/
	hasRefresh: false,
	/**是否启用新增按钮*/
	hasAdd: false,
	/**是否启用修改按钮*/
	hasEdit: false,
	/**是否启用删除按钮*/
	hasDel: false,
	/**是否启用查询框*/
	hasSearch: false,
	/**默认加载数据*/
	defaultLoad: false,
	LogData:null,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	/**是否启用序号列*/
	hasRownumberer: false,
//  /**复选框*/
//	multiSelect: true,
//	selType: 'checkboxmodel',
	/**默认每页数量*/
	defaultPageSize: 1500,
	/**带分页栏*/
	hasPagingtoolbar: false,
	LinkData:[],
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			xtype:'checkcolumn',text:'勾选',dataIndex:'IsLinked',
			width:40,align:'center',sortable:false,menuDisabled:true,
			stopSelection:false,type:'boolean'
		},{
			dataIndex: 'SerialNo',
			text: '条码号',
			flex: 1
		}];
		return columns;
	},
	/**禁用所有的操作功能*/
	disableControl: function() {
		this.enableControl(true);
	},
	/**数据加载*/
	changeData: function(data) {
		var me=this;
		me.store.removeAll(); //清空数据
		var LogData=Ext.decode(data);
		if(LogData.length>0){
			for(var i = 0; i < LogData.length; i++) {
				var obj={
		        	SerialNo:LogData[i].SerialNo
		        }
				me.store.insert(me.getStore().getCount(),obj);
			}
		}
		me.setSerialNo();
	},
	/**获取选中行的条码号*/
	getSerialNoData: function() {
		var me=this,
		    records = me.store.data.items,
			len = records.length;
			
		var SerialNo='';
		for(var i=0;i<len;i++){
			var IsLinked = records[i].get('IsLinked');
			if(IsLinked){
				SerialNo+=','+records[i].get('SerialNo');
			}
		}
	    SerialNo=0==SerialNo.indexOf(",")?SerialNo.substr(1):SerialNo;
		return SerialNo;
	},
	/**还原选中值*/
	setSerialNo: function() {
		var me=this,
			list = me.LinkData,
			records = me.store.data.items,
			len = records.length;
			
		for(var i=0;i<len;i++){
			records[i].set({
				LinkId:'',
				IsLinked:false
			});
			records[i].commit();
		}
		for(var i in list){
			var rec = me.store.findRecord('SerialNo',list[i].SerialNo);
			if(rec) {
				rec.set({
					IsLinked:true
				});
				rec.commit();
			}
		}
	},
	/**扫码选中*/
	onSacnbarcode: function(value) {
		var me=this;
	    var rec = me.store.findRecord('SerialNo',value);
		if(rec) {
			rec.set({
				IsLinked:true
			});
		}
	}
});