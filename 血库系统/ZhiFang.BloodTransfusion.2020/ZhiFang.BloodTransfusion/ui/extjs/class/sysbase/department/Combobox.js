/**
 * 科室下拉框
 * @author longfc
 * @version 2020-03-26
 */
Ext.define('Shell.class.sysbase.department.Combobox', {
	extend: 'Shell.ux.panel.AppPanel',

	/**后台排序*/
	remoteSort: true,
	/**
	 * 排序字段
	 * 
	 * @exception 
	 * [{property:'DContractPrice_ContractPrice',direction:'ASC'}]
	 */
	defaultOrderBy: [{
		property: 'Department_DispOrder',
		direction: 'ASC'
	}],

	initComponent: function() {
		var me = this;
		me.store = me.createStore();
		me.callParent(arguments);
	},
	/**创建数据集*/
	createStore: function() {
		var me = this;
		return Ext.create('Ext.data.Store', {
			fields: me.getStoreFields(),
			remoteSort: me.remoteSort,
			sorters: me.defaultOrderBy,
			proxy: {
				type: 'ajax',
				url: '',
				reader: {
					type: 'json',
					totalProperty: 'count',
					root: 'list'
				},
				extractResponseData: function(response) {
					var data = JShell.Server.toJson(response.responseText);
					if (data.success) {
						var info = data.value;
						if (info) {
							var type = Ext.typeOf(info);
							if (type == 'object') {
								info = info;
							} else if (type == 'array') {
								info.list = info;
								info.count = info.list.length;
							} else {
								info = {};
							}

							data.count = info.count || 0;
							data.list = info.list || [];
						} else {
							data.count = 0;
							data.list = [];
						}
						data = me.changeResult(data);
						me.fireEvent('changeResult', me, data);
					} else {
						me.errorInfo = data.msg;
					}
					response.responseText = Ext.JSON.encode(data);

					return response;
				}
			},
			listeners: {
				load: function(store, records, successful) {
					me.onAfterLoad(records, successful);
				}
			}
		});
	},
	/**创建数据字段*/
	getStoreFields: function() {
		return [{
				name: 'Department_CName',
				type: 'auto'
			},
			{
				name: 'Department_Id',
				type: 'auto'
			},
			{
				name: 'Department_DispOrder',
				type: 'int'
			}
		];
	},
	onAfterLoad: function() {
		var me = this;
		if (me.hasSetValue) {

		}
	}
});
