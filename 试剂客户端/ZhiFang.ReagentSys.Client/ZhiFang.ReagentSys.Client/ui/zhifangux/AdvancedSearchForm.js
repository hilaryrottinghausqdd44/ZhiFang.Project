//非构建类--通用型组件或控件--高级表单查询组件--表单全部与关系的查询条件
//未完成或存在问题:
/**
 * 修改中
 * 
 * 对外公开属性
 *   title:'',//窗体标题,默认为空
 *   titleAlign :"left",//窗体标题位置,默认为left
 *   width:790,//窗体宽度,默认值为790
 *   minWidth:790,//窗体最小宽度,默认值为790
 *   height:520,//窗体高度,默认值520
 *   bodyCls:'bg-white',//控件主体背景样式,默认值'bg-white',为"css/icon.css"里的.bg-white
 *   cls:'bg-white',//控件样式设置,默认值'bg-white',为"css/icon.css"里的.bg-white
 *   padding:1,
 *   lastValues:{},//最终往后台提交的数据
 *   saveServerUrl:'',//往后台保存数据的服务地址
 *   dataServerUrl:'',//数据后台服务地址
 *   myComboboxRoot:'list',//生成下拉列表控件时读取后台返回的数据源的root,默认值为list
 *  
 * 对外公开事件
 * btnOKClick 确定提交事件
 * btnClearAllClick 清除所有事件
 * btnColseClick 关闭事件
 * 
 * 对外公开方法
 * GetValue() //获取设置当前控件的最终结果值,如""
 * GetLastValue()//获取设置当前控件的最终结果值,如""
 * setWidth 设置组件宽度
 * getWidth 返回组件宽度
 * setHeight 设置组件高度
 * getHeight 返回组件高度
 * setTitle 设置组件标题
 * getTitle 返回组件标题
 */
 
Ext.Loader.setConfig({enabled: true});
Ext.Loader.setPath('Ext.zhifangux', getRootPath()+'/ui/zhifangux');

