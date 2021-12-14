/**
 * 配置信息
 * @author liangyl
 * @version 2017-10-13
 */
Ext.define('Shell.class.rea.client.set.Form', {
	extend: 'Shell.ux.form.Panel',
	requires:[
		'Shell.ux.form.field.CheckTrigger',
	    'Shell.ux.form.field.BoolComboBox'
    ],
	title: '配置信息',
	
	width:240,
    height:210,

	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	
	/**内容周围距离*/
	bodyPadding:'10px 0px 0px 10px',
	/**布局方式*/
	layout:'anchor',
	/** 每个组件的默认属性*/
    defaults:{
    	anchor:'100%',
        labelWidth:65,
        labelAlign:'right'
    },
    /**列表store,用于判断平台数据是否已存在*/
	FieldNameStore:null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this,
			items = [];
	
		//平台字段名
		items.push({
			fieldLabel: '平台字段名',name: 'FieldName',
			itemId: 'FieldName',emptyText:'必填项',allowBlank:false
		});
		//Excel列名
		items.push({
			fieldLabel:'Excel列名',name:'ExcelFieldName',itemId:'ExcelFieldName'
		});
		//主键字段
		items.push({
			fieldLabel:'主键字段',name:'IsPrimaryKey'
		});
		//必填字段
		items.push({
			fieldLabel:'必填字段',name:'IsRequiredField'
		});
		//默认值
		items.push({
			fieldLabel:'默认值',name:'DefaultValue'
		});
		return items;
	},
	/**保存按钮点击处理方法*/
	onSaveClick:function(){
		var me = this;
			values = me.getForm().getValues();
		if(!me.getForm().isValid()) {
			me.fireEvent('isValid', me);
			return;
		}
		var record = me.FieldNameStore.findRecord('FieldName', values.FieldName);
        if(record){
        	JShell.Msg.error('该平台字段名已存在');
        	return;
        }
        
		var entity = {
			FieldName:values.FieldName,
			ExcelFieldName:values.ExcelFieldName,
			IsPrimaryKey:values.IsPrimaryKey,
			IsRequiredField:values.IsRequiredField,
			DefaultValue:values.DefaultValue
		}
		me.fireEvent('save',me,entity);
	},
	/**更改标题*/
	changeTitle:function(){
	}
});