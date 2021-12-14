/**
 * 点击同步货品弹出的form
 * @author lyj
 * @version 2021-07-13
 */
Ext.define('Shell.class.rea.client.goodsorglink.ReaGoodsSynFrom', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.picker.DateTime',
		'Shell.ux.form.field.DateTime',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '同步货品',
	width: 280,
	height: 160,
	/**同步货品服务*/
	synchGoodsUrl: '/ReaCustomInterface.svc/RS_GetReaGoodsByInterface',
	/**获取默认时间的服务*/
	getTsUrl:'/ReaManageService.svc/RS_UDTO_GetMaxTS',
	/**显示成功信息*/
	showSuccessInfo:false,
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否启用取消按钮*/
	hasCancel: true,
	formtype:'add',

	/**内容周围距离*/
	bodyPadding: 10,
	/**布局方式*/
	layout: 'anchor',
	/** 每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 77,
		labelAlign: 'right',
		width: 155
	},
	/**当前选择的机构Id*/
	ReaCenOrgId: null,

	afterRender: function() {
		var me = this;
		me.getTs();
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this;
		var items = [
		{
			fieldLabel: '选择的时间',
			xtype: 'datetimefield',
			readOnly:false,
			itemId: 'ts',
			name: 'ts',
			format: 'Y-m-d H:i:s',
			width: me.defaults.width * 1
		}];

		return items;
	},
	/**@overwrite 取消按钮点击处理方法*/
	onCancelClick: function() {
		this.close();
	},
	/**@overwrite 获取默认时间*/
	getTs: function(){
		var me = this;
		var url = JShell.System.Path.ROOT + me.getTsUrl ;
		JShell.Server.get(url, function(data) {
			if (data.success) {
				var value=data.value;
				me.getForm().setValues({"ts":new Date(Date.parse(value.replace(/-/g,"/")))});
			} else {
				JShell.Msg.error('获取默认时间失败！' + data.msg);
			}
		});
	},
	/**@overwrite 重写保存按钮*/
	onSaveClick: function(){
		var me = this;
		var values = me.getForm().getValues();
		var ts = values.ts; // 获取由手工输入的单号或者是扫码产生的单号
		if(!ts) {
			JShell.Msg.alert("请输入时间!", null, 2000);
			return;
		}
		var url = JShell.System.Path.ROOT + me.synchGoodsUrl + "?ts="+ts+"&supplierId="+me.ReaCenOrgId;
		JShell.Server.get(url, function(data) {
			if (data.success) {
				me.fireEvent('saveSyn', me);
				JShell.Msg.alert('同步货品成功！', null, 1500);
			} else {
				JShell.Msg.error('同步货品失败！' + data.msg);
			}
		});

	}
	
});