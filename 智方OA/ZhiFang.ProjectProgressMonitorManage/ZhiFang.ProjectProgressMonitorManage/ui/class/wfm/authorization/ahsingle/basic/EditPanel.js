/**
 * 单站点授权
 * @author longfc	
 * @version 2016-12-20
 */
Ext.define('Shell.class.wfm.authorization.ahsingle.basic.EditPanel', {
	extend: 'Shell.ux.panel.AppPanel',
	width: 800,
	height: 500,
	title: '单站点授权',

	/**修改服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdateAHSingleLicenceByField',
	
	/**自定义按钮功能栏*/
	buttonToolbarItems: null,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**通过按钮文字*/
	OverButtonName: '',
	/**不通过按钮文字*/
	BackButtonName: '',
	/**通过状态文字*/
	OverName: '',
	/**不通过状态文字*/
	BackName: '',
	/**处理意见字段*/
	OperMsgField: '',
	/**处理时间字段*/
	AuditDataTimeMsgField: '',
	/**授权ID*/
	PK: null,
	PClientID: null,
	/**表单参数*/
	FormConfig: null,
	/**处理意见内容*/
	OperMsg: '',
	/**需要保存的数据*/
	Entity: null,
	classNameSpace: 'ZhiFang.Entity.ProjectProgressMonitorManage', //类域
	className: 'LicenceStatus', //类名
	/**是否使用编辑表单*/
	isUseEditForm: false,
	/**是否新授权(授权码为纯数字)*/
	IsNumLicenceByMACValue: true,
	/**是否显示新授权选择项(授权码为纯数字)*/
	hasIsNumLicenceByMAC: false,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initListeners();
		me.loadData();
	},
	initListeners: function() {
		var me = this;
	},
	initComponent: function() {
		var me = this;
		me.addEvents('save');
		me.items = me.createItems();
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.ContractPanel = Ext.create('Shell.class.wfm.authorization.contract.ShowPanel', {
			region: 'west',
			width: 340,
			header: true,
			border: false,
			split: true,
			itemId: 'ContractPanel',
			PClientID: me.PClientID
		});
		me.EditTabPanel = Ext.create('Shell.class.wfm.authorization.ahsingle.basic.EditTabPanel', {
			region: 'center',
			header: false,
			border: false,
			isUseEditForm: me.isUseEditForm,
			itemId: 'EditTabPanel',
			OverButtonName: me.OverButtonName,
			BackButtonName: me.BackButtonName,
			OverName: me.OverName,
			BackName: me.BackName,
			OperMsgField: me.OperMsgField,
			AuditDataTimeMsgField: me.AuditDataTimeMsgField,
			FormConfig: me.FormConfig,
			PK: me.PK
		});
		return [me.ContractPanel, me.EditTabPanel];
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasButtontoolbar) {
			items = me.createButtontoolbar();
		}
		return Ext.create('Ext.toolbar.Toolbar', {
			dock: 'bottom',
			itemId: 'buttonsToolbar',
			items: items
		});
	},

	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = ['->'];
		if(me.hasIsNumLicenceByMAC) {
			items.push({
				boxLabel: '新授权 注：2006年5月及以后采用新授权',
				itemId: 'checkIsCharLicenceByMAC',
				checked: me.IsNumLicenceByMACValue,
				value: me.IsNumLicenceByMACValue,
				inputValue: false,
				xtype: 'checkbox',
				style: {
					marginRight: '10px'
				},
				listeners: {
					change: function(com, newValue, oldValue, eOpts) {
						if(newValue == true) {
							me.IsNumLicenceByMACValue = true;
						} else {
							me.IsNumLicenceByMACValue = false;
						}
					}
				}
			});
		}
		if(me.OverButtonName) {
			items.push({
				iconCls: 'button-save',
				text: me.OverButtonName,
				tooltip: me.OverButtonName,
				handler: function() {
					me.onOver();
				}
			});
		}
		if(me.BackButtonName) {
			items.push({
				iconCls: 'button-save',
				text: me.BackButtonName,
				tooltip: me.BackButtonName,
				handler: function() {
					me.onBack();
				}
			});
		}
		return items;
	},
	/**通过*/
	onOver: function() {
		var me = this;

		if(me.OperMsgField) {
			//处理意见
			JShell.Msg.confirm({
				title: '<div style="text-align:center;">处理意见</div>',
				msg: '',
				closable: false,
				multiline: true //多行输入框
			}, function(but, text) {
				if(but != "ok") return;
				me.OperMsg = text;
				me.onSaveClick(me.OverName);
			});
		} else {
			//确定进行该操作
			JShell.Msg.confirm({
				msg: '确定进行该操作？'
			}, function(but, text) {
				if(but != "ok") return;
				me.onSaveClick(me.OverName);
			});
		}
	},
	/**未通过*/
	onBack: function() {
		var me = this;

		if(me.OperMsgField) {
			//处理意见
			JShell.Msg.confirm({
				title: '<div style="text-align:center;">处理意见</div>',
				msg: '',
				closable: false,
				multiline: true //多行输入框
			}, function(but, text) {
				if(but != "ok") return;
				me.OperMsg = text;
				me.onSaveClick(me.BackName);
			});
		} else {
			//确定进行该操作
			JShell.Msg.confirm({
				msg: '确定进行该操作？'
			}, function(but, text) {
				if(but != "ok") return;
				me.onSaveClick(me.BackName);
			});
		}
	},
	/**保存前验证方法*/
	verificationSubmit: function() {
		var me = this;
		return true;
	},
	onSaveClick: function(StatusName) {
		var me = this;
		var result = me.verificationSubmit();
		if(result == true) {
			JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage', 'LicenceStatus', function() {
				if(!JShell.System.ClassDict.LicenceStatus) {
					JShell.Msg.error('未获取到授权状态，请重新保存');
					return;
				}
				var info = JShell.System.ClassDict.getClassInfoByName('LicenceStatus', StatusName);
				me.onSave(info.Id);
			});
		}
	},
	/**保存数据*/
	onSave: function(Status) {
		var me = this,
			url = JShell.System.Path.getRootUrl(me.editUrl),
			params = me.getParams(Status);
		JShell.Server.post(url, Ext.JSON.encode(params), function(data) {
			if(data.success) {
				me.fireEvent('save', me, me.PK)
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**获取编辑实体参数(一审需要重写)*/
	getEditParams: function(status) {
		var me = this;
		var editParams = {
			entity: {
				Id: me.PK,
				Status: status
			},
			fields: 'Id,Status'
		};
		return editParams;
	},
	/**获取参数*/
	getParams: function(status) {
		var me = this;
		var Sysdate = JcallShell.System.Date.getDate();
		var ReviewDate = JcallShell.Date.toString(Sysdate);
		var ReviewDateStr = JShell.Date.toServerDate(ReviewDate);
		var params = me.getEditParams(status);
		//处理意见
		if(me.OperMsgField) {
			if(me.OperMsg != null && me.OperMsg != undefined) {
				me.OperMsg = me.OperMsg.replace(/\\/g, '&#92');
				me.OperMsg = me.OperMsg.replace(/[\r\n]/g, '<br />');
			}
			params.entity[me.OperMsgField] = me.OperMsg;
		}
		if(me.AuditDataTimeMsgField && ReviewDateStr) {
			params.entity[me.AuditDataTimeMsgField] = ReviewDateStr;
		}
		//需要保存的数据
		if(me.Entity && Ext.typeOf(me.Entity) == 'object') {
			for(var i in me.Entity) {
				params.entity[i] = me.Entity[i];
			}
		}
		return params;
	},
	/**@public加载数据*/
	loadData: function() {}
});