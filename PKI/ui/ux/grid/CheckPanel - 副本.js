/**
 * 选择列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.ux.grid.CheckPanel', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.selection.CheckboxModel'],

	title: '选择列表',
	width: 270,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '',
	/**默认加载*/
	defaultLoad: true,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**默认选中*/
	autoSelect: false,
	/**是否单选*/
	checkOne: true,
	/**是否带确认按钮*/
	hasAcceptButton: true,

	checkCounts: 0,
	isDeselectAll: false,
	oldCheckRecords: [],
	/**查询框信息*/
	searchInfo: {
		emptyText: '',
		isLike: true,
		fields: []
	},

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.getView().headerCt.on({
			headerclick: function(headerCt, header, e) {
				if (header.isCheckerHd) {
					var isChecked = header.el.hasCls(Ext.baseCSSPrefix + 'grid-hd-checker-on');
					if (isChecked) {
						me.isDeselectAll=false;
						me.checkCounts = 0;
						me.oldCheckRecords = me.getSelectionModel().getSelection();
					} else {
						//全不选择
						me.isDeselectAll = true;
						me.checkCounts = 1;
						me.oldCheckRecords =[];
					}
				}
			}

		});
		//单选双击触发确认事件
		if (me.checkOne) {
			me.on({
				itemdblclick: function(view, record) {
					me.fireEvent('accept', me, record);
				}
			});
		}
	},
	initComponent: function() {
		var me = this;
		me.addEvents('accept');

		if (me.checkOne) {
			if (!me.searchInfo.width) me.searchInfo.width = 145;
			//自定义按钮功能栏
			me.buttonToolbarItems = [{
				text: '清除',
				iconCls: 'button-cancel',
				tooltip: '<b>清除原先的选择</b>',
				handler: function() {
					me.fireEvent('accept', me, null);
				}
			}, '->', {
				type: 'search',
				info: me.searchInfo
			}];
			if (me.hasAcceptButton) me.buttonToolbarItems.push('accept');
		} else {
			if (!me.searchInfo.width) me.searchInfo.width = 205;
			//自定义按钮功能栏
			me.buttonToolbarItems = [{
				type: 'search',
				info: me.searchInfo
			}];
			if (me.hasAcceptButton) me.buttonToolbarItems.push('->', 'accept');
			//复选框
			me.multiSelect = true;

			me.selModel = Ext.create('Ext.selection.CheckboxModel', {
				checkOnly: false,
				listeners: {
					deselect: function(model, record, index) { //取消选中时产生的事件
						if (me.checkCounts == 0 && me.isDeselectAll == false) {
							me.checkCounts = me.checkCounts + 1;
							me.oldCheckRecords = me.getSelectionModel().getSelection();
							//alert(me.oldCheckRecords.length);
						}
					},
					selectionchange: function(model, selected) { //选择有改变时产生的事件
						me.isDeselectAll=true;
						me.checkCounts = 0;
					}

				}
			});

		}

		me.callParent(arguments);
	},
	/**确定按钮处理*/
	onAcceptClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();

		if (me.checkOne) {
			if (records.length != 1) {
				JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
				return;
			}
			me.fireEvent('accept', me, records[0]);
		} else {
			if (records.length == 0) {
				JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
				return;
			}
			me.fireEvent('accept', me, records);
		}
	}
});