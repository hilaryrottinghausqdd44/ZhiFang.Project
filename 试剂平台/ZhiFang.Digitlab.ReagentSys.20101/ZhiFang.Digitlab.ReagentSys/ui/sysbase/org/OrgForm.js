/**
 * 部门表单
 * @author Jcall
 * @version 2015-07-07
 */
Ext.define('Shell.sysbase.org.OrgForm',{
	extend:'Ext.form.Panel',
	
	/**按钮栏位置top/bottom*/
	toolbarDock:'top',
	
	/**根部门ID*/
	rootOrgId:0,
	/**跟部门名称*/
	rootOrgName:'所有部门',
	
	/**默认加载数据时启用遮罩层*/
	hasLoadMask:true,
	title:'部门表单',
	width:440,
	height:600,
	bodyPadding:20,
	autoScroll:true,
	/**绝对定位布局*/
	layout:'absolute',
	defaults:{
		labelAlign:'right',
        labelWidth:60,
		width:200
    },
    type:'show',
    addUrl:Shell.util.Path.rootPath + '/RBACService.svc/RBAC_UDTO_AddHRDept',
	editUrl:Shell.util.Path.rootPath + '/RBACService.svc/RBAC_UDTO_UpdateHRDeptByField',
	selectUrl:Shell.util.Path.rootPath + '/RBACService.svc/RBAC_UDTO_SearchHRDeptById',
	fields:'HRDept_Id,HRDept_DataTimeStamp,HRDept_ParentID,HRDept_UseCode,HRDept_CName,' +
		'HRDept_EName,HRDept_SName,HRDept_Shortcode,HRDept_PinYinZiTou,HRDept_IsUse,HRDept_DispOrder,' +
		'HRDept_Tel,HRDept_Fax,HRDept_ZipCode,HRDept_Address,HRDept_Contact,HRDept_Comment',
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		var HRDept_CName = me.getComponent('HRDept_CName');
		HRDept_CName.on({
            change:function(field,newValue){
                me.doHRDept_CNameChange(newValue);
            }
        });
        
        me.type == 'show' ? me.setReadonly(true) : me.setReadonly(false);
	},
	initComponent:function(){
		var me = this;
		me.addEvents('save');
		me.items = me.createItems();
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		
		return [{
			xtype:'trigger',
			itemId:'HRDept_ParentCName',
			name:'HRDept_ParentCName',
			x:5,y:10,
			fieldLabel:'上级部门',
			triggerCls:'x-form-search-trigger',
			editable:false,
			onTriggerClick:function(){me.openParentHRDeptWin();}
		},{
			xtype:'textfield',
			itemId:'HRDept_UseCode',
			name:'HRDept_UseCode',
			x:215,y:10,
			fieldLabel: '代码'
			
		},{
			xtype:'textfield',
			itemId:'HRDept_CName',
			name:'HRDept_CName',
			x:5,y:35,
			fieldLabel:'部门名称'
		},{
			xtype:'textfield',
			itemId:'HRDept_EName',
			name:'HRDept_EName',
			x:215,y:35,
			fieldLabel:'英文名称'
		},{
			xtype:'textfield',
			itemId:'HRDept_SName',
			name:'HRDept_SName',
			x:5,y:60,
			fieldLabel:'简称'
		},{
			xtype:'textfield',
			itemId:'HRDept_Shortcode',
			name:'HRDept_Shortcode',
			x:215,y:60,
			fieldLabel:'快捷码'
		},{
			xtype:'textfield',
			itemId:'HRDept_PinYinZiTou',
			name:'HRDept_PinYinZiTou',
			x:5,y:85,
			fieldLabel:'拼音字头'
		},{
			xtype:'checkbox',
			itemId:'HRDept_IsUse',
			name:'HRDept_IsUse',
			x:215,y:85,
			fieldLabel:'是否使用',
			inputValue:true,
			uncheckedValue:false,
			checked:true
		},{
			xtype:'numberfield',
			maxValue:100000,
			step:1,
			itemId:'HRDept_DispOrder',
			name:'HRDept_DispOrder',
			x:5,y:110,
			fieldLabel: '显示次序'
		},{
			xtype:'textfield',
			itemId:'HRDept_Tel',
			name:'HRDept_Tel',
			x:215,y:110,
			fieldLabel:'电话'
		},{
			xtype:'textfield',
			itemId:'HRDept_Fax',
			name:'HRDept_Fax',
			x:5,y:135,
			fieldLabel:'传真'
		},{
			xtype:'textfield',
			itemId:'HRDept_ZipCode',
			name:'HRDept_ZipCode',
			x:215,y:135,
			fieldLabel:'邮编'
		},{
			xtype:'textfield',
			itemId:'HRDept_Address',
			name:'HRDept_Address',
			x:5,y:160,
			fieldLabel:'地址'
		},{
			xtype:'textfield',
			itemId:'HRDept_Contact',
			name:'HRDept_Contact',
			x:215,y:160,
			fieldLabel:'联系人'
		},{
			xtype:'textarea',
			itemId:'HRDept_Comment',
			name:'HRDept_Comment',
			x:5,y:185,width:410,
			fieldLabel:'描述',
			margin:'0 0 10 0'
		},{
			xtype:'textfield',
			itemId:'HRDept_Id',
			name:'HRDept_Id',
			x:0,y:0,
			fieldLabel:'主键ID',
			hidden:true
		},{
			xtype:'textfield',
			itemId:'HRDept_DataTimeStamp',
			name:'HRDept_DataTimeStamp',
			x:0,y:0,
			fieldLabel:'时间戳',
			hidden:true
		},{
			xtype:'textfield',
			itemId:'HRDept_ParentID',
			name:'HRDept_ParentID',
			x:0,y:0,
			fieldLabel:'上级部门ID',
			hidden:true
		}];
	},
	createDockedItems:function(){
		var me = this;
		
		return [{
			xtype:'toolbar',
			dock:me.toolbarDock,
			itemId:'toolbar',
			items:[{
				xtype:'label',
				itemId:'type'
			},'->',{
				xtype:'button',
				text:'保存',
				iconCls:'build-button-save',
				handler:function(but){
					me.submit();
				}
			},{
				xtype:'button',
				text:'重置',
				iconCls:'build-button-refresh',
				handler:function(but){
					me.getForm().reset();
				}
			}]
		}];
	},
	openParentHRDeptWin:function(){
		var me = this;
		me.CheckOrgTree = me.CheckOrgTree || Shell.util.Win.openClass('Shell.sysbase.org.CheckOrgTree',{
			closeAction:'hide',
			width:250,
			height:400,
			rootOrgId:me.rootOrgId,
			rootOrgName:me.rootOrgName,
			listeners:{
				okClick:function(p,recode){
					me.changeHRDept_ParentInfo({
						Id:recode.get('tid'),
						Name:recode.get('text')
					});
					me.CheckOrgTree.hide();
				},
				itemdblclick:function(p,recode){
					me.changeHRDept_ParentInfo({
						Id:recode.get('tid'),
						Name:recode.get('text')
					});
					me.CheckOrgTree.hide();
				}
			}
		});
		
		me.CheckOrgTree.show();
	},
	submit:function(){
		var me = this;
		if (!me.getForm().isValid()) return;
		
		var url = me.type == 'add' ? me.addUrl : me.editUrl;
		var params = me.getEntity();
		
		if(!params) return;
		
		params = Ext.JSON.encode(params);
		
		if(me.hasLoadMask){
			me.SaveMask = me.SaveMask || new Ext.LoadMask(me.getEl(),{msg:'数据保存中...',removeMask:true});
			me.SaveMask.show();//显示遮罩层
    	}
		
		Shell.util.Server.post(url,params,function(data){
			if(me.hasLoadMask && me.SaveMask){me.SaveMask.hide();}//隐藏遮罩层
			if(data.success){
				me.fireEvent('save');
			}else{
				Shell.util.Msg.error(data.msg);
			}
		});
	},
	isAdd:function(parentInfo){
		var me = this;
		me.type = "add";
		me.changeTypeText('新增');
		me.getForm().reset();
		me.enableBottomToolbar();
		me.changeHRDept_ParentInfo(parentInfo);
		me.setReadonly(false);
	},
	isEdit:function(id,parentInfo){
		var me = this;
		me.setHRDept_ParentInfo(parentInfo);
		me.type = "edit";
		me.changeTypeText('修改');
		me.load(id);
		me.enableBottomToolbar();
		me.setReadonly(false);
	},
	isShow:function(id,parentInfo){
		var me = this;
		me.setHRDept_ParentInfo(parentInfo);
		me.type = "show";
		me.changeTypeText('查看');
		me.load(id);
		me.disableBottomToolbar();
		me.setReadonly(true);
	},
	load:function(id){
		var me = this;
		var url = me.selectUrl + '?isPlanish=true&fields=' + me.fields + '&id=' + id;
		
		if(me.hasLoadMask){
			me.LoadMask = me.LoadMask || new Ext.LoadMask(me.getEl(),{msg:'数据加载中...',removeMask:true});
			me.LoadMask.show();//显示遮罩层
    	}
		
		Shell.util.Server.get(url,function(data){
			if(me.hasLoadMask && me.LoadMask){me.LoadMask.hide();}//隐藏遮罩层
			if(data.success){
				me.setValues(data.value);
				me.changeHRDept_ParentInfo({
					Id:me.HRDept_ParentId,
					Name:me.HRDept_ParentName
				});
			}else{
				Shell.util.Msg.error(data.msg);
			}
		});
	},
	setValues:function(values){
		var me = this;
		me.hasSetValue = false;
		me.getForm().setValues(values);
		me.hasSetValue = true;
	},
	enableBottomToolbar:function(){
		this.getComponent('toolbar').enable();
	},
	disableBottomToolbar:function(){
		this.getComponent('toolbar').disable();
	},
	setHRDept_ParentInfo:function(config){
		var me = this;
		me.HRDept_ParentId = config.Id;
		me.HRDept_ParentName = config.Name;
	},
	changeHRDept_ParentInfo:function(config){
		var me = this,
			HRDept_ParentId= me.getComponent('HRDept_ParentID'),
			HRDept_ParentName= me.getComponent('HRDept_ParentCName');
		HRDept_ParentId.setValue(config.Id);
		HRDept_ParentName.setValue(config.Name);
	},
	changeTypeText:function(value){
		var text = '<b style="color:#04408c;margin:0 5px;">' + value + '</b>';
		this.getComponent('toolbar').getComponent('type').setText(text,false);
	},
	setReadonly:function(bo){
		var me = this,
			action = bo ? true : false,
			items = [
				'HRDept_UseCode','HRDept_CName','HRDept_EName','HRDept_SName','HRDept_Shortcode',
				'HRDept_PinYinZiTou','HRDept_IsUse','HRDept_DispOrder','HRDept_Tel',
				'HRDept_Fax','HRDept_ZipCode','HRDept_Address','HRDept_Contact','HRDept_Comment'
			];
			
		for(var i in items){
			me.getComponent(items[i]).setReadOnly(bo);
		}
	},
	getEntity:function(){
		var me = this,
			values = me.getForm().getValues(),
			data = {},
			entity = {};
		
		entity.IsUse = values.HRDept_IsUse;
		
		if(values.HRDept_ParentID) entity.ParentID = values.HRDept_ParentID;
		if(values.HRDept_UseCode) entity.UseCode = values.HRDept_UseCode;
		if(values.HRDept_CName) entity.CName = values.HRDept_CName;
		if(values.HRDept_EName) entity.EName = values.HRDept_EName;
		if(values.HRDept_SName) entity.SName = values.HRDept_SName;
		if(values.HRDept_Shortcode) entity.Shortcode = values.HRDept_Shortcode;
		if(values.HRDept_PinYinZiTou) entity.PinYinZiTou = values.HRDept_PinYinZiTou;
		if(values.HRDept_DispOrder) entity.DispOrder = values.HRDept_DispOrder;
		if(values.HRDept_Tel) entity.Tel = values.HRDept_Tel;
		if(values.HRDept_Fax) entity.Fax = values.HRDept_Fax;
		if(values.HRDept_ZipCode) entity.ZipCode = values.HRDept_ZipCode;
		if(values.HRDept_Address) entity.Address = values.HRDept_Address;
		if(values.HRDept_Contact) entity.Contact = values.HRDept_Contact;
		if(values.HRDept_Comment) entity.Comment = values.HRDept_Comment;
		
		if(me.type == 'edit'){
			entity.Id = values.HRDept_Id;
			entity.DataTimeStamp = values.HRDept_DataTimeStamp.split(',');
			data.fields = 'Id,ParentID,UseCode,CName,EName,SName,Shortcode,' +
				'PinYinZiTou,IsUse,DispOrder,Tel,Fax,ZipCode,Address,Contact,Comment';
				
			if(entity.Id == entity.ParentID){
				//上级部门不能是本身
				Shell.util.Msg.warning('上级部门与本部门不能相同');
				return null;
			}
		}
		
		data.entity = entity;
		
		return data;
	},
	doHRDept_CNameChange:function(value){
		var me = this;
		
		if(!me.hasSetValue) return;
		
		var HRDept_PinYinZiTou = me.getComponent('HRDept_PinYinZiTou'),
			HRDept_Shortcode = me.getComponent('HRDept_Shortcode');
		
		var url = Shell.util.Path.rootPath + '/ConstructionService.svc/GetPinYin?chinese=' + value,
			isAscii = escape(value).indexOf("%u") == -1 ? true : false;
		
		if(isAscii){//全英文直接联动
			HRDept_PinYinZiTou.setValue(value);
			HRDept_Shortcode.setValue(value);
		}else{
			Shell.util.Action.delay(function(){
				Shell.util.Server.get(url,function(data){
					if(data.success){
						HRDept_PinYinZiTou.setValue(data.value);
						HRDept_Shortcode.setValue(data.value);
					}else{
						Shell.util.Msg.error(data.msg);
					}
				});
			});
		}
	}
});