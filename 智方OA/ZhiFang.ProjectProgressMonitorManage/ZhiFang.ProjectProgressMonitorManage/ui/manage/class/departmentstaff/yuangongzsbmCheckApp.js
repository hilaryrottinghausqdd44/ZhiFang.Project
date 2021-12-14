	/***
	 * 部门管理---员工直属部门选择App
	 */
	Ext.define('yuangongzsbmCheckApp', {
	    extend:'Ext.panel.Panel',
	    panelType:'Ext.panel.Panel',
	    alias:'widget.yuangongzsbmCheckApp',
	    title:'选择员工',
	    //员工所属部门Id(外部传入)
	    hRDeptId:'',
	    //员工所属部门时间戳(外部传入)
	    hRDeptDataTimeStamp:'',
	    appPanel:null,//外部打开当前表单时传入的应用组件,比如可能是列表或者树
	    bmzshql:'',//外部传入HQL
	    header:true,
	    width:631,
	    height:400,
	    layout:{
	        type:'border',
	        regionWeights:{
	            north:4,
	            south:3
	        }
	    },
	    getAppInfoServerUrl:getRootPath() + '/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById',
	    appInfos:[ {
	        y:393,
	        width:629,
	        height:40,
	        appId:'5764169395081335526',
	        header:false,
	        itemId:'querenguanbiForm',
	        title:'',
	        region:'south',
	        split:false,
	        collapsible:false,
	        collapsed:false,
	        border:true,
	        sequencenum:0,
	        defaultactive:false
	    }, {
	        y:99,
	        width:629,
	        height:294,
	        appId:'5152665333035313528',
	        header:false,
	        itemId:'yuangongListCheck',
	        title:'选择员工',
	        region:'center',
	        split:false,
	        collapsible:false,
	        collapsed:false,
	        border:true,
	        sequencenum:0,
	        defaultactive:false
	    }],
	    comNum:0,
	    afterRender:function() {
	        var me = this;
	        me.callParent(arguments);
	        me.createItems();
	        me.on({
	        close:function( panel ,eOpts ){
	            if(me.appPanel&&me.appPanel!=null){
	                //更新部门相关员工列表数据
//	                me.appPanel.load(me.bmxgyghql);
	             }
	        }
	        });
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
	            if (obj.success && obj.appInfo != '') {
	                var ModuleOperCode = obj.appInfo.BTDAppComponents_ModuleOperCode;
	                var ClassCode = obj.appInfo.BTDAppComponents_ClassCode;
	                var cl = eval(ClassCode);
	                var callback2 = function(panel) {
	                    me.initLink();
	                };
	                appInfo.callback = callback2;
	                var panel = Ext.create(cl, appInfo);
	                me.add(panel);
	            } else {
	                appInfo.html = obj.ErrorInfo;
	                var panel = Ext.create('Ext.panel.Panel', appInfo);
	                me.add(panel);
	            }
	        };
	        return callback;
	    },
	    getAppInfos:function() {
	        var me = this;
	        var appInfos = me.appInfos;
	        for (var i in appInfos) {
	            if (appInfos[i].title == '') {
	                delete appInfos[i].title;
	            } else if (appInfos[i].title == '_') {
	                appInfos[i].title = '';
	            }
	        }
	        return Ext.clone(appInfos);
	    },
	    initLink:function(panel) {
	        var me = this;
	        if (Ext.typeOf(me.callback) == 'function') {
	            me.callback(me);
	        }
	        var appInfos = me.getAppInfos();
	        var length = appInfos.length;
	        me.comNum++;
	        if (me.comNum == length) {
	        	 if (me.panelType == 'Ext.tab.Panel') {
	                 var f = function() {
	                     me.setActiveTab(me.defaultactive);
	                     me.un('tabchange', f);
	                 };
	                 me.on('tabchange', f);
	             }
		        var querenForm = me.getComponent('querenguanbiForm');
		        var ygList=me.getComponent('yuangongListCheck');
		        var rbsex={};
		        var HRempDeptId='';
		        ygList.on({
		            afterOpenShowWin:function(formPanel){
		                //还原部门信息
		                var record = formPanel.selectionRecord;//构建列表时传入
		                var records=record;
		                if(record==null){
		                     records=ygList.getSelectionModel().getSelection();
		                     record=records[0];
		                }
		                if(record&&record!=null){ 
		                var value=record.get('HREmployee_HRDept_Id');
		                var text=record.get('HREmployee_HRDept_CName');
		                var hrDeptId=formPanel.getComponent('HREmployee_HRDept_Id');
		                if(hrDeptId.xtype=='combobox'){
		                        var arrTemp=[[value,text]];
		                        hrDeptId.store=new Ext.data.SimpleStore({ 
		                            fields:['value','text'], 
		                            data:arrTemp 
		                            ,autoLoad:true
		                        });
		                        hrDeptId.setValue(value);
		                    }
		              }else{
		                    Ext.Msg.alert('提示','请选择一行记录！');
		                }
		            },
		           select:function(view,record){
	                    if(record.data['HREmployee_BSex_Name']=='男'){   
	                        //封装说明；var values={checkboxname:['4']}; 
	                        //var values={单选组名:值}             
	                        rbsex={HREmployee_BSex_Id: ['5350598518561423778']};
	                    }  
	                     if(record.data['HREmployee_BSex_Name']=='女'){
	                         rbsex={HREmployee_BSex_Id: ['5480995475585737435']};                
	                    }
	                     HRempDeptId=record.get('HREmployee_HRDept_CName');


		            },
		            onClick:function(){
		            	 if(me.appPanel&&me.appPanel!=null){
				            //更新部门直属员工列表数据
				            me.appPanel.load(me.bmzshql);
		                 }
			             me.close();
		            }
		        });
		      
		        var toolbar=ygList.getComponent('toolbar').getComponent('buttonstoolbar');
		        //查看
		        var show=toolbar.getComponent('show');
		        show.on({
		        	click:function(){
		        	var records = ygList.getSelectionModel().getSelection();
		                if (records.length == 1) {
		                    var id = records[0].get("HREmployee_Id");
		                    me.openFormWin(id, records[0],rbsex,HRempDeptId);
		                } else {
		                    Ext.Msg.alert("提示", "请选择一条数据进行操作！");
		                }
		           }
		        });
		        //查询栏
		        var searchText=toolbar.getComponent('searchText');
			    
		        searchText.on({
		           specialkey: function(field,e){    
		                if (e.getKey()==Ext.EventObject.ENTER){  
		                	var newValue=searchText.getValue();
		                	 me.filterFn(newValue);
		                }  
		            }
		        });
		        //查询按钮
		        var searchbtn=toolbar.getComponent('searchbtn');
		        searchbtn.on({
		            click:function(){
		        	var newValue=searchText.getValue();
		                me.filterFn(newValue);
		            }
		        });
		        var btnClose = querenForm.getComponent('btnClose');
	            btnClose.on({
	                click:function(com, e, opets) {
//	                    if(me.appPanel&&me.appPanel!=null){
//	                        //更新部门相关员工列表数据
//	                        me.appPanel.load(me.bmxgyghql);
//	                    }
	                    me.close();
	                }
	            });

		     
	        }
	    },
	   
	    /**
	     * 模糊查询过滤函数
	     * @param {} value
	     */
	     filterFn: function (value) {
	         var me = this, valtemp = value;
//		         var bumenyuangongList=formPanel.getComponent('yuangongListCheck');
	         var ygList=me.getComponent('yuangongListCheck');
	         var  store=ygList.getStore();
	         if (!valtemp) {
	             store.clearFilter();
	             return;
	         }
	         valtemp = String(value).trim().split(" ");
	         store.filterBy(function (record, id) {
	             var data = record.data;
	             for (var p in data) {
	                 var porp = String(data[p]);
	                 for (var i = 0; i < valtemp.length; i++) {
	                     var macther = valtemp[i];
	                     var macther2 = '^' + Ext.escapeRe(macther);
	                     mathcer = new RegExp(macther2);
	                     if (mathcer.test(porp)) {
	                         return true;
	                     } 
	                 } 
	             }
	             return false;
	         });
	     },
	     openFormWin :function(id, record,rbsex,HRempDeptId) {
	        var winId = "5121157369999666069";
	        var callback = function(appInfo) {
	            if (appInfo && appInfo != "") {
	                var ClassCode = appInfo.appInfo.BTDAppComponents_ClassCode;
	                if (ClassCode && ClassCode != "") {
	                    var panelParams = {
	                        dataId:id,
	                        selectionRecord:record,
	                        modal:true,
	                        floating:true,
	                        closable:true,
	                        draggable:true,
	                        HRempDeptId:HRempDeptId,
	                        rbsex:rbsex
	                    };
	                    var Class = eval(ClassCode);
	                    var panel = Ext.create(Class, panelParams).show();
	                    var hREmployeeBSexId= panel.getComponent('HREmployee_BSex_Id');
	                    hREmployeeBSexId.setValue(rbsex);
	                    var HREmployeeHRDeptId= panel.getComponent('HREmployee_HRDept_Id');
	                    HREmployeeHRDeptId.setValue(HRempDeptId);
	                    panel.on({
	                        saveClick:function() {
	                            panel.close();
	                            me.load();
	                            me.fireEvent("saveClick");
	                        } 
	                    });
	                    panel.on({
	                        saveClick:function() {
	                            panel.close();
	                            me.load();
	                            me.fireEvent("saveClick");
	                        }
	                    });
	               
	                } else {
	                    Ext.Msg.alert("提示", "没有类代码！");
	                }
	            }
	        };
	        this.getAppInfoFromServer(winId, callback);
	    },
	    getAppInfoFromServer:function(id, callback) {
	        var me = this;
	        var url = me.getAppInfoServerUrl + '?isPlanish=true&id=' + id;
	        Ext.Ajax.defaultPostHeader = 'application/json';
	        Ext.Ajax.request({
	            async:false,
	            url:url,
	            method:'GET',
	            timeout:2000,
	            success:function(response, opts) {
	                var result = Ext.JSON.decode(response.responseText);
	                if (result.success) {
	                    var appInfo = '';
	                    if (result.ResultDataValue && result.ResultDataValue != '') {
	                        appInfo = Ext.JSON.decode(result.ResultDataValue);
	                    }
	                    if (Ext.typeOf(callback) == 'function') {
	                        var obj = {
	                            success:false,
	                            ErrorInfo:'没有获取到应用组件信息!'
	                        };
	                        if (appInfo != '') {
	                            obj = {
	                                success:true,
	                                appInfo:appInfo
	                            };
	                        }
	                        callback(obj);
	                    }
	                } else {
	                    if (Ext.typeOf(callback) == 'function') {
	                        var obj = {
	                            success:false,
	                            ErrorInfo:'获取应用组件信息失败！错误信息' + result.ErrorInfo + ''
	                        };
	                        callback(obj);
	                    }
	                }
	            },
	            failure:function(response, options) {
	                if (Ext.typeOf(callback) == 'function') {
	                    var obj = {
	                        success:false,
	                        ErrorInfo:'获取应用组件信息请求失败！'
	                    };
	                    callback(obj);
	                }
	            }
	        });
	    }
	});