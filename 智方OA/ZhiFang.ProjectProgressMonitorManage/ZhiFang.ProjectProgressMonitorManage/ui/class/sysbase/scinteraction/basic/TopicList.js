/**
 * 互动话题列表
 * @author longfc
 * @version 2017-03-21
 */
Ext.define('Shell.class.sysbase.scinteraction.basic.TopicList', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Ext.ux.RowExpander'
	],

	title: '互动话题 ',
	width: 320,
	height: 500,
	/**默认展开内容*/
	defaultShowContent: true,
	autoSelect: true,
	/**默认勾选查询全部交流*/
	defaultShowInteraction: false,
	/**获取数据服务路径*/
	selectUrl: '/SystemCommonService.svc/SC_UDTO_SearchSCInteractionByHQL?isPlanish=true',
	/**附件对象名*/
	objectName: '',
	/**附件关联对象名*/
	fObejctName: '',
	/**附件关联对象主键*/
	fObjectValue: '',
	/**交流关联对象是否ID,@author Jcall,@version 2016-08-19*/
	fObjectIsID: false,
	/**默认加载*/
	defaultLoad: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用序号列*/
	hasRownumberer: false,

	/**默认每页数量*/
	defaultPageSize: 20,
	/**分页栏下拉框数据*/
	pageSizeList: [
		[10, 10],
		[20, 20],
		[50, 50],
		[100, 100],
		[200, 200]
	],
	constructor: function(config) {
		var me = this;

		me.plugins = [{
			ptype: 'rowexpander',
			rowBodyTpl: [
				'<p><b>内容:</b></p>',
				'<p>{' + config.objectName + '_Contents}</p>'
			]
		}];

		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			load: function() {
				me.changeShowType(me.showType);
			},
			resize: function() {
				var gridWidth = me.getWidth();
				var width = gridWidth - me.columns[0].getWidth() - me.columns[1].getWidth() - me.columns[3].getWidth();
				me.columns[2].setWidth(width - 20);
			}
		});
	},
	initComponent: function() {
		var me = this;
		//me.initDefaultWhere();
		me.columns = me.createGridColumns();
		//查询框信息
		var searchInfo = {
			width: 135,
			itemId: 'search',
			emptyText: '话题',
			isLike: true,
			fields: [me.objectName.toLowerCase() + '.Memo']
		};

		me.buttonToolbarItems = ['refresh', {
			type: 'search',
			info: searchInfo
		}, {
			xtype: 'checkbox',
			boxLabel: '展开内容',
			itemId: 'showContent',
			hidden: true,
			checked: me.defaultShowContent,
			listeners: {
				change: function(field, newValue, oldValue) {
					me.changeShowType(newValue);
				}
			}
		}, {
			xtype: 'checkbox',
			boxLabel: '全部交流',
			itemId: 'showAllInteraction',
			checked: me.defaultShowInteraction,
			listeners: {
				change: function(field, newValue, oldValue) {
					//me.changeShowType(newValue);
				}
			}
		}];

		me.showType = me.defaultShowContent;
		me.defaultOrderBy = [{ property: me.objectName + '_DataAddTime', direction: 'DESC' }];
		me.callParent(arguments);
	},
	initDefaultWhere: function() {
		var me = this;
		me.objectNameLower = me.objectName.toLocaleLowerCase();
		if(me.defaultWhere) {
			me.defaultWhere = "(" + me.defaultWhere + ") and ";
		} else {
			me.defaultWhere = "";
		}
		/**交流关联对象是否ID,@author Jcall,@version 2016-08-19*/
		if(me.fObjectIsID) {
			me.defaultWhere += me.objectNameLower + ".IsCommunication=1 and " + me.objectNameLower + ".IsUse=1 and " + me.objectNameLower + "." + me.fObejctName + "ID=" + me.fObjectValue;
		} else {
			me.defaultWhere += me.objectNameLower + ".IsCommunication=1 and " + me.objectNameLower + ".IsUse=1 and " + me.objectNameLower + "." + me.fObejctName + ".Id=" + me.fObjectValue;
		}

	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '发表时间',
			dataIndex: me.objectName + '_DataAddTime',
			isDate: true,
			hidden: true,
			hasTime: true,
			width: 155
		}, {
			text: '话题',
			flex: 1,
			dataIndex: me.objectName + '_Memo',
			sortable: false,
			menuDisabled: true,
			renderer: function(value, meta, record) {
				var isOwner = record.get(me.objectName + '_SenderID') ==
					JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
				var color = isOwner ? "color:green" : "color:#000";
				var v = '<b style=\'' + color + '\'>' + record.get(me.objectName + '_Memo') + '</b>';
				if(v) {
					meta.tdAttr = 'data-qtip="' + v + '"';
				}
				return v;
			}
		}, {
			xtype: 'rownumberer',
			text: '序号',
			width: 40,
			hidden: true,
			align: 'center'
		}, {
			text: '最新回复时间',
			dataIndex: me.objectName + '_LastReplyDateTime',
			//isDate: true,
			//hasTime: false,
			width: 90,
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				var v = JShell.Date.toString(value, true) || '';
				if(value) {
					var qtip = JShell.Date.toString(value, false) || '';
					meta.tdAttr = 'data-qtip="' + qtip + '"';
				}
				return v;
			}
		}, {
			type: 'number',
			xtype: 'numbercolumn',
			text: '回复数',
			width: 55,
			dataIndex: me.objectName + '_ReplyCount',
			sortable: false,
			menuDisabled: true,
			align: 'center',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0' : "0");
				return value;
			}
		}, {
			text: '主键ID',
			isKey: true,
			hidden: true,
			hideable: false,
			dataIndex: me.objectName + '_Id'
		}, {
			text: '发表人ID',
			dataIndex: me.objectName + '_SenderID',
			notShow: true
		}, {
			text: '话题BobjectID',
			dataIndex: me.objectName + '_BobjectID',
			notShow: true
		}, {
			text: '内容',
			dataIndex: me.objectName + '_Contents',
			notShow: true
		}];

		return columns;
	},
	changeShowType: function(value) {
		var me = this;
		me.showType = value ? true : false;
		me.toggleRow(me.showType);
	},
	toggleRow: function(bo) {
		var me = this,
			plugins = me.plugins[0],
			view = plugins.view,
			records = me.store.data,
			len = records.length;

		for(var i = 0; i < len; i++) {
			var rowNode = view.getNode(i),
				row = Ext.get(rowNode),
				nextBd = Ext.get(row).down(plugins.rowBodyTrSelector),
				record = view.getRecord(rowNode);
			if(bo) {
				row.removeCls(plugins.rowCollapsedCls);
				nextBd.removeCls(plugins.rowBodyHiddenCls);
				plugins.recordsExpanded[record.internalId] = true;
			} else {
				row.addCls(plugins.rowCollapsedCls);
				nextBd.addCls(plugins.rowBodyHiddenCls);
				plugins.recordsExpanded[record.internalId] = false;
			}
		}
		view.refreshSize();
		if(bo) {
			view.fireEvent('expandbody', rowNode, record, nextBd.dom);
		} else {
			view.fireEvent('collapsebody', rowNode, record, nextBd.dom);
		}
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			search = buttonsToolbar.getComponent('search').getValue();
		params = [];
		me.defaultWhere = "";
		me.initDefaultWhere();
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		if(search) {
			if(me.internalWhere) {
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			} else {
				me.internalWhere = me.getSearchWhere(search);
			}
		}
		return me.callParent(arguments);
	},
	/**返回数据处理方法*/
	changeResult: function(data) {

		return data;
	}
});