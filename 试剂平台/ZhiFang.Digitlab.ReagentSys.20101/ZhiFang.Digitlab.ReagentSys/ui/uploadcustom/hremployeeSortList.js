//非构建类--通用型组件或控件--列表排序组件:解决表数据的排序问题--需要修改
//存在问题:1:第一次启用手工调整开始时如何定位到gridpanel里的第一行第一列
//2: 启用手工拖动现在只支持一行拖动
//3: 启用手工拖动调整开始后,在第二次手工拖动时排序存在问题--2013-05-22解决处理好
/**
 * 对外公开属性
 *  title: '',//标题,默认值为''
 *  titleAlign :'center',//标题显示位置
 *  width:660,//容器宽度像素,默认值为660
 *  height:480,//容器高度像素,默认值为480
 *  bodyCls:'bg-white',//控件主体背景样式,默认值'bg-white',为'css/icon.css'里的.bg-white
 *  cls:'bg-white',//控件样式设置,默认值'bg-white',为'css/icon.css'里的.bg-white
 *  listorder:'',//传入指定的排序字段名
 *  saveType:1, //保存方式:SaveType=0 实时保存，SaveType=1展现保存按钮进行保存,默认值为1
 *  dataServerUrl:'',//后台服务地址
 *  saveServerUrl:'',//后台保存服务地址
 *  isToEdit:false,//是否启用单元格编辑,默认为false,不启用,
 *  myGridRoot:'list',// gridpanel的后台返回数据的root
 *   
 * 对外公开事件
 * onOKCilck 确定事件
 * onDrag 拖拽时触发
 * edit 当列表项被改变后触发
 * onCancelCilck 取消事件
 * 
 * 对外公开方法
 * setSaveType 设置保存方式
 * getSaveType 获取保存方式
 * getlastValue 返回给后台的数据方法
 * getValue 返回给后台的数据方法
 * setWidth 设置组件宽度
 * getWidth 返回组件宽度
 * setHeight 设置组件高度
 * getHeight 返回组件高度
 * setTitle 设置组件标题
 * getTitle 返回组件标题
 * 
 */
Ext.ns('Ext.zhifangux');

