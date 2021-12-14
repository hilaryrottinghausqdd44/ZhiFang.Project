/**
 * 新增特推项目产品
 * @author liangyl	
 * @version 2016-12-28
 */
Ext.define('Shell.class.weixin.item.product.apply.AddPanel',{
	extend: 'Ext.tab.Panel',
	title: '特推项目产品',

	requires:['Shell.ux.toolbar.Button'],
	
	width: 500,
	height: 350,

	autoScroll: false,

	/**ID*/
	PK: null,
	AreaID:null	,
	/**区域*/
	ClientNo:null,
	formtype:'add',
	/**保存数据提示*/
	saveText: JShell.Server.SAVE_TEXT,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.Form.on({
			isValid:function(p){
				me.setActiveTab(me.Form);
			},
			beforesave: function(p) {
				me.showMask(me.saveText);
			},
			save: function(p, id) {
				me.hideMask();
				me.PK = id;  
				var Image=p.getComponent('OSRecommendationItemProduct_Image');
				if(Image.getValue()){
					p.onSaveImage(Image,id,'RecommendationItemProduct');
				}
				if(p.AreaID)me.AreaID=p.AreaID;
				if(p.ClientNo)me.ClientNo=p.ClientNo;
				me.fireEvent('save', me);
			},
			saveerror:function(p){
				me.hideMask();
			}
		});
	},
	
	initComponent: function() {
		var me = this;
		me.addEvents('save');
		//内部组件
		me.items = me.createItems();
		//创建挂靠功能栏
		me.dockedItems = me.createDockedItems();

		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this;
		me.Form = Ext.create('Shell.class.weixin.item.product.apply.Form', {
			formtype:me.formtype,
			hasLoadMask: false,//开启加载数据遮罩层
			title: '特推项目',
			PK:me.PK,
			AreaID:me.AreaID,
			/**名称*/
			AreaName:me.AreaName,
			ClientNo:me.ClientNo,
			hasButtontoolbar:false//带功能按钮栏
		});
		me.MemoForm = Ext.create('Shell.class.weixin.item.product.apply.MemoForm', {
			title: '描述',
			header: false,
			formtype: me.formtype,
			layout: 'fit',
			itemId: 'MemoForm',
			border: false,
			PK:me.PK,
			hasButtontoolbar:false//带功能按钮栏
		});
		return [me.Form,me.MemoForm];
	},
	/**创建挂靠功能栏*/
	createDockedItems:function(){
		var me = this;
		var dockedItems = {
			xtype:'uxButtontoolbar',
			dock:'bottom',
			itemId:'buttonsToolbar',
			items:['->',{
				text:'提交',
				iconCls:'button-save',
				tooltip:'提交',
				handler:function(){
					me.onSave(true);
				}
			},'reset']
		};
		return dockedItems;
	},
	/**保存按钮点击处理方法*/
	onSave:function(isSubmit){
		var me = this,
			values = me.Form.getForm().getValues();
		if(me.Form.formtype=='add'){
			var Image=me.Form.getComponent('OSRecommendationItemProduct_Image');
//			if(!Image.getValue()){
//				Image.allowBlank=false;
//			}
		}
		if(!me.Form.getForm().isValid()){
			me.Form.fireEvent('isValid',me);
			return;
		}
		var contentvalues = me.MemoForm.getForm().getValues();
		JShell.System.ClassDict.init('ZhiFang.WeiXin.Entity','OSRecommendationItemProducStatus',function(){
			if(!JShell.System.ClassDict.OSRecommendationItemProducStatus){
    			JShell.Msg.error('未获取到特推项目产品状态，请刷新列表');
    			return;
    		}

            var info = JShell.System.ClassDict.getClassInfoByName('OSRecommendationItemProducStatus','已审核');
			me.Form.getForm().setValues({
				OSRecommendationItemProduct_Status:info.Id,
				OSRecommendationItemProduct_Memo:contentvalues.OSRecommendationItemProduct_Memo.replace(/\\/g, '&#92')
			});
            me.Form.onSaveClick();
    	});
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		me.body.mask(text); //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.body) {
			me.body.unmask();
		} //隐藏遮罩层
	}
});