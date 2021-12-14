/**
 * 采购说明HTML
 * @author liangyl
 * @version 2017-08-07
 */
Ext.define('Shell.class.wfm.business.projecttrack.follow.apply.PurchaseDescForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.UEditor'
	],
	title: '采购说明',
	/**获取数据服务路径*/
    selectUrl:'/SingleTableService.svc/ST_UDTO_SearchPContractFollowById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/SingleTableService.svc/ST_UDTO_AddPContractFollow',
    /**修改服务地址*/
    editUrl:'/SingleTableService.svc/ST_UDTO_UpdatePContractFollowStatus',
	PK:null,
	width: 1366,
	height: 400,
	formtype: "add",
    /**显示成功信息*/
	showSuccessInfo: true,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	/**开启加载数据遮罩层*/
	hasLoadMask: false,
	autoScroll: false,
	layout: 'fit',
	bodyPadding: '0px 0px 1px 0px',
	/**是否启用保存按钮*/
	hasSave:true,
    /**是否重置按钮*/
	hasReset:true,
	 /**是否暂存按钮*/
	hasTemSave:true,
	/**是否启用取消按钮*/
	hasCancel:true,
	initComponent: function() {
    	var me = this;
		me.buttonToolbarItems = ['->'];
		if(me.hasTemSave) {
			me.buttonToolbarItems.push({
				text: '暂存',
				iconCls: 'button-save',
				tooltip: '暂存',
				handler: function() {
					me.onSave(false);
				}
			});
		}
		if(me.hasSave) {
			me.buttonToolbarItems.push({
				text: '提交',
				iconCls: 'button-save',
				tooltip: '提交',
				handler: function() {
					me.onSave(true);
				}
			});
		}
		if(me.hasReset) {
			me.buttonToolbarItems.push('reset');
		}
		me.callParent(arguments);
	},
	/**重写渲染完毕执行*/
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		if(me.formtype=='add'){
			var PurchaseDescHTML=me.getComponent('PContractFollow_PurchaseDescHTML');
			var value='<table heihgt=""data-sort="sortDisabled"width="586"height="166"cellspacing="2"cellpadding="3"bordercolor="#000000"border="1"bgcolor="#ffffff"><tbody><tr class="firstRow"><td rowspan="2"colspan="1"><span style="font-family:黑体size=3">硬件采购</span></td><td><span style="font-family:黑体size=3">采购总额</span></td><td><span style="font-family:黑体size=3">物品名称</span></td><td><span style="font-family:黑体size=3">单价</span></td><td><span style="font-family:黑体size=3">数量</span></td></tr><tr><td></td><td></td><td></td><td></td></tr><tr><td rowspan="2"colspan="1"><span style="font-family:黑体size=3">软件采购</span></td><td><span style="font-family:黑体size=3">采购总额</span></td><td><span style="font-family:黑体size=3">物品名称</span></td><td><span style="font-family:黑体size=3">单价</span></td><td><span style="font-family:黑体size=3">数量</span></td></tr><tr><td></td><td></td><td></td><td></td></tr></tbody></table>';
			PurchaseDescHTML.setValue(value);
		}
	},

	/**创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		var width = document.body.clientWidth;
		var height = document.body.clientHeight * 0.725;
		height = (height > 385 ? height : 385);

		items = [{
			name: 'PContractFollow_PurchaseDescHTML',
			itemId: 'PContractFollow_PurchaseDescHTML',
			margin: '0px 0px 0px 0px',
			xtype: 'ueditor',
			width: '100%',
			height: height,
			autoScroll: true,
			border: false
		},{
        	fieldLabel:'状态',name:'PContractFollow_ContractStatus',itemId:'PContractFollow_ContractStatus',hidden:true
        },{
			fieldLabel:'主键ID',name:'PContractFollow_Id',hidden:true
		}];
		return items;
	},
	/**更改标题*/
	changeTitle: function() {
		var me = this;
	},
		/**保存按钮点击处理方法*/
	onSave:function(isSubmit){
		var me = this,
			values = me.getForm().getValues();
		
		if(!me.getForm().isValid()){
			me.fireEvent('isValid',me);
			return;
		}
		
		JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage','PContractFollowStatus',function(){
			if(!JShell.System.ClassDict.PContractFollowStatus){
    			JShell.Msg.error('未获取到项目跟踪状态，请重新保存');
    			return;
    		}
			if(isSubmit){//提交
    			var info = JShell.System.ClassDict.getClassInfoByName('PContractFollowStatus','申请');
				me.getForm().setValues({
					PContractFollow_ContractStatus:info.Id
				});
			}else{//暂存
				var info = JShell.System.ClassDict.getClassInfoByName('PContractFollowStatus','暂存');
				me.getForm().setValues({
					PContractFollow_ContractStatus:info.Id
				});
			}
			
			//新增时需要校验项目跟踪编号是否不重复
			if(values.PContractFollow_Id){
				me.onSaveClick();
			}
    	});
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		if(!JShell.System.Cookie.map.USERID){
			JShell.Msg.error('用户登录信息不存在，请重新登录后操作！');
			return;
		}
			
		var entity = {
			ContractStatus:values.PContractFollow_ContractStatus,//状态
            PurchaseDescHTML:values.PContractFollow_PurchaseDescHTML
		};
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		
		var fields = ['Id','ContractStatus','PurchaseDescHTML'];
		entity.fields = fields.join(',');
		
		entity.entity.Id = values.PContractFollow_Id;
		return entity;
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		var value='<table heihgt=""data-sort="sortDisabled"width="586"height="166"cellspacing="2"cellpadding="3"bordercolor="#000000"border="1"bgcolor="#ffffff"><tbody><tr class="firstRow"><td rowspan="2"colspan="1"><span style="font-family:黑体size=3">硬件采购</span></td><td><span style="font-family:黑体size=3">采购总额</span></td><td><span style="font-family:黑体size=3">物品名称</span></td><td><span style="font-family:黑体size=3">单价</span></td><td><span style="font-family:黑体size=3">数量</span></td></tr><tr><td></td><td></td><td></td><td></td></tr><tr><td rowspan="2"colspan="1"><span style="font-family:黑体size=3">软件采购</span></td><td><span style="font-family:黑体size=3">采购总额</span></td><td><span style="font-family:黑体size=3">物品名称</span></td><td><span style="font-family:黑体size=3">单价</span></td><td><span style="font-family:黑体size=3">数量</span></td></tr><tr><td></td><td></td><td></td><td></td></tr></tbody></table>';
		if(!data.PContractFollow_PurchaseDescHTML){
			data.PContractFollow_PurchaseDescHTML = value;
		}
		return data;
	}
});