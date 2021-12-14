/**
 * 记录项类型字典
 * @author longfc
 * @version 2020-02-11
 */
Ext.define('Shell.class.sysbase.screcordtype.Grid', {
	extend: 'Shell.class.assist.basic.GridPanel',

	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.BoolComboBox'
	],
	title: '记录项类型字典列表',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchSCRecordTypeByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdateSCRecordTypeByField',
	/**删除数据服务路径*/
	delUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_DelSCRecordType',

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
	hasDel: false,
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否启用查询框*/
	hasSearch: true,

	/**查询栏参数设置*/
	searchToolbarConfig: {},

	defaultOrderBy: [{
		property: 'SCRecordType_DispOrder',
		direction: 'ASC'
	}],
	/**用户UI配置Key*/
	userUIKey: 'screcordtype.Grid',
	/**用户UI配置Name*/
	userUIName: "过程记录分类列表",
	
	/**是否检验项目对照*/
	ISTESTITEM:true,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;

		//查询框信息
		me.searchInfo = {
			width: 220,
			emptyText: '项目对照码/样本对照码/名称',
			isLike: true,
			fields: ['screcordtype.SampleTypeCode', 'screcordtype.CName', 'screcordtype.TestItemCode']
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
		var columns = [ {
			text: '编码',
			dataIndex: 'SCRecordType_Id',
			isKey: true,
			width: 100,
			hideable: false
		}, {
			dataIndex: 'SCRecordType_ContentTypeID',
			text: '所属类别',
			width: 120,
			renderer: function(value, meta) {
				var v = "";
				if (value == "10000") {
					v = "院感登记";
					meta.style = "color:green;";
				} else if (value == "20000") {
					v = "输血过程登记";
					meta.style = "color:orange;";
				} 
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		},{
			text: '编码',
			dataIndex: 'SCRecordType_TypeCode',
			width: 100,
			//hidden: true,
			menuDisabled: true,
			editor: {},
			defaultRenderer: true
		}, {
			text: '项目对照码',
			dataIndex: 'SCRecordType_TestItemCode',
			width: 100,
			//hidden: true,
			menuDisabled: true,
			editor: {},
			defaultRenderer: true
		},{
			text: '项目名称',
			dataIndex: 'SCRecordType_TestItemCName',
			width: 100,
			menuDisabled: true,
			hidden:me.ISTESTITEM==true?false:true,
			defaultRenderer: true
		},{
			text: '样本对照码',
			dataIndex: 'SCRecordType_SampleTypeCode',
			width: 100,
			//hidden: true,
			menuDisabled: true,
			editor: {},
			defaultRenderer: true
		},{
			text: '样本名称',
			dataIndex: 'SCRecordType_SampleTypeCName',
			width: 100,
			menuDisabled: true,
			hidden:me.ISTESTITEM==true?false:true,
			defaultRenderer: true
		},{
			text: '类型名称',
			dataIndex: 'SCRecordType_CName',
			width: 100,
			menuDisabled: true,
			editor: {},
			renderer: function(value, meta, record) {
				var bgColor = record.get("SCRecordType_BGColor");
				if(bgColor) {
					meta.style = 'background-color:' + bgColor + ';';//color:#ffffff;
				}
				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return value;
			}
		},{
			text: '背景颜色',
			dataIndex: 'SCRecordType_BGColor',
			width: 80,
			hidden:true,
			menuDisabled: true,
			defaultRenderer: true
		},{
			text: '快捷码',
			dataIndex: 'SCRecordType_ShortCode',
			width: 80,
			menuDisabled: true,
			defaultRenderer: true
		},{
			text: '拼音字头',
			dataIndex: 'SCRecordType_PinYinZiTou',
			width: 80,
			menuDisabled: true,
			defaultRenderer: true
		},  {
			xtype: 'checkcolumn',
			text: '使用',
			dataIndex: 'SCRecordType_IsUse',
			width: 40,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		}, {
			text: '创建时间',
			dataIndex: 'SCRecordType_DataAddTime',
			width: 130,
			hidden: true,
			isDate: true,
			hasTime: true
		}, {
			text: '备注',
			dataIndex: 'SCRecordType_Memo',
			width: 200,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '次序',
			dataIndex: 'SCRecordType_DispOrder',
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
		var isUse = rec.get('SCRecordType_IsUse');
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
				TypeCode: rec.get('SCRecordType_TypeCode'),
				TestItemCode: rec.get('SCRecordType_TestItemCode'),
				SampleTypeCode: rec.get('SCRecordType_SampleTypeCode'),
				CName: rec.get('SCRecordType_CName'),
				DispOrder: rec.get('SCRecordType_DispOrder')
			},
			fields: 'Id,IsUse,DispOrder,TypeCode,TestItemCode,SampleTypeCode,CName'
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
			var IsUse = data.list[i].SCRecordType_IsUse;
			if (IsUse == 'True' || IsUse ==
				'true' || IsUse == '1' || IsUse == 1) {
				IsUse = true;
			} else {
				IsUse = false;
			}
			data.list[i].SCRecordType_IsUse = IsUse;
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
			defaultWhere: "scoperation.BusinessModuleCode='BDictType'"
		};
		var win = JShell.Win.open('Shell.class.assist.scoperation.SCOperation', config);
		win.show();
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var url=me.callParent(arguments);
		if(me.ISTESTITEM==true){
			url=url+"&isTestItem=true";
		}
		return url;
	}
});
