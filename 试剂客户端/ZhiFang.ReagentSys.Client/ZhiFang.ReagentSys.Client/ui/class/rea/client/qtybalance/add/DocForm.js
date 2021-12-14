/**
 * 库存结转
 * @author longfc
 * @version 2018-04-13
 */
Ext.define('Shell.class.rea.client.qtybalance.add.DocForm', {
	extend: 'Shell.class.rea.client.qtybalance.basic.DocForm',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '库存结转信息',
	formtype: 'show',
	width: 680,
	height: 165,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsQtyBalanceDocById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ReaManageService.svc/RS_UDTO_AddReaBmsQtyBalanceDocOfQtyBalance',
	/**修改服务地址*/
	editUrl: '',
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	buttonDock: "top",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;

		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = me.buttonToolbarItems || [];
		if(items.length == 0) {
			if(me.hasSave) items.push("save");
			if(me.hasReset) items.push('reset');
		}
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: me.buttonDock,
			itemId: 'buttonsToolbar',
			items: items,
			hidden: true
		});
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		var Sysdate = JcallShell.System.Date.getDate();
		var curDateTime=JcallShell.Date.toString(Sysdate);
		var OperDate = me.getComponent('ReaBmsQtyBalanceDoc_OperDate');
		var DataAddTime = me.getComponent('ReaBmsQtyBalanceDoc_DataAddTime');

		OperDate.setValue(curDateTime);
		DataAddTime.setValue(curDateTime);

		var OperName = me.getComponent('ReaBmsQtyBalanceDoc_OperName');
		var CreaterName = me.getComponent('ReaBmsQtyBalanceDoc_CreaterName');
		OperName.setValue(userName);
		CreaterName.setValue(userName);
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			Id: -1,
			PreQtyBalanceDocNo: values.ReaBmsQtyBalanceDoc_PreQtyBalanceDocNo,
			QtyBalanceDocNo: values.ReaBmsQtyBalanceDoc_QtyBalanceDocNo,
			OperName: values.ReaBmsQtyBalanceDoc_OperName,
			CreaterName: values.ReaBmsQtyBalanceDoc_CreaterName
		};
		entity.Memo = values.ReaBmsQtyBalanceDoc_Memo.replace(/\\/g, '&#92');
		entity.Memo = entity.Memo.replace(/[\r\n]/g, '<br />');
		
		if(values.ReaBmsQtyBalanceDoc_PreQtyBalanceDocID) entity.PreQtyBalanceDocID = values.ReaBmsQtyBalanceDoc_PreQtyBalanceDocID;
		if(values.ReaBmsQtyBalanceDoc_PreBalanceDateTime) entity.PreBalanceDateTime = JShell.Date.toServerDate(values.ReaBmsQtyBalanceDoc_PreBalanceDateTime);
		
		if(values.ReaBmsQtyBalanceDoc_CreaterID) entity.CreaterID = values.ReaBmsQtyBalanceDoc_CreaterID;
		if(me.formtype!="add"&&values.ReaBmsQtyBalanceDoc_DataAddTime) entity.DataAddTime = JShell.Date.toServerDate(values.ReaBmsQtyBalanceDoc_DataAddTime);
		if(values.ReaBmsQtyBalanceDoc_OperDate) entity.OperDate = JShell.Date.toServerDate(values.ReaBmsQtyBalanceDoc_OperDate);
		return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();

		var fields = [
			'Id', 'OperName', 'OperDate', 'Memo'
		];
		entity.fields = fields.join(',');
		entity.entity.Id = values.ReaBmsQtyBalanceDoc_Id;
		return entity;
	},
	/**@description 新增生成库存结转判断*/
	onAddJudgment: function(callback) {
		var me = this;
		var url = JShell.System.Path.ROOT + "/ReaManageService.svc/RS_UDTO_GetJudgeISAddReaBmsQtyBalanceDoc?beginDate=&endDate=";
		JShell.Server.get(url, function(data) {
			data = Ext.JSON.decode(data.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, ''));
			if(data.success) {
				//当前月是否已存在库存结转,BoolFlag=true为存在				
				var isCover = data.BoolFlag;
				callback(isCover);
			} else {
				JShell.Msg.error(data.ErrorInfo);
			}
		}, null, null, true);
	},
	/**保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this;
		var values = me.getForm().getValues();
		if(!me.getForm().isValid()) return;
		var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();
		if(!params) {
			JShell.Msg.error("封装提交信息错!");
			return;
		}
		if(me.formtype == 'add') {
			//调用服务先判断
			me.onAddJudgment(function(isCover) {
				//是否需要弹出提示框
				if(isCover) {
					//处理意见
					JShell.Msg.confirm({
						title: '<div style="text-align:center;">库存结转新增提示</div>',
						msg: '当前月已经生成过库存结转单,是否继续将当月已生成的库存结转单禁用,并重新生成当月新的库存结转单？<br />点【确定】按钮,继续新增保存！<br />点【取消】按钮,不作保存操作,直接关闭退出。',
						closable: false,
						multiline: false
					}, function(but, text) {
						if(but != "ok") return;
						me.onAddSave(params, true);
					});
				} else {
					me.onAddSave(params, false);
				}
			});
		} else
			me.fireEvent('onEditClick', me, params);
	},
	/**@description 新增生成库存结*/
	onAddSave: function(params, isCover) {
		var me = this;
		var id = params.entity.Id;
		var params = {
			"isCover": isCover,
			"entity": params.entity,
			"beginDate": null,
			"endDate": null
		};
		params = JcallShell.JSON.encode(params);

		me.showMask(me.saveText); //显示遮罩层
		var url = me.addUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		JShell.Server.post(url, params, function(data) {
			me.hideMask(); //隐藏遮罩层
			if(data.success) {
				id = me.formtype == 'add' ? data.value.id : id;
				id += '';
				me.fireEvent('save', me, id);
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	}
});