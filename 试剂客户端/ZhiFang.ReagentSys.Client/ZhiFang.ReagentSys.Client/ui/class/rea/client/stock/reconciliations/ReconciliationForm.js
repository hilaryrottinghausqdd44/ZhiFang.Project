/**
 * 对帐
 * @author liangyl
 * @version 2018-10-23
 */
Ext.define('Shell.class.rea.client.stock.reconciliations.ReconciliationForm', {
	extend: 'Shell.ux.form.Panel',
	requires:[
		'Shell.ux.form.field.SimpleComboBox'
    ],
	title: '对帐',
	//获取数据服务路径
    selectUrl:'/ReaSysManageService.svc/ST_UDTO_SearchReaBmsInDocById?isPlanish=true',
	//新增服务地址
    addUrl:'/ReaSysManageService.svc/ST_UDTO_AddReaBmsInDoc',
    //修改服务地址
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaBmsInDocByField',
	
	width:240,
    height:140,

	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	bodyPadding:'20px 10px',
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
	/**入库对帐标志Key*/
	ReaBmsInDocReconciliationMark: "ReaBmsInDocReconciliationMark",
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		JShell.REA.StatusList.getStatusList(me.ReaBmsInDocReconciliationMark, false, false, null);
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this,
			items = [];
			var tempStatus = me.removeSomeStatusList();
		//对帐标志
		items.push({
			fieldLabel: '对帐状态',
			name: 'ReaBmsInDoc_ReconciliationMark',
			itemId: 'ReaBmsInDoc_ReconciliationMark',
			xtype: 'uxSimpleComboBox',hasStyle: true,labelWidth: 60,
			labelAlign: 'right',emptyText:'必填项',allowBlank:false,
			data:tempStatus
		});
		//主键
		items.push({
			fieldLabel:'主键',name:'ReaBmsInDoc_Id',hidden:true
		});
		return items;
	},
	/**状态查询选择项过滤*/
	removeSomeStatusList: function() {
		var me = this;
		var tempList = JShell.JSON.decode(JShell.JSON.encode(JShell.REA.StatusList.Status[me.ReaBmsInDocReconciliationMark].List));
		var removeArr = [];
	   for(var i = 0; i<tempList.length; i++){
	   	   if(tempList[i]){
	   	   		if(!tempList[i][0]){
	   	   			removeArr.push(tempList[i]);
	   	   		}
	   	   }
	    }
		Ext.Array.each(removeArr, function(name, index, countriesItSelf) {
			Ext.Array.remove(tempList, removeArr[index]);
		});
		return tempList;
	},
	/**更改标题*/
	changeTitle:function(){
	},
	//@overwrite 获取新增的数据
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			ReconciliationMark:values.ReaBmsInDoc_ReconciliationMark
		};
		return {entity:entity};
	},
	//@overwrite 获取修改的数据
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			fields = me.getStoreFields(),
			entity = me.getAddParams(),
			fieldsArr = [];
		
		for(var i in fields){
			var arr = fields[i].split('_');
			if(arr.length > 2) continue;
			fieldsArr.push(arr[1]);
		}
		entity.fields = fieldsArr.join(',');
		
		entity.entity.Id = values.ReaBmsInDoc_Id;
		return entity;
	}
	
});