Ext.define('Ext.zhifangux.hremployeeSortList', {
    extend:'Ext.panel.Panel',
    alias:'widget.hremployeesortlist',
    layout:'border',
    frame:true,
    title:'',
    //标题,默认值为''
    border:true,
    //边框线显示 true,或隐藏false
    titleAlign:'center',
    //标题显示位置
    bodyCls:'bg-white',
    //控件主体背景样式,默认值'bg-white',为'css/icon.css'里的.bg-white
    cls:'bg-white',
    //控件样式设置,默认值'bg-white',为'css/icon.css'里的.bg-white
    width:560,
    //容器宽度像素,默认值为660
    height:480,
    //容器高度像素,默认值为480
    
    saveType:0,
    //保存方式:saveType=0 实时保存，saveType=1展现保存按钮进行保存,默认值为1
    
    //后台保存服务地址
    isToEdit:false,
    //是否启用单元格编辑,默认为false,不启用,
    isdisabled:true,
    //是否启用手工调整开始
    myGridRoot:'list',
    // gridpanel的后台返回数据的root
    internalWhere:'',
    //列表的内部hql
    externalWhere:'',
    
    saveServerUrl:'/RBACService.svc/RBAC_UDTO_UpdateHREmployeeByField',
    listorder:'HREmployee_DispOrder',
    //传入指定的排序字段名
    //列表的外部hql
    objectName:'HREmployee',
    //需要更新数据的数据对象名称
    gridCom:null,
    getserverUrl:getRootPath() + '/RBACService.svc/RBAC_UDTO_SearchHREmployeeByHQL?isPlanish=true&fields=HREmployee_CName,HREmployee_EName,HREmployee_IsEnabled,HREmployee_HRDept_CName,HREmployee_Id,HREmployee_DispOrder,HREmployee_HRDept_Id,HREmployee_HRDept_DataTimeStamp,HREmployee_DataTimeStamp',
    fields:[ 'HREmployee_CName', 'HREmployee_EName', 'HREmployee_IsEnabled', 'HREmployee_HRDept_CName', 'HREmployee_Id', 'HREmployee_DispOrder', 'HREmployee_HRDept_Id', 'HREmployee_HRDept_DataTimeStamp', 'HREmployee_DataTimeStamp' ],
    columns:[ {
            text:'员工名称',
            dataIndex:'HREmployee_CName',
            width:97,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        }, {
            text:'英文名称',
            dataIndex:'HREmployee_EName',
            width:100,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        }, {
            text:'在职',
            dataIndex:'HREmployee_IsEnabled',
            width:46,
            xtype:'booleancolumn',
            trueText:'是',
            falseText:'否',
            defaultRenderer:function(value) {
                if (value === undefined) {
                    return this.undefinedText;
                }
                if (!value || value === 'false' || value === '0' || value === 0) {
                    return this.falseText;
                }
                return this.trueText;
            },
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        }, {
            text:'部门名称',
            dataIndex:'HREmployee_HRDept_CName',
            width:115,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        }, {
            text:'员工主键ID',
            dataIndex:'HREmployee_Id',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'显示次序',
            dataIndex:'HREmployee_DispOrder',
            width:88,
            sortable:true,
            hidden:false,
            hideable:true,
            editor:{
                allowBlank:true
            },
            align:'left'
        }, {
            text:'部门主键ID',
            dataIndex:'HREmployee_HRDept_Id',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'部门时间戳',
            dataIndex:'HREmployee_HRDept_DataTimeStamp',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'时间戳',
            dataIndex:'HREmployee_DataTimeStamp',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        } ],
    //列表组件对象
    /***
      * 重新加载列表数据
      * @param {} where
      */
    load:function(where) {
        var me = this;
        me.externalWhere = where;
        var w = '';
        if (me.leftInternalWhere) {
            w += me.internalWhere;
        }
        if (where && where != '') {
            if (w != '') {
                w += ' and ' + where;
            } else {
                w += where;
            }
        }
        var leftGrid = me.getGridItem();
        if (leftGrid) {
            leftGrid.store.proxy.url = me.getserverUrl + '&where=' + w;
            leftGrid.store.load();
        }
    },
    /**
     * setTitle 获取组件标题
     * @param {} title
     */
    getTitle:function(title) {
        var me = this;
        return me.title;
    },
    /**
     * 设置组件宽度
     * @param {} width
     */
    setWidth:function(width) {
        var me = this;
        return me.setSize(width);
    },
    /**
     * 返回组件宽度
     * @return {}
     */
    getWidth:function() {
        var me = this;
        return me.width;
    },
    /**
     * 设置组件高度
     * @param {} height
     */
    setHeight:function(height) {
        var me = this;
        return me.setSize(undefined, height);
    },
    /**
     * 返回组件高度
     * @return {}
     */
    getHeight:function() {
        var me = this;
        return me.height;
    },
    /**
     * 解析处理传入数据列表值字段,
     * 封装生成store数据的Field数组
     * 
     */
    setmyGridField:function() {
        var me = this;
    },
    /**
    * 手工调整开始
    */
    onStartBtnClick:function() {
        var me = this;
        me.isToEdit = true;
    },
    /**
    * 手工调整完成
    */
    onFinishBtnClick:function() {
        var me = this;
        var grid = me.getGridItem();
        grid.getStore().sort(me.listorder, 'asc');
    },
    /**
    * 全部按排序字段排序
    */
    onAllNewSortBtnClick:function() {
        var me = this;
        var grid = me.getGridItem();
        grid.getStore().sort(me.listorder, 'asc');
        var store = grid.getStore();
        //与后台数据同步
        var text = '' + me.listorder;
        var tempArr = me.listorder.split('_');
        var tempStr = tempArr[tempArr.length - 1];
        var value = 0;
        store.each(function(model) {
            value = ++value;
            var strDataTimeStamp = '' + me.objectName + '_' + 'DataTimeStamp';
            var DataTimeStampValue = model.get(strDataTimeStamp);
            var DataTimeStampArr = [];
            if (DataTimeStampValue && DataTimeStampValue != undefined) {
                DataTimeStampArr = DataTimeStampValue.split(',');
            } else {
                Ext.Msg.alert('提示', '不能保存,构建时没有选择列表的数据对象的时间戳列');
                return;
            }
            var id = model.get(me.objectName + '_Id');
            var obj = '{' + 'Id:' + id + ',' + tempStr + ':' + value + ',' + 'DataTimeStamp:[' + DataTimeStampArr + ']}';
            obj = Ext.decode(obj);
            me.saveTheData(obj);
        });
        me.load('');
    },
    /**
    * 确 定
    */
    onOKCilck:function() {
        var me = this;
        var grid = me.getGridItem();
        var store = grid.getStore();
        
	    var editer_id = me.objectName + "_Id";
	    var editerFields = "";
	    var arrFields = [ "HREmployee_DispOrder" ];
	    if (editer_id === "" || editer_id === null) {
	        Ext.Msg.alert("提示", "请选择交互字段进行操作！");
	        return;
	    } else {
	        arrFields.push(editer_id);
	    }
	    var strCount = store.getModifiedRecords();
        for (var i = 0; i < strCount.length; i++) {
            for (var j = 0; j < arrFields.length; j++) {
                editerFields = editerFields + arrFields[j] + ":'" + strCount[i].get(arrFields[j]) + "',";
            }
            editerFields = editerFields.substring(0, editerFields.length - 1);
            editerFields = "{" + editerFields + "}";
            var editerJSON = Ext.JSON.decode(editerFields);
            me.saveToTable(editerJSON);
            editerFields = "";
        }
        me.load('');
                    
    },
    saveToTable:function(strobj) {
        var me=this;
        var url = '';
        if (me.saveServerUrl!= "") {
            url = getRootPath() + me.saveServerUrl;
        } else {
            Ext.Msg.alert("提示", '<b style="color:red">' + "【没有配置保存数据服务地址！】</b>");
            return null;
        }
        var values = strobj;
        var maxLength = 0;
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
            for (var i = 1; i < keyArr.length; i++) {
                ob = ob + '["' + keyArr[i] + '"]';
                objSJC = keyArr[i];
                if (!eval(ob)) {
                    eval(ob + "={};");
                }
            }
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
        for (var i in values) {
            var keyArr = i.split("_");
            field = field + keyArr.slice(1).join("_") + ",";
        }
        if (field != "") {
            field = field.slice(0, -1);
        }
        Ext.Ajax.defaultPostHeader = "application/json";
        Ext.Ajax.request({
            async:false,
            url:url,
            params:Ext.JSON.encode({
                entity:obj,
                fields:field
            }),
            method:"POST",
            timeout:5000,
            success:function(response, opts) {
                var result = Ext.JSON.decode(response.responseText);
                if (result.success) {
                    Ext.Msg.alert("提示", "保存成功！");
                } else {
                    Ext.Msg.alert("提示", '保存信息失败！错误信息【<b style="color:red">' + result.ErrorInfo + "</b>】");
                }
            },
            failure:function(response, options) {
                Ext.Msg.alert("提示", "保存信息请求失败！");
            }
        });
    },
    getGridItem:function() {
        var me = this;
        var com = me.getComponent('MyGridItemId');
        return com;
    },
    /**
    * 最顶部移动
    */
    onTopBtnClick:function() {
        var me = this;
        me.fireEvent('onTopBtnClick');
        var grid = me.getGridItem();
        var store = grid.getStore();
        var rows = grid.getSelectionModel().getSelection();
        var DispOrder2 = rows[0].data[me.listorder];
        //得到当前选中行的的排序编号
        var oldindex = rows[0].index;
        if (oldindex == 0) //为第一行时处理
        {
            return;
        }
        store.each(function(record) {
            if (DispOrder2 == Number(record.get(me.listorder))) {
                record.set(me.listorder, 1);
                record.commit();
            } else if (Number(record.index) <= oldindex) {
                record.set(me.listorder, Number(record.get(me.listorder)) + 1);
                record.commit();
            }
            //数据保存处理 0: 实时保存;1: 保存按钮
            if (me.saveType == 0) {
                me.immediateSaveData(record);
                store.sync();
                //与后台数据同步
                grid.getStore().sort(me.listorder, 'asc');
            }
        });
    },
    /**
    * 最底部移动
    */
    onBottomBtnClick:function() {
        var me = this;
        me.fireEvent('onBottomBtnClick');
        var grid = me.getGridItem();
        var store = grid.getStore();
        //
        var rows = grid.getSelectionModel().getSelection();
        var DispOrder2 = rows[0].data[me.listorder];
        //得到当前选中行的的排序编号
        var oldindex = rows[0].index;
        if (oldindex == store.count() - 1) //为最未尾行时处理
        {
            return;
        }
        store.each(function(record) {
            if (DispOrder2 == Number(record.get(me.listorder))) {
                record.set(me.listorder, store.count());
                record.commit();
            } else if (Number(record.index) >= oldindex) {
                record.set(me.listorder, Number(record.get(me.listorder)) - 1);
                record.commit();
            }
            //数据保存处理 0: 实时保存;1: 保存按钮
            if (me.saveType == 0) {
                me.immediateSaveData(record);
                store.sync();
                //与后台数据同步
                grid.getStore().sort(me.listorder, 'asc');
            }
        });
    },
    /**
    * 向上移动操作:当前移动行与当前移动行的前一行的排序编号互换位置;当前移动行排序编号减去1,当前移动行的前一行的排序编号加1
    */
    onUpBtnClick:function() {
        var me = this;
        me.fireEvent('onUpBtnClick');
        var grid = me.getGridItem();
        var store = grid.getStore();
        //
        var rows = grid.getSelectionModel().getSelection();
        var DispOrder2 = rows[0].data[me.listorder];
        //得到当前选中行的的排序编号
        if (rows[0].index == 0) //当前选中行为第一行时的处理,直接返回
        {
            return;
        }
        store.each(function(record) {
            if (DispOrder2 == Number(record.get(me.listorder))) {
                record.set(me.listorder, DispOrder2 - 1);
                record.commit();
            } else if (Number(record.get(me.listorder)) == DispOrder2 - 1) {
                record.set(me.listorder, Number(record.get(me.listorder)) + 1);
                record.commit();
            }
            //数据保存处理 0: 实时保存;1: 保存按钮
            if (me.saveType == 0) {
                me.immediateSaveData(record);
                store.sync();
                //与后台数据同步
                grid.getStore().sort(me.listorder, 'asc');
            }
        });
    },
    /**
    * 向下移动:当前移动行与当前移动行的下一行的排序编号互换位置;当前移动行排序编号减去1,当前移动行的前一行的排序编号加1
    */
    onDownBtnClick:function() {
        var me = this;
        me.fireEvent('onDownBtnClick');
        var grid = me.getGridItem();
        var store = grid.getStore();
        //
        var rows = grid.getSelectionModel().getSelection();
        var DispOrder2 = rows[0].data[me.listorder];
        //得到当前选中行的的排序编号
        if (rows[0].index == store.count() - 1) //当前选中行为最后一行时的处理,直接返回
        {
            return;
        }
        store.each(function(record) {
            if (DispOrder2 == Number(record.get(me.listorder))) {
                record.set(me.listorder, DispOrder2 + 1);
                record.commit();
            } else if (Number(record.get(me.listorder)) == DispOrder2 + 1) {
                record.set(me.listorder, Number(record.get(me.listorder)) - 1);
                record.commit();
            }
            //数据保存处理 0: 实时保存;1: 保存按钮
            if (me.saveType == 0) {
                me.immediateSaveData(record);
                store.sync();
                //与后台数据同步
                grid.getStore().sort(me.listorder, 'asc');
            }
        });
    },
    /**
    * 设置保存方式
    * 0: 实时保存 1: 保存按钮
    * @return {}
    */
    setSaveType:function(saveType) {
        this.saveType = saveType;
    },
    /**
   * 获取保存方式
   * 0: 实时保存 1: 保存按钮
   * @return {}
   */
    getSaveType:function() {
        return this.saveType;
    },
   /**
	* 创建gridPanel列表对象
	* 
	*/
    createMyGrid:function() {
        var me = this;
        var myGrid = Ext.create('Ext.grid.Panel', {
            itemId:'MyGridItemId',
            width:me.width - 175,
            height:me.height - 58,
            store:me.getStore(),
            columns:me.columns,
            enableDragDrop:true,
            //激活行拖动  
            ddGroup:'mySortlistGridDD',
            dropConfig:{
                appendOnly:false
            },
            resizable:{
                handles:'s e'
            },
            viewConfig:{
                plugins:{
                    ptype:'gridviewdragdrop',
                    enableDrag:true,
                    //可以设置是否启用或禁用拖放
                    enableDrop:true,
                    dragText:'请拖放到合适的行位置'
                },
                listeners:{
                    drop:function(node, data, overModel, dropPosition, eOpts) {
                        var oldIndex = data.item.viewIndex;
                        //原来的行记录索引
                        var newindex = overModel.index;
                        //拖放后的行记录索引
                        var grid = me.getGridItem();
                        var store = grid.getStore();
                        store.each(function(record) {
                            //先更新原来的
                            //行记录往上拖放
                            if (oldIndex > newindex && oldIndex == Number(record.index)) {
                                //如果找到原来的行记录,更新原来的行记录的排序编号为新的行记录索引+1
                                record.set(me.listorder, newindex + 1);
                                record.commit();
                            } else if (oldIndex < newindex && oldIndex == Number(record.index)) {
                                //如果找到原来的行记录,更新原来的行记录的排序编号为新的行记录索引+1
                                record.set(me.listorder, newindex + 1);
                                record.commit();
                            } else if (oldIndex > newindex && Number(record.index) < oldIndex && Number(record.index) >= newindex) {
                                record.set(me.listorder, Number(record.get(me.listorder)) + 1);
                                record.commit();
                            } else if (oldIndex < newindex && Number(record.index) > oldIndex && Number(record.index) <= newindex) {
                                record.set(me.listorder, Number(record.get(me.listorder)) - 1);
                                record.commit();
                            }
                            //数据保存处理 0: 实时保存;1: 保存按钮
                            if (me.saveType == 0) {
                                me.immediateSaveData(record);
                                store.sync();
                                //与后台数据同步
                                grid.getStore().sort(me.listorder, 'asc');
                            }
                        });
                    }
                }
            },
            editor:{
                allowBlank:false
            },

            selType:'rowmodel',
            //'cellmodel',//单元格形式   //rowmodel
            plugins:[ Ext.create('Ext.grid.plugin.CellEditing', {
                //RowEditing
                editing:false,
                pluginId:'my_SortListcellplugin',
                clicksToEdit:1
            }) ],
            listeners:{
                beforeedit:function(editor, e, eOpts) {
                    //设置是否启用单元格编辑,点击了手工调整开始才会设置启用isToEdit=true
                    if (me.isToEdit) {
                        return true;
                    } else {
                        return false;
                    }
                },
                edit:function(editor, e, eOpts) {
                    var rows = this.getSelectionModel().getSelection();
                    var newDispOrder = rows[0].data['DispOrder'];
                    //得到当前编辑行编辑后的的排序编号
                    me.fireEvent('edit');
                },
                itemdblclick:function() {
                    me.fireEvent('itemdblclick');
                },
                itemclick:function() {
                    me.fireEvent('itemclick');
                },
                columnresize:function(ct, column, width, e, eOpts) {
                    me.fireEvent('columnresize');
                },
                columnmove:function(ct, column, fromIdx, toIdx, eOpts) {
                    me.fireEvent('columnmove');
                }
            }
        });
        me.gridCom = myGrid;
        return myGrid;
    },
   /***
    * 重新加载数据
    * @param {} where
    */
    load:function(where) {
        var me = this;
        var grid = me.getGridItem();
        me.externalWhere = where;
        var w = '';
        if (me.internalWhere) {
            w += me.internalWhere;
        }
        if (where && where != '') {
            if (w != '') {
                w += ' and ' + where;
            } else {
                w += where;
            }
        }
        grid.store.proxy.url = me.getserverUrl + '&where=' + w;
        grid.store.load();
        grid.getStore().sort(me.listorder, 'asc');
    },
   /**
	* 创建列表对象数据源
	* 
	* 
	*/
    getStore:function() {
        var me = this;
        var myGridStore = null;
        if (me.getserverUrl == '') {
            Ext.Msg.alert('提示', '没有配置数据服务地址或者配置失败！');
            return null;
        }
        Ext.Ajax.request({
            async:false,
            //非异步
            url:me.getserverUrl,
            method:'GET',
            timeout:5000,
            success:function(response, opts) {
                var result = Ext.JSON.decode(response.responseText);
                if (result.success) {
                    var ResultDataValue = {
                        count:0,
                        list:[]
                    };
                    if (result['ResultDataValue'] && result['ResultDataValue'] != '') {
                        ResultDataValue = Ext.JSON.decode(result['ResultDataValue']);
                    }
                    var count = ResultDataValue['count'];
                    myGridStore = Ext.create('Ext.data.Store', {
                        fields:me.fields,
                        //实现数据项适配的功能
                        data:ResultDataValue,
                        proxy:{
                            type:'memory',
                            reader:{
                                type:'json',
                                root:me.myGridRoot
                            }
                        }
                    });
                } else {
                    Ext.Msg.alert('提示', '获取列表数据信息失败！');
                }
            },
            failure:function(response, options) {
                Ext.Msg.alert('提示', '获取列表数据信息请求失败！');
            }
        });
        return myGridStore;
    },
    initComponent:function() {
        var me = this;
        if (me.width < 560) {
            me.width = 560;
        }
        if (me.height < 480) {
            me.height = 480;
        }

        //创建
        me.setupItems();
        //添加事件，别的地方就能对这个事件进行监听
        this.addEvents('edit');
        //当列表项被改变后触发
        this.addEvents('onDrag');
        //拖拽时触发
        this.addEvents('onOKCilck');
        //确认按钮
        this.addEvents('onCancelCilck');
        //取消按钮
        this.addEvents('onDownBtnClick');
        this.addEvents('onUpBtnClick');
        this.addEvents('onBottomBtnClick');
        this.addEvents('onTopBtnClick');
        this.addEvents('itemdblclick');
        this.addEvents('itemclick');
        this.addEvents('columnresize');
        this.addEvents('columnmove');
        me.listeners = me.listeners || [];
        this.callParent(arguments);
    },
    /**
    * 创建容器的子项控件
    */
    setupItems:function() {
        var me = this;
        me.items = [ {
            xtype:me.createMyGrid(),
            width:me.width - 180,
            height:me.height - 60,
            region:'center'
        }, {
            region:'east',
            width:125,
            height:me.height - 60,
            layout:{
                type:'vbox',
                padding:'15',//上,右,下,左.
                width:110,
                height:me.height - 58,
                align:'stretch',
                //控件横向拉伸至容器大小
                pack:'start '
            },
            defaults:{
                margins:'5 5 10 5'
            },
            items:[ {
                xtype:'button',
                text:'顶 端',
                listeners:{
                    click:{
                        element:'el',
                        fn:function() {
                            me.onTopBtnClick();
                        }
                    }
                },
                itemId:'top'
            }, {
                xtype:'button',
                itemId:'up',
                listeners:{
                    click:{
                        element:'el',
                        fn:function() {
                            me.onUpBtnClick();
                        }
                    }
                },
                text:'向 上'
            }, {
                xtype:'button',
                itemId:'down',
                listeners:{
                    click:{
                        element:'el',
                        fn:function() {
                            me.onDownBtnClick();
                        }
                    }
                },
                text:'向 下'
            }, {
                xtype:'button',
                itemId:'bottom',
                listeners:{
                    click:{
                        element:'el',
                        fn:function() {
                            me.onBottomBtnClick();
                        }
                    }
                },
                text:'未 尾'
            }, {
                xtype:'button',
                itemId:'btnstart',
                text:'手工开始调整',
                listeners:{
                    click:{
                        element:'el',
                        fn:function() {
                            me.onStartBtnClick();
                        }
                    }
                }
            }, {
                xtype:'button',
                itemId:'btnfinish',
                text:'手工调整完成',
                listeners:{
                    click:{
                        element:'el',
                        fn:function() {
                            me.onFinishBtnClick();
                        }
                    }
                }
            }, {
                xtype:'button',
                text:'按排序字段排序',
                itemId:'btnallnewsort',
                listeners:{
                    click:{
                        element:'el',
                        fn:function() {
                            me.onAllNewSortBtnClick();
                        }
                    }
                }
            }, {
                xtype:'button',
                itemId:'btnok',
                text:'确 定',
                listeners:{
                    click:{
                        element:'el',
                        fn:function() {
                            me.fireEvent('onOKCilck');
                            me.onOKCilck();
                        }
                    }
                }
            }, {
                xtype:'button',
                itemId:'btncancel',
                text:'取 消',
                listeners:{
                    click:{
                        element:'el',
                        fn:function() {
                            me.fireEvent('onCancelClick');
                        }
                    }
                }
            } ]
        } ];
    },
    afterRender:function() {
        var me = this;
        me.callParent(arguments);
        
    },
    /**
     * 往后台保存所有的数据
     * 手工完成保存/确定
     */
    saveTheData:function(obj) {
        var me = this;
        var tempArr = me.listorder.split('_');
        var tempStr = tempArr[tempArr.length - 1];
        var url ='';
        if (me.saveServerUrl!= "") {
            url = getRootPath() + me.saveServerUrl;
        } else {
            Ext.Msg.alert("提示", '<b style="color:red">' + "【没有配置保存数据服务地址！】</b>");
            return null;
        }
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            async:false,
            //非异步
            url:url,
            params:Ext.JSON.encode({
                'entity':obj
                ,'fields':'Id,' + tempStr + ',' + 'DataTimeStamp'
            }),
            method:'POST',
            timeout:5000,
            success:function(response, opts) {},
            failure:function(response, options) {
                Ext.Msg.alert('提示', '保存信息请求失败！');
            }
        });
    },
    /**
     * 即时保存数据
     * 上移,下移,顶端,底部
     */
    immediateSaveData:function(record) {
        var me = this;
        var text = me.listorder;
        var id = record.get(me.objectName + '_Id');
        var value = record.get(text);
        var tempArr = me.listorder.split('_');
        var tempStr = tempArr[tempArr.length - 1];
        var strDataTimeStamp = '' + me.objectName + '_' + 'DataTimeStamp';
        var DataTimeStampValue = record.get(strDataTimeStamp);
        var DataTimeStampArr = [];
        if (DataTimeStampValue && DataTimeStampValue != undefined) {
            DataTimeStampArr = DataTimeStampValue.split(',');
        } else {
            Ext.Msg.alert('提示', '不能保存,构建时没有选择列表的数据对象的时间戳列');
            return;
        }
        var url ='';
        if (me.saveServerUrl!= "") {
            url = getRootPath() + me.saveServerUrl;
        } else {
            Ext.Msg.alert("提示", '<b style="color:red">' + "【没有配置保存数据服务地址！】</b>");
            return null;
        }
        var obj = '{' + 'Id:' + id + ',' + tempStr + ':' + value + ',' + 'DataTimeStamp:[' + DataTimeStampArr + ']}';
        obj = Ext.decode(obj);
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            async:false,
            //非异步
            url:url,
            params:Ext.JSON.encode({
                'entity':obj
                ,'fields':'Id,' + tempStr + ',' + 'DataTimeStamp'
            }),
            method:'POST',
            timeout:5000,
            success:function(response, opts) {},
            failure:function(response, options) {
                Ext.Msg.alert('提示', '保存信息请求失败！');
            }
        });
    }
});