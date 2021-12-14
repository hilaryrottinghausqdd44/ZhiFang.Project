

Ext.ns('Ext.zhifangux');
Ext.define('Ext.zhifangux.DepTree', {
    extend: 'Ext.panel.Panel',
    alias: 'widget.deptree',
    layout: 'absolute',
    margin: 4,
    border: false,
    frame: false,
    padding: 0,
     TreeStore: function () {
         var me = this;
         var store = new Ext.create('Ext.data.TreeStore',{
             proxy: {//数据代理
                 type: 'ajax',//请求方式
                 url:'server/bmtree.json'//数据访问路径  'server/BZtree.json'
             },
             defaultRootProperty: 'tree',//子节点的属性名
             checked:false,
             root: {
                 text: '树菜单',
                 leaf: false,      //是否是页子节点
               //  children:data,
                 checked:false,
                 expanded: false    //是否默认打开
             },
             folderSort: true,
             //排序
             sorters: [{
                 property: 'text',
                 direction: 'ASC'
             }]
         }
         
         );
            
         return store;
     },
     
    /**
    * 常量设置
    * @private
    */
    initConstants: function () {
        var me = this;
       
    },

   
    /**
    * 生成树
    * @private
    * @return {}
    */
    createTree: function () {
        var me = this;
        var tree = {
            xtype: 'treepanel',
            id: 'tree2',        
            width: 250,       
            height: 300, 
            
            root: {
                expanded: true,
                children:[
                {
                 checked:true
                }
                ]
                //checked:true
            },
            store: me.TreeStore(),        
           // renderTo: document.body,
            viewConfig : {  
                //checkbox联动        
//    		    checkchange:function(node,checked){
//    	        if(node.data.leaf==false){     //当选中的不是叶子时
//    	             if(checked){
//    	                //打开父节点
//    	                node.expand();
//    	                //遍历子节点
//    	                node.eachChild(function(n){
//    	                    n.data.checked=true;
//    	                    n.updateInfo({checked:true});
//    	                         if(n.data.leaf==false){
//    	                             me.setNode(n);
//    	                         }
//    	                })
//    	             }
//    	             else{
//    	                 node.expand();
//    	                 //遍历子节点
//    	                 node.eachChild(function(n){
//    	                     n.data.checked=false;
//    	                     n.updateInfo({checked:false}); 
//    	                     if(n.data.leaf==false){
//    	                          me.setNodefalse(n);
//    	                     }
//
//    	                  })
//    	             }
//    	        }else   //当前选中的是叶子时
//    	        {
//    	            if(!checked){
//    	                node.parentNode.data.checked=false;
//    	                node.parentNode.updateInfo({checked:false}); 
//    	                me.setNodefalse(n);
//    	            }
//    	        }
//    	     }
    		onCheckboxChange : function(e, t) {          
    		var item = e.getTarget(this.getItemSelector(), this.getTargetEl()), record;         
    		if (item){           
    			record = this.getRecord(item);          
    			var check = !record.get('checked');           
    			record.set('checked', check);           
    			if (check) {            
    				record.bubble(function(parentNode) {             
    					parentNode.set('checked', true);            
    					parentNode.expand(false, true);            
    			    });           
                    record.cascadeBy(function(node) {             
                    	node.set('checked', true);            
                    	node.expand(false, true);            
                    });           
                } else {            
                    record.cascadeBy(function(node) {            
                        node.set('checked', false);           
                    });           
               }          
    	   } 
    	}        
    	}
        }
//        tree.listeners = {
//        	render:function(t){               
//                var tree = t.store;
//                var rootnodes = tree.getRootNode().childNodes; //获取主节点      
//                //alert(rootnodes);
////                 function iterNodes(rootnodes){            
////                     for(var i=0;i<rootnodes.length;i++){               
////                          rootnodes[i].data.checked=false   //设置checkebox      
////                          if(rootnodes[i].childNodes.length>0){                        
////                               iterNodes(rootnodes[i].childNodes) //如果有子节点就递归。     
////                           }            
////                     }    
////                }   
////                iterNodes(rootnodes);
//           }
//        }
        return tree;
    },
   
    /**
    * 注册事件
    * @private
    */
    addAppEvents: function () {
        var me = this;
//        me.addEvents('closeClick'); //删除按钮点击
//        me.addEvents('dropClick'); //下拉按钮点击
//        me.addEvents('imgClick'); //点击图片
      
    },
  afterRender: function() {
        var me = this;
       var dd=Ext.getCmp("tree2");
       dd.checked=false;
        alert(dd);
        me.callParent(arguments);
    },
   
    /**
    * 组装组件内容
    * @private
    */
    setAppItems: function () {
        var me = this;
        var items = [];
        //图片
        var tree = me.createTree();
        if (tree)
        	items.push(tree);


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