/**
 * 出库查询
 * @author liangyl
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.out.useout.App', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '出库查询',
	header: false,
	border: false,
	layout: {
		type: 'border'
	},
	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	bodyPadding: 1,
	/**@description 新增/编辑/查看*/
	formtype: 'show',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.DocGrid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					me.isShow(record);
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					me.isShow(record);
				},null,500);
			},
			nodata:function(p){
				me.ShowPanel.clearData();
			}
		});
		
		me.DocGrid.onSearch();
	},
	initComponent: function() {
		var me = this;
		//自定义按钮功能栏
		me.dockedItems = me.createButtonUseOutItems();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.DocGrid = Ext.create('Shell.class.rea.client.out.useout.DocGrid', {
			header: false,
			title: '出库主单',
			itemId: 'DocGrid',
			region: 'west',
			width: 360,
			split: true,
			collapsible: true,
			collapseMode:'mini'	
		});
		me.ShowPanel = Ext.create('Shell.class.rea.client.out.useout.ShowPanel', {
			header: false,
			itemId: 'ShowPanel',
			region: 'center',
			border:false,
			collapsible: false,
			collapsed: false
		});
		var appInfos = [me.DocGrid, me.ShowPanel];
		return appInfos;
	},
	/**创建功能 按钮栏*/
	createButtonUseOutItems: function() {
		var me = this;
		var items = [];
		items.push({
			xtype: 'button',
			iconCls: 'button-check',
			itemId: "btnConfirm",
			text: '供应商确认',
			tooltip: '将当前选择订单供应商确认',
			handler: function() {
				me.onConfirmClick();
			}
		}, '-',{
			xtype: 'button',
			iconCls: 'button-cancel',
			itemId: "btnCancelConfirm",
			text: '取消确认',
			tooltip: '将已上传平台的订单取消确认',
			handler: function() {
				me.onCancelConfirmClick();
			}
		});
		// 新增用户出库单上传NC
		items.push('-',{
			text: '出库单上传至NC',
			tooltip: '出库单上传至NC',
			iconCls: 'button-config',
			handler: function() {
				me.outUpload();
			}
		});
		
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsUseToolbar',
			items: items
		});
	},
	/**@description 供应商确认按钮点击处理方法*/
	onConfirmClick: function() {
		var me = this;
		var records = me.DocGrid.getSelectionModel().getSelection();
		if (records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		//出库单的数据标志
		var IOFlag = records[0].get("ReaBmsOutDoc_IOFlag");
		//已上传,取消确认
		if (IOFlag != "3" && IOFlag != "6") {
			var IOFlagEnum = JShell.REA.StatusList.Status[me.DocGrid.IOFlagKey].Enum;
			var IOFlagName = "";
			if (IOFlagEnum)
				IOFlagName = IOFlagEnum[IOFlag];
			JShell.Msg.error("当前出库单数据标志为【" + IOFlagName + "】,不能操作!");
			return;
		}
		var Status = records[0].get("ReaBmsOutDoc_Status");
		me.onConfirmSave(records, 10, 3);
	},
	/**@description 供应商取消确认按钮点击处理方法*/
	onCancelConfirmClick: function() {
		var me = this;
		var records = me.DocGrid.getSelectionModel().getSelection();
		if (records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		//订单的数据标志
		var IOFlag = records[0].get("ReaBmsOutDoc_IOFlag");
		//已上传,取消确认
		if (IOFlag != "5" ) {
			var IOFlagEnum = JShell.REA.StatusList.Status[me.DocGrid.IOFlagKey].Enum;
			var IOFlagName = "";
			if (IOFlagEnum)
				IOFlagName = IOFlagEnum[IOFlag];
			JShell.Msg.error("当前出库单数据标志为【" + IOFlagName + "】,不能操作!");
			return;
		}
		var ThirdFlag=records[0].get("ReaBmsOutDoc_IsThirdFlag");
		if (ThirdFlag!=""&&ThirdFlag == "1") {
			JShell.Msg.error("同步成功!不能供应商取消确认操作!");
			return;
		}
		var Status = records[0].get("ReaBmsOutDoc_Status");
		if (Status != "10") {
			JShell.Msg.error("当前出库单不是供应商确认状态!不能供应商取消确认操作!");
			return;
		}
		me.onConfirmSave(records, 11, 4);
	},
	/**@description 保存处理方法*/
	onConfirmSave: function(records, status, ioFlag) {
		var me = this;

		var info = ioFlag == 3 ? "供应商确认" : "供应商取消确认";
		//处理意见
		JShell.Msg.confirm({
			title: '<div style="text-align:center;">' + info + '操作</div>',
			msg: '供货商备注',
			closable: false,
			multiline: true //多行输入框
		}, function(but, text) {
			if (but != "ok") return;
			var compMemo = text;
			if (compMemo) {
				compMemo = compMemo.replace(/\\/g, '&#92');
				compMemo = compMemo.replace(/[\r\n]/g, '<br />');
			}
			var id = records[0].get("ReaBmsOutDoc_Id");
			var SupplierConfirmID = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.USERID);
			var SupplierConfirmName = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.USERNAME);
			var Sysdate = JcallShell.System.Date.getDate();
			var DataAddTime = JcallShell.Date.toString(Sysdate);
			var entity = {
				"Id": id,
				"SupplierConfirmID": ioFlag == 3 ? SupplierConfirmID : "0",
				"SupplierConfirmName":ioFlag == 3 ? SupplierConfirmName:"",
				"StatusName": ioFlag == 3 ? "供应商确认":"取消确认",
				"Status": status,
				"IOFlag":ioFlag == 3 ? 5 : 6,
				"IsThirdFlag":0,
				"SupplierConfirmMemo": compMemo
			};
			var url = JShell.System.Path.ROOT + "/ReaSysManageService.svc/ST_UDTO_UpdateReaBmsOutDocByField";
			if( ioFlag == 3 && JShell.Date.toServerDate(DataAddTime)){
				entity.SupplierConfirmTime=JShell.Date.toServerDate(DataAddTime);
			}
			var params = {
				"entity": entity,
				"fields": "Id,SupplierConfirmID,SupplierConfirmName,SupplierConfirmTime,StatusName,Status,IOFlag,IsThirdFlag,SupplierConfirmMemo"
			};

			JShell.Server.post(url, JShell.JSON.encode(params), function(data) {
				if (data.success) {
					JShell.Msg.alert(info + '成功', null, 2000);
					me.DocGrid.onSearch();
				} else {
					JShell.Msg.error(info + '失败！' + data.msg);
				}
			});
		});
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
	setFormType: function(formtype) {
		var me = this;
		me.formtype = formtype;
		me.ShowPanel.formtype = formtype;
		me.ShowPanel.DocForm.formtype = formtype;
		me.ShowPanel.DtlPanel.formtype = formtype;
	},
	isShow: function(record) {
		var me = this;
		me.setFormType("show");
		me.ShowPanel.isShow(record, me.DocGrid);
	},
	/**新增用户出库单上传至NC*/
	outUpload: function() {
		var me = this;
		var records = me.DocGrid.getSelectionModel().getSelection();
		if (records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var Status = records[0].get("ReaBmsOutDoc_Status");
		if (Status != "10") {
			JShell.Msg.error("当前出库单非供应商确认状态!不能上传至NC操作!");
			return;
		}
		
		var id = records[0].get("ReaBmsOutDoc_Id");
		var url = JShell.System.Path.ROOT + "/ReaCustomInterface.svc/RS_SendOutInfoByInterface?outDocId=" + id;
		
		JShell.Server.get(url, function(data) {
			if (data.success) {
				JShell.Msg.alert('出库单上传至NC成功！', null, 1500);
				me.DocGrid.onSearch();
			} else {
				JShell.Msg.error('出库单上传至NC失败！' + data.msg);
			}
		});
	}
});