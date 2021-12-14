/***
 * 部门管理---部门员工关系
 */
Ext.ns('Ext.manage');
Ext.define('Ext.manage.departmentstaff.bumenyuangonggxApp', {
    extend: 'Ext.panel.Panel',
    panelType: 'Ext.panel.Panel',
    alias: 'widget.bumenyuangonggxApp',
    title: '',
    layout: 'border',
    comNum: 0,
    isTrue:false,
    afterRender: function() {
        var me = this;
        me.callParent(arguments);
        me.initLink();
        if (Ext.typeOf(me.callback) == 'function') {
            me.callback(me);
        }
    },
    /**
   * 初始化
   */
    initComponent: function() {
        var me = this;
        me.bodyPadding = 2;
        me.items = me.createItems();
        me.callParent(arguments);
    },
    //获取当前节点下的所有的子节点(不包含当前节点)
    nodevalue: '',
    findchildnode: function(node) {
        var me = this;
        var childnodes = node.childNodes;
        var nd;
        for (var i = 0; i < childnodes.length; i++) {
            //从节点中取出子节点依次遍历
            nd = childnodes[i];
            me.nodevalue += nd.data.Id + ',';
            if (nd.hasChildNodes()) {}
            //判断子节点下是否存在子节点
            me.findchildnode(nd);
        }
    },
    initLink: function() {
        var me = this;
        //部门树
        var bmTree = me.getComponent('bumenTreeQuery');
        //页签
        var BasicTabPanel = me.getComponent('BasicTabPanel');
        //部门下属员工
        var bumenxiashuList = BasicTabPanel.getComponent('bumenxiashuList');
        //部门相关员工
        var bumenxiangguanList = BasicTabPanel.getComponent('bumenxiangguanList');
        //部门下属员工查询框
        var searchText = bumenxiashuList.getComponent('buttonstoolbar').getComponent('searchText');
       //部门下属员工查询框
        var searchTextxx = bumenxiangguanList.getComponent('buttonstoolbar').getComponent('searchText');
        //当前激活的页
        var comtab = '';
        //当前选中树节点的Id
        var treeId = '';
        var nodevalueP = '';
        var DataTimeStamp = '';
        //切换加载
        BasicTabPanel.on({
            tabchange: function(tab) {
                comtab = BasicTabPanel.getActiveTab(tab.items.items[0]);
                //部门树
                bmTree.on({
                    select: function(view, record) {
                        //部门的下属部门tab:部门列表只呈现一下级
                        var nodes = bmTree.getSelectionModel().getSelection();
                        me.nodevalue = '';
                        me.findchildnode(nodes[0]);
                        //补上当前选择中的节点
                        me.nodevalue = me.nodevalue + nodes[0].data.Id;
                        nodevalueP = me.nodevalue;
                        var Id = '';
                        //                        treeId = '';
                        if (record != null) {
                            Id = record.get('Id');
                            treeId = Id;
                            //                            alert(Id);
                        }

                        if (Id == '' || Id == null || Id == undefined) {
                            Ext.Msg.alert('提示', '请在部门树选择部门后再操作');
                            return;
                        }
                    }
                });
                if (treeId != '') {
                    var bmzshql = 'hremployee.HRDept.Id=' + treeId;
                    var bmxgyghql = 'hrdeptemp.HRDept.Id in(' + me.nodevalue + ')';
                    if (comtab === bumenxiashuList) {
                        var Itemindex = bumenxiashuList.getStore().findExact('HREmployee_HRDept_Id', treeId);
                        if (Itemindex === -1) {
                            bumenxiashuList.load(bmzshql);
                        }
                    } else if (comtab == bumenxiangguanList) {
                        var HRDept = bumenxiangguanList.getStore().findExact('HREmployee_HRDept_Id', treeId);
                        if (HRDept === -1) {
                            bumenxiangguanList.load(bmxgyghql);
                        }
                    }
                }
            }
        });
        //部门树
        bmTree.on({
            select: function(view, record) {
                //部门的下属部门tab:部门列表只呈现一下级
                var nodes = bmTree.getSelectionModel().getSelection();
                me.nodevalue = '';
                me.findchildnode(nodes[0]);
                //补上当前选择中的节点
                me.nodevalue = me.nodevalue + nodes[0].data.Id;
                DataTimeStamp = me.nodevalue + nodes[0].data.DataTimeStamp;
                nodevalueP = me.nodevalue;
                var Id = '';
                if (record != null) {
                    Id = record.get('Id');
                    treeId = Id;

                }
                if (Id == '' || Id == null || Id == undefined) {
                    Ext.Msg.alert('提示', '请在部门树选择部门后再操作');
                    return;
                }
                if (Id != '' || Id != null || Id != undefined) {
                    var bmzshql = 'hremployee.HRDept.Id=' + Id;
                    var bmxgyghql = 'hrdeptemp.HRDept.Id in(' + me.nodevalue + ')';
                    comtab = BasicTabPanel.getActiveTab(BasicTabPanel.items.items[0]);
                    if (comtab === bumenxiashuList) {
                        var Itemindex = bumenxiashuList.getStore().findExact('HREmployee_HRDept_Id', Id);
                        if (Itemindex === -1) {
                            bumenxiashuList.load(bmzshql);
                        }
                    } else if (comtab == bumenxiangguanList) {
                        var HRDept = bumenxiangguanList.getStore().findExact('HREmployee_HRDept_Id', Id);
                        if (HRDept === -1) {
                            bumenxiangguanList.load(bmxgyghql);
                        }
                    }
                }
            }
        });
        //部门下属员工
        bumenxiashuList.on({
            addClick: function() {
                me.showFormWin(treeId, nodevalueP, DataTimeStamp);
            },
            searchClick: function() {
                var newValue = searchText.getValue();
                me.filterFnxs(newValue);
            }
        });
        searchText.on({
            specialkey: function(field, e) {
                if (e.getKey() == Ext.EventObject.ENTER) {
                    var newValue = searchText.getValue();
                    me.filterFnxs(newValue);
                }
            }
        });
        //部门相关员工
        bumenxiangguanList.on({
            addClick: function() {
                me.showFormWin(treeId, nodevalueP, DataTimeStamp);
            },
            searchClick: function() {
                //过滤
            	 var newValue = searchTextxx.getValue();
                 me.filterFnxgyg(newValue);
            }
        });
        searchTextxx.on({
            specialkey: function(field, e) {
                if (e.getKey() == Ext.EventObject.ENTER) {
                    var newValue = searchTextxx.getValue();
                    me.filterFnxgyg(newValue);
                }
            }
        });

        /**
    	 * 打开应用效果窗口
    	 * @private
    	 * @param {} title
    	 * @param {} ClassCode
    	 * @param {} id
    	*/
        me.showFormWin = function(treeId, nodevalueP, DataTimeStamp) {
            var me = this;
            Ext.Loader.setConfig({
                enabled: true
            });
            Ext.Loader.setPath("Ext.manage.departmentstaff.xuanzexuangongApp", getRootPath() + "/ui/manage/class/departmentstaff/xuanzexuangongApp.js");
            var maxHeight = document.body.clientHeight * .98;
            var maxWidth = document.body.clientWidth * .98;
            var win = Ext.create("Ext.manage.departmentstaff.xuanzexuangongApp", {
                maxWidth: maxWidth,
                height: 400,
                width: 680,
                autoScroll: true,
                header: true,
                modal: true,
                title: '选择员工',
                //模态
                floating: true,
                //漂浮
                closable: true,
                //有关闭按钮
                resizable: true,
                //可变大小
                draggable: true
            });
            if (win.height > maxHeight) {
                win.height = maxHeight;
            }
            //解决chrome浏览器的滚动条问题
            var callback = function() {
                win.hide();
                win.show();
            };
            var comtab = BasicTabPanel.getActiveTab(BasicTabPanel.items.items[0]);
            win.show(null, callback);
            var ItemValue=[];
            bumenxiangguanList.getStore().data.each(function(item) {
                var emp = item.data.HRDeptEmp_HREmployee_Id;
                ItemValue.push(emp);
            });
            var  sqlvalue=Ext.encode(ItemValue);
    	    sqlvalue = sqlvalue.replace(/\[/g,'');
    	    sqlvalue = sqlvalue.replace(/\]/g,'');
    	    
    	    //列表
    	    var xuanzeyuangongList = win.getComponent('xuanzeyuangongList');
    	    //工具条
    	    var buttonstoolbar = xuanzeyuangongList.getComponent('toolbar').getComponent('buttonstoolbar');
            //更新
    	    var refresh = buttonstoolbar.getComponent('refresh');
    	    //查看
    	    var show = buttonstoolbar.getComponent('show');
    	    //文本过滤
    	    var searchText = buttonstoolbar.getComponent('searchText');
    	    //性别
    	    var rbsex='';
    	    //部门
    	    var HRDeptCName='';
            //查询
    	    var searchbtn = buttonstoolbar.getComponent('searchbtn');
            if (comtab === bumenxiashuList) {
            	var  hql='hremployee.HRDept.Id!='+treeId;
            	xuanzeyuangongList.load(hql);
            	//更新
            	refresh.on({
            		click:function(){
            	    	xuanzeyuangongList.load();
                 	}
            	});
            }else if (comtab === bumenxiangguanList){
        	   if (sqlvalue!=''){
        		   xuanzeyuangongList.load('hremployee.Id'+' '+'not in ('+sqlvalue+')');
        	   }
        		//更新
	           	refresh.on({
	           		click:function(){
		           	   if (sqlvalue!=''){
	        		       xuanzeyuangongList.load('hremployee.Id'+' '+'not in ('+sqlvalue+')');
	        	       }                	}
	           	});
            }
//            文本过滤
            searchText.on({
        	    specialkey: function(field,e){    
                    if (e.getKey()==Ext.EventObject.ENTER){  
                    	var newValue=searchText.getValue();
                    	 me.filterFn(newValue);
                    }  
                }
            });
            //查询
            searchbtn.on({
        	    click: function(){    
                	var newValue=searchText.getValue();
                    me.filterFn(newValue);
	            }
	        });
            xuanzeyuangongList.on({
            	select:function(view,record){
	                 HRDeptCName = record.get('HREmployee_HRDept_CName');
	                 rbsex = record.get('HREmployee_BSex_Name');
	            }
            });
            
            //查看
            show.on({
            	click:function(){
	            	var records = xuanzeyuangongList.getSelectionModel().getSelection();
	                if (records.length == 1) {
	                    var id = records[0].get("HREmployee_Id");
	                    me.showForm(id, records[0],HRDeptCName,rbsex);
	                } else {
	                    Ext.Msg.alert("提示", "请选择一条数据进行操作！");
	                }
                }
            });
            /**
        	 * 打开查看效果窗口
        	 * @private
        	 * @param {} title
        	 * @param {} ClassCode
        	 * @param {} id
        	*/
           me.showForm=function(id, record,HRDeptCName,rbsex) {
                var me = this;
                Ext.Loader.setConfig({
                    enabled:true
                });
                Ext.Loader.setPath("Ext.manage.departmentstaff.showyuangongForm", getRootPath() + "/ui/manage/class/departmentstaff/showyuangongForm.js");
                var maxHeight = document.body.clientHeight * .85;
                var maxWidth = document.body.clientWidth * .98;
                var win = Ext.create("Ext.manage.departmentstaff.showyuangongForm", {
                    maxWidth:maxWidth,
                    autoScroll:true,
                    width:450,
                    height:385,
                    header:true,
                    dataId:id,
                    selectionRecord:record,
                    height:450,
                    title:'查看员工',
                    modal:true,
                    //模态
                    floating:true,
                    //漂浮
                    closable:true,
                    //有关闭按钮
                    resizable:true,
                    //可变大小
                    draggable:true
                });
                if (win.height > maxHeight) {
                    win.height = maxHeight;
                }
                //解决chrome浏览器的滚动条问题
                var callback = function() {
                    win.hide();
                    win.show();
                };
                win.show(null, callback);
                var HREmployeeHRDeptId= win.getComponent('HREmployee_HRDept_Id');
                HREmployeeHRDeptId.on({
                	change:function(){
 	                    HREmployeeHRDeptId.setValue(HRDeptCName);
                    } 
                });
                //性别
                var BSex= win.getComponent('HREmployee_BSex_Id');
                BSex.on({
                	change:function(){
 	                    BSex.setValue(rbsex);
                    } 
                });
            };
                
            /**
             * 模糊查询过滤函数(左）
             * @param {} value
             */
             me.filterFn= function (value) {
                 var me = this, valtemp = value;
                 var store = xuanzeyuangongList.getStore(); //reload()
                 if (!valtemp) {
                     store.clearFilter();
                     return;
                 }
                 valtemp = String(value).trim().split(" ");
                 store.filterBy(function (record, id) {
                     var data = record.data;
                     var CName=record.data.HREmployee_CName;
                     var HRDeptCName=record.data.HREmployee_HRDept_CName;
                     var HRPositionCName=record.data.HREmployee_HRPosition_CName;
                     var dataarr={
                		 HREmployee_CName:CName,
                    	 HREmployee_HRDept_CName:HRDeptCName,
                    	 HREmployee_HRPosition_CName:HRPositionCName
                     };
                     for (var p in dataarr) {
                         var porp = String(dataarr[p]);
                         for (var i = 0; i < valtemp.length; i++) {
                             var macther = valtemp[i];
                             var macther2 = Ext.escapeRe(macther);
                             mathcer = new RegExp(macther2);
                             if (mathcer.test(porp)) {
                                 return true;
                             } 
                         } 
                     }
                     return false;
                 });
             };
            
            win.on({
                btnOKClick: function() {
                	me.isTrue=false;
                    if (comtab === bumenxiashuList) {
                        var data = win.getComponent('xuanzeyuangongList').getAllChecked();
                        var btnOK= win.getComponent('btnForm').getComponent('btnOK');
                        btnOK.disable();
                		if(data==''){
                			btnOK.enable();
                		}
                        Ext.each(data,function(item, index, allItems) {
                            var emp = "'" + item.HREmployee_Id + "'";
                            var id = "'" + treeId + "'";
                            var HRDept = '{Id:' + id + '' + '}';
                            var newAdd = '{' + 'Id:' + emp + ',' + 'HRDept:' + HRDept + '}';
                            var fields = 'HRDept_Id,Id';
                            var obj = {
                                entity: Ext.decode(newAdd)
                            };
                            obj.fields = 'HRDept_Id,Id';
                            var obj = {
                                entity: Ext.decode(newAdd),
                                fields: fields
                            };
                            var url = '' + getRootPath() + '/RBACService.svc/RBAC_UDTO_UpdateHREmployeeByField';
                            var params = Ext.JSON.encode(obj);
                            me.saveToTable(url, params, win);
                        });
                        if(me.isTrue==true){
                        	Ext.Msg.alert('提示', '保存成功！');
                        	 //部门直属员工列表tab
                            var bmzshql = 'hremployee.HRDept.Id=' + treeId;
                            bumenxiashuList.load(bmzshql);
                        }
                       
                    } else if (comtab === bumenxiangguanList) {
                        var dataXX = win.getComponent('xuanzeyuangongList').getAllChecked();
                        Ext.each(dataXX,function(item, index, allItems) {
                            var depDataTimeStampArr = '';
                            if (DataTimeStamp && DataTimeStamp != undefined) {
                                depDataTimeStampArr = DataTimeStamp.split(',');
                            }
                            var id = "'" + treeId + "'";
                            var HRDeptXX = '{Id:' + id + ',' + 'DataTimeStamp:[' + depDataTimeStampArr + ']}';
                            var HREmployeeDataTimeStamp = item.HREmployee_DataTimeStamp;
                            var rightArrXX = '';
                            if (HREmployeeDataTimeStamp && HREmployeeDataTimeStamp != undefined) {
                                rightArrXX = HREmployeeDataTimeStamp.split(',');
                            }
                            var empXX = "'" + item.HREmployee_Id + "'";
                            var HREmployeeXX = '{Id:' + empXX + ',' + 'DataTimeStamp:[' + rightArrXX + ']}';
                            var newAddXX = '{' + 'LabID:0,' + 'Id:-1,' + 'HRDept:' + HRDeptXX + ',' + 'HREmployee:' + HREmployeeXX + '' + '}';
                            var objXX = {
                                entity: Ext.decode(newAddXX)
                            };
                            var urlXX = '' + getRootPath() + '/RBACService.svc/RBAC_UDTO_AddHRDeptEmp';
                            var paramsXX = Ext.JSON.encode(objXX);
                            me.saveToTable(urlXX, paramsXX, win);
                        });
                        if(me.isTrue==true){
                        	Ext.Msg.alert('提示', '保存成功！');
                        	var bmxgyghql = 'hrdeptemp.HRDept.Id in(' + nodevalueP + ')';
                            bumenxiangguanList.load(bmxgyghql);
                        }
                        
                    }
                },
                closeClick: function() {
                    win.close();
                }

            });
        };
        /**
         * 模糊查询过滤函数(a部门相关员工）
         * @param {} value
         */
        me.filterFnxgyg = function(value) {
            var me = this,
            valtemp = value;
            var store = bumenxiangguanList.getStore(); //reload()
            if (!valtemp) {
                store.clearFilter();
                return;
            }
            valtemp = String(value).trim().split(' ');
            store.filterBy(function(record, id) {
                var data = record.data;
                var CName = record.data.HRDeptEmp_HREmployee_CName;
                var HRDept = record.data.HRDeptEmp_HREmployee_HRDept_CName;
                var dataarr = {
            		HRDeptEmp_HREmployee_CName: CName,
                    HRDeptEmp_HREmployee_HRDept_CName: HRDept
                };
                for (var p in dataarr) {
                    var porp = String(dataarr[p]);
                    for (var i = 0; i < valtemp.length; i++) {
                        var macther = valtemp[i];
                        var macther2 = Ext.escapeRe(macther);
                        mathcer = new RegExp(macther2);
                        if (mathcer.test(porp)) {
                            return true;
                        }
                    }
                }
                return false;
            });
        };
        /**
         * 模糊查询过滤函数(左）
         * @param {} value
         */
        me.filterFnxs = function(value) {
            var me = this,
            valtemp = value;
            var store = bumenxiashuList.getStore(); //reload()
            if (!valtemp) {
                store.clearFilter();
                return;
            }
            valtemp = String(value).trim().split(' ');
            store.filterBy(function(record, id) {
                var data = record.data;
                var CName = record.data.HREmployee_CName;
                var HRDept = record.data.HREmployee_HRDept_CName;
                var dataarr = {
                    HREmployee_CName: CName,
                    HREmployee_HRDept_CName: HRDept
                };
                for (var p in dataarr) {
                    var porp = String(dataarr[p]);
                    for (var i = 0; i < valtemp.length; i++) {
                        var macther = valtemp[i];
                        var macther2 = Ext.escapeRe(macther);
                        mathcer = new RegExp(macther2);
                        if (mathcer.test(porp)) {
                            return true;
                        }
                    }
                }
                return false;
            });
        };
    },
  
    //保存
    saveToTable: function(url, strobj, win) {
        var me = this;
        Ext.Ajax.defaultPostHeader = "application/json";
        Ext.Ajax.request({
            async: false,
            url: url,
            params: strobj,
            method: "POST",
            timeout: 5e3,
            success: function(response, opts) {
                var result = Ext.JSON.decode(response.responseText);
                if (result.success) {
                	me.isTrue=true;
//                    Ext.Msg.alert("提示", "保存成功！");
                    win.close();
                } else {
                	me.isTrue=false;
                    Ext.Msg.alert("提示", '保存信息失败！错误信息【<b style="color:red">' + result.ErrorInfo + "</b>】");
                }
            },
            failure: function(response, options) {
                Ext.Msg.alert("提示", "保存信息请求失败！");
            }
        });
    },
    createItems: function() {
        var me = this;
        Ext.Loader.setConfig({
            enabled: true
        });
        Ext.Loader.setPath('Ext.zhifangux.BasicTabPanel', getRootPath() + '/ui/zhifangux/BasicTabPanel.js');
        Ext.Loader.setPath('Ext.zhifangux.FormPanel', getRootPath() + '/ui/zhifangux/FormPanel.js');
        Ext.Loader.setPath('Ext.manage.departmentstaff.bumenxiangguanList', getRootPath() + '/ui/manage/class/departmentstaff/bumenxiangguanList.js');
        Ext.Loader.setPath('Ext.manage.departmentstaff.bumenTreeQuery', getRootPath() + '/ui/manage/class/departmentstaff/bumenTreeQuery.js');
        Ext.Loader.setPath('Ext.manage.departmentstaff.bumenxiashuList', getRootPath() + '/ui/manage/class/departmentstaff/bumenxiashuList.js');
        //部门树
        var bumenTreeQuery = Ext.create("Ext.manage.departmentstaff.bumenTreeQuery", {
            itemId: 'bumenTreeQuery',
            split: true,
            header: true,
            collapsible: true,
            region: 'west',
            width: 300
        });
        //部门下属员工
        var bumenxiashuList = Ext.create("Ext.manage.departmentstaff.bumenxiashuList", {
            itemId: 'bumenxiashuList',
            header: false,
            collapsible: true,
            region: 'west',
            border: false,
            width: 260
        });
        //部门相关员工
        var bumenxiangguanList = Ext.create("Ext.manage.departmentstaff.bumenxiangguanList", {
            itemId: 'bumenxiangguanList',
            header: false,
            collapsible: true,
            border: false,
            region: 'west',
            width: 280
        });
        var BasicTabPanel = Ext.create("Ext.zhifangux.BasicTabPanel", {
            name: 'BasicTabPanel',
            itemId: 'BasicTabPanel',
            region: 'center',
            split: true,
            border: true,
            header: false,
            collapsible: true,
            activeTab: 0,
            collapsed: false,
            layout: 'auto',
            items: [bumenxiashuList, bumenxiangguanList]
        });
        var appInfos = [bumenTreeQuery, BasicTabPanel];
        return appInfos;
    }

});