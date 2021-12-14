//非构建类--通用型组件或控件--树节点移动组件
//1.存在问题,界面风格不统一
/*公共属性
 * MoveType        移动类型
 * SaveType        保存类型
 * DataServerUrl       后台数据服务地址
 * TreeItem        获取整体树对象
 * SaveServerUrl       后台保存服务地址
 * 
 * 公开的方法
 * ExpandAll       展开所有
 * CollapseAll     收缩所有
 * SetMoveType     设置移动模式
 * GetMoveType     获取移动模式
 * SetSaveType     设置移动保存模式
 * GetSaveType     获取移动保存模式
 * SaveTree        保存树对象
 * ReLoadTree      重载树对象
 * 
 * 公开的事件
 * OnChanged       当树节点移动后触发
 * OnClick     单击树节点时触发
 * OnMove      移动节点时触发
 * */

Ext.ns("Ext.zhifangux");
Ext.define('Ext.zhifangux.UXTreePanel',{
	extend: 'Ext.panel.Panel',
    alias: 'widget.uxtreepanel', 
    border:true,
    width:300,
    height:400,
    title: '',//标题,默认值为''
    MoveType:"",          //移动类型
    SaveType:"",          //保存类型 0,1,2
    DataServerUrl:'',// 'server/newtree.json', //后台数据服务地址
    TreeItem:"",          //获取整体树对象
    SaveServerUrl:"",    //后台保存服务地址
    padding:0,
   
    //公共方法
    ExpandAll:function(){
        this.getComponent('tree').expandAll();
    },
    /*收缩所有节点
     * */
    CollapseAll:function(){
    	this.getComponent('tree').collapseAll();
    },
    /**
     * 【设置移动模式MoveType】
     * @public
     */
    SetMoveType:function(MoveType ){
       this.MoveType=MoveType;
    },
    /**
     * 【获取移动模式MoveType】
     * @public
     */
    GetMoveType:function(){
       return this.MoveType;
    },
    /**
     * 【设置移动保存模式SaveType】
     * @public
     */
    SetSaveType:function(SaveType ){
       this.SaveType=SaveType;
    },
    /**
     * 【获取移动保存模式SaveType】
     * @public
     */
    GetSaveType:function(){
       return this.SaveType=2;
    },
    //定义控制checkbox、button是不显示
     chboxbool:false,   //选择框是否可见
     butsave:true,     //保存按钮是否可
       /**
     * 常量设置
     * @private
     */
     initConstants:function(){
        var me = this;
        if(me.GetSaveType()==0){
            me.chboxbool=true;
            me.butsave=true;
        };
        //SaveType=1,界面上出现保存按钮，用户按保存后将移动结果保存起来
        if(me.GetSaveType()==1){
            me.chboxbool=true;
            me.butsave=false;
        };
        //SaveType=2,界面上出现复选框，  是否手工保存，默认为是，有保存按钮，取消复选，保存按钮隐藏
        if(me.GetSaveType()==2){
            me.chboxbool=false;
            me.butsave=false;
        };
     },
    /* 
     * 后台数据服务地址
    * */
    SetDataServerUrl:function(dateserverurl)
    {
        var me=this;
        me.DataServerUrl=dataserverurl;
    },
   /**
     * 【获取SaveServerUrl】
     * @public
     */
    SetSaveServerUrl:function(SaveServerUrl){
       this.SaveServerUrl=SaveServerUrl;
    },
    /**
     * 【获取SaveServerUrl】
     * @public
     */
    GetSaveServerUrl:function(){
       return this.SaveServerUrl;
    },
    //设置树节点
    addtreestore:function(){    
        var me=this;
        var store = new Ext.create('Ext.data.TreeStore',{
 	        proxy: {//数据代理
 	            type: 'ajax',//请求方式
 	            url:me.DataServerUrl  //数据访问路径  'server/BZtree.json'
 	        },
 	        defaultRootProperty: 'tree',//子节点的属性名
 	        root: {
 	            text: '树菜单',
 	            leaf: false,      //是否是页子节点
 	            expanded: true    //是否默认打开
 	        },
 	        //排序
 	        sorters: [{
 	            property: 'leaf',
 	            direction: 'ASC'
 	        }, {
 	            property: 'text',
 	            direction: 'ASC'
 	       }]
       });
      return store;
     },   
   
     //保存的对象
     Save:function(modified)
     {
    	 var t =this.getComponent('tree');

    	 var modified =t.getStore().getRootNode().childNodes;
    	 var me = this;
    	 var json = []; 
         Ext.each(modified, function(item){ 
             json.push(item.data); 
         }); 
         if (json.length > 0){ 
             Ext.Ajax.request({ 
            	 async:false,//非异步
                 url: me.SaveServerUrl, 
                 params:{ data: Ext.JSON.encode(json) }, 
                 method: "POST", 
                 success: function(response){ 
                     Ext.Msg.alert("信息", "数据更新成功！", function(){
                    	 var t2 = me.getComponent('left');
                    	 t2.getStore().reload();
                    	 var t1 = me.getComponent('right');
                    	 t1.getStore().reload();
                     });
                     alert(Ext.JSON.encode(json));
                 }, 
                 failure: function(response){ 
                     Ext.Msg.alert("警告", "数据更新失败，请稍后再试！"); 
                 } 
             }); 
         } 
         else { 
             Ext.Msg.alert("警告", "没有任何需要更新的数据！"); 
         };
     },

    /**
    * 生成树
    * @private
    * @return {}
    */
    createTreePanel: function () {
        var me = this;
        var tree = {
			 xtype:'treepanel',    //treepanel
	         itemId:'tree',
	         //title:'菜单树',//标题
	         width:me.width,
	         lines:false,
	         region:'north',
	         height:me.height-35,
	         store:me.addtreestore(),// store,//数据源
	         border:false,//是否有边框
	        // cls:'bg-white',//控件样式设置,默认值'bg-white',为"css/icon.css"里的.bg-white
	         collapsible: false,//是否在上方有收缩按钮
	         split: false,//是否可收缩   // 设置拖曳 ?
	         enableDD : true,//是否可拖拽
	         rootVisible: true,//是否显示根节点
	         containerScroll : true,//是否支持滚动条
	         autoScroll: true,//内容溢出的时候是否产生滚动条     
	         dockedItems: [
	             {xtype: 'toolbar',dock: 'top',border:false,  bodyCls:'bg-white',//控件主体背景样式,默认值'bg-white',为"css/icon.css"里的.bg-white
	              cls:'bg-white',//控件样式设置,默认值'bg-white',为"css/icon.css"里的.bg-white
	                items: [
	                   {xtype: 'button',text: '展开所有节点',
	                 	handler:function(){                  
	                         //展开所有节点                           
	                          me.ExpandAll() ;  
	                       }  
	                   },
	                   {xtype: 'button',text: '收缩所有节点',
	                 	  handler:function(){                  
	                       //收缩所有节点                         
	                 	      me.CollapseAll();  
	                     }   
	                   }
	               ]
	             }
	       ],
	       viewConfig: {
	            plugins: {
	                ptype: 'treeviewdragdrop',
	                allowLeafInserts : true
	            },
	            listeners:{
                    beforedrop:function(node,data,overModel,dropPosition,dropFunction,eOpts ){
                    }
                }
	       }            
        };
        tree.listeners = {
        };
        return tree;
    },
    createbutton: function () {
        var me = this;
        var button = {
            xtype: 'panel',
            height:35,
            width:me.width,
            dock: 'bottom',
            title: '',
            border: true,

            layout: {
                align: 'middle',
                pack: 'center',
                type: 'hbox'
            },
            items: [
                { xtype: 'checkbox',itemId:'ckbox', boxLabel:'是否手工保存',checked:true,hidden:me.chboxbool, x:155,y:35,margin:5,   //hidden:true,me.chboxbool
                     listeners:{
                         change:function(){                            
                              var checkboll=this.getValue();
                              if(checkboll){
                                   me.butsave=false;   //选择框为选中时，保存按钮为可见
                                   Ext.getCmp('butSave').getId().hidden=true;
                               }
                               else{
                                   me.butsave=true;    //选择框为不选中时，保存按钮不可见                           
                                   Ext.getCmp('butSave').hidden=me.butsave;
                                   Ext.getCmp('butSave').text='删除保存';
                               };                           
                           }
                    }
                },
	            { xtype: 'button', itemId:'butSave',hidden:me.butsave, text: '保   存',margin:5,
                	 handler : function() { 
                	    
		                 var t =me.getComponent('tree');
			             me.Save();
	                 }
	           }  
            ]
        };
        return button;
    },
    /**
    * 注册事件
    * @private
    */
    addAppEvents: function () {
        var me = this;
        me.addEvents('OnChanged');//当树节点移动后触发
        me.addEvents('OnClick');//单击树节点时触发
        me.addEvents('OnMove');//移动节点时触发    
    },
    /**
    * 组装组件内容
    * @private
    */
    setAppItems: function () {
        var me = this;
        var items = [];
        var tree = me.createTreePanel();
        if (tree)
            items.push(tree);
        var button = me.createbutton();
        if (button)
            items.push(button);
        me.items = items;
    },
   
    /**
    * 初始化组件
    */
    initComponent: function () {
        var me = this;
        //常量设置
        me.initConstants();
        //注册事件
        me.addAppEvents();
        //组装组件内容
        me.setAppItems();
        this.callParent(arguments);
	    }
	});
	Ext.override(Ext.tree.ViewDropZone, {
	    getPosition: function (e, node) {
	        var view = this.view,
	        record = view.getRecord(node),
	        y = e.getPageY(),
	        noAppend = record.isLeaf(),
	        noBelow = false,
	        region = Ext.fly(node).getRegion(),
	        fragment;
	
	        if (record.isRoot()) {
	            return 'append';
	        }
	
	        if (this.appendOnly) {
	            return noAppend ? false : 'append';
	        }
	        if (!this.allowParentInsert) {
	            noBelow = this.allowLeafInserts || (record.hasChildNodes() && record.isExpanded());
	        }
	        fragment = (region.bottom - region.top) / (noAppend ? 2 : 3);
	        if (y >= region.top && y < (region.top + fragment)) {
	            return 'before';
	        }
	        else if (!noBelow && (noAppend || (y >= (region.bottom - fragment) && y <= region.bottom))) {
	            return 'after';
	        }
	        else {
	            return 'append';
	        }
	    },
	    handleNodeDrop: function (data, targetNode, position) {
	        var me = this,
	        view = me.view,
	        parentNode = targetNode.parentNode,
	        store = view.getStore(),
	        recordDomNodes = [],
	        records, i, len,
	        insertionMethod, argList,
	        needTargetExpand,
	        transferData,
	        processDrop;
	        if (data.copy) {
	            records = data.records;
	            data.records = [];
	            for (i = 0, len = records.length; i < len; i++) {
	                data.records.push(Ext.apply({}, records[i].data));
	            }
	        }
	        me.cancelExpand();
	        if (position == 'before') {
	            insertionMethod = parentNode.insertBefore;
	            argList = [null, targetNode];
	            targetNode = parentNode;
	        }
	        else if (position == 'after') {
	            if (targetNode.nextSibling) {
	                insertionMethod = parentNode.insertBefore;
	                argList = [null, targetNode.nextSibling];
	            }
	            else {
	                insertionMethod = parentNode.appendChild;
	                argList = [null];
	            }
	            targetNode = parentNode;
	        }
	        else {
	            //leehom add begin
	            if (this.allowLeafInserts) {
	                if (targetNode.get('leaf')) {
	                    targetNode.set('leaf', false);
	                    targetNode.set('expanded', true);
	                }
	            }
	            if (!targetNode.isExpanded()) {
	                needTargetExpand = true;
	            }
	            insertionMethod = targetNode.appendChild;
	            argList = [null];
	        }
	
	        transferData = function () {
	            var node;
	            for (i = 0, len = data.records.length; i < len; i++) {
	                argList[0] = data.records[i];
	                node = insertionMethod.apply(targetNode, argList);
	
	                if (Ext.enableFx && me.dropHighlight) {
	                    recordDomNodes.push(view.getNode(node));
	                }
	            }
	            if (Ext.enableFx && me.dropHighlight) {
	                Ext.Array.forEach(recordDomNodes, function (n) {
	                    if (n) {
	                        Ext.fly(n.firstChild ? n.firstChild : n).highlight(me.dropHighlightColor);
	                    }
	                });
	            }
	        };
	        if (needTargetExpand) {
	            targetNode.expand(false, transferData);
	        }
	        else {
	            transferData();
	        }
		    }
		});
	Ext.override(Ext.tree.plugin.TreeViewDragDrop, {
	    allowLeafInserts: true,
	
	    onViewRender: function (view) {
	        var me = this;
	        if (me.enableDrag) {
	            me.dragZone = Ext.create('Ext.tree.ViewDragZone', {
	                view: view,
	                allowLeafInserts: me.allowLeafInserts,
	                ddGroup: me.dragGroup || me.ddGroup,
	                dragText: me.dragText,
	                repairHighlightColor: me.nodeHighlightColor,
	                repairHighlight: me.nodeHighlightOnRepair
	            });
	        }
	
	        if (me.enableDrop) {
	            me.dropZone = Ext.create('Ext.tree.ViewDropZone', {
	                view: view,
	                ddGroup: me.dropGroup || me.ddGroup,
	                allowContainerDrops: me.allowContainerDrops,
	                appendOnly: me.appendOnly,
	                allowLeafInserts: me.allowLeafInserts,
	                allowParentInserts: me.allowParentInserts,
	                expandDelay: me.expandDelay,
	                dropHighlightColor: me.nodeHighlightColor,
	                dropHighlight: me.nodeHighlightOnDrop
	            });
	        }
	    }
});