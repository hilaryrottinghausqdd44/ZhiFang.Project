//非构建类--通用型组件或控件--部门下拉绑定树选择器--未完成
//1:没有公开返回结果的方法
//2:下拉列表选择第二次报错(TypeError: protoEl is null)

Ext.ns("Ext.zhifangux");
Ext.define('Ext.zhifangux.UXComboboxTree',{
    extend: 'Ext.panel.Panel',//'Ext.panel.Panel',
    alias: 'widget.uxcomboboxtree',
    
    layout:'absolute',
    frame:true,
    padding:0,
    autoScroll:true,
    frameHeader:true,
    
    title: '',//标题
    border:false,//外层边框设置
    bodyCls:'bg-white',//控件主体背景样式,默认值'bg-white',为"css/icon.css"里的.bg-white
    cls:'bg-white',//控件样式设置,默认值'bg-white',为"css/icon.css"里的.bg-white
    width:300,          //下拉框宽度
    height:25,         //下拉框高度
    treeItemid:'',     //树ID
    comboItemid:'',    //下拉框ID
    fieldLabel:'',     //下拉框labal
    fieldText:'',      //下拉框text值
    DataServerUrl:'',     //树数据源
    
    childtree:'',       //树子节点的属性名
    roottext:'',        //根节点名称
    
    editable:'',   //是否可编辑
    emptyText:'',     //combox为空时提示显示默认值
    labelAlign:'left',
    
    /*初始化
     * 
     * */
    initComponent:function()
    {
        var me=this;       
        //创建树      
        me.createComboTree();
        //组装组件内容
        me.setItems();
        //注册事件
        me.addAppEvents();
       this.callParent(arguments);  
    //添加事件，别的地方就能对这个事件进行监听
      this.addEvents('OnChange');
   
    },    
    
    //初始化下拉框
    setItems:function()
    {
        var me=this;
        var items=[];
        var addcombo=me.createcombo();
        if(addcombo)
        {
            items.push(addcombo);
        }
        me.items=items;
    },
    
    /**
     * 注册事件
     * @private
     */
    addAppEvents:function(){
        var me = this;
        //me.addEvents('OnClick');//单击树节点时触发
        me.addEvents('OnChanged');//当改变后触发        
    },
    
    /**
     * 【获取fieldText】
     * @public
     */
    GetfieldText:function(){
       return this.fieldText;
    },
    
    /**
     * 【获取fieldLabel】
     * @public
     */
    SetfieldLabel:function(fieldLabel ){
       this.fieldLabel=fieldLabel;
    },
    
    /**
     * 【获取fieldLabel】
     * @public
     */
    GetfieldLabel:function(){
       return this.fieldLabel;
    },
    
    //创建下拉树对象
    createComboTree:function()
    {
        var me=this;
        
        tree = new Ext.tree.TreePanel({ 
            width:me.Width,
            height:me.Height, 
            itemId:'combotree',
            store:me.addtreestore(),
            border: true,//是否有边框
            collapsible: false,//是否在上方有收缩按钮
            split: false,//是否可收缩   // 设置拖曳 ?
            enableDD : true,//是否可拖拽
            rootVisible: false,//是否显示根节点     //控制树是否显示根节点
            containerScroll : false,//是否支持滚动条
            autoScroll: true,//内容溢出的时候是否产生滚动条  
            
            expanded:true,//此处展开所有。
            collapsedCls: 'collapsed' ,  
          //监听事件       
            listeners:{                
               itemclick:function(record,item,e,eOpts )
                    {     
                       me.getComponent('cbo_tree').setValue(item.data.text);
                       //me.getComponent('cbo1').setValue(item.data.text);   ///'cbo1'
                       //折叠此字段选择器“下拉
                       me.getComponent('cbo_tree').collapse();  //'cbo1'
                       me.fieldText=item.data.text;
                    }
                 }   
        });        
        return tree;
    },
   createMoreTree:function(){  
        var me = this;
        var tree = null;   
        tree={
            xtype:'treepanel',    //treepanel
            itemId:'tree',
            //title:'菜单树',//标题
            width:me.Width,
            height:me.Height,          
            store:me.addtreestore(),
            border: true,//是否有边框
            collapsible: false,//是否在上方有收缩按钮
            split: false,//是否可收缩   // 设置拖曳 ?
            enableDD : true,//是否可拖拽
            rootVisible: false,//是否显示根节点     //控制树是否显示根节点
            containerScroll : false,//是否支持滚动条
            autoScroll: true,//内容溢出的时候是否产生滚动条  
            expanded:true,//此处展开所有。
            collapsedCls: 'collapsed'               
        };  //          
        tree.listeners={      
            checkchange:function(node,checked)
            {
                if(node.data.leaf==false){     //当选中的不是叶子时
                     if(checked)
                     {
                        //打开父节点
                        node.expand();
                        //遍历子节点
                        node.eachChild(function(n){
                                 n.data.checked=true;
                                 n.updateInfo({checked:true});
                                 if(n.data.leaf==false){
                                   me.setNode(n);
                                 }
                                 } )
                     }
                     else{
                        node.expand();
                             //遍历子节点
                        node.eachChild(function(n){
                                 n.data.checked=false;
                                 n.updateInfo({checked:false}); 
                                  //me.setNode(n);
                                  if(n.data.leaf==false){
                                      me.setNodefalse(n);
                                  }

                                 } )
                     }
                }
                else   //当前选中的是叶子时
                {
                    if(!checked)
                    {
                        node.parentNode.data.checked=false;
                        node.parentNode.updateInfo({checked:false}); 
                        me.setNodefalse(n);
                    }
                }
                
            },              
         
             //单击树节点时触发
                itemclick:{
                    element:'el',
                    fn:function(){
                        me.fireEvent('OnChanged');
                        }
                     }                  
        }
        //返回树对象
        return tree;            
    },
    
 
    //生成下拉框对象
    createcombo:function()
    {
        var me=this;  //me.items
        combobox={
            xtype:'combo', 
            itemId:'cbo_tree',
            width : 25,
            border:false,
            labelAlign :me.labelAlign,
            fieldLabel:me.fieldLabel,//'Choose State',       
            store : new Ext.data.SimpleStore({  
                fields : [],  
                data : [[]]  
            }),  
            editable : me.editable||false,  
            emptyText : me.emptyText||"请选择",   
            triggerAction : 'all',  
            maxHeight : 100,  
            anchor : '100%',  
            displayField : 'text',  
            valueField : 'id',  
            tpl : "<tpl for='.'><div style='height:200px'><div id='tree1'></div></div></tpl>",
            queryMode: 'local',
            mode: 'local'
            };
            
            combobox.listeners={                 
                expand:function(){ 
                    tree.render('tree1');
                    tree.expandAll();                    
                },
                //单击下拉框时触发
                change:{                    
                    element:'el',
                    fn:function(){                        
                        //var cbo=me.getComponent('cbo1');
                        var cbo=me.getComponent('cbo_tree');
                        if(cbo)
                        {
                          tree.render('tree1');
                          tree.expandAll();    
                        }
                        cbo.fireEvent('Onchang');
                        }
                     }
                
            };
            layout: 'hbox';
            return  combobox; 
    }, 
    
   /* 设置树节点
    * 
   */       
   addtreestore:function(){    
        var me=this;
        var store = new Ext.create('Ext.data.TreeStore',{
        proxy: {//数据代理
            type: 'ajax',//请求方式
            url:me.DataServerUrl  //数据访问路径  'server/BZtree.json'
        },
        defaultRootProperty:me.childtree, //'tree',//子节点的属性名
        root: {
            text: me.roottext ,//'树菜单',
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
    }
    );
    return store;
       }    
}
)