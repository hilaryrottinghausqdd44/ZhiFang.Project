/**
 * @description 部门采购申请录入
 * @author longfc
 * @version 2017-10-23
 */
Ext.define('Shell.class.rea.client.apply.basic.App', {
	extend: 'Ext.panel.Panel',

	title: '采购申请',
	header: false,
	border: false,

	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	bodyPadding: 1,
	layout: {
		type: 'border'
	},
	/**录入:entry/审核:check/生成订单:create*/
	OTYPE: "entry",
	/**新增/编辑/查看*/
	formtype: 'show',
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
		me.OTYPE = me.OTYPE || "";
		//自定义按钮功能栏
		me.dockedItems = me.createButtonToolbarItems();
		me.items = me.createItems();
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
			OTYPE: me.OTYPE,
			collapsible: false,
			collapsed: false
		});
		me.EditPanel = Ext.create('Shell.class.rea.client.apply.basic.EditPanel', {
			header: false,
			itemId: 'EditPanel',
			region: 'center',
			collapsible: false,
			collapsed: false,
			OTYPE: me.OTYPE
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
	}
});