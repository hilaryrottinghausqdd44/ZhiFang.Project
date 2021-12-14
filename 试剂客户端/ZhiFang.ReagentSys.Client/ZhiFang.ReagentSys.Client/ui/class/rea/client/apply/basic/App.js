/**
 * @description 部门采购申请录入
 * @author longfc
 * @version 2017-10-23
 */
Ext.define('Shell.class.rea.client.apply.basic.App', {
	extend: 'Ext.panel.Panel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	
	title: '采购申请',
	header: false,
	border: false,

	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	bodyPadding: 1,
	layout: {
		type: 'border'
	},

	/**新增/编辑/查看*/
	formtype: 'show',
	/**PDF报表模板*/
	pdfFrx: null,
	/**业务报表类型:对应BTemplateType枚举的key*/
	breportType: 1,
	/**模板/报表类型:Frx;Excel*/
	reaReportClass: "Excel",
	/**模板分类:Excel模板,Frx模板*/
	publicTemplateDir: "Excel模板",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.EditPanel.on({
			save: function(p, success, id) {
				if(success == true) {
					me.ApplyGrid.onSearch();
				}
			}
		});
	},
	initComponent: function() {
		var me = this;
		//自定义按钮功能栏
		me.dockedItems = me.createButtonToolbarItems();
		//me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.ApplyGrid = Ext.create('Shell.class.rea.client.apply.basic.ApplyGrid', {
			header: false,
			itemId: 'ApplyGrid',
			region: 'west',
			width: 360,
			split: true,
			collapsible: false,
			collapsed: false
		});
		me.EditPanel = Ext.create('Shell.class.rea.client.apply.basic.EditPanel', {
			header: false,
			itemId: 'EditPanel',
			region: 'center',
			collapsible: false,
			collapsed: false
		});
		var appInfos = [me.ApplyGrid, me.EditPanel];
		return appInfos;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [];
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	setFormType: function(formtype) {
		var me = this;
		me.formtype = formtype;
		me.EditPanel.formtype = formtype;
		me.EditPanel.ApplyForm.formtype = formtype;
		me.EditPanel.ReqDtlGrid.formtype = formtype;
	},
	nodata: function(record) {
		var me = this;
		me.formtype = "show";
		me.EditPanel.clearData();
	},
	isShow: function(record) {
		var me = this;
		me.setFormType("show");
		me.EditPanel.isShow(record, me.ApplyGrid);
		me.setBtnDisabled("btnSave", true);
		me.EditPanel.ReqDtlGrid.buttonsDisabled = true;
	},
	isAdd: function(record) {
		var me = this;
		me.setFormType("add");
		me.EditPanel.isAdd();
		me.setBtnDisabled("btnSave", true);
	},
	isEdit: function(record) {
		var me = this;
		me.setFormType("edit");
		me.EditPanel.isEdit(record, me.ApplyGrid);
		me.setBtnDisabled("btnSave", false);
	},
	setBtnDisabled: function(com, disabled) {
		var me = this;
		var buttonsToolbar = me.getComponent("buttonsToolbar");
		if(buttonsToolbar) {
			var btn = buttonsToolbar.getComponent(com);
			if(btn) btn.setDisabled(disabled);
		}
	},
	/**@description编辑保存申请单及明细的保存处理*/
	onSaveOfUpdate: function(params, callback) {
		var me = this;
		//需要保存主单及明细
		if(!params) params = me.EditPanel.getSaveParams();
		if(!params) return false;
		var url = me.EditPanel.formtype == 'add' ? me.EditPanel.ApplyForm.addUrl : me.EditPanel.ApplyForm.editUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;

		JShell.Server.post(url, Ext.JSON.encode(params), function(data) {
			callback(data);
		});
	},
	/**@description 申请主单批量操作(只操作主单,不操作明细)处理方法*/
	confirmUpdate: function(list, key, value, msg) {
		var me = this;
		JShell.Msg.confirm({
			title: msg
		}, function(but) {
			if(but != "ok") return;
			me.saveErrorCount = 0;
			me.saveCount = 0;
			me.saveLength = list.length;
			me.showMask(me.saveText); //显示遮罩层
			for(var i in list) {
				me.updateOne(i, list[i], key, value, msg);
			}
		});
	},
	/**单个主单修改(只操作主单,不操作明细)数据保存*/
	updateOne: function(index, Id, key, value, msg) {
		var me = this,
			url = JShell.System.Path.ROOT + me.ApplyGrid.editUrl;
		//var Id=record.get(me.ApplyGrid.PKField);
		var params = {
			entity: {
				"Id": Id
			},
			fields: 'Id'
		};
		params.entity[key] = value;
		params.fields += ',' + key;
		setTimeout(function() {
			JShell.Server.post(url, JcallShell.JSON.encode(params), function(data) {
				if(data.success) {
					me.saveCount++;
				} else {
					me.saveErrorCount++;
				}
				if(me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					if(me.saveErrorCount == 0) {
						JShell.Msg.alert(msg + '操作成功', null, 1000);
						me.ApplyGrid.onSearch();
					} else {
						JShell.Msg.error(msg + '操作失败，请重新操作！');
					}
				}
			});
		}, 100 * index);
	},
	/**模板分类选择项*/
	createReportClassButtonItem: function(items) {
		var me = this;
		if(!items) {
			items = [];
		}
		items.push({
			xtype: 'uxSimpleComboBox',
			itemId: 'ReportClass',
			labelWidth: 0,
			width: 85,
			value: me.reaReportClass,
			data: [
				["", "请选择"],
				["Frx", "PDF预览"],
				["Excel", "Excel导出"]
			],
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onReportClassCheck(newValue);
				}
			}
		});
		return items;
	},
	/**模板选择项*/
	createTemplate: function(items) {
		var me = this;
		if(!items) {
			items = [];
		}
		items.push({
			fieldLabel: '',
			emptyText: '模板选择',
			labelWidth: 0,
			width: 165,
			name: 'cboTemplate',
			itemId: 'cboTemplate',
			xtype: 'uxCheckTrigger',
			classConfig: {
				width: 205,
				height: 460,
				checkOne: true,
				breportType: me.breportType,
				/**模板分类:Excel模板,Frx模板*/
				publicTemplateDir: me.publicTemplateDir
			},
			className: 'Shell.class.rea.client.template.CheckGrid',
			listeners: {
				beforetriggerclick: function(p) {

				},
				check: function(p, record) {
					me.onTemplateCheck(p, record);
				}
			}
		});
		return items;
	},
	/**@description 报告模板分类选择后*/
	onReportClassCheck: function(newValue) {
		var me = this;
		me.reaReportClass = newValue;
		me.pdfFrx = "";
		if(me.reaReportClass == "Frx") {
			me.publicTemplateDir = "Frx模板";
		} else if(me.reaReportClass == "Excel") {
			me.publicTemplateDir = "Excel模板";
		}
		var buttonsToolbar = me.getComponent("buttonsToolbar");
		var cbo = buttonsToolbar.getComponent("cboTemplate");
		cbo.setValue("");
		cbo.classConfig["publicTemplateDir"] = me.publicTemplateDir;
		var picker = cbo.getPicker();
		if(picker) {
			picker["publicTemplateDir"] = me.publicTemplateDir;
			cbo.getPicker().load();
		}
	},
	onTemplateCheck: function(p, record) {
		var me = this;
		var buttonsToolbar = me.getComponent("buttonsToolbar");
		var cbo = buttonsToolbar.getComponent("cboTemplate");
		var cname = "";
		if(record) {
			me.pdfFrx = record.get("FileName");
			cname = record.get("CName");
		} else {
			me.pdfFrx = "";
		}
		if(cbo) {
			cbo.setValue(cname);
		}
		p.close();
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if(me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.hasLoadMask) {
			me.body.unmask();
		} //隐藏遮罩层
	},
	/**@description 选择某一货品耗材采购申请单,预览PDF清单*/
	onPrintClick: function() {
		var me = this,
			operateType = '1';
		var records = me.ApplyGrid.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		var id = records[0].get(me.ApplyGrid.PKField);
		if(!id) {
			JShell.Msg.error("请先选择采购申请单后再操作!");
			return;
		}
		//		if(!me.reaReportClass || me.reaReportClass != "Frx") {
		//			JShell.Msg.error("请先选择Frx模板后再操作!");
		//			return;
		//		}
		if(!me.pdfFrx) {
			JShell.Msg.error("请先选择清单模板后再操作!");
			return;
		}
		//已审核通过,转为订单的状态可以预览PDF清单
		var status = records[0].get("ReaBmsReqDoc_Status");
		if(status != "2" && status != "3" && status != "5") {
			var statusName = "";
			if(JShell.REA.StatusList.Status[me.ApplyGrid.StatusKey])
				statusName = JShell.REA.StatusList.Status[me.ApplyGrid.StatusKey].Enum[status];
			JShell.Msg.error("当前申请单状态为【" + statusName + "】,不能预览PDF清单!");
			return;
		}
		var url = JShell.System.Path.getRootUrl("/ReaManageService.svc/RS_UDTO_SearchBusinessReportOfPdfById");

		var params = [];
		params.push("reaReportClass=" + me.reaReportClass);
		params.push("operateType=" + operateType);
		params.push("id=" + id);
		params.push("breportType=" + me.breportType);
		if(me.pdfFrx) {
			params.push("frx=" + JShell.String.encode(me.pdfFrx));
		}
		url += "?" + params.join("&");

		window.open(url);
	},
	/**选择某一货品耗材采购申请单,导出EXCEL*/
	onDownLoadExcel: function() {
		var me = this,
			operateType = '0'
		var records = me.ApplyGrid.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		var id = records[0].get(me.ApplyGrid.PKField);
		if(!id) {
			JShell.Msg.error("请先选择采购申请单后再操作!");
			return;
		}
		if(!me.reaReportClass || me.reaReportClass != "Excel") {
			JShell.Msg.error("请先选择Excel模板后再操作!");
			return;
		}
		if(!me.pdfFrx) {
			JShell.Msg.error("请先选择Excel模板后再操作!");
			return;
		}
		//已提交,已审核通过,转为订单的状态可以预览PDF清单
		var status = records[0].get("ReaBmsReqDoc_Status");
		if(status != "2" && status != "3" && status != "5") {
			var statusName = "";
			if(JShell.REA.StatusList.Status[me.ApplyGrid.StatusKey])
				statusName = JShell.REA.StatusList.Status[me.ApplyGrid.StatusKey].Enum[status];
			JShell.Msg.error("当前申请单状态为【" + statusName + "】,不能导出EXCEL清单!");
			return;
		}

		var url = JShell.System.Path.getRootUrl("/ReaManageService.svc/RS_UDTO_SearchBusinessReportOfExcelById");
		var params = [];
		params.push("operateType=" + operateType);
		params.push("breportType=" + me.breportType);
		params.push("id=" + id);
		if(me.pdfFrx) {
			params.push("frx=" + JShell.String.encode(me.pdfFrx));
		}
		url += "?" + params.join("&");
		window.open(url);
	}
});