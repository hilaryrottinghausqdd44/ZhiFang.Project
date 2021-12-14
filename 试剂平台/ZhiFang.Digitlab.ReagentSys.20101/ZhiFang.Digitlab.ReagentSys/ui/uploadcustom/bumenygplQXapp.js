
Ext.Loader.setConfig({enabled:true});
Ext.ns('Ext.manage');
Ext.Loader.setPath('Ext.zhifangux.CheckList', getRootPath() + '/ui/zhifangux/CheckList.js');
Ext.define("bumenygRightsApp", {
    extend:"Ext.panel.Panel",
    panelType:"Ext.panel.Panel",
    alias:"widget.bumenygRightsApp",
    title:"部门员工权限",
    width:1200,
    height:500,
    externalWhere:'',
    valueField:[{
        text:'主键',
        dataIndex:'RBACRole_Id',
        width:181,
        hidden:true,
        align:'left'
    }, {
        text:'代码',
        dataIndex:'RBACRole_UseCode',
        width:87,
        sortable:false,
        hidden:false,
        hideable:true,
        align:'left'
    } ,{
        text:'名称',
        dataIndex:'RBACRole_CName',
        width:126,
        sortable:false,
        hidden:false,
        hideable:true,
        align:'left'
    },  {
        text:'描述',
        dataIndex:'RBACRole_Comment',
        width:181,
        sortable:false,
        hidden:false,
        hideable:true,
        align:'left'
    },{
        text:'显示次序',
        dataIndex:'RBACRole_DispOrder',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true,
        align:'left'
    }],
    internalDockedItem :[{
            xtype:'toolbar',
            dock:'bottom',
            hidden:true,
            itemId:'toolbar-bottom',
            items:[ {
                xtype:'label',
                itemId:'count',
                text:'共0条'
            } ]
        }    
       ,{
        xtype:'toolbar',
        itemId:'buttonstoolbar',
        dock:'top',
        items:[ {
            itemId:'refresh',
            text:'更新',
            iconCls:'build-button-refresh'
        }, '->', {
            xtype:'textfield',
            itemId:'searchText',
            width:130,
            emptyText:'代码/名称/描述'
        }, {
            xtype:'button',
            text:'查询',
            itemId:'searchbtn',
            iconCls:'search-img-16 ',
            tooltip:'代码/名称/描述',
            listeners: {
            click:function(){
            }
        }
            
        } ]
    } ],
    layout:{
        type:"border",
        regionWeights:{
            west:2,
            east:1
        }
    },
    getAppInfoServerUrl:getRootPath() + "/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById",
    appInfos:[ {
        x:296,
        width:486,
        height:273,
        appId:"4737011784260186118",
        header:false,
        itemId:"bumenyuangongTab",
        title:"部门员工选择",
        region:"center",
        layout:'fit',
        split:false,
        collapsible:false,
        collapsed:false,
        border:true,
        sequencenum:0,
        defaultactive:false
    }, {
        width:291,
        height:273,
        appId:"5057693520969860629",

        header:true,
        itemId:"quanxianbumenTree",
        title:"部门",
        region:"west",
        layout:'fit',
        split:true,
        collapsible:true,
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
        //权限角色列表
        var list=me.getComponent('center').getComponent('topCenter').getComponent('bumenyuangongjuese');
        var count=list.counts;
        me.setCount(count);
        //alert('共'+count+'条');        
    },
    createItems:function() {
        var me = this;
        var appInfos = me.getAppInfos();
        for (var i in appInfos) {
            var id = appInfos[i].appId;
            var callback = me.getCallback(appInfos[i]);
            me.getAppInfoFromServer(id, callback);
        }
	  me.add({
	        width:360,
	        height:273,//388,
	        //appId:'4642494654686007788',
	        header:false,
            xtype: 'panel',
	        itemId:'center',
	        title:'',
            layout:'fit',
            split:true,
            collapsible:true, //允许伸缩
            collapsed:false,
            border:true,
            defaultactive:true,
	        region:'east',
	        items:[
				    {
				    title: '权限角色批量选择',
				    itemId:'topCenter',//
				    header:true,
				    autoScroll :true,
				    //height:430,
				    region:'center',
				    xtype: 'panel',
				    border:true,
				    split:true,
				    collapsible:true,
				    collapsed:false,
                    defaultactive:false,
				    layout:'fit',
                    dockedItems:[{
				        xtype: 'toolbar',
                        itemId:'juesepiliangForm',
				        dock: 'bottom',
				        items: [
                                {
                                xtype:'button',itemId:'btnOK',x:25,y:7,height:26,iconCls:'build-button-save',
                                width:135,text:'批量保存勾选权限　',name:'btnOK',
                                listeners: {
                                    click: {
                                        element: 'el',
                                        fn: function(){ 
                                            me.fireEvent('okClick');
                                        }
                                    }   
                                }                       
                               },
                               {
                                xtype:'button',itemId:'btnDel',x:180,y:7,height:26,iconCls:'build-button-delete',
                                width:135,text:'批量删除勾选权限　',name:'btnDel',
                                listeners: {
                                    click: {
                                        element: 'el',
                                        fn: function(){ 
                                            me.fireEvent('okClick');
                                        }
                                    }   
                                }                       
                               }
                        ]}],
				    items: [
					    {
					    width:350,
					    //height:250,
					    xtype:'CheckList',
					    //appId:"111",
					    header:false,
					    itemId:"bumenyuangongjuese",
					    title:"权限角色批量选择",
					    region:"east",
                        layout:'fit',
					    split:true,
					    collapsible:true,
					    collapsed:false,
					    border:true,
					    isbutton:false,
					    valueField:me.valueField,
					    primaryKey:'RBACRole_Id',
					    internalDockedItem:me.internalDockedItem,
					    serverUrl:getRootPath()+ '/RBACService.svc/RBAC_UDTO_SearchRBACRoleByHQL'
					        }]
				    }/*,
                   {
				        height:42,
				        xtype:'form',
				        //appId:'4744920941830014381',
				        header:false,
				        itemId:'juesepiliangForm',
				        title:'',
				        region:'south',
				        layout: 'absolute',
				        split:true,
				        collapsible:true,
				        collapsed:false,
				        border:true,
				        items:[
					            {
					            xtype:'button',itemId:'btnOK',x:25,y:7,height:26,
					            width:115,text:'批量保存勾选权限',name:'btnOK',
					            listeners: {
					                click: {
					                    element: 'el',
					                    fn: function(){ 
					                        me.fireEvent('okClick');
					                    }
					                }   
					            }			            
					           },
	                           {
	                            xtype:'button',itemId:'btnDel',x:180,y:7,height:26,
	                            width:115,text:'批量删除勾选权限',name:'btnDel',
	                            listeners: {
	                                click: {
	                                    element: 'el',
	                                    fn: function(){ 
	                                        me.fireEvent('okClick');
	                                    }
	                                }   
	                            }                       
	                           }
	                    ]			     
			        }*/
	            ]  
	        });    
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
            var _quanxianbumenTree = me.getComponent("quanxianbumenTree");
            var bumenyuangongCheck = me.getComponent("bumenyuangongTab").getComponent("bumenyuangongCheck");
            var bumenxgyuangongcheck = me.getComponent("bumenyuangongTab").getComponent("bumenxgyuangongcheck");
            _quanxianbumenTree.on({
                select:function(view, record) {
                    var Id = record.get("Id");
                    var hql = "id=" + Id + "^";
                    //var bumenyuangongCheck = me.getComponent("bumenyuangongTab").getComponent("bumenyuangongCheck");
                    bumenyuangongCheck.load(hql);
                }
            });
            var _quanxianbumenTree = me.getComponent("quanxianbumenTree");
            _quanxianbumenTree.on({
                select:function(view, record) {
                    var Id = record.get("Id");
                    var hql = "hrdeptemp.HRDept.Id=" + Id;
                    //var bumenxgyuangongcheck = me.getComponent("bumenyuangongTab").getComponent("bumenxgyuangongcheck");
                    bumenxgyuangongcheck.load(hql);
                }
            });
            //获取批量角色选择列表ID
            var list=me.getComponent('center').getComponent('topCenter').getComponent('bumenyuangongjuese');
            //获取批量保存、删除的表单ID
            //var frm=me.getComponent('center').getComponent('juesepiliangForm');//topCenter
            var frm=me.getComponent('center').getComponent('topCenter').getComponent('juesepiliangForm');
            //工具栏
            var buttonstoolbar=list.getComponent('toolbar').getComponent('buttonstoolbar');
            var btnSearch=buttonstoolbar.getComponent('searchbtn');
            var txtsearch=buttonstoolbar.getComponent('searchText');
            var btnrefresh=buttonstoolbar.getComponent('refresh');
            //查询按钮事件
            btnSearch.on({
                click:function()
                {
                   var newValue= txtsearch.getValue();
                   me.filterFn(newValue);
                   var count=list.store.count();
                   me.setCount(count);
                   //var store=list.store;
                  //Ext.apply(store.proxy.extraParams, newValue);
                }
                
            });
            //文本输入框回车事件
            txtsearch.on({
                specialkey: function(field,e){    
	                if (e.getKey()==Ext.EventObject.ENTER){  
	                    var newValue=field.getValue();
	                     me.filterFn(newValue);
                         var count=list.store.count();
                         me.setCount(count);
	                }  
                }
            }); 
            //更新事件
            btnrefresh.on({
                click:function(){
                    //jueseCheckList.load(me.externalWhere);
                    list.load(me.externalWhere);
                    var count=list.store.count();
                    me.setCount(count);
                }
            });
                         
            //监听表单批量保存按钮
            frm.getComponent('btnOK').on({
	            click:function()
	            {
                    //批量保存勾选权限
                    me.jueseSave(0);
                }
            });
            frm.getComponent('btnDel').on({
                click:function()
                {
                    //alert('批量删除勾选权限');
                    //批量删除勾选权限
                       //me.jueseSave(1);
		              Ext.Msg.confirm("警告","确定要删除吗？",function (button){
		                if(button == "yes"){
		                    me.jueseSave(1);
		                }
		            });
                }
            });
             
            if (Ext.typeOf(me.callback) == "function") {
                me.callback(me);
            }
        }
    },
  //=====================内部方法=======================
        //批量增加,减少
    Jurisdiction:function(empIdList,roleIdList,flag){
        var EmpIdList=getRootPath() + '/' +'RBACService.svc/RBAC_RJ_SetEmpRolesByEmpIdList';
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            url:EmpIdList+ '?empIdList=' + empIdList +'&'+'roleIdList=' + roleIdList+'&'+'flag='+ flag,
            method:'GET',
            timeout:5000,
            success:function(response,opts){
               Ext.Msg.alert('提示','批量提交成功!');
            },
            failure : function(response,options){ 
                Ext.Msg.alert('提示','请求服务失败!');
            }
       });
    },
     /**
     * 模糊查询过滤函数
     * @param {} value
     */
     filterFn: function (value) {
         var me = this, valtemp = value;
         //获取右上模块操作选择列表
         //var getjueseChecklist = me;
         var list=me.getComponent('center').getComponent('topCenter').getComponent('bumenyuangongjuese');
        //工具栏
        //var buttonstoolbar=list.getComponent('toolbar').getComponent('buttonstoolbar');
        
         var store = list.getStore();
         if (!valtemp) {
             store.clearFilter();
             return;
         }
         valtemp = String(value).trim().split(' ');
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
     
     //批量保存部门员工与角色方法
     //falg为0增加，1删除
   jueseSave:function(flag)
     {
        var me = this;
        //获取批量角色选择列表ID
        var list=me.getComponent('center').getComponent('topCenter').getComponent('bumenyuangongjuese');
        var bumenyuangongCheck = me.getComponent("bumenyuangongTab").getComponent("bumenyuangongCheck");
        var bumenxgyuangongcheck = me.getComponent("bumenyuangongTab").getComponent("bumenxgyuangongcheck");
        var records=list.getAllChecked();
        //获取下属员工与相关员工所有的应用ID
        var appTab = me.getComponent("bumenyuangongTab")
        var tabName=appTab.activeTab.title;
       
        //部门下属员工
        var bmxsygdata=bumenyuangongCheck.getSelectionModel().getSelection();
        //部门相关员工
        var bmxgygdata=bumenxgyuangongcheck.getSelectionModel().getSelection();
        
        //取得选中下属员工行HREmployee_Id  用于批量添加  减少
         var empValues=[];                   
        Ext.each(bmxsygdata,function(item,index,allItems){
             var  HREmployee_Id=item.data.HREmployee_Id;
             //ChangeRoleId= RBACRole_Id;
             empValues.push(HREmployee_Id);
         });                   
        
        //取得选中相关员工行HREmployee_Id  用于批量添加  减少
         var xsygValues=[];
         Ext.each(bmxgygdata,function(item,index,allItems){
             var  HRDeptEmp_HREmployee_Id=item.data.HRDeptEmp_HREmployee_Id;
             //ChangeRoleId= RBACRole_Id;
             xsygValues.push(HRDeptEmp_HREmployee_Id);
        });                    
        
        //批量选择角色，选择值（增加)
        var RoleId='';
        var RoleValues=[];
        Ext.each(records,function(item,index,allItems){
            var  RBACRole_Id=item.RBACRole_Id;
            RoleId= RBACRole_Id;
            RoleValues.push(RBACRole_Id);
        });
        
         //判断当前激活的标签保存对应的部门员工角色
        if(tabName=='部门下属员工')
        {
            //alert('部门下属员工');
            //批量保存下属员工与角色
            if(empValues==''||RoleValues=='')
            {
                Ext.Msg.alert('提示','请选择部门下属员工与权限角色！');
                return;
            }
            else
            {
                var emp=Ext.encode(empValues);
                emp = emp.replace(/\[/g,'');
                emp = emp.replace(/\]/g,'');
                emp = emp.replace(/'/g,'');
                emp = emp.replace(/"/g,'');
                var RoleId=Ext.encode(RoleValues); 
                RoleId = RoleId.replace(/\[/g,'');
                RoleId = RoleId.replace(/\]/g,'');
                RoleId = RoleId.replace(/'/g,'');
                RoleId = RoleId.replace(/"/g,'');
                //alert('保存成功！');
                me.Jurisdiction(emp,RoleId,flag)
            }
               return;
        }
        if(tabName=='部门相关员工')
        {
            //alert('部门相关员工');
             //批量保存相关员工与角色
            if(xsygValues==''||RoleValues=='')
            {
                Ext.Msg.alert('提示','请选择部门相关员工与权限角色！');
                return;
            }
            else
            {
                var emp1=Ext.encode(xsygValues);
                emp1 = emp1.replace(/\[/g,'');
                emp1 = emp1.replace(/\]/g,'');
                emp1 = emp1.replace(/'/g,'');
                emp1 = emp1.replace(/"/g,'');
                var RoleId=Ext.encode(RoleValues); 
                RoleId = RoleId.replace(/\[/g,'');
                RoleId = RoleId.replace(/\]/g,'');
                RoleId = RoleId.replace(/'/g,'');
                RoleId = RoleId.replace(/"/g,'');
                //alert('保存成功！');
                me.Jurisdiction(emp1,RoleId,flag)
            }
                return;
        }                    
 
     },
     //共几条记录
     setCount:function(count) {
            var me = this;
            //获取批量角色选择列表ID
            var list=me.getComponent('center').getComponent('topCenter').getComponent('bumenyuangongjuese');
            var com = list.getComponent('toolbarbottom').getComponent('toolbar-bottom').getComponent("count");
            //list.getComponent('toolbar').getComponent('buttonstoolbar');
            //var buttonstoolbar=list.getComponent('toolbar').getComponent('buttonstoolbar');
            //var btnSearch=buttonstoolbar.getComponent('searchbtn');
            var str = "共" + count + "条";
            com.setText(str, false);
      },
     //==================================
      /**
     * 初始化表单构建组件
     */
    initComponent:function(){
        var me = this;
        Ext.Loader.setConfig({enabled:true});
        Ext.ns('Ext.manage');
        Ext.Loader.setPath('Ext.zhifangux.CheckList', getRootPath() + '/ui/zhifangux/CheckList.js');
        
        me.callParent(arguments);
       
    },
    getAppInfoFromServer:function(id, callback) {
        var me = this;
        var url = me.getAppInfoServerUrl + "?isPlanish=true&id=" + id;
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
                            ErrorInfo:'获取应用组件信息失败！错误信息【<b style="color:red">' + result.ErrorInfo + "</b>】"
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