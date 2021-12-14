/**
 * @description 采购申请合并报表
 * @author longfc
 * @version 2018-11-27
 */
Ext.define('Shell.class.rea.client.apply.mergereport.App', {
	extend: 'Shell.class.rea.client.apply.basic.App',

	title: '合并报表',
	/**模板/报表类型:Frx;Excel*/
	reaReportClass: "Frx",
	/**模板分类:Excel模板,Frx模板*/
	publicTemplateDir: "Frx模板",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.ApplyGrid.on({
			selectionchange: function(model, selected, eOpts) {
				JShell.Action.delay(function() {
					//me.selectAfter();
					me.loadData(selected[selected.length - 1]);
				}, null, 500);
			},
			nodata: function(p) {
				me.nodata();
			}
		});
	},
	initComponent: function() {
		var me = this;
		//自定义按钮功能栏
		me.dockedItems = me.createButtonToolbarItems();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.ApplyGrid = Ext.create('Shell.class.rea.client.apply.mergereport.ApplyGrid', {
			header: false,
			itemId: 'ApplyGrid',
			region: 'west',
			width: 345,
			split: true,
			collapsible: false,
			collapsed: false,
			animCollapse: false,
			animate: false
		});
		me.EditPanel = Ext.create('Shell.class.rea.client.apply.mergereport.EditPanel', {
			header: false,
			itemId: 'EditPanel',
			region: 'center',
			collapsible: false,
			collapsed: false
		});
		var appInfos = [me.ApplyGrid, me.EditPanel];
		return appInfos;
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
				["Frx", "PDF预览"]
			],
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onReportClassCheck(newValue);
				}
			}
		});
		return items;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [];

		if(items.length > 0) items.push('-');

		items = me.createReportClassButtonItem(items);
		items = me.createTemplate(items);
		items.push({
			xtype: 'button',
			iconCls: 'button-print',
			itemId: "btnPrint",
			text: '预览',
			tooltip: '预览PDF清单',
			handler: function() {
				me.onPrintClick();
			}
		});

		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			border: false,
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	nodata: function() {
		var me = this;
		me.callParent(arguments);
	},
	loadData: function(record) {
		var me = this;
		me.isShow(record);
	},
	isShow: function(record) {
		var me = this;
		me.callParent(arguments);
		me.setFormType("show");
	},
	/**@description 生成订单按钮点击处理方法*/
	onPrintClick: function() {
		var me = this;
		var records = me.ApplyGrid.getSelectionModel().getSelection();
		if(records.length <= 0) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		if(!me.pdfFrx) {
			JShell.Msg.error("请先选择清单模板后再操作!");
			return;
		}
		var idStr = "";
		var len = records.length;
		for(var i = 0; i < len; i++) {
			var rec = records[i];

			var visible = "" + rec.get("ReaBmsReqDoc_Visible");
			if(visible == "false" || visible == false) visible = '0';
			if(visible == "0" || visible == "false") {
				JShell.Msg.error("选择的申请单中包含有已禁用的申请单,不能合并报表!");
				return;
			}
			var status = rec.get("ReaBmsReqDoc_Status");
			//已审核
			if(status == "2" || status == "3" || status == "5") {
				idStr += rec.get("ReaBmsReqDoc_Id") + ",";
			} else {
				idStr = "";
				var statusName = "";
				if(JShell.REA.StatusList.Status[me.ApplyGrid.StatusKey])
					statusName = JShell.REA.StatusList.Status[me.ApplyGrid.StatusKey].Enum[status];
				JShell.Msg.error("选择的申请单中包含有状态为【" + statusName + "】的申请单,不能合并报表!");
				return;
			}
		}

		JShell.Msg.confirm({
			title: '<div style="text-align:center;">合并报表</div>',
			msg: '请确认是否对当前选择的申请单进行合并报表?',
			closable: false
		}, function(but, text) {
			if(but != "ok") return;
			if(idStr) {
				idStr = idStr.substring(0, idStr.length - 1);
				me.onPrint(idStr);
			}
		});
	},
	onPrint: function(idStr) {
		var me = this,
			operateType = '1';
		if(!me.pdfFrx) {
			JShell.Msg.error("请先选择清单模板后再操作!");
			return;
		}
		var url = JShell.System.Path.getRootUrl("/ReaManageService.svc/RS_UDTO_SearchReaBmsReqDocMergeReportOfPdfByIdStr");
		var params = [];
		params.push("reaReportClass=" + me.reaReportClass);
		params.push("operateType=" + operateType);
		params.push("idStr=" + idStr);
		params.push("breportType=" + me.breportType);
		if(me.pdfFrx) {
			params.push("frx=" + JShell.String.encode(me.pdfFrx));
		}
		url += "?" + params.join("&");
		window.open(url);
	}
});