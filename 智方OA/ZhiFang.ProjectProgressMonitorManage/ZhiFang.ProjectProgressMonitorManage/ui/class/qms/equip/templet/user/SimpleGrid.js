/**
 * 模板列表(简单)
 * @author liangyl
 * @version 2016-08-24
 */
Ext.define('Shell.class.qms.equip.templet.user.SimpleGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '仪器模板列表',
	/**获取数据服务路径*/
	selectUrl: '/QMSReport.svc/ST_UDTO_SearchETempletEmpByHQL?isPlanish=true',
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'ETempletEmp_ETemplet_CName',
		direction: 'ASC'
	}],
	height:500,
	width:400,	
	/**默认加载数据*/
	defaultLoad: true,
	/**默认选中数据*/
	autoSelect: true,
	autoScroll: false,
	/**主键列*/
	PKField: 'ETempletEmp_Id',
	defaultWhere:'',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);  
	},
	initComponent: function() {
		var me = this;
	    var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		me.defaultWhere = 'etempletemp.HREmployee.Id=' + userId;
		
		//查询框信息
		me.searchInfo = {
			width: 160,
			emptyText: '模板名称',
			isLike: true,
			itemId: 'search',
			fields: ['etempletemp.ETemplet.CName']
		};
		me.buttonToolbarItems = ['refresh', '->', {
			type: 'search',
			info: me.searchInfo
		}];
		//创建数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text:'编号',dataIndex:'ETempletEmp_Id',width:150,
			sortable:false,menuDisabled:true,defaultRenderer:true,hidden:true
		},{
			text:'仪器模板id',dataIndex:'ETempletEmp_ETemplet_Id',width:150,
			sortable:false,menuDisabled:true,defaultRenderer:true,hidden:true
		},{
			text:'仪器名称',dataIndex:'ETempletEmp_ETemplet_EEquip_CName',width:100,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'质量记录名称',dataIndex:'ETempletEmp_ETemplet_CName',flex:1,
			sortable:true,menuDisabled:true,defaultRenderer:true
		},{
			text:'模板代码',dataIndex:'ETempletEmp_ETemplet_UseCode',width:100,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text: '开始时间',
			dataIndex: 'ETempletEmp_ETemplet_BeginDate',
			width: 80,
			sortable:false,menuDisabled:true,hidden:true,
			isDate:true
		}, {
			text: '结束时间',
			dataIndex: 'ETempletEmp_ETemplet_EndDate',
			width: 80,
			isDate:true,hidden:true,
			sortable:false,menuDisabled:true
		}];
		return columns;
	}
});