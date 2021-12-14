//非构建类--通用型组件或控件--树的多选
/* 树的多选
 * 公共属性
 * LinkageType      联动方式
 * DataServerUrl       后台服务地址
 * SaveServerUrl       后台保存服务地址
 * width		宽
 * height		高
 * Root		    根节点

 * 公共方法  
 * SetLinkageType       设置联动方式
 * GetLinkageType      获取联动方式
 * Save        保存列表对象
 * ReLoad      重载列表对象
 * GetValue    Tree_id 获取选中树对象
 * 公共事件
 * OnChanged        当改变后触发
 * OnClick     单击树节点时触发
 * 
*/

Ext.ns("Ext.zhifangux");
Ext.define('Ext.zhifangux.UXMorecheckTree',{   
    extend: 'Ext.panel.Panel',
    alias: 'widget.uxmorchecktree',
    //layout:'absolute',
    title: '',//标题
    border:false,//外层边框设置
    frame:false,
    padding:0,
    bodyCls:'bg-white',//控件主体背景样式,默认值'bg-white',为"css/icon.css"里的.bg-white
    cls:'bg-white',//控件样式设置,默认值'bg-white',为"css/icon.css"里的.bg-white
    treeData:[],   // 后台json数据集合
    lastValue: [],  //返回获取到数据对象数组
    
    //公共属性
    LinkageType:0,         //联动方式，设置否联动参数
    DataServerUrl:'',       //后台服务地址
    SaveServerUrl:'',        //后台保存服务地址
    width: 300,
    height: 400,
    Root:'tree',
    
    //公共方法
    /**
     * 设置联动方式
     * @private
     * 参数LinkageType=1为联动，LinkageType=0为不联动
    */ 
    SetLinkageType:function(value){
        var me=this;
        me.LinkageType=value;
    },
    /**
    *获取联动方式
    */ 
    GetLinkageType:function(){
        var me=this;
        return me.LinkageType;
    },
 
    
    /**
     *保存列表对象
    */ 
    Save:function(modified)
    {
         var modified=this.GetValue();
	   	 var me = this;
	   	 var json = []; 
	        Ext.each(modified, function(item){ 
	            json.push(item); 
	        }); 
	        if (json.length > 0){ 
	            Ext.Ajax.request({ 
	           	    async:false,//非异步
	                url: me.SaveServerUrl, 
	                params:{ data: Ext.JSON.encode(json) }, 
	                method: "POST", 
	                success: function(response){ 
	                    Ext.Msg.alert("信息", "数据更新成功！", function(){
	                   	 var t1 = me.getComponent('tree');
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
     *重载列表对象
    */ 
    ReLoad:function(){
    	var tree = this.getComponent('tree');
    	tree.getStore().reload();
    },
    /**获取选中树对象
     * @private
     * @GetValue方法
     * @tree_id  树对象ID
    */ 
    GetValue:function(tree_id){      
    	var me = this;
    	
    	 var records =me.getComponent('tree').getView().getChecked();
         if(records.length > 0) {
            //当有选择的数据的时候   
       	    me.lastValue=[];
             for (var i = 0; i < records.length; i++) {
               //循环迭代所有的选择的row    
            	//records[i].checked= true;
             	me.lastValue.push(records[i].raw.checked=true);
             }
         }
         else{
             Ext.Msg.alert("请先选择节点");
             return;
         }
        return me.lastValue;
    },
    //向上遍历父节点
    nodep:function (node){ 
		var bnode=true;
		Ext.Array.each(node.childNodes,function(v){ 
			if(!v.data.checked){
				bnode=false;
				return;
			}
		});
		return bnode;
	},
	parentnode:function (node){
		if(node.parentNode != null){
			if(this.nodep(node.parentNode)){
				node.parentNode.set('checked',true);
			}else{ 
				node.parentNode.set('checked',false);
			}
			this.parentnode(node.parentNode);
		}
	},
	/**
     * 遍历子结点 选中 与取消选中操作
     * */
	chd:function (node,check){
		node.set('checked',check);
		if(node.isNode){
			node.eachChild(function(child){
				if (node.isLeaf){
					
				}
				else
				{
				   chd(child,check);
				}
			}); 
		}
	},

	//假树
	  setNodefalse:function(n) {
        var me=this;
        n.data.checked = false;
        n.updateInfo({ checked: false });
        var childs=n.childNodes;//alert(childs.length+'这个方法在这里调用不到');
        for (var i = 0; i < childs.length; i++) {
            childs[i].data.checked = false;
            childs[i].updateInfo({ checked: false });
            if(childs[i].data.leaf==false){
                me.setNodefalse(childs[i]);
            }
        }
     },
	 setNode:function(n) {
        var me=this;
        n.expand();
        n.data.checked = true;
        n.updateInfo({ checked: true });
        var childs=n.childNodes;//alert(childs.length+'内层调用');
            for (var i = 0; i < childs.length; i++) {
                //alert(i+childs[i].data.text+childs[i].data.leaf+'为true是叶节点');
                childs[i].data.checked = true;
                childs[i].updateInfo({ checked: true });
                if(childs[i].data.leaf==false)
                {
                   me.setNode(childs[i]);
                }
            }
     },
      
    /**
     * 获取数据源
     */     
    addtreestore:function(){    
        var me=this;
        var store = new Ext.create('Ext.data.TreeStore',{
	        proxy: {//数据代理
	            type: 'ajax',//请求方式
	            url:me.DataServerUrl  //数据访问路径  'server/BZtree.json'
	        },
	        defaultRootProperty:me.Root,//子节点的属性名
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
    /**
     * 常量设置
     * @private
     */
     initConstants: function () {
         var me = this;
         if (me.LinkageType == 0) {
        	 me.LinkageType = '0'; //真树
         }
         else if(me.LinkageType ==1) {
        	 me.LinkageType = '1'; //假树
         }
     },
    /**
    * 生成树对象
    */
    createMoreTree:function(){  
        var me = this;
        var tree = null;   
        tree={
            xtype:'treepanel',    //treepanel
            itemId:'tree',
            title:'',
            width:me.width,
            height:me.height,          
            store:me.addtreestore(),
            checkModel:'cascade', //对树的级联多选
            useArrows:true,//隐藏前导线使用箭头表示节点的展开
            rootVisible: false,//是否显示根节点     //控制树是否显示根节点
            containerScroll : false,//是否支持滚动条
            autoScroll: true,//内容溢出的时候是否产生滚动条  
            expanded:true,//此处展开所有。             
            dockedItems: [{
	            xtype: 'toolbar',
	            cls:'bg-white',//控件样式设置,默认值'bg-white',为"css/icon.css"里的.bg-white
	            border: me.treeBorder,//是否有边框
	            items: {
	                text: '获取选中项',
	                cls:'bg-white',//控件样式设置,默认值'bg-white',为"css/icon.css"里的.bg-white
	                handler: function(){
	                    var records =me.getComponent('tree').getView().getChecked();
                        if(records.length > 0) {
                           //当有选择的数据的时候   
                      	    me.lastValue=[];
                            for (var i = 0; i < records.length; i++) {
                              //循环迭代所有的选择的row                                              	
                            	me.lastValue.push(records[i].raw);
                            }
                     	   Ext.MessageBox.show({
                               title: 'Selected Nodes',
                               msg: Ext.encode(me.lastValue),
                               icon: Ext.MessageBox.INFO
                           });
                        }
                        else{
                            Ext.Msg.alert("请先选择节点");
                            return;
                        }
	                }
	            }
	        }]
        };  //          
        tree.listeners={
        	checkchange:function(node,checked){
        	    if (me.LinkageType == '1') {
        	    	if(checked){
        	    		   //打开父节点
                        node.expand();
                        //遍历子节点
  	    			    node.eachChild(function (child){
  	    			    	child.data.checked=true;
  	    			    	child.updateInfo({checked:true});
                          if(child.data.leaf==false){
                              me.setNode(child);
                          }
                          
                          
  	    				    me.chd(child,true);
  	    			    });
  	    		    }else{
  	    		    	 node.expand();
                         //遍历子节点
  	    			    node.eachChild(function (child){

  	    			    	child.data.checked=false;
  	    			    	child.updateInfo({checked:false}); 
                             if(child.data.leaf==false){
                                 me.setNodefalse(child);
                             }
  	    			    	
  	    				    me.chd(child,false);
  	    			    });
  	    	        }
      		        me.parentnode(node);//进行父级选中操作 
                }else{         	
                	if(node.data.leaf==false){     //当选中的不是叶子时
                         //alert('当前选中的节点是：'+node.data.text);
                        if(checked){
                             //打开父节点
                             node.expand();
                             //遍历子节点
                             node.eachChild(function(n){
                                  n.data.checked=true;
                                  n.updateInfo({checked:true});
                                  if(n.data.leaf==false){
                                      me.setNode(n);
                                  }
                             });
                          }else{
                              node.expand();
                                  //遍历子节点
                              node.eachChild(function(n){
                                  n.data.checked=false;
                                  n.updateInfo({checked:false}); 
                                  if(n.data.leaf==false){
                                      me.setNodefalse(n);
                                  }
                              });
                          }
                     }
                     else{
                        node.parentNode.data.checked=false;
                        node.parentNode.updateInfo({checked:false}); 
                     }
                }
        	    me.fireEvent('OnChanged');
            },
             //单击树节点时触发
             itemclick:function(){    
            	me.fireEvent('OnClick');
             }                 
        };
        //返回树对象
        return tree;            
    },

     /**
     * 注册事件
     * @private
     */
    addAppEvents:function(){
        var me = this;
        me.addEvents('OnClick');//单击树节点时触发
        me.addEvents('OnChanged');//当改变后触发        
    },
    /**
     * 组装组件内容
     * @private
     */
    setAppItems:function(){
        var me=this;
        var items=[];
        var treepanel =me.createMoreTree();
        if(treepanel)
            items.push(treepanel);           
        me.items=items; 
    },

    /**
    * 初始化组件
    */
    initComponent:function(){
        var me=this;
       
        //常量设置
        me.initConstants();
      
        //注册事件
        me.addAppEvents();

        //组装组件内容
        me.setAppItems();

        this.callParent(arguments); 
     }
});

  
