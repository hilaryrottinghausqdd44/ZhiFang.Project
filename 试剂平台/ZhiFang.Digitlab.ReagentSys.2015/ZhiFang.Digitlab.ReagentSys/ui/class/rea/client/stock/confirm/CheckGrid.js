/**
 * 验收总单选择
 * @author liangyl
 * @version 2017-12-06
 */
Ext.define('Shell.class.rea.client.stock.confirm.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'验收单选择列表',
    width:570,
    height:400,
    
	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDocConfirmByHQL?isPlanish=true',
	
    /**是否单选*/
	checkOne:true,
    /**默认加载数据*/
//	defaultLoad:false,
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		//查询框信息
		me.searchInfo = {
			width:155,emptyText:'验收单号',isLike:true,itemId:'Search',
			fields:['bmscensaledocconfirm.SaleDocConfirmNo']
		};		
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'BmsCenSaleDocConfirm_DataAddTime',
			text: '申请时间',align: 'center',
			width: 130,isDate: true,hasTime: true,defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDocConfirm_SaleDocConfirmNo',
			text: '验收单号',width: 100,defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDocConfirm_ReaCompName',
			text: '供货方',width: 100,defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDocConfirm_AccepterName',
			text: '主验收人',width: 80,defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDocConfirm_Id',text: '主键ID',hidden: true,	hideable: false,isKey: true
		}, {
			dataIndex: 'BmsCenSaleDocConfirm_ReaCompID',text: '供货方ID',hidden: true,hideable: false
		}];
		return columns;
	},
	initButtonToolbarItems:function(){
		var me = this;
		if(!me.searchInfo.width) me.searchInfo.width = 205;
		//自定义按钮功能栏
		me.buttonToolbarItems = me.buttonToolbarItems || [];
		me.buttonToolbarItems.push('refresh','-',{type:'search',info:me.searchInfo});
	    me.buttonToolbarItems.push('->','accept');
	}
});