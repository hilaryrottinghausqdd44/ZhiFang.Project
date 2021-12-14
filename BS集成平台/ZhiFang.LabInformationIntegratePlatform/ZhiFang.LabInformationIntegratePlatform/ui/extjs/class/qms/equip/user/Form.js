/**
 * 员工信息
 * @author liangyl	
 * @version 2018-11-09
 */
Ext.define('Shell.class.qms.equip.user.Form',{
	extend:'Shell.class.sysbase.user.Form',
    selectHqlUrl: '/RBACService.svc/RBAC_UDTO_SearchHREmployeeByHQL?isPlanish=true',

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
				fieldLabel:'直接上级',emptyText:'必填项',allowBlank:false,
				name:'HREmployee_ManagerName',itemId:'HREmployee_ManagerName',
				xtype:'uxCheckTrigger',className:'Shell.class.sysbase.user.CheckApp'
			},{
				fieldLabel:'直接上级主键ID',hidden:true,
				name:'HREmployee_ManagerID',itemId:'HREmployee_ManagerID'
			});
		}
		//姓名,工号
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
		//拼音字头、快捷码
		items.push({
			fieldLabel:'拼音字头',name:'HREmployee_PinYinZiTou',itemId:'HREmployee_PinYinZiTou'
			//emptyText:'必填项',allowBlank:false
		},{
			fieldLabel:'快捷码',name:'HREmployee_Shortcode',itemId:'HREmployee_Shortcode'
			//emptyText:'必填项',allowBlank:false
		},{
			fieldLabel:'工号',name:'HREmployee_UseCode',itemId:'HREmployee_UseCode'
//			emptyText:'必填项',allowBlank:false
		});
	  
		 //入职时间
		items.push({
			fieldLabel:'入职时间',name:'HREmployee_EntryTime',itemId:'HREmployee_EntryTime',
			xtype:'datefield',format:'Y-m-d'
	    },{
			fieldLabel: '职称',xtype: 'uxCheckTrigger',name: 'HREmployee_BProfessionalAbility_Name',
			itemId: 'HREmployee_BProfessionalAbility_Name',colspan:1,
			className: 'Shell.class.sysbase.professionalability.CheckGrid',
			listeners :{
				check: function(p, record) {
					var Name = me.getComponent('HREmployee_BProfessionalAbility_Name'),
					    Id = me.getComponent('HREmployee_BProfessionalAbility_Id');
					Name.setValue(record ? record.get('BProfessionalAbility_Name') : '');
					Id.setValue(record ? record.get('BProfessionalAbility_Id') : '');
					p.close();
				}
			}
		}, {
			fieldLabel: '职称主键ID',hidden: true,name: 'HREmployee_BProfessionalAbility_Id',itemId: 'HREmployee_BProfessionalAbility_Id'
		},{
			fieldLabel: '职务',xtype: 'uxCheckTrigger',name: 'HREmployee_HRPosition_CName',
			itemId: 'HREmployee_HRPosition_CName',colspan:1,
			className: 'Shell.class.sysbase.position.CheckGrid',
			listeners :{
				check: function(p, record) {
					var Name = me.getComponent('HREmployee_HRPosition_CName'),
					 Id = me.getComponent('HREmployee_HRPosition_Id');
					Name.setValue(record ? record.get('HRPosition_CName') : '');
					Id.setValue(record ? record.get('HRPosition_Id') : '');
					p.close();
				}
			}
		}, {
			fieldLabel: '职务主键ID',hidden: true,name: 'HREmployee_HRPosition_Id',itemId: 'HREmployee_HRPosition_Id'
		},{
			fieldLabel: '学历',xtype: 'uxCheckTrigger',name: 'HREmployee_BEducationLevel_Name',
			itemId: 'HREmployee_BEducationLevel_Name',colspan:1,
			className: 'Shell.class.sysbase.educationlevel.CheckGrid',
			listeners :{
				check: function(p, record) {
					var Name = me.getComponent('HREmployee_BEducationLevel_Name'),
					  Id = me.getComponent('HREmployee_BEducationLevel_Id');
					Name.setValue(record ? record.get('BEducationLevel_Name') : '');
					Id.setValue(record ? record.get('BEducationLevel_Id') : '');
					p.close();
				}
			}
		}, {
			fieldLabel: '学历主键ID',hidden: true,name: 'HREmployee_BEducationLevel_Id',itemId: 'HREmployee_BEducationLevel_Id'
		},{
			fieldLabel: '学位',xtype: 'uxCheckTrigger',name: 'HREmployee_BDegree_Name',
			itemId: 'HREmployee_BDegree_Name',colspan:1,
			className: 'Shell.class.sysbase.degree.CheckGrid',
			listeners :{
				check: function(p, record) {
					var Name = me.getComponent('HREmployee_BDegree_Name'),
					  Id = me.getComponent('HREmployee_BDegree_Id');
					Name.setValue(record ? record.get('BDegree_Name') : '');
					Id.setValue(record ? record.get('BDegree_Id') : '');
					p.close();
				}
			}
		}, {
			fieldLabel: '学位主键ID',hidden: true,name: 'HREmployee_BDegree_Id',itemId: 'HREmployee_BDegree_Id'
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
		
		if(me.hasManager){
			entity.ManagerID = values.HREmployee_ManagerID || null;
			entity.ManagerName = values.HREmployee_ManagerName;
		}
		if(values.HREmployee_EntryTime){
			entity.EntryTime=JShell.Date.toServerDate(values.HREmployee_EntryTime);
		}
		if(values.HREmployee_BSex_Id){
			entity.BSex = {
				Id:values.HREmployee_BSex_Id,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
		}
//		
		if(values.HREmployee_BProfessionalAbility_Id){
			entity.BProfessionalAbility = {
				Id:values.HREmployee_BProfessionalAbility_Id,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
		}
		if(values.HREmployee_HRPosition_Id){
			entity.HRPosition = {
				Id:values.HREmployee_HRPosition_Id,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
		}
		if(values.HREmployee_BEducationLevel_Id){
			entity.BEducationLevel = {
				Id:values.HREmployee_BEducationLevel_Id,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
		}
		if(values.HREmployee_BDegree_Id){
			entity.BDegree = {
				Id:values.HREmployee_BDegree_Id,
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
		
		var fields = [
			'HRDept_Id','NameL','NameF','CName',
			'PinYinZiTou','Shortcode','UseCode','MobileTel',
			'Birthday','IsEnabled','IsUse','BSex_Id','Id','EntryTime',
			'BProfessionalAbility_Id','HRPosition_Id',
			'BEducationLevel_Id','BDegree_Id'
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
		data.HREmployee_EntryTime = JShell.Date.getDate(data.HREmployee_EntryTime);
		return data;
	},
    //校验工号是否唯一
	checkUseCode:function(UseCode,Id,callback){
		var me=this;
		var url = JShell.System.Path.getRootUrl(me.selectHqlUrl);
		url += "&fields=HREmployee_Id";
		//去掉特殊字符 &和？
		var UseCode=JcallShell.String.encode(UseCode);   
		var where= "&where=hremployee.UseCode='" + UseCode+"'";
	 
		if(Id)url +=where+' and hremployee.Id!='+Id;
		url += where;
		JShell.Server.get(url,function(data){
			if(data.success){
				callback(data);
			}else{
				JShell.Msg.error(data.msg);
			}
		},false);
	},
	/**保存按钮点击处理方法*/
	onSaveClick:function(){
		var me = this;
		
		if(!me.getForm().isValid()) return;
		//校验员工
		var values = me.getForm().getValues();
		var isExec=true;
		me.checkUseCode(values.HREmployee_UseCode,values.HREmployee_Id,function(data){
			if(data&& data.value){
				var list =data.value.list;
				if(list.length>0){
					isExec=false;
				}
			}
		});
		var msg ="工号:【"+values.HREmployee_UseCode+"】已存在,请使用其他工号";
		if(!isExec){
			JShell.Msg.alert(msg,null,2000);
			return;
		}
		var url = me.formtype == 'add' ? me.addUrl : me.editUrl;
		url = JShell.System.Path.getRootUrl(url);
		
		var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();
		
		if(!params) return;
		
		var id = params.entity.Id;
		
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
});