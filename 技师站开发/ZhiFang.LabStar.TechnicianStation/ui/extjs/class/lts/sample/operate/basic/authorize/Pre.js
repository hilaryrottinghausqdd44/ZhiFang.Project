
/**
 * 快捷预授权临时(一次性)
 * @author Jcall
 * @version 2020-09-08
 */
Ext.define('Shell.class.lts.sample.operate.basic.authorize.Pre', {
	extend:'Shell.ux.form.Panel',
	requires:[
		'Shell.ux.form.field.CheckTrigger'
    ],
    
    title:'快捷预授权临时(一次性)',
    width:665,
	height:400,
	bodyPadding:'20px 10px',
    formtype:'add',
	layout:{type:'table',columns:4},
	defaults:{width:180,labelWidth:70,labelAlign:'right'},
	
	//登录校验
	checkLoginUrl:'/ServerWCF/RBACService.svc/RBAC_BA_Login?isValidate=true',
	//根据员工ID获取用户账号
	selectUserUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACUserByHQL?isPlanish=true',
	//新增操作权限
	addAuthorizeUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_AddLisOperateAuthorize', 
	//新增操作授权对应小组
	addAuthorizeSectionUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_AddLisOperateASection',
	
	//自定义,用于保存到内存中。检验确认人Handler,审核人Checker
	OperateType:'Handler',
	//操作类型ID
	OperateTypeID:'',
	//操作类型名称
	OperateTypeText:'',
    //检验小组
    SectionID:null,
    
	afterRender:function(){
		var me = this ;
		me.callParent(arguments);
		
		me.on({
			afterIsAdd:function(){
				//初始化数据
				me.initData();
			}
		});
	},
	initComponent:function(){
		var me = this;
		
		//操作数据-打开的小组
		me.OpenedSection = Ext.create('Shell.class.basic.data.OpenedSection');
		
		//自定义按钮功能栏
		me.buttonToolbarItems = ['->','accept'];
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		
		var items = [{
			xtype:'displayfield',fieldLabel:'授权操作',colspan:4,value:me.OperateTypeText
		},{
			fieldLabel:'授权人',xtype:'uxCheckTrigger',colspan:1,
			name:'AuthorizeUserName',itemId:'AuthorizeUserName',
			className:'Shell.class.basic.user.CheckGrid',
			classConfig:{TSysCode:'1001001'},
			allowBlank:false,emptyText:'必填项',
			listeners:{
				check:function(p,record){
					p.setValue(record ? record.get('HREmpIdentity_HREmployee_CName') : '');
					p.nextNode().setValue(record ? record.get('HREmpIdentity_HREmployee_Id') : '');
					p.close();
				}
			}
		},{
			fieldLabel:'授权人员ID',xtype:'textfield',itemId:'AuthorizeUserID',name:'AuthorizeUserID',hidden:true
		},{
			fieldLabel:'密码',xtype:'textfield',inputType:'text',colspan:1,
			itemId:'PassWord',name:'PassWord',
			allowBlank:false,emptyText:'必填项',inputType:'password'
		},{
			xtype: 'displayfield',colspan: 2,fieldLabel: '',value: ''
		},{
			xtype:'textfield',colspan:4,itemId:'OperateUserName',name:'OperateUserName',
			fieldLabel:'被授权人',readOnly: true,locked: true
		},{
			xtype:'textfield',fieldLabel:'登录者本人Id',itemId:'OperateUserID',name:'OperateUserID',hidden:true
		},{
			xtype:'radiogroup',fieldLabel:'授权时间',colspan:4,width:430,
			vertical:true,columns:6,itemId:'DateTime',name:'DateTime',
			items:[
				{boxLabel:'5分钟',name:'rb',inputValue:'1'},
				{boxLabel:'30分钟',name:'rb',inputValue:'2',checked:true},
				{boxLabel:'2小时',name:'rb',inputValue:'3'},
				{boxLabel:'4小时',name:'rb',inputValue:'4'},
				{boxLabel:'当日',name:'rb',inputValue:'5'},
				{boxLabel:'自定义', name:'rb',inputValue:'6'}
			],
			listeners:{
				change:function(com,newValue,oldValue,eOpts){
					me.changeDateTime(newValue.rb);
				}
			}
		},{
			xtype:'fieldcontainer',fieldLabel:'时间范围',colspan:4,itemId:'TimeRange',
			items:[{
				xtype:'fieldcontainer',itemId:'BeginTime',layout:{type:'hbox'},width:170,
				items:[{
					xtype:'datefield',width:95,format:'Y-m-d',labelSeparator:'',labelWidth:5,
					itemId:'BeginTime_Date',name:'BeginTime_Date'
				},{
					xtype:'timefield',width:75,format:'H:i:s',
					itemId:'BeginTime_Time',name:'BeginTime_Time'
				}]
			},{
				xtype:'fieldcontainer',itemId:'EndTime',layout:{type:'hbox'},width:170,
				items:[{
					xtype:'datefield',width:95,format:'Y-m-d',labelSeparator:'',labelWidth:5,
					itemId:'EndTime_Date',name:'EndTime_Date'
				},{
					xtype:'timefield',width:75,format:'H:i:s',
					itemId:'EndTime_Time',name:'EndTime_Time'
				}]
			}]
		},{
			xtype:'checkboxgroup',fieldLabel:'被授权小组',colspan:4,width:650,
			columns:6,vertical:true,itemId:'SectionID',
			emptyText:'必填项',allowBlank:false,
			items:[]
		},{
			xtype:'button',width:105,text:'更多小组选择',tooltip:'更多小组选择',iconCls:'button-add',
			handler:function(){me.onOpenCheckSectionWin();}
		}];
		
		return items;
	},
	//更改标题
	changeTitle:function(){
		var me = this;
	},
	
	//获取新增的数据
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			AuthorizeType:'1',//临时
			OperateTypeID:me.OperateTypeID,
			OperateType:me.OperateTypeText,
			AuthorizeUserID:values.AuthorizeUserID,
			AuthorizeUser:values.AuthorizeUserName,
			OperateUserID:values.OperateUserID,
			OperateUser:values.OperateUserName,
			IsUse:1
		};
		
		var TimeRange = me.onTimeRangeValid();
		if(!TimeRange){
			return;
		}
		entity.BeginTime = TimeRange.BeginTime;
		entity.EndTime = TimeRange.EndTime;
	    
		return entity;
	},
	//确定按钮
	onAcceptClick:function(){
		var me = this,
			values = me.getForm().getValues();
			
		//必填项校验
		if(!me.getForm().isValid()){
			return;
		}
		//时间范围校验
		if(!me.onTimeRangeValid()){
			return;
		}
		
		//根据授权人ID返回登录账号
		me.getAccountByEmpId(values.AuthorizeUserID,function(account){
	    	//先校验授权人输入密码是否正确
			me.checkLogin(account,values.PassWord,function(){
				//新增保存授权操作
				me.addAuthorize(function(AuthorizeId){
					//保存所有操作授权对应小组关系
					me.addAuthorizeSectionList(AuthorizeId,function(SectionIds){
						//保存后处理
						me.afterSave(SectionIds);
					});
				});
			});
	    });
	},
	//时间范围校验
	onTimeRangeValid:function(){
		var me = this,
			TimeRange = me.getComponent('TimeRange'),
			BeginTime = TimeRange.getComponent('BeginTime'),
			EndTime = TimeRange.getComponent('EndTime'),
			BeginTime_Date = BeginTime.getComponent('BeginTime_Date').getValue() || '',
			BeginTime_Time = BeginTime.getComponent('BeginTime_Time').getValue() || '',
			EndTime_Date = EndTime.getComponent('EndTime_Date').getValue() || '',
			EndTime_Time = EndTime.getComponent('EndTime_Time').getValue() || '';
		
		BeginTime_Date = JShell.Date.toString(BeginTime_Date,true) || '';
		BeginTime_Time = JShell.Date.toString(BeginTime_Time,false) || '';
		EndTime_Date = JShell.Date.toString(EndTime_Date,true) || '';
		EndTime_Time = JShell.Date.toString(EndTime_Time,false) || '';
		
		var info = {
			BeginTime:BeginTime_Date + ' ' + BeginTime_Time.slice(-8),
			EndTime:EndTime_Date + ' ' + EndTime_Time.slice(-8)
		};
		
		if(info.BeginTime.length > 0 && info.BeginTime.length < 19){
			JShell.Msg.error("开始时间格式错误！");
			return;
		}
		if(info.BeginTime.length > 0 && info.BeginTime.length < 19){
			JShell.Msg.error("结束时间格式错误！");
			return;
		}
		
		if(!info.BeginTime || !info.EndTime){
			JShell.Msg.error("预授权模式下，开始时间和结束时间不能为空！");
			return;
		}
		if(info.BeginTime > info.EndTime){
			JShell.Msg.error("预授权模式下，开始时间不能大于结束时间！");
			return;
		}
		
		return info;
	},
	//根据员工ID获取登录账号
	getAccountByEmpId:function(EmpId,callback){
		var me = this,
			url = JShell.System.Path.LIIP_ROOT + me.selectUserUrl;
			
		url += '&fields=RBACUser_Account&where=rbacuser.HREmployee.Id=' + EmpId;
		
		me.showMask(me.saveText);//显示遮罩层
		JShell.Server.get(url,function(data){
			me.hideMask();//显示遮罩层
			if(data.success){
				var list = (data.value || {}).list || [];
				if(list.length == 0){
		    		JShell.Msg.error('被授权人没有找到账号，请先维护');
		    		return;
		    	}else if(list.length > 1){
		    		JShell.Msg.error('被授权人存在多个账号信息，请联系管理员维护！');
		    		return;
		    	}else{
		    		callback(list[0].RBACUser_Account);
		    	}
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	//校验授权人用户名和密码是否正确
	checkLogin:function(account,passWord,callback) {
		var me = this,
			url = JShell.System.Path.LIIP_ROOT + me.checkLoginUrl;
			
		url += '&strUserAccount=' + account + '&strPassWord=' + passWord;
		
		me.showMask(me.saveText);
		JShell.Server.get(url,function(data){
			me.hideMask();
			if(data == 'true'){
				callback();
			}else{
				JShell.Msg.error('授权人密码错误,请重新输入!');
			}
		},true,null,true);
	},
	//新增操作权限
	addAuthorize:function(callback){
		var me = this,
			url =  JShell.System.Path.ROOT + me.addAuthorizeUrl;
			
		var entity = me.getAddParams();
		if(!entity){
			return;
		}
		entity.BeginTime = JShell.Date.toServerDate(entity.BeginTime);
		entity.EndTime = JShell.Date.toServerDate(entity.EndTime);
		
		me.showMask(me.saveText);//显示遮罩层
		JShell.Server.post(url,Ext.encode({
			entity:entity
		}),function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				var id = (data.value || {}).id || '';
				callback(id);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	
	//保存所有操作授权对应小组关系
	addAuthorizeSectionList:function(AuthorizeId,callback){
		var me = this,
			SectionIds = me.getComponent('SectionID').getValue().rb;
			
		if(Ext.typeOf(SectionIds) == 'string'){
			SectionIds = [SectionIds];
		}
		
		me.addAuthorizeSectionOne(AuthorizeId,callback,SectionIds,0,0);
	},
	//单个保存操作授权对应小组关系
	addAuthorizeSectionOne:function(AuthorizeId,callback,SectionIds,index,errorCount){
		//操作授权对应小组Lis_OperateASection
		var me = this,
			url =  JShell.System.Path.ROOT + me.addAuthorizeSectionUrl;
			
		if(index >= SectionIds.length){//结束保存
			if(errorCount == 0){
				callback(SectionIds);
			}else{
				JShell.Msg.error('操作授权对应小组关系保存中' + errorCount + '条失败！');
			}
			return;
		}
			
		var entity ={
			LisOperateAuthorize:{Id:AuthorizeId,DataTimeStamp:[0,0,0,0,0,0,0,0]},
			LBSection:{Id:SectionIds[index],DataTimeStamp:[0,0,0,0,0,0,0,0]}
		};
		
		me.showMask(me.saveText);//显示遮罩层
		JShell.Server.post(url,Ext.encode({
			entity:entity
		}),function(data){
			me.hideMask();//隐藏遮罩层
			if(!data.success){
				errorCount++;
			}
			me.addAuthorizeSectionOne(AuthorizeId,callback,SectionIds,++index,errorCount);
		});
	},
	//保存后处理
	afterSave:function(SectionIds){
		var me = this,
			entity = me.getAddParams(),
			isThisSection = false;
			
		for(var i in SectionIds){
			if(SectionIds[i] == me.SectionID){
				isThisSection = true;
				break;
			}
		}
		
		if(!isThisSection){
			JShell.Msg.error('该授权不包含当前小组，请重新选择！');
			return;
		}
		
		me.fireEvent('save',me,{
			AuthorizeUserID:entity.AuthorizeUserID,//授权人ID
			AuthorizeUserName:entity.AuthorizeUser,//授权人名称
			BeginTime:entity.BeginTime,//开始时间
			EndTime:entity.EndTime//结束时间
		});
	},
	//弹出小组选择窗口
	onOpenCheckSectionWin:function(){
		var me = this,
			items = me.getComponent('SectionID').items.items,
			checkedSectionIds = [];//获取已选择的小组
		
		for(var i=0;i<items.length;i++){
			checkedSectionIds.push(items[i].inputValue);
		}
		
		JShell.Win.open('Shell.class.lts.section.role.CheckGrid',{
			width:340,height:400,
			checkedIds:checkedSectionIds.join(','),
			listeners:{
				accept:function(p,records){
					var list = [];
					for(var i=0;i<records.length;i++){
						list.push({
							name:'rb',
							boxLabel:records[i].data.LBRight_LBSection_CName,
							inputValue:records[i].data.LBRight_LBSection_Id
						});
					}
					me.getComponent('SectionID').add(list);
					p.close();
				}
			}
		}).show();
	},
	//时间快捷选择联动
	changeDateTime:function(value){
		var me = this,
			sysdate = JShell.System.Date.getDate(),
			TimeRange = me.getComponent('TimeRange'),
			BeginTime = TimeRange.getComponent('BeginTime'),
        	EndTime = TimeRange.getComponent('EndTime'),
        	BeginTime_Date = BeginTime.getComponent('BeginTime_Date'),
			BeginTime_Time = BeginTime.getComponent('BeginTime_Time'),
			EndTime_Date = EndTime.getComponent('EndTime_Date'),
			EndTime_Time = EndTime.getComponent('EndTime_Time');
			startDate = JShell.Date.toString(sysdate),
			endDate = "";
			
		switch(value){
			case '1'://5分钟
				endDate = JShell.Date.toString(me.getTimeByMinute(5));
				break;
			case '2'://半小时
				endDate = JShell.Date.toString(me.getTimeByMinute(30));
				break;
			case '3'://2小时
				endDate = JShell.Date.toString(me.getTimeByMinute(120));
				break;
			case '4'://4小时
				endDate = JShell.Date.toString(me.getTimeByMinute(240));
				break;
			case '5'://当日
				startDate = JShell.Date.toString(sysdate,true) + ' 00:00:00';
				endDate = JShell.Date.toString(sysdate,true) + ' 23:59:59';
				break;
			default://自定义
				startDate = "";
				endDate = "";
				break;
		}
		
		BeginTime_Date.setValue(startDate.slice(0,10));
		BeginTime_Time.setValue(startDate.slice(-8));
		EndTime_Date.setValue(endDate.slice(0,10));
		EndTime_Time.setValue(endDate.slice(-8));
	},
	//根据分钟返回截止时间
	getTimeByMinute:function(minute){
		var me = this,
			sysdate = JShell.System.Date.getDate();
			
		if(!sysdate){
			return "";
		}
		
		return new Date(sysdate.getTime() + 1000*60*minute);
	},
	
	//界面数据初始化
	initData:function(){
		var me = this;
		
		//初始化被授权小组
		var sectionList = me.OpenedSection.getList();
		var itemlist = [];
		for(var i=0;i<sectionList.length;i++){
			itemlist.push({
				name:'rb',
				boxLabel:sectionList[i].Name,
				inputValue:sectionList[i].Id,
				checked:sectionList[i].Id == me.SectionID ? true : false
			});
		}
		me.getComponent('SectionID').add(itemlist);
		
		//初始化被授权人=当前登录者、授权时间默认为半小时
		var defaultDateTimeValue = "2";
		me.getForm().setValues({
			"DateTime":defaultDateTimeValue,
			"OperateUserID":JShell.System.Cookie.get(JShell.System.Cookie.map.USERID),
			"OperateUserName":JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME)
		});
		me.changeDateTime(defaultDateTimeValue);
	}
});