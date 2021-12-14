/***
 * 部门管理--定制添加部门员工查询条件
 */
Ext.ns('Ext.zhifangux');

Ext.define('Ext.zhifangux.addDeptStaff', {
    extend:'Ext.form.Panel',
    alias:'widget.adddeptstaff',
    title:'添加部门员工查询条件',
    width:526,
    height:92,
    autoScroll:true,
    layout:'absolute',
    //自定义变量
    /***
     * 外部传入的gridpanel组件对象
     * @type String
     */
    externalGridCom:'',
    /***
     * 返回最终生成的where串
     * @return {}
     */
    getValue:function() {
        var me = this;
        var lastValue = '';
        //未指定部门员工
        var comIsNull=me.getcbogHRDeptIdIsNullValue();
        var comHRDeptId=''+me.getHREmployeeHRDeptIdValue();
        if(comIsNull==true){
            lastValue=' hremployee.HRDept.Id is null';
        }else if(comIsNull==false&&comHRDeptId!='null'&&comHRDeptId.length>0){
            lastValue=' hremployee.HRDept.Id ='+comHRDeptId;
            
        }else if(comIsNull==false&&(comHRDeptId==null||comHRDeptId=='')){
            Ext.Msg.alert('提示','请至少选择一个查询条件');
            lastValue = '';
        }
        return lastValue;
    },
    /***
     * 与外部构建列表的模糊匹配文本
     * @return {}
     */
    gettxtgridSearch:function(){
        var me=this;
        var com=me.getComponent('txtgridSearch');
        return com;
    },
    /***
     * 未指定部门员工
     * @return {}
     */
    getcbogHRDeptIdIsNull:function(){
        var me=this;
        var com=me.getComponent('cbogHRDeptIdIsNull');
        return com;
    },
    /***
     * 未指定部门员工
     * @return {}
     */
    getcbogHRDeptIdIsNullValue:function(){
        var me=this;
        var com=me.getComponent('cbogHRDeptIdIsNull');
        var item=com.items.items[0];
        var value=item['checked'];
        if(value=='1'||value=='true'||value==true){
            value=true;
        }else{
            value=false;
        }
        return value;
    },
    /***
     * 员工部门id
     * @return {}
     */
    getHREmployeeHRDeptId:function(){
        var me=this;
        var com=me.getComponent('HREmployee_HRDept_Id');
        return com;
    },
    /***
     * 员工部门id
     * @return {}
     */
    getHREmployeeHRDeptIdValue:function(){
        var me=this;
        var com=me.getComponent('HREmployee_HRDept_Id');
        var value=com.getValue();
        return value;
    },
    /***
     * 查询按钮
     * @return {}
     */
    getbuttonSearch:function(){
        var me=this;
        var com=me.getComponent('buttonSearch');
        return com;
    },
    initComponent:function() {
        var me = this;
        me.addEvents('selectClick');
        me.listeners=me.listeners||[];
        me.changeStoreData = function(response) {
            var data = Ext.JSON.decode(response.responseText);
            var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
            data.ResultDataValue = ResultDataValue;
            data.list = ResultDataValue.list;
            response.responseText = Ext.JSON.encode(data);
            return response;
        };
        me.items = [ {
            xtype:'combobox',
            isOperation:false,
            name:'HREmployee_HRDept_Id',
            fieldLabel:'部门选择',
            emptyText :'请选择部门',
            labelWidth:55,
            width:180,
            height:22,
            itemId:'HREmployee_HRDept_Id',
            x:156,
            y:5,
            readOnly:false,
            hidden:false,
            mode:'local',
            allowBlank:true,
            displayField:'HRDept_CName',
            valueField:'HRDept_Id',
            store:new Ext.data.Store({
                fields:[ 'HRDept_CName', 'HRDept_Id'],
                proxy:{
                    type:'ajax',
                    async:false,
                    url:getRootPath() + '/' + 'RBACService.svc/RBAC_UDTO_SearchHRDeptByHQL?isPlanish=true&where=&',
                    reader:{
                        type:'json',
                        root:'list'
                    },
                    extractResponseData:me.changeStoreData
                },
                autoLoad:true
            })
        }, {
            xtype:'textfield',
            isOperation:false,
            name:'txtgridSearch',
            fieldLabel:'',
            emptyText :'模糊匹配部门员工列表',
            labelWidth:0,
            width:300,
            height:22,
            itemId:'txtgridSearch',
            x:35,
            y:32,
            readOnly:false,
            hidden:false
        }, {
            xtype:'checkboxgroup',
            name:'cbogHRDeptIdIsNull',
            isOperation:false,
            fieldLabel:'',
            labelWidth:0,
            width:115,
            height:22,
            columnWidth:100,
            columns:1,
            itemId:'cbogHRDeptIdIsNull',
            x:29,
            y:5,
            readOnly:false,
            vertical:true,
            padding:2,
            autoScroll:true,
            hidden:false,
            isdataValue:true,
            items:[ {
                checked:false,
                name:'cbogHRDeptIdIsNull',
                inputValue:'hremployee.HRDept.Id is null',
                boxLabel:'未指定部门员工'
            } ],
            tempGroupName:'cbogHRDeptIdIsNull'
        }, {
            xtype:'button',
            isOperation:false,
            name:'buttonSearch',
            width:80,
            height:22,
            text:'查询',
            btnType:'select',
            itemId:'buttonSearch',
            x:412,
            y:5,
            iconCls:'build-button-select',
            listeners:{
                click:function() {
                    me.fireEvent('selectClick');
                }
            }
        } ];
        me.callParent(arguments);
    },
    afterRender:function() {
        var me=this;
        me.callParent(arguments);
        if (Ext.typeOf(me.callback) == 'function') {
            me.callback(me);
        }
    },
    /***
     * 模糊查询过滤匹配gridpanel行记录的函数
     */
    filterFn:function(value){
        var me=this,valtemp=value;
        var grid=me.externalGridCom;
        if(grid==null||grid==''){
            return;
        }
        var strSplit=' ';//字符串分割符
        var store=grid.getStore();
        if(!valtemp){
           store.clearFilter();
           return;
        }
        //字符串不为数字
        if(isNaN(parseInt(valtemp,10))){
        //字符串里的字母统一转换为小写格式,以匹配支持gridpanel的store里所有大小写字母字符串
           valtemp=String(value.toUpperCase()).trim().split(strSplit);
        }else{
           valtemp=String(value).trim();
           valtemp=String(valtemp.toUpperCase()).split(strSplit);
        }

         //调用filterBy匹配
        store.filterBy(function(record, id) {
            var data=record.data;
            for(var p in data){
                var porp=String(data[p]);
                for(var i=0;i<valtemp.length;i++){
                    var macther=valtemp[i];
                    var macther2='^'+Ext.escapeRe(macther);
                    mathcer=new RegExp(macther2);
                    if(mathcer.test(porp)){
                        return true;
                    }
                }
           }
           return false;
       });
       
   }
});