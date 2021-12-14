/**
 * 检验项目信息维护
 * @author liangyl
 * @version 2017-09-08
 */
Ext.define('Shell.class.rea.client.testitem.item.Grid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.BoolComboBox'
	],
	title: '检验项目信息维护',
	width: 800,
	height: 500,
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaTestItemByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaTestItem',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaTestItemByField',
	/**下载Excel文件*/
	downLoadExcelUrl: '/ReaManageService.svc/RS_UDTO_DownLoadExcel',
	/**导出Excel文件*/
	reportExcelUrl: '/ReaSysManageService.svc/ST_UDTO_GetTestItemReportExcelPath',

	/**默认加载数据*/
	defaultLoad: true,
	/**用户UI配置Key*/
	userUIKey: 'testitem.item.Grid',
	/**用户UI配置Name*/
	userUIName: "检验项目信息列表",

	initComponent: function() {
		var me = this;
		me.addEvents('addclick', 'editclick');

		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1,
			pluginId: 'NewsGridEditing'
		});
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaTestItem_CName',
			text: '项目名称',
			sortable: true,
			width: 160,
			editor: {},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestItem_EName',
			text: '英文名称',
			sortable: true,
			width: 100,
			editor: {},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestItem_SName',
			text: '简称',
			sortable: true,
			width: 100,
			editor: {},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestItem_Price',
			text: '价格',
			editor: {
				xtype: 'numberfield'
			},
			sortable: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestItem_LisCode',
			text: 'Lis编码',
			sortable: true,
			width: 80,
			editor: {},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestItem_ShortCode',
			text: '代码',
			sortable: true,
			width: 80,
			editor: {},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestItem_Visible',
			text: '是否使用',
			width: 70,
			align: 'center',
			type: 'bool',
			isBool: true,
			editor: {
				xtype: 'uxBoolComboBox',
				value: true,
				hasStyle: true
			}
		}, {
			dataIndex: 'ReaTestItem_DispOrder',
			text: '显示次序',
			type: 'int',
			editor: {
				xtype: 'numberfield'
			},
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestItem_ZX1',
			text: 'ZX1',
			width: 100,
			editor: {},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestItem_ZX2',
			editor: {},
			text: 'ZX2',
			sortable: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestItem_ZX3',
			editor: {},
			text: 'ZX3',
			sortable: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaTestItem_Memo',
			editor: {},
			text: '备注',
			sortable: true,
			width: 150,
			renderer: function(value, meta, record) {
				var v = me.showMemoText(value, meta);
				return v;
			}
		}, {
			dataIndex: 'ReaTestItem_Id',
			text: '主键ID',
			hidden: true,
			isKey: true
		}];

		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh', '-', 'add', 'del', '-', 'save', '-', {
			xtype: 'button',
			iconCls: 'file-excel',
			text: '导入导出',
			tooltip: 'EXCEL导入导出检验项目',
			menu: [{
				iconCls: 'button-exp',
				text: '检验项目模板下载',
				tooltip: '检验项目模板下载',
				handler: function() {
					me.onUserDownload();
				}
			}, '-', {
				iconCls: 'button-exp',
				text: '导入检验项目',
				tooltip: '导入检验项目',
				handler: function() {
					me.onUserUpload(true);
				}
			}, {
				text: '导出',
				iconCls: 'file-excel',
				tooltip: '导出复合条件的货品信息到EXCEL',
				listeners: {
					click: function(but) {
						me.onExportExcel(2);
					}
				}
			}]
		}, '-', {
			xtype: 'button',
			iconCls: 'file-excel',
			text: '获取LIS检验项目',
			tooltip: '从LIS系统导入检验项目',
			listeners: {
				click: function(but) {
					me.onExportLis();
				}
			}
		}];
		//查询框信息
		me.searchInfo = {
			width: 215,
			isLike: true,
			itemId: 'Search',
			emptyText: '项目名称/英文名称/Lis编码/代码',
			fields: ['reatestitem.CName', 'reatestitem.EName', 'reatestitem.LisCode', 'reatestitem.ShortCode']
		};
		items.push('->', {
			type: 'search',
			itemId: 'Search',
			info: me.searchInfo
		});
		return items;
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		this.fireEvent('addclick', this);
	},
	/**@overwrite 编辑按钮点击处理方法*/
	onEditClick: function() {
		this.fireEvent('editclick', this);
	},
	//用户模板下载
	onUserDownload: function() {
		var me = this;
		var url = JShell.System.Path.UI + '/models/rea/reatestitem/' + JShell.REA.TestItem.EXCEL + '?v=' + new Date().getTime();
		window.open(url);
	},
	/**综合查询*/
	onGridSearch: function() {
		var me = this;
		JShell.Action.delay(function() {
			me.onSearch();
		}, 100);
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		me.internalWhere = me.getWhere();
		return me.callParent(arguments);
	},
	/**获取带查询参数的URL*/
	getWhere: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			Search = null,
			params = [];
		if(!buttonsToolbar) return;
		search = buttonsToolbar.getComponent('Search').getValue();
		me.internalWhere = '';

		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		if(search) {
			if(me.internalWhere) {
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			} else {
				me.internalWhere = "(" + me.getSearchWhere(search) + ")";
			}
		}
		return me.internalWhere;
	},
	/**保存*/
	onSaveClick: function() {
		var me = this,
			records = me.store.data.items;

		var isError = false;
		var changedRecords = me.store.getModifiedRecords(), //获取修改过的行记录
			len = changedRecords.length;

		if(len == 0) {
			JShell.Msg.alert("没有变更，不需要保存！");
			return;
		}
		var IsValidate = me.IsValidate();
		if(!IsValidate) {
			JShell.Msg.error("项目名称不能为空！");
			return;
		}
		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;

		for(var i = 0; i < len; i++) {
			me.updateOne(i, changedRecords[i]);
		}
	},

	/**修改信息*/
	updateOne: function(i, record) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		var entity = {
			Id: record.get('ReaTestItem_Id'),
			CName: record.get('ReaTestItem_CName'),
			EName: record.get('ReaTestItem_EName'),
			Price: record.get('ReaTestItem_Price'),
			LisCode: record.get('ReaTestItem_LisCode'),
			ShortCode: record.get('ReaTestItem_ShortCode'),
			Visible: record.get('ReaTestItem_Visible') ? 1 : 0,
			DispOrder: record.get('ReaTestItem_DispOrder'),
			ZX1: record.get('ReaTestItem_ZX1'),
			ZX2: record.get('ReaTestItem_ZX2'),
			ZX3: record.get('ReaTestItem_ZX3'),
			Memo: record.get('ReaTestItem_Memo'),
			SName: record.get('ReaTestItem_SName')

		};
		fields = 'Id,CName,EName,Price,LisCode,ShortCode,Visible,DispOrder,Memo,ZX1,ZX2,ZX3,SName';
		var params = Ext.JSON.encode({
			entity: entity,
			fields: fields
		});

		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				me.saveCount++;
				if(record) {
					record.set(me.DelField, true);
					record.commit();
				}
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
					JShell.Msg.error("保存信息有误！");
				}
			}
		}, false);
	},
	/**验证*/
	IsValidate: function() {
		var me = this;
		var changedRecords = me.getStore().getModifiedRecords(), //获取修改过的行记录
			len = changedRecords.length;
		var isExect = true;
		//验证项目名称不能为空
		for(var i = 0; i < len; i++) {
			if(!changedRecords[i].get('ReaTestItem_CName')) {
				isExect = false;
			}
		}
		return isExect;
	},
	showMemoText: function(value, meta) {
		var me = this;
		var val = value.replace(/(^\s*)|(\s*$)/g, "");
		val = val.replace(/\\r\\n/g, "<br />");
		val = val.replace(/\\n/g, "<br />");
		var v = "" + value;
		var index1 = v.indexOf("</br>");
		if(index1 > 0) v = v.substring(0, index1);
		if(v.length > 0) v = (v.length > 32 ? v.substring(0, 32) : v);
		if(value.length > 32) {
			v = v + "...";
		}
		var qtipValue = "<p border=0 style='vertical-align:top;font-size:12px; word-break:break-all;'>" + value + "</p>";
		meta.tdAttr = 'data-qtip="' + qtipValue + '"';
		return v
	},
	//用户导入
	onUserUpload: function(hasOrg) {
		var me = this;

		JShell.Win.open('Shell.class.rea.client.testitem.item.UploadUser', {
			//			DeptID: hasOrg ? me.DeptId : '',
			listeners: {
				save: function(p) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	/**
	 * 项目导出
	 * @param {Object} type 导出类型：勾选导出(1)，条件导出(2)
	 */
	onExportExcel: function(type) {
		this.onExportExcelByForm(type);
	},
	/**表单方式提交*/
	onExportExcelByForm: function(type) {
		var me = this;

		me.UpdateForm = me.UpdateForm || Ext.create('Ext.form.Panel', {
			items: [{
					xtype: 'filefield',
					name: 'file'
				},
				{
					xtype: 'textfield',
					name: 'reportType',
					value: "1"
				},
				{
					xtype: 'textfield',
					name: 'idList'
				},
				{
					xtype: 'textfield',
					name: 'where'
				},
				{
					xtype: 'textfield',
					name: 'isHeader',
					value: "0"
				}
			]
		});
		//清空数据
		me.UpdateForm.getForm().setValues({
			idList: '',
			where: ''
		});
		if(type == 1) { //类型为勾选导出
			var records = me.getSelectionModel().getSelection(),
				len = records.length,
				ids = [];

			if(len == 0) {
				JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
				return;
			}

			for(var i = 0; i < len; i++) {
				ids.push(records[i].get(me.PKField));
				me.UpdateForm.getForm().setValues({
					idList: ids.join(",")
				});
			}
		} else if(type == 2) { //类型为条件导出
			var where = me.getWhere();
			if(where.length == 0) {
				where = '1=1';
			} else {
				where = "(" + where + ")";
			}
			me.UpdateForm.getForm().setValues({
				where: where
			});
		}
		me.showMask("数据请求中...");
		var url = JShell.System.Path.ROOT + me.reportExcelUrl;
		me.UpdateForm.getForm().submit({
			url: url,
			//waitMsg:JShell.Server.SAVE_TEXT,
			success: function(form, action) {
				me.hideMask();
				var fileName = action.result.ResultDataValue;
				var downloadUrl = JShell.System.Path.ROOT + me.downLoadExcelUrl;
				downloadUrl += '?isUpLoadFile=1&operateType=0&downFileName=客户端检验项目数据&fileName=' + fileName.split('\/')[2];
				downloadUrl = encodeURI(downloadUrl);
				window.open(downloadUrl);
			},
			failure: function(form, action) {
				me.hideMask();
				JShell.Msg.error(action.result.ErrorInfo);
			}
		});
	},
	/**
	 * 从LIS系统导入项目
	 */
	onExportLis: function() {
		var me = this;
		var url = JShell.System.Path.ROOT + '/ReaManageService.svc/RS_UDTO_EditSyncReaTestItemInfo';
		me.CenOrgEnum = {}, me.CenOrgList = [];
		me.showMask("获取LIS检验项目中...");
		JShell.Server.get(url, function(data) {
			me.hideMask();
			if(data.success) {
				me.onSearch();
			} else {
				JShell.Msg.error(data.msg);
			}
		}, false);
	}
});