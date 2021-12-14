/**
 * 记录项短语维护
 * @author longfc
 * @version 2020-02-11
 */
Ext.define('Shell.class.sysbase.screcordphrase.Grid', {
	extend: 'Shell.class.assist.basic.GridPanel',

	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.BoolComboBox'
	],
	title: '记录项短语维护列表',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchSCRecordPhraseByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdateSCRecordPhraseByField',
	/**删除数据服务路径*/
	delUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_DelSCRecordPhrase',

	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,

	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize: 50,

	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: true,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,

	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	//	/**是否启用修改按钮*/
	//	hasEdit:true,
	/**是否启用删除按钮*/
	hasDel: true,
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否启用查询框*/
	hasSearch: true,

	/**查询栏参数设置*/
	searchToolbarConfig: {},

	defaultOrderBy: [{
		property: 'SCRecordPhrase_DispOrder',
		direction: 'ASC'
	}],
	/**用户UI配置Key*/
	userUIKey: 'SCRecordPhrase.Grid',
	/**用户UI配置Name*/
	userUIName: "过程记录分类列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;

		//查询框信息
		me.searchInfo = {
			width: 220,
			emptyText: '快捷码/名称',
			isLike: true,
			fields: ['screcordphrase.ShortCode', 'screcordphrase.CName']
		};
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var me = this;
		var columns = [{
			text: '编码',
			dataIndex: 'SCRecordPhrase_Id',
			isKey: true,
			width: 150,
			hideable: false
		},{
			text: '名称',
			dataIndex: 'SCRecordPhrase_CName',
			width: 140,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true,
			editor: {}
		}, {
			text: '简称',
			dataIndex: 'SCRecordPhrase_SName',
			width: 80,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		},{
			text: '快捷码',
			dataIndex: 'SCRecordPhrase_ShortCode',
			width: 80,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		},{
			text: '拼音字头',
			dataIndex: 'SCRecordPhrase_PinYinZiTou',
			width: 80,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		},{
			xtype: 'checkcolumn',
			text: '使用',
			dataIndex: 'SCRecordPhrase_IsUse',
			width: 40,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		}, {
			text: '创建时间',
			dataIndex: 'SCRecordPhrase_DataAddTime',
			width: 130,
			hidden: true,
			isDate: true,
			hasTime: true
		}, {
			text: '备注',
			dataIndex: 'SCRecordPhrase_Memo',
			width: 200,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '次序',
			dataIndex: 'SCRecordPhrase_DispOrder',
			width: 100,
			defaultRenderer: true,
			align: 'center',
			type: 'int',
			editor: {
				xtype: 'numberfield'
			}
		}, {
			xtype: 'actioncolumn',
			text: '操作',
			align: 'center',
			width: 45,
			hideable: false,
			sortable: false,
			items: [{
				iconCls: 'button-show hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.onShowOperation(rec);
				}
			}]
		}];

		return columns;
	},
	onSaveClick: function() {
		var me = this,
			records = me.store.getModifiedRecords(), //获取修改过的行记录
			len = records.length;

		if (len == 0) return;

		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;

		for (var i = 0; i < len; i++) {
			var rec = records[i];
			me.updateOne(i,rec);
		}
	},
	updateOne: function(index, rec) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
	
		var id = rec.get(me.PKField);
		var isUse = rec.get('SCRecordPhrase_IsUse');
		var empID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var empName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if (!empID) empID = -1;
		if (!empName) empName = "";
		var params = Ext.JSON.encode({
			empID:empID,
			empName:empName,
			entity: {
				Id: id,
				IsUse: isUse,
				CName: rec.get('SCRecordPhrase_CName'),
				DispOrder: rec.get('SCRecordPhrase_DispOrder')
			},
			fields: 'Id,IsUse,DispOrder,CName'
		});

		setTimeout(function() {
			JShell.Server.post(url, params, function(data) {
				var record = me.store.findRecord(me.PKField, id);
				if (data.success) {
					if (record) {
						record.set(me.DelField, true);
						record.commit();
					}
					me.saveCount++;
				} else {
					me.saveErrorCount++;
					if (record) {
						record.set(me.DelField, false);
						record.commit();
					}
				}
				if (me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					if (me.saveErrorCount == 0) me.onSearch();
				}
			});
		}, 100 * index);
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		this.fireEvent('addclick', this);
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var len = data.list.length;
		for (var i = 0; i < len; i++) {
			var IsUse = data.list[i].SCRecordPhrase_IsUse;
			if (IsUse == 'True' || IsUse ==
				'true' || IsUse == '1' || IsUse == 1) {
				IsUse = true;
			} else {
				IsUse = false;
			}
			data.list[i].SCRecordPhrase_IsUse = IsUse;
		}
		return data;
	},
	onShowOperation: function(record) {
		var me = this;
		if (!record) {
			var records = me.ApplyGrid.getSelectionModel().getSelection();
			if (records.length != 1) {
				JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
				return;
			}
			record = records[0];
		}
		var id = record.get(me.PKField);
		var maxWidth = document.body.clientWidth * 0.96;
		var height = document.body.clientHeight * 0.92;
		var config = {
			resizable: true,
			width: maxWidth,
			height: height,
			PK: id,
			classNameSpace: 'ZhiFang.Entity.WebAssist', //类域
			className: 'UpdateOperationType', //类名
			title: '操作记录',
			defaultWhere: "scoperation.BusinessModuleCode='SCRecordPhrase'"
		};
		var win = JShell.Win.open('Shell.class.assist.scoperation.SCOperation', config);
		win.show();
	}
});
