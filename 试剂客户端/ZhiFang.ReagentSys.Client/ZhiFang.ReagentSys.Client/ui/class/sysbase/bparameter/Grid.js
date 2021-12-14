/**
 * 系统参数列表
 * @author longfc
 * @version 2016-10-11
 */
Ext.define('Shell.class.sysbase.bparameter.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '系统运行参数',
	width: 480,

	/**默认加载*/
	defaultLoad: true,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: true,

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否启用查询框*/
	hasSearch: true,

	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchBParameterByHQL?isPlanish=true',
	editUrl: "/SingleTableService.svc/ST_UDTO_UpdateBParameterByField",

	SYSParaNoList: [],
	/**申请单状态枚举*/
	SYSParaNoEnum: {},
	/**申请单状态背景颜色枚举*/
	SYSParaNoBGColorEnum: {},
	SYSParaNoFColorEnum: {},
	SYSParaNoBGColorEnum: {},
	/**排序字段*/
	defaultOrderBy: [{
		property: 'BParameter_DispOrder',
		direction: 'ASC'
	}],
	/**列表分组*/
	features: [Ext.create("Ext.grid.feature.Grouping", {
		groupByText: "按当前列进行分组",
		showGroupsText: "显示分组",
		groupHeaderTpl: "{name}",
		startCollapsed: true, //是否收缩
		hideGroupedColumn: true,
		enableGroupingMenu: false,
	})],
	/**列表分组是否展开全部*/
	isExpandAll: false,

	initComponent: function() {
		var me = this;
		
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		me.getSYSParaNoData();
		me.addEvents('onAddClick', me);
		me.addEvents('onEditClick', me);
		me.addEvents('onShowClick', me);

		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建功能 按钮栏*/
	createButtontoolbar: function() {
		var me = this;
		var items = me.createFullscreenItems();

		if(me.hasRefresh) items.push('refresh');
		if(me.hasAdd) items.push('add');
		if(me.hasEdit) items.push('edit');
		if(me.hasDel) items.push('del');
		if(me.hasShow) items.push('show');
		if(me.hasSave) items.push('save');
		//查询框信息
		me.searchInfo = {
			width: 245,
			emptyText: '分组/参数名称/参数编码',
			isLike: true,
			fields: ['bparameter.Name', 'bparameter.SName', 'bparameter.ParaNo']
		};
		items.push('->', {
			type: 'search',
			info: me.searchInfo
		});
		//items.push('-', 'save');
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	/**创建功能 按钮栏*/
	createFullscreenItems: function() {
		var me = this;
		var items = [];
		//全部展开、全部收缩
		items.push({
			text: '全部展开',
			itemId: 'btnExpandAll',
			iconCls: 'button-arrow-out',
			hidden: me.isExpandAll,
			handler: function() {
				me.onExpandAll();
			}
		}, {
			text: '全部收缩',
			itemId: 'btnShrinkAll',
			iconCls: 'button-arrow-in',
			hidden: !me.isExpandAll,
			handler: function() {
				me.onShrinkAll();
			}
		}, '-');
		return items;
	},
	/**全部展开*/
	onExpandAll: function() {
		var me = this,
			toolbar = me.getComponent('buttonsToolbar'),
			btnShrinkAll = toolbar.getComponent('btnShrinkAll'),
			btnExpandAll = toolbar.getComponent('btnExpandAll');
		btnExpandAll.hide();
		btnShrinkAll.show();
		me.features[0].expandAll();
	},
	/**全部收缩*/
	onShrinkAll: function() {
		var me = this,
			toolbar = me.getComponent('buttonsToolbar'),
			btnShrinkAll = toolbar.getComponent('btnShrinkAll'),
			btnExpandAll = toolbar.getComponent('btnExpandAll');

		btnExpandAll.show();
		btnShrinkAll.hide();
		me.features[0].collapseAll();
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '参数类型',
			dataIndex: 'BParameter_ParaType',
			width: 80,
			hidden: true,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'BParameter_SName',
			text: '<b style="color:blue;">参数分组</b>',
			width: 70,
			defaultRenderer: true
		}, {
			text: '参数编码',
			dataIndex: 'BParameter_ParaNo',
			width: 160,
			sortable: false,
			hidden: true,
			menuDisabled: true,
			renderer: function(value, meta) {
				var v = value;
				if(me.SYSParaNoEnum != null)
					v = me.SYSParaNoEnum[value];
				var bColor = "";
				if(me.SYSParaNoBGColorEnum != null)
					bColor = me.SYSParaNoBGColorEnum[value];
				var fColor = "";
				if(me.SYSParaNoFColorEnum != null)
					fColor = me.SYSParaNoFColorEnum[value];
				var style = 'font-weight:bold;';
				if(bColor) {
					style = style + "background-color:" + bColor + ";";
				}
				if(fColor) {
					style = style + "color:" + fColor + ";";
				}
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = style;
				return v;
			}
		}, {
			text: '参数名称',
			dataIndex: 'BParameter_Name',
			width: 160,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			dataIndex: 'BParameter_Shortcode',
			text: '<b style="color:blue;">快捷码</b>',
			width: 70,
			editor: {
				allowBlank: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'BParameter_DispOrder',
			text: '<b style="color:blue;">显示次序</b>',
			width: 75,
			editor: {
				xtype: 'numberfield',
				minValue: 0
			},
			defaultRenderer: true
		}, {
			dataIndex: 'BParameter_ParaValue',
			text: '<b style="color:blue;">参数值</b>',
			width: 160,
			editor: {
				allowBlank: false
			},
			defaultRenderer: true
		}, {
			xtype: 'checkcolumn',
			dataIndex: 'BParameter_IsUse',
			text: '<b style="color:blue;">使用</b>',
			width: 45,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		}, {
			text: '说明',
			dataIndex: 'BParameter_ParaDesc',
			width: 180,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '主键ID',
			dataIndex: 'BParameter_Id',
			isKey: true,
			hidden: true
		}, {
			xtype: 'actioncolumn',
			text: '<b style="color:blue;">提示</b>',
			align: 'center',
			width: 40,
			hidden: true,
			hideable: false,
			sortable: false,
			items: [{
				getClass: function(v, meta, record) {
					var ParaType = record.get("BParameter_ParaType");
					if(ParaType == "RunSYS") {
						meta.tdAttr = 'data-qtip="<b>设置</b>"';
						return 'button-edit hand';
					} else {
						return 'button-edit hand';
					}
				},
				handler: function(grid, rowIndex, colIndex) {

				}
			}]
		}]
		columns.push({
			dataIndex: me.DelField,
			text: '',
			width: 40,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			renderer: function(value, meta, record) {
				var v = '';
				if(value === 'true') {
					v = '<b style="color:green">' + JShell.All.SUCCESS_TEXT + '</b>';
				}
				if(value === 'false') {
					v = '<b style="color:red">' + JShell.All.FAILURE_TEXT + '</b>';
				}
				var msg = record.get('ErrorInfo');
				if(msg) {
					meta.tdAttr = 'data-qtip="<b style=\'color:red\'>' + msg + '</b>"';
				}

				return v;
			}
		}, {
			dataIndex: 'ErrorInfo',
			text: '错误信息',
			hidden: true,
			hideable: false,
			sortable: false,
			menuDisabled: true
		});
		return columns;
	},
	/**创建数据集*/
	createStore: function() {
		var me = this;
		return Ext.create('Ext.data.Store', {
			fields: me.getStoreFields(),
			pageSize: me.defaultPageSize,
			remoteSort: me.remoteSort,
			sorters: me.defaultOrderBy,
			groupField: 'BParameter_SName',
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
					if(data.success) {
						var info = data.value;
						if(info) {
							var type = Ext.typeOf(info);
							if(type == 'object') {
								info = info;
							} else if(type == 'array') {
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
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		var me = this;
		me.fireEvent('onAddClick', this);
	},
	onEditClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.fireEvent('onEditClick', me, records[0]);
	},
	/**获取申请总单状态参数*/
	getParams: function() {
		var me = this,
			params = {};
		params = {
			"jsonpara": [{
				"classname": "SYSParaNo",
				"classnamespace": "ZhiFang.Entity.ReagentSys.Client"
			}]
		};
		return params;
	},
	/**获取借款状态信息*/
	getSYSParaNoData: function(callback) {
		var me = this;
		if(me.SYSParaNoList.length > 0) return;
		var params = {},
			url = JShell.System.Path.getRootUrl(JcallShell.System.ClassDict._classDicListUrl);
		params = Ext.encode(me.getParams());
		me.SYSParaNoList = [];
		me.SYSParaNoEnum = {};
		me.SYSParaNoFColorEnum = {};
		me.SYSParaNoBGColorEnum = {};
		var tempArr = [];
		JcallShell.Server.post(url, params, function(data) {
			if(data.success) {
				if(data.value) {
					if(data.value[0].SYSParaNo.length > 0) {
						//me.SYSParaNoList.push(["", '全部', 'font-weight:bold;color:black;text-align:center;']);
						Ext.Array.each(data.value[0].SYSParaNo, function(obj, index) {
							var style = ['font-weight:bold;text-align:center;'];
							if(obj.FontColor) {
								me.SYSParaNoFColorEnum[obj.Id] = obj.FontColor;
							}
							if(obj.BGColor) {
								style.push('color:' + obj.BGColor); //background-
								me.SYSParaNoBGColorEnum[obj.Id] = obj.BGColor;
							}
							me.SYSParaNoEnum[obj.Id] = obj.Name;
							tempArr = [obj.Id, obj.Name, style.join(';')];
							me.SYSParaNoList.push(tempArr);
						});
					}
				}
			}
		}, false);
	},
	onSaveClick: function() {
		var me = this,
			records = me.store.getModifiedRecords(), //获取修改过的行记录
			len = records.length;

		if(len == 0) return;

		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		for(var i = 0; i < len; i++) {
			me.updateOneInfo(i, records[i]);
		}
	},
	/**修改单个*/
	updateOneInfo: function(index, record) {
		var me = this,
			url = JShell.System.Path.getRootUrl(me.editUrl);
		var id = record.get(me.PKField);
		var IsUse = record.get('BParameter_IsUse');
		if(IsUse == false || IsUse == "false") IsUse = 0;
		if(IsUse == "1" || IsUse == "true" || IsUse == true) IsUse = 1;

		var DispOrder = record.get('BParameter_DispOrder');
		if(!DispOrder) DispOrder = 0;

		var entity = {
			'Id': id,
			'DispOrder': DispOrder,
			'SName': record.get('BParameter_SName'),
			'Shortcode': record.get('BParameter_Shortcode'),
			'ParaValue': record.get('BParameter_ParaValue')
		};
		var params = JShell.JSON.encode({
			entity: entity,
			fields: 'Id,SName, Shortcode,DispOrder,ParaValue'
		});

		setTimeout(function() {
			JShell.Server.post(url, params, function(data) {
				if(data.success) {
					if(record) {
						record.set(me.DelField, true);
						record.commit();
					}
					me.saveCount++;
				} else {
					me.saveErrorCount++;
					if(record) {
						record.set(me.DelField, false);
						record.set('ErrorInfo', data.msg);
						record.commit();
					}
				}
				if(me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					if(me.saveErrorCount == 0) {
						me.onSearch();
					} else {
						JShell.Msg.error(me.saveErrorCount + '条数据发生错误!');
					}
				}
			});
		}, 100 * index);
	}
});