Ext.ns('Ext.zhifangux');
Ext.define('Ext.zhifangux.AdvancedSearchForm', {
    extend: 'Ext.form.Panel',
    alias: 'widget.advancedsearchform',
     
    requires: ['Ext.zhifangux.DateTime'],//需要加载的控件
    autoScroll: true,

    frame : true,
    layout : "absolute", // 整个大的表单是form布局
    
    id:'',//
    title:'',//窗体标题,默认为空
    titleAlign :"left",//窗体标题位置,默认为left
    border:false,
    width:790,//窗体宽度,默认值为790
    minWidth:790,//窗体最小宽度,默认值为790
    height:520,//窗体高度,默认值520
    bodyCls:'bg-white',//控件主体背景样式,默认值'bg-white',为"css/icon.css"里的.bg-white
    cls:'bg-white',//控件样式设置,默认值'bg-white',为"css/icon.css"里的.bg-white
    padding:1,
    lastValues:[],//最终往后台提交的数据
    saveServerUrl:'',//往后台保存数据的服务地址
    dataServerUrl:'',//数据后台服务地址
    myComboboxRoot:'list',//生成下拉列表控件时读取后台返回的数据源的root,默认值为list
     /**
     * setTitle 获取组件标题
     * @param {} title
     */
    getTitle:function(title){
        var me=this;
       return me.title;
    },
   /**
     * 设置组件宽度
     * @param {} width
     */
    setWidth:function(width){
        var me=this;
        return me.setSize(width);
    },
    /**
     * 返回组件宽度
     * @return {}
     */
    getWidth:function(){
        var me=this;
        //alert("getWidth "+me.width);
        return me.width;
    },
    /**
     * 设置组件高度
     * @param {} height
     */
    setHeight:function(height){
        var me=this;
         return me.setSize(undefined, height);
    },
    /**
     * 返回组件高度
     * @return {}
     */
    getHeight:function(){
        var me=this;
        return me.height;
    },
    //展示列表(form)
    FormViewRefresh:function (){
	    var me=this;
	    //var form = Ext.getCmp('my_FormView');
        var form = me.getComponent('my_FormView');
	    //SetHead(form);//设置标题属性
	    me.SetItems(form);//设置视图属性
            
        },
        //表单内部组件设置
    SetItems: function(form){
        var me=this;
            me.SetItemsC(form);//不带外框
        },
        //每个组件都不带外框
    SetItemsC: function(form){
        //取后台数据服务的数据对象
        var me=this,y=0;
        var listItems = null;
         Ext.Ajax.request({
        async:false,//非异步
        url:me.dataServerUrl,
        
        method:'POST',
        timeout:5000,
        success:function(response,opts){
        var result = Ext.JSON.decode(response.responseText);
        if(result.success){
        listItems=result.list;
        }else{
            Ext.Msg.alert('提示','获取信息失败！');
            }
        },
        failure : function(response,options){ 
            Ext.Msg.alert('提示','获取信息请求失败！');
        }
    });
        form.removeAll();
        for(var i in listItems){
            var ob = me.GetComponent(listItems[i]);
            if(ob != null){
                form.add(ob);
                y=ob.y;//将当前行的控件的y轴赋值给y保存
            }
        }
        var btnAdd=me.createbtnAdd(y);
        var btnClearAll=me.createbtnClearAll(y);
        var btnColse=me.createbtnColse(y);
        form.add(btnAdd);
        form.add(btnClearAll);
        form.add(btnColse);
    },
        
     //获取组件
     GetComponent:function(ob){
            var me,me2=this;
            var Type= ob.Type;
            if(Type === "ComboBox"){//下拉框
                me = me2.GetComboBox(ob);
            }else if(Type === "TextField"){//文本框
                me = me2.GetTextField(ob);
            }else if(Type === "TextAreaField"){//文本域
                me = me2.GetTextAreaField(ob);
            }else if(Type === "NumberField"){//数字框
                me = me2.GetNumberField(ob);
            }else if(Type === "DateField"){//日期框
                me =me2.GetDateField(ob);
            }else if(Type === "TimeField"){//时间框
                me = me2.GetTimeField(ob);
            }else if(Type === "Checkbox"){//复选框
                me = me2.GetCheckbox(ob);
            }else if(Type === "Radio"){//单选框
                me = me2.GetRadio(ob);
            }else if(Type === "Label"){//纯文本
                me = me2.GetLabel(ob);
            }else if(Type === "Button"){//按钮
                me = me2.GetButton(ob);
            }else{
                return null;
            }
            
            me.id = ob.Id;
            me.name=ob.EName,
            //me.readOnly= false,
            me.height = ob.Height;
            me.hidden = ob.IsHidden;
            //me.draggable = true;//可拖动
            me.x = ob.X;
            me.y = ob.Y;
            me.width = ob.Width;
            me.height = ob.Height;
            me.resizable = {
                dynamic: false,//是否动态
                pinned: false,
                handles: 'all'
            };
            me.DataUrl= ob.DataUrl;
            //me.listeners = GetMouseListeners(me);//组件拖动监听
            //me.resizable.listeners = GetResizableListeners(me);//组件大小变化监听
            return me;
        },
        
    //获取下拉框
     GetComboBox:function(ob){
       var DataUrl=ob.DataUrl,me2=this;
        var me = {
            xtype:'combobox',
            name:ob.EName,
            fieldLabel:ob.CName,
            mode:'local',
            editable:false,
            displayField:'text',
            valueField:'value',
            //value:1,
            store:me2.getMyComboboxStore(DataUrl),
            labelWidth:ob.LabelWidth
        };
        return me;
    },
        //获得生成下拉列表的数据源store
     getMyComboboxStore:function(DataUrl){
    var me = this;
    var myComboboxStore=null;
    var url=DataUrl;
    if(url==null||url==""){
    Ext.Msg.alert('提示','需要绑定下拉列表的数据服务地址！');
    return null; 
    }
        Ext.Ajax.request({
            async:false,//非异步
            url:DataUrl,
            method:'POST',
            timeout:10000,
            success:function(response,opts){
            var result = Ext.JSON.decode(response.responseText);
            if(result.success){
            myComboboxStore=Ext.create('Ext.data.Store', {  
            fields:['value','text'],
            data:result,
            proxy: {
            type: 'memory',
            reader: {
            type: 'json',
            root: me.myComboboxRoot
            }
        }
    });
            }else{
                    Ext.Msg.alert('提示','获取信息失败！');
                }
            },
            failure : function(response,options){ 
                Ext.Msg.alert('提示','获取信息请求失败！');
            }
        });
    return myComboboxStore;
   },
     //获取文本框
     GetTextField:function(ob){
            var me ={
                xtype:'textfield',
                //readOnly: false,
                itemId:ob.Id,
                value:'',
                name:ob.EName,
                fieldLabel:ob.CName,//'<font size=2 color=red>'+ob.CName+'</font>',
                labelWidth:ob.LabelWidth
            };
            return me;
        },
      //获取文本域
     GetTextAreaField:function(ob){
            var me ={
                xtype:'textareafield',
                readOnly: false,
                name:ob.EName,
                fieldLabel:ob.CName,
                labelWidth:ob.LabelWidth
            };
            return me;
        },
      //获取数字框
     GetNumberField:function(ob){
            var me = {
                xtype:'numberfield',
                id:ob.Id,
                name:ob.EName,
                fieldLabel:ob.CName,
                labelWidth:ob.LabelWidth
            };
            return me;
        },
     //获取日期框
     GetDateField:function(ob){
            var me = {
                xtype:'datetimefield',
                //id:ob.Id,
                //readOnly: false,
                //selectOnFocus: true,
                DateFormat: 'Y-m-d',
                name:ob.EName,
                LabField:ob.CName,
                LabFieldWidth:ob.LabelWidth
                
            };
            return me;
        },
     //获取时间框
     GetTimeField:function(ob){
            var me = {
                xtype:'datetimefield',
                TimeFormat: 'H:i',
                name:ob.EName,
                LabField:ob.CName,
                LabFieldWidth:ob.LabelWidth
            };
            return me;
        },
      //获取复选框
     GetCheckbox:function(ob){
            var me = {
                xtype:'checkboxfield',
                id:ob.Id,
                name:ob.EName,
                boxLabel:ob.CName,
                labelWidth:ob.LabelWidth
            };
            return me;
        },
      //获取单选框
     GetRadio:function(ob){
            var me = {
                xtype:'radiofield',
               // id:ob.Id,
                name:ob.EName,
                boxLabel:ob.CName,
                labelWidth:ob.LabelWidth
            };
            return me;
        },
     //获取纯文本
     GetLabel:function(ob){
            var me = {
                xtype:'label',
                name:ob.EName,
                text:ob.CName
            };
            return me;
        },
        //获取按钮
     GetButton:function(ob){
            var me = {
                xtype:'button',                
                name:ob.EName,
                text:ob.CName
            };
            return me;
        },
    /**
     * 增加单击事件
     */
     btnAddCilck:function(){},
    /**
     * 返回最终的数据
     * {"conditionName":"fdsa","roleList":[{"CategoryKey":"staff",
     * "CategoryName":"\u4eba\u5458","RelationId":"=",
     * "RelationName":"\u7b49\u4e8e","roleId":"10001",
     * "roleName":"xiaoxiao2"}],"fieldList":[]}
     * @return {}
     */
    GetLastValues:function(){
    var me=this;
    return me.lastValues;
    },
  
    initComponent:function(){
    var me = this;
    
    me.items= [
    {// 行1
        layout : "form",
        border:false,
        layout : "absolute", // 整个大的表单是form布局
        itemId:'my_FormView',
        height :me.height-me.padding-5,
        width:me.width-me.padding-5
    }
    ];
		//添加事件，别的地方就能对这个事件进行监听
		this.addEvents('btnOKClick');
        this.addEvents('btnClearAllClick');
        this.addEvents('btnColseClick');
    	this.callParent(arguments);
       //赋初始值
       me.FormViewRefresh();   
    },
    //提交事件
    //btnOKSave:function(){},
    //创建确定,清除,关闭按钮
    createbtnAdd:function(y){
    var me=this;
    var btnAdd=Ext.create('Ext.Button', {
        itemId:'MyRDS_btnAdd',
        text : "确定",
        y:me.height-40,
        x:me.width/2-80,//确定控件的x轴为容器的宽度的一半往左偏移80
        width : 80,
        maxWidth : 80,
        handler: function () {
            //alert("createbtnAdd");
            me.btnOKClick();
            me.fireEvent('btnOKClick');
            }
    });
    return btnAdd;
    },
     //创建清除按钮
    createbtnClearAll:function(y){
    var me=this;
    var btnClearAll=Ext.create('Ext.Button', {
        y:me.height-40,
        x:me.width/2+30,
        itemId:'MyRDS_btnClearAll',
        text : "清除",
        width : 80,
        maxWidth : 80,
        handler: function () {
            //alert("btnClearAllClick");
            me.btnClearAllClick();
            me.fireEvent('btnClearAllClick');
            }
    });
    return btnClearAll;
    },
     //创建关闭按钮
    createbtnColse:function(y){
    var me=this;
    var btnColse=Ext.create('Ext.Button', {
        y:me.height-40,
        x:me.width/2+140,
        itemId:'MyRDS_btnColse',
        text : "关闭",
        width : 80,
        maxWidth : 80,
        handler: function () {
           //alert("btnColseClick");
           //me.btnColseClick();
            me.fireEvent('btnColseClick');
            }
    });
    return btnColse;
    },
    //确定按钮事件处理
    btnOKClick:function(){
    var me=this;
    var myform = me.getComponent('my_FormView');
    var myitem=myform.items.items
    var length=myform.items.length-3;//表单总控件数量需要将确定按钮,清除按钮,关闭控件排除
    var value=null;
    me.lastValues=[];
    for(var i=0;i<length;i++){
      var ob =myform.items.items[i];
      value=me.GetComponentValue(ob);
       if(value==""||value==undefined||value=="undefined"){
            //return null;
           }else{
            if(i==length){
            value=value;
            }else{
            value=value+" and ";
            }
            me.lastValues.push(value);
           }
      //alert(value);
            }
    alert(Ext.encode(me.lastValues));
    //查询条件的字段名称取控件的name值
    //var value=GetComponentValue(ob);
    
    },
    //清除按钮事件处理
    btnClearAllClick:function(){
        
    },
     /**
     *返回结果值
     * @public
     */
    GetLastValue:function(){
     return this.lastValues;
    },
      /**
     * 【返回结果值】
     * @param {} 
     * @return {}返回值为
     */
    GetValue:function(){
     return this.lastValues;
    },
     //获取组件的选中结果值
     GetComponentValue:function(ob){
            var value;
            var Type= ob.xtype;
            var str="\"";
            if(Type === "combobox"){//下拉框
                value=ob.getValue();
            }else if(Type === "textfield"){//文本框
                value=ob.getValue();
            }else if(Type === "textareafield"){//文本域
               value=ob.getValue();
            }else if(Type === "numberfield"){//数字框
                value=ob.getValue();
            }else if(Type === "datetimefield"){//日期框
                value=ob.GetValue();
            }
            else if(Type === "checkboxfield"){//复选框
                value=ob.getValue();
            }else if(Type === "radiofield"){//单选框
                value=ob.getValue();
            }else{
               return null;
            }
            
           if(value==""||value==undefined||value=="undefined"){
            return null;
           }else{
            value=ob.name+"="+str+value+str;
           }
           alert(value);
            return value;
        },
    afterRender: function() {
        var me = this;
        me.callParent(arguments); 
    }

});