/**
 * 可选角色
 * @author liangyl	
 * @version 2018-05-14
 */
Ext.define('Shell.class.rea.center.register.RoleForm', {
	extend: 'Shell.ux.form.Panel',
	requires:[
		'Shell.ux.form.field.CheckTrigger',
	    'Shell.ux.form.field.BoolComboBox'
    ],
	title: '可选角色',
	
	width:220,
    height:300,
	margin: '0 0 1 0',
	 /**获取数据服务路径*/
    selectUrl:'/ReaSysManageService.svc/ST_UDTO_SearchCenOrgById?isPlanish=true',
 
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	
	/**内容周围距离*/
	bodyPadding:'10px 10px 0 10px',
	/**布局方式*/
	layout:'anchor',
	/** 每个组件的默认属性*/
    defaults:{
    	anchor:'100%',
        labelWidth:80,
        labelAlign:'right'
    },
    /***表单的默认状态,add(新增)edit(修改)show(查看)*/
	formtype:'add',
    buttonDock:'top',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
		me.buttonToolbarItems =[{
			xtype: 'label',text: '可选角色',
			margin: '0 0 8 5',style: "font-weight:bold;color:blue;",
			itemId: 'RoleInfo',name: 'RoleInfo'
		}];
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this,
			items = [];
		//正式版
		items.push({
			xtype:'checkboxfield',boxLabel  : '正式版',itemId:'cb1',name : 'cb1',
            inputValue: 'false', checked  : false
		});
		//精简版
		items.push({
			boxLabel  : '精简版',itemId:'cb2',name : 'cb2',
            xtype:'checkboxfield',inputValue: 'false', checked  : false
		});
		//临时授权版
		items.push({
			boxLabel  : '临时授权版',itemId:'cb3',name : 'cb3',
            xtype:'checkboxfield', inputValue: 'false', checked  : false
		});
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
		var entity = {
		
		};
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			fields = [
				'Id','CName'
			],
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		
		entity.fields = fields.join(',');
		
//		entity.entity.Id = values.CenOrg_Id;
		return entity;
	},

	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		//只允许单选操作
		var cb1 = me.getComponent('cb1');
		var cb2 = me.getComponent('cb2');
		var cb3 = me.getComponent('cb3');
        cb1.on({
        	change:function(com,  newValue,  oldValue,  eOpts){
				if(newValue==true){
					cb2.setValue(false);
					cb3.setValue(false);
				}
			}
        });
        cb2.on({
        	change:function(com,  newValue,  oldValue,  eOpts){
				if(newValue==true){
					cb1.setValue(false);
					cb3.setValue(false);
				}
			}
        });
        cb3.on({
        	change:function(com,  newValue,  oldValue,  eOpts){
				if(newValue==true){
					cb1.setValue(false);
					cb2.setValue(false);
				}
			}
        });
	}
});