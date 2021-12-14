/**
 * 供货方条码规则维护
 * @author longfc
 * @version 2018-01-10
 */
Ext.define('Shell.class.rea.client.barcodeformat.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '条码规则',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaCenBarCodeFormatByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaCenBarCodeFormat',
	/**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaCenBarCodeFormat',
	/**修改服务*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaCenBarCodeFormatByField',
	/**是否多选行*/
	checkOne: true,
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,
	hasAdd: true,
	hasEdit: true,
	hasSave: true,
	/**默认加载数据*/
	defaultLoad: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**当前的供货方平台机构编码*/
	PlatformOrgNo: null,

	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaCenBarCodeFormat_DispOrder',
		direction: 'ASC'
	}],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		//查询框信息
		me.searchInfo = {
			width: 200,
			emptyText: '规则名称',
			isLike: true,
			itemId: 'Search',
			fields: ['reacenbarcodeformat.CName']
		};
		if(!me.checkOne) me.setCheckboxModel();
		//数据列
		me.columns = me.createGridColumns();
		//创建数据集
		me.store = me.createStore();
		me.callParent(arguments);
	},
	setCheckboxModel: function() {
		var me = this;
		//复选框
		me.multiSelect = true;
		me.selType = 'checkboxmodel';
	},
	createGridColumns: function() {
		var me = this;
		var column = [{
			dataIndex: 'ReaCenBarCodeFormat_CName',
			text: '<b style="color:blue;">规则名称</b>',
			width: 90,
			editor: {
				allowBlank: false
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaCenBarCodeFormat_SName',
			text: '<b style="color:blue;">规则前缀</b>',
			width: 70,
			editor: {
				allowBlank: false
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaCenBarCodeFormat_ShortCode',
			text: '<b style="color:blue;">分割符</b>',
			width: 50,
			editor: {
				allowBlank: false
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaCenBarCodeFormat_SplitCount',
			text: '<b style="color:blue;">分隔符数</b>',
			width: 70,
			editor: {
				xtype: 'numberfield',
				minValue: 0,
				allowBlank: false
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaCenBarCodeFormat_DispOrder',
			text: '<b style="color:blue;">显示次序</b>',
			width: 75,
			editor: {
				xtype: 'numberfield',
				minValue: 0
			},
			defaultRenderer: true
		}, {
			xtype: 'checkcolumn',
			dataIndex: 'ReaCenBarCodeFormat_IsUse',
			text: '<b style="color:blue;">使用</b>',
			width: 40,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		},  {
			dataIndex: 'ReaCenBarCodeFormat_Memo',
			text: '<b style="color:blue;">备注</b>',
			width: 75,
			editor: {
				xtype: 'textarea',
				height: 60
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaCenBarCodeFormat_BarCodeFormatExample',
			text: '样例',
			width: 315,
			//			editor: {
			//				xtype: 'textarea',
			//				height: 60
			//			},
			defaultRenderer: true
		},{
			dataIndex: 'ReaCenBarCodeFormat_Id',
			sortable: false,
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];
		return column;
	},
	onAddClick: function() {
		var me = this;
		me.showFromPanel();
	},
	onEditClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		me.showFromPanel(records[0]);
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
		var IsUse = record.get('ReaCenBarCodeFormat_IsUse');
		if(IsUse == false || IsUse == "false") IsUse = 0;
		if(IsUse == "1" || IsUse == "true" || IsUse == true) IsUse = 1;
		var DispOrder = record.get('ReaCenBarCodeFormat_DispOrder');
		if(!DispOrder) DispOrder = 0;

		var barCodeFormatExample = record.get('ReaCenBarCodeFormat_BarCodeFormatExample').replace(/\\/g, '&#92');
		barCodeFormatExample = barCodeFormatExample.replace(/[\r\n]/g, '');

		var memo = record.get('ReaCenBarCodeFormat_Memo').replace(/\\/g, '&#92');
		memo = memo.replace(/[\r\n]/g, '');

		var entity = {
			'Id': id,
			'IsUse': IsUse,
			'DispOrder': DispOrder,
			'CName': record.get('ReaCenBarCodeFormat_CName'),
			'SName': record.get('ReaCenBarCodeFormat_SName'),
			'ShortCode': record.get('ReaCenBarCodeFormat_ShortCode'),
			'SplitCount': record.get('ReaCenBarCodeFormat_SplitCount'),
			'BarCodeFormatExample': barCodeFormatExample,
			'Memo': memo
		};
		var params = JShell.JSON.encode({
			entity: entity,
			fields: 'Id, CName, SName, ShortCode, IsUse, BarCodeFormatExample, Memo'
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
	},
	/**删除一条数据*/
	delOneById: function(record, index, id) {
		var me = this;
		var url = (me.delUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.delUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'id=' + id;
		setTimeout(function() {
			JShell.Server.get(url, function(data) {
				if(data.success) {
					me.store.remove(record);
					me.delCount++;
				} else {
					me.delErrorCount++;
				}
				if(me.delCount + me.delErrorCount == me.delLength) {
					me.hideMask(); //隐藏遮罩层
					if(me.delErrorCount == 0) {} else {
						JShell.Msg.error('存在失败信息，具体错误内容请查看数据行的失败提示！');
					}
				}
			});
		}, 100 * index);
	},
	/**@description 弹出验收录入信息*/
	showFromPanel: function(record) {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.99;
		var height = document.body.clientHeight * 0.98;
		var id = null;
		if(record) id = record.get(me.PKField);

		var config = {
			resizable: true,
			PK: null,
			PlatformOrgNo:me.PlatformOrgNo,
			SUB_WIN_NO: '1',
			width: 460,
			height: 320,
			listeners: {
				save: function(p, pk) {
					p.close();
					me.onSearch();
				}
			}
		};
		if(!id)
			config.formtype = 'add';
		else {
			config.formtype = 'edit';
			config.PK = id;		
		}
		var win = JShell.Win.open('Shell.class.rea.client.barcodeformat.Form', config);
		win.show();
	}
});