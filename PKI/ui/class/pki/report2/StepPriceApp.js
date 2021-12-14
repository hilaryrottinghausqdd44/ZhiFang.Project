/**
 * 月度阶梯报表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.report2.StepPriceApp',{
    extend:'Shell.class.pki.report2.ReportApp',
    title:'月度阶梯报表',

    /**报表类型*/
	reportType:'2',
	  /**阶梯价格计算*/
	isladderPrice:'1',
	/**创建数据列*/
	createGridColumns:function(){

		var columns = [{
			dataIndex: 'DStatClass_DealerName',
			text: '经销商',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_ItemName',
			text: '项目',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_StatDate',
			text: '录入时间的月份',
			defaultRenderer: true
		},{
			dataIndex: 'DStatClass_AllItemCount',
			text: '样本数量合计',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_PersonalCount',
			text: '个人样本数量合计',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_FreeCount',
			text: '免单样本数量合计',
			defaultRenderer: true
		},{
			dataIndex: 'DStatClass_HospitalCount',
			text: '医院样本数量合计',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_StepPriceCount',
			text: '阶梯样本数合计',
			defaultRenderer: true
		}, {
			dataIndex: 'DStatClass_StepPriceSum',
			text: '阶梯价合计',
			defaultRenderer: true
		},{
			dataIndex: 'DStatClass_DealerID',
			text: '经销商ID',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'DStatClass_ItemID',
			text: '项目ID',
			hidden: true,
			hideable: false
		}];

		return columns;
	}
});