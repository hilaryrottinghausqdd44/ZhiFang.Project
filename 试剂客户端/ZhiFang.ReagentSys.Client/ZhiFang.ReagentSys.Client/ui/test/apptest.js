Ext.onReady(function(){
	Ext.QuickTips.init();//初始化后就会激活提示功能
	Ext.Loader.setConfig({enabled: true});//允许动态加载
//	var ClassCode = "Ext.define('RYZZJG',{extend:'Ext.form.Panel',alias:'widget.RYZZJG',title:'表单',width:713,height:339,autoScroll:true,type:'add',dataId:-1,layout:'absolute',GetGroupItems:function(url2,valueField,displayField,groupName,defaultValue){var url=url2;if(url==''||url==null){Ext.Msg.alert('提示',url);return null;}var localData=[];Ext.Ajax.request({async:false,timeout:6000,url:url,method:'GET',success:function(response,opts){var result = Ext.JSON.decode(response.responseText);if(result.success){var ResultDataValue = Ext.JSON.decode(result['ResultDataValue']);var count = ResultDataValue['Count'];var mychecked=false;var arrStr=[];if(defaultValue!=''){arrStr=defaultValue.split(',');}for(var i=0;i<count;i++){var DeptID=ResultDataValue.List[i][valueField];var CName=ResultDataValue.List[i][displayField];if(arrStr.length>0){mychecked=Ext.Array.contains(arrStr,DeptID);}var tempItem={checked:mychecked,name:groupName,boxLabel:CName,inputValue:DeptID};localData.push(tempItem);}}else{Ext.Msg.alert('提示','获取信息失败！');}}});return localData;},initComponent:function(){var me=this;me.addEvents('saveClick');me.load=function(id){Ext.Ajax.request({async:false,url:getRootPath()+'/RBACService.svc/RBAC_UDTO_SearchHREmployeeById?isPlanish=true&fields=HREmployee_UseCode,HREmployee_NameL,HREmployee_NameF,HREmployee_CName,HREmployee_EName,HREmployee_Shortcode,HREmployee_PinYinZiTou,HREmployee_IsUse,HREmployee_Sex,HREmployee_Birthday,HREmployee_Email,HREmployee_MobileTel,HREmployee_OfficeTel,HREmployee_Address,HREmployee_City,HREmployee_Province,HREmployee_Country,HREmployee_MaritalStatus,HREmployee_EducationLevel,HREmployee_IdNumber,HREmployee_Degree,HREmployee_Position,HREmployee_GraduateSchool,HREmployee_HealthStatus,HREmployee_PoliticsStatus,HREmployee_Nationality,HREmployee_JobDuty&id='+(id?id:-1),method:'GET',timeout:5000,success:function(response,opts){var result=Ext.JSON.decode(response.responseText);if(result.success){var values=Ext.JSON.decode(result.ResultDataValue);me.getForm().setValues(values);}else{Ext.Msg.alert('提示','获取表单数据失败！');}},failure:function(response,options){Ext.Msg.alert('提示','获取表单数据请求失败！');}});};me.changeStoreData=function(response){var data = Ext.JSON.decode(response.responseText);var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);data.ResultDataValue = ResultDataValue;data.List = ResultDataValue.List;response.responseText = Ext.JSON.encode(data);return response;};me.setReadOnly=function(bo){var items = me.items;for(var i in items){items[i].readOnly=bo;}};me.items=[{xtype:'textfield',name:'HREmployee_UseCode',fieldLabel:'代码',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_UseCode',x:5,y:5,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_NameL',fieldLabel:'姓',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_NameL',x:175,y:5,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_NameF',fieldLabel:'名',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_NameF',x:345,y:5,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_CName',fieldLabel:'名称',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_CName',x:515,y:5,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_EName',fieldLabel:'英文名称',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_EName',x:5,y:31,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_Shortcode',fieldLabel:'快捷码',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_Shortcode',x:175,y:31,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_PinYinZiTou',fieldLabel:'拼音字头',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_PinYinZiTou',x:345,y:31,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_IsUse',fieldLabel:'是否使用',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_IsUse',x:564,y:299,readOnly:false,hidden:true},{xtype:'textfield',name:'HREmployee_Sex',fieldLabel:'性别',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_Sex',x:515,y:31,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_Birthday',fieldLabel:'出生日期',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_Birthday',x:5,y:166,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_Email',fieldLabel:'电子邮箱',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_Email',x:174,y:164,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_MobileTel',fieldLabel:'手机号码',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_MobileTel',x:344,y:137,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_OfficeTel',fieldLabel:'办公电话',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_OfficeTel',x:5,y:57,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_Address',fieldLabel:'联系地址',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_Address',x:175,y:57,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_City',fieldLabel:'城市',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_City',x:345,y:57,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_Province',fieldLabel:'省份',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_Province',x:515,y:57,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_Country',fieldLabel:'国家',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_Country',x:5,y:83,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_MaritalStatus',fieldLabel:'婚姻状况',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_MaritalStatus',x:175,y:83,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_EducationLevel',fieldLabel:'学历',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_EducationLevel',x:345,y:83,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_IdNumber',fieldLabel:'身份证号',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_IdNumber',x:515,y:83,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_Degree',fieldLabel:'学位',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_Degree',x:5,y:109,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_Position',fieldLabel:'职位',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_Position',x:175,y:109,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_GraduateSchool',fieldLabel:'毕业院校',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_GraduateSchool',x:345,y:109,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_HealthStatus',fieldLabel:'健康状况',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_HealthStatus',x:515,y:109,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_PoliticsStatus',fieldLabel:'政治面貌',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_PoliticsStatus',x:5,y:135,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_Nationality',fieldLabel:'民族',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_Nationality',x:175,y:135,readOnly:false,hidden:false},{xtype:'textareafield',name:'HREmployee_JobDuty',fieldLabel:'工作职责',labelWidth:55,labelStyle:'font-style:normal;',width:530,itemId:'HREmployee_JobDuty',x:5,y:194,readOnly:false,hidden:false}];me.dockedItems=[{xtype:'toolbar',dock:'bottom',items:['->',{xtype:'button',text:'保存',iconCls:'build-button-save',handler:function(){this.ownerCt.ownerCt.submit();this.ownerCt.ownerCt.fireEvent('saveClick');}},{xtype:'button',text:'重置',iconCls:'build-button-refresh',handler:function(){var form = this.ownerCt.ownerCt;form.getForm().reset();}}]}];me.callParent(arguments);},afterRender:function(){var me=this;if(me.type == 'edit'){me.load(me.dataId);}else if(me.type == 'show'){me.load(me.dataId);me.setReadOnly(true);}me.callParent(arguments);}});";
//	var Class = eval(ClassCode);
	
	var Class = Ext.define('RYZZJG',{
		extend:'Ext.form.Panel',
		alias:'widget.RYZZJG',
		title:'表单',width:713,height:339,
		autoScroll:true,type:'add',dataId:-1,
		layout:'absolute',
		GetGroupItems:function(url2,valueField,displayField,groupName,defaultValue){
			var url=url2;
			if(url==''||url==null){
				Ext.Msg.alert('提示',url);
				return null;
			}
			var localData=[];
			Ext.Ajax.request({
				async:false,timeout:6000,url:url,method:'GET',
				success:function(response,opts){
					var result = Ext.JSON.decode(response.responseText);
					if(result.success){var ResultDataValue = Ext.JSON.decode(result['ResultDataValue']);
						var count = ResultDataValue['Count'];
						var mychecked=false;var arrStr=[];
						if(defaultValue!=''){arrStr=defaultValue.split(',');}
						for(var i=0;i<count;i++){
							var DeptID=ResultDataValue.List[i][valueField];
							var CName=ResultDataValue.List[i][displayField];
							if(arrStr.length>0){mychecked=Ext.Array.contains(arrStr,DeptID);}
							var tempItem={checked:mychecked,name:groupName,boxLabel:CName,inputValue:DeptID};
							localData.push(tempItem);
						}
					}else{Ext.Msg.alert('提示','获取信息失败！');}
				}
			});
			return localData;
		},
		initComponent:function(){
			var me=this;me.addEvents('saveClick');
			me.load=function(id){
				Ext.Ajax.request({
					async:false,
					url:getRootPath()+'/RBACService.svc/RBAC_UDTO_SearchHREmployeeById?isPlanish=true&fields=HREmployee_UseCode,HREmployee_NameL,HREmployee_NameF,HREmployee_CName,HREmployee_EName,HREmployee_Shortcode,HREmployee_PinYinZiTou,HREmployee_IsUse,HREmployee_Sex,HREmployee_Birthday,HREmployee_Email,HREmployee_MobileTel,HREmployee_OfficeTel,HREmployee_Address,HREmployee_City,HREmployee_Province,HREmployee_Country,HREmployee_MaritalStatus,HREmployee_EducationLevel,HREmployee_IdNumber,HREmployee_Degree,HREmployee_Position,HREmployee_GraduateSchool,HREmployee_HealthStatus,HREmployee_PoliticsStatus,HREmployee_Nationality,HREmployee_JobDuty&id='+(id?id:-1),
					method:'GET',timeout:5000,
					success:function(response,opts){
						var result=Ext.JSON.decode(response.responseText);
						if(result.success){
							var values=Ext.JSON.decode(result.ResultDataValue);
							me.getForm().setValues(values);
						}else{
							Ext.Msg.alert('提示','获取表单数据失败！');
						}
					},
					failure:function(response,options){Ext.Msg.alert('提示','获取表单数据请求失败！');}
				});
			};
			me.changeStoreData=function(response){
				var data = Ext.JSON.decode(response.responseText);
				var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
				data.ResultDataValue = ResultDataValue;
				data.List = ResultDataValue.List;
				response.responseText = Ext.JSON.encode(data);
				return response;
			};
			me.setReadOnly=function(bo){
				var items = me.items.items;
				for(var i in items){items[i].setReadOnly(bo);}
			};
			me.items=[{
				xtype:'textfield',name:'HREmployee_UseCode',fieldLabel:'代码',labelWidth:55,labelStyle:'font-style:normal;',
				width:160,itemId:'HREmployee_UseCode',x:5,y:5,readOnly:false,hidden:false
			},{
				xtype:'textfield',name:'HREmployee_NameL',fieldLabel:'姓',labelWidth:55,labelStyle:'font-style:normal;',
				width:160,itemId:'HREmployee_NameL',x:175,y:5,readOnly:false,hidden:false
			},{xtype:'textfield',name:'HREmployee_NameF',fieldLabel:'名',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_NameF',x:345,y:5,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_CName',fieldLabel:'名称',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_CName',x:515,y:5,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_EName',fieldLabel:'英文名称',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_EName',x:5,y:31,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_Shortcode',fieldLabel:'快捷码',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_Shortcode',x:175,y:31,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_PinYinZiTou',fieldLabel:'拼音字头',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_PinYinZiTou',x:345,y:31,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_IsUse',fieldLabel:'是否使用',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_IsUse',x:564,y:299,readOnly:false,hidden:true},{xtype:'textfield',name:'HREmployee_Sex',fieldLabel:'性别',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_Sex',x:515,y:31,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_Birthday',fieldLabel:'出生日期',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_Birthday',x:5,y:166,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_Email',fieldLabel:'电子邮箱',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_Email',x:174,y:164,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_MobileTel',fieldLabel:'手机号码',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_MobileTel',x:344,y:137,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_OfficeTel',fieldLabel:'办公电话',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_OfficeTel',x:5,y:57,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_Address',fieldLabel:'联系地址',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_Address',x:175,y:57,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_City',fieldLabel:'城市',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_City',x:345,y:57,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_Province',fieldLabel:'省份',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_Province',x:515,y:57,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_Country',fieldLabel:'国家',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_Country',x:5,y:83,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_MaritalStatus',fieldLabel:'婚姻状况',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_MaritalStatus',x:175,y:83,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_EducationLevel',fieldLabel:'学历',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_EducationLevel',x:345,y:83,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_IdNumber',fieldLabel:'身份证号',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_IdNumber',x:515,y:83,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_Degree',fieldLabel:'学位',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_Degree',x:5,y:109,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_Position',fieldLabel:'职位',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_Position',x:175,y:109,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_GraduateSchool',fieldLabel:'毕业院校',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_GraduateSchool',x:345,y:109,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_HealthStatus',fieldLabel:'健康状况',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_HealthStatus',x:515,y:109,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_PoliticsStatus',fieldLabel:'政治面貌',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_PoliticsStatus',x:5,y:135,readOnly:false,hidden:false},{xtype:'textfield',name:'HREmployee_Nationality',fieldLabel:'民族',labelWidth:55,labelStyle:'font-style:normal;',width:160,itemId:'HREmployee_Nationality',x:175,y:135,readOnly:false,hidden:false},{xtype:'textareafield',name:'HREmployee_JobDuty',fieldLabel:'工作职责',labelWidth:55,labelStyle:'font-style:normal;',width:530,itemId:'HREmployee_JobDuty',x:5,y:194,readOnly:false,hidden:false}];me.dockedItems=[{xtype:'toolbar',dock:'bottom',items:['->',{xtype:'button',text:'保存',iconCls:'build-button-save',handler:function(){this.ownerCt.ownerCt.submit();this.ownerCt.ownerCt.fireEvent('saveClick');}},{xtype:'button',text:'重置',iconCls:'build-button-refresh',handler:function(){var form = this.ownerCt.ownerCt;form.getForm().reset();}}]}];
			me.callParent(arguments);
		},
		afterRender:function(){
			var me=this;
			me.callParent(arguments);
			if(me.type == 'edit'){
				me.load(me.dataId);
			}else if(me.type == 'show'){
				me.load(me.dataId);
				me.setReadOnly(true);
			}
		}
	});
	
	var panelParams = {
		dataId:1,
		modal:true,//模态
		floating:true,//漂浮
		closable:true,//有关闭按钮
		draggable:true//可移动
	};
	
	but1 = {
		xtype:'button',text:'修改',
		handler:function(){
			panelParams.type = "edit";
			var panel = Ext.create(Class,panelParams).show();
			//panel.getForm().setValues({HREmployee_CName:'测试1'});
		}
	};
	but2 = {
		xtype:'button',text:'查看',
		handler:function(){
			panelParams.type = "show";
			var panel = Ext.create(Class,panelParams);
			panel.show();
			//panel.getForm().setValues({HREmployee_CName:'测试2'});
		}
	};
	//总体布局
	Ext.create('Ext.container.Viewport',{
		padding:2,
		//layout:'fit',
		items:[but1,but2]
	});
});