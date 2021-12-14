/**
 * 学位维护
 * @author liangyl	
 * @version 2018-11-09
 */
Ext.define('Shell.class.sysbase.degree.Form', {
	extend:'Shell.ux.form.Panel',
		requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title:'学位维护',
	width:400,
	height:300,
	bodyPadding:'20px 10px',
	layout:'anchor',
	defaults:{
		anchor:'100%',
		labelWidth:80,
		labelAlign:'right'
	},
	autoScroll:true,
	
	//获取数据服务路径
    selectUrl:'/SingleTableService.svc/ST_UDTO_SearchBDegreeById?isPlanish=true',
	//新增服务地址
    addUrl:'/SingleTableService.svc/ST_UDTO_AddBDegree',
    //修改服务地址
    editUrl:'/SingleTableService.svc/ST_UDTO_UpdateBDegreeByField',
	
	//是否启用保存按钮
	hasSave:true,
	//是否重置按钮
	hasReset:true,
	
	afterRender:function(){
		var me = this ;
		me.callParent(arguments);
		//表单监听
		me.initFromListeners();
	},
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		var items = [
			{fieldLabel:'名称',name:'BDegree_Name',itemId:'BDegree_Name',
			emptyText:'必填项',allowBlank:false},
			{fieldLabel:'简称',name:'BDegree_SName'},
			{fieldLabel:'快捷码',name:'BDegree_Shortcode'},
			{fieldLabel:'拼音字头',name:'BDegree_PinYinZiTou'},
			{fieldLabel:'备注',name:'BDegree_Comment',xtype:'textarea'},
			{fieldLabel:'使用',name:'BDegree_IsUse',xtype:'checkbox',checked:true},
			{fieldLabel:'主键ID',name:'BDegree_Id',hidden:true}
		];
			
		return items;
	},
	//@overwrite 获取新增的数据
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			Name:values.BDegree_Name,
			SName:values.BDegree_SName,
			PinYinZiTou:values.BDegree_PinYinZiTou,
			Shortcode:values.BDegree_Shortcode,
			UseCode:values.BDegree_UseCode,
			Comment:values.BDegree_Comment,
			IsUse:values.BDegree_IsUse ? 1 : 0
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
		
		entity.entity.Id = values.BDegree_Id;
		return entity;
	},
	/**初始化表单监听*/
	initFromListeners: function() {
		var me = this;

		//拼音字头
		var CName = me.getComponent('BDegree_Name');
		CName.on({
			change: function(field, newValue, oldValue, eOpts) {
				if(newValue != "") {
					JShell.Action.delay(function() {
						JShell.System.getPinYinZiTou(newValue, function(value) {
							me.getForm().setValues({
								BDegree_Shortcode: value,
								BDegree_PinYinZiTou:value
							});
						});
					}, null, 200);
				} else {
					me.getForm().setValues({
						BDegree_Shortcode: "",
						BDegree_PinYinZiTou:""
					});
				}
			}
		});
	}
});