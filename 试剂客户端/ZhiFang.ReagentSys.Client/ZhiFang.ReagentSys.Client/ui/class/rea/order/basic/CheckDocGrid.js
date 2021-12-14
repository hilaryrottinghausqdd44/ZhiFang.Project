/**
 * 订货单选择列表
 * @author Jcall
 * @version 2018-01-12
 */
Ext.define('Shell.class.rea.order.basic.CheckDocGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'订货单选择列表',
    width:800,
    height:500,
    
    /**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchBmsCenOrderDocByHQL?isPlanish=true',
    
	initComponent:function(){
		var me = this;
		
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere){
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += '(bmscenorderdoc.DeleteFlag=0 or bmscenorderdoc.DeleteFlag is null)';
		
		//查询框信息
		me.searchInfo = {
			width:160,isLike:true,itemId: 'Search',
			emptyText:'订货单号',
			fields:['bmscenorderdoc.OrderDocNo']
		};
		
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			dataIndex: 'BmsCenOrderDoc_OperDate',
			text: '订购日期',
			align:'center',
			width: 130,
			isDate:true,
			hasTime:true
		},{
			dataIndex: 'BmsCenOrderDoc_OrderDocNo',
			text: '订货单号',
			width: 150,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDoc_TotalPrice',
			text: '总价',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDoc_UrgentFlag',
			text: '紧急标志',
			align:'center',
			width: 60,
			renderer: function(value, meta) {
				var info = JShell.REA.Enum.BmsCenOrderDoc_UrgentFlag['E' + value] || {};
				var v = info.value || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + (info.bcolor || '#FFFFFF') + 
					';color:' + (info.color || '#000000');
				return v;
			}
		},{
			dataIndex: 'BmsCenOrderDoc_Status',
			text: '单据状态',
			align:'center',
			width: 60,
			renderer: function(value, meta) {
				var info = JShell.REA.Enum.BmsCenOrderDoc_Status['E' + value] || {};
				var v = info.value || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + (info.bcolor || '#FFFFFF') + 
					';color:' + (info.color || '#000000');
				return v;
			}
		},{
			dataIndex: 'BmsCenOrderDoc_Comp_CName',
			text: '供货方',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDoc_Lab_CName',
			text: '订货方',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDoc_DeptName',
			text: '订购部门',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDoc_UserName',
			text: '订购人员',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDoc_Checker',
			text: '审核人员',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDoc_CheckTime',
			text: '审核日期',
			align:'center',
			width: 130,
			isDate:true,
			hasTime:true
		},{
			dataIndex: 'BmsCenOrderDoc_Confirm',
			text: '确认人员',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDoc_ConfirmTime',
			text: '确认时间',
			width: 130,
			isDate:true,
			hasTime:true
		},{
			dataIndex: 'BmsCenOrderDoc_LabMemo',
			text: '订货方备注',
			width: 150,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDoc_CompMemo',
			text: '供货方备注',
			width: 150,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDoc_Memo',
			text: '供货单备注',
			width: 150,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDoc_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];
		
		return columns;
	}
});