//检验项目
Ext.ns('Ext.zhifangux');
Ext.define('Ext.zhifangux.TestItemsClassified', {
    extend:'Ext.tab.Panel',
    alias:'widget.testitemsclassified',
    layout:'fit',
    border:false,
    name:'',
    width:430,
    //标签页的宽度
    height:250,
    //标签页的高度  
    defaultactiveTab:0,
    //默认选中标签页
    defaultLoad:false,
    dataobj:{},
    //加载数据的开关
    defaultTabLoad:false,
    DEactiveTab:'',
    //页签切换时的数据加载开关
    //公开构建应用ID、应用名称
    tabpanelId:'5672430509330954909',
    tabpanelName:'',
    //取后台数据库字段动态标签页
    txtName:'bspecialty.Name',
    //显示名称Name
    txtIndex:'bspecialty.Id',
    comTab:'',
    //隐藏ID
    //======常量===
    tabCloumnData:[],
    arrRowData:[],
    //获取列表选择行数据集
    searchfield:'bspecialtyitem.BSpecialty.Id',
    //过虑标签页对应的项目字段
    tabfields:[ 'BSpecialty_Id', 'BSpecialty_LabID', 'BSpecialty_Name', 'BSpecialty_SName' ],
    tabUrl:getRootPath() + '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBSpecialtyByHQL?page=1&limit=1000&isPlanish=true',
    /**
     * 获取应用信息的后台服务地址
     * @type String
     */
    getAppInfoServerUrl:getRootPath() + '/ServerWCF/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById',
    //标签选项id
    tabPanel:'',
    /**
     * 初始化组件
     */
    initComponent:function() {
        var me = this;
        //创建标签选项页          
        me.tabPosition = 'top';
        //注册事件
        me.addEvents('selectionchange');
        me.addEvents('deselect');
        me.addEvents('onClick');
        me.addEvents('comload');
        //标签页单击事件
        me.listeners = me.listeners || [];   
        me.listeners.tabchange = function(tabPanel, newCard, oldCard) {
            var callback = function(classObj) {
                //获取当前页面ID
                var tabId = newCard.id;               
                var com2 = Ext.create(classObj);
                me.comTab=com2;
                com2.header=false;
                //列表选择事件
                com2.on({
                    select:function(RowModel, record, index,  eOpts ){
                        var itemNo = record.data.BSpecialtyItem_ItemAllItem_Id;
                        var itemName = record.data.BSpecialtyItem_ItemAllItem_CName;
                        var DataTimeStamp = record.data.BSpecialtyItem_ItemAllItem_DataTimeStamp;
                        var addData = {
                            MEPTOrderItem_ItemAllItem_Id:itemNo,
                            MEPTOrderItem_ItemAllItem_CName:itemName,
                            MEPTOrderItem_ItemAllItem_DataTimeStamp:DataTimeStamp
                        };
                        dataobj=addData;
                    },
                    deselect:function(RowModel, record, index, eOpts) {                        
                        me.fireEvent('deselect');
                    }
                });
                //伪字符串
                var where = '';
                if(tabId==0){
                	where +='';
                }else{
                	where += me.searchfield + '=' + tabId;
                }
                var itemlength = tabPanel.activeTab.items.length;
                //页签items为空时加载数据
                if( me.comTab.getStore().getCount()==0){
                    if (itemlength < 1) {
		                   tabPanel.activeTab.items.add(com2);
		                   tabPanel.activeTab.doLayout();
		                   var com=tabPanel.activeTab.items.items[0];
		                   var length=com.store.data.length;
		                   var stord1=com.getStore();
		                   com.load(where);   //where
		                   stord1.on({
		                	   load:function(){
		                	       me.fireEvent('comload');
		                	   }
		                   });
		                   return;
		                }
		            }
//		            else  //实时刷新页面数据
//		            {                        
////		                   tabPanel.activeTab.items.clear();
//		                   tabPanel.activeTab.items.add(com2);
//		                   tabPanel.activeTab.doLayout();
////		                   		                   
//		                   var com=tabPanel.activeTab.items.items[0];
////		                   var length=com.store.data.length;
////		                   var stord1=com.getStore();
////		                   com.load(where);     //通过标签ID过虑项目分类显示
////		                
//		            };
		            me.fireEvent('onClick');		           
		            };
		           var classId=me.ChangeCode(me.tabpanelId,callback);  
		           
		           
			    };
            me.listeners.afterrender=function(){
            	var callbackNew=function(){
            		me.setDefaultCheck(me.defaultactiveTab);
            		var tab=me.activeTab;
            		me.comTab=tab.items.items[0];
            		me.DEactiveTab=me.comTab;
                    if(Ext.typeOf(me.callback)=='function'){me.callback(me);} 
                };
			    me.initTab(callbackNew);
                                
            };
        this.callParent(arguments);
    },
    /**
     * 获取后台数据生成动态标签页
     * @param {} List
     * @param {} com
     * @param {} appclass
     * @return {}
     */
    gettabpanelStore:function(callback) {
        var me = this;
        var arritems = [];
        Ext.Ajax.request({
            async:false,
            //非异步
            url:me.tabUrl,
            method:'GET',
            timeout:5e3,
            success:function(response, opts) {
                var data = Ext.JSON.decode(response.responseText);
                if (data.ResultDataValue && data.ResultDataValue != '') {
                    var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
                    data.ResultDataValue = ResultDataValue;
                } else {
                    data.ResultDataValue = {
                        count:0,
                        list:[]
                    };
                }
                response.responseText = Ext.JSON.encode(data);
                me.tabCloumnData = data.ResultDataValue.list;
                for (var i = 0; i < ResultDataValue.count; i++) {
                	var BSpecialty_Name= ResultDataValue.list[i].BSpecialty_Name;
	                	
		            var BSpecialty_Id=ResultDataValue.list[i].BSpecialty_Id;
	              
		            if (ResultDataValue.list[i].BSpecialty_Name!=''){
		            	var addData = {
	                		'BSpecialty_Id':BSpecialty_Id,
	                        'BSpecialty_Name':BSpecialty_Name
	                    };
		            	arritems.push(addData);
                	}
		            
                }
                callback(arritems);
            },
            failure:function(response, opts) {
                Ext.Msg.alert('提示', '专业请求失败！' + response.responseText);
            }
        });
        //return arritems;
    },

    //===========获取类代码=================
    //转换获取到的类代码
    ChangeCode:function(appID, callback2) {
        var me = this;
        var codeclass = null;
        var callback = function(info) {
            if(info.success){
                var appInfo = info.appInfo;
                var ClassCode = appInfo.BTDAppComponents_ClassCode;
                var Class = eval(ClassCode);
                var panel = Ext.create(Class);
                
            }
            if (appInfo && appInfo != '') {
                var ClassCode = appInfo.BTDAppComponents_ClassCode;
                var Class = eval(ClassCode);
                codeclass = Class;
                callback2(codeclass);
            }
        };
        me.getAppInfoFromServer(appID, callback);
        return codeclass;
    },
    //根据构建应用ID编号获取类代码
    getAppInfoFromServer:function(id, callback) {
        var me = this;
        if (id && id != -1) {
            var url = me.getAppInfoServerUrl + '?isPlanish=true&id=' + id;
            Ext.Ajax.defaultPostHeader = 'application/json';
            Ext.Ajax.request({
                async:false,
                //非异步
                url:url,
                method:'GET',
                timeout:2e3,
                success:function(response, opts) {
                    var result = Ext.JSON.decode(response.responseText);
                    var info = {success:false,ErrorInfo:'',appInfo:''};
                    if (result.success) {
                        if (result.ResultDataValue && result.ResultDataValue != '') {
                            var appInfo = Ext.JSON.decode(result.ResultDataValue);
                            info.success = true;
                            info.appInfo = appInfo;
                            
                        } else {
                            info.ErrorInfo = '没有获取到应用信息！';
                        }
                    } else {
                        info.ErrorInfo = result.ErrorInfo;
                    }
                    if (Ext.typeOf(callback) == 'function') {
                        callback(info);
                    }
                },
                failure:function(response, options) {
                    var info = {success:false,ErrorInfo:'获取应用信息请求失败！',appInfo:''};
                    if (Ext.typeOf(callback) == 'function') {
                        callback(info);
                    }
                }
            });
        }
    },
    //========公开方法=====
    /***
     * 初始化tabpanel
     * @param {} value
     * @return {}
     */
    initTab:function(callbackTab) {
        var me = this;
        var Id = '', Name = '';
        //标签页隐藏、显示名称
        Id='BSpecialty_Id';
        Name='BSpecialty_Name';
        var arrtab = new Array();
        var callback = function(tabDateList) {
            if (tabDateList.length > 0) {
            	var allarr=[];
                var  arr={'BSpecialty_Name':'全部',
        			'BSpecialty_Id':'0'
            	};
                allarr.push(arr);
                var tabDateListarr=[];
                tabDateListarr = allarr.concat(tabDateList);

                //获取到tab标签页信息列表
                for (var i in tabDateListarr) {
                    var tabIndex = tabDateListarr[i][Id];
                    arrtab[i] = {
                        title:tabDateListarr[i][Name],
                        //标签页显示名称  List[i].Name
                        id:tabIndex,
                        //标签页隐藏值  i
                        layout:'fit',
                        border:false,
//                        title:'',
                        enableTabScroll:true,
                        forceFit:true,
                        defaults:{
                            autoScroll:true,
                            bodyPadding:0
                        },
                        items:[],
                        //标签页显示信息  {xtype:appclass}
                        listeners:{
                            activate:function(tab) {
                            }
                            
                        }
                    };
                    me.add(arrtab[i]);
                }
            } 
            callbackTab();
        };
        me.gettabpanelStore(callback);
    },
    /**绑定后台数据取到的隐藏值和显示值
     * 获取设置标签页隐藏值ID、显示名称属性
     * @param {} tabActivate
     */
    getDisplay:function(value) {
        var displayField = '';
        var arr = value.split('.');
        displayfield = arr[arr.length - 1];
        return displayfield;
    },
    /**
     *公开一个默认选中某一页签的方法
     * @param {} tabActivate
     */
    setDefaultCheck:function(tabActivate) {
        var me = this;
        //me.activeTab =tabActivate;
        //var tab = Ext.getCmp(tabActivate);
        me.setActiveTab(tabActivate);
    },
    /**
     * 获取选择中页签的id值
     */
    getValue:function() {
        var me = this;
        var Idvalue = '';
        //获取当前激活标签页ID
        idvalue = me.activeTab.id;
        return idvalue;
    },
    /**
     * 获取选择中页签的名称
     */
    getTitleName:function() {
        var me = this;
        var NameValue = '';
        //获取当前激活标签页ID
        NameValue = me.activeTab.title;
        return NameValue;
    }
});
