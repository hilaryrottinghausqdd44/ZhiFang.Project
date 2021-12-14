/**
 * 基础列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.ux.grid.Panel', {
	extend: 'Ext.grid.Panel',
	alias: 'widget.uxGridPanel',
	mixins:['Shell.ux.Langage'],
	
	/**语言包内容*/
	Shell_ux_grid_Panel:{
		NumberText: '序号',
	},

	/**获取数据服务路径*/
	selectUrl: JShell.System.Path.DEFAULT_ERROR_URL,
	/**删除数据服务路径*/
	delUrl: JShell.System.Path.DEFAULT_ERROR_URL,

	/**开启加载数据遮罩层*/
	hasLoadMask: true,
	/**加载数据提示*/
	loadingText: JShell.Server.LOADING_TEXT,
	/**保存数据提示*/
	saveText: JShell.Server.SAVE_TEXT,
	/**删除数据提示*/
	delText: JShell.Server.DEL_TEXT,

	/**默认数据条件*/
	defaultWhere: '',
	/**内部数据条件*/
	internalWhere: '',
	/**外部数据条件*/
	externalWhere: '',

	/**主键列*/
	PKField: 'Id',
	/**删除标志字段*/
	DelField: 'delState',
	/**默认加载数据*/
	defaultLoad: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: true,
	/**后台排序*/
	remoteSort: true,
	/**
	 * 排序字段
	 * 
	 * @exception 
	 * [{property:'DContractPrice_ContractPrice',direction:'ASC'}]
	 */
	defaultOrderBy: [],
	/**默认每页数量*/
	defaultPageSize: 50,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**错误信息样式*/
	errorFormat: '<div style="color:red;text-align:center;margin:5px;font-weight:bold;">{msg}</div>',
	/**消息信息样式*/
	msgFormat: '<div style="color:blue;text-align:center;margin:5px;font-weight:bold;">{msg}</div>',
	/**错误信息*/
	errorInfo: null,

	/**是否启用序号列*/
	hasRownumberer: true,
	/**默认选中数据，默认第一行，也可以默认选中其他行，也可以是主键的值匹配*/
	autoSelect: true,

	/**是否启用刷新按钮*/
	hasRefresh: false,
	/**是否启用新增按钮*/
	hasAdd: false,
	/**是否启用修改按钮*/
	hasEdit: false,
	/**是否启用删除按钮*/
	hasDel: false,
	/**是否启用查看按钮*/
	hasShow: false,
	/**是否启用保存按钮*/
	hasSave: false,
	/**是否启用查询框*/
	hasSearch: false,
	/**查询框信息*/
	searchInfo: {
		width: 120,
		emptyText: '',
		isLike: false,
		fields: []
	},

	/**自定义按钮功能栏*/
	buttonToolbarItems: null,
	/**分页栏自定义功能组件*/
	agingToolbarCustomItems: null,
	/**隐藏删除列*/
	hideDelColumn: false,
	/**序号列宽度*/
	rowNumbererWidth: 50,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		if (me.defaultLoad) {
			me.onSearch();
		} else {
			if (me.defaultDisableControl) {
				me.disableControl();
			}
		}
	},

	initComponent: function() {
		var me = this;
		
		//替换语言包
		me.changeLangage('Shell.ux.grid.Panel');
		
		me.addEvents('changeResult', 'nodata');

		me.viewConfig = me.viewConfig || {};
		me.viewConfig.loadMask = me.hasLoadMask;
		me.viewConfig.loadingText = me.loadingText;
		//文本复制
		me.viewConfig.enableTextSelection =
			me.viewConfig.enableTextSelection == false ? false : true;
		//创建数据列
		me.columns = me.createColumns();
		//创建数据集
		me.store = me.createStore();
		//创建挂靠功能栏
		me.dockedItems = me.createDockedItems();
		
		me.callParent(arguments);
	},
	/**创建数据集*/
	createStore: function() {
		var me = this;
		return Ext.create('Ext.data.Store', {
			fields: me.getStoreFields(),
			pageSize: me.defaultPageSize,
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
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		return data;
	},
	/**创建数据列*/
	createColumns: function() {
		var me = this,
			columns = me.columns || [];

		if (me.hasRownumberer && columns[0].xtype != 'rownumberer') {
			columns.unshift({
				xtype: 'rownumberer',
				text: me.Shell_ux_grid_Panel.NumberText,
				width: me.rowNumbererWidth,
				align: 'center'
			});
		}

		var len = columns.length;

		for (var i = 0; i < len; i++) {
			if (columns[i].isKey) {
				me.PKField = columns[i].dataIndex;
				continue;
			}
			if (columns[i].isDate) {
				columns[i].renderer = function(value, meta, record, rowIndex, colIndex) {
					var bo = me.columns[colIndex].hasTime ? false : true;
					var v = JShell.Date.toString(value, bo) || '';
					if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
					return v;
				}
				continue;
			}
			if (columns[i].isBool) {
				columns[i].renderer = function(value, meta) {
					var v = value + '';
					if (v == 'true') {
						meta.style = 'color:green';
						v = JShell.All.TRUE;
					} else if (v == 'false') {
						meta.style = 'color:red';
						v = JShell.All.FALSE;
					} else {
						v == '';
					}

					return v;
				}
				continue;
			}

			if (columns[i].defaultRenderer) {
				columns[i].renderer = function(value, meta) {
					if (value) {
						value = Ext.typeOf(value) == 'string' ? value.replace(/"/g, '&quot;') : value;
						meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
					}
					return value;
				}
			}
		}

		if (me.hasDel && me.multiSelect == true && me.selType == 'checkboxmodel') {
			columns.push({
				dataIndex: me.DelField,
				text: '',
				width: 40,
				hideable: false,
				sortable: false,
				hidden: me.hideDelColumn,
				menuDisabled: true,
				renderer: function(value,meta, record) {
					var v = '';
					if (value === 'true') {
						v = '<b style="color:green">' + JShell.All.SUCCESS_TEXT + '</b>';
					}
					if (value === 'false') {
						v = '<b style="color:red">' + JShell.All.FAILURE_TEXT + '</b>';
					}
					var msg = record.get('ErrorInfo');
					if(msg){
						meta.tdAttr = 'data-qtip="<b style=\'color:red\'>' + msg + '</b>"';
					}
					
					return v;
				}
			});
		}

		return columns;
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];

		if (me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if (me.hasPagingtoolbar) items.push(me.createPagingtoolbar());

		return items;
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = me.buttonToolbarItems || [];

		if (items.length == 0) {
			if (me.hasRefresh) items.push('refresh');
			if (me.hasAdd) items.push('add');
			if (me.hasEdit) items.push('edit');
			if (me.hasDel) items.push('del');
			if (me.hasShow) items.push('show');
			if (me.hasSave) items.push('save');
			if (me.hasSearch) items.push('->', {
				type: 'search',
				info: me.searchInfo
			});
		}

		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	/**创建分页栏*/
	createPagingtoolbar: function() {
		var me = this;

		var config = {
			dock: 'bottom',
			itemId:'pagingToolbar',
			store: me.store
		};

		if (me.defaultPageSize) config.defaultPageSize = me.defaultPageSize;
		if (me.pageSizeList) config.pageSizeList = me.pageSizeList;
		//分页栏自定义功能组件
		if (me.agingToolbarCustomItems) config.customItems = me.agingToolbarCustomItems;

		return Ext.create('Shell.ux.toolbar.Paging', config);
	},
	/**创建数据字段*/
	getStoreFields: function(isString) {
		var me = this,
			columns = me.columns || [],
			length = columns.length,
			fields = [];

		for (var i = 0; i < length; i++) {
			if (columns[i].dataIndex) {
				var obj = isString ? columns[i].dataIndex : {
					name: columns[i].dataIndex,
					type: columns[i].type ? columns[i].type : 'string'
				};
				fields.push(obj);
			}
		}

		return fields;
	},

	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		
		me.getView().update();
		me.store.proxy.url = me.getLoadUrl(); //查询条件
		
		me.disableControl(); //禁用 所有的操作功能
		if (!me.defaultLoad) return false;
	},
	/**加载数据后*/
	onAfterLoad: function(records, successful) {
		var me = this;

		me.enableControl(); //启用所有的操作功能

		if (me.errorInfo) {
			var error = me.errorFormat.replace(/{msg}/, me.errorInfo);
			me.getView().update(error);
			me.errorInfo = null;
		} else {
			if (!records || records.length <= 0) {
				var msg = me.msgFormat.replace(/{msg}/, JShell.Server.NO_DATA);
				me.getView().update(msg);
			}
		}

		if (!records || records.length <= 0) {
			me.fireEvent('nodata', me);
			return;
		}
		//默认选中处理
		me.doAutoSelect(records, me.autoSelect);
	},
	/**默认选中处理*/
	doAutoSelect: function(records, autoSelect) {

		if (!records || records.length <= 0) {
			return;
		}

		var me = this,
			len = records.length - 1;

		if (len < 0) return;

		if (autoSelect === false) return;

		var type = Ext.typeOf(autoSelect),
			num = autoSelect === true ? 0 : -1;

		if (type === 'string') { //需要选中的行主键
			num = me.store.find(me.PKField, autoSelect);
		}

		if (num > len) num = len - 1;

		//选中行号为num的数据行
		if (num >= 0) {
			me.getSelectionModel().select(num);
		}
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];

		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;

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

		//		//排序
		//		if (me.defaultOrderBy.length > 0) {
		//	        var len = me.defaultOrderBy.length;
		//              orderby = [];
		//
		//          for (var i = 0; i < len;i++){
		//              orderby.push(me.defaultOrderBy[i].field + " " + me.defaultOrderBy[i].order);
		//	        }
		//	        where += ' ORDER BY ' + orderby.join(",");
		//	    }

		if (where) {
			url += '&where=' + JShell.String.encode(where);
		}

		return url;
	},
	/**启用所有的操作功能*/
	enableControl: function(bo) {
		var me = this,
			enable = bo === false ? false : true,
			toolbars = me.dockedItems.items || [],
			length = toolbars.length,
			items = [];

		for (var i = 0; i < length; i++) {
			if (toolbars[i].xtype == 'header') continue;
			if (toolbars[i].isLocked) continue;
			var fields = toolbars[i].items.items;
			items = items.concat(fields);
		}

		var iLength = items.length;
		for (var i = 0; i < iLength; i++) {
			if(!items[i].isLocked){
				items[i][enable ? 'enable' : 'disable']();
			}
		}
		if (bo) {
			me.defaultLoad = true;
		}
	},
	/**禁用所有的操作功能*/
	disableControl: function() {
		this.enableControl(false);
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		this.load(null, true, autoSelect);
	},
	/**@public 根据where条件加载数据*/
	load: function(where, isPrivate, autoSelect) {
		var me = this,
			collapsed = me.getCollapsed();

		me.defaultLoad = true;
		me.externalWhere = isPrivate ? me.externalWhere : where;

		//收缩的面板不加载数据,展开时再加载，避免加载无效数据
		if (collapsed) {
			me.isCollapsed = true;
			return;
		}

		me.store.currentPage = 1;
		me.store.load();
	},
	/**清空数据,禁用功能按钮*/
	clearData: function() {
		var me = this;
		me.disableControl(); //禁用 所有的操作功能
		me.store.removeAll(); //清空数据
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if (me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
		me.disableControl(); //禁用所有的操作功能
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if (me.hasLoadMask) {
			me.body.unmask();
		} //隐藏遮罩层
		me.enableControl(); //启用所有的操作功能
	},

	/**刷新按钮点击处理方法*/
	onRefreshClick: function() {
		this.onSearch();
	},
	/**删除按钮点击处理方法*/
	onDelClick: function() {
		var me = this,
			records = me.getSelectionModel().getSelection();

		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}

		JShell.Msg.del(function(but) {
			if (but != "ok") return;

			me.delErrorCount = 0;
			me.delCount = 0;
			me.delLength = records.length;

			me.showMask(me.delText); //显示遮罩层
			for (var i in records) {
				var id = records[i].get(me.PKField);
				me.delOneById(i, id);
			}
		});
	},
	/**删除一条数据*/
	delOneById: function(index, id) {
		var me = this;
		var url = (me.delUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.delUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'id=' + id;

		setTimeout(function() {
			JShell.Server.get(url, function(data) {
				var record = me.store.findRecord(me.PKField, id);
				if (data.success) {
					if (record) {
						record.set(me.DelField, true);
						record.commit();
					}
					me.delCount++;
				} else {
					me.delErrorCount++;
					if (record) {
						record.set(me.DelField, false);
						record.set('ErrorInfo', data.msg);
						record.commit();
					}
				}
				if (me.delCount + me.delErrorCount == me.delLength) {
					me.hideMask(); //隐藏遮罩层
					if (me.delErrorCount == 0){
						me.onSearch();
					}else{
						JShell.Msg.error('存在失败信息，具体错误内容请查看数据行的失败提示！');
					}
				}
			});
		}, 100 * index);
	},

	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		JShell.Msg.overwrite('onAddClick');
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick: function() {
		JShell.Msg.overwrite('onEditClick');
	},
	/**@overwrite 查看按钮点击处理方法*/
	onShowClick: function() {
		JShell.Msg.overwrite('onShowClick');
	},
	/**@overwrite 保存按钮点击处理方法*/
	onSaveClick: function() {
		JShell.Msg.overwrite('onSaveClick');
	},
	/**@overwrite 查询按钮点击处理方法*/
	onSearchClick: function(but, value) {
		var me = this;
		//查询栏为空时直接查询
		if (!value) {
			me.internalWhere = "";
			me.onSearch();
			return;
		}

		me.internalWhere = me.getSearchWhere(value);

		me.onSearch();
	},
	/**获取查询框内容*/
	getSearchWhere: function(value) {
		var me = this;
		//查询栏不为空时先处理内部条件再查询
		var searchInfo = me.searchInfo,
			isLike = searchInfo.isLike,
			fields = searchInfo.fields || [],
			len = fields.length,
			where = [];

		for (var i = 0; i < len; i++) {
			if (isLike) {
				where.push(fields[i] + " like '%" + value + "%'");
			} else {
				where.push(fields[i] + "='" + value + "'");
			}
		}
		return where.join(' or ');
	}
});