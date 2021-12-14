Ext.ns('Ext.zhifangux');
	Ext.define('Ext.zhifangux.juesexinxiApp', {
	    extend:"Ext.panel.Panel",
	    panelType:"Ext.panel.Panel",
	    alias:"widget.juesexinxiApp",
	    title:"角色信息",
	    width:865,
	    height:353,
	    layout:{
	        type:"border",
	        regionWeights:{
	            north:4,
	            east:5
	        }
	    },
		/**
		 * 根据中文获取拼音字头服务地址
		 * @type 
		 */
		getPinYinZiTouServerUrl:getRootPath()+'/ConstructionService.svc/GetPinYin',
	    
		getAppInfoServerUrl:getRootPath() + "/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById",
	    appInfos:[ {
	        x:436,
	        width:427,
	        height:326,
	        appId:"5264508789072395067",
	        header:true,
	        itemId:"jueseForm",
	        title:"角色信息",
	        region:"east",
	        split:true,
	        collapsible:true,
	        collapsed:false,
	        border:true,
	        sequencenum:0,
	        defaultactive:false
	    }, {
	        y:112,
	        width:431,
	        height:214,
	        appId:"5211717749716748252",
	        header:true,
	        itemId:"jueseList",
	        title:"角色列表",
	        region:"center",
	        split:false,
	        collapsible:false,
	        collapsed:false,
	        border:true,
	        sequencenum:0,
	        defaultactive:false
	    } ],
	    comNum:0,
	    afterRender:function() {
	        var me = this;
	        me.callParent(arguments);
	        me.createItems();
	        
	    },
	    createItems:function() {
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
	                //var ModuleOperCode = obj.appInfo.BTDAppComponents_ModuleOperCode;
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
	    /**
	     * 更改中文名称时处理--拼音字头
	     * 快捷码和拼音字头自动生成
	     * interactionFields:交互字段
	     * @private
	     */
	    setPinYinZiTouValue:function(newValue) {
	        var me = this;
	        var changePinYinZiTou = function(newValue) {
	            var jueseForm = me.getComponent("jueseForm");
	            var PinYinZiTou = jueseForm.getComponent('RBACRole_PinYinZiTou');
	            if (newValue != '' && newValue != null) {
	                //替换返回值的空格
	                newValue = newValue.replace(/ /g, '');
	            }
	            if (PinYinZiTou) {
	                PinYinZiTou.setValue(newValue);
	            }
	            var Shortcode = jueseForm.getComponent('RBACRole_Shortcode');
	            if (Shortcode) {
	                //快捷码
	                Shortcode.setValue(newValue);
	            }
	        };
	        if (newValue != '') {
	            getPinYinZiTouFromServer(newValue, changePinYinZiTou);
	        } else {

	            var jueseForm = me.getComponent('jueseForm');
	            var PinYinZiTou = jueseForm.getComponent('RBACRole_PinYinZiTou');
	            if (PinYinZiTou) {
	                PinYinZiTou.setValue('');
	            }
	            var Shortcode = jueseForm.getComponent('RBACRole_Shortcode');
	            if (Shortcode) {
	                //快捷码
	                Shortcode.setValue('');
	            }
	        }
	    },
	    initLink:function(panel) {
	        var me = this;
	        if (Ext.typeOf(me.callback) == "function") {
	            me.callback(me);
	        }
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
	            var _jueseList = me.getComponent("jueseList");
	            _jueseList.on({
	                itemclick:function(view, record) {
	                    var id = record.get(_jueseList.objectName + "_Id");
	                    var _jueseForm = me.getComponent("jueseForm");
	                    _jueseForm.load(id);
	                }
	            });
	            var _jueseList = me.getComponent("jueseList");
	            _jueseList.on({
	                addClick:function(but) {
	                    var _jueseForm = me.getComponent("jueseForm");
	                    _jueseForm.isAdd();
	                }
	            });
	            var _jueseList = me.getComponent("jueseList");
	            _jueseList.on({
	                editClick:function(but) {
	                    var list = _jueseList;
	                    var records = list.getSelectionModel().getSelection();
	                    if (records.length == 1) {
	                        var record = records[0];
	                        var id = record.get(_jueseList.objectName + "_Id");
	                        var _jueseForm = me.getComponent("jueseForm");
	                        _jueseForm.isEdit(id);
	                    } else {
	                        alertError("请选择一条数据进行操作！");
	                    }
	                }
	            });
	            var _jueseList = me.getComponent("jueseList");
	            _jueseList.on({
	                showClick:function(but) {
	                    var list = _jueseList;
	                    var records = list.getSelectionModel().getSelection();
	                    if (records.length == 1) {
	                        var record = records[0];
	                        var id = record.get(_jueseList.objectName + "_Id");
	                        var _jueseForm = me.getComponent("jueseForm");
	                        _jueseForm.isShow(id);
	                    } else {
	                        alertError("请选择一条数据进行操作！");
	                    }
	                }
	            });
	            var _jueseList = me.getComponent("jueseList");
	            _jueseList.on({
	                select:function(view, record) {
	                    var id = record.get(_jueseList.objectName + "_Id");
	                    var _jueseForm = me.getComponent("jueseForm");
	                    _jueseForm.load(id);
	                }
	            });
	            var _jueseForm = me.getComponent("jueseForm");
	            _jueseForm.on({
	                saveClick:function(but) {
	                    var _jueseList = me.getComponent("jueseList");
	                    _jueseList.load();
	                }
	            });
	            var jueseForm = me.getComponent("jueseForm");
	            var I=0;
	            //名称
	            var CName = jueseForm.getComponent("RBACRole_CName");
	            //拼音字头
	            var PinYinZiTou = jueseForm.getComponent("RBACRole_PinYinZiTou");  
	            //快捷码
	            var Shortcode = jueseForm.getComponent("RBACRole_Shortcode");
	            //使用
	            var IsUse = jueseForm.getComponent("RBACRole_IsUse");
	            //显示次序
	            var DispOrder = jueseForm.getComponent("RBACRole_DispOrder");
	            //角色列表
	            var jueseList = me.getComponent("jueseList");
	           
	            jueseForm.on({
	                saveClick:function(){
			            //获取表单所有值
			            var paramsfrom=jueseForm.getForm().getValues();
			            var RBACRoleIdkey='RBACRole_Id';  //员工ID
			            var RBACRoleId=paramsfrom[RBACRoleIdkey];
			            if(RBACRoleId!=''){
			            	jueseList.autoSelect=RBACRoleId;
			                //新增、修改后保存更新部门员工列表
			            	jueseList.load(true);
			            }              
	                }
	            });
	            var maxValue=[];
	            var storeList=jueseList.getStore();
	           
	            jueseList.on({
	            	select:function(){
	            	    maxValue=[];
	            	    storeList.each(function(record) {
	            	    	
	            	        if(record.get("RBACRole_DispOrder")) { 
	       			    	    var dd='';
	       			    	    dd=parseInt(record.get("RBACRole_DispOrder"));  
	       			    	    maxValue.push(dd);
	            	    	 }
	                     });
				    	 I= Ext.Array.max(maxValue);
	                },
	                addClick:function(){
	            	    var  bb=parseInt(I) +1;
	            	    IsUse.setValue(true);
	            	    DispOrder.setValue(bb);
	                }
	            });
	            CName.on({
	            	change:function(field, newValue, oldValue, eOpts) {
		                if(newValue!=''&&jueseForm.isLoadingComplete==true&&(jueseForm.type=='edit'||jueseForm.type=='add')){
		                    me.setPinYinZiTouValue(newValue);
		                }else{
		                	jueseForm.isLoadingComplete=true;
		                }
		            }
	            });
	           
	        }
	    },
	    getAppInfoFromServer:function(id, callback) {
	        var me = this;
	        var url = me.getAppInfoServerUrl + "?fields=BTDAppComponents_ClassCode&isPlanish=true&id=" + id;
	        Ext.Ajax.defaultPostHeader = "application/json";
	        Ext.Ajax.request({
	            async:false,
	            url:url,
	            method:"GET",
	            timeout:2e3,
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
	                            ErrorInfo:result.ErrorInfo
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
	    }
	});