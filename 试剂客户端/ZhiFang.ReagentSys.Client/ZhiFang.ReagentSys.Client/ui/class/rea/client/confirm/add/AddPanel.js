/**
 * 客户端验收
 * @author longfc
 * @version 2017-12-15
 */
Ext.define('Shell.class.rea.client.confirm.add.AddPanel', {
	extend: 'Ext.panel.Panel',

	title: '验收信息',

	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	bodyPadding: 1,

	layout: {
		type: 'border'
	},
	OTYPE: "",
	/**当前选择的主单Id*/
	PK: null,
	/**新增/编辑/查看*/
	formtype: 'add',
	/**供应商ID*/
	ReaCompID: null,
	ReaCompCName: null,
	dtInfoWin: null,
	/**验收双确认方式:secAccepterType：默认本实验室(1),供应商(2),供应商或实验室(3)*/
	secAccepterType: 1,
	ACCEPTMODEL:"1",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('save');
		//自定义按钮功能栏
		me.dockedItems = me.createButtonToolbarItems();
		//me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.DtlGrid = Ext.create('Shell.class.rea.client.confirm.add.DtlGrid', {
			header: false,
			itemId: 'DtlGrid',
			region: 'center',
			defaultLoad: false,
			split: true,
			collapsible: true,
			collapsed: false
		});
		me.DocForm = Ext.create('Shell.class.rea.client.confirm.add.DocForm', {
			header: false,
			itemId: 'DocForm',
			region: 'north',
			width: me.width,
			height: 100,
			split: true,
			collapsible: true,
			collapsed: false
		});
		var appInfos = [me.DtlGrid, me.DocForm];
		return appInfos;
	},
	/**创建功能 按钮栏*/
	createButtonItems: function() {
		var me = this;
		var items = [];
		items.push({
			xtype: 'button',
			iconCls: 'button-save',
			itemId: "btnTemp",
			text: '验收暂存',
			tooltip: '验收暂存',
			handler: function() {
				me.onTempSaveClick();
			}
		});
		if(me.ACCEPTMODEL!="0"){
			items.push({
				xtype: 'button',
				iconCls: 'button-check',
				itemId: "btnCheck",
				text: '验收确认',
				tooltip: '验收确认',
				handler: function() {
					me.onConfirmClick();
				}
			}, {
				boxLabel: '验收确认后入库',
				name: 'cboIsStorage',
				itemId: 'cboIsStorage',
				xtype: 'checkbox',
				inputValue: 1,
				checked: true
			});
		}
		
		return items;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [];
		items=me.createButtonItems();
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	nodata: function() {
		var me = this;
		var me = this;
		me.PK = null;
		me.formtype = "show";

		me.DocForm.PK = null;
		me.DocForm.formtype = "show";
		me.DocForm.StatusName = "";
		me.DocForm.isShow();
		me.DocForm.getForm().reset();
		me.DocForm.getComponent('buttonsToolbar').hide();

		me.DtlGrid.PK = null;
		me.DtlGrid.ReaCompID = null;
		me.DtlGrid.formtype = "show";
		me.DtlGrid.defaultWhere = "";
		me.DtlGrid.Status = null;
		me.DtlGrid.store.removeAll();
		me.DtlGrid.disableControl();
	},
	clearData: function() {
		var me = this;
		me.nodata();
	},
	isAdd: function() {
		var me = this;
		me.PK = null;
		me.formtype = "add";

		me.DocForm.PK = null;
		me.DocForm.Status = "0";
		me.DocForm.formtype = "add";
		me.DocForm.isAdd();
		me.DocForm.getComponent('buttonsToolbar').hide();

		me.DtlGrid.PK = null;
		me.DtlGrid.formtype = "add";
		me.DtlGrid.defaultWhere = "";
		me.DtlGrid.Status = "0";
		me.DtlGrid.store.removeAll();
		me.DtlGrid.enableControl();
	},
	isEdit: function(id) {
		var me = this;
		if(id) me.PK = id;
		me.formtype = "edit";

		me.DocForm.PK = me.PK;
		me.DocForm.Status = "0";
		me.DocForm.isEdit(me.PK);
		me.DocForm.getComponent('buttonsToolbar').hide();
		me.setReaCompNameReadOnly(true);

		me.DtlGrid.PK = me.PK;
		me.DtlGrid.ReaCompID = me.ReaCompID;
		me.DtlGrid.formtype = "edit";
		//只显示暂存未验收的验收货品明细
		me.DtlGrid.defaultWhere = "reabmscensaledtlconfirm.Status=0 and reabmscensaledtlconfirm.SaleDocConfirmID=" + me.PK;
		me.DtlGrid.Status = "0";
		me.DtlGrid.store.removeAll();
		me.DtlGrid.enableControl();
		me.DtlGrid.onSearch();
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if(me.hasLoadMask) {
			me.body.mask(text);
		}
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.hasLoadMask) {
			me.body.unmask();
		}
	},
	ShowDtlPanel: function(grid, info,IsShowScan) {
		var me = this;
		var win = Ext.WindowManager.get(me.OTYPE);
		if(!win) {
			var config = {
				title: "货品信息(5秒后会自动隐藏)",
				resizable: false,
				maximizable: false,
				modal: false,
				closable: true, //关闭功能
				draggable: true, //移动功能
				floating: true, //浮动模式
				width: 280,
				height: 380,
				alwaysOnTop: true,
				itemId: me.OTYPE,
				id: me.OTYPE
			};
			win = JShell.Win.open('Shell.class.rea.client.confirm.basic.DtlInfo', config);
			Ext.WindowManager.register(win);
		}
		if(win) {
			//WIN宽高、位置
			var winHeight = me.getHeight();
			var winWidth = me.getWidth();
			var zIndex = me.zIndexManager.zseed + 100;
			var position = me.getPosition();
			var winPosition = [position[0] + winWidth - win.width - 20, winHeight - win.height - 25];
			win.initData(info);
			if(grid.getIShowDtlInfoValue() == true) {
				win.showAt(winPosition);
			} else {
				win.hide();
				if(!IsShowScan){
					me.DtlGrid.setScanCodeFocus();
				}
			}
			if(grid.hideTimes && grid.hideTimes > 0) {
				JcallShell.Action.delay(function() {
					win.hide();
					if(!IsShowScan){
						me.DtlGrid.setScanCodeFocus();
					}
				}, null, grid.hideTimes);
			}
		}
	},
	/**获取验收确认的系统参数信息*/
	getSecAccepterType: function(callback) {
		var me = this;
		var secAccepterType = Ext.util.Cookies.get("SecAccepterAccount"); //JcallShell.System.Cookie.get
		if(!secAccepterType) {
			var url = JShell.System.Path.getRootUrl("/SingleTableService.svc/ST_UDTO_SearchBParameterByByParaNo?paraNo=SecAccepterAccount");
			JcallShell.Server.get(url, function(data) {
				if(data.success) {
					var obj = data.value;
					if(obj) {
						var paraValue = parseInt(obj.ParaValue);
						if(paraValue)
							me.secAccepterType = parseInt(paraValue);
						else
							paraValue = me.secAccepterType;
						var days = 30;
						var exp = new Date();
						var expires = exp.setTime(exp.getTime() + days * 24 * 60 * 60 * 1000);
						Ext.util.Cookies.set("SecAccepterAccount", paraValue);
					}
					if(callback) callback();
				} else {
					JShell.Msg.error('获取系统参数(验收确认信息)出错！' + data.msg);
				}
			}, false);
		} else {
			me.secAccepterType = parseInt(secAccepterType);
			if(callback) callback();
		}
	},
	/**@description 弹出确认验收录入信息*/
	onOpenCheckForm: function(status) {
		var me = this,
			isError = false;
		me.getSecAccepterType(function() {
			var win = JShell.Win.open('Shell.class.rea.client.confirm.basic.CheckForm', {
				modal: false,
				resizable: false,
				isError: isError,
				PK: me.PK,
				listeners: {
					accept: function(p, data) {
						me.secAccepterType = p.secAccepterType;
						//me.getEl().setStyle('z-index',me.zIndexManager.zseed-110);
						p.close();
						me.onSaveCheckData(status, data);
					}
				}
			}).show();

			if(me.PK) win.isEdit(me.PK);
		});
	},
	/**@description 弹出确认验收录入信息确认后*/
	onSaveCheckData: function(status, data) {
		var me = this;
		me.onSave(status, data);
	},
	/**@description 供货商是可编辑还是只读处理*/
	setReaCompNameReadOnly: function(bo) {
		var me = this;
		var com = me.DocForm.getComponent('ReaBmsCenSaleDocConfirm_CompanyName');
		if(com) com.setReadOnly(bo);
	},
	/**@description 验收确认后入库*/
	getcboIsStorageValue: function() {
		var me = this;
		var iShowDtlInfo = me.getComponent("buttonsToolbar").getComponent("cboIsStorage");
		return iShowDtlInfo.getValue();
	},
	onTempSaveClick: function() {
		var me = this;
		me.validatorSave("0");

		me.DocForm.Status = "0";
		me.DtlGrid.Status = "0";
		me.onSave("0", null);
	},
	onConfirmClick: function() {
		var me = this;
		if(me.validatorSave("1")) {
			me.DocForm.Status = "1";
			me.DtlGrid.Status = "1";
			//验收确认弹出录入
			me.onOpenCheckForm("1");
		}
	},
	validatorSave: function(status) {
		var me = this;
		if(!me.DocForm.getForm().isValid()) {
			JShell.Msg.error("供货商信息为空,不能保存!");
			return false;
		}
		if(me.DtlGrid.store.getCount() <= 0) {
			JShell.Msg.error("待验收货品明细为空,不能保存!");
			return false;
		}
		var docEntity = me.formtype == 'add' ? me.DocForm.getAddParams() : me.DocForm.getEditParams();
		if(!docEntity.entity) {
			JShell.Msg.error("获取封装验收细信息为空");
			return false;
		}
		var result = me.DtlGrid.validatorSave();
		if(result.isValid == false) {
			JShell.Msg.error(result.info);
			return false;
		}
		var dtParams = me.DtlGrid.getSaveParams(status);
		switch(me.formtype) {
			case "add":
				if(!dtParams.dtAddList || dtParams.dtAddList.length <= 0) {
					JShell.Msg.error("获取验收货品明细信息为空,不能保存!");
					return false;
				}
				break;
			case "edit":
				if(!dtParams.dtAddList) {
					dtParams.dtAddList = [];
				}
				if(!dtParams.dtEditList) {
					dtParams.dtEditList = [];
				}
				if(dtParams.dtAddList.length <= 0 && dtParams.dtEditList.length <= 0) {
					JShell.Msg.error("获取验收货品明细信息为空,不能保存!");
					return false;
				}
				break;
			default:
				break;
		}
		return true;
	},
	/**UI应用关闭前将编辑列表的编辑状态取消,防止关闭时UI错乱*/
	cancelEdit: function() {
		var me = this;
		var plugin = me.DtlGrid.getPlugin(me.DtlGrid.cellpluginId);
		if(plugin) {
			plugin.cancelEdit();
		}
	}
});