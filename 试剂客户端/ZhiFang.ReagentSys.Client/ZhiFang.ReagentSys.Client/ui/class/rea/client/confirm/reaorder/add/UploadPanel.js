/**
 * 客户端验收
 * @author longfc
 * @version 2017-12-15
 */
Ext.define('Shell.class.rea.client.confirm.reaorder.add.UploadPanel', {
	extend: 'Ext.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],

	title: '供货订单导入',
	width: 400,
	height: 150,
	
	/**显示成功信息*/
	showSuccessInfo: true,
	/**导入类型*/
	formType: '',
	bodyPadding: 10,
	layout: 'anchor',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 60,
		labelAlign: 'right',
	},

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('save');
		me.items = me.items || me.createItems();
		me.dockedItems = me.dockedItems || me.createDockedItems();
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this,
			items = [{
				xtype: 'textfield',
				fieldLabel: '订货明细字段',
				hidden: true,
				name: 'dtlfields',
				itemId: 'dtlfields',
				readOnly: true,
				locked: true,
				value: "ReaOrderDtlOfConfirmVO_BarCodeType,ReaOrderDtlOfConfirmVO_ReaGoodsName,ReaOrderDtlOfConfirmVO_DtlGoodsQty,ReaOrderDtlOfConfirmVO_GoodsQty,ReaOrderDtlOfConfirmVO_ReceivedCount,ReaOrderDtlOfConfirmVO_RejectedCount,ReaOrderDtlOfConfirmVO_Price,ReaOrderDtlOfConfirmVO_SumTotal,ReaOrderDtlOfConfirmVO_GoodsUnit,ReaOrderDtlOfConfirmVO_UnitMemo,ReaOrderDtlOfConfirmVO_BiddingNo,ReaOrderDtlOfConfirmVO_Id,ReaOrderDtlOfConfirmVO_ReaGoodsID,ReaOrderDtlOfConfirmVO_LabcGoodsLinkID,ReaOrderDtlOfConfirmVO_CompGoodsLinkID,ReaOrderDtlOfConfirmVO_LotSerial,ReaOrderDtlOfConfirmVO_LotNo,ReaOrderDtlOfConfirmVO_ReaGoods_ApproveDocNo,ReaOrderDtlOfConfirmVO_ReaGoods_RegistNo,ReaOrderDtlOfConfirmVO_ReaGoods_RegistDate,ReaOrderDtlOfConfirmVO_ReaGoods_RegistNoInvalidDate,ReaOrderDtlOfConfirmVO_ConfirmCount,ReaOrderDtlOfConfirmVO_AcceptFlag,ReaOrderDtlOfConfirmVO_OrderDocID,ReaOrderDtlOfConfirmVO_OrderDocNo,ReaOrderDtlOfConfirmVO_ReaBmsCenSaleDtlConfirmLinkVOListStr,ReaOrderDtlOfConfirmVO_ReaGoods_EName,ReaOrderDtlOfConfirmVO_ReaGoods_SName,ReaOrderDtlOfConfirmVO_ReaGoodsNo,ReaOrderDtlOfConfirmVO_ProdGoodsNo,ReaOrderDtlOfConfirmVO_CenOrgGoodsNo,ReaOrderDtlOfConfirmVO_GoodsNo,ReaOrderDtlOfConfirmVO_ReaGoods_GoodsSort,ReaOrderDtlOfConfirmVO_ProdDate,ReaOrderDtlOfConfirmVO_InvalidDate,ReaOrderDtlOfConfirmVO_RegisterNo,ReaOrderDtlOfConfirmVO_StorageType,ReaOrderDtlOfConfirmVO_SaleGoodsQty,ReaOrderDtlOfConfirmVO_ReaCompID,ReaOrderDtlOfConfirmVO_CompanyName,ReaOrderDtlOfConfirmVO_ReaServerCompCode,ReaOrderDtlOfConfirmVO_ReaCompCode"
			}];
		//文件
		items.push({
			xtype: 'filefield',
			allowBlank: false,
			emptyText: 'Excel格式文件',
			buttonConfig: {
				iconCls: 'button-search',
				text: '选择'
			},
			name: 'file',
			itemId: 'file',
			fieldLabel: '供货文件',
			validator: function(value) {
				if(!value) {
					return true;
				} else {
					var arr = value.split('.');
					var fileValue = arr[arr.length - 1].toString().toLowerCase();
					if(fileValue && (fileValue != 'xlsx' && fileValue != 'xls')) {
						return '只能上传Excel格式文件';
					} else {
						return true;
					}
				}
			}
		});
		return items;
	},
	/**创建挂靠*/
	createDockedItems: function() {
		var me = this,
			dockedItems = [];
		dockedItems.push(Ext.create('Shell.ux.toolbar.Button', {
			dock: 'bottom',
			items: ['->', {
				iconCls: 'file-excel',
				name: "btnAccept",
				itemId: "btnAccept",
				text: '确定',
				handler: function() {
					me.onAcceptClick();
				}
			}, 'cancel']
		}));
		return dockedItems;
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
	/**点击取消按钮处理*/
	onCancelClick: function() {
		this.close();
	},
	/**点击确定按钮处理*/
	onAcceptClick: function() {
		var me = this;
		if(!me.getForm().isValid()) return;

		me.showMask();
		me.getForm().submit({
			url: JShell.System.Path.ROOT + '/ReaManageService.svc/RS_UDTO_UploadSupplyReaOrderDataByExcel',
			waitMsg: JShell.Server.SAVE_TEXT,
			method: 'POST',
			success: function(form, action) {
				me.hideMask();
				var msg = action.result.ErrorInfo;
				if(!action.result.success) {
					var msg = action.result.ErrorInfo;
					JShell.Msg.error(msg);
				}
				//console.log(action.result);
				me.fireEvent('save', me, action.result);
			},
			failure: function(form, action) {
				me.hideMask();
				var msg = action.result.ErrorInfo;
				msg = msg ? msg : '文件上传失败！';
				JShell.Msg.error(msg);
				me.fireEvent('save', me, action.result);
			}
		});
	}
});