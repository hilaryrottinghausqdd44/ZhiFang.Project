/**
 * 普通选择器构建工具
 * 【可配参数】
 * 
 * 服务地址配置
 * objectUrl：获取数据对象列表的服务地址
 * objectPropertyUrl：获取数据对象内容的服务地址
 * objectGetDataServerUrl：获取获取服务列表的服务地址
 * objectSaveDataServerUrl：获取保存服务列表的服务地址
 * dictionaryListServerUrl：查询对象属性所属字典服务列表的服务地址
 * addServerUrl：保存新增信息的后台服务地址
 * editServerUrl：保存修改信息的后台服务地址
 * getAppParamsServerUrl：获取参数的后台服务地址
 * 
 * buildTitle：构建名称
 * appId：列表元应用的ID，默认是-1，即新增，大于0的为修改
 * 
 * 
 */
 Ext.ns('Ext.zhifangux');
 Ext.define('Ext.zhifangux.tabsPanel',{
    extend:'Ext.tab.Panel',
    alias: 'widget.tabspanel',
    //layout:'absolute',
    border:false,    
    name:'',    
    width:430,          //标签页的宽度
    height:250,         //标签页的高度  
    
    defaultactiveTab:1,   //默认选中标签页
    
    defaultLoad:false,     //加载数据的开关
    defaultTabLoad:false,   //页签切换时的数据加载开关
    
    //公开构建应用ID、应用名称
    tabpanelId:'4624755649523553172',   //  5530591812943632585
    tabpanelName:'',
    
    //取后台数据库字段动态标签页
    txtName:'bspecialty.Name',    //显示名称Name
    txtIndex:'bspecialty.Id',     //隐藏ID
    
    //======常量===
    tabCloumnData:[],
    arrRowData:[],   //获取列表选择行数据集
    searchfield:"bspecialtyitem.BSpecialty.Id",     //过虑标签页对应的项目字段
    
    tabfields:['BSpecialty_Id','BSpecialty_LabID','BSpecialty_Name','BSpecialty_SName'],
    tabUrl:getRootPath()+'/SingleTableService.svc/ST_UDTO_SearchBSpecialtyByHQL',
    /**
     * 获取应用信息的后台服务地址
     * @type String
     */
    getAppInfoServerUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById',
    //标签选项id
    tabPanel:'',
     /**
     * 初始化组件
     */
    initComponent:function(){
        var me = this;
        //创建标签选项页          
        me.tabPosition='top';
        //me.setDefaultCheck(me.defaultactiveTab);
        
       //注册事件
        me.addEvents('selectionchange');
        me.addEvents('onClick');//标签页单击事件
       
        me.listeners = {
            tabchange:function(tabPanel, newCard, oldCard){    
            	var callback = function(classObj){
            		//获取当前页面ID
                    var tabId=newCard.id;
                    var com2 =Ext.create(classObj); 
                    //伪字符串
                    var where="";
                    where+=me.searchfield+"="+tabId;   //"bspecialtyitem.BSpecialty.Id="+tabId;
                    var itemlength=tabPanel.activeTab.items.length;
                    //页签items为空时加载数据
                    if(!me.defaultTabLoad)
                    { 
	                    if(itemlength<1)
	                    {
	                       tabPanel.activeTab.items.add(com2);
	                       tabPanel.activeTab.doLayout();
	                       var com=tabPanel.activeTab.items.items[0];
	                       var length=com.store.data.length;
	                       var stord1=com.getStore();
	                       com.load(where);   //where
	                    };
                    }
                    else  //实时刷新页面数据
                    {
                           tabPanel.activeTab.items.clear();
                           tabPanel.activeTab.items.add(com2);
                           tabPanel.activeTab.doLayout();
                           tabPanel.activeTab.items.addListener(com2.getSelectionModel().getSelection());
                           
                           var com=tabPanel.activeTab.items.items[0];
                           var length=com.store.data.length;
                           var stord1=com.getStore();
                           com.load(where);     //通过标签ID过虑项目分类显示
                        
                    };
                    var itemlength1=tabPanel.activeTab.items.length;
                    //列表选择事件
				    com2.on({
                        itemclick:function(com,record,item,num){
                            //alert("列表显示值名称:"+num);
                        },
                        select:function(rowmodel,record,index,eOpts ){
                            //var numrow=record.stores[0].data.length;
                            //alert("多选择行事件列表显示值名称:"+numrow);
                        },
                        //列表选中行记录数据
                        selectionchange:function(model,arrselected,eOpts)
                        {
                            if(arrselected.length>0)
                               me.arrRowData=arrselected;
                               //alert("当前选择行记录总数："+arrselected.length);
                               //alert("当前选择行记录"+Ext.encode(me.arrRowData));
                            me.fireEvent('selectionchange');
                        }
                        
                    });
                    //me.fireEvent('selectionchange');
                    me.fireEvent('onClick');
                    //var tabid=me.getValue();
                    //alert("当前标签页的ID："+tabid);
            	};
                me.ChangeCode(me.tabpanelId,callback);  //5530591812943632585  //5060718092433697681
                    
                        
			},
            afterrender:function()
            {
                var me = this;
			    me.initTab();  
                me.setDefaultCheck(me.defaultactiveTab);
            },
            activate:function(tab){
                 
                 alert("刷新页面！");
            } ,
            beforeactivate:function( tabpanel,  eOpts )
            {
                 alert("页面激活前！");
                var classId=me.ChangeCode('5530591812943632585');  //5530591812943632585  //5060718092433697681
                var com=Ext.create(classId.xtype);
                var length=com.store.data.length;
                
            }
        };
        this.callParent(arguments);
    },    
	
    //=============创建类方法======
    //通过url取出item类别，动态的生成tabpanel项
    tabpanelStore:function(callbacktab){
        var me=this;
        var arritems=[];
        Ext.Ajax.request({
            async:false,//非异步
            url:me.tabUrl,
            method:'GET',
            timeout:5000,
            success: function(response, opts) {
                var data = Ext.JSON.decode(response.responseText);
                if(data.ResultDataValue&&data.ResultDataValue!="")
                {
                    var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
                    data.ResultDataValue = ResultDataValue;
                }
                else{
                    data.ResultDataValue = {'count':0,'list':[]};
                }
                response.responseText = Ext.JSON.encode(data);
                me.tabCloumnData=data.ResultDataValue.list;
                arritems=data.ResultDataValue.list;
                /*if(me.tabCloumnData.length>0){
                    me.addtabgrid(me.tabCloumnData,me);  //me.tabPanel
                }*/
            },
            failure: function(response, opts) {
                Ext.Msg.alert('提示','专业请求失败！'+response.responseText);
            }
        });
        
        return arritems;
    },
    
    /**
     * 获取后台数据生成动态标签页
     * @param {} List
     * @param {} com
     * @param {} appclass
     * @return {}
     */
    gettabpanelStore:function(callback){
        var me=this;
        var arritems=[];
        Ext.Ajax.request({
            async:false,//非异步
            url:me.tabUrl,
            method:'GET',
            timeout:5000,
            success: function(response, opts) {
                var data = Ext.JSON.decode(response.responseText);
                if(data.ResultDataValue&&data.ResultDataValue!="")
                {
                    var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
                    data.ResultDataValue = ResultDataValue;
                }
                else{
                    data.ResultDataValue = {'count':0,'list':[]};
                }
                response.responseText = Ext.JSON.encode(data);
                me.tabCloumnData=data.ResultDataValue.list;
                arritems=data.ResultDataValue.list;
                callback(arritems);
                /*if(me.tabCloumnData.length>0){
                    me.addtabgrid(me.tabCloumnData,me);  //me.tabPanel
                }*/
            },
            failure: function(response, opts) {
                Ext.Msg.alert('提示','专业请求失败！'+response.responseText);
            }
        });
        
        return arritems;
    },
    /**
     * 具体动态创建标签选项页方法
     * 
     */
   addtabgrid:function(List,com,appclass){
        var me=this;
        var items=[];
        var arrtab=new Array();
        var Id='',Name='';   //标签页隐藏、显示名称
        Id=me.getDisplay(me.txtIndex);
        Name=me.getDisplay(me.txtName);
        if(List.length>0)
        {        
	        for(var i in List){
	            var b=List[i][Id];                     //BSpecialty_Id  标签页ID号
	            arrtab[i]={ title:List[i][Name],       //标签页显示名称  List[i].Name
                            id:i,                      //标签页隐藏值
                            layout:'fit',
                            enableTabScroll: true,
				            forceFit:true,
				            defaults :{
						                autoScroll: true,
						                bodyPadding:0
				                       }//,
                      /*      items :[],   //标签页显示信息  {xtype:appclass}
                            listeners:{
                              activate:function(tab){
								      //this.getUpdater().refresh();
                                      
                                      alert("更新页面处理！");
								     }
                            }*/
                          };
                 arrtab[i].handler=function(but,e){
                    var records = but.ownerCt.ownerCt.getSelectionModel().getSelection();
                    alert("获取列表数据");
                 };
	            //com.items.push(arrtab[i]);
	            items.push(arrtab[i]);
	        };            
        }
        else{
            //初始化取数据为空时标签页默认页值
            arrtab[0]={title:"标签页1",id:0,items :[{xtype:'grid',  //"'"+b+"'"
                    title:'列表1',
                    columns:[
                        {text:'项目列一'},
                        {text:'项目列二'},
                        {text:'项目列三'}
                    ],
                    itemId:'center',
                    width:600,
                    height:250}],listeners:{}};
                  arrtab[1]={title:"标签页2",id:1,items :[{xtype:'grid',  //"'"+b+"'"
                    title:'列表2',
                    columns:[
                        {text:'项目列一'},
                        {text:'项目列二'},
                        {text:'项目列三'}
                    ],
                    itemId:'center',
                    width:600,
                    height:250}],listeners:{}};    
            items.push(arrtab[0]);items.push(arrtab[1]);
        }
        //items=com.items;
        return items;
    },
    
    //===========获取类代码=================
    
    //转换获取到的类代码
    ChangeCode:function(appID,callback2)
    {
        var me=this;
        var codeclass=null;
        var callback = function(appInfo){
            if(appInfo && appInfo != ''){
                var ClassCode = appInfo.BTDAppComponents_ClassCode;
                var Class = eval(ClassCode);
                codeclass=Class;
                callback2(codeclass);
            }
        };
        
        me.getAppInfoFromServer(appID,callback);
        //return codeclass;
        
    },
    
    
    //根据构建应用ID编号获取类代码
    getAppInfoFromServer:function(id,callback){
        var me = this;
        
        if(id && id != -1){
            var url = me.getAppInfoServerUrl + "?isPlanish=true&id=" + id;
            Ext.Ajax.defaultPostHeader = 'application/json';
            Ext.Ajax.request({
                async:false,//非异步
                url:url,
                method:'GET',
                timeout:2000,
                success:function(response,opts){
                    var result = Ext.JSON.decode(response.responseText);
                    if(result.success){
                        if(result.ResultDataValue && result.ResultDataValue != ""){
                            var appInfo = Ext.JSON.decode(result.ResultDataValue);
                            if(Ext.typeOf(callback) == "function"){
                                callback(appInfo);//回调函数
                            }
                        }else{
                            Ext.Msg.alert('提示','没有获取到应用信息！');
                        }
                    }else{
                        Ext.Msg.alert('提示','获取应用信息失败！错误信息【<b style="color:red">'+ result.ErrorInfo +"</b>】");
                    }
                },
                failure : function(response,options){ 
                    Ext.Msg.alert('提示','获取应用信息请求失败！');
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
    initTab:function()
    {
        var me=this;
        var Id='',Name='';   //标签页隐藏、显示名称
        Id=me.getDisplay(me.txtIndex);
        Name=me.getDisplay(me.txtName);
        var arrtab=new Array();
        var callback=function(tabDateList)
	       {
	            if(tabDateList.length>0)
	            {
		            //获取到tab标签页信息列表
		            for(var i in tabDateList)
		            {
		                var tabIndex=tabDateList[i][Id]; 
		                arrtab[i]={
		                    title:tabDateList[i][Name],       //标签页显示名称  List[i].Name
		                    id:tabIndex,                      //标签页隐藏值  i
		                    layout:'fit',
		                    enableTabScroll: true,
		                    forceFit:true,
		                    defaults :{
		                        autoScroll: true,
		                        bodyPadding:0
		                       },
		                    items :[],   //标签页显示信息  {xtype:appclass}
		                    listeners:{
		                      activate:function(tab){  }
		                    } 
		                };
		                me.add(arrtab[i]);
		            }
		            //me.setDefaultCheck(me.defaultactiveTab);
	        }else
            {
                //初始化取数据为空时标签页默认页值
            arrtab[0]={title:"标签页1",
                       id:0,
                       layout:'fit',
                        enableTabScroll: true,
                        forceFit:true,
                        defaults :{
                            autoScroll: true,
                            bodyPadding:0
                           },
                       items :[],
                       listeners:{}};
                  arrtab[1]={title:"标签页2",
                             id:1,
                             layout:'fit',
	                         enableTabScroll: true,
	                         forceFit:true,
	                         defaults :{
	                            autoScroll: true,
	                            bodyPadding:0
	                            },
                             items :[],
                             listeners:{}};    
            me.add(arrtab[0]);me.add(arrtab[1]);
                
            };
	      };
        me.gettabpanelStore(callback);
    },
    /**绑定后台数据取到的隐藏值和显示值
     * 获取设置标签页隐藏值ID、显示名称属性
     * @param {} tabActivate
     */
    getDisplay:function(value)
    {
        var displayField='';
        var arr=value.split('.');
        displayfield=arr[arr.length-1];
        return displayfield;        
    },
    
    /**
     *公开一个默认选中某一页签的方法
     * @param {} tabActivate
     */
    setDefaultCheck:function(tabActivate)
    {
        var me=this;
        me.activeTab=tabActivate;
    },
    
    /**
     * 获取选择中页签的id值
     */
    getValue:function()
    {
        var me=this;
        var Idvalue='';
        //获取当前激活标签页ID
        idvalue=me.activeTab.id;
        return idvalue;
        
    },
        /**
     * 获取选择中页签的名称
     */
    getTitleName:function()
    {
        var me=this;
        var NameValue='';
        //获取当前激活标签页ID
        NameValue=me.activeTab.title;
        return NameValue;
        
    }
 })