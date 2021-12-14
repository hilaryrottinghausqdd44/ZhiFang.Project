Ext.define('Shell.class.weixin.dict.lab.BLabSampleType.MsgFrom', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.BoolComboBox'
	],

	title: '确认提示信息',
	width: 350,
	height: 200,
	bodyPadding: 10,

	/**复制保存数据服务路径*/
	copyUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_AddLabSampleTypeCopy',    
      
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,

	/**布局方式*/
	layout: 'anchor',
	/**目标实验室值*/
	LabCodeList:[],
	/**选择的项目,要复制到实验室的项目*/
	ItemNoList:[],
    /**是否全部复制*/
	IsAll:false,
	defaultType:2,
	TypeList:[[0,'克隆复制'],[1,'覆盖复制'],[2,'差异复制']],
	ClienteleId:null,
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 60,
		labelAlign: 'right'
	},
	
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this;
        var items =[];
    	items.push({
			fieldLabel: '复制类型',
			name: 'copyType',
			itemId: 'copyType',
	        xtype: 'uxSimpleComboBox',
	        hasStyle: true,
			value: me.defaultType,
			data: me.TypeList,
			listeners: {
				change : function(com,newValue,oldValue,eOpts ){
					var lbtext='';
					switch (newValue){
						case 0:
						    lbtext='现有项目都会被删除,然后复制,并且操作不可逆';
							break;
						case 1:
						    lbtext='项目存在和不存在都会被复制,已存在的项目会被删除并替换为现复制的项目,并且操作不可逆';
							break;
						case 2:
						    lbtext='项目存在则跳过,不存在会被复制';
							break;
						default:
							break;
					}
					var Tag=me.getComponent('Tag');
					Tag.setText(lbtext);
				}
			}
		},{
	        xtype: 'label',
	        text: '项目存在则跳过,不存在会被复制',
	        itemId:'Tag', 
	        style: "font-weight:bold;color:red;",
	        margin: '0 0 5 75'
		});
		return items;
	},
	
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			copyType: values.copyType
		};
		return entity;
	},
	
	/**创建功能按钮栏*/
	createButtontoolbar:function(){
		var me = this,
			items = me.buttonToolbarItems || [];
		
		items.push('->','Accept');
		
		if(items.length == 0) return null;
		
		var hidden = me.openFormType && (me.formtype == 'show' ? true : false);
		
		return Ext.create('Shell.ux.toolbar.Button',{
			dock:me.buttonDock,
			itemId:'buttonsToolbar',
			items:items,
			hidden:hidden
		});
	},
	onAcceptClick:function(){
		var me =this;
		me.onSaveInfo();
	},
	/**复制保存*/
	onSaveInfo:function(){
		var me=this;
		var values = me.getForm().getValues();
        var params = Ext.JSON.encode({
			LabCodeList:me.LabCodeList,
        	ItemNoList:me.ItemNoList,
        	//是否全部复制
        	Isall:me.IsAll,
        	//复制类型
        	OverRideType:values.copyType,
        	originalLabCode:me.ClienteleId
		});
		
		if(!params) return;
		me.showMask(me.saveText); //显示遮罩层
		var url = (me.copyUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.copyUrl;
		JShell.Server.post(url, params, function(data) {
			me.hideMask(); //隐藏遮罩层
			if(data.success) {
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
			    me.fireEvent('save',me);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	}
});