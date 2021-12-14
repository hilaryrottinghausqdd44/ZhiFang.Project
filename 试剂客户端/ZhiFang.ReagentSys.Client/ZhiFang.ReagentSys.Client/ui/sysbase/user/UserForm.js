/**
 * 用户信息表单
 * @author Jcall
 * @version 2015-07-07
 */
Ext.define('Shell.sysbase.user.UserForm',{
	extend:'Ext.form.Panel',
	
	/**按钮栏位置top/bottom*/
	toolbarDock:'top',
	
	/**根部门ID*/
	rootOrgId:0,
	/**跟部门名称*/
	rootOrgName:'所有部门',
	
	/**默认加载数据时启用遮罩层*/
	hasLoadMask:true,
	title:'用户信息',
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
    addUrl:Shell.util.Path.rootPath + '/RBACService.svc/RBAC_UDTO_AddHREmployee',
	editUrl:Shell.util.Path.rootPath + '/RBACService.svc/RBAC_UDTO_UpdateHREmployeeByField',
	selectUrl:Shell.util.Path.rootPath + '/RBACService.svc/RBAC_UDTO_SearchHREmployeeById',
	fields:'HREmployee_NameL,HREmployee_NameF,HREmployee_CName,HREmployee_PinYinZiTou,' +
		'HREmployee_EName,HREmployee_IdNumber,HREmployee_Shortcode,HREmployee_BSex_Id,' +
		'HREmployee_Birthday,HREmployee_UseCode,HREmployee_HRDept_Id,HREmployee_HRPosition_Id,' +
		'HREmployee_JobDuty,HREmployee_IsEnabled,HREmployee_MobileTel,HREmployee_OfficeTel,' +
		'HREmployee_ExtTel,HREmployee_Email,HREmployee_Comment,HREmployee_LabID,HREmployee_Id,' +
		'HREmployee_HRDept_DataTimeStamp,HREmployee_HRPosition_DataTimeStamp,' +
		'HREmployee_BSex_DataTimeStamp,HREmployee_DataTimeStamp',
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		var HREmployee_NameL = me.getComponent('HREmployee_NameL');
		HREmployee_NameL.on({
            change:function(field,newValue){
                me.doHREmployee_CNameChange();
            }
        });
        var HREmployee_NameF = me.getComponent('HREmployee_NameF');
		HREmployee_NameF.on({
            change:function(field,newValue){
                me.doHREmployee_CNameChange();
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
			xtype:'textfield',
			itemId:'HREmployee_NameL',
			name:'HREmployee_NameL',
			x:5,y:10,
			fieldLabel:'姓',
			readOnly:true
		},{
			xtype:'textfield',
			itemId:'HREmployee_UseCode',
			name:'HREmployee_UseCode',
			x:215,y:10,
			fieldLabel: '代码'
		},{
			xtype:'textfield',
			itemId:'HREmployee_NameF',
			name:'HREmployee_NameF',
			x:5,y:35,
			fieldLabel:'名'
		},{
			xtype:'trigger',
			itemId:'HREmployee_HRDept_CName',
			name:'HREmployee_HRDept_CName',
			x:215,y:35,
			fieldLabel:'部门',
			triggerCls:'x-form-search-trigger',
			editable:false,
			onTriggerClick:function(){me.openParentHRDeptWin();}
		},{
			xtype:'textfield',
			itemId:'HREmployee_CName',
			name:'HREmployee_CName',
			x:5,y:60,
			fieldLabel:'名称'
		}, {
			xtype:'combobox',
			itemId:'HREmployee_HRPosition_Id',
			name:'HREmployee_HRPosition_Id',
			x:215,y:60,
			fieldLabel:'职位',
			
			queryMode:'local',
			editable:false,
			displayField:'HRPosition_CName',
			valueField:'HRPosition_Id',
			DataTimeStampField:'HREmployee_HRPosition_DataTimeStamp',
			store:new Ext.data.Store({
				fields:['HRPosition_CName','HRPosition_Id','HRPosition_DataTimeStamp'],
				pageSize:1000,
				proxy:{
					type:'ajax',
					async:false,
					url:Shell.util.Path.rootPath + '/RBACService.svc/RBAC_UDTO_SearchHRPositionByHQL?' +
						'isPlanish=true&fields=HRPosition_CName,HRPosition_Id,HRPosition_DataTimeStamp' +
						'&where=hrposition.IsUse=1',
					reader:{type:'json',root:'list'},
					extractResponseData:me.changeStoreData
				},
				autoLoad:true
			})
		}, {
			xtype:'textfield',
			itemId:'HREmployee_PinYinZiTou',
			name:'HREmployee_PinYinZiTou',
			x:5,y:85,
			fieldLabel:'拼音字头'
		}, {
			xtype:'textfield',
			itemId:'HREmployee_JobDuty',
			name:'HREmployee_JobDuty',
			x:215,y:85,
			fieldLabel:'工作职责'
		}, {
			xtype:'textfield',
			itemId:'HREmployee_EName',
			name:'HREmployee_EName',
			x:5,y:110,
			fieldLabel: '英文名称'
		}, {
			xtype:'combobox',
			itemId:'HREmployee_IsEnabled',
			name:'HREmployee_IsEnabled',
			x:215,y:110,
			fieldLabel:'在职',
			displayField:'text',
			valueField:'value',
			editable:false,
			value:'1',
			store:new Ext.data.SimpleStore({
				fields:['value','text'],
				data:[['1','是'],['0','否']]
			})
		}, {
			xtype:'textfield',
			itemId:'HREmployee_IdNumber',
			name:'HREmployee_IdNumber',
			x:5,y:135,
			fieldLabel:'身份证号'
		}, {
			xtype:'textfield',
			itemId:'HREmployee_MobileTel',
			name:'HREmployee_MobileTel',
			x:215,y:135,
			fieldLabel:'手机号码'
		}, {
			xtype:'textfield',
			itemId:'HREmployee_Shortcode',
			name:'HREmployee_Shortcode',
			x:5,y:160,
			fieldLabel:'快捷码'
		}, {
			xtype:'textfield',
			itemId:'HREmployee_OfficeTel',
			name:'HREmployee_OfficeTel',
			x:215,y:160,
			fieldLabel:'办公电话'
		}, {
			xtype:'combobox',
			itemId:'HREmployee_BSex_Id',
			name:'HREmployee_BSex_Id',
			x:5,y:185,
			fieldLabel:'性别',
			
			queryMode:'local',
			editable:false,
			displayField:'BSex_Name',
			valueField:'BSex_Id',
			DataTimeStampField:'HREmployee_BSex_DataTimeStamp',
			store:new Ext.data.Store({
				fields:['BSex_Name','BSex_Id','BSex_DataTimeStamp'],
				pageSize:1000,
				proxy:{
					type:'ajax',
					async:false,
					url:Shell.util.Path.rootPath + '/SingleTableService.svc/ST_UDTO_SearchBSexByHQL?' +
						'isPlanish=true&fields=BSex_Name,BSex_Id,BSex_DataTimeStamp&where=bsex.IsUse=1',
					reader:{type:'json',root:'list'},
					extractResponseData:me.changeStoreData
				},
				autoLoad:true
			})
		},{
			xtype:'textfield',
			itemId:'HREmployee_ExtTel',
			name:'HREmployee_ExtTel',
			x:215,y:185,
			fieldLabel:'分机'
		},{
			xtype:'datefield',
			itemId:'HREmployee_Birthday',
			name:'HREmployee_Birthday',
			x:5,y:210,
			fieldLabel:'出生日期',
			format:'Y-m-d'
		},{
			xtype:'textfield',
			itemId:'HREmployee_Email',
			name:'HREmployee_Email',
			x:215,y:210,
			fieldLabel:'Email'
		},{
			xtype:'textarea',
			itemId:'HREmployee_Comment',
			name:'HREmployee_Comment',
			x:5,y:235,width:410,
			fieldLabel:'描述',
			margin:'0 0 10 0'
		},{
			xtype:'textfield',
			itemId:'HREmployee_Id',
			name:'HREmployee_Id',
			x:0,y:0,
			fieldLabel:'主键ID',
			hidden:true
		},{
			xtype:'textfield',
			itemId:'HREmployee_HRDept_Id',
			name:'HREmployee_HRDept_Id',
			x:0,y:0,
			fieldLabel:'部门主键ID',
			hidden:true
		},{
			xtype:'textfield',
			itemId:'HREmployee_DataTimeStamp',
			name:'HREmployee_DataTimeStamp',
			x:0,y:0,
			fieldLabel:'主键时间戳',
			hidden:true
		},{
			xtype:'textfield',
			itemId:'HREmployee_HRDept_DataTimeStamp',
			name:'HREmployee_HRDept_DataTimeStamp',
			x:0,y:0,
			fieldLabel:'部门时间戳',
			hidden:true
		},{
			xtype:'textfield',
			itemId:'HREmployee_HRPosition_DataTimeStamp',
			name:'HREmployee_HRPosition_DataTimeStamp',
			x:0,y:0,
			fieldLabel:'职位时间戳',
			hidden:true
		},{
			xtype:'textfield',
			itemId:'HREmployee_BSex_DataTimeStamp',
			name:'HREmployee_BSex_DataTimeStamp',
			x:0,y:0,
			fieldLabel:'性别时间戳',
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
					me.changeHREmployee_HRDeptInfo({
						Id:recode.get('tid'),
						Name:recode.get('text')
					});
					me.CheckOrgTree.hide();
				},
				itemdblclick:function(p,recode){
					me.changeHREmployee_HRDeptInfo({
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
	isAdd:function(info){
		var me = this;
		me.type = "add";
		me.changeTypeText('新增');
		me.getForm().reset();
		me.enableBottomToolbar();
		me.changeHREmployee_HRDeptInfo(info);
		me.setReadonly(false);
	},
	isEdit:function(id,info){
		var me = this;
		me.setHREmployee_HRDeptInfo(info);
		me.type = "edit";
		me.changeTypeText('修改');
		me.load(id);
		me.enableBottomToolbar();
		me.setReadonly(false);
	},
	isShow:function(id,parentInfo){
		var me = this;
		me.setHREmployee_HRDeptInfo(info);
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
				me.changeHREmployee_HRDeptInfo({
					Id:me.HREmployee_HRDeptId,
					Name:me.HREmployee_HRDeptName
				});
			}else{
				Shell.util.Msg.error(data.msg);
			}
		});
	},
	setValues:function(values){
		var me = this;
		me.hasSetValue = false;
		
		values.HREmployee_Birthday = Shell.util.Date.getDate(values.HREmployee_Birthday);
		
		me.getForm().setValues(values);
		me.hasSetValue = true;
	},
	enableBottomToolbar:function(){
		this.getComponent('toolbar').enable();
	},
	disableBottomToolbar:function(){
		this.getComponent('toolbar').disable();
	},
	
	setHREmployee_HRDeptInfo:function(config){
		var me = this;
		me.HREmployee_HRDeptId = config.Id;
		me.HREmployee_HRDeptName = config.Name;
	},
	changeHREmployee_HRDeptInfo:function(config){
		var me = this,
			HREmployee_HRDeptId= me.getComponent('HREmployee_HRDept_Id'),
			HREmployee_HRDeptName= me.getComponent('HREmployee_HRDept_CName');
		HREmployee_HRDeptId.setValue(config.Id);
		HREmployee_HRDeptName.setValue(config.Name);
	},
	changeTypeText:function(value){
		var text = '<b style="color:#04408c;margin:0 5px;">' + value + '</b>';
		this.getComponent('toolbar').getComponent('type').setText(text,false);
	},
	setReadonly:function(bo){
		var me = this,
			action = bo ? true : false,
			items = [
				'HREmployee_NameL','HREmployee_UseCode','HREmployee_NameF','HREmployee_HRDept_CName',
				'HREmployee_CName','HREmployee_HRPosition_Id','HREmployee_PinYinZiTou','HREmployee_JobDuty',
				'HREmployee_EName','HREmployee_IsEnabled','HREmployee_IdNumber','HREmployee_MobileTel',
				'HREmployee_Shortcode','HREmployee_OfficeTel','HREmployee_BSex_Id',
				'HREmployee_ExtTel','HREmployee_Birthday','HREmployee_Email','HREmployee_Comment'
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
		
		if(values.HREmployee_NameL) entity.NameL = values.HREmployee_NameL;
		if(values.HREmployee_UseCode) entity.UseCode = values.HREmployee_UseCode;
		if(values.HREmployee_NameF) entity.NameF = values.HREmployee_NameF;
		if(values.HREmployee_CName) entity.CName = values.HREmployee_CName;
		
		if(values.HREmployee_PinYinZiTou) entity.PinYinZiTou = values.HREmployee_PinYinZiTou;
		if(values.HREmployee_JobDuty) entity.JobDuty = values.HREmployee_JobDuty;
		if(values.HREmployee_EName) entity.EName = values.HREmployee_EName;
		if(values.HREmployee_IsEnabled) entity.IsEnabled = values.HREmployee_IsEnabled;
		
		if(values.HREmployee_IdNumber) entity.IdNumber = values.HREmployee_IdNumber;
		if(values.HREmployee_MobileTel) entity.MobileTel = values.HREmployee_MobileTel;
		if(values.HREmployee_Shortcode) entity.Shortcode = values.HREmployee_Shortcode;
		
		if(values.HREmployee_OfficeTel) entity.OfficeTel = values.HREmployee_OfficeTel;
		if(values.HREmployee_ExtTel) entity.ExtTel = values.HREmployee_ExtTel;
		if(values.HREmployee_Birthday) entity.Birthday = Shell.util.Date.toServerDate(values.HREmployee_Birthday);
		
		if(values.HREmployee_Email) entity.Email = values.HREmployee_Email;
		if(values.HREmployee_Comment) entity.Comment = values.HREmployee_Comment;
		
		if(values.HREmployee_HRDept_Id){
			entity.HRDept = {Id:values.HREmployee_HRDept_Id};
		}
		if(values.HREmployee_HRPosition_Id){
			entity.HRPosition = {Id:values.HREmployee_HRPosition_Id};
		}
		if(values.HREmployee_BSex_Id){
			entity.BSex = {Id:values.HREmployee_BSex_Id};
		}
		
		if(me.type == 'edit'){
			entity.Id = values.HREmployee_Id;
			entity.DataTimeStamp = values.HREmployee_DataTimeStamp.split(',');
			
			data.fields = 'Id,NameL,UseCode,NameF,CName,PinYinZiTou,JobDuty,EName,' +
				'IsEnabled,IdNumber,MobileTel,Shortcode,OfficeTel,ExtTel,Birthday,' +
				'Email,Comment,HRDept_Id,HRPosition_Id,BSex_Id';
		}else{
			if(entity.HRDept){entity.HRDept.DataTimeStamp=values.HREmployee_HRDept_DataTimeStamp.split(',')}
			if(entity.HRPosition){entity.HRPosition.DataTimeStamp=values.HREmployee_HRPosition_DataTimeStamp.split(',')}
			if(entity.BSex){entity.BSex.DataTimeStamp=values.HREmployee_BSex_DataTimeStamp.split(',')}
		}
		
		data.entity = entity;
		
		return data;
	},
	doHREmployee_CNameChange:function(){
		var me = this;
		
		if(!me.hasSetValue) return;
		
		var HREmployee_NameL = me.getComponent('HREmployee_NameL'),
       		HREmployee_NameF = me.getComponent('HREmployee_NameF'),
        	HREmployee_CName = me.getComponent('HREmployee_CName'),
			HREmployee_PinYinZiTou = me.getComponent('HREmployee_PinYinZiTou'),
			HREmployee_Shortcode = me.getComponent('HREmployee_Shortcode'),
			value = '';
			
		value = HREmployee_NameL.getValue() + HREmployee_NameF.getValue();
		
		HREmployee_CName.setValue(value);
		
		var url = Shell.util.Path.rootPath + '/ConstructionService.svc/GetPinYin?chinese=' + value,
			isAscii = escape(value).indexOf("%u") == -1 ? true : false;
		
		if(isAscii){//全英文直接联动
			HREmployee_PinYinZiTou.setValue(value);
			HREmployee_Shortcode.setValue(value);
		}else{
			Shell.util.Action.delay(function(){
				Shell.util.Server.get(url,function(data){
					if(data.success){
						HREmployee_PinYinZiTou.setValue(data.value);
						HREmployee_Shortcode.setValue(data.value);
					}else{
						Shell.util.Msg.error(data.msg);
					}
				});
			});
		}
	},
	/**
     * 列表格式数据匹配方法
     * @private
     * @param {} response
     * @return {}
     */
    changeStoreData:function(response,callback){
        var result = Ext.JSON.decode(response.responseText);
        result.count = 0;result.list = [];
        
        if(result.ResultDataValue){
            var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
            result.count = ResultDataValue.count;
            result.list = ResultDataValue.list;
            result.ResultDataValue = '';
        }
        if(Ext.typeOf(callback) === 'function'){
        	callback(result);
        }
        response.responseText = Ext.JSON.encode(result);
        return response;
    }
});