/**
 * 开票方对账报表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.report2.BillingUnitApp',{
    extend:'Shell.class.pki.report2.ReportApp',
    title:'开票方对账报表',
    
    /**报表类型*/
	reportType:'4',
	/**创建数据列*/
	createGridColumns:function(){
		//查询结果分组：开票方，录入时间的月份
		//合计：样本数量合计，免单数量合计，个人数量合计,应收价合计，免单金额合计，个人金额（终端价）合计
		
		var columns = [{
			dataIndex: 'DStatClass_BillingUnitName',
			text: '开票方',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_StatDate',
			text: '录入时间',
			defaultRenderer: true
		},{
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
			dataIndex: 'DStatClass_ItemPriceSum',
			text: '应收价合计',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_FreePriceSum',
			text: '免单金额合计',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_EditPriceSum',
			text: '个人金额(终端价)合计',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_BillingUnitID',
			text: '开票方ID',
			hidden: true,
			hideable: false
		}];
		
		return columns;
	}
});