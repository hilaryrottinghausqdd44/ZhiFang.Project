/** 账户更新
**/
Ext.ns('Ext.zhifangux');
Ext.define('Ext.zhifangux.UpdateAccount', {
	extend:'Ext.form.Panel',
	alias:'widget.updateaccount',
	title:'账号更新',
    isClose:false,
	width:330,
	height:220,	
	/** 验证是否同名
	**/
    CheckUserNameUrl:getRootPath()+'/'+'RBACService.svc/RBAC_RJ_ValidateUserAccountIsExist',
	/** 用户类型
	**/
	userTpe:'present',
	/** 接收用户信息
	**/
	userInfo:'',
	header:true,
	objectName:'RBACUser',
	isSuccessMsg:true,
	getAppInfoServerUrl:getRootPath()+'/'+'ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById',
	addDataServerUrl:'RBACService.svc/RBAC_UDTO_AddRBACUser',
	editDataServerUrl:'RBACService.svc/RBAC_UDTO_UpdateRBACUserByField',
	classCode:'BTDAppComponents_ClassCode',
	PresentUrl:getRootPath()+'/'+'RBACService.svc/RBAC_UDTO_GetHREmployeeBySessionHREmpID',
	autoScroll:true,
	type:'add',
	bodyCls:'',
	layout:'absolute',
	setWinformInfo:function(record,com){
		var me = this;
		var itemId=com.boundField;
		var value=record.get('Id');
		var text=record.get('text');
		var winformtext=me.getComponent(itemId);
		winformtext.treeNodeID=record.get('Id');
	    if(winformtext.xtype=='combobox'){
			var arrTemp=[[value,text]];
			winformtext.store=new Ext.data.SimpleStore({
				fields:['value','text'],
				data:arrTemp ,autoLoad:true
			});
			winformtext.setValue(value);
		}else{
			winformtext.treeNodeID=value;
			winformtext.setValue(text);  
		}
    },
    getInfoByIdFormServer:function(id,callback){
    	var me = this;
    	var myUrl = me.getAppInfoServerUrl+'?isPlanish=true&id='+id;
    	Ext.Ajax.defaultPostHeader = 'application/json';
    	Ext.Ajax.request({ 
    		async:false,
    		url:myUrl,
    		method:'GET',
    		timeout:2000,
    		success:function(response,opts){
	    		var result = Ext.JSON.decode(response.responseText);
	    		if(result.success){
	    			var appInfo = '';
	    			if(result.ResultDataValue && result.ResultDataValue != ''){
	    				result.ResultDataValue = result.ResultDataValue.replace(/\\n/g,'\\\\u000a');
	    				appInfo = Ext.JSON.decode(result.ResultDataValue);
	    			}
	    			if(Ext.typeOf(callback) == 'function'){
	    				if(appInfo == ''){
	    					Ext.Msg.alert('提示','没有获取到应用组件信息！');
	    				}else{callback(appInfo);}
	    			}
	    		}
	    		else{Ext.Msg.alert('提示','获取应用信息失败！');}
    		}, 
    		failure:function(response,options){  
    			Ext.Msg.alert('提示','获取应用信息请求失败！');
    			
    		}	
    	});
    },
    functionBtnClick:function(com,e,optes){
    	var me=this;
    	var textItemId=com.boundField;
    	var textCom=me.getComponent(textItemId);
    	var appComID=textCom.appComID;
    	if(appComID!=''&&appComID!=null&&appComID!=undefined){
    		var callback = function(appInfo){
    			var title = textCom.getValue();
    			var ClassCode = '';
    			if(appInfo && appInfo != ''){
    				ClassCode = appInfo[me.classCode];
    			}if(ClassCode && ClassCode != ''){
    				me.openAppShowWin(title,ClassCode,com);
    			}else{ Ext.Msg.alert('提示','没有类代码！');}
    		};
    		me.getInfoByIdFormServer(appComID,callback);
    	}else{Ext.Msg.alert('提示','功能按钮没有绑定应用！');}
    },
    openAppShowWin:function(title,classCode,com){
    	var me = this;
    	var panel = eval(classCode);
    	var maxHeight = document.body.clientHeight*0.98;
    	var maxWidth = document.body.clientWidth*0.98;
    	var appList = Ext.create(panel,{
    		maxWidth:maxWidth,
    		maxHeight:maxHeight,
    		autoScroll:true,
    		model:true,floating:true,
    		closable:true,draggable:true
    	}).show();
    	appList.on({
    		okClick:function(){
	    		var records=appList.getValue();
	    		if(records.length == 0){
	    			Ext.Msg.alert('提示','请选择一个应用！');
	    		}else if(records.length == 1){
	    			me.setWinformInfo(records[0],com);
	    		}
	    	},
	    	itemdblclick:function(view,record,item,index,e,eOpts){
	    		me.setWinformInfo(record,com);
	    	}
    	});
    },
    GetGroupItems:function(url2,valueField,displayField,groupName,defaultValue){
    	var myUrl=url2;
    	if(myUrl==''||myUrl==null){
    		Ext.Msg.alert('提示',myUrl);
    		return null;
    	}else{
    		myUrl=getRootPath()+'/'+myUrl;
    	}
    	var localData=[];
    	Ext.Ajax.request({
    		async:false,
    		timeout:6000,
    		url:myUrl,
    		method:'GET',
    		success:function(response,opts){
    		    var result = Ext.JSON.decode(response.responseText);
    		    if(result.success){
    		    	var ResultDataValue = {count:0,list:[]};
    		    	if(result['ResultDataValue'] && result['ResultDataValue'] != ''){
    		    		ResultDataValue = Ext.JSON.decode(result['ResultDataValue']);
    		    	}
    		    	var count = ResultDataValue['count'];
    		    	var mychecked=false;var arrStr=[];
    		    	if(defaultValue!=''){
    		    		arrStr=defaultValue.split(',');
    		    	}
    		    	for(var i=0;i<count;i++){
    		    		var DeptID=ResultDataValue.list[i][valueField];
    		    		var CName=ResultDataValue.list[i][displayField];
    		    		if(arrStr.length>0){
    		    			mychecked=Ext.Array.contains(arrStr,DeptID);
    		    		}
    		    		var tempItem={checked:mychecked,name:groupName,boxLabel:CName,inputValue:DeptID};
    		    		localData.push(tempItem);
    		    	}
    		   }else{Ext.Msg.alert('提示','获取信息失败！');}
	    	}
	    });
        return localData;
    },
    beforeRender:function(){
    	var me=this;
    	me.callParent(arguments);
    	if (!(me.header === false)) {
    		me.updateHeader();
    	}
    },
    initComponent:function(){
    	var me=this;
    	me.OverrideField();
    	me.LenTypes();
    	me.addEvents('beforeSave');
    	me.addEvents('saveClick');
    	me.addEvents('functionBtnClick');
    	me.load=function(id){
    		Ext.Ajax.request({
    			async:false,
    			url:getRootPath()+'/RBACService.svc/RBAC_UDTO_SearchRBACUserById?isPlanish=true&fields=RBACUser_Account,RBACUser_EnMPwd,RBACUser_AccBeginTime,RBACUser_AccEndTime,RBACUser_Id&id='+(id?id:-1),
    			method:'GET',
    			timeout:5000,
    			success:function(response,opts){
	    			var result=Ext.JSON.decode(response.responseText);
	    			if(result.success){
	    				if(result.ResultDataValue&&result.ResultDataValue!=''){
	    					if(me.type == 'add'){
	    						me.type='edit';
	    					}
	    					var values=Ext.JSON.decode(result.ResultDataValue);
	    					me.getForm().setValues(values);
	    				}
	    			}else{Ext.Msg.alert('提示','获取表单数据失败！');}
	    		},
    		    failure:function(response,options){Ext.Msg.alert('提示','获取表单数据请求失败！');}
    		});
    	};
    	me.setValueByItemId=function(key,value){
    		me.getForm().setValues([{id:key,value:value}]);
    	};
    	me.changeStoreData=function(response){
    		var data = Ext.JSON.decode(response.responseText);
    		if(data.ResultDataValue && data.ResultDataValue !=''){
    			var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
    			data.ResultDataValue = ResultDataValue;
    			data.list = ResultDataValue.list;
    		}else{
    			data.list=[];
    		}response.responseText = Ext.JSON.encode(data);
    		return response;
    	};
    	me.setReadOnly=function(bo){
    		var items2 = me.items.items;
    		for(var i in items2){
    			if(!items2[i].hasReadOnly){
    				var type=items2[i].xtype; 
    				if(type=='button'||type=='label'||type == "box"){}
    				else{
    					items2[i].setReadOnly(bo);	
    				}
    			}
    		}
    	};
    	me.items=[{xtype:'textfield',
    		name:'RBACUser_Account',
    	    fieldLabel:'员工账号',labelWidth:80,
    	    labelStyle:'font-style:normal;',
    	    width:215,height:22,itemId:'RBACUser_Account',
    	    x:10,y:5,readOnly:false,appComID:'',
    	    treeNodeID:'',value:'',isFunctionBtn:false,
    	    allowBlank:false,
    	    emptyText : '3-12位不能重复的账号',
    	    blankText  : '请输入员工账号',
    	    maxLengthText : '长度不能超过12个字符',
    	    minLengthText : '长度不能超过3个字符',
    	    hidden:false,sortNum:1,
    	    showValidIcon: false,
            msgTarget:'side',
            vtype:'isLen',  
    	    validateOnBlur: true,
            //validationDelay:2000,
            validateOnChange: true,
    	    listeners:{
    		    scope:this,
    		    specialkey:function(field,e){
    		        var iNum = 1;
    		        var sNumField = 'sortNum';
    		        var form = field.ownerCt;
    		        var num = field[sNumField];
    		        var items = form.items.items;
    		        var max = 5;
    		        if(e.getKey() == Ext.EventObject.ENTER || e.getKey() == Ext.EventObject.TAB){
    		        	e.preventDefault();
    		        }
    		        if(num > 0 && (e.getKey() == Ext.EventObject.ENTER || e.getKey() == Ext.EventObject.TAB)){
    		        	if(!e.shiftKey){num = (num+iNum > max) ? num+iNum-max : num+iNum;}
    		        	else{num = (num-iNum < 1) ? num-iNum+max : num-iNum;}
    		        	for(var i in items){
    		        		if(items[i].sortNum == num){
    		        	    	items[i].focus(false,100);break;
    		        	    	
    		        		}
    		        	}
    		       }
    	     },
    	     blur: function (The,eOpts ){
    	    	 var RBACUser_Account=me.getComponent('RBACUser_Account');
                 var txtAccount=me.getComponent('txtAccount');
            	 var Account=RBACUser_Account.getValue();
            	 var id=me.getComponent('RBACUser_Id').getValue();
                 
                 var txtBJ=me.getComponent('txtBJ');
                 var txtBJCD=me.getComponent('txtBJCD');
                 
                 var imageid=me.getComponent('yzimage');
                 
                 var checkuser=function(checkinfo)
                 {
                    var datauser=checkinfo;
                    if(datauser.result=='true')
                    {    
                      if(Account.length>=3&&Account.length<=12)
	                    {
	                       txtBJ.setVisible(true);   
                           txtBJCD.setVisible(false); 
	                       //Ext.Msg.alert('提示', '该名称己经存在,请重新输入！');
	                       imageid.getEl().dom.src='';//getRootPath()+'/'+'ui/css/images/png/64x64/delete.png';
	                    }
                        else
                        {
                            //账号长度不符合
                            txtBJ.setVisible(false);
                            txtBJCD.setVisible(true);
                            imageid.getEl().dom.src='';
                        }
                                             
                    }
                    if(datauser.result=='false')
                    {
                        if(Account.length>=3&&Account.length<=12)
                        {
	                        txtBJ.setVisible(false); 
                            txtBJCD.setVisible(false);
	                       //Ext.Msg.alert('提示', '该名称未注册！');
	                       //var imageid=me.getComponent('yzimage');
	                       imageid.getEl().dom.src=getIconRootPathBySize(64) + "/"+ 'accept.png';
                        }
                        else
                        {
                            txtBJ.setVisible(false);
                            txtBJCD.setVisible(true);
                            imageid.getEl().dom.src='';
                        }
                    }
                    
                 };  
                 //判断是旧账号不做校验
                 if(txtAccount.getValue()!=RBACUser_Account.getValue())
                 {
            	   me.validator(Account,checkuser);
                 }
                 else
                 {
                    imageid.getEl().dom.src='';
                    txtBJ.setVisible(false); 
                 }
	    	    
             }
    	     
    	},
    	hasReadOnly:false,
    	labelAlign:'left'
    	},/*{xtype:'textfield',
            name:'txtBJ',cls:'textfield-red', 
            fieldLabel:'',labelWidth:80,//style:'color:red;background:blue;',
            labelStyle:'font-style:normal;',
            width:80,height:22,itemId:'txtBJ',hidden:true,
            x:230,y:5,readOnly:false,value:'该用户已存在！'},*/
        {xtype:'label',
            cls:'textfield-red', 
            //labelWidth:80,//
            style:'color:red;background:white;',
            labelStyle:'font-style:normal;',
            width:82,height:22,itemId:'txtBJ',hidden:true,
            x:218,y:5,text:'账号已存在'
         },
         {xtype:'label',
            cls:'textfield-red', 
            //labelWidth:80,//
            style:'color:red;background:white;',
            labelStyle:'font-style:normal;',
            width:82,height:22,itemId:'txtBJCD',hidden:true,
            x:218,y:5,text:'账号长度不符'
         },
        {
            xtype:'box', //或者xtype: 'component',  
            width: 26, //图片宽度  
            id:'yzimage',
            itemId:'yzimage', 
            x:220,
            y:3,
            height:26, //图片高度  
            autoEl: { // "/" + "ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById",
                tag: 'img',    //指定为img标签  
                src:'' //getRootPath()+'/'+'ui/css/images/png/64x64/accept.png'    //指定url路径
            }  
          }  ,
            {
    		xtype:'checkboxfield',name:'RBACUser_EnMPwd',
    		labelWidth:80,labelStyle:'font-style:normal;',
    		boxLabel :'允许修改密码',  
    		fieldLabel:' ',
            labelSeparator :'',
    		width:220,height:22,itemId:'RBACUser_EnMPwd',x:5,y:95,readOnly:false,appComID:'',
    		treeNodeID:'',value:'',isFunctionBtn:false,hidden:false,sortNum:2,
    		listeners:{
    		scope:this,
    		specialkey:function(field,e){
    		var iNum = 1;
    		var sNumField = 'sortNum';
    		var form = field.ownerCt;
    		var num = field[sNumField];
    		var items = form.items.items;
    		var max = 5;
    		if(e.getKey() == Ext.EventObject.ENTER || e.getKey() == Ext.EventObject.TAB){
    			e.preventDefault();
    			
    		}if(num > 0 && (e.getKey() == Ext.EventObject.ENTER || e.getKey() == Ext.EventObject.TAB)){
    			if(!e.shiftKey){num = (num+iNum > max) ? num+iNum-max : num+iNum;}
    			else{num = (num-iNum < 1) ? num-iNum+max : num-iNum;}
    			for(var i in items){
    				if(items[i].sortNum == num){
    					items[i].focus(false,100);break;
    				}
    			}
    		}
    	}
    	},
    	hasReadOnly:false,labelAlign:'left'
    	},{xtype:'textfield',
            name:'txtAccount',hidden:true,
            fieldLabel:'old员工账号',labelWidth:80,
            labelStyle:'font-style:normal;',
            width:220,height:22,itemId:'txtAccount',
            x:2,y:125,readOnly:true
        },
        {
    		xtype:'datefield',name:'RBACUser_AccBeginTime',fieldLabel:'账号开始日期',labelWidth:80,
    		labelStyle:'font-style:normal;',width:215,height:22,editable:true,
    		itemId:'RBACUser_AccBeginTime',x:10,y:35,readOnly:false,hidden:false,
    		format:'Y-m-d',sortNum:3,  
            msgTarget:'side',
    		listeners:{
    		scope:this,
    		specialkey:function(field,e){
    		var iNum = 1;var sNumField = 'sortNum';
    		var form = field.ownerCt;
    		var num = field[sNumField];
    		var items = form.items.items;
    		var max = 5;if(e.getKey() == Ext.EventObject.ENTER || e.getKey() == Ext.EventObject.TAB){e.preventDefault();
    		}
    		if(num > 0 && (e.getKey() == Ext.EventObject.ENTER || e.getKey() == Ext.EventObject.TAB)){
    			if(!e.shiftKey){num = (num+iNum > max) ? num+iNum-max : num+iNum;}
    			else{num = (num-iNum < 1) ? num-iNum+max : num-iNum;}
    			for(var i in items){
    				if(items[i].sortNum == num){
    					items[i].focus(false,100);break;
    				}
    			}
    		}
    	}
    	},hasReadOnly:false,labelAlign:'left'
    	},{
    		xtype:'datefield',name:'RBACUser_AccEndTime',
    		fieldLabel:'账号截止日期',labelWidth:80,labelStyle:'font-style:normal;',
    		width:215,height:22,editable:true,itemId:'RBACUser_AccEndTime',
    		x:10,y:65,readOnly:false,hidden:false,format:'Y-m-d',sortNum:4,
            msgTarget:'side',
    		listeners:{
    		scope:this,
    		specialkey:function(field,e){
    		var iNum = 1;var sNumField = 'sortNum';
    		var form = field.ownerCt;var num = field[sNumField];
    		var items = form.items.items;var max = 5;
    		if(e.getKey() == Ext.EventObject.ENTER || e.getKey() == Ext.EventObject.TAB){e.preventDefault();}
    		if(num > 0 && (e.getKey() == Ext.EventObject.ENTER || e.getKey() == Ext.EventObject.TAB)){
    			if(!e.shiftKey){num = (num+iNum > max) ? num+iNum-max : num+iNum;}
    			else{num = (num-iNum < 1) ? num-iNum+max : num-iNum;}
    			for(var i in items){if(items[i].sortNum == num){items[i].focus(false,100);break;}}
    			}
    		}
    	},
    	hasReadOnly:false,labelAlign:'left'
    	},{
            xtype:"textfield",
            name:"RBACUser_HREmployee_Id",
            fieldLabel:"主键ID",
            labelWidth:60,
            labelStyle:"font-style:normal;",
            width:160,
            height:22,
            itemId:"RBACUser_HREmployee_Id",
            x:5,
            y:135,
            readOnly:false,
            appComID:"",
            treeNodeID:"",
            value:"",
            isFunctionBtn:false,
            hidden:true,
            sortNum:6,
            listeners:{
                scope:this,
                specialkey:function(field, e) {
                    var iNum = 1;
                    var sNumField = "sortNum";
                    var form = field.ownerCt;
                    var num = field[sNumField];
                    var items = form.items.items;
                    var max = 9;
                    if (e.getKey() == Ext.EventObject.ENTER || e.getKey() == Ext.EventObject.TAB) {
                        e.preventDefault();
                    }
                    if (num > 0 && (e.getKey() == Ext.EventObject.ENTER || e.getKey() == Ext.EventObject.TAB)) {
                        if (!e.shiftKey) {
                            num = num + iNum > max ? num + iNum - max :num + iNum;
                        } else {
                            num = num - iNum < 1 ? num - iNum + max :num - iNum;
                        }
                        for (var i in items) {
                            if (items[i].sortNum == num) {
                                items[i].focus(false, 100);
                                break;
                            }
                        }
                    }
                }
            },
            hasReadOnly:false,
            labelAlign:"left"
        }, {
            xtype:"textfield",
            name:"RBACUser_HREmployee_DataTimeStamp",
            fieldLabel:"时间戳",
            labelWidth:60,
            labelStyle:"font-style:normal;",
            width:160,
            height:22,
            itemId:"RBACUser_HREmployee_DataTimeStamp",
            x:5,
            y:161,
            readOnly:false,
            appComID:"",
            treeNodeID:"",
            value:"",
            isFunctionBtn:false,
            hidden:true,
            sortNum:7,
            listeners:{
                scope:this,
                specialkey:function(field, e) {
                    var iNum = 1;
                    var sNumField = "sortNum";
                    var form = field.ownerCt;
                    var num = field[sNumField];
                    var items = form.items.items;
                    var max = 9;
                    if (e.getKey() == Ext.EventObject.ENTER || e.getKey() == Ext.EventObject.TAB) {
                        e.preventDefault();
                    }
                    if (num > 0 && (e.getKey() == Ext.EventObject.ENTER || e.getKey() == Ext.EventObject.TAB)) {
                        if (!e.shiftKey) {
                            num = num + iNum > max ? num + iNum - max :num + iNum;
                        } else {
                            num = num - iNum < 1 ? num - iNum + max :num - iNum;
                        }
                        for (var i in items) {
                            if (items[i].sortNum == num) {
                                items[i].focus(false, 100);
                                break;
                            }
                        }
                    }
                }
            },
            hasReadOnly:false,
            labelAlign:"left"
        }, {
            xtype:"textfield",
            name:"RBACUser_Id",
            fieldLabel:"主键ID",
            labelWidth:60,
            labelStyle:"font-style:normal;",
            width:160,
            height:22,
            itemId:"RBACUser_Id",
            x:5,
            y:187,
            readOnly:false,
            appComID:"",
            treeNodeID:"",
            value:"",
            isFunctionBtn:false,
            hidden:true,
            sortNum:8,
            listeners:{
                scope:this,
                specialkey:function(field, e) {
                    var iNum = 1;
                    var sNumField = "sortNum";
                    var form = field.ownerCt;
                    var num = field[sNumField];
                    var items = form.items.items;
                    var max = 9;
                    if (e.getKey() == Ext.EventObject.ENTER || e.getKey() == Ext.EventObject.TAB) {
                        e.preventDefault();
                    }
                    if (num > 0 && (e.getKey() == Ext.EventObject.ENTER || e.getKey() == Ext.EventObject.TAB)) {
                        if (!e.shiftKey) {
                            num = num + iNum > max ? num + iNum - max :num + iNum;
                        } else {
                            num = num - iNum < 1 ? num - iNum + max :num - iNum;
                        }
                        for (var i in items) {
                            if (items[i].sortNum == num) {
                                items[i].focus(false, 100);
                                break;
                            }
                        }
                    }
                }
            },
            hasReadOnly:false,
            labelAlign:"left"
        }, {
            xtype:"textfield",
            name:"RBACUser_DataTimeStamp",
            fieldLabel:"时间戳",
            labelWidth:60,
            labelStyle:"font-style:normal;",
            width:160,
            height:22,
            itemId:"RBACUser_DataTimeStamp",
            x:5,
            y:213,
            readOnly:false,
            appComID:"",
            treeNodeID:"",
            value:"",
            isFunctionBtn:false,
            hidden:true,
            sortNum:9,
            listeners:{
                scope:this,
                specialkey:function(field, e) {
                    var iNum = 1;
                    var sNumField = "sortNum";
                    var form = field.ownerCt;
                    var num = field[sNumField];
                    var items = form.items.items;
                    var max = 9;
                    if (e.getKey() == Ext.EventObject.ENTER || e.getKey() == Ext.EventObject.TAB) {
                        e.preventDefault();
                    }
                    if (num > 0 && (e.getKey() == Ext.EventObject.ENTER || e.getKey() == Ext.EventObject.TAB)) {
                        if (!e.shiftKey) {
                            num = num + iNum > max ? num + iNum - max :num + iNum;
                        } else {
                            num = num - iNum < 1 ? num - iNum + max :num - iNum;
                        }
                        for (var i in items) {
                            if (items[i].sortNum == num) {
                                items[i].focus(false, 100);
                                break;
                            }
                        }
                    }
                }
            },
            hasReadOnly:false,
            labelAlign:"left"
        }];
    	if(me.type == 'show'){me.height -= 25;}
    	else{
    		me.dockedItems=[{
    			xtype:'toolbar',dock:'bottom',itemId:'buttons',
    			items:['->',{xtype:'button',text:'保存',iconCls:'button-save',
    				handler:function(){
    				 var Account=me.getComponent('RBACUser_Account').getValue();
                     var EnMPwd=me.getComponent('RBACUser_EnMPwd').getValue();
                     var AccBeginTime=me.getComponent('RBACUser_AccBeginTime').getValue();
                     var AccBeginTimeFormat=''+ Ext.util.Format.date(AccBeginTime,'Y-m-d');
                     var  BeginValue=convertJSONDateToJSDateObject(AccBeginTimeFormat);
                     var AccEndTime=me.getComponent('RBACUser_AccEndTime').getValue();
                     var EndValueFormat=''+ Ext.util.Format.date(AccEndTime,'Y-m-d');
                     var  EndValue=convertJSONDateToJSDateObject(EndValueFormat);
                     var Id=me.getComponent('RBACUser_Id').getValue();            
                     var objdata={RBACUser_Id:Id,RBACUser_Account:Account,RBACUser_EnMPwd:EnMPwd ,RBACUser_AccBeginTime:BeginValue,RBACUser_AccEndTime:EndValue};
                     me.updateUser(objdata); 
    				
    			}
    			},{xtype:'button',text:'重置',iconCls:'build-button-refresh',
    				handler:function(){me.getForm().reset();}
    			}]
    		}];
    	}
    	me.callParent(arguments);
    },
    afterRender:function(){
    	var me=this;
    	me.callParent(arguments);
    	if(Ext.typeOf(me.callback)=='function'){
    		me.callback(me);
    	}
    	if (me.userTpe=='present'){
    		me.getServer();
    	}else if(me.userTpe=='amdin'){
    		me.load();
    	}
    },
    /**
     * 根据传进来的用户取出用户后台的值
     * @private
     */
    getServer:function() {
        var me = this;
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            async:false,
            //非异步
            url:me.PresentUrl,
            method:'GET',
            success: function (response) {
                var result = Ext.JSON.decode(response.responseText);
                if (result.success) {
            	   if(result.ResultDataValue && result.ResultDataValue != ""){
                       var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
                       if(ResultDataValue.RBACUserList&&ResultDataValue.RBACUserList!=""&&ResultDataValue.RBACUserList!=undefined)
                       {
	                    	var Account = ResultDataValue.RBACUserList[0]['Account'];
	                    	var PWD = ResultDataValue.RBACUserList[0]['PWD'];
	                    	var EnMPwd = ResultDataValue.RBACUserList[0]['EnMPwd'];
	                    	var AccBeginTime = ResultDataValue.RBACUserList[0]['AccBeginTime'];
	                    	var AccEndTime = ResultDataValue.RBACUserList[0]['AccEndTime'];
	                    	var DataTimeStamp = ResultDataValue.RBACUserList[0]['DataTimeStamp'];
	                    	var Id = ResultDataValue.RBACUserList[0]['Id'];
                       }
                       else
                       {
                            var Account ='';
                            var PWD ='';
                            var EnMPwd ='';
                            var AccBeginTime ='';
                            var AccEndTime ='';
                            var DataTimeStamp ='';
                            var Id ='';
                        
                       }

                       //取表单的itemId
                        var RBACUser_Account=me.getComponent('RBACUser_Account');
                        var RBACUser_EnMPwd=me.getComponent('RBACUser_EnMPwd');
                        var RBACUser_AccBeginTime=me.getComponent('RBACUser_AccBeginTime');
                        var RBACUser_AccEndTime=me.getComponent('RBACUser_AccEndTime');
                        var RBACUser_Id=me.getComponent('RBACUser_Id');
                      //赋值
                        RBACUser_Account.setValue(Account);
                        RBACUser_EnMPwd.setValue(PWD);
                        RBACUser_AccBeginTime.setValue(AccBeginTime);
                        RBACUser_AccEndTime.setValue(AccEndTime);
                        RBACUser_Id.setValue(Id);
                    }
                }
            },
            failure:function(response, options) {
                Ext.Msg.alert('提示', '获取信息请求失败！');
            }
       });
    },
  //登录名验证
    validator: function (value,callback) {
    	var me=this;
        var validator = this;
        var error = true;
        Ext.Ajax.request({
            async: false,
            scope: validator,
            url:me.CheckUserNameUrl+ '?strUserAccount=' + value,
            method: 'GET',
            success: function (response) {
                var result = Ext.JSON.decode(response.responseText);
                if (result.success) {
                     if (result.ResultDataValue && result.ResultDataValue != "") {
                         var data = Ext.JSON.decode(result.ResultDataValue);
                         if(Ext.typeOf(callback) == "function"){
                                callback(data);//回调函数
                            }
                    }else{
                            Ext.Msg.alert('提示','没有获取到应用信息！');
                        }
                    //Ext.Msg.alert('提示', '该名称己经存在,请重新输入！');
                    //var imageid=me.getComponent('yzimage');
                    //imageid.getEl().dom.src=getRootPath()+'/'+'ui/css/images/png/64x64/delete.png';
                }else{
                        Ext.Msg.alert('提示','获取应用信息失败！错误信息【<b style="color:red">'+ result.ErrorInfo +"</b>】");
                    }
                
            },
            failure:function(response, options) {
                Ext.Msg.alert('提示', '获取信息请求失败！');
                //var imageid=me.getComponent('yzimage');
                //imageid.getEl().dom.src=getRootPath()+'/'+'ui/css/images/png/64x64/accept.png';
            }
        });
        return error;
    },
    //更改
    updateUser:function(strobj){       
    	var me = this;
    	var myUrl = "";
    	if (me.editDataServerUrl != "") {
    	    myUrl = getRootPath() + "/" + me.editDataServerUrl;
    	}
    	var values = strobj;
    	var maxLength = 0;
    	//循环判断找出字段中与'_'分隔的数组最大长度如：HREmployee_DataTimeStamp为2
    	for (var i in values) {
    	    var arr = i.split("_");
    	    if (arr.length > maxLength) {
    	        maxLength = arr.length;
    	    }
    	}
    	var obj = {};
    	var addObj = function(key, num, value) {
    	    var keyArr = key.split("_");
    	    var ob = "obj";
    	    var objSJC = "";
    	    //保存时间戳
    	    for (var i = 1; i < keyArr.length; i++) {
    	        //获取保存到数据库的字段
    	        ob = ob + '["' + keyArr[i] + '"]';
    	        objSJC = keyArr[i];
    	        if (!eval(ob)) {
    	            eval(ob + "={};");
    	        }
    	    }
    	    //对应字段赋值
    	    if (keyArr.length == num + 1) {
    	        if (objSJC == "DataTimeStamp") {
    	            value = value.split(",");
    	        }
    	        eval(ob + "=value;");
    	    }
    	};
    	for (var i = 1; i < maxLength; i++) {
    	    for (var j in values) {
    	        var value = values[j];
    	        addObj(j, i, value);
    	    }
    	}
    	var field = "";
    	if (maxLength == 2) {
    	    for (var i in values) {
    	        var keyArr = i.split("_");
    	        field = field + keyArr[1] + ",";
    	    }
    	}
    	if (field != "") {
    	    field = field.substring(0, field.length - 1);
    	}
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            async:false,//非异步
            url:myUrl,
            params:Ext.JSON.encode({entity:obj,fields:field}),
            method:'POST',
            timeout:7000,
            success:function(response,opts){
                var result = Ext.JSON.decode(response.responseText);                
                 me.fireEvent("saveClick");
                if(result.success){
                    Ext.Msg.alert('提示','保存成功！'); /*
                    if(me.isClose==true){
                         me.close();
                      }*/
                }else{
                    Ext.Msg.alert('提示','保存应用组件失败！错误信息【<b style="color:red">'+ result.ErrorInfo +"</b>】");
                }
            },
            failure : function(response,options){ 
                Ext.Msg.alert('提示','保存应用组件请求失败！');
            }
        });       
    },
    OverrideField:function(){
    	var nn=Ext.layout.component.field.Field.override({
    	    getErrorStrategy:function() {
            var me = this, owner = me.owner, strategies = me.errorStrategies, msgTarget = owner.msgTarget;
            var strategy = !owner.preventMark && Ext.isString(msgTarget) ? strategies[msgTarget] || strategies.elementId :strategies.none;
            if (msgTarget == "side" && owner.showValidIcon) {
                if (owner.isIconInit) {
                    owner.errorEl.setDisplayed(false);
                    owner.isIconInit = true;
                }
                owner.on("validitychange", function(me, valid) {
                    me.errorEl.setDisplayed(true);
                });
                Ext.apply(strategy, {
                    adjustHorizInsets:Ext.emptyFn,
                    layoutHoriz:function(ownerContext, owner, size) {
                        ownerContext.errorContext.setProp("x", size.width);
                    },
                    layoutVert:function(ownerContext, owner) {
                        ownerContext.errorContext.setProp("y", ownerContext.insets.top);
                    },
                    prepare:function(ownerContext, owner) {
                        var errorEl = owner.errorEl;
                        errorEl.addCls(Ext.baseCSSPrefix + "form-invalid-icon");
                        errorEl.set({
                            "data-errorqtip":owner.getActiveError() || ""
                        });
                        var activeError = owner.getActiveError();
                        var hasError = !!activeError;
                        errorEl[hasError ? "removeCls" :"addCls"]("icon-yes");
                        Ext.layout.component.field.Field.initTip();
                    }
                });
            }
            return strategy;
        }
    });
    	return nn;
    },
    /**
     * 验证账号名长度
     * @private
     */
    LenTypes:function(){
    	var bb=Ext.apply(Ext.form.VTypes,{
    		isLen:function(val,field){
		    //返回true，则验证通过，否则验证失败
 			var exp=/^[a-zA-Z0-9_]{3,13}$/;
				var reg = val.match(exp);
					if(reg==null){
						return false;
					}else{
						return true;
					}
     			},
     			isLenText: '3-12位不能重复的账号'
      });
      return bb;
    }
});
