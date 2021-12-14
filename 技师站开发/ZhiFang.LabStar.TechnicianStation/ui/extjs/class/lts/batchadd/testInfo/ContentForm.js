/**
 * 检验评语内容
 * @author liangyl
 * @version 2019-12-26
 */
Ext.define('Shell.class.lts.batchadd.testInfo.ContentForm', {
	extend:'Shell.ux.form.Panel',

    formtype:'add',
	title:'当前小组检验评语',
	layout: 'fit',
	hasButtontoolbar:false,
    //检验备注
    TestInfo:'',
    //当前小组评语类型
    TypeName:'',
    bodyPadding:'5px 5px 0px 5px',
	afterRender:function(){
		var me = this ;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		var items = [
		   {xtype: 'fieldset',title: '当前小组的'+me.TypeName,collapsible: true,
				defaultType: 'textfield',itemId: 'LisTestForm',
				layout: me.layout,
				items: [
			        {fieldLabel:'',name:'LBPhrase_CName',itemId:'LBPhrase_CName',xtype:'textarea',minHeight:40,value:me.TestInfo}
			    ]
			}
		];
		return items;
	},
		/**更改标题*/
	changeTitle:function(){
		var me = this;
	},
	setValues : function(obj){
		var me = this;
		me.getForm().setValues(obj);
	},
	getValues : function(){
		var me = this;
		return me.getForm().getValues();
	}
});