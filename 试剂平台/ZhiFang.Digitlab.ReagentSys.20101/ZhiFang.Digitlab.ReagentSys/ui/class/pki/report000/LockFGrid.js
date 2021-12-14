/**
 * 月度财务锁定报表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.report.LockFGrid',{
    extend:'Shell.class.pki.report.BasicGrid',
    title:'月度财务锁定报表',
    
	/**报表类型*/
	reportType:'3',
	
	/**创建数据列*/
	createGridColumns:function(){
		//查询结果分组：销售，经销商，录入时间的月份																						
		//合计：应收价合计，样本数量合计，免单数量合计，个人数量合计	
		
		var columns = [{
			dataIndex: 'DStatClass_SellerName',
			text: '销售',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_DealerName',
			text: '经销商',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_StatDate',
			text: '录入时间',
			defaultRenderer: true
		},{
			dataIndex: 'DStatClass_ItemPriceSum',
			text: '应收价合计',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_AllItemCount',
			text: '样本数量合计',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_FreeCount',
			text: '免单数量合计',
			defaultRenderer: true
		},{
			dataIndex: 'DStatClass_PersonalCount',
			text: '个人数量合计',
			defaultRenderer: true
		},{
			dataIndex: 'DStatClass_SellerID',
			text: '销售ID',
			hidden: true,
			hideable: false
		}];
		
		return columns;
	}
});