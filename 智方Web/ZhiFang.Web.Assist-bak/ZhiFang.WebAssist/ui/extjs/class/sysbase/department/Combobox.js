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
	
	/**默认数据条件*/
	defaultWhere: '',
	/**内部数据条件*/
	internalWhere: '',
	/**外部数据条件*/
	externalWhere: '',
	
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchDepartmentByHQL?isPlanish=true',
	
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
				beforeload: function() {
					return me.onBeforeLoad();
				},
				load: function(store, records, successful) {
					me.onAfterLoad(records, successful);
				}
			}
		});
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		
		me.store.proxy.url = me.getLoadUrl(); //查询条件
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :me.getPathRoot() + me.selectUrl);
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
		//默认条件
		if (me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if (me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if (me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}
		var where = arr.join(") and (");
		if (where) where = "(" + where + ")";
	
		if (where) {
			url += '&where=' + JShell.String.encode(where);
		}
	
		return url;
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
				name: 'Department_ShortCode',
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
