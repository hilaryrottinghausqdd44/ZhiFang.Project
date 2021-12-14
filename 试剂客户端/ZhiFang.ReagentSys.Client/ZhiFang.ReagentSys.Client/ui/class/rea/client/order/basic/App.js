/**
 * @description 订单申请录入
 * @author longfc
 * @version 2017-10-23
 */
Ext.define('Shell.class.rea.client.order.basic.App', {
	extend: 'Ext.panel.Panel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '订单申请',
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
	breportType: 2,
	/**模板/报表类型:Frx;Excel*/
	reaReportClass: "Excel",
	/**模板分类:Excel模板,Frx模板*/
	publicTemplateDir: "Excel模板",
	//按钮是否可点击
	BUTTON_CAN_CLICK:true,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.EditPanel.on({
			save: function(p, success, id) {
				if(success == true) {
					me.OrderGrid.onSearch();
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
		me.OrderGrid = Ext.create('Shell.class.rea.client.order.apply.OrderGrid', {
			header: true,
			itemId: 'OrderGrid',
			region: 'west',
			width: 345,
			split: true,
			collapsible: false,
			collapsed: false
		});
		var width=parseInt(document.body.clientWidth-me.OrderGrid.width-220);
		me.OrderPanel = Ext.create('Shell.class.rea.client.order.apply.OrderPanel', {
			header: false,
			itemId: 'OrderPanel',
			width:width,
			region: 'center',
			collapsible: false,
			collapsed: false
		});
		var appInfos = [me.OrderGrid, me.OrderPanel];
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
			width: 75,
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
		}
		if(cbo) {
			cbo.setValue(cname);
		}
		p.close();
	},
	clearData: function() {
		var me = this;
	},
	nodata: function(record) {
		var me = this;
		me.setFormType("show");
		me.setBtnDisabled("btnSave", true);
		me.EditPanel.clearData();
		me.clearData();
	},
	setFormType: function(formtype) {
		var me = this;
		me.formtype = formtype;
		me.EditPanel.formtype = formtype;
		me.EditPanel.DocForm.formtype = formtype;
		me.EditPanel.OrderDtlGrid.formtype = formtype;
		var readOnly = false;
		if(formtype != "add") {
			readOnly = true;
		}
		me.EditPanel.DocForm.setCompReadOnlys(readOnly);
	},
	isShow: function(record) {
		var me = this;
		me.setFormType("show");
		me.EditPanel.isShow(record, me.OrderGrid);
		me.setBtnDisabled("btnSave", true);
		me.EditPanel.OrderDtlGrid.buttonsDisabled = true;
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
		me.EditPanel.isEdit(record, me.OrderGrid);
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
	/**@description 编辑保存订单及明细的保存处理*/
	onSaveOfUpdate: function(params, callback) {
		var me = this;
		//需要保存主单及明细
		if(!params) params = me.EditPanel.getSaveParams();
		if(!params) return false;
		var url = me.EditPanel.formtype == 'add' ? me.EditPanel.DocForm.addUrl : me.EditPanel.DocForm.editUrl;
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
	/**@description 单个主单修改(只操作主单,不操作明细)数据保存*/
	updateOne: function(index, Id, key, value, msg) {
		var me = this,
			url = JShell.System.Path.ROOT + me.OrderGrid.editUrl;
		var params = {
			entity: {
				"Id": Id
			},
			fields: 'Id'
		};
		params.entity[key] = value;
		params.fields += ',' + key;
		setTimeout(function() {
			JShell.Server.post(url, Ext.JSON.encode(params), function(data) {
				if(data.success) {
					me.saveCount++;
				} else {
					me.saveErrorCount++;
				}
				if(me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					if(me.saveErrorCount == 0) {
						JShell.Msg.alert(msg + '操作成功', null, 1000);
						me.OrderGrid.onSearch();
					} else {
						JShell.Msg.error(msg + '操作失败，请重新操作！');
					}
				}
			});
		}, 100 * index);
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
		}
	},
	/**@description 选择某一试剂耗材订单,预览PDF清单*/
	onPrintClick: function() {
		var me = this,
			operateType = '1';
		var records = me.OrderGrid.getSelectionModel().getSelection();
		if(records.length <=0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		
		if(records.length >1) {
			JShell.Msg.error("当前选择的模板只支持单个订单操作!");
			return;
		}
		var id = records[0].get(me.OrderGrid.PKField);
		if(!id) {
			JShell.Msg.error("请先选择订单后再操作!");
			return;
		}
		//		if(!me.reaReportClass || me.reaReportClass != "Frx") {
		//			JShell.Msg.error("请先选择Frx模板后再操作!");
		//			return;
		//		}
		if(!me.pdfFrx) {
			JShell.Msg.error("请先选择订货清单模板后再操作!");
			return;
		}
		//暂存订单的状态不可以预览PDF清单
		var status = records[0].get("ReaBmsCenOrderDoc_Status");
		if(status == "0") {
			var statusName = "";
			if(JShell.REA.StatusList.Status[me.OrderGrid.StatusKey])
				statusName = JShell.REA.StatusList.Status[me.OrderGrid.StatusKey].Enum[status];
			JShell.Msg.error("当前订单状态为【" + statusName + "】,不能预览PDF清单!");
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
	/**选择某一试剂耗材订单,导出EXCEL*/
	onDownLoadExcel: function() {
		var me = this,
			operateType = '0';
		var records = me.OrderGrid.getSelectionModel().getSelection();
		if(records.length <=0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		
		if(records.length >1) {
			JShell.Msg.error("当前选择的模板只支持单个订单操作!");
			return;
		}
		var id = records[0].get(me.OrderGrid.PKField);
		if(!id) {
			JShell.Msg.error("请先选择订单后再操作!");
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
		//暂存订单的状态不可以导出
		var status = records[0].get("ReaBmsCenOrderDoc_Status");
		if(status == "0") {
			var statusName = "";
			if(JShell.REA.StatusList.Status[me.OrderGrid.StatusKey])
				statusName = JShell.REA.StatusList.Status[me.OrderGrid.StatusKey].Enum[status];
			JShell.Msg.error("当前订单状态为【" + statusName + "】,不能导出EXCEL清单!");
			return;
		}

		var url = JShell.System.Path.getRootUrl("/ReaManageService.svc/RS_UDTO_SearchBusinessReportOfExcelById");
		var params = [];
		params.push("operateType=" + operateType);
		params.push("id=" + id);
		params.push("breportType=" + me.breportType);
		if(me.pdfFrx) {
			params.push("frx=" + JShell.String.encode(me.pdfFrx));
		}
		url += "?" + params.join("&");
		window.open(url);
	}
});