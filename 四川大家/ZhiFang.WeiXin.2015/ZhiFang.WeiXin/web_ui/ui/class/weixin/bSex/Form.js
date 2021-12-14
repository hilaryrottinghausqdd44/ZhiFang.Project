/**
 * 实验室性别维护
 * @author GHX
 * @version 2021-02-03
 */
Ext.define('Shell.class.weixin.bSex.Form',{
    extend:'Shell.ux.form.Panel',
    requires:[
		'Shell.ux.form.field.CheckTrigger_NEW'
    ],
    
    title:'性别信息',
    width:240,
	height:400,
	bodyPadding:10,
    
    /**获取数据服务路径*/
    selectUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBSexById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_AddBSex',
    /**修改服务地址*/
    editUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateBSexByField', 
	
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	
	/**布局方式*/
	layout:'anchor',
	/**每个组件的默认属性*/
    defaults:{
    	anchor:'100%',
        labelWidth:60,
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
			{fieldLabel:'名称',emptyText:'必填项',allowBlank:false,
				name:'BSex_Name',itemId:'BSex_Name'},		
			{fieldLabel:'简称',name:'BSex_SName'},
			{fieldLabel:'快捷码',name:'BSex_Shortcode'},
			{fieldLabel:'拼音字头',name:'BSex_PinYinZiTou'},
			{fieldLabel:'实验室编码',itemId:'BSex_LabID',name:'BSex_LabID',disabled:true,hidden:true},
			{fieldLabel:'实验室',xtype: 'uxCheckTrigger',name:'BSex_LabName',
				itemId: 'BSex_LabName',				
				className: 'Shell.class.weixin.blabSampleType.ClientCheckGrid',
				listeners: {
					check: function(p, record) {
						me.getComponent('BSex_LabID').setValue(record ? record.get('CLIENTELE_Id') : '');
			            me.getComponent('BSex_LabName').setValue(record ? record.get('CLIENTELE_CNAME') : '');
						p.close();
					}
				}
			},
			{fieldLabel:'备注',height:85,labelAlign:'top',
				name:'BSex_Comment',xtype:'textarea'
			},
			{boxLabel:'是否使用',name:'BSex_IsUse',
				xtype:'checkbox',checked:true
			},
			{fieldLabel:'主键ID',name:'BSex_Id',hidden:true}
		];
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
		var LabID = me.getComponent("BSex_LabID").getValue();
		if(!LabID){
			LabID = 0;
		}
		var entity = {
			Name:values.BSex_Name,
			SName:values.BSex_SName,
			Shortcode:values.BSex_Shortcode,
			PinYinZiTou:values.BSex_PinYinZiTou,
			Comment:values.BSex_Comment,
			LabID:LabID,
			IsUse:values.BSex_IsUse ? 1 : 0
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
			if(fields[i] != "BSex_LabName"){
				var arr = fields[i].split('_');
				if(arr.length > 2) continue;
				fieldsArr.push(arr[1]);
			}
						
		}
		entity.fields = fieldsArr.join(',');
		
		entity.entity.Id = values.BSex_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		return data;
	},
	/**初始化表单监听*/
	initFromListeners:function(){
		var me = this;
		
		var BSex_Name = me.getComponent('BSex_Name');
		BSex_Name.on({
			change:function(field,newValue,oldValue,eOpts){
				if(newValue != ""){
					JShell.Action.delay(function(){
						JShell.System.getPinYinZiTou(newValue,function(value){
							me.getForm().setValues({
								BSex_PinYinZiTou:value,
								BSex_Shortcode:value
							});
						});
					},null,200);
				}else{
					me.getForm().setValues({
						BSex_PinYinZiTou:"",
						BSex_Shortcode:""
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
					if(data.value.BSex_LabID){
						var clenturl = JShell.System.Path.ROOT+'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchCLIENTELEById?isPlanish=true';
						clenturl +="&id="+data.value.BSex_LabID+"&fields=CLIENTELE_CNAME";
						JShell.Server.get(clenturl,function(dataa){
							if(dataa.success){
								if(dataa.value){
									dataa.value = JShell.Server.Mapping(dataa.value);
									me.getComponent("BSex_LabName").setValue(dataa.value.CLIENTELE_CNAME);
								}else{
									me.getComponent("BSex_LabName").setValue("");
								}
							}
						},false);
					}
					me.lastData = me.changeResult(data.value);
	                me.getForm().setValues(data.value);
	            }
			}else{
				JShell.Msg.error(data.msg);
			}
			me.fireEvent('load',me,data);
		});
	},
});