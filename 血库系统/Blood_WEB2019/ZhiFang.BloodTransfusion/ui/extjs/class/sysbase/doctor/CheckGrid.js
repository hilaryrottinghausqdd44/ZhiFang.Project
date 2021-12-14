/**
 * 医生选择列表
 * @author longfc
 * @version 2020-03-26
 */
Ext.define('Shell.class.sysbase.doctor.CheckGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '医生选择列表',
	width: 270,
	height: 300,

	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchDoctorByHQL?isPlanish=true',
	defaultOrderBy: [{
		property: 'Doctor_DispOrder',
		direction: 'ASC'
	}],
	/**是否单选*/
	checkOne: true,
	searchInfoVal: "",

	initComponent: function() {
		var me = this;

		me.defaultWhere = me.defaultWhere || '';
		if (me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'doctor.Visible=1';

		//查询框信息
		me.searchInfo = {
			width: 145,
			emptyText: '编码/名称/Code1',
			isLike: true,
			itemId: "search",
			value: me.searchInfoVal,
			fields: ['doctor.Id', 'doctor.CName', 'doctor.Code1']
		};
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '编码',
			dataIndex: 'Doctor_Id',
			width: 100,
			isKey: true,
			hideable: false
		}, {
			text: '名称',
			dataIndex: 'Doctor_CName',
			width: 100,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: 'Code1',
			dataIndex: 'Doctor_Code1',
			width: 65,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: 'Code2',
			dataIndex: 'Doctor_Code2',
			width: 65,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: 'Code3',
			dataIndex: 'Doctor_Code3',
			width: 65,
			menuDisabled: true,
			defaultRenderer: true
		}]

		return columns;
	},
	setSearchValue: function(value,isSearch) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var search = buttonsToolbar.getComponent('search');
		if(search){
			search.setValue(value);
			if(isSearch)me.onSearch();
		}
	}
});
