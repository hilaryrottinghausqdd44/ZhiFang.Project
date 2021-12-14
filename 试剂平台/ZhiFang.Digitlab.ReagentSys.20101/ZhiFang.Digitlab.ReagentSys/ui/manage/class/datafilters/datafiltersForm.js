
/***
 * 数据过滤条件表单
 */
Ext.ns('Ext.manage');
Ext.define('Ext.manage.datafilters.datafiltersForm', {
    extend:'Ext.form.Panel',
    alias:'widget.datafiltersForm',
    title:'表单',
    //width:500,
    height:66,
    isLoadingComplete:false,
    ParentID:0,
    LevelNum:1,
    TreeCatalog:1,
    ParentName:'',
    setdataTimeStampValue:true,
    header:true,
    objectName:'RBACRoleRight',
    isSuccessMsg:true,
    classCode:'BTDAppComponents_ClassCode',
    autoScroll:true,
    bodyCls:'',
    layout:'absolute',
    /***
     * 是否隐藏复制按钮
     * @type Boolean
     */
    isShowCopy:true,
    /**
     * 是否隐藏取消按钮
     * @type Boolean
     */
    isShowCancel:true,
    /***
     * 是否隐藏粘贴按钮
     * @type Boolean
     */
    isShowPaste:true,
    /***
     * 外部传入的模块操作ID
     * @type String
     */
    moduleOperId:'',

    beforeRender:function() {
        var me = this;
        me.callParent(arguments);

    },
    /**
     * 启用所有的操作功能
     * @private
     */
    enableControl:function(){
        var me = this,
            items = [];
            items=me.items.items
        for(var i in items){
            items[i].enable();
        }
    },
    /**
     * 禁用所有的操作功能
     * @private
     */
    disableControl:function(){
        var me = this,
            items = [];
            items=me.items.items
        for(var i in items){
            items[i].disable();
        }
    },
    initComponent:function() {
        var me = this;
        me.defaultTitle = me.title || '';
        me.addEvents('beforeSave');
        me.addEvents('saveClick');
        me.addEvents('changeUseRowFilter');
        
        var btnPredefinedAttributes={
            xtype:'button',
            text:'预定义可选属性',
            itemId:'btnPredefinedAttributes',
            name:'btnPredefinedAttributes',
            x:200,
            y:7,
            width:95,
            listeners:{
                click:function(com, e, eOpts ){
                    me.fireEvent('predefinedAttributesClick',com, e, me.objectName);
                }
            }
        };
        var btnisShowCopy={
            xtype:'button',
            text:'复制',
            itemId:'btnisShowCopy',
            name:'btnisShowCopy',
            x:320,
            y:7,
            hidden:me.isShowCopy,
            width:55,
            listeners:{
                click:function(com, e, eOpts ){
                    
                }
            }
        };
       var btnCancel={
            xtype:'button',
            text:'取消',
            itemId:'btnCancel',
            name:'btnCancel',
            hidden:true,
            x:385,
            y:7,
            width:55,
            listeners:{
                click:function(com, e, eOpts ){
                    
                }
            }
        };
        var btnisShowPaste={
            xtype:'button',
            text:'粘贴',
            itemId:'btnisShowPaste',
            name:'btnisShowPaste',
            hidden:true,
            x:385,
            y:7,
            width:55,
            listeners:{
                click:function(com, e, eOpts ){
                    
                }
            }
        };
        me.items = [ {
            xtype:'checkboxfield',
            itemId:'isUseRowFilter',
            x:5,
            y:7,
            name:'isUseRowFilter',
            fieldLabel:'',
            labelWidth:1,
            width:90,
            boxLabel:'采用过滤条件',
            inputValue:'true',
            uncheckedValue:'false',
            sortNum:1,
            listeners:{
                change:function ( com, newValue, oldValue, eOpts ) {
                    me.fireEvent('changeUseRowFilter',com, newValue, oldValue, eOpts );
                }
            },
            hasReadOnly:false,
            labelAlign:'left'
        }, {
            xtype:'button',
            text:'新增过滤条件',
            itemId:'btnAddDatafilters',
            name:'btnAddDatafilters',
            x:100,
            y:7,
            width:92,
            listeners:{
                click:function(com, e, eOpts ){
                    
                }
            }
        },btnPredefinedAttributes,btnCancel,btnisShowCopy,btnisShowPaste ];
        me.callParent(arguments);
    },
    /***
     * 布尔勾选赋值
     * 采用数据过滤条件
     * @return {}
     */
    setisUseRowFilter:function(value){
        var me=this;
        var com=me.getComponent('isUseRowFilter');
        if(com){
            com.setValue(value);
        }
    },
    afterRender:function() {
        var me = this;
        me.callParent(arguments);
        if (Ext.typeOf(me.callback) == 'function') {
            me.callback(me);
        }
    }
});