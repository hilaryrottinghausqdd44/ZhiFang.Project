/**
 * 订货方供货管理
 * @author longfc
 * @version 2018-05-08
 */
Ext.define('Shell.class.rea.client.reasale.lab.EditPanel', {
	extend: 'Shell.class.rea.client.reasale.basic.add.EditPanel',

	title: '供货信息',
	/**当前选择的主单Id*/
	PK: null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.DocForm.on({
			onSave: function(form, params) {
				me.onSaveClick(params);
			},
			//表单的供货商选择后联动供货明细列表
			setReaCompInfo: function(v, reaComp) {
				me.DtlPanel.setReaCompInfo(reaComp);
			},
			isEditAfter: function(form) {
				var bo = true;
				var bo = true;
				if(me.DtlPanel.DtlGrid.store.getCount() <= 0) bo = false;
				me.setCompanyNameReadOnly(bo);
			}
		});
		me.DtlPanel.DtlGrid.store.on({
			load: function(store, records, successful) {
				var bo = true;
				if(!store || store.getCount() <= 0) bo = false;
				me.setCompanyNameReadOnly(bo);
			}
		});
		me.DtlPanel.on({
			onLaunchFullScreen: function() {
				me.fireEvent('onLaunchFullScreen', me);
			},
			onExitFullScreen: function() {
				me.fireEvent('onExitFullScreen', me);
			},
			onAddAfter: function(grid) {
				me.setFromTotalPrice(grid);
				me.setCompanyNameReadOnly(true);
			},
			onDelAfter: function(grid) {
				me.setFromTotalPrice(grid);
				var bo = true;
				if(grid.store.getCount() <= 0) bo = false;
				me.setCompanyNameReadOnly(bo);
			},
			onEditAfter: function(grid) {
				JShell.Action.delay(function() {
					me.setFromTotalPrice(grid);
					me.setCompanyNameReadOnly(true);
				}, null, 500);
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;

		me.DocForm = Ext.create('Shell.class.rea.client.reasale.lab.add.DocForm', {
			header: false,
			itemId: 'DocForm',
			region: 'north',
			height: 195,
			split: true,
			collapsible: true,
			collapsed: false,
			animCollapse: false
		});
		me.DtlPanel = Ext.create('Shell.class.rea.client.reasale.lab.DtlPanel', {
			header: false,
			itemId: 'DtlPanel',
			region: 'center',
			split: true,
			collapsible: true,
			collapsed: false
		});
		var appInfos = [me.DtlPanel, me.DocForm];
		return appInfos;
	},
	/**@description 供货商是可编辑还是只读处理*/
	setCompanyNameReadOnly: function(bo) {
		var me = this;
		var com = me.DocForm.getComponent('ReaBmsCenSaleDoc_CompanyName');
		com.setReadOnly(bo);
	},
	/**@description 重新计算表单的总价格*/
	setFromTotalPrice: function(grid) {
		var me = this;
		var com = me.DocForm.getComponent('ReaBmsCenSaleDoc_TotalPrice');
		var totalPrice = me.calcTotalPrice(grid);
		if(totalPrice) totalPrice = parseFloat(totalPrice);
		com.setValue(totalPrice);
	},
	/**@description 保存按钮点击处理方法*/
	onSaveClick: function(params) {
		var me = this;
		if(me.PK) me.setFormType("edit");
		me.onSave(params);
	},
	/**@description 保存处理方法*/
	onSave: function(params, status) {
		var me = this;
		if(!params) {
			if(!me.DocForm.getForm().isValid()) {
				JShell.Msg.error("请输入供货单基本信息后再提交!");
				return;
			}
			var values = me.DocForm.getForm().getValues();
			params = me.DocForm.formtype == 'add' ? me.DocForm.getAddParams() : me.DocForm.getEditParams();
			if(!params) {
				JShell.Msg.error("封装提交信息错!");
				return;
			}
			if(status) params.entity.Status = status;
		}
		//取消提交,取消审核
		if(status && (status == "3" || status == "3")) {
			params.dtAddList = null;
			params.dtEditList = null;
		} else {
			var grid = me.DtlPanel.DtlGrid;
			if(grid.store.getCount() <= 0) {
				JShell.Msg.alert('请选择货品明细信息再操作!', null, 1000);
				return;
			}
			var totalGoodsQty = 0;
			grid.store.each(function(record) {
				var goodsQty = record.get("ReaBmsCenSaleDtl_GoodsQty");
				if(!goodsQty) goodsQty = 0;
				totalGoodsQty += parseFloat(goodsQty);
				if(totalGoodsQty > 0) return false;
			});
			if(totalGoodsQty == 0) {
				JShell.Msg.error("当前货品数量为0!不能保存!");
				return;
			}
			//确认提交的供货明细验证
			if(params.entity.Status == '2') {
				var isValid = grid.validToSave();
				if(isValid == false) return;
			}
			//需要保存主单及明细
			var dtlParams = grid.getSaveDtl();
			if(!dtlParams) return;

			params.entity.TotalPrice = dtlParams.entity.TotalPrice;
			params.dtAddList = dtlParams.dtAddList;
			if(me.formtype == "edit") {
				params.dtEditList = dtlParams.dtEditList;
				params.dtlFields = dtlParams.dtlFields;
			}
		}

		var id = params.entity.Id;
		var url = me.formtype == 'add' ? me.addUrl : me.editUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		JShell.Server.post(url, Ext.JSON.encode(params), function(data) {
			if(data.success) {
				id = me.formtype == 'add' ? data.value.id : id;
				id += '';
				//审核通过成功后继续生成供货明细条码信息
				if(status == "4") {
					me.fireEvent('createBarCode', me, id);
				} else {
					me.fireEvent('save', me, id);
					JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, 2000);
				}
			} else {
				JShell.Msg.error('保存失败！' + data.msg);
			}
		});
	}
});