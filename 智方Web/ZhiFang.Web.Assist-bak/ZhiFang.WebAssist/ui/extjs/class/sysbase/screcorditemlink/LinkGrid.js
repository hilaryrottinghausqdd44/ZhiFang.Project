/**
 * 记录项类型与记录项字典关系
 * @author longfc
 * @version 2020-02-11
 */
Ext.define('Shell.class.sysbase.screcorditemlink.LinkGrid', {
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
	selectUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchSCRecordItemLinkByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdateSCRecordItemLinkByField',
	/**删除数据服务路径*/
	delUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_DelSCRecordItemLink',
	/**新增服务地址*/
	addUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_AddSCRecordItemLink',

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
		property: 'SCRecordItemLink_DispOrder',
		direction: 'ASC'
	}],
	/**用户UI配置Key*/
	userUIKey: 'screcordtype.Grid',
	/**用户UI配置Name*/
	userUIName: "过程记录分类列表",
	RecordTypeID: "",
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
			emptyText: '编码/名称',
			isLike: true,
			fields: ['screcorditemlink.SCRecordTypeItem.ItemCode', 'screcorditemlink.SCRecordTypeItem.CName']
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
			dataIndex: 'SCRecordItemLink_Id',
			hidden:true,
			isKey: true,
			width: 150,
			hideable: false
		}, {
			dataIndex: 'SCRecordItemLink_SCRecordType_ContentTypeID',
			text: '所属类别',
			width: 80,
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
		}, {
			text: '类型编码',
			dataIndex: 'SCRecordItemLink_SCRecordType_TypeCode',
			width: 110,
			hidden: true,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '记录项类型',
			dataIndex: 'SCRecordItemLink_SCRecordType_CName',
			width: 80,
			menuDisabled: true,
			defaultRenderer: true,
			editor: {}
		}, {
			text: '记录项编码',
			dataIndex: 'SCRecordItemLink_SCRecordTypeItem_Id',
			width: 100,
			hideable: false
		}, {
			text: '对照编码',
			dataIndex: 'SCRecordItemLink_TestItemCode',
			width: 100,
			defaultRenderer: true,
			editor: {}
		},{
			text: '对照项目',
			dataIndex: 'SCRecordTypeItem_TestItemCName',
			width: 100,
			menuDisabled: true,
			hidden:me.ISTESTITEM==true?false:true,
			defaultRenderer: true
		}, {
			text: '记录项名称',
			dataIndex: 'SCRecordItemLink_SCRecordTypeItem_CName',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			xtype: 'checkcolumn',
			text: '开单是否可见',
			dataIndex: 'SCRecordItemLink_IsBillVisible',
			width: 90,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		}, {
			xtype: 'checkcolumn',
			text: '使用',
			dataIndex: 'SCRecordItemLink_IsUse',
			width: 40,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		}, {
			text: '次序',
			dataIndex: 'SCRecordItemLink_DispOrder',
			width: 100,
			defaultRenderer: true,
			align: 'center',
			type: 'int',
			editor: {
				xtype: 'numberfield'
			}
		}];

		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh', '-',
			{
				text: '记录项选择',
				tooltip: '记录项选择',
				iconCls: 'button-add',
				handler: function() {
					me.onAddClick();
				}
			}, 'edit', 'del', 'save', '-'
		];
		items.push('-', {
			type: 'search',
			info: me.searchInfo
		});
		return items;
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		var me = this;
		if (!me.RecordTypeID) {
			JShell.Msg.error('请先选择一个记录项类型后再操作');
			return;
		}
		var defaultWhere = " screcordtypeitem.IsUse=1 ";
		var maxWidth = document.body.clientWidth * 0.98;
		var height = document.body.clientHeight * 0.92;

		var linkWhere = "screcorditemlink.SCRecordType.Id=" + me.RecordTypeID; //关系表查询条件
		JShell.Win.open('Shell.class.sysbase.screcorditemlink.choose.App', {
			resizable: true,
			width: maxWidth,
			height: height,
			linkWhere: linkWhere,
			leftDefaultWhere: defaultWhere,
			defaultWhere: defaultWhere,
			RecordTypeID: me.RecordTypeID,
			listeners: {
				accept: function(p, records) {
					me.onSave(p, records);
				}
			}
		}).show();
	},
	/**保存关系数据*/
	onSave: function(p, records) {
		var me = this;

		if (records.length == 0) return;

		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = records.length;

		for (var i in records) {
			me.onAddOneLink(p, records[i], i);
		}
	},
	onAddOneLink: function(p, record, index) {
		var me = this,
			url = JShell.System.Path.ROOT + me.addUrl;

		var dispOrder = record.get('SCRecordTypeItem_DispOrder');
		if (!dispOrder) dispOrder = 0;
		var testItemCode = record.get('SCRecordTypeItem_ItemCode');

		var params = {
			entity: {
				SCRecordType: {
					Id: me.RecordTypeID,
					DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0]
				},
				SCRecordTypeItem: {
					Id: record.get('SCRecordTypeItem_Id'),
					DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0]
				},
				TestItemCode: testItemCode,
				DispOrder: dispOrder,
				IsUse: 1,
				IsBillVisible:1
			}
		};
		//创建者信息
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);

		params = Ext.JSON.encode(params);
		var url = JShell.System.Path.ROOT + me.addUrl;
		setTimeout(function() {
			//提交数据到后台
			JShell.Server.post(url, params, function(data) {
				if (data.success) {
					me.saveCount++;
				} else {
					me.saveErrorCount++;
				}
				if (me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					p.close();
					//if (me.saveErrorCount == 0) me.onSearch();
					me.onSearch();
				}
			});
		}, 100 * index);
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
			me.updateOne(i, rec);
		}
	},
	updateOne: function(index, rec) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;

		var id = rec.get(me.PKField);
		var isUse = rec.get('SCRecordItemLink_IsUse');
		var empID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var empName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if (!empID) empID = -1;
		if (!empName) empName = "";
		var dispOrder = rec.get('SCRecordItemLink_DispOrder');
		if (!dispOrder) dispOrder = 0;
		var testItemCode = rec.get('SCRecordItemLink_TestItemCode');
		var isBillVisible = rec.get('SCRecordItemLink_IsBillVisible');
		
		var params = Ext.JSON.encode({
			empID: empID,
			empName: empName,
			entity: {
				Id: id,
				IsUse: isUse,
				DispOrder: dispOrder,
				TestItemCode: testItemCode,
				IsBillVisible: isBillVisible
			},
			fields: 'Id,IsUse,DispOrder,TestItemCode,IsBillVisible'
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

	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var len = data.list.length;
		for (var i = 0; i < len; i++) {
			var IsUse = data.list[i].SCRecordItemLink_IsUse;
			if (IsUse == 'True' || IsUse ==
				'true' || IsUse == '1' || IsUse == 1) {
				IsUse = true;
			} else {
				IsUse = false;
			}
			var IsUse = data.list[i].SCRecordItemLink_IsUse;
			data.list[i].SCRecordItemLink_IsUse = IsUse;
		}
		return data;
	},
	/**加载数据后*/
	onAfterLoad: function(records, successful) {
		var me = this;
		me.callParent(arguments);
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;

		me.store.removeAll();
		if (!me.RecordTypeID) {
			var msg = me.msgFormat.replace(/{msg}/, "请选择监测类型后再操作");
			me.getView().update(msg);
			return false;
		} else {
			me.getView().update();
		}

		me.store.proxy.url = me.getLoadUrl(); //查询条件

		me.disableControl(); //禁用 所有的操作功能
		if (!me.defaultLoad) return false;
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
