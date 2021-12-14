/**
 * 模板信息维护
 * @author longfc
 * @version 2020-03-27
 */
Ext.define('Shell.class.blood.template.Grid', {
	extend: 'Shell.class.blood.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.BoolComboBox'
	],
	title: '模板信息维护',
	width: 800,
	height: 500,
	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchBTemplateByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/BloodTransfusionManageService.svc/BT_UDTO_DelBTemplate',
	/**修改服务地址*/
	editUrl: '/BloodTransfusionManageService.svc/BT_UDTO_UpdateBTemplateByField',
	/**下载模板文件地址*/
	downLoadUrl: '/BloodTransfusionManageService.svc/BT_UDTO_DownLoadTemplateFrx',
	/**默认加载数据*/
	defaultLoad: true,
	/**模板信息类型Key*/
	BTemplateType: "BTemplateType",
	/**用户UI配置Key*/
	userUIKey: 'template.Grid',
	/**用户UI配置Name*/
	userUIName: "模板信息列表",
	
	initComponent: function() {
		var me = this;
		JShell.REA.StatusList.getStatusList(me.BTemplateType, false, true, null);
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [ {
			dataIndex: 'BTemplate_TemplateType',
			text: '模板分类',
			sortable: true,
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'BTemplate_TypeID',
			text: '模板类型',
			sortable: true,
			width: 100,
			renderer: function(value, meta) {
				var v = value;
				if(JShell.REA.StatusList.Status[me.BTemplateType].Enum != null)
					v = JShell.REA.StatusList.Status[me.BTemplateType].Enum[value];
				var bColor = "";
				if(JShell.REA.StatusList.Status[me.BTemplateType].BGColor != null)
					bColor = JShell.REA.StatusList.Status[me.BTemplateType].BGColor[value];
				var fColor = "";
				if(JShell.REA.StatusList.Status[me.BTemplateType].FColor != null)
					fColor = JShell.REA.StatusList.Status[me.BTemplateType].FColor[value];
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
			dataIndex: 'BTemplate_CName',
			text: '模板名称',
			sortable: true,
			width: 160,
			defaultRenderer: true
		}, {
			dataIndex: 'BTemplate_SName',
			text: '简称',
			sortable: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'BTemplate_Shortcode',
			text: '快捷码',
			sortable: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'BTemplate_PinYinZiTou',
			text: '拼音字头',
			sortable: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'BTemplate_FileName',
			text: '文件名称',
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'BTemplate_FilePath',
			text: '模板存放路径',
			width: 100,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'BTemplate_FileExt',
			text: '文件扩展名',
			width: 100,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'BTemplate_ContentType',
			text: '内容类型',
			width: 100,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'BTemplate_FileSize',
			text: '文件大小',
			width: 110,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'BTemplate_IsDefault',
			text: '默认模板',
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
			dataIndex: 'BTemplate_IsUse',
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
			dataIndex: 'BTemplate_DispOrder',
			text: '显示次序',
			sortable: true,
			width: 80,
			defaultRenderer: true
		}, {
			xtype: 'actioncolumn',
			text: '下载',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-down hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.downloadFile(rec);
				}
			}]
		}, {
			dataIndex: 'BTemplate_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];

		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh', '-', 'add', 'edit', 'del', '-', {
			text: 'Excel模板选择',
			tooltip: 'frx公共模板选择',
			iconCls: 'button-add',
			itemId: 'btnAddExcelTemplate',
			handler: function() {
				me.onPublicTemplate("Excel模板");
			}
		}, '-',{
			text: 'frx模板选择',
			tooltip: 'frx公共模板选择',
			iconCls: 'button-add',
			itemId: 'btnAddFrxTemplate',
			handler: function() {
				me.onPublicTemplate("Frx模板");
			}
		}, '-', {
			text: '设置默认模板',
			tooltip: '设置默认模板',
			iconCls: 'button-config',
			itemId: 'btnMinUnit',
			handler: function() {
				me.showConfigForm();
			}
		}, {
			emptyText: '模板类型',
			xtype: 'uxSimpleComboBox',
			name: 'BTemplateType',
			itemId: 'BTemplateType',
			hasStyle: true,
			data: JShell.REA.StatusList.Status[me.BTemplateType].List,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onGridSearch();
				}
			},
			width: 100,
			labelWidth: 0
		}];
		//查询框信息
		me.searchInfo = {
			width: 200,
			isLike: true,
			itemId: 'Search',
			emptyText: '模板名称/简称/快捷码/拼音字头',
			fields: ['btemplate.CName', 'btemplate.SName', 'btemplate.Shortcode', 'btemplate.PinYinZiTou']
		};
		items.push('->', {
			type: 'search',
			info: me.searchInfo
		});
		return items;
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		var me = this;
		me.showForm(null);
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick: function() {
		var me = this,
			records = me.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var Id = records[0].get('BTemplate_Id');
		me.showForm(Id);
	},
	/**显示表单*/
	showForm: function(id) {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.78;
		var height = document.body.clientHeight * 0.8;
		var config = {
			resizable: false,
			width: 480,
			height: 300,
			listeners: {
				save: function(p) {
					me.onSearch();
					p.close();
				}
			}
		};

		if(id) {
			config.formtype = 'edit';
			config.PK = id;
		} else {
			config.formtype = 'add';
		}
		JShell.Win.open('Shell.class.rea.client.template.Form', config).show();
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
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			BTemplateType = buttonsToolbar.getComponent('BTemplateType').getValue(),
			Search = buttonsToolbar.getComponent('Search').getValue(),
			params = [];
		me.internalWhere = '';
		if(BTemplateType) {
			params.push("btemplate.TypeID='" + BTemplateType + "'");
		}
		if(Search) {
			params.push('(' + me.getSearchWhere(Search) + ')');
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		}

		return me.callParent(arguments);
	},
	/**设置默认模板*/
	showConfigForm: function() {
		var me = this,
			config = {
				resizable: false,
				listeners: {
					save: function(p) {
						p.close();
						me.onGridSearch();
					}
				}
			};
		JShell.Win.open('Shell.class.rea.client.template.DefaultTemplateGrid', config).show();
	},
	/**下载模板文件*/
	downloadFile: function(record) {
		var me = this;
		var id = "";
		if(record != null) {
			id = record.get('BTemplate_Id');
		}
		var url = JShell.System.Path.getRootUrl(me.downLoadUrl);
		url += '?operateType=0&id=' + id;
		window.open(url);
	},
	/**公共模板选择*/
	onPublicTemplate: function(publicTemplateDir) {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.78;
		var height = document.body.clientHeight * 0.8;
		var config = {
			resizable: true,
			width: 540,
			height: height,
			publicTemplateDir:publicTemplateDir,
			listeners: {
				accept: function(p, records) {
					if(records && records.length > 0) {
						me.onSavePublicTemplate(p, records);
					} else {
						p.close();
					}
				}
			}
		};
		JShell.Win.open('Shell.class.rea.client.template.PublicTemplateCheck', config).show();
	},
	onSavePublicTemplate: function(p, records) {
		var me = this;
		var list = [];
		for(var i = 0; i < records.length; i++) {
			var obj = {
				"BTemplateType": records[i].get("BTemplateType"),
				"CName": records[i].get("CName"),
				"FileName": records[i].get("FileName"),
				"FullPath": records[i].get("FullPath")
			};
			list.push(obj);
		}
		if(!list || list.length <= 0) {
			return;
		}
		var labId = 0,
			labCName = "";
		var params = {
			"labId": labId,
			"labCName": labCName,
			"entityList": JShell.JSON.encode(list)
		};
		var url = JShell.System.Path.getRootUrl("/ReaManageService.svc/RS_UDTO_AddBTemplateOfPublicTemplate");
		JShell.Server.post(url, JcallShell.JSON.encode(params), function(data) {
			if(data.success) {
				me.onSearch();
				p.close();
			} else {
				JShell.Msg.error('保存报表模板错误:' + data.msg);
			}
		});
	}
});