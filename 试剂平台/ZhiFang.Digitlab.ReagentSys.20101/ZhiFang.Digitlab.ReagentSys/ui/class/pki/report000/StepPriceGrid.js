/**
 * 月度阶梯报表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.report.StepPriceGrid',{
    extend:'Shell.class.pki.report.BasicGrid',
    title:'月度阶梯报表',
    
	/**报表类型*/
	reportType:'2',
	
	/**创建数据列*/
	createGridColumns:function(){
		//查询结果分组：经销商，项目，录入时间的月份																					
		//合计：样本数量合计，个人样本数量合计，免单样本数量合计，医院样本数量合计，
		//阶梯样本数合计（=样本数量合计-免单样本），
		//阶梯价合计（注意：该合计不是阶梯样本合计*阶梯单价，请按照需求列表对阶梯价的描述分情况计算 ）
		
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
		}];
		
		return columns;
	}
});