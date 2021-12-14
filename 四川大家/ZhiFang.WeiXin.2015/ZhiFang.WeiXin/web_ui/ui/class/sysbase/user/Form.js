/**
 * 员工信息
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.user.Form',{
	extend:'Shell.ux.form.Panel',
	requires:[
		'Shell.ux.form.field.CheckTrigger',
	    'Shell.ux.form.field.SimpleComboBox'
    ],
    
    title:'员工信息',
    
    /**获取数据服务路径*/
    selectUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHREmployeeById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_AddHREmployee',
    /**修改服务地址*/
    editUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_UpdateHREmployeeByField',
    
    /**获取用户账号列表服务*/
    getUserListUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACUserListByHQL?&isPlanish=true',
    
    bodyPadding:10,
    /**布局方式*/
	layout:'anchor',
	/**每个组件的默认属性*/
    defaults:{
    	anchor:'100%',
        labelWidth:60,
        labelAlign:'right'
    },
	/**启用表单状态初始化*/
	openFormType:true,
	/**显示成功信息*/
	showSuccessInfo:true,
	/**保存后返回表单内容数据,*/
	isReturnData:true,
	
	/**是否存在上级*/
	hasManager:true,
    
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
		//拼音字头监听
		me.initPinYinZiTouListeners();
		
		me.on({
			load:function(){
				me.initAccountButton();
			}
		});
	},
	initComponent:function(){
		var me = this;
		
		me.buttonToolbarItems = [{
			text:'新增账号',
			iconCls:'button-add',
			itemId:'AccountAdd',
			tooltip:'<b>新增账号</b>',
			hidden:true,
			handler:function(){me.onAccountAdd();}
		},{
			text:'账号维护',
			iconCls:'button-config',
			itemId:'AccountEdit',
			tooltip:'<b>账号维护</b>',
			hidden:true,
			handler:function(){me.onAccountEdit();}
		},'->','save','reset'];
		
		me.callParent(arguments);
	},
	/**@overwrite 创建内部组件*/
	createItems:function(){
		var me = this,
			items = [];
		
		//隶属部门
		items.push({
			fieldLabel:'隶属部门',
			emptyText:'必填项',allowBlank:false,
			name:'HREmployee_HRDept_CName',
			itemId:'HREmployee_HRDept_CName',
			xtype:'uxCheckTrigger',
			className:'Shell.class.sysbase.org.CheckTree'
		},{
			fieldLabel:'隶属部门主键ID',hidden:true,
			name:'HREmployee_HRDept_Id',
			itemId:'HREmployee_HRDept_Id'
		},{
			fieldLabel:'隶属部门时间戳',hidden:true,
			name:'HREmployee_HRDept_DataTimeStamp',
			itemId:'HREmployee_HRDept_DataTimeStamp'
		});
		
		if(me.hasManager){
			items.push({
				fieldLabel:'直接上级',//emptyText:'必填项',allowBlank:false,
				name:'HREmployee_ManagerName',itemId:'HREmployee_ManagerName',
				xtype:'uxCheckTrigger',className:'Shell.class.sysbase.user.CheckApp'
			},{
				fieldLabel:'直接上级主键ID',hidden:true,
				name:'HREmployee_ManagerID',itemId:'HREmployee_ManagerID'
			});
		}
				
		//姓名
		items.push({
			fieldLabel:'姓',name:'HREmployee_NameL',itemId:'HREmployee_NameL',
			emptyText:'必填项',allowBlank:false
		},{
			fieldLabel:'名',name:'HREmployee_NameF',itemId:'HREmployee_NameF',
			emptyText:'必填项',allowBlank:false
		},{
			fieldLabel:'名称',name:'HREmployee_CName',readOnly:true,locked:true,
			itemId:'HREmployee_CName',hidden:true
		});
		
		//拼音字头、快捷码
		items.push({
			fieldLabel:'拼音字头',name:'HREmployee_PinYinZiTou',itemId:'HREmployee_PinYinZiTou'
			//emptyText:'必填项',allowBlank:false
		},{
			fieldLabel:'快捷码',name:'HREmployee_Shortcode',itemId:'HREmployee_Shortcode'
			//emptyText:'必填项',allowBlank:false
		},{
			fieldLabel:'员工代码',name:'HREmployee_UseCode',itemId:'HREmployee_UseCode'
			//emptyText:'必填项',allowBlank:false
		});
		
		//项目类别
		items.push({
			fieldLabel:'性别',
			//emptyText:'必填项',allowBlank:false,
			name:'HREmployee_BSex_Name',
			itemId:'HREmployee_BSex_Name',
			xtype:'uxCheckTrigger',
			className:'Shell.class.sysbase.sex.CheckGrid'
		},{
			fieldLabel:'性别主键ID',hidden:true,
			name:'HREmployee_BSex_Id',
			itemId:'HREmployee_BSex_Id'
		});
		//出生日期
		items.push({
			fieldLabel:'出生日期',name:'HREmployee_Birthday',
			xtype:'datefield',format:'Y-m-d'
		});
		
		//手机号
		items.push({
			fieldLabel:'手机号',name:'HREmployee_MobileTel',
			regex: /^1[34578]\d{9}$/,
			regexText:'不是手机号码'
		});
		items.push({
			boxLabel:'是否在职',name:'HREmployee_IsEnabled',
			xtype:'checkbox',checked:true
		},{
			boxLabel:'是否使用',name:'HREmployee_IsUse',
			xtype:'checkbox',checked:true
		},{
			fieldLabel:'主键ID',name:'HREmployee_Id',hidden:true
		});
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		//机构不存在时不让保存
		if(!values.HREmployee_HRDept_Id || values.HREmployee_HRDept_Id == "0"){
			JShell.Msg.error("请选择一个有效的隶属部门！");
			return;
		}
			
		var entity = {
			HRDept:{
				Id:values.HREmployee_HRDept_Id,
				DataTimeStamp:values.HREmployee_HRDept_DataTimeStamp.split(',')
			},
			NameL:values.HREmployee_NameL,
			NameF:values.HREmployee_NameF,
			CName:values.HREmployee_CName,
			
			PinYinZiTou:values.HREmployee_PinYinZiTou,
			Shortcode:values.HREmployee_Shortcode,
			UseCode:values.HREmployee_UseCode,
			
			Birthday:JShell.Date.toServerDate(values.HREmployee_Birthday),
			IsEnabled:values.HREmployee_IsEnabled ? 1 : 0,
			
			IsUse:values.HREmployee_IsUse ? true : false,
			MobileTel:values.HREmployee_MobileTel
		};
		
		if(me.hasManager && values.HRDept_ManagerID){
			entity.ManagerID = values.HREmployee_ManagerID || null;
			entity.ManagerName = values.HREmployee_ManagerName;
		}
		
		if(values.HREmployee_BSex_Id){
			entity.BSex = {
				Id:values.HREmployee_BSex_Id,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
		}
		
		
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			fields = me.getStoreFields(),
			entity = me.getAddParams();
		
		//机构不存在时不让保存
		if(!values.HREmployee_HRDept_Id || values.HREmployee_HRDept_Id == "0"){
			JShell.Msg.error("请选择一个有效的隶属部门！");
			return;
		}
		
		var fields = [
			'HRDept_Id','NameL','NameF','CName',
			'PinYinZiTou','Shortcode','UseCode','MobileTel',
			'Birthday','IsEnabled','IsUse','BSex_Id','Id'
		];
		
		if(me.hasManager){
			fields.push('ManagerID','ManagerName');
		}
		
		entity.fields = fields.join(',');
		
		entity.entity.Id = values.HREmployee_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		data.HREmployee_Birthday = JShell.Date.getDate(data.HREmployee_Birthday);
		return data;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		
		if(me.hasManager){
			var ManagerID = me.getComponent('HREmployee_ManagerID'),
				ManagerName = me.getComponent('HREmployee_ManagerName');
			
			//直接上级监听
			if(ManagerName){
				ManagerName.on({
					check: function(p, record) {
						ManagerName.setValue(record ? record.get('HREmployee_CName') : '');
						ManagerID.setValue(record ? record.get('HREmployee_Id') : '');
						p.close();
					}
				});
			}
		}
		
		var HREmployee_BSex_Name = me.getComponent('HREmployee_BSex_Name');
		var HREmployee_BSex_Id = me.getComponent('HREmployee_BSex_Id');
		//性别监听
		if(HREmployee_BSex_Name){
			HREmployee_BSex_Name.on({
				check: function(p, record) {
					HREmployee_BSex_Name.setValue(record ? record.get('BSex_Name') : '');
					HREmployee_BSex_Id.setValue(record ? record.get('BSex_Id') : '');
					p.close();
				}
			});
		}
		
		//部门监听
		var dictList = ['HRDept'];
		
		for(var i=0;i<dictList.length;i++){
			me.doHRDeptListeners(dictList[i]);
		}
	},
	/**员工监听*/
	doHRDeptListeners:function(name){
		var me = this;
		var CName = me.getComponent('HREmployee_' + name + '_CName');
		var Id = me.getComponent('HREmployee_' + name + '_Id');
		var DataTimeStamp = me.getComponent('HREmployee_' + name + '_DataTimeStamp');
		
		CName.on({
			check: function(p, record) {
				CName.setValue(record ? record.get('text') : '');
				Id.setValue(record ? record.get('tid') : '');
				DataTimeStamp.setValue(record ? record.get('value').DataTimeStamp : '');
				p.close();
			}
		});
	},
	/**拼音字头监听*/
	initPinYinZiTouListeners:function(){
		var me = this,
			HREmployee_NameL = me.getComponent('HREmployee_NameL'),
			HREmployee_NameF = me.getComponent('HREmployee_NameF'),
			HREmployee_CName = me.getComponent('HREmployee_CName');
			
		HREmployee_NameL.on({
			change:function(field,newValue,oldValue,eOpts){
				setTimeout(function(){
					me.changeCName();
				},100);
			}
		});
		HREmployee_NameF.on({
			change:function(field,newValue,oldValue,eOpts){
				setTimeout(function(){
					me.changeCName();
				},100);
			}
		});
		HREmployee_CName.on({
			change:function(field,newValue,oldValue,eOpts){
				setTimeout(function(){
					me.changePinYinZiTou();
				},100);
			}
		});
	},
	changeCName:function(){
		var me = this,
			HREmployee_NameL = me.getComponent('HREmployee_NameL'),
			HREmployee_NameF = me.getComponent('HREmployee_NameF'),
			HREmployee_CName = me.getComponent('HREmployee_CName');
			
		var name = HREmployee_NameL.getValue() + HREmployee_NameF.getValue();
		
		HREmployee_CName.setValue(name);
	},
	changePinYinZiTou:function(data){
		var me = this,
			HREmployee_CName = me.getComponent('HREmployee_CName'),
			HREmployee_PinYinZiTou = me.getComponent('HREmployee_PinYinZiTou'),
			HREmployee_Shortcode = me.getComponent('HREmployee_Shortcode');
			
		var name = HREmployee_CName.getValue();
			
		if(name != ""){
			JShell.Action.delay(function(){
				JShell.System.getPinYinZiTou(name,function(value){
					me.getForm().setValues({
						HREmployee_PinYinZiTou:value,
						HREmployee_Shortcode:value
					});
				});
			},null,200);
		}else{
			me.getForm().setValues({
				HREmployee_PinYinZiTou:"",
				HREmployee_Shortcode:""
			});
		}
	},
	isAdd:function(Id,Name,DataTimeStamp){
		var me = this;
		me.onChangeAccountButton(3);
		me.callParent(arguments);
		
		me.getForm().setValues({
			HREmployee_HRDept_Id:Id,
			HREmployee_HRDept_CName:Name,
			HREmployee_HRDept_DataTimeStamp:DataTimeStamp
		});
	},
	
	/**查看账号信息*/
	initAccountButton:function(){
		var me = this,
			url = JShell.System.Path.getRootUrl(me.getUserListUrl);
		
		url += '&fields=RBACUser_Id';
		url += '&where=rbacuser.HREmployee.Id=' + me.PK;
		
		JShell.Server.get(url,function(data){
			if(data.success){
				if(data.value && data.value.count == 1){
					me.AccountID = data.value.list[0].RBACUser_Id;
					me.onChangeAccountButton(2);
				}else{
					me.onChangeAccountButton(1);
				}
			}else{
				me.onChangeAccountButton(3);
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**账号按钮变化*/
	onChangeAccountButton:function(type){
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			AccountAdd = buttonsToolbar.getComponent('AccountAdd'),
			AccountEdit = buttonsToolbar.getComponent('AccountEdit');
			
		if(type == 1){
			AccountEdit.hide();
			AccountAdd.show();
		}else if(type == 2){
			AccountAdd.hide();
			AccountEdit.show();
		}else if(type == 3){
			AccountAdd.hide();
			AccountEdit.hide();
		}
	},
	onAccountAdd:function(){
		var me = this;
		JShell.Win.open('Shell.class.sysbase.user.account.Add', {
			resizable: false,
			HREmployeeId:me.PK,
			listeners:{
				save:function(p,data){
					me.AccountID = data.id;
					p.close();
					me.onChangeAccountButton(2);
					JShell.Msg.alert('账户创建成功',null,1000);
				}
			}
		}).show();
	},
	onAccountEdit:function(){
		var me = this;
		JShell.Win.open('Shell.class.sysbase.user.account.Edit', {
			resizable: false,
			PK:me.AccountID,
			listeners:{
				save:function(p,id){
					p.close();
					JShell.Msg.alert('账户已修改',null,1000);
				}
			}
		}).show();
	},
	/**保存后返回数据*/
	returnData:function(id){
		var me = this,
			values = me.getForm().getValues(),
			data = me.callParent(arguments);
			
		data.HRDept.CName = values.HREmployee_HRDept_CName;
		
		return data;
	}
});