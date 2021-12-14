
/**
 * 快捷预授权临时(一次性)
 * @author liangyl
 * @version 2020-05-09
 */
Ext.define('Shell.class.lts.operate.basic.PreAuthorize', {
	extend:'Shell.ux.form.Panel',
	requires:[
		'Shell.ux.form.field.CheckTrigger',
		'Shell.class.lts.operate.basic.DateTime'
    ],
    formtype:'add',
	title:'快捷预授权临时(一次性)',
	width:665,
	height:400,
	bodyPadding:'20px 10px',
	layout: {type: 'table',columns: 4 },
	defaults:{width:180,labelWidth:70,labelAlign:'right'},
    //检验小组
    SectionID:null,
	 //自定义,用于保存到内存中。检验确认人Handler,审核人Checker
	OperateType:'Handler',
	//操作类型名称
	OperateTypeText:'检验确认',
	//操作类型ID
	OperateTypeID:'2',
	 //按钮是否可点击
    BUTTON_CAN_CLICK:true,
	//登录校验
	Login_Url:'/ServerWCF/RBACService.svc/RBAC_BA_Login',
	//根据员工id获取用户账号
	select_RBACUser_Url :'/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACUserByHQL?isPlanish=true',
	//新增操作权限
	addUrl :'/ServerWCF/LabStarService.svc/LS_UDTO_AddLisOperateAuthorize', 
	//新增操作授权对应小组
	addSUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_AddLisOperateASection',
	afterRender:function(){
		var me = this ;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		me.addEvents('save');
		//自定义按钮功能栏
		me.buttonToolbarItems = ['accept'];
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		//最后一次打开的小组页签
        var list = JShell.LocalStorage.get('sample_last_opened_section_list',true) || [];
		//被授权小组list
		var itemlist = [];
		for(var i=0;i<list.length;i++){
			itemlist.push({ boxLabel:list[i].name, name: 'rb', inputValue: list[i].id});
		}
		var items = [{
		        xtype: 'displayfield',fieldLabel: '授权操作',colspan:4,value: me.OperateTypeText
		    }, {
				fieldLabel:'授权人',xtype:'uxCheckTrigger',
				name:'AuthorizeUserName',itemId:'AuthorizeUserName',
				className:'Shell.class.basic.user.CheckGrid',
				classConfig:{TSysCode:'1001001'},emptyText: '必填项',
                allowBlank: false,colspan: 1,
				listeners:{
					check:function(p,record){
						p.setValue(record ? record.get('HREmpIdentity_HREmployee_CName') : '');
						p.nextNode().setValue(record ? record.get('HREmpIdentity_HREmployee_Id') : '');
						p.close();
					}
				}
			},
            {xtype:'textfield',itemId:'AuthorizeUserID',name:'AuthorizeUserID',fieldLabel:'授权人员ID',hidden:true},
		    {xtype:'textfield',colspan: 1,itemId:'strPassWord',name:'strPassWord',inputType:'text',fieldLabel:'密码',emptyText: '必填项',allowBlank: false,
		      listeners: {
	            change: function(field,newValue,oldValue,eOpts){
	            	var inputEl = field.inputEl.dom;
	            	inputEl.type = "password";
	            }
	        }},
		    {xtype: 'displayfield',colspan: 2,fieldLabel: '',value: ''},
		    {xtype:'textfield',colspan: 4,itemId:'OperateUserName',name:'OperateUserName',fieldLabel:'被授权人',readOnly: true,locked: true},
		    {xtype:'textfield',itemId:'OperateUserID',name:'OperateUserID',fieldLabel:'登录者本人Id',hidden:true},
            {
		        xtype: 'radiogroup',fieldLabel: '授权时间',columns: 6,width:430,
		        vertical: true,colspan:4,itemId:'DateTime',name:'DateTime',
		        items: [
		            { boxLabel: '5分钟', name: 'rb', inputValue: '1' },
		            { boxLabel: '30分钟', name: 'rb', inputValue: '2', checked: true},
		            { boxLabel: '2小时', name: 'rb', inputValue: '3' },
		            { boxLabel: '4小时', name: 'rb', inputValue: '4' },
		            { boxLabel: '当日', name: 'rb', inputValue: '5' },
		            { boxLabel: '自定义', name: 'rb', inputValue: '6' }
		        ],
		        listeners : {
	            	change : function(com,newValue,oldValue,eOpts ){
	            		me.changeDateTime(newValue);
	            	}
	            }
		    },{
                xtype: 'fieldcontainer',colspan:4,itemId:'TimeRange',fieldLabel:'时间范围',
				layout: {type: 'table',columns: 2 },
				defaults:{width:110,labelWidth:80,labelAlign:'right'},
                items: [
                   {xtype: 'datatimefield',width:180,colspan:1,fieldLabel: '',name: 'BeginTime',itemId:'BeginTime'},
                   {xtype: 'displayfield',fieldLabel: '',value: ''},
                   {xtype: 'datatimefield',width:180,fieldLabel: '',labelWidth: 0,name: 'EndTime',itemId:'EndTime'}
                ]
            },{
		        xtype: 'checkboxgroup',
		        fieldLabel: '被授权小组',
		        columns: 6,colspan:4,width:650,
		        vertical: true,itemId:'SectionID',
		        emptyText: '必填项',allowBlank: false,
		        items:itemlist
		    },
		    {xtype:'button',width:105,text:'更多小组选择',tooltip:'更多小组选择',iconCls:'button-add',
			    handler:function(){
		    	  me.openWin();
			    }
			}
		];
		return items;
	},
	
	/**更改标题*/
	changeTitle:function(){
		var me = this;
	},
	/**@overwrite 重置按钮点击处理方法*/
	onResetClick:function(){
		var me = this;
		me.callParent(arguments);
		me.loadDatas();
	},
	//@overwrite 获取新增的数据
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			OperateType:me.OperateTypeText,
			OperateTypeID:me.OperateTypeID,
			AuthorizeUserID:values.AuthorizeUserID,
			AuthorizeUser:values.AuthorizeUserName,
			OperateUserID:values.OperateUserID,
			OperateUser:values.OperateUserName,
			IsUse:1
		};
	    
		return entity;
	},
	//确定按钮
	onAcceptClick:function(){
		var me = this,
			values = me.getForm().getValues();
			
		if(!me.getForm().isValid()) return;
		
		if(!me.BUTTON_CAN_CLICK)return;
		//开始时间和结束时间大小判断
		var BeginTime = me.getComponent('TimeRange').getComponent('BeginTime').getValue();
        var EndTime = me.getComponent('TimeRange').getComponent('EndTime').getValue();
        if(JShell.Date.getDate(BeginTime)>JShell.Date.getDate(EndTime)){
        	JShell.Msg.error('开始时间不能小于结束时间!');
        	return;
        }
        //获取选择的小组
		var ids = me.getComponent('SectionID').getValue().rb;
		//根据授权人ID返回登录账号
	    me.getAccountByID(values.AuthorizeUserID,function(list){
	    	if(list.length==0){
	    		JShell.Msg.alert('被授权人没有找到账号，请先维护');
	    		return;
	    	}
	    	//账号
	    	var strAccount = list[0].RBACUser_Account;
	    	//先校验被授权人输入密码是否正确
			me.checkLogin(strAccount,values.strPassWord,function(text){
				if(text == 'false'){
					JShell.Msg.error('被授权人输入密码不对,请重新输入');
	    		    return;
				}
				//新增保存授权操作（只做新增）
                me.addAuthorize(BeginTime,EndTime,function(id){
					me.saveErrorCount = 0;
					me.saveCount = 0;
					me.saveLength = ids.length;
		            for(var i=0;i<ids.length;i++){
		            	//新增保存操作授权对应小组（只做新增）
		            	me.addASection(id,ids[i]);
		            }
                });
			});
	    });
	},
	//新增操作权限
	addAuthorize : function(BeginTime,EndTime,callback){
		var me = this;
		JShell.System.ClassDict.init('ZhiFang.Entity.LabStar','AuthorizeType',function(){
			if(!JShell.System.ClassDict.AuthorizeType){
    			JShell.Msg.error('未获取到授权类型,请重新保存');
    			return;
    		}
			
    		var info = JShell.System.ClassDict.getClassInfoByName('AuthorizeType','临时');
    		var url =  JShell.System.Path.ROOT + me.addUrl;
			var entity = me.getAddParams();
			entity.AuthorizeType= info.Id;
            entity.BeginTime=JShell.Date.toServerDate(BeginTime);
            entity.EndTime=JShell.Date.toServerDate(EndTime);
            
			if(!entity) return;	
			me.showMask(me.saveText);//显示遮罩层
			
			me.BUTTON_CAN_CLICK = false;
			JShell.Server.post(url,Ext.encode({entity:entity}),function(data){
				me.hideMask();//隐藏遮罩层
				me.BUTTON_CAN_CLICK=true;
				if(data.success){
					var id = data.value.id;
                    callback(id);
				}else{
					JShell.Msg.error(data.msg);
				}
			});
    	});
	},
	//操作授权对应小组Lis_OperateASection
	addASection : function(id,SectionID){
		var me = this;
		var url =  JShell.System.Path.ROOT + me.addSUrl;
		var entity ={
			LisOperateAuthorize:{Id:id,DataTimeStamp:[0,0,0,0,0,0,0,0]},
			LBSection:{Id:SectionID,DataTimeStamp:[0,0,0,0,0,0,0,0]}
		};
		if(!entity) return;	
		me.showMask(me.saveText);//显示遮罩层
		me.BUTTON_CAN_CLICK = false;
		
		JShell.Server.post(url,Ext.encode({entity:entity}),function(data){
			me.hideMask();//隐藏遮罩层
			me.BUTTON_CAN_CLICK=true;
			if(data.success){
				me.saveCount++;
			}else{
				me.saveErrorCount++;
				JShell.Msg.error(data.msg);
			}
			me.onSaveEnd(data);
		},false);
	},
	onSaveEnd:function(data){
		var me = this;
		if (me.saveCount + me.saveErrorCount == me.saveLength) {
			if (me.saveErrorCount == 0){
				me.localSaveStore();
				me.loadDatas();
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,1000);
				me.fireEvent('save',me);
			}else{
				JShell.Msg.error('存在失败信息，请重新保存！');
			}
		}
	},
	localSaveStore:function(){
		var  me = this;
		//获取选择的小组
		var ids = me.getComponent('SectionID').getValue().rb;
		var LabStar_TS = window.top.ZhiFangLabStarTechnicianStation;
		LabStar_TS = LabStar_TS || {};
		LabStar_TS.operation =  LabStar_TS.operation || {};
		
		for(var i=0;i<ids.length;i++){
			LabStar_TS.operation[ids[i]] = {};
            LabStar_TS.operation[ids[i]][me.OperateType] = me.getEntity();
		}
		me.saveStore(LabStar_TS);
	},
	
	//预授权选择弹出窗口
	openWin:function(){
		var me = this;
        //获取已选择的小组
        var checkedIds=[];
        var items = me.getComponent('SectionID').items.items;
        for(var i=0;i<items.length;i++){
        	checkedIds.push(items[i].inputValue);
        }
    	JShell.Win.open('Shell.class.lts.section.role.CheckGrid',{
			width:340,
			height:400,
			//已选择的小组
			checkedIds:checkedIds.join(','),
			listeners:{
				accept:function(p,records){
					var list = [];
					for(var i=0;i<records.length;i++){
						list.push({ boxLabel:records[i].data.LBRight_LBSection_CName, name: 'rb', inputValue: records[i].data.LBRight_LBSection_Id});
					}
					me.getComponent('SectionID').add(list);
					p.close();
				}
			}
		}).show();
	},
	//时间快捷选择联动
	changeDateTime : function(newValue){
		var me = this;
		var sysdate = JShell.System.Date.getDate();
		var TimeRange = me.getComponent('TimeRange');
		var BeginTime = TimeRange.getComponent('BeginTime');
        var EndTime = TimeRange.getComponent('EndTime');
        var startDate ="",endDate="";
        startDate = JShell.Date.toString(sysdate);
        switch(newValue.rb) {
			case '1': //5分钟
			    endDate = JShell.Date.toString(me.getTimeByMinute(5));
				break;
			case '2': //半小时
			    endDate = JShell.Date.toString(me.getTimeByMinute(30));
				break;
			case '3'://2小时
			    endDate = JShell.Date.toString(me.getTimeByMinute(120));
				break;
			case '4'://4小时
			    endDate = JShell.Date.toString(me.getTimeByMinute(240));
				break;
			case '5'://当日
			    startDate=JShell.Date.toString(sysdate,true);
			    if(startDate)startDate = startDate+' 00:00:00';
			    if(startDate)endDate = JShell.Date.toString(sysdate,true)+' 23:59:59';
				break;
			default: //自定义
			    startDate ="",endDate="";
				break
		}
        BeginTime.setValue(startDate);
        EndTime.setValue(endDate);
	},

    //根据分钟返回截止时间
    getTimeByMinute : function(minute){
   	   var me = this;
   	   var sysdate = JShell.System.Date.getDate();
   	   if(!sysdate)return "";
		//默认为当前时间半小时后
	   var end = sysdate.getTime() + 1000*60*minute;
	   return new Date(end)
    },
	loadDatas : function(){
		var me = this;
		//界面默认设置
		me.defalutConfig();
		
		if(JShell.LocalStorage.get('LabStar_TS') ) {
			var LabStar_TS = Ext.JSON.decode(JShell.LocalStorage.get('LabStar_TS'));
			if(LabStar_TS.operation){
				var checkIds = [];
				//勾选小组
				for(var key in LabStar_TS.operation) {
		            if(LabStar_TS.operation.hasOwnProperty(key)) {  // 建议加上判断,如果没有扩展对象属性可以不加
		            	
	            		for(var key2 in LabStar_TS.operation[key]) {
	            			if(LabStar_TS.operation[key].hasOwnProperty(key2)) {
	            				if(me.OperateType == key2)checkIds.push(key);
	            			}
	            		}
		            }
		        }
				//授权小组还原
		        me.getComponent('SectionID').setValue({
                    rb:checkIds
				});
				//当前打开的小组	
				if(LabStar_TS.operation[me.SectionID]){
					var obj  = LabStar_TS.operation[me.SectionID][me.OperateType];
				    if(obj)me.loadStorageDatas(obj);
				}
			}
		}
	},
	//界面默认设置
	defalutConfig:function(){
		var me = this;
		//默认为半小时
		var sysdate = JShell.System.Date.getDate();
		var TimeRange = me.getComponent('TimeRange');
		var BeginTime = TimeRange.getComponent('BeginTime');
        var EndTime = TimeRange.getComponent('EndTime');
        var sysdate = JShell.System.Date.getDate();
        BeginTime.setValue(JShell.Date.toString(sysdate) ? JShell.Date.toString(sysdate) : "");
        EndTime.setValue(JShell.Date.toString(me.getTimeByMinute(30)) ? JShell.Date.toString(me.getTimeByMinute(30)) : '');
        
        //被授权人--当前登录者
        me.getComponent('OperateUserName').setValue(JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME));
        me.getComponent('OperateUserID').setValue(JShell.System.Cookie.get(JShell.System.Cookie.map.USERID));
	},
	//保存在内存中的实体
	getEntity : function(){
		var me = this,
			values = me.getForm().getValues();
	    var entity ={
	    	UserName:values.OperateUserName,//被授权人
			UserId:values.OperateUserID,//被授权人
			AuthorizeUserID:values.AuthorizeUserID,//授权人
			AuthorizeUserName:values.AuthorizeUserName,//授权人
			BeginTime:me.getComponent('TimeRange').getComponent('BeginTime').getValue(),
			EndTime:me.getComponent('TimeRange').getComponent('EndTime').getValue(),
			isCheckTip:false,
			isLogin : false //预授权
	    };
	    return entity;
    },
	/**本地保存localstorage*/
	saveStore: function(obj) {
		var me = this;
		if(JShell.LocalStorage.get('LabStar_TS') ) {
			var LabStar_TS = Ext.JSON.decode(JShell.LocalStorage.get('LabStar_TS'));
            //获取选择的小组
		    var ids = me.getComponent('SectionID').getValue().rb;
		    for(var key in LabStar_TS.operation) {
		    	var flag=false;
	            if(LabStar_TS.operation.hasOwnProperty(key)) {  // 建议加上判断,如果没有扩展对象属性可以不加
	            	for(var j=0;j<ids.length;j++){
	            		if(key == ids[j]){
	            			flag=true;
	            			break;
	            		}
                    }
	            }
	            //删除不勾选的小组
	            if(!flag && LabStar_TS.operation[key][me.OperateType])delete LabStar_TS.operation[key][me.OperateType];
	        }
		    for(var i=0;i<ids.length;i++){
                if(!LabStar_TS.operation[ids[i]])LabStar_TS.operation[ids[i]]={};
	            LabStar_TS.operation[ids[i]][me.OperateType]=me.getEntity();
			}
//			JShell.LocalStorage.remove('LabStar_TS');
			JShell.LocalStorage.set('LabStar_TS', JSON.stringify(LabStar_TS));
		}else{
			JShell.LocalStorage.set('LabStar_TS', JSON.stringify(obj));
		}
	},
	//赋值
	loadStorageDatas:function(obj){
		var me = this;
	    me.getComponent('AuthorizeUserName').setValue(obj.AuthorizeUserName);
        me.getComponent('AuthorizeUserID').setValue(obj.AuthorizeUserID);
		me.getComponent('OperateUserName').setValue(obj.UserName);
        me.getComponent('OperateUserID').setValue(obj.UserId);
        if(obj.BeginTime && obj.EndTime){
        	me.getComponent('DateTime').setValue({rb:'6'});
	        me.getComponent('TimeRange').getComponent('BeginTime').setValue(obj.BeginTime);
	        me.getComponent('TimeRange').getComponent('EndTime').setValue(obj.EndTime);
        }
    },
	//根据员工id获取登录账号
	getAccountByID:function(EmpID,callback){
		var me = this;
		var url = JShell.System.Path.LIIP_ROOT + me.select_RBACUser_Url;
		url += '&fields=RBACUser_Account';
		url+='&where=rbacuser.HREmployee.Id='+EmpID;
		me.BUTTON_CAN_CLICK=false;
		me.showMask(me.saveText);//显示遮罩层
		JShell.Server.get(url,function(data){
			me.hideMask();//显示遮罩层
			me.BUTTON_CAN_CLICK=true;
			if(data.success){
				var list = data.value ? data.value.list : [];
				callback(list);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**校验被授权人用户名和密码是否正确*/
	checkLogin: function(strUserAccount,strPassWord,callback) {
		var me = this,
			url = JShell.System.Path.LIIP_ROOT + me.Login_Url;
			
		url += '?strUserAccount='+strUserAccount+'&strPassWord='+strPassWord+'&isValidate=true';
		me.BUTTON_CAN_CLICK=false;
		me.showMask(me.saveText);
		JShell.Server.get(url, function(text) {
			me.hideMask();
			me.BUTTON_CAN_CLICK=true;
			callback(text);
		}, false, null, true);
	}
});