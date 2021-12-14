//非构建类--通用型组件或控件--行数据条件--未完成
//(1.部门类别角色操作权限--实现角色条件及数据项条件)
//(2.人员类别角色操作权限--实现角色条件及数据项条件)
//(3.岗位类别角色操作权限--实现角色条件及数据项条件)
//(4.职位类别角色操作权限--实现角色条件及数据项条件)

Ext.Loader.setConfig({enabled: true});
Ext.Loader.setPath('Ext.zhifangux', getRootPath()+'/ui/zhifangux');

Ext.ns('Ext.zhifangux');
Ext.define('Ext.zhifangux.RowsDataSet', {
    extend: 'Ext.panel.Panel',
    alias: 'widget.rowsdataset',
     
    requires: ['Ext.zhifangux.PeoplePicker','Ext.zhifangux.DepSelector','Ext.zhifangux.UXRadioGroup'],
     
    border:false,
    width:800,
    height:510,
    //autoHeight : true,
    frame : true,
    layout : "form", // 整个大的表单是form布局
    labelWidth : 40,
    labelAlign : "left",
    padding:0,
    
    CategoryValue:'',//角色类别临时变量
    RoleRelationValue:'',//角色关系临时变量
    FieldValue:'',//字段临时变量
    FieldRelationValue:'',//字段关系临时变量
    win:null,//创建和弹出选择器窗体
    winHeight:600,        //弹出选择器窗体高度像素
    winWidth:600,       //弹出选择器窗体宽度像素
    winTitle:'',        //弹出选择器窗体标题
    
    //-------------------外部传值--------------------
    //一.部门人员选择器
    UsersValueField:[],//部门人员选择器--人员部门数据列表值字段,需要外部传入数据适配
    UsersDeptValueField:[],//部门下拉绑定树选择器--数据列表值字段,需要外部传入数据适配
    UsersDataServerUrl: '', //部门人员选择器--人员后台查询服务地址
    UsersDeptDataServerUrl:'', //部门下拉绑定树选择器--部门后台服务查询地址
    
    UsersDeptId:'DeptpId',      //匹配部门人员选择器的人员部门Id
    UsersDeptName:'DeptpName', //匹配部门人员选择器的人员部门名称 
    UsersId:'',      //匹配部门人员选择器的返回结果里的人员Id值字段
    UsersName:'',   //匹配部门人员选择器的返回结果里的人员名称值字段
    Usersheight:520,        //部门人员选择器容器高度像素
    Userswidth:680,       //部门人员选择器容器宽度像素
        
    //二.部门选择器
    DeptId:'',      //匹配部门选择器的的返回结果部门Id
    DeptName:'',   //匹配部门选择器的的返回结果部门名称 
    DeptDataServerUrl:'',//匹配部门选择器的数据源
    deptHeight:360,        //部门选择器容器高度像素
    deptWidth:330,       //部门选择器容器宽度像素
    //三.岗位选择器
    postHeight:500,        //岗位选择器容器高度像素
    postWidth:430,       //岗位选择器容器宽度像素
    postserverUrl: '',  //岗位选择器后台服务地址
    radiogroupName:'rb',//单选组的组名称
    postId:'inputValue',      //匹配岗位选择器的的返回结果Id
    postName:'boxLabel',   //匹配岗位选择器的的返回结果名称 
    
    //四.职位选择器
    positionHeight:500,        //职位选择器容器高度像素
    positionWidth:430,       //职位选择器容器宽度像素
    postserverUrl: '',
    positionId:'inputValue',      //匹配职位选择器的的返回结果Id
    positionName:'boxLabel',   //匹配职位选择器的的返回结果名称 
    
    //五.
    FieldServerUrl:'',//字段数据后台服务地址
    myField:[],//字段下拉列表数据匹配 fields:['text','Description']
    fieldsRoot:'',//数据表字段后台数据的根
    SaveServerUrl:'',//往后台保存数据的服务地址
    lastValues:{},//最终往后台提交的数据
   //-------------------外部传值--------------------
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
   
    /**
     * 字段增加单击事件
     */
    btnAddFieldCilck:function(){
    var me=this;
    var conditionName=Ext.getCmp("MyRDS_txtConditionName").getValue();//条件名称
    if(conditionName==""||conditionName.lenght===0){
    Ext.Msg.alert("条件名称或者角色名称不能为空!");
    return;
    }
    
    var FieldKey=Ext.getCmp("MyRDS_Fieldtemp").getValue();//字段下拉key
    var FieldName=Ext.getCmp("MyRDS_Fieldtemp").getRawValue();//值字段下拉Name
    
    if(FieldKey==""||FieldKey==null){
    Ext.Msg.alert("字段名称不能为空!");
    return;
    }
    
    var ValueTypeKey=Ext.getCmp("MyRDS_ValueTypetemp").getValue();//值类型key
    var ValueTypeName=Ext.getCmp("MyRDS_ValueTypetemp").getRawValue();//值类型Name
    
    var TypeValues="";
    
    //值类型为常量时的取值
    if(ValueTypeKey=="constant"){
    TypeValues=Ext.getCmp("MyRDS_FieldValueTemp").getValue();//数值
    }
    //值类型为宏条件时的取值
   if(ValueTypeKey=="grand"){
    TypeValues=Ext.getCmp("MyRDS_grandtemp").getRawValue();//数值取宏条件的名称Name
    }
    
    if(ValueTypeKey=="constant"&&(TypeValues==""||TypeValues==null)){
      Ext.Msg.alert("数值不能为空!"); 
      return;
    }

    
    var FRelationKey=Ext.getCmp("MyRDS_FieldRelationtemp").getValue();//字段关系
    var FRelationName=Ext.getCmp("MyRDS_FieldRelationtemp").getRawValue();//字段关系下拉Name
    
    var rightGrid=Ext.getCmp("MyRightGrid_itemId");//me.getComponent("MyLeftGriditemId");
    var store=rightGrid.getStore();
    store.add({'FieldKey':FieldKey,'FieldName':FieldName,'ValueTypeKey':ValueTypeKey,'ValueTypeName':ValueTypeName,'TypeValues':TypeValues,'FRelationKey':FRelationKey,'FRelationName':FRelationName});
    
    },
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
     /**
     * 角色操作权限增加单击事件
     */
    btnAddRoleCilck:function(){
    var me=this;
    
    var conditionName=Ext.getCmp("MyRDS_txtConditionName").getValue();
    
    var roleName=Ext.getCmp("MyRDS_txtRoleName").getValue();//角色名称
    var roleId=Ext.getCmp("MyRDS_txtRoleId").getValue();//角色id
    
    var CategoryName=Ext.getCmp("MyRDS_Category").getRawValue();//类别选择器value
    var CategoryKey=Ext.getCmp("MyRDS_Category").getValue();//类别选择器key
    
    var RelationId=Ext.getCmp("MyRDS_RelationId").getValue();//关系id
    var RelationName=Ext.getCmp("MyRDS_RelationId").getRawValue();//关系名称
    
    //alert(CategoryName);
    if(conditionName==""||conditionName.lenght===0){
    Ext.Msg.alert("条件名称或者角色名称不能为空!");
    return;
    }
    
    var leftGrid=Ext.getCmp("MyLeftGridIdtemp");
    var store=leftGrid.getStore();
    store.add({'CategoryKey':CategoryKey,'CategoryName':CategoryName,'RelationId':RelationId,'RelationName':RelationName,'roleId':roleId,'roleName':roleName});

    },    
    initComponent:function(){
    var me = this;
    me.items= [
        {// 行1
        labelAlign : "left",
        border:false,
        items : [{
            xtype : "textfield",
            labelWidth :50,
            fieldLabel :"名称 ",
            id:'MyRDS_txtConditionName',
            width:480,
            maxWidth : 520
        }
        ]
    },
       {// 行2
        labelAlign : "left",
        border:false,
        items : [
        {
            style:{fontSize:'20px',fontWeight:'bold'},
            height:30,
            html : "角色操作权限："
        }
        ]
    },
    { // 行3
    layout : "column", // 从左往右的布局
    items : [{
    border:false,
    columnWidth : .2, // 该列有整行中所占百分比
    layout : "form", // 从上往下的布局
    items : [
        {//类别下拉选择器
    fieldLabel: '类别 ',
    labelWidth :40,
    id:'MyRDS_Category',
    store: me.getMyCategoryStore(),
    queryMode: 'local',
    displayField: "CategoryName",
    maxWidth : 120,
    value:'staff',//默认值department
    valueField: "CategoryId",
    xtype:'combo',
    listeners:{
    change:function( newValue,oldValue, eOpts )
    {
     me.CategoryValue=newValue.value;
     var rawValue=newValue.rawValue;//取名称
     Ext.getCmp("MyRDS_txtRoleName").setValue("");
     Ext.getCmp("MyRDS_txtRoleId").setValue("");
     me.SetWinHeight();
     me.SetWinWidth();
    }
    }
     }]
       }, {
            columnWidth : .2,
            layout : "form",
            border:false,
            items : [
                {
    //关系下拉选择器
    fieldLabel: '关系 ',
    labelWidth :40,
    id:'MyRDS_RelationId',
    store: me.getMyRelationStore(),
    queryMode: 'local',
    displayField: "RelationName",
    maxWidth : 120,
    valueField: "RelationId",
    value:'=',//默认值
    xtype:'combo',
    listeners:{
    change:function( newValue,oldValue, eOpts )
    {
     me.RelationValue=newValue.value;
    }
    }
        }]
        }, {
            columnWidth : .2,
            layout : "form",
            border:false,
            items : [
                {
                border:false,
                readOnly:true,
                xtype : "textfield",
                id:'MyRDS_txtRoleName',
                fieldLabel : "名称",
                labelWidth :40,
                width : 120,
                maxWidth : 120,
                listeners:{
                focus:function(The, eOpts )
                {
                 var roleName=Ext.getCmp("MyRDS_Category").getValue();
                //创建和弹出选择器
                 me.OpenCategoryWin(roleName);
                 }
                }
            }]
        }, {
            columnWidth : .2,
            border:false,
            layout : "form",
            items : [{
                xtype : "textfield",
                readOnly:true,
                id:'MyRDS_txtRoleId',
                labelWidth :40,
                fieldLabel : "ID",
                width : 160
            }]
        }, {
            columnWidth : .2,
            border:false,
            layout : "form",
            width : 120,
            items : [{
                xtype : "button",
                itemId:'MyRDS_btnRoleAdd',
                text : "增加",
                width : 60,
                maxWidth : 60,
                 listeners : { 
                 click:{ 
                 element:'el', fn:function(){
                 me.btnAddRoleCilck();
                 me.fireEvent('btnAddRoleClick');
                 }
                 }
                 }
            }]
        }
        ]
    },  
    {// 行4
        labelAlign : "left",
        border:false,
        items : [
        {
          style:{fontSize:'20px',fontWeight:'bold'},
          height:30,
          html : "表字段操作权限: "
        }
        ]
    },
    {//行5
    layout : "column", // 从左往右的布局
    items : [{
    columnWidth : .2, // 该列有整行中所占百分比
    layout : "form", // 从上往下的布局
    border:false,
    items : [
        {//字段下拉选择器
    fieldLabel: '字段 ',
    labelWidth :40,
    id:'MyRDS_Fieldtemp',
    store: me.getMyFieldStore(),
    queryMode: 'local',
    displayField: me.myField[1],
    maxWidth : 120,
    valueField:me.myField[0],
    xtype:'combo',
    listeners:{
    change:function( newValue,oldValue, eOpts )
    {
     me.CategoryValue=newValue.value;
    }
    }
     }]
        }, {
            columnWidth : .2,
            layout : "form",
            border:false,
            items : [
          {
    //关系下拉选择器
    fieldLabel: '关系 ',
    labelWidth :40,
    id:'MyRDS_FieldRelationtemp',
    store: me.getMyRelationStore(),
    queryMode: 'local',
    displayField: "RelationName",
    maxWidth : 120,
    valueField: "RelationId",
    value:'=',//默认值
    xtype:'combo',
    listeners:{
    change:function( newValue,oldValue, eOpts )
    {
     me.RelationValue=newValue.value;
    }
    }
      }]
        }, 
         {
            columnWidth : .2,
            layout : "form",
            border:false,
            items : [{
    //值类型下拉选择器
    fieldLabel: '值类型 ',
    labelWidth :50,
    id:'MyRDS_ValueTypetemp',
    store: me.getMyValueTypeStore(),
    queryMode: 'local',
    displayField: "ValueTypeName",
    maxWidth : 120,
    valueField: "ValueTypeId",
    value:'constant',//默认值
    xtype:'combo',
    listeners:{
    change:function( newValue,oldValue, eOpts )
    {
     me.ValueType=newValue.value;
     
     Ext.getCmp("MyRDS_FieldValueTemp").setValue("");
     //Ext.getCmp("MyRDS_txtRoleId").setValue("");
     
     Ext.getCmp("MyRDS_FieldValueTemp").hide();
     //如果值类型为宏条件时
     if(newValue.value=="constant"){
       Ext.getCmp("MyRDS_grandtemp").hide();
       Ext.getCmp("MyRDS_FieldValueTemp").show();
     }else if(newValue.value=="grand"){
      Ext.getCmp("MyRDS_grandtemp").show();
      Ext.getCmp("MyRDS_FieldValueTemp").hide();
     }else{
      Ext.getCmp("MyRDS_grandtemp").hide();
      Ext.getCmp("MyRDS_FieldValueTemp").show();
     }
    }
    }
     }]
        }, 
         {
            columnWidth : .2,
            layout : "form",
            border:false,
            items : [
                {
                xtype : "textfield",
                labelWidth :40,
                id:'MyRDS_FieldValueTemp',
                fieldLabel : "数值",
                width : 160
            },
            {//宏数值下拉选择器
				fieldLabel: '数值 ',
				labelWidth :40,
				id:'MyRDS_grandtemp',
				store: me.getMyGrandStore(),
				queryMode: 'local',
                hidden:true,
				displayField: "GrandName",
				maxWidth : 110,
				valueField: "GrandId",
				value:'EmployeeID',//默认值
				xtype:'combo',
				listeners:{
				change:function( newValue,oldValue, eOpts )
				{
				 me.RelationValue=newValue.value;
				}
				}
				 }
            ]
        }, 
            {
            columnWidth : .2,
            layout : "form",
            border:false,
            width : 120,
            items : [{
	            //border:false,
	            xtype : "button",
                itemId:'MyRDS_btnAddfield',
	            text : "增加",
	            width : 60,
                maxWidth : 60,
	            handler: function () {
                    me.btnAddFieldCilck();
	                me.fireEvent('btnAddFieldClick');
	                }
            }]
        }
        ]
    },
    {// 行6
        layout : "form",
        items : [{
            xtype : "button",
            text : "提交并返回",
            width : 80,
            maxWidth : 80,
            listeners:{
                click:function()
                {
                    
                 me.btnOKSave();
                 me.fireEvent('btnOKClick');
                }
                }
        }]
    },
    {   // 行7
        layout : "column",
        border:false,
        padding:2,
        items : [{
            columnWidth : .48,
            border:false,
            padding:2,
            layout : "form",
            items : [{
                xtype :me.createLeftGrid(),
                width : me.width/2
            }]
        }, {
            columnWidth : .48,
            layout : "form",
            padding:2,
            border:false,
            items : [{
                xtype :me.createRightGrid()
                //width : me.width/2
            }]
        }]
    } 
    ];

		//添加事件，别的地方就能对这个事件进行监听
		this.addEvents('btnOKClick');
		this.addEvents('btnAddFieldClick');
		this.addEvents('btnAddRoleClick');
        
    	this.callParent(arguments);
       //赋初始值
       me.anitialValue();
            
    },
    //提交事件
    btnOKSave:function(){
    //alert("btnOKSave");
    var me=this;
    var conditionName=Ext.getCmp("MyRDS_txtConditionName").getValue();//条件名称
    if(conditionName==""||conditionName.lenght===0){
    Ext.Msg.alert("条件名称或者角色名称不能为空!");
    return;
    }
    
    var leftGrid=Ext.getCmp("MyLeftGridIdtemp");//me.getComponent("MyLeftGriditemId");
    var leftStore=leftGrid.getStore();
    
    var rightGrid=Ext.getCmp("MyRightGrid_itemId");//me.getComponent("MyLeftGriditemId");
    var rightStore=rightGrid.getStore();
    
    var leftValues=[];
    leftStore.each(function(model){
    leftValues.push(model.data);
        });
    var rightValues=[];
    rightStore.each(function(model){
    rightValues.push(model.data);
        });
     me.lastValues={};//最终往后台提交的数据
     
     me.lastValues.conditionName=conditionName;
     me.lastValues.roleList=leftValues;
     me.lastValues.fieldList=rightValues;
     alert(Ext.encode(me.lastValues));
     //return;
        Ext.Ajax.request({
            async:false,//非异步
            url:me.SaveServerUrl,
            params: {
                 datas: me.lastValues
            },
            method:'POST',
            //timeout:2000,
            success:function(response,opts){
            var result = Ext.JSON.decode(response.responseText);
            if(result.success){
            //重新加载数据源
           // myGridStore=me.getMyGridStore();
            //重新加载列表数据
            }else{
                Ext.Msg.alert('提示','保存信息失败！');
                }
            },
            failure : function(response,options){ 
                Ext.Msg.alert('提示','保存信息请求失败！');
            }
        });
    },
    //赋初始值
    anitialValue:function(){
        var me=this;
       Ext.getCmp("MyRDS_grandtemp").hide();
       Ext.getCmp("MyRDS_FieldValueTemp").show();
     if (me.CategoryValue === undefined && me.CategoryValue==null) {
         me.CategoryValue=Ext.getCmp("MyRDS_Category").getValue();//角色类别临时变量
        }
        if (me.RoleRelationValue === undefined && me.RoleRelationValue==null) {
         me.RoleRelationValue=Ext.getCmp("MyRDS_RoleRelation").getValue();//角色关系临时变量
         }
        if (me.FieldValue === undefined && me.FieldValue==null) {
         me.FieldValue=Ext.getCmp("MyRDS_Field").getValue();//字段临时变量
          }
        if (me.FieldRelationValue === undefined && me.FieldRelationValue==null) {
         me.FieldRelationValue=Ext.getCmp("MyRDS_FieldRelation").getValue();//字段关系临时变量
           }
        if (me.ValueType === undefined && me.ValueType==null) {
         me.ValueType=Ext.getCmp("MyRDS_ValueTypetemp").getValue();//值类型临时变量
          }
        
    },
    afterRender: function() {
        var me = this;
        me.callParent(arguments); 
    },
    
     /**
     * 创建字段列表对象数据源
     * 实现数据项适配的功能
     * 
     */
   getMyFieldStore:function(){
        var me = this;
        var MyFieldStore=null;
        Ext.Ajax.request({
            async:false,//非异步
            url:me.FieldServerUrl,
            //部门参数
            params: {
                 //Deptparams: me.DeptId
            },
            method:'POST',
            timeout:2000,
            success:function(response,opts){
            var result = Ext.JSON.decode(response.responseText);
            if(result.success){
        
            MyFieldStore=Ext.create('Ext.data.Store', {  
            fields:me.myField,//实现数据项适配的功能,需要调用时传入
            data:result,
            proxy: {
            type: 'memory',
            reader: {
            type: 'json',
            root: me.fieldsRoot//fieldsRoot='fields'
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
    return MyFieldStore;
   },
    
   //类别
    getMyCategoryStore:function(){
    var MyCategoryStore = Ext.create('Ext.data.Store', {
    fields: ['CategoryId', 'CategoryName'],
    data : [
        {"CategoryId":"department", "CategoryName":"部门"},
        {"CategoryId":"staff", "CategoryName":"人员"},
        {"CategoryId":"post", "CategoryName":"岗位"},
        {"CategoryId":"position", "CategoryName":"职位"}
    ]
    });
    return MyCategoryStore;
    },
    //关系
    getMyRelationStore:function(){
    var MyRelationStore = Ext.create('Ext.data.Store', {
    fields: ['RelationId', 'RelationName'],
    data : [
        {"RelationId":"=", "RelationName":"等于"},
        {"RelationId":"!=", "RelationName":"不等于"},
        {"RelationId":"＞", "RelationName":"大于"},
        {"RelationId":"≥", "RelationName":"大于等于"},
        {"RelationId":"＜", "RelationName":"小于"},
        {"RelationId":"≤", "RelationName":"小于等于"},
        //需要修改
        {"RelationId":"include", "RelationName":"包含"},
        {"RelationId":"noinclude", "RelationName":"不包含"},
        {"RelationId":"includein", "RelationName":"包含于"},
        {"RelationId":"not in", "RelationName":"不包含于"},
        {"RelationId":"begin", "RelationName":"开始于"},
        {"RelationId":"end", "RelationName":"结束于"}
    ]
    });
    return MyRelationStore;
    },
     //值类型
    getMyValueTypeStore:function(){
    var MyValueTypeStore = Ext.create('Ext.data.Store', {
    fields: ['ValueTypeId', 'ValueTypeName'],
    data : [
        {"ValueTypeId":"constant", "ValueTypeName":"常量"},
        {"ValueTypeId":"grand", "ValueTypeName":"宏"}
    ]
    });
    return MyValueTypeStore;
    },
    //宏条件
    getMyGrandStore:function(){
    var MyGrandStore = Ext.create('Ext.data.Store', {
    fields: ['GrandId', 'GrandName'],
    data : [
        {"GrandId":"EmployeeID", "GrandName":"登录者编号"},
        {"GrandId":"EmployeeName", "GrandName":"登录者姓名"},
        {"GrandId":"DepartmentID", "GrandName":"登录者部门编号"},
        {"GrandId":"DepartmentName", "GrandName":"登录者部门名称"},
        {"GrandId":"PostID", "GrandName":"登录者岗位编号"},
        {"GrandId":"PostName", "GrandName":"登录者岗位名称"},
        {"GrandId":"DutyID", "GrandName":"登录者职位编号"},
        {"GrandId":"DutyName", "GrandName":"登录者职位名称"},
        {"GrandId":"CurrentTime", "GrandName":"当前日期时间"}
    ]
    });
    return MyGrandStore;
    },
   /**
  * 创建左gridPanel角色操作权限列表
  * @return {}
  */
   createLeftGrid:function(){
    var me=this;
   var myGrid=Ext.create('Ext.grid.Panel', {
    store:me.getMyLeftGridStore(),
    id:"MyLeftGridIdtemp",
    width:me.width/2,
    height:280,
    columns:me.LeftGridField
    });
    return myGrid;
   },

  /**
  * 创建右gridPanel表字段操作权限列表
  * @return {}
  */
   createRightGrid:function(){
    var me=this;
   var myGrid=Ext.create('Ext.grid.Panel', {
    id:"MyRightGrid_itemId",
    store:me.getMyRightGridStore(),
    width:me.width/2,
    height:280,
    columns:me.RightGridField
    });
    return myGrid;
   },
   /**
     * 打开并操作角色类别窗体
     * @param {} roleName
     */
   OpenCategoryWin:function(roleName){
    var me=this;
    var xy=me.getPosition();
    var myxtype=null;
    //Ext.Msg.alert(roleName);
    switch(roleName)
    {
	    case "department"://部门
	    if(!myxtype){
        myxtype=Ext.create('widget.depselector', {
        SelectType:0,
        Width:me.deptWidth,
        Height:me.deptHeight,
        DataServerUrl:me.DeptDataServerUrl, 
        listeners:{ 
            onCancelCilck:function(){
            if(me.win){
                    me.win.close();
                   }
              },
             OnSave:function(){
                var arr=this.getAllValue();//Ext.encode(this.lastValues);
                Ext.each(arr,function(item,index,allItems){ 
                Ext.getCmp("MyRDS_txtRoleId").setValue(item[me.DeptId]);
                Ext.getCmp("MyRDS_txtRoleName").setValue(item[me.DeptName]);
                   })
                if(me.win){
                    me.win.close();
                   }
              }  
            }
        });
        }
	    break;
	    case "staff"://部门人员选择器
		if(!myxtype){
        myxtype=Ext.create('widget.peoplepicker', {
        title: '',
        itemId:'PeoplePicker_temp',
        titleAlign :"center",
        border : false,//边框线显示 true,或隐藏false
        autoScroll : true,
        height:me.Usersheight,        //部门人员选择器容器高度像素
        width:me.Userswidth,       //部门人员选择器容器宽度像素
        multiSelect:false,//单选或多选开关; false:单选,true:多选
        simpleSelect:false,//单选或多选开关;false:单选,true:多选,必须要设置该值,单选或多选才生效
        //margin :  '11 10 10 332',
        DataServerUrl:me.UsersDataServerUrl, //人员后台查询服务地址
        DeptDataServerUrl:me.UsersDeptDataServerUrl, //部门后台服务地址
        DeptId:me.UsersDeptId,      //人员部门Id
        DeptpName:me.UsersDeptName,   //人员部门名称 
        
        ValueField:me.UsersValueField,//人员部门数据列表值字段,需要外部数据适配后传进来
        DeptValueField:me.UsersDeptValueField,//部门数据列表值字段,需要外部做好数据适配后传进来
        UserRoot:me.UserRoot,//部门人员获取/读取后台数据时的根节点
        DeptRoot:me.DeptRoot,//部门获取/读取后台数据时的根节点
        
        listeners:{ 
            onCancelCilck:function(){
               if(me.win){
                    me.win.close();
                   }
              },
             onOKCilck:function(){
                var arr=this.getAllValue();//Ext.encode(this.lastValues);
                Ext.each(arr,function(item,index,allItems){ 
                Ext.getCmp("MyRDS_txtRoleId").setValue(item[me.UsersId]);
                Ext.getCmp("MyRDS_txtRoleName").setValue(item[me.UsersName]);
                   })
                   if(me.win){
                    me.win.close();
                   }
              }  
            }
            });
              }
        break;
        case "post"://岗位
         if(!myxtype){
               myxtype = Ext.create('Ext.panel.Panel', {
                    border:false,
                    width:me.postWidth+50,
                    height:me.postHeight+50,
                    items: [{
                       xtype: 'uxradiogroup',
                       region: 'north',
                       serverUrl:me.postserverUrl,
                       radiogroupName:'post_rb',
                       Width:me.postWidth,
                       Height:me.postHeight,
                       id:'my_post'
                   },{
                       xtype: 'panel',
                       region: 'center',
                       height: 34,
                       title: '',
                       layout:'absolute',
                       border:false,
                       items:[
                           {xtype: 'button',text: '确定',x: 190,y: 5,
                            handler: function () {
                               var post=Ext.getCmp('my_post');
                               var arr=post.GetLastValue();//Ext.encode(this.lastValues);
                               Ext.each(arr,function(item,index,allItems){
                                   Ext.getCmp("MyRDS_txtRoleName").setValue(item['boxLabel']);
                                   Ext.getCmp("MyRDS_txtRoleId").setValue(item['inputValue']);
                               });
                                var bm = Ext.getCmp('MyRDS_wintemp');
                                bm.close();
                               } 
                           
                           },
                           { 
                           xtype: 'button',text: '取消',x: 240,y: 5,
                            handler: function () {
                                var bm = Ext.getCmp('MyRDS_wintemp');
                                bm.close();
                                me.fireEvent('OnPostCilck');
                               }   
                           }
                       ]
                   }
               ]  
            });
         
         }
        break;
        case "position"://职位
        if(!myxtype){
                myxtype = Ext.create('Ext.panel.Panel', {
                    border:false,
                    width:me.positionWidth+50,
                    height:me.positionHeight+50,
                    items: [{
                       xtype: 'uxradiogroup',
                       region: 'north',
                       serverUrl:me.positionserverUrl,
                       radiogroupName:'position_rb',
                       Width:me.positionWidth,
                       Height:me.positionHeight,
                       id:'my_position'
                   },{
                       xtype: 'panel',
                       region: 'center',
                       height: 34,
                       title: '',
                       layout:'absolute',
                       border:false,
                       items:[
                           {xtype: 'button',text: '确定',x: 190,y: 5,
                            handler: function () {
                               var position=Ext.getCmp('my_position');
                               var arr=position.GetLastValue();//Ext.encode(this.lastValues);
                               Ext.each(arr,function(item,index,allItems){
                                   Ext.getCmp("MyRDS_txtRoleName").setValue(item['boxLabel']);
                                   Ext.getCmp("MyRDS_txtRoleId").setValue(item['inputValue']);
                               });
                                var bm = Ext.getCmp('MyRDS_wintemp');
                                bm.close();
                               } 
                           
                           },
                           { 
                           xtype: 'button',text: '取消',x: 240,y: 5,
                            handler: function () {
                                var bm = Ext.getCmp('MyRDS_wintemp');
                                bm.close();
                                me.fireEvent('OnPositionCilck');
                               }   
                           }
                       ]
                   }
               ]  
            });
        }
        
		break;
		default:
  
        }
         me.win=null;
         me.win = Ext.create('widget.window', {
                title:me.winTitle,
                id:"MyRDS_wintemp",
                autoScroll : true,
                border : false,//边框线显示 true,或隐藏false
                width: me.winWidth,
                height:me.winHeight,// me.SetWinWidth(),
                minWidth: me.winWidth,
                minHeight: me.winHeight,
                maxWidth: me.winWidth+5,
                maxHeight: me.winHeight+10,
                x:xy[0]+38,y:xy[1]+10,
                layout: {
                    type: 'border',
                    padding: 5
                },
                items: [{
                xtype:myxtype
                }]
            });
            me.win.show();
        },
   //角色操作权限列表值字段
  LeftGridField:[
        { text: '类别ID',dataIndex: 'CategoryKey',width:20, hidden: true},
        { text: '类别',dataIndex: 'CategoryName',width:130},
        { text: '关系ID',dataIndex: 'RelationId',width:20, hidden: true},
        { text: '关系',dataIndex: 'RelationName',width:130},
        { text: '角色ID',dataIndex: 'roleId',width:20, hidden: true},
        { text: '角色',dataIndex: 'roleName',width:130}
        ],
   //表字段操作权限列表值字段
  RightGridField:[
        { text: '字段ID',dataIndex: 'FieldKey',width:20, hidden: true},
        { text: '字段',dataIndex: 'FieldName',width:100},
        { text: '关系ID',dataIndex: 'FRelationKey',width:20, hidden: true},
        { text: '字段',dataIndex: 'FRelationName',width:100},
        { text: '值类型ID',dataIndex: 'ValueTypeKey',width:20, hidden: true},
        { text: '值类型',dataIndex: 'ValueTypeName',width:100},
        { text: '数值',dataIndex: 'TypeValues',width:100}
        ],
     
  leftFields:[{name:'CategoryKey'}, {name:'CategoryName'},{name: 'RelationId'},{name: 'RelationName'},{name: 'roleId'},{name: 'roleName'}],
  rightFields:[{name:'FieldKey'},{name:'FieldName'},{name:'ValueTypeKey'},{name:'ValueTypeName'},{name:'TypeValues'},{name:'FRelationKey'},{name:'FRelationName'}],
  getMyLeftGridStore:function(){
    var me=this;
   var store=new Ext.data.ArrayStore(
   {
   fields:me.leftFields
   });
   return store;
    },
   getMyRightGridStore:function(){
   var me=this;
   var store=new Ext.data.ArrayStore(
   {
   fields:me.rightFields
   });
   return store;
    },
     /**
     * 设置弹出窗口的宽度
     */
    SetWinWidth:function(){
    var me=this;
    var CategoryName=Ext.getCmp("MyRDS_Category").getValue();//类别选择器
      switch(CategoryName)
    {
        case "department"://部门
        me.winWidth=me.deptWidth+30;
        //alert(me.winWidth);
        break;
        case "staff"://人员
        me.winWidth=me.Userswidth+10;
        break;
        case "post"://岗位
        me.winWidth=me.postWidth+30;
        break;
        case "position"://职位
        me.winWidth=me.positionWidth+30;
        break;
        
        default:
       
    }
    },
    /**
     * 设置弹出窗口的高度
     */
    SetWinHeight:function(){
       var me=this;
    var CategoryName=Ext.getCmp("MyRDS_Category").getValue();//类别选择器
      switch(CategoryName)
    {
        case "department"://部门
        me.winHeight=me.deptHeight+60;
        break;
        case "staff"://人员
        me.winHeight=me.Usersheight+40;
        break;
        case "post"://岗位
        me.winHeight=me.postHeight+80;
        break;
        case "position"://职位
        me.winHeight=me.positionHeight+80;
        break;
        
        default:
    }
    }

});