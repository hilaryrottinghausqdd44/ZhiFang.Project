/**
 * CS试剂客户端升级BS
 * @author longfc
 * @version 2018-10-17
 */
Ext.define('Shell.class.rea.client.cstobs.BtnForm', {
	extend: 'Shell.ux.form.Panel',
	title: 'CS试剂客户端升级BS',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	width: 320,
	height: 390,
	/**内容周围距离*/
	bodyPadding: '0px 20px 0px 10px',
	formtype: "add",
	autoScroll: true,
	/**新增服务地址*/
	addUrl: '/ReaManageService.svc/RS_UDTO_AddCSUpdateToBSByStep',
	/**新增服务地址*/
	delUrl: '/ReaManageService.svc/RS_UDTO_DeleteCSUpdateToBSQtyDtlInfo',

	/**布局方式*/
	layout: {
		type: 'table',
		columns: 1 //每行有几列
	},

	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('changeResult', 'load', 'saveClick');
		me.defaultTitle = me.title;
		me.items = me.items || me.createItems();
		me._thisfields = [];
		me.initPKField(); //初始化主键字段
		//创建挂靠功能栏
		var dockedItems = me.createDockedItems();
		if(dockedItems.length > 0) {
			me.dockedItems = dockedItems;
		}
		me.callParent(arguments);
	},
	createItems: function(html) {
		var me = this;
		return [{
			xtype: 'button',
			itemId: 'addHRDept',
			text: '导入部门信息',
			margin: '5px 5px',
			width: 145,
			handler: function() {
				var str = 'HRDept';
				me.onSaveClick(str);
			}
		}, {
			xtype: 'button',
			itemId: 'addRBACUser',
			text: '导入人员帐号',
			margin: '5px 5px',
			width: 145,
			handler: function() {
				var str = 'HREmployee|RBACUser';
				me.onSaveClick(str);
			}
		}, {
			xtype: 'button',
			itemId: 'addHREmployee',
			text: '导入人员帐号',
			margin: '5px 5px',
			width: 145,
			hidden: true,
			handler: function() {
				var str = 'HREmployee';
				me.onSaveClick(str);
			}
		}, {
			xtype: 'button',
			itemId: 'addRBACRole',
			text: '导入角色信息',
			margin: '5px 5px',
			width: 145,
			handler: function() {
				var str = 'RBACRole';
				me.onSaveClick(str);
			}
		}, {
			xtype: 'button',
			itemId: 'addReaCenOrg',
			text: '导入供应商信息',
			margin: '5px 5px',
			width: 145,
			handler: function() {
				var str = 'ReaCenOrg';
				me.onSaveClick(str);
			}
		}, {
			xtype: 'button',
			itemId: 'addReaStorage',
			text: '导入库房信息',
			margin: '5px 5px',
			width: 145,
			handler: function() {
				var str = 'ReaStorage';
				me.onSaveClick(str);
			}
		}, {
			xtype: 'button',
			itemId: 'addReaPlace',
			text: '导入库房货架信息',
			margin: '5px 5px',
			width: 145,
			handler: function() {
				var str = 'ReaPlace';
				me.onSaveClick(str);
			}
		}, {
			xtype: 'button',
			itemId: 'addReaTestEquipLab',
			text: '导入仪器信息',
			margin: '5px 5px',
			width: 145,
			handler: function() {
				var str = 'ReaTestEquipLab';
				me.onSaveClick(str);
			}
		}, {
			xtype: 'button',
			itemId: 'addReaGoods',
			text: '导入机构货品信息',
			margin: '5px 5px',
			width: 145,
			handler: function() {
				var str = 'ReaGoods';
				me.onSaveClick(str);
			}
		}, {
			xtype: 'button',
			itemId: 'addReaEquipReagentLink',
			text: '导入仪器试剂信息',
			margin: '5px 5px',
			width: 145,
			handler: function() {
				var str = 'ReaEquipReagentLink';
				me.onSaveClick(str);
			}
		}, {
			xtype: 'button',
			itemId: 'addReaDeptGoods',
			text: '导入部门货品信息',
			margin: '5px 5px',
			width: 145,
			handler: function() {
				var str = 'ReaDeptGoods';
				me.onSaveClick(str);
			}
		}, {
			xtype: 'button',
			itemId: 'addReaGoodsOrgLink',
			text: '导入供货商货品信息',
			margin: '5px 5px',
			width: 145,
			handler: function() {
				var str = 'ReaGoodsOrgLink';
				me.onSaveClick(str);
			}
		}, {
			xtype: 'button',
			itemId: 'delReaBmsQtyDtl',
			text: '清空BS库存信息',
			margin: '5px 5px',
			width: 145,
			handler: function() {
				var str = 'DeleteOldQtyInfo';
				me.onSaveClick(str);
			}
		}, {
			xtype: 'button',
			itemId: 'addReaBmsQtyDtl',
			text: '导入CS库存',
			margin: '5px 5px',
			width: 145,
			handler: function() {
				var str = 'ReaBmsQtyDtl';
				me.onSaveClick(str);
			}
		}, {
			xtype: 'displayfield',
			width: me.width - 20,
			fieldLabel: '',
			labelWidth: 0,
			//hidden:true,
			value: '' + //<h2>功能按钮说明：</h2>
				'<b style="color:#0000FF;">以上步骤完成后,靖查看各步骤操作是否都显示为【CS试剂客户端升级BS成功】,如有显示【CS试剂客户端升级BS失败】,请查看操作提示或再重复操作对应步骤按钮</b>'
		}];

	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = me.buttonToolbarItems || [];
		items.push();
	},
	/**更改标题*/
	changeTitle: function() {
		var me = this;
	},
	onSaveClick: function(entity) {
		var me = this;
		var url = me.addUrl;
		if(entity=="DeleteOldQtyInfo")url = me.delUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		var params = {
			"entity": entity
		};
		if(!params) return;
		var obj = Ext.JSON.encode(params);
		me.showMask(JShell.Server.SAVE_TEXT); //显示遮罩层
		var timeout = 600000;
		JShell.Server.post(url, obj, function(data) {
			me.hideMask();
			if(data.success) {
				JShell.Msg.alert("从CS导入数据到BS当前操作完成!", null, 1000);
				var cenorg = null
				me.fireEvent('saveClick', cenorg);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	}
});