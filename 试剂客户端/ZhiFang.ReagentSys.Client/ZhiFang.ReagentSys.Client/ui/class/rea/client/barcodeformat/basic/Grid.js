/**
 * 条码规则
 * @author longfc
 * @version 2018-01-10
 */
Ext.define('Shell.class.rea.client.barcodeformat.basic.Grid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	
	title: '条码规则信息',
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
	hasAdd: false,
	hasEdit: false,
	hasSave: false,
	hasDel: false,
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
	/**应用类型:是否平台:是:1,否:0或null*/
	APPTYPE: "1",
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//只有在平台上才能维护条码信息
		if(me.APPTYPE == "1") {
			me.hasAdd = true;
			me.hasSave = true;
			me.hasDel = true;
			me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
				clicksToEdit: 1
			});
		}

		//查询框信息
		me.searchInfo = {
			width: 200,
			emptyText: '规则名称/规则前缀',
			isLike: true,
			itemId: 'Search',
			fields: ['reacenbarcodeformat.CName', 'reacenbarcodeformat.SName']
		};
		if(!me.checkOne) me.setCheckboxModel();
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	setCheckboxModel: function() {
		var me = this;
		//复选框
		me.multiSelect = true;
		me.selType = 'checkboxmodel';
	},
	initbuttonToolbarItems: function() {
		var me = this;
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this;
		me.initbuttonToolbarItems();
		var items = me.buttonToolbarItems || [];
		if(me.hasRefresh) items.push('refresh');
		if(me.hasAdd) items.push('add');
		if(me.hasEdit) items.push('edit');
		if(me.hasDel) items.push('del');
		if(me.hasShow) items.push('show');
		if(me.hasSave) items.push('save');
		if(items.length > 0) items.push('-');
		items.push({
			xtype: 'checkboxfield',
			boxLabel: '显示禁用',
			checked: false,
			inputValue: 0,
			name: 'cboIsUse',
			itemId: 'cboIsUse',
			listeners: {
				change: function(field, newValue, oldValue, e) {
					if(newValue != oldValue)
						me.onSearch();
				}
			}
		});
		if(me.hasSearch) items.push('->', {
			type: 'search',
			info: me.searchInfo
		});

		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaCenBarCodeFormat_BarCodeType',
			text: '条码类型',
			width: 60,
			editor: {
				xtype: 'uxSimpleComboBox',
				value: "2",
				hasStyle: true,
				data: [
					["1", '一维码', 'color:green;'],
					["2", '二维码', 'color:orange;']
				]
			},
			renderer: function(value, meta) {
				var v = "";
				if(value == "1") {
					v = "一维码";
					meta.style = "color:green;";
				} else if(value == "2") {
					v = "二维码";
					meta.style = "color:orange;";
				}

				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaCenBarCodeFormat_CName',
			text: '<b style="color:blue;">规则名称</b>',
			width: 95,
			editor: {
				allowBlank: false
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaCenBarCodeFormat_SName',
			text: '<b style="color:blue;">规则前缀</b>',
			width: 65,
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
			width: 65,
			editor: {
				xtype: 'numberfield',
				minValue: 0,
				allowBlank: false
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
		}, {
			dataIndex: 'ReaCenBarCodeFormat_DispOrder',
			text: '<b style="color:blue;">优先级别</b>',
			width: 75,
			editor: {
				xtype: 'numberfield',
				minValue: 0
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaCenBarCodeFormat_ProdOrgNoIndex',
			IsnotField: true,
			text: '供应商编码位置',
			width: 105,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaCenBarCodeFormat_ProdGoodsNoIndex',
			text: '货品编码位置',
			IsnotField: true,
			width: 95,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaCenBarCodeFormat_LotNoIndex',
			text: '批号位置',
			IsnotField: true,
			width: 70,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaCenBarCodeFormat_InvalidDateIndex',
			text: '效期位置',
			IsnotField: true,
			width: 70,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaCenBarCodeFormat_BarCodeFormatExample',
			text: '样例',
			width: 85,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaCenBarCodeFormat_Id',
			sortable: false,
			text: '主键ID',
			hidden: true,
			isKey: true
		}, {
			dataIndex: 'ReaCenBarCodeFormat_Type',
			text: '条码分类',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaCenBarCodeFormat_RegularExpression',
			text: '条码规则信息',
			hidden: true,
			hideable: false,
			renderer: function(value, meta) {
				return "";
			}
		}, {
			dataIndex: 'ReaCenBarCodeFormat_LabID',
			text: 'LabID',
			hidden: true,
			defaultRenderer: true
		}];
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
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			IsUse = buttonsToolbar.getComponent('cboIsUse'),
			Search = buttonsToolbar.getComponent('Search').getValue(),
			params = [];
		if(Search) {
			params.push('(' + me.getSearchWhere(Search) + ')');
		}
		var isUseValue = 1;
		if(IsUse) {
			isUseValue = IsUse.getValue();
			//显示禁用
			if(isUseValue == true || isUseValue == 1)
				isUseValue = 0;
			else
				isUseValue = 1;
		}
		if(isUseValue == 1)
			params.push('reacenbarcodeformat.IsUse=' + isUseValue);
		me.internalWhere = params.join(' and ');

		return me.callParent(arguments);
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

		var type = record.get('ReaCenBarCodeFormat_Type');
		if(!type)
			type = 2;
		else
			type = 1;

		var dispOrder = record.get('ReaCenBarCodeFormat_DispOrder');
		var splitCount = record.get('ReaCenBarCodeFormat_SplitCount');
		var barCodeType = record.get('ReaCenBarCodeFormat_BarCodeType');
		if(!barCodeType) barCodeType = 2;
		
		if(!dispOrder) dispOrder = 0;
		if(!splitCount) splitCount = 0;
		var entity = {
			'Id': id,
			"BarCodeType": barCodeType,
			"Type": type,
			'IsUse': IsUse,
			'DispOrder': dispOrder,
			'CName': record.get('ReaCenBarCodeFormat_CName'),
			'SName': record.get('ReaCenBarCodeFormat_SName'),
			'ShortCode': record.get('ReaCenBarCodeFormat_ShortCode'),
			'SplitCount': splitCount
		};
		var params = JShell.JSON.encode({
			entity: entity,
			fields: 'Id,Type, BarCodeType,CName, SName, ShortCode, IsUse,DispOrder,SplitCount'
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
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		var len = data.list.length;
		for(var i = 0; i < len; i++) {
			//data.list[i].ReaCenBarCodeFormat_IsUse = data.list[i].ReaCenBarCodeFormat_IsUse == '1' ? true : false;
			var jsonInfo = me.getBarCodeInfoJSON();
			if(data.list[i].ReaCenBarCodeFormat_RegularExpression) {
				jsonInfo = JShell.JSON.decode(data.list[i].ReaCenBarCodeFormat_RegularExpression);
				if(!jsonInfo) jsonInfo = me.getBarCodeInfoJSON();
			}
			data.list[i].ReaCenBarCodeFormat_ProdOrgNoIndex = jsonInfo.ProdOrgNo.Index;
			data.list[i].ReaCenBarCodeFormat_ProdOrgNoStart = jsonInfo.ProdOrgNo.StartIndex;
			data.list[i].ReaCenBarCodeFormat_ProdOrgNoLength = jsonInfo.ProdOrgNo.Length;

			data.list[i].ReaCenBarCodeFormat_LotNoIndex = jsonInfo.LotNo.Index;
			data.list[i].ReaCenBarCodeFormat_LotNoStart = jsonInfo.LotNo.StartIndex;
			data.list[i].ReaCenBarCodeFormat_LotNoLength = jsonInfo.LotNo.Length;

			data.list[i].ReaCenBarCodeFormat_ProdGoodsNoIndex = jsonInfo.ProdGoodsNo.Index;
			data.list[i].ReaCenBarCodeFormat_ProdGoodsNoStart = jsonInfo.ProdGoodsNo.StartIndex;
			data.list[i].ReaCenBarCodeFormat_ProdGoodsNoLength = jsonInfo.ProdGoodsNo.Length;

			data.list[i].ReaCenBarCodeFormat_InvalidDateIndex = jsonInfo.InvalidDate.Index;
			data.list[i].ReaCenBarCodeFormat_InvalidDateStart = jsonInfo.InvalidDate.StartIndex;
			data.list[i].ReaCenBarCodeFormat_InvalidDateLength = jsonInfo.InvalidDate.Length;

			var UnitIndex = -1,
				UnitStart = -1,
				UnitLength = -1;
			if(jsonInfo.Unit) {
				UnitIndex = jsonInfo.Unit.Index;
				UnitStart = jsonInfo.Unit.StartIndex;
				UnitLength = jsonInfo.Unit.Length;
			}
			data.list[i].ReaCenBarCodeFormat_UnitIndex = UnitIndex;
			data.list[i].ReaCenBarCodeFormat_UnitStart = UnitStart;
			data.list[i].ReaCenBarCodeFormat_UnitLength = UnitLength;

		}
		return data;
	},
	getBarCodeInfoJSON: function() {
		var me = this;
		var jsonInfo = {
			"LotNo": { //批号
				"Index": -1, //位置
				"StartIndex": -1, //起始位置
				"Length": -1 //长度
			},
			"InvalidDate": { //效期
				"Index": -1,
				"StartIndex": -1,
				"Length": -1
			},
			"ProdOrgNo": { //厂商机构码
				"Index": -1,
				"StartIndex": -1,
				"Length": -1
			},
			"ProdGoodsNo": { //厂商货品编码
				"Index": -1,
				"StartIndex": -1,
				"Length": -1
			},
			"Unit": { //货品单位
				"Index": -1,
				"StartIndex": -1,
				"Length": -1
			},
			"DtlNo": { //流水号
				"Index": -1,
				"StartIndex": -1,
				"Length": -1
			},
			"GoodsQty": { //明细总数
				"Index": -1,
				"StartIndex": -1,
				"Length": -1
			},
			"CurDispOrder": { //当前序号
				"Index": -1,
				"StartIndex": -1,
				"Length": -1
			}
		};
		return jsonInfo;
	}
});