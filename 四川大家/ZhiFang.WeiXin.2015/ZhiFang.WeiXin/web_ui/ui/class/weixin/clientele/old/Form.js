/**
 * 实验室表单
 * @author Jcall
 * @version 2016-12-27
 */
Ext.define('Shell.class.weixin.clientele.Form',{
    extend:'Shell.ux.form.Panel',
    requires:[
		'Shell.ux.form.field.CheckTrigger_NEW'
    ],
    
    title:'实验室表单',    
	bodyPadding:10,
    
    /**获取数据服务路径*/
    selectUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchCLIENTELEById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_AddCLIENTELE',
    /**修改服务地址*/
    editUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_UpdateCLIENTELEByField', 
	
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	
	/**布局方式*/
	layout:'anchor',
	/**每个组件的默认属性*/
    defaults:{
    	anchor:'100%',
        labelWidth:80,
        labelAlign:'right'
    },
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//表单监听
		me.initFromListeners();
	},
	
	/**@overwrite 创建内部组件*/
	createItems:function(){
		var me = this;
		
		var items = [
			{fieldLabel:'实验室名称',emptyText:'必填项',allowBlank:false,
				name:'CLIENTELE_CNAME',itemId:'CLIENTELE_CNAME'},
			{fieldLabel:'实验室简称',name:'CLIENTELE_ENAME'},
			{fieldLabel:'快捷码',name:'CLIENTELE_SHORTCODE'},
			{
				xtype: 'uxCheckTrigger', fieldLabel: '所属区域', name: 'CLIENTELE_AreaName', itemId: 'CLIENTELE_AreaName',
				className: 'Shell.class.weixin.clientele.dic.CheckGrid',//colspan:2,labelWidth:60,width:300,
				classConfig: {checkOne: true,autoSelect: true},
				anchor: '100%', allowBlank: false, 
				emptyText: '必选项',
				listeners: {
					check: function (p, record) {
						me.getComponent('CLIENTELE_AreaName').setValue(record ? record.get('ClientEleArea_AreaCName') : '');
						me.getComponent('CLIENTELE_AreaID').setValue(record ? record.get('ClientEleArea_Id') : '');
						p.close();
					},
					beforetriggerclick:function(p){
						console.log(me.getComponent('Shell.class.weixin.clientele.dic.CheckGrid'));
					}
				}
			}, {
					xtype: 'textfield',
					fieldLabel: '编号',
					name: 'CLIENTELE_AreaID',
					itemId: 'CLIENTELE_AreaID',
					hidden: false,
					labelWidth: 80,
					anchor: '90%'
			},
			{boxLabel:'是否使用',name:'CLIENTELE_ISUSE',
				xtype:'checkbox',checked:true
			},
			{fieldLabel:'主键ID',name:'CLIENTELE_Id',hidden:true}
		];
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			CNAME:values.CLIENTELE_CNAME,
			ENAME:values.CLIENTELE_ENAME,
			SHORTCODE:values.CLIENTELE_SHORTCODE,
			ISUSE:values.CLIENTELE_ISUSE ? 1 : 0
		};
		
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
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
		
		entity.entity.Id = values.CLIENTELE_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		return data;
	},
	/**初始化表单监听*/
	initFromListeners:function(){
		var me = this;
		
		var CLIENTELE_CNAME = me.getComponent('CLIENTELE_CNAME');
		CLIENTELE_CNAME.on({
			change:function(field,newValue,oldValue,eOpts){
				if(newValue != ""){
					JShell.Action.delay(function(){
						JShell.System.getPinYinZiTou(newValue,function(value){
							me.getForm().setValues({
								CLIENTELE_SHORTCODE:value
							});
						});
					},null,200);
				}else{
					me.getForm().setValues({
						CLIENTELE_SHORTCODE:""
					});
				}
			}
		});
	},
	/**根据主键ID加载数据*/
	load:function(id){
		var me = this,
			url = me.selectUrl,
			collapsed = me.getCollapsed();
			
		if(!id) return;
		
		me.PK = id;//面板主键
		
		//收缩的面板不加载数据,展开时再加载，避免加载无效数据
		if(collapsed){
			me.isCollapsed = true;
			return;
		}	
		
		me.showMask(me.loadingText);//显示遮罩层
		url = (url.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		url += (url.indexOf('?') == -1 ? "?" : "&" ) + "id=" + id;
		url += '&fields=' + me.getStoreFields().join(',');
		
		
		
		JShell.Server.get(url,function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				if(data.value){
					data.value = JShell.Server.Mapping(data.value);
					me.lastData = me.changeResult(data.value);
					console.log(data.value.CLIENTELE_AreaID);
					me.getComponent("CLIENTELE_AreaName").classConfig.externalWhere=" clientelearea.Id="+data.value.CLIENTELE_AreaID;
					me.getComponent('CLIENTELE_AreaName').onTriggerClick();					
	                me.getForm().setValues(data.value);
	            }
			}else{
				JShell.Msg.error(data.msg);
			}
			me.fireEvent('load',me,data);
		});
	}
});