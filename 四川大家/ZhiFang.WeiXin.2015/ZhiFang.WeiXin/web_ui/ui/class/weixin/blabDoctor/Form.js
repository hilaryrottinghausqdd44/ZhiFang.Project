/**
 * 实验室医生维护
 * @author GHX
 * @version 2021-02-05
 */
Ext.define('Shell.class.weixin.blabDoctor.Form',{
    extend:'Shell.ux.form.Panel',
    requires:[
		'Shell.ux.form.field.CheckTrigger_NEW'
    ],
    
    title:'实验室医生信息',
    width:240,
	height:400,
	bodyPadding:10,
    
    /**获取数据服务路径*/
    selectUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBLabDoctorById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_AddBLabDoctor',
    /**修改服务地址*/
    editUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateBLabDoctorByField', 
	
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
			{fieldLabel:'主键ID',disabled:true,name:'BLabDoctor_Id',itemId:'BLabDoctor_Id',emptyText:'必填项',allowBlank:false},
			{fieldLabel:'名称',emptyText:'必填项',allowBlank:false,name:'BLabDoctor_CName',itemId:'BLabDoctor_CName'},
			{fieldLabel:'编码',emptyText:'必填项',allowBlank:false,name:'BLabDoctor_LabDoctorNo',itemId:'BLabDoctor_LabDoctorNo'},
			{fieldLabel:'快捷码',name:'BLabDoctor_ShortCode'},
			/* {fieldLabel:'排序',name:'BLabDoctor_DispOrder'}, */
			{fieldLabel:'实验室编码',itemId:'BLabDoctor_LabCode',allowBlank:false,name:'BLabDoctor_LabCode',emptyText:'必填项',disabled:true,hidden:false},
			{fieldLabel:'实验室',xtype: 'uxCheckTrigger',name:'BLabDoctor_LabName',
				itemId: 'BLabDoctor_LabName',				
				className: 'Shell.class.weixin.blabSampleType.ClientCheckGrid',
				listeners: {
					check: function(p, record) {
						me.getComponent('BLabDoctor_LabCode').setValue(record ? record.get('CLIENTELE_Id') : '');
			            me.getComponent('BLabDoctor_LabName').setValue(record ? record.get('CLIENTELE_CNAME') : '');
						p.close();
					}
				}
			},
			{boxLabel:'是否使用',name:'BLabDoctor_Visible',
				xtype:'checkbox',checked:true
			}
		];
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
		var BLabDoctor_LabCode = me.getComponent("BLabDoctor_LabCode").getValue();
		var entity = {
			Id:values.BLabDoctor_Id,
			CName:values.BLabDoctor_CName,
			ShortCode:values.BLabDoctor_ShortCode,
			//DispOrder:values.BLabDoctor_DispOrder,
			LabDoctorNo:values.BLabDoctor_LabDoctorNo,
			LabCode:BLabDoctor_LabCode,
			Visible:values.BLabDoctor_Visible ? 1 : 0
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
			if(fields[i] != "BLabDoctor_LabName"){
				var arr = fields[i].split('_');
				if(arr.length > 2) continue;
				fieldsArr.push(arr[1]);
			}
		}
		entity.fields = fieldsArr.join(',');
		
		entity.entity.Id = me.getComponent("BLabDoctor_Id").getValue();
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		return data;
	},
	/**初始化表单监听*/
	initFromListeners:function(){
		var me = this;
		
		var BLabDoctor_CName = me.getComponent('BLabDoctor_CName');
		BLabDoctor_CName.on({
			change:function(field,newValue,oldValue,eOpts){
				if(newValue != ""){
					JShell.Action.delay(function(){
						JShell.System.getPinYinZiTou(newValue,function(value){
							me.getForm().setValues({
								BLabDoctor_ShortCode:value
							});
						});
					},null,200);
				}else{
					me.getForm().setValues({
						BLabDoctor_ShortCode:""
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
					if(data.value.BLabDoctor_LabCode){
						var clenturl = JShell.System.Path.ROOT+'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchCLIENTELEById?isPlanish=true';
						clenturl +="&id="+data.value.BLabDoctor_LabCode+"&fields=CLIENTELE_CNAME";
						JShell.Server.get(clenturl,function(dataa){
							if(dataa.success){
								if(dataa.value){
									dataa.value = JShell.Server.Mapping(dataa.value);
									me.getComponent("BLabDoctor_LabName").setValue(dataa.value.CLIENTELE_CNAME);
								}else{
									me.getComponent("BLabDoctor_LabName").setValue("");
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
	/**保存按钮点击处理方法*/
	onSaveClick:function(){
		var me = this;
		
		if(!me.getForm().isValid()) return;
		
		var url = me.formtype == 'add' ? me.addUrl : me.editUrl;
		url = JShell.System.Path.getRootUrl(url);
		
		var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();
		
		if(!params) return;
		
		var id = params.entity.Id;
		
		var issave = true;
		if(me.formtype == 'add' && id){
			var sidurl = JShell.System.Path.ROOT+me.selectUrl;
			sidurl +="&id="+id+"&fields=BLabDoctor_Id";
			JShell.Server.get(sidurl,function(dataa){
				if(dataa.success){
					if(dataa.value){
						issave = false;
						JShell.Msg.error("ID重复请重新填写！");
						return;
					}
				}
			},false);
		};
		if(issave){
			params = Ext.JSON.encode(params);
			
			me.showMask(me.saveText);//显示遮罩层
			me.fireEvent('beforesave',me);
			JShell.Server.post(url,params,function(data){
				me.hideMask();//隐藏遮罩层
				if(data.success){
					id = me.formtype == 'add' ? data.value : id;
					if(Ext.typeOf(id) == 'object'){
						id = id.id;
					}
					
					if(me.isReturnData){
						me.fireEvent('save',me,me.returnData(id));
					}else{
						me.fireEvent('save',me,id);
					}
					
					if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,me.hideTimes);
				}else{
					me.fireEvent('saveerror',me);
					JShell.Msg.error(data.msg);
				}
			});
		}
	},
	isAdd:function(){
		var me = this;
		
		me.showButtonsToolbar(true);//显示功能按钮栏
		me.setReadOnly(false);
		me.getComponent("BLabDoctor_Id").setDisabled(false);
		me.formtype = 'add';
		me.PK = '';
		me.changeTitle();//标题更改
		me.enableControl();//启用所有的操作功能
		me.onResetClick();
	},
	isEdit:function(id){
		var me = this;
		
		me.showButtonsToolbar(true);//显示功能按钮栏
		me.setReadOnly(false);
		me.getComponent("BLabDoctor_Id").setDisabled(true);
		me.formtype = 'edit';
		me.changeTitle();//标题更改
		
		me.load(id);
	},
});