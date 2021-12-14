/**
 * 字典列表
 * @author longfc
 * @version 2016-10-11
 */
Ext.define('Shell.class.sysbase.pdict.PDictGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '字典列表',
	width: 270,
	/**默认加载*/
	defaultLoad: true,
	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPDictByHQL?isPlanish=true',
	defaultOrderBy: [{
		property: 'PDict_DispOrder',
		direction: 'ASC'
	}],
	/**是否单选*/
	checkOne: false,
	/**是否手工添加全部的选择行*/
	isAddAllRecord: true,
	hasRownumberer: false,
	initComponent: function() {
		var me = this;

		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'pdict.IsUse=1';

		//查询框信息
		me.searchInfo = {
			width: 165,
			emptyText: '名称',
			isLike: true,
			fields: ['pdict.CName']
		};
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [];
		//重新调整序号
		columns.push({
			xtype: 'rownumberer',
			text: me.Shell_ux_grid_Panel.NumberText,
			width: me.rowNumbererWidth,
			align: 'center',
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				//rowIndex = (rowIndex >= 1 ? rowIndex - 1 : rowIndex);
				return rowIndex;
			}
		});
		columns.push({
			text: '名称',
			dataIndex: 'PDict_CName',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '备注',
			dataIndex: 'PDict_Memo',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '主键ID',
			dataIndex: 'PDict_Id',
			isKey: true,
			hidden: true,
			hideable: false
		});

		return columns;
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me = this;
		if(me.isAddAllRecord == true) {
			var record = {
				'PDict_Id': "-1",
				'PDict_CName': "全部",
				'PDict_Memo': ''
			};
			if(data.list) data.list.unshift(record);
			if(data.count) data.count = data.count + 1;
		}
		return data;
	}
});