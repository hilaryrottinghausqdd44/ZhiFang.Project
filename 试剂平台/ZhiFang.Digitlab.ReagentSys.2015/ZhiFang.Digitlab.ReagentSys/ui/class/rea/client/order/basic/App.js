/**
 * @description 订单申请录入
 * @author longfc
 * @version 2017-10-23
 */
Ext.define('Shell.class.rea.client.order.basic.App', {
	extend: 'Ext.panel.Panel',

	title: '订单申请',
	header: false,
	border: false,

	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	bodyPadding: 1,
	layout: {
		type: 'border'
	},
	/**录入:entry/审核:check*/
	OTYPE: "entry",
	/**新增/编辑/查看*/
	formtype: 'show',
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
		me.OTYPE = me.OTYPE || "";
		//自定义按钮功能栏
		me.dockedItems = me.createButtonToolbarItems();
		me.items = me.createItems();
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
		me.OrderPanel = Ext.create('Shell.class.rea.client.order.apply.OrderPanel', {
			header: false,
			itemId: 'OrderPanel',
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
	/**@description 编辑保存申请单及明细的保存处理*/
	onSaveOfUpdate: function(params, callback) {
		var me = this;
		//需要保存主单及明细
		if(!params) params = me.EditPanel.getSaveParams();
		if(!params) return false;
		var url = me.EditPanel.formtype == 'add' ? me.EditPanel.DocForm.addUrl : me.EditPanel.DocForm.editUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;

		JShell.Server.post(url, JcallShell.JSON.encode(params), function(data) {
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
	}
});