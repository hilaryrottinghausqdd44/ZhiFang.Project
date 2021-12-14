/**
 * 描述
 * @author liangyl
 * @version 2016-12-09
 */
Ext.define('Shell.class.weixin.item.product.apply.MemoForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.UEditor'
	],
	title: '描述',
		/**获取数据服务路径*/
	selectUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchOSRecommendationItemProductById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_AddOSRecommendationItemProduct',
	/**修改服务地址*/
    editUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSRecommendationItemProductByField',
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
	hasTemSave:false,
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
	},

	/**创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		var width = document.body.clientWidth;
		var height = document.body.clientHeight * 0.725;
		height = (height > 385 ? height : 385);

		items = [{
			name: 'OSRecommendationItemProduct_Memo',
			itemId: 'OSRecommendationItemProduct_Memo',
			margin: '0px 0px 0px 0px',
			xtype: 'ueditor',
			width: '100%',
			height: height,
			autoScroll: true,
			emptyText: '必填项',
			allowBlank: false,
			border: false
		},{
        	fieldLabel:'状态',name:'OSRecommendationItemProduct_Status',itemId:'OSRecommendationItemProduct_Status',hidden:true
        },{
			fieldLabel:'主键ID',name:'OSRecommendationItemProduct_Id',hidden:true
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
		JShell.System.ClassDict.init('ZhiFang.WeiXin.Entity','OSRecommendationItemProducStatus',function(){
			if(!JShell.System.ClassDict.OSRecommendationItemProducStatus){
    			JShell.Msg.error('未获取到特推项目产品状态，请刷新列表');
    			return;
    		}
			var info = JShell.System.ClassDict.getClassInfoByName('OSRecommendationItemProducStatus','待审核');
			me.getForm().setValues({
				OSRecommendationItemProduct_Status:info.Id
			});
        });

		if(values.OSRecommendationItemProduct_Id){
			me.onSaveClick();
		}
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
//		if(!JShell.System.Cookie.map.USERID){
//			JShell.Msg.error('用户登录信息不存在，请重新登录后操作！');
//			return;
//		}
			
		var entity = {
			Status:values.OSRecommendationItemProduct_Status,//状态
            Memo:values.OSRecommendationItemProduct_Memo
		};
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		
		var fields = ['Id','Memo','Status'];
		entity.fields = fields.join(',');
		
		entity.entity.Id = values.OSRecommendationItemProduct_Id;
		return entity;
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		return data;
	}
});