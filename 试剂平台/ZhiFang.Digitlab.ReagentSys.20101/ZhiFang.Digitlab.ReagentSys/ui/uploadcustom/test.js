Ext.define("yuangongyuzhanghao", {
    extend:"Ext.panel.Panel",
    panelType:"Ext.panel.Panel",
    alias:"widget.yuangongyuzhanghao",
    title:"员工与账号",
    width:875,
    height:382,
    layout:{
        type:"border",
        regionWeights:{east:1}
    },
    fields:'RBACUser_Id,RBACUser_UseCode,RBACUser_Account,RBACUser_PWD,RBACUser_EnMPwd,RBACUser_PwdExprd,RBACUser_HREmployee_Id,RBACUser_HREmployee_DataTimeStamp,' +
            'RBACUser_AccExprd,RBACUser_AccLock,RBACUser_AuUnlock,RBACUser_LoginTime,RBACUser_AccBeginTime,RBACUser_AccEndTime',
    //获取用户信息
    getAppInfoServerUrl:getRootPath() + "/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById",
    resetPasswordUrl:getRootPath() +'/RBACService.svc/RBAC_RJ_ResetAccountPassword',
    //修改用户信息
    editDataServerUrl:'RBACService.svc/RBAC_UDTO_UpdateRBACUserByField',
    appInfos:[ {
        width:460,
        appId:"5553644655354790415",
        header:false,
        itemId:"yuangongyuzhanghuTab",
        title:"员工账户",
        region:"east",
        split:true,
        collapsible:true,
        collapsed:false,
        border:true
    }, {
        appId:"4891921432055379413",
        header:false,
        itemId:"yuangongListTab",
        title:"员工列表",
        region:"center",
        border:true
    } ],
    comNum:0,
    afterRender:function() {
        var me = this;
        me.callParent(arguments);
        me.create();
    },
    create:function() {
        var me = this;
        var appInfos = me.getAppInfos();
        for (var i in appInfos) {
            var id = appInfos[i].appId;
            var callback = me.getCallback(appInfos[i]);
            me.getAppInfoFromServer(id, callback);
        }
    },
    getCallback:function(appInfo) {
        var me = this;
        var callback = function(obj) {
            if (obj.success && obj.appInfo != "") {
                var ModuleOperCode = obj.appInfo.BTDAppComponents_ModuleOperCode;
                var ClassCode = obj.appInfo.BTDAppComponents_ClassCode;
                var cl = eval(ClassCode);
                var callback2 = function(panel) {
                    me.initLink(panel);
                };
                appInfo.callback = callback2;
                var panel = Ext.create(cl, appInfo);
                me.add(panel);
                if (me.panelType == "Ext.tab.Panel") {
                    if (appInfo.defaultactive) {
                        me.defaultactive = appInfo.itemId;
                    }
                    me.setActiveTab(panel);
                }
            } else {
                appInfo.html = obj.ErrorInfo;
                var panel = Ext.create("Ext.panel.Panel", appInfo);
                me.add(panel);
                if (me.panelType == "Ext.tab.Panel") {
                    if (appInfo.defaultactive) {
                        me.defaultactive = appInfo.itemId;
                    }
                    me.setActiveTab(panel);
                }
            }
        };
        return callback;
    },
    getAppInfos:function() {
        var me = this;
        var appInfos = me.appInfos;
        for (var i in appInfos) {
            if (appInfos[i].title == "") {
                delete appInfos[i].title;
            } else if (appInfos[i].title == "_") {
                appInfos[i].title = "";
            }
        }
        return Ext.clone(appInfos);
    },
    initLink:function(panel) {
        var me = this;
        var appInfos = me.getAppInfos();
        var length = appInfos.length;
        me.comNum++;
        if (me.comNum == length) {
            if (me.panelType == "Ext.tab.Panel") {
                var f = function() {
                    me.setActiveTab(me.defaultactive);
                    me.un("tabchange", f);
                };
                me.on("tabchange", f);
            }
            var allUserInfo="";  //当前用户ID
            var treeData="";     //获取当前选择树的结点信息
            var listRecord="";   //获取当前选中列表行记录
            var _yuangongListTab_yuangongListRead = me.getComponent("yuangongListTab").getComponent("yuangongListRead");
            
            //员工账号表单
            var ygzhxxform=me.getComponent("yuangongyuzhanghuTab").getComponent("zhanghaoxianshiForm");            
            
            //员工信息表单ID
            var ygxxForm = me.getComponent("yuangongyuzhanghuTab").getComponent("yuangongxxForm");       
            
            //拼音头字母处理
            var hRHREmployeeNameL= ygxxForm.getComponent('HREmployee_NameL');
            var hRHREmployeeNameF= ygxxForm.getComponent('HREmployee_NameF');
            var hREmployeeCName= ygxxForm.getComponent('HREmployee_CName');
            //员工姓汉字输入
            if (hRHREmployeeNameL && hRHREmployeeNameL != undefined) {
                hRHREmployeeNameL.on({
					change:function(field, newValue, oldValue, eOpts) {
			        	if (ygxxForm.isLoadingComplete==true&&(ygxxForm.type=='edit'||ygxxForm.type=='add')){
			            	var NameF= hRHREmployeeNameF.getValue();
			                if(NameF != null && NameF != ''){
				                me.setPinYinZiTouValue(newValue+NameF);
				               	hREmployeeCName.setValue(newValue+NameF);
				            }else{
		                       	me.setPinYinZiTouValue(newValue);
		                       	hREmployeeCName.setValue(newValue);
							}
						}
					}
				});
			}
            //员工名汉字输入
            if (hRHREmployeeNameF && hRHREmployeeNameF != undefined) {
            	hRHREmployeeNameF.on({
                	change:function(field, newValue, oldValue, eOpts) {
                    	if (ygxxForm.isLoadingComplete==true&&(ygxxForm.type=='edit'||ygxxForm.type=='add')){
                      		var NameL= hRHREmployeeNameL.getValue();
                            if(NameL != null && NameL != ''){
                               	me.setPinYinZiTouValue(NameL+newValue);
                               	hREmployeeCName.setValue(NameL+newValue);
                            }else{
                               	me.setPinYinZiTouValue(newValue);
                               	hREmployeeCName.setValue(newValue);
                            }
                        }
                    }
                });
            }
            
      		//员工信息表单
      		ygxxForm.on({        
        		saveClick:function(){
		            //获取表单所有值
		            var params=ygxxForm.getForm().getValues();
		            var userkey=ygxxForm.objectName+'_Id';
		            var userid=params[userkey];
            		if(userid!=''){
	            		_yuangongListTab_yuangongListRead.autoSelect=userid;
	            		//新增、修改后保存更新部门员工列表
	            		_yuangongListTab_yuangongListRead.load(true);
            		}
         		},
         		//保存前检验
         		beforeSave:function() {
            		var employeeNameL= ygxxForm.getComponent('HREmployee_NameL');
            		var employeeNameF= ygxxForm.getComponent('HREmployee_NameF');
            		//员工姓为空时处理
            		if (employeeNameL) {
                		var txtValue =employeeNameL.getValue();
                		if (txtValue == null || txtValue == '' || txtValue == undefined){
                    		alertError('员工姓不能为空！');
                    		return false;
                		}
             		}
           			//员工名字为空时处理
            		if (employeeNameF) {
                	var value =employeeNameF.getValue();
                	if (value == null || value == '' || value == undefined){
                    	alertError('员工名字不能为空！');
                    	return false;
                	}
             	}
         	}
      	});
      	//员工列表监听事件
      	_yuangongListTab_yuangongListRead.store.on({
        	load:function(store,records){
           		var record=records;
           		if(store.data.length==0){
               		//alert("树的选择对象！");
                	var _yuangongyuzhanghuTab_yuangongxxForm = me.getComponent("yuangongyuzhanghuTab").getComponent("yuangongxxForm");
                	var _yuangongyuzhanghuTab_zhanghaoxianshiForm=me.getComponent("yuangongyuzhanghuTab").getComponent("zhanghaoxianshiForm");
	                //清空表单信息
	                _yuangongyuzhanghuTab_yuangongxxForm.getForm().reset();
	                _yuangongyuzhanghuTab_zhanghaoxianshiForm.getForm().reset();
	                
	                //获取账号表单按钮
	                //btnAdd账号申请；btnUse账号停用；btnUpdate账号更新；btnPassWord密码重置
	                var zhanghaoxianshiForm=me.getComponent("yuangongyuzhanghuTab").getComponent("zhanghaoxianshiForm");
	                var btnAdd=zhanghaoxianshiForm.getComponent('btnAdd');
	                var btnUse=zhanghaoxianshiForm.getComponent('btnUse');
	                var btnUpdate=zhanghaoxianshiForm.getComponent('btnUpdate');
	                var btnPassWord=zhanghaoxianshiForm.getComponent('btnPassWord');
	                
	                btnAdd.disable(true);
	                btnUse.disable(true);
	                btnUpdate.disable(true);
	                btnPassWord.disable(true);
           		} 
        	}
      	});
	 	_yuangongListTab_yuangongListRead.on({
	        select:function(view, record) {
	            var id = record.get(_yuangongListTab_yuangongListRead.objectName + "_Id");
	            me.listRecord=record[0];
	            //员工信息表单
	            var _yuangongyuzhanghuTab_yuangongxxForm = me.getComponent("yuangongyuzhanghuTab").getComponent("yuangongxxForm");
	            	            
	            if (record != null) {
	                Id = record.get(_yuangongListTab_yuangongListRead.objectName + "_Id");
	                 _yuangongyuzhanghuTab_yuangongxxForm.load(id);
	                //解决部门名称显示部门ID    姓别ID：HREmployee_BSex_Id
	                me.setDaptValue(record);
	            }
	            //员工账号表单
	            var _yuangongyuzhanghuTab_zhanghaoxianshiForm=me.getComponent("yuangongyuzhanghuTab").getComponent("zhanghaoxianshiForm");
	            
	            var getUserInfoUrl=getRootPath() + "/RBACService.svc/RBAC_UDTO_SearchRBACUserListByHQL?&isPlanish=true"+
                	"&fields="+me.fields+"&where="+"rbacuser.HREmployee.Id="+id;//RBACUser_HREmployee_Id                	                                      
	          	var callback = function(accountInfo) {
	                if (accountInfo && accountInfo != "") {
	                    //员工与账号信息
	                    var userInfo=accountInfo;
	                    allUserInfo=accountInfo;
	                    var txtaccount=_yuangongyuzhanghuTab_zhanghaoxianshiForm.getComponent('RBACUser_Account');
	                    var txtAccBeginTime=_yuangongyuzhanghuTab_zhanghaoxianshiForm.getComponent('RBACUser_AccBeginTime');
	                    var txtAccEndTime=_yuangongyuzhanghuTab_zhanghaoxianshiForm.getComponent('RBACUser_AccEndTime');
	                    
	                    //允许修改密码RBACUser_EnMPwd
	                    var txtEnMPwd=_yuangongyuzhanghuTab_zhanghaoxianshiForm.getComponent('RBACUser_EnMPwd');
	                    //账号被锁定RBACUser_AccLock
	                    var txtAccLock=_yuangongyuzhanghuTab_zhanghaoxianshiForm.getComponent('RBACUser_AccLock');
	                    
	                    txtaccount.setValue(userInfo['RBACUser_Account']);
	                    txtAccBeginTime.setValue(userInfo['RBACUser_AccBeginTime']);
	                    txtAccEndTime.setValue(userInfo['RBACUser_AccEndTime']);
	                    
	                    txtEnMPwd.setValue(userInfo['RBACUser_EnMPwd']);
	                    txtAccLock.setValue(userInfo['RBACUser_AccLock']);
                        
                        txtEnMPwd.setReadOnly(true);
	                    txtAccLock.setReadOnly(true);
	                    //获取账号表单按钮
	                    //btnAdd账号申请；btnUse账号停用；btnUpdate账号更新；btnPassWord密码重置
	                    var zhanghaoxianshiForm=me.getComponent("yuangongyuzhanghuTab").getComponent("zhanghaoxianshiForm");
	                    var btnAdd=zhanghaoxianshiForm.getComponent('btnAdd');
	                    var btnUse=zhanghaoxianshiForm.getComponent('btnUse');
	                    var btnUpdate=zhanghaoxianshiForm.getComponent('btnUpdate');
	                    var btnPassWord=zhanghaoxianshiForm.getComponent('btnPassWord');
	                    //设置申请单是否可用
	                    if(userInfo['RBACUser_Account']!=""){  
	                        btnAdd.disable(true);
	                        btnUse.enable(true);
	                        btnUpdate.enable(true);
	                        btnPassWord.enable(true);
	                        if(userInfo['RBACUser_AccLock']=="true"){                                   
	                           btnUse.setText('账号启用');                                   
	                        }else{                                    
	                           btnUse.setText('账号停用');
	                        }                             
	                    }else{
	                        btnAdd.enable(true);
	                        btnUse.disable(true);
	                        btnUpdate.disable(true);
	                        btnPassWord.disable(true);
	                    }
	                }
				};
	           	me.getAccountInfo(getUserInfoUrl,callback)       
			},
		    addClick:function(but) {
		        //激活员工信息标签页
		        var formTab=me.getComponent("yuangongyuzhanghuTab");
		        formTab.setActiveTab("yuangongxxForm");
		        
		        var _yuangongyuzhanghuTab_yuangongxxForm = me.getComponent("yuangongyuzhanghuTab").getComponent("yuangongxxForm");
		        _yuangongyuzhanghuTab_yuangongxxForm.isAdd();
                _yuangongyuzhanghuTab_yuangongxxForm.isLoadingComplete=true;
		        var record = null;
		        //me.setParentIDComValue(record,'add'); 
		        me.doAddClick();
			},
	        editClick:function(but) {
	            //激活员工信息标签页
	            var formTab=me.getComponent("yuangongyuzhanghuTab");
	            formTab.setActiveTab("yuangongxxForm");
	            
	            var list = _yuangongListTab_yuangongListRead;
	            var records = list.getSelectionModel().getSelection();
	            if (records.length == 1) {
	                var record = records[0];
	                var id = record.get(_yuangongListTab_yuangongListRead.objectName + "_Id");
	                var _yuangongyuzhanghuTab_yuangongxxForm = me.getComponent("yuangongyuzhanghuTab").getComponent("yuangongxxForm");
	                _yuangongyuzhanghuTab_yuangongxxForm.isEdit(id);
                    _yuangongyuzhanghuTab_yuangongxxForm.isLoadingComplete=true;
	                 //me.setParentIDComValue(record, 'edit');
                     me.setDaptValue(record);
	            } else {
	                alertInfo("请选择一条数据进行操作！");
	            }
	         },
	         saveClick:function(but) {
	            var _yuangongListTab_yuangongListRead = me.getComponent("yuangongListTab").getComponent("yuangongListRead");
	            //_yuangongListTab_yuangongListRead.load();
	         }
	    });
        
        //列表树与列表之间的联动
		var treeId=me.getComponent("yuangongListTab").getComponent("bumenTreeQuery");
        treeId.on({
          	load:function(treeStore, node,records,successful,eOpts){
                //更新树数据的状态标志
                me.bmTreeLoadStore=true;
            },
          	select:function(view, record) {
            	var records = treeId.getSelectionModel().getSelection();
            	//alert("当前选中树结点！");
                var record = null;
                if (records && records.length > 0) {
                    record = records[0];
                    treeData=records[0];
                } else {
                    record = null;
                }
            
          	}
        });
        //监听账户表单
        ygzhxxform.on({
            //监听账号更新信息表单
           	AfterOpenWinbtnUpdate:function(from){
               	//alert('员工账号表单');
	            var id=allUserInfo['RBACUser_Id'];
	            var  hREmployeeid=allUserInfo['RBACUser_HREmployee_Id'];
	            var txtHREmployeeid=from.getComponent("RBACUser_HREmployee_Id");
	            var txtRBACUserAccount=from.getComponent("RBACUser_Account");
	            var dateRBACUserAccBeginTime=from.getComponent("RBACUser_AccBeginTime");
	            var dateRBACUserAccEndTime=from.getComponent("RBACUser_AccEndTime");
	            txtHREmployeeid.setValue(hREmployeeid);  
	            //alert("开始时间："+Ext.Date.format(new Date(allUserInfo['RBACUser_AccBeginTime']),'Y-m-d H:i:s'));
	            var dtBegin=Ext.Date.format(new Date(allUserInfo['RBACUser_AccBeginTime']),'Y-m-d');
	            var dtEnd=Ext.Date.format(new Date(allUserInfo['RBACUser_AccEndTime']),'Y-m-d');
            
            	from.load(id);
	            //保存原账号,在更新时是原账号不做校验
	            var frmparams=ygzhxxform.getForm().getValues();
	            var txtAccount=from.getComponent("txtAccount");
	            var oldAccount=frmparams['RBACUser_Account'];
	            txtAccount.setValue(oldAccount);
	            
	            if(allUserInfo['RBACUser_AccBeginTime']!=''){
                	dateRBACUserAccBeginTime.setValue(dtBegin);
            	}else{
	                var dtstar=Ext.Date.format(new Date(),'Y-m-d');
	                dateRBACUserAccBeginTime.setValue(dtstar);
	            }
                dateRBACUserAccEndTime.setValue(dtEnd);
               	from.on({
                  	saveClick:function(){
	                    from.close();
			            //获取表单所有值
			            var paramsfrom=from.getForm().getValues();
			            var HREmployeeIdkey='RBACUser_HREmployee_Id';  //员工ID
			            var HREmployeeId=paramsfrom[HREmployeeIdkey];
			            if(HREmployeeId!=''){
			                _yuangongListTab_yuangongListRead.autoSelect=HREmployeeId;
			                //新增、修改后保存更新部门员工列表
			                _yuangongListTab_yuangongListRead.load(true);
		            	}              
                  	}
               	})
           	},
           	//监听账号申请信息表单
           	AfterOpenWinbtnAdd:function(form){
                var listId=me.getComponent("yuangongListTab").getComponent("yuangongListRead");
                var records= listId.getSelectionModel().getSelection();
                var record=records[0];
                var ygfrm=form;
                var txtuserid=ygfrm.getComponent("RBACUser_HREmployee_Id");
                
                var txtHREmployeeDataTimeStam=ygfrm.getComponent("RBACUser_HREmployee_DataTimeStamp");
                
                if(record&&record!=""&&record!=undefined){
                   //员工id
                   var ygid=record.data[_yuangongListTab_yuangongListRead.objectName + "_Id"];  //
                   var HREmployeeDataTimeStam=record.data["HREmployee_DataTimeStamp"];//HREmployee_DataTimeStamp
                   //alert('当前列表选中员工ID：'+ygid);
                   txtuserid.setValue(ygid);
                   txtHREmployeeDataTimeStam.setValue(HREmployeeDataTimeStam);
                   //alert('当前文本框员工ID'+txtuserid.getValue());
                }
               form.on({
                saveClick:function()
                  {
                       //获取表单所有值
                    form.close();
                    var paramsfrom=form.getForm().getValues();
                    var HREmployeeIdkey='RBACUser_HREmployee_Id';  //员工ID
                    var HREmployeeId=paramsfrom[HREmployeeIdkey];
                    if(HREmployeeId!='')
                    {
                        _yuangongListTab_yuangongListRead.autoSelect=HREmployeeId;
                        //新增、修改后保存更新部门员工列表
                        _yuangongListTab_yuangongListRead.load(true);
                    } 
                  }
               }) 
                
               
           }
        });
        
        //账号表单
        var zhanghaoxianshiForm=me.getComponent("yuangongyuzhanghuTab").getComponent("zhanghaoxianshiForm");
        var btnAdd=zhanghaoxianshiForm.getComponent('btnAdd');
        var btnUse=zhanghaoxianshiForm.getComponent('btnUse');
        var btnUpdate=zhanghaoxianshiForm.getComponent('btnUpdate');
        var btnPassWord=zhanghaoxianshiForm.getComponent('btnPassWord');
        var txtAccLock=zhanghaoxianshiForm.getComponent('RBACUser_AccLock');
       //监听表单自定义按钮 
      btnUse.on({
		    click:function( but,e,eOpts )
		    {
                //alert('是否可能按钮！'+but.getText());                    
                //alert('用户ID：'+allUserInfo['RBACUser_Id']);
                var boolAccLock="";
		        if(but.getText()=='账号停用')
		        {
		           but.setText('账号启用');
		           boolAccLock=true;
                   txtAccLock.setValue(true);
                   //txtAccLock.setValue('是');
		           //me.updateUserLock(objdata);
		        }
		        else
		        {
		            but.setText('账号停用');
		            boolAccLock=false;
                    txtAccLock.setValue(false);
		         }
               var objdata={RBACUser_Id:allUserInfo['RBACUser_Id'],RBACUser_AccLock:boolAccLock};
               //0只做为保存成功后的弹出提示
               me.updateUserInfo(objdata,me.editDataServerUrl,0);//editDataServerUrl
		    }
		});
        
        //重置密码
      btnPassWord.on({
            click:function( but,e,eOpts )
            { 
	            var userInfodata=allUserInfo;
	            var userId=userInfodata['RBACUser_Id'];
	            if(userId&&userId!=''&&userId!=undefined)
	            {
	              /*  var callback=function(resetPassword)
	                {
	                   var objdata={RBACUser_Id:userId,RBACUser_PWD:resetPassword};
	                    //resetPassword为保存成功后显示更新后的密码
	                   me.updateUserInfo(objdata,me.editDataServerUrl,resetPassword);                 
	                };*/
	              me.resetPassword(userId);
	                
	            }
            }            
        });
                
        if (Ext.typeOf(me.callback) == "function") {
            me.callback(me);
        }
    }
    },
        //获取用户账户信息
	    getAccountInfo:function(getUserInfoUrl,callback){
	        var me = this;
	        Ext.Ajax.defaultPostHeader = 'application/json';
	        Ext.Ajax.request({
	        async:false,//非异步
	        url:getUserInfoUrl,
	        method:'GET',
	        timeout:2000,
	        success:function(response,opts){
	            var result = Ext.JSON.decode(response.responseText);
	            if(result.success){
	                if(result.ResultDataValue && result.ResultDataValue != ""){
	                    var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
	                    if(ResultDataValue.list&&ResultDataValue.list!=""&&ResultDataValue.list!=undefined)
	                    {
	                        var addData = {
	                            RBACUser_Account:ResultDataValue.list[0]['RBACUser_Account'],   //用户账号
	                            RBACUser_PWD:ResultDataValue.list[0]['RBACUser_PWD'],         //用户密码
	                            RBACUser_EnMPwd:ResultDataValue.list[0]['RBACUser_EnMPwd'],   //允许修改密码
	                            RBACUser_AccBeginTime:ResultDataValue.list[0]['RBACUser_AccBeginTime'], 
	                            RBACUser_AccEndTime:ResultDataValue.list[0]['RBACUser_AccEndTime'],
	                            RBACUser_Id:ResultDataValue.list[0]['RBACUser_Id'],
                                RBACUser_HREmployee_Id:ResultDataValue.list[0]['RBACUser_HREmployee_Id'],
                                RBACUser_HREmployee_DataTimeStamp:ResultDataValue.list[0]['RBACUser_HREmployee_DataTimeStamp'],
	                            RBACUser_DataTimeStamp:ResultDataValue.list[0]['RBACUser_DataTimeStamp'],
	                            RBACUser_AccLock:ResultDataValue.list[0]['RBACUser_AccLock'],    //账号被锁定
	                            RBACUser_AuUnlock:ResultDataValue.list[0]['RBACUser_AuUnlock']     //自动解锁
	                        };
	                    }
	                    else
	                    {
	                      var addData = {
	                            RBACUser_Account:'',   //用户账号
	                            RBACUser_PWD:'',         //用户密码
	                            RBACUser_EnMPwd:'',   //允许修改密码
	                            RBACUser_AccBeginTime:'', 
	                            RBACUser_AccEndTime:'',
	                            RBACUser_Id:'',
                                RBACUser_HREmployee_Id:'',
                                RBACUser_HREmployee_DataTimeStamp:'',
	                            RBACUser_DataTimeStamp:'',
	                            RBACUser_AccLock:'',    //账号被锁定
	                            RBACUser_AuUnlock:''     //自动解锁
	                        };  
	                    }
		           /*if (Ext.typeOf(callback) == "function") {
		                if (addData == "") {
		                    Ext.Msg.alert("提示", "没有获取到用户信息！");
		                } else {
		                    callback(addData);
		                }
		            }   */         
	                }else{
	                    //Ext.Msg.alert('提示','没有获取到应用信息！');
                        var addData = {
                                RBACUser_Account:'',   //用户账号
                                RBACUser_PWD:'',         //用户密码
                                RBACUser_EnMPwd:'',   //允许修改密码
                                RBACUser_AccBeginTime:'', 
                                RBACUser_AccEndTime:'',
                                RBACUser_Id:'',
                                RBACUser_HREmployee_Id:'',
                                RBACUser_HREmployee_DataTimeStamp:'',
                                RBACUser_DataTimeStamp:'',
                                RBACUser_AccLock:'',    //账号被锁定
                                RBACUser_AuUnlock:''     //自动解锁
                            };  
	                }                    
                    if (Ext.typeOf(callback) == "function") {
                        if (addData == "") {
                            alertError("没有获取到用户信息！");
                        } else {
                            callback(addData);
                        }
                    } 
                    
	            }else{
	                alertError('获取应用信息失败！错误信息:'+ result.ErrorInfo);
	            }
	        },
	        failure : function(response,options){ 
	            alertError('获取应用信息请求失败！');
	        }
	     });        
	  }, 
  
	    //根据账户ID重置密码
	  resetPassword:function(accountId,callback)
	  {	      
	        var me = this;
	        Ext.Ajax.defaultPostHeader = 'application/json';
	        Ext.Ajax.request({
	        async:false,//非异步
	        url:me.resetPasswordUrl+'?Id='+accountId,
	        method:'GET',
	        timeout:2000,
	        success:function(response,opts){
	            var result = Ext.JSON.decode(response.responseText);
                var AccountPassword="";
	            if(result.success){
	                if(result.ResultDataValue && result.ResultDataValue != ""){
	                    var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
	                    if(ResultDataValue.AccountPassword&&ResultDataValue.AccountPassword!="")
	                    {
                            AccountPassword=ResultDataValue['AccountPassword'];
                            alertInfo('您的新密码为：'+AccountPassword); 
                        }
	              /* if (Ext.typeOf(callback) == "function") {
	                    if (AccountPassword == "") {
	                        Ext.Msg.alert("提示", "没有获取到用户新密码！");
	                    } else {
	                        //callback(AccountPassword);
                            Ext.Msg.alert('提示','您的新密码为：'+AccountPassword); 
	                    }
	                }*/            
	                }else{
	                    alertError('没有获取到应用信息！');
	                }
	            }else{
	                alertError('提示','获取应用信息失败！错误信息:'+ result.ErrorInfo);
	            }
	        },
	        failure : function(response,options){ 
	            alertError('获取应用信息请求失败！');
	        }
	     });
        
	  },
    //设置新增员工表单默认值
    setParentIDComValue:function(record, type) {
        var me = this;
        var treeId=me.getComponent("yuangongListTab").getComponent("bumenTreeQuery");
        //员工信息表单
        var ygxxForm = me.getComponent("yuangongyuzhanghuTab").getComponent("yuangongxxForm");
        //部门管理部门树和部门信息联动后树节点还原问题bumenApp
        var hREmployeeHRDeptId = ygxxForm.getComponent('HREmployee_HRDept_Id');
       if (type == 'add') {            
            //员工是否在职，新增时默认为勾选
            var hREmployeeIsEnabled = ygxxForm.getComponent('HREmployee_IsEnabled');
            //var value=[{valueField:1,displayField:'是'}];
            if(hREmployeeIsEnabled&&hREmployeeIsEnabled!=undefined){
                hREmployeeIsEnabled.setValue('1');
            }
                        
         }
        var records = treeId.getSelectionModel().getSelection();
        var node = null;
        if (records && records.length > 0) {
            node = records[0];
        } else {
            node = null;
        }
        if (node && node != undefined && node != null) {
            if (hREmployeeHRDeptId) {
                var parentNode = null;
                if (type == 'add') {
                    parentNode = node;
                } else {
                    parentNode = node.parentNode;
                }
                var value = '';
                var text = '';
                if (parentNode && parentNode != null) {
                    var parentID = parentNode.data.Id;
                    if (parentID == '' || parentID == null) {
                        value = '';
                        text = '';
                    } else {
                        value = parentID;
                        text = parentNode.data.text;
                    }
                } else {
                    value = '';
                    text = '';
                }
                var arrTemp = [ [ value, text ] ];
                hREmployeeHRDeptId.store = Ext.create('Ext.data.SimpleStore', {
                    fields:[ 'value', 'text' ],
                    data:arrTemp,
                    autoLoad:true
                });
                hREmployeeHRDeptId.setValue(value);
            }
        }
    },
    /**
     * 选择列表的员工所在部门
     * @param {} newValue
     */
    setDaptValue:function(record)
    {
        var me=this;
        //员工信息表单
        var ygxxForm = me.getComponent("yuangongyuzhanghuTab").getComponent("yuangongxxForm");
        //部门管理部门树和部门信息联动后树节点还原问题bumenApp
        var hREmployeeHRDeptId = ygxxForm.getComponent('HREmployee_HRDept_Id');
        //姓别单选组ID
        var hREmployeeBSexId = ygxxForm.getComponent('HREmployee_BSex_Id');
        var node = null;
        if (record && record != undefined && record != null) {
            node=record;
        }        
        //还原姓别
        if(hREmployeeBSexId)
        {
            var rbsex={};
            var rbsexValue='';
            //var rbsexvalue='';
            if(record.data['HREmployee_BSex_Name']=='男')
            {   
                //封装说明；var values={checkboxname:['4']}; 
                //var values={单选组名:值}             
                rbsex={HREmployee_BSex_Id: ['5350598518561423778']};
            }  
             if(record.data['HREmployee_BSex_Name']=='女')
            {
                 rbsex={HREmployee_BSex_Id: ['5480995475585737435']};                
            }
            hREmployeeBSexId.setValue(rbsex);
        }
        
        if (hREmployeeHRDeptId) {
                var parentNode = null;
                parentNode =node;
                var value = '';
                var text = '';
                if (parentNode && parentNode != null) {
                    var parentID = parentNode.data['HREmployee_HRDept_Id'];
                    if (parentID == '' || parentID == null) {
                        value = '';
                        text = '';
                    } else {
                        value = parentID;
                        text = parentNode.data['HREmployee_HRDept_CName'];
                    }
                } else {
                    value = '';
                    text = '';
                }
                var arrTemp = [ [ value, text ] ];
                hREmployeeHRDeptId.store = Ext.create('Ext.data.SimpleStore', {
                    fields:[ 'value', 'text' ],
                    data:arrTemp,
                    autoLoad:true
                });
                hREmployeeHRDeptId.setValue(value);
            }
        
    },
    
     /**
     * 更改中文名称时处理--拼音字头
     * 快捷码和拼音字头自动生成
     * interactionFields:交互字段
     * @private
     */
    setPinYinZiTouValue:function(newValue) {
        var me = this;
        var changePinYinZiTou = function(newValue) {
            //var bmForm = me.getComponent('bumenForm');
            //员工信息表单ID
            var ygxxForm = me.getComponent("yuangongyuzhanghuTab").getComponent("yuangongxxForm");  
            var hREmployeePinYinZiTou = ygxxForm.getComponent('HREmployee_PinYinZiTou');
            if (newValue != '' && newValue != null) {
                //替换返回值的空格
                newValue = newValue.replace(/ /g, '');
            }
            if (hREmployeePinYinZiTou) {
                hREmployeePinYinZiTou.setValue(newValue);
            }
            var hREmployeeShortcode = ygxxForm.getComponent('HREmployee_Shortcode');
            if (hREmployeeShortcode) {
                //快捷码
                hREmployeeShortcode.setValue(newValue);
            }
        };
        if (newValue != '') {
            getPinYinZiTouFromServer(newValue, changePinYinZiTou);
        } else {

            //var bmForm = me.getComponent('bumenForm');
            var ygxxForm = me.getComponent("yuangongyuzhanghuTab").getComponent("yuangongxxForm"); 
            var hREmployeePinYinZiTou = ygxxForm.getComponent('HREmployee_PinYinZiTou');
            if (hREmployeePinYinZiTou) {
                hREmployeePinYinZiTou.setValue('');
            }
             var hREmployeeShortcode = ygxxForm.getComponent('HREmployee_Shortcode');//bmForm
            if (hREmployeeShortcode) {
                //快捷码
                hREmployeeShortcode.setValue('');
            }
        }
    },
    
    //更新账号是否锁定  mark只标记保存成功后提示说明
    updateUserInfo:function(strobj,myUpdataUrl,mark){       
        var me = this; 
        var myUrl='';
        if(myUpdataUrl!="")
        {
            myUrl=getRootPath() + "/"+myUpdataUrl;
        }
        var values=strobj;
        var maxLength = 0;
        //循环判断找出字段中与'_'分隔的数组最大长度如：HREmployee_DataTimeStamp为2
            for(var i in values)
               {
                var arr = i.split('_');
                if(arr.length > maxLength)
                 {
                    maxLength = arr.length;
                 }                   
               }
             var obj = {};
             var addObj = function(key,num,value)
                {
                    var keyArr = key.split('_');
                    var ob = 'obj';
                    var objSJC='';    //保存时间戳
                    for(var i=1;i<keyArr.length;i++)
                    {
                        //获取保存到数据库的字段
                          ob = ob + '[\"' + keyArr[i] + '\"]';
                          objSJC=keyArr[i];
                          if(!eval(ob))
                          {
                             eval(ob + '={};');
                          }
                      }
                      //对应字段赋值
                      if(keyArr.length == num+1)
                        {
                            if(objSJC=='DataTimeStamp')
                            {
                                value=value.split(',');
                            }
                           eval(ob + '=value;');
                        }
                 };
               for(var i=1;i<maxLength;i++)
                {
                     for(var j in values)
                        {
                          var value = values[j];
                          addObj(j,i,value);
                        }
                 }
                 var field = '';
                 if(maxLength == 2)
                    {
                       for(var i in values)
                          {
                              var keyArr = i.split('_');
                              field = field + keyArr[1] + ',';
                          }
                     }
                  if(field != '')
                    {
                        field = field.substring(0,field.length-1);
                     }
        
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            async:false,//非异步
            url:myUrl,//editDataServerUrl
            params:Ext.JSON.encode({entity:obj,fields:field}),
            method:'POST',
            timeout:7000,
            success:function(response,opts){
                var result = Ext.JSON.decode(response.responseText);
                if(result.success){
                    if(mark==0)
                    {
                     alertInfo('保存成功！'); 
                    }
                    if(mark!=0)
                    {
                      alertInfo('您的新密码为：'+mark); 
                    }
                    
                }else{
                    alertError('保存应用组件失败！错误信息:'+ result.ErrorInfo);
                }
            },
            failure : function(response,options){ 
                alertError('保存应用组件请求失败！');
            }
        });       
    },
    
    getAppInfoFromServer:function(id, callback) {
        var me = this;
        var url = me.getAppInfoServerUrl + "?isPlanish=true&id=" + id;
        Ext.Ajax.defaultPostHeader = "application/json";
        Ext.Ajax.request({
            async:false,
            url:url,
            method:"GET",
            timeout:2000,
            success:function(response, opts) {
                var result = Ext.JSON.decode(response.responseText);
                if (result.success) {
                    var appInfo = "";
                    if (result.ResultDataValue && result.ResultDataValue != "") {
                        appInfo = Ext.JSON.decode(result.ResultDataValue);
                    }
                    if (Ext.typeOf(callback) == "function") {
                        var obj = {
                            success:false,
                            ErrorInfo:"没有获取到应用组件信息!"
                        };
                        if (appInfo != "") {
                            obj = {
                                success:true,
                                appInfo:appInfo
                            };
                        }
                        callback(obj);
                    }
                } else {
                    if (Ext.typeOf(callback) == "function") {
                        var obj = {
                            success:false,
                            ErrorInfo:'获取应用组件信息失败！错误信息:' + result.ErrorInfo
                        };
                        callback(obj);
                    }
                }
            },
            failure:function(response, options) {
                if (Ext.typeOf(callback) == "function") {
                    var obj = {
                        success:false,
                        ErrorInfo:"获取应用组件信息请求失败！"
                    };
                    callback(obj);
                }
            }
        });
    },
    doAddClick:function(){
	    var me = this;
	    var form = me.getComponent("yuangongyuzhanghuTab").getComponent("yuangongxxForm");
	    var tree = me.getComponent("yuangongListTab").getComponent("bumenTreeQuery");
	    var records = tree ? tree.getSelectionModel().getSelection() : null;
	    var node = records && records.length == 1 ? records[0] : null;
	    if(node){
	        var obj={
	            itemId:'HREmployee_HRDept_Id',
	            Id:node.data.Id,
	            Name:node.data.CName,
	            DataTimeStamp:node.data.DataTimeStamp
	        };
	        form.setDataComboboxValue(obj);
	    }
        //员工是否在职，新增时默认为勾选
        var hREmployeeIsEnabled = form.getComponent('HREmployee_IsEnabled');
        //var value=[{valueField:1,displayField:'是'}];
        if(hREmployeeIsEnabled&&hREmployeeIsEnabled!=undefined){
            hREmployeeIsEnabled.setValue('1');
        }
	}
});