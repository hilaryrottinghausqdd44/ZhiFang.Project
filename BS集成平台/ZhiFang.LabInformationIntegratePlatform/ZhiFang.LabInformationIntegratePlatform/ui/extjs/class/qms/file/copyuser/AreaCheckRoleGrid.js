Ext.define('Shell.class.qms.file.copyuser.AreaCheckRoleGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '角色选择列表',
	width: 295,
	height: 500,
	checkOne: false,
	PKCheckField: 'RBACRole_Id',
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchBHospitalAreaByHQL?isPlanish=true',
	initComponent: function() {
		var me = this;
		//查询框信息me.searchInfo||
		me.searchInfo = {
			width: 170,
			emptyText: '角色名称',
			isLike: true,
			fields: ['btestitem.CName']
		};
		//数据列
		me.columns = [{
			dataIndex: 'BHospitalArea_Name',
			text: '名称',
			width: 200,
			defaultRenderer: true
		}, {
				dataIndex: 'BHospitalArea_Id',
			text: '角色ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
				dataIndex: 'BHospitalArea_DataTimeStamp',
			text: '角色时间戳',
			hidden: true,
			hideable: false
		}];
		me.fireEvent('load', me);
		me.callParent(arguments);
	}
});