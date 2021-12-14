/** 修改密码
**/
Ext.ns('Ext.zhifangux');
Ext.define('Ext.zhifangux.CipherForm', {
    extend:'Ext.form.Panel',
    alias:'widget.cipherform',
    title:'修改密码',
    width:320,
    height:200,
    header:true,
    objectName:'RBACUser',
    isSuccessMsg:true,
    getAppInfoServerUrl:getRootPath() + '/' + 'ServerWCF/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById',
    addDataServerUrl:'ServerWCF/RBACService.svc/RBAC_UDTO_AddRBACUser',
    editDataServerUrl:'ServerWCF/RBACService.svc/RBAC_UDTO_UpdateRBACUserByField',
    //获取用户帐号
    getAccountUrl:getRootPath() + '/' + 'ServerWCF/RBACService.svc/RBAC_UDTO_GetHREmployeeBySessionHREmpID',
    //验证输入旧密码服务
    CheckUserPWDUrl:getRootPath()+'/'+'ServerWCF/RBACService.svc/AutoCreateUserAccount',
    classCode:'BTDAppComponents_ClassCode',
    autoScroll:true,
    type:'add',
    /**
     * 用户类型:userType
     * 管理员类型:amdin
     *当前用户类型:present
     * @type String
     */
    userType:'present',
    /**
     * 员工信息:
     * @type String
     */
    userInfo:'',
    /**
     * 帐户Id
     * @type String
     */
    accountId:'',
    bodyCls:'',
    layout:'absolute',
    setWinformInfo:function(record, com) {
        var me = this;
        var itemId = com.boundField;
        var value = record.get('Id');
        var text = record.get('text');
        var winformtext = me.getComponent(itemId);
        winformtext.treeNodeID = record.get('Id');
        if (winformtext.xtype == 'combobox') {
            var arrTemp = [ [ value, text ] ];
            winformtext.store = new Ext.data.SimpleStore({
                fields:[ 'value', 'text' ],
                data:arrTemp,
                autoLoad:true
            });
            winformtext.setValue(value);
        } else {
            winformtext.treeNodeID = value;
            winformtext.setValue(text);
        }
    },
    getInfoByIdFormServer:function(id, callback) {
        var me = this;
        var myUrl = me.getAppInfoServerUrl + '?isPlanish=true&id=' + id;
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            async:false,
            url:myUrl,
            method:'GET',
            timeout:2000,
            success:function(response, opts) {
                var result = Ext.JSON.decode(response.responseText);
                if (result.success) {
                    var appInfo = '';
                    if (result.ResultDataValue && result.ResultDataValue != '') {
                        result.ResultDataValue = result.ResultDataValue.replace(/\\n/g, '\\\\u000a');
                        appInfo = Ext.JSON.decode(result.ResultDataValue);
                    }
                    if (Ext.typeOf(callback) == 'function') {
                        if (appInfo == '') {
                            Ext.Msg.alert('提示', '没有获取到应用组件信息！');
                        } else {
                            callback(appInfo);
                        }
                    }
                } else {
                    Ext.Msg.alert('提示', '获取应用信息失败！');
                }
            },
            failure:function(response, options) {
                Ext.Msg.alert('提示', '获取应用信息请求失败！');
            }
        });
    },
    functionBtnClick:function(com, e, optes) {
        var me = this;
        var textItemId = com.boundField;
        var textCom = me.getComponent(textItemId);
        var appComID = textCom.appComID;
        if (appComID != '' && appComID != null && appComID != undefined) {
            var callback = function(appInfo) {
                var title = textCom.getValue();
                var ClassCode = '';
                if (appInfo && appInfo != '') {
                    ClassCode = appInfo[me.classCode];
                }
                if (ClassCode && ClassCode != '') {
                    me.openAppShowWin(title, ClassCode, com);
                } else {
                    Ext.Msg.alert('提示', '没有类代码！');
                }
            };
            me.getInfoByIdFormServer(appComID, callback);
        } else {
            Ext.Msg.alert('提示', '功能按钮没有绑定应用！');
        }
    },
    openAppShowWin:function(title, classCode, com) {
        var me = this;
        var panel = eval(classCode);
        var maxHeight = document.body.clientHeight * .98;
        var maxWidth = document.body.clientWidth * .98;
        var appList = Ext.create(panel, {
            maxWidth:maxWidth,
            maxHeight:maxHeight,
            autoScroll:true,
            model:true,
            floating:true,
            closable:true,
            draggable:true
        }).show();
        appList.on({
            okClick:function() {
                var records = appList.getValue();
                if (records.length == 0) {
                    Ext.Msg.alert('提示', '请选择一个应用！');
                } else if (records.length == 1) {
                    me.setWinformInfo(records[0], com);
                }
            },
            itemdblclick:function(view, record, item, index, e, eOpts) {
                me.setWinformInfo(record, com);
            }
        });
    },
    GetGroupItems:function(url2, valueField, displayField, groupName, defaultValue) {
        var myUrl = url2;
        if (myUrl == '' || myUrl == null) {
            Ext.Msg.alert('提示', myUrl);
            return null;
        } else {
            myUrl = getRootPath() + '/' + myUrl;
        }
        var localData = [];
        Ext.Ajax.request({
            async:false,
            timeout:6000,
            url:myUrl,
            method:'GET',
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
                    var mychecked = false;
                    var arrStr = [];
                    if (defaultValue != '') {
                        arrStr = defaultValue.split(',');
                    }
                    for (var i = 0; i < count; i++) {
                        var DeptID = ResultDataValue.list[i][valueField];
                        var CName = ResultDataValue.list[i][displayField];
                        if (arrStr.length > 0) {
                            mychecked = Ext.Array.contains(arrStr, DeptID);
                        }
                        var tempItem = {
                            checked:mychecked,
                            name:groupName,
                            boxLabel:CName,
                            inputValue:DeptID
                        };
                        localData.push(tempItem);
                    }
                } else {
                    Ext.Msg.alert('提示', '获取信息失败！');
                }
            }
        });
        return localData;
    },
    beforeRender:function() {
        var me = this;
        me.callParent(arguments);
        if (!(me.header === false)) {
            me.updateHeader();
        }
    },
    initComponent:function() {
        var me = this;
        me.addEvents('beforeSave');
        me.addEvents('saveClick');
        me.addEvents('functionBtnClick');
        me.VTypes();
        me.LenTypes();
        me.OverrideField();
        me.load = function(id) {
            Ext.Ajax.request({
                async:false,
                url:getRootPath() + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACUserById?isPlanish=true&fields=RBACUser_Account,RBACUser_PWD,RBACUser_EnMPwd,RBACUser_PwdExprd&id=' + (id ? id :-1),
                method:'GET',
                timeout:5000,
                success:function(response, opts) {
                    var result = Ext.JSON.decode(response.responseText);
                    if (result.success) {
                        if (result.ResultDataValue && result.ResultDataValue != '') {
                            if (me.type == 'add') {
                                me.type = 'edit';
                            }
                            var values = Ext.JSON.decode(result.ResultDataValue);
                            me.getForm().setValues(values);
                        }
                    } else {
                        Ext.Msg.alert('提示', '获取表单数据失败！');
                    }
                },
                failure:function(response, options) {
                    Ext.Msg.alert('提示', '获取表单数据请求失败！');
                }
            });
        };
        me.submit = function() {
            var me = this;
            var bo = me.fireEvent('beforeSave');
            if (!bo) return;
            if (!me.getForm().isValid()) return;
            if (me.type == 'show') {
                Ext.Msg.alert('提示', '查看页面不能提交！');
                return;
            }
            var url = '';
            if (me.type == 'add') {
                url = me.addDataServerUrl;
            } else if (me.type == 'edit') {
                url = me.editDataServerUrl;
            }
            if (url == '') return;
            var values = me.getForm().getValues();
            //values={RBACUser_Account:values['RBACUser_Account'],RBACUser_PWD:values['RBACUser_PWD']};
            //values={RBACUser_Account:'zhangliu1',RBACUser_PWD:'1234'};
            var maxLength = 0;
            for (var i in values) {
                var arr = i.split('_');
                if (arr.length > maxLength) {
                    maxLength = arr.length;
                }
            }
            var obj = {};
            var addObj = function(key, num, value) {
                var keyArr = key.split('_');
                var ob = 'obj';
                for (var i = 1; i < keyArr.length; i++) {
                    ob = ob + '[\"' + keyArr[i] + '\"]';
                    if (!eval(ob)) {
                        eval(ob + '={};');
                    }
                }
                if (keyArr.length == num + 1) {
                    eval(ob + '=value;');
                }
            };
            for (var i = 1; i < maxLength; i++) {
                for (var j in values) {
                    var value = values[j];
                    addObj(j, i, value);
                }
            }
            if (obj.Id == '' || obj.Id == null) {
                obj.Id = -1;
            }
            var isEmptyObject = function(obj) {
                for (var name in obj) {
                    return false;
                }
                return true;
            };
            var deleteNodeArr = [];
            for (var i in obj) {
                if (isEmptyObject(obj[i])) {
                    deleteNodeArr.push(i);
                }
            }
            for (var i in deleteNodeArr) {
                delete obj[deleteNodeArr[i]];
            }
            var changeDataTimeStamp = function(obj) {
                for (var i in obj) {
                    if (Ext.typeOf(obj[i]) == 'object') {
                        changeDataTimeStamp(obj[i]);
                    } else {
                        if (i == 'DataTimeStamp' && obj[i] && obj[i] != '') {
                            obj[i] = obj[i].split(',');
                        }
                        if (i == 'DataTimeStamp' && me.type == 'edit') {
                            delete obj[i];
                        }
                    }
                }
            };
            changeDataTimeStamp(obj);
            var params = {
                entity:obj
            };
            if (me.type == 'edit') {
                var field = '';
                for (var i in values) {
                    var keyArr = i.split('_');
                    if (keyArr.slice(-1) != 'DataTimeStamp') {
                        field = field + keyArr.slice(1).join('_') + ',';
                    }
                }
                if (field != '') {
                    field = field.slice(0, -1);
                }
                params.fields = field;
            }
            Ext.Ajax.defaultPostHeader = 'application/json';
            Ext.Ajax.request({
                async:false,
                url:getRootPath() + '/' + url,
                params:obj,
                params:Ext.JSON.encode(params),
                method:'POST',
                timeout:5000,
                success:function(response, opts) {
                    var result = Ext.JSON.decode(response.responseText);
                    if (result.success) {
                        if (result.ResultDataValue && result.ResultDataValue != '') {
                            var key = me.objectName + '_Id';
                            me.getForm().setValues([ {
                                id:key,
                                value:result.ResultDataValue
                            } ]);
                        }
                        me.fireEvent('saveClick');
                        if (me.isSuccessMsg == true) {
                            Ext.Msg.alert('提示', '保存成功！');
                        }
                    } else {
                        Ext.Msg.alert('提示', '<b>处理错误！原因如下：<font style=\"color:red\">' + action.result.ErrorInfo + '</font></b>');
                    }
                },
                failure:function(response, options) {
                    Ext.Msg.alert('提示', '保存请求失败！');
                }
            });
        };
        me.isAdd = function() {
            me.getForm().reset();
            var buts = me.getComponent('dockedItems-buttons');
            if (buts) {
                if (me.type == 'show') {
                    me.setHeight(me.getHeight() + 25);
                }
                buts.show();
            }
            me.type = 'add';
            me.setReadOnly(false);
            me.getForm().reset();
        };
        me.isEdit = function(id) {
            var buts = me.getComponent('dockedItems-buttons');
            if (buts) {
                if (me.type == 'show') {
                    me.setHeight(me.getHeight() + 25);
                }
                buts.show();
            }
            me.type = 'edit';
            me.setReadOnly(false);
            me.load(id);
        };
        me.isShow = function(id) {
            var buts = me.getComponent('dockedItems-buttons');
            if (buts) {
                if (me.type != 'show') {
                    me.setHeight(me.getHeight() - 25);
                }
                buts.hide();
            }
            me.type = 'show';
            me.setReadOnly(true);
            me.load(id);
        };
        me.setValueByItemId = function(key, value) {
            me.getForm().setValues([ {
                id:key,
                value:value
            } ]);
        };
        me.changeStoreData = function(response) {
            var data = Ext.JSON.decode(response.responseText);
            if (data.ResultDataValue && data.ResultDataValue != '') {
                var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
                data.ResultDataValue = ResultDataValue;
                data.list = ResultDataValue.list;
            } else {
                data.list = [];
            }
            response.responseText = Ext.JSON.encode(data);
            return response;
        };
        me.setReadOnly = function(bo) {
            var items2 = me.items.items;
            for (var i in items2) {
                if (!items2[i].hasReadOnly) {
                    var type = items2[i].xtype;
                    if (type == 'button' || type == 'label') {} else {
                        items2[i].setReadOnly(bo);
                    }
                }
            }
        };
        me.items = [ {
            xtype:'textfield',
            name:'RBACUser_Account',
            fieldLabel:'员工帐号',
            labelWidth:80,
            labelStyle:'font-style:normal;text-align:left;',
            width:240,
            height:25,
            itemId:'RBACUser_Account',
            value:'zhangliu1',
            x:15,
            y:5,
            readOnly:true,
            appComID:'',
            treeNodeID:'',
            value:'',
            isFunctionBtn:false,
            hidden:false,
            sortNum:1,
            listeners:{
                scope:this,
                specialkey:function(field, e) {
                    var iNum = 1;
                    var sNumField = 'sortNum';
                    var form = field.ownerCt;
                    var num = field[sNumField];
                    var items = form.items.items;
                    var max = 4;
                    if (e.getKey() == Ext.EventObje1ct.ENTER || e.getKey() == Ext.EventObject.TAB) {
                        e.preventDefault();
                    }
                    if (num > 0 && (e.getKey() == Ext.EventObject.ENTER || e.getKey() == Ext.EventObject.TAB)) {
                        if (!e.shiftKey) {
                            num = num + iNum > max ? num + iNum - max :num + iNum;
                        } else {
                            num = num - iNum < 1 ? num - iNum + max :num - iNum;
                        }
                        for (var i in items) {
                            if (items[i].sortNum == num) {
                                items[i].focus(false, 100);
                                break;
                            }
                        }
                    }
                }
            },
            hasReadOnly:true,
            labelAlign:'left'
        }, {
            xtype:'textfield',
            name:'RBACUser_EnMPwd',
            fieldLabel:'请输入旧密码',
            labelWidth:80,
            labelStyle:'font-style:normal;text-align:left;',
            width:240,
            height:25,
            inputType:'password',
            itemId:'RBACUser_EnMPwd',
            x:15,
            y:41,
            readOnly:false,
            appComID:'',
            treeNodeID:'',
            value:'',
            isFunctionBtn:false,
            hidden:false,
            sortNum:2,
            listeners:{
                scope:this,
                specialkey:function(field, e) {
                    var iNum = 1;
                    var sNumField = 'sortNum';
                    var form = field.ownerCt;
                    var num = field[sNumField];
                    var items = form.items.items;
                    var max = 4;
                    if (e.getKey() == Ext.EventObject.ENTER || e.getKey() == Ext.EventObject.TAB) {
                        e.preventDefault();
                    }
                    if (num > 0 && (e.getKey() == Ext.EventObject.ENTER || e.getKey() == Ext.EventObject.TAB)) {
                        if (!e.shiftKey) {
                            num = num + iNum > max ? num + iNum - max :num + iNum;
                        } else {
                            num = num - iNum < 1 ? num - iNum + max :num - iNum;
                        }
                        for (var i in items) {
                            if (items[i].sortNum == num) {
                                items[i].focus(false, 100);
                                break;
                            }
                        }
                    }
                },
                blur:function(com,The,eOpts)
                {    
                 //新密码设置                
                 var setnewPWD=me.getComponent('RBACUser_PwdExprd');
                 //新密码验证
                 var newPWD = me.getComponent('RBACUser_PWD');
                 //帐户
                 var RBACUser_Account1=me.getComponent('RBACUser_Account');
                 //确定按钮OK
                 var butOK=me.getComponent('OK');
                 //var value='zhangliu1';
                 //RBACUser_Account.setValue(value);
                 var odlpwdvalue=com.getValue();            
                 //获取旧密码
                 var userInfostr=me.userInfo;
                 var  old=userInfostr['RBACUser_PWD'];
                 if(odlpwdvalue==old)
                 {                    
                    setnewPWD.setReadOnly(false); 
                    newPWD.setReadOnly(false);                    
                    butOK.setVisible(true);
                 }else{
                    setnewPWD.setReadOnly(true); 
                    newPWD.setReadOnly(true); 
                    butOK.setVisible(false);                    
                    //Ext.Msg.alert('提示', '您输入的密码不正确！');
                 }               
                 
                }
            },
            hasReadOnly:false,
            labelAlign:'left'
        }, {
            xtype:'textfield',
            name:'RBACUser_PwdExprd',
            fieldLabel:'新密码设置',
            labelWidth:80,
            labelStyle:'font-style:normal;text-align:left;',
            width:260,
            height:25,
            itemId:'RBACUser_PwdExprd',
            id:'RBACUser_PwdExprd1',
            emptyText:'请输入4-12位新密码',
            inputType:'password',
            x:15,
            y:74,
            readOnly:true,
            appComID:'',
            treeNodeID:'',
            value:'',
            maxLengthText: '长度不能超过12个字符',
            minLengthText: '长度不能超过4个字符',
            allowBlank : false,
            //validateOnBlur: true,
            //validateOnChange: false,
            showValidIcon: true,
            msgTarget:'side',
            vtype:'isLen',  
            isFunctionBtn:false,
            hidden:false,
            sortNum:3,
            listeners:{
                scope:this,
                specialkey:function(field, e) {
                    var iNum = 1;
                    var sNumField = 'sortNum';
                    var form = field.ownerCt;
                    var num = field[sNumField];
                    var items = form.items.items;
                    var max = 4;
                    if (e.getKey() == Ext.EventObject.ENTER || e.getKey() == Ext.EventObject.TAB) {
                        e.preventDefault();
                    }
                    if (num > 0 && (e.getKey() == Ext.EventObject.ENTER || e.getKey() == Ext.EventObject.TAB)) {
                        if (!e.shiftKey) {
                            num = num + iNum > max ? num + iNum - max :num + iNum;
                        } else {
                            num = num - iNum < 1 ? num - iNum + max :num - iNum;
                        }
                        for (var i in items) {
                            if (items[i].sortNum == num) {
                                items[i].focus(false, 100);
                                break;
                            }
                        }
                    }
                }
            },
            hasReadOnly:true,
            labelAlign:'left'
        }, /*{
            xtype:'label',
            text:'4-12位',
            labelStyle:'font-style:normal;text-align:center;font-size:12pt;color:6677;',
            width:60,
            heigth:25,
            x:253,
            y:80
        },*/
        {
            xtype:'textfield',
            name:'RBACUser_PWD',
            fieldLabel:'新密码验证',
            labelWidth:80,
            labelStyle:'font-style:normal;text-align:left;',
            width:260,
            height:25,
            itemId:'RBACUser_PWD',
            emptyText :'请再一次输入密码',
            inputType:'password',
            x:15,
            y:107,
            readOnly:true,
            showValidIcon: true,
            msgTarget:'side',
            appComID:'',
            treeNodeID:'',
            value:'',
            isFunctionBtn:false,            
            blankText:'不允许为空',
            allowBlank:false,
            vtype:'password',   
            initialPassField:'RBACUser_PwdExprd1',//
            //validateOnBlur: true,
            //validateOnChange: false,
            hidden:false,
            sortNum:4,
            listeners:{
                scope:this,
                specialkey:function(field, e) {
                    var iNum = 1;
                    var sNumField = 'sortNum';
                    var form = field.ownerCt;
                    var num = field[sNumField];
                    var items = form.items.items;
                    var max = 4;
                    if (e.getKey() == Ext.EventObject.ENTER || e.getKey() == Ext.EventObject.TAB) {
                        e.preventDefault();
                    }
                    if (num > 0 && (e.getKey() == Ext.EventObject.ENTER || e.getKey() == Ext.EventObject.TAB)) {
                        if (!e.shiftKey) {
                            num = num + iNum > max ? num + iNum - max :num + iNum;
                        } else {
                            num = num - iNum < 1 ? num - iNum + max :num - iNum;
                        }
                        for (var i in items) {
                            if (items[i].sortNum == num) {
                                items[i].focus(false, 100);
                                break;
                            }
                        }
                    }
                },
                change:function(com,newValue,oldValue,eOpts )
                {
                    //me.OverrideField();
                  //textfield后加图片方法               
               /*  var _parentNode = me.getComponent('RBACUser_PWD').parentNode;
                 Ext.get(_parentNode).createChild({
                     tag : 'span',
                     src : '../css/images/png/64x64/accept.png'
                 });*/
                }
            },
            hasReadOnly:true,
            labelAlign:'left'
        },{
            xtype:"textfield",
            name:"RBACUser_HREmployee_Id",
            fieldLabel:"主键ID",
            labelWidth:60,
            labelStyle:"font-style:normal;",
            width:160,
            height:22,
            itemId:"RBACUser_HREmployee_Id",
            x:5,
            y:135,
            readOnly:false,
            appComID:"",
            treeNodeID:"",
            value:"",
            isFunctionBtn:false,
            hidden:true,
            sortNum:6,
            listeners:{
                scope:this,
                specialkey:function(field, e) {
                    var iNum = 1;
                    var sNumField = "sortNum";
                    var form = field.ownerCt;
                    var num = field[sNumField];
                    var items = form.items.items;
                    var max = 9;
                    if (e.getKey() == Ext.EventObject.ENTER || e.getKey() == Ext.EventObject.TAB) {
                        e.preventDefault();
                    }
                    if (num > 0 && (e.getKey() == Ext.EventObject.ENTER || e.getKey() == Ext.EventObject.TAB)) {
                        if (!e.shiftKey) {
                            num = num + iNum > max ? num + iNum - max :num + iNum;
                        } else {
                            num = num - iNum < 1 ? num - iNum + max :num - iNum;
                        }
                        for (var i in items) {
                            if (items[i].sortNum == num) {
                                items[i].focus(false, 100);
                                break;
                            }
                        }
                    }
                }
            },
            hasReadOnly:false,
            labelAlign:"left"
        }, {
            xtype:"textfield",
            name:"RBACUser_HREmployee_DataTimeStamp",
            fieldLabel:"时间戳",
            labelWidth:60,
            labelStyle:"font-style:normal;",
            width:160,
            height:22,
            itemId:"RBACUser_HREmployee_DataTimeStamp",
            x:5,
            y:161,
            readOnly:false,
            appComID:"",
            treeNodeID:"",
            value:"",
            isFunctionBtn:false,
            hidden:true,
            sortNum:7,
            listeners:{
                scope:this,
                specialkey:function(field, e) {
                    var iNum = 1;
                    var sNumField = "sortNum";
                    var form = field.ownerCt;
                    var num = field[sNumField];
                    var items = form.items.items;
                    var max = 9;
                    if (e.getKey() == Ext.EventObject.ENTER || e.getKey() == Ext.EventObject.TAB) {
                        e.preventDefault();
                    }
                    if (num > 0 && (e.getKey() == Ext.EventObject.ENTER || e.getKey() == Ext.EventObject.TAB)) {
                        if (!e.shiftKey) {
                            num = num + iNum > max ? num + iNum - max :num + iNum;
                        } else {
                            num = num - iNum < 1 ? num - iNum + max :num - iNum;
                        }
                        for (var i in items) {
                            if (items[i].sortNum == num) {
                                items[i].focus(false, 100);
                                break;
                            }
                        }
                    }
                }
            },
            hasReadOnly:false,
            labelAlign:"left"
        }, {
            xtype:"textfield",
            name:"RBACUser_Id",
            fieldLabel:"主键ID",
            labelWidth:60,
            labelStyle:"font-style:normal;",
            width:160,
            height:22,
            itemId:"RBACUser_Id",
            x:5,
            y:187,
            readOnly:false,
            appComID:"",
            treeNodeID:"",
            value:"",
            isFunctionBtn:false,
            hidden:true,
            sortNum:8,
            listeners:{
                scope:this,
                specialkey:function(field, e) {
                    var iNum = 1;
                    var sNumField = "sortNum";
                    var form = field.ownerCt;
                    var num = field[sNumField];
                    var items = form.items.items;
                    var max = 9;
                    if (e.getKey() == Ext.EventObject.ENTER || e.getKey() == Ext.EventObject.TAB) {
                        e.preventDefault();
                    }
                    if (num > 0 && (e.getKey() == Ext.EventObject.ENTER || e.getKey() == Ext.EventObject.TAB)) {
                        if (!e.shiftKey) {
                            num = num + iNum > max ? num + iNum - max :num + iNum;
                        } else {
                            num = num - iNum < 1 ? num - iNum + max :num - iNum;
                        }
                        for (var i in items) {
                            if (items[i].sortNum == num) {
                                items[i].focus(false, 100);
                                break;
                            }
                        }
                    }
                }
            },
            hasReadOnly:false,
            labelAlign:"left"
        }, {
            xtype:"textfield",
            name:"RBACUser_DataTimeStamp",
            fieldLabel:"时间戳",
            labelWidth:60,
            labelStyle:"font-style:normal;",
            width:160,
            height:22,
            itemId:"RBACUser_DataTimeStamp",
            x:5,
            y:213,
            readOnly:false,
            appComID:"",
            treeNodeID:"",
            value:"",
            isFunctionBtn:false,
            hidden:true,
            sortNum:9,
            listeners:{
                scope:this,
                specialkey:function(field, e) {
                    var iNum = 1;
                    var sNumField = "sortNum";
                    var form = field.ownerCt;
                    var num = field[sNumField];
                    var items = form.items.items;
                    var max = 9;
                    if (e.getKey() == Ext.EventObject.ENTER || e.getKey() == Ext.EventObject.TAB) {
                        e.preventDefault();
                    }
                    if (num > 0 && (e.getKey() == Ext.EventObject.ENTER || e.getKey() == Ext.EventObject.TAB)) {
                        if (!e.shiftKey) {
                            num = num + iNum > max ? num + iNum - max :num + iNum;
                        } else {
                            num = num - iNum < 1 ? num - iNum + max :num - iNum;
                        }
                        for (var i in items) {
                            if (items[i].sortNum == num) {
                                items[i].focus(false, 100);
                                break;
                            }
                        }
                    }
                }
            },
            hasReadOnly:false,
            labelAlign:"left"
        },
        {
            xtype:'button',
            text:'确定',
            itemId:'OK',
            hidden:true,
            x:129,
            y:144,
            width:79,
            height:22,
            listeners:{
                click:function(but,e,eOpts){
                    //新密码设置          
                    var newpassword=me.getComponent('RBACUser_PwdExprd');
                    var newPwd=newpassword.getValue();
                    var olduserid=me.userInfo['RBACUser_Id'];
                    var objdata={RBACUser_Id:olduserid,RBACUser_PWD:newPwd};
                    //var objdata={RBACUser_Id:'4648802724965916819',RBACUser_PWD:'123456789'};
                    //var editerJSON=Ext.JSON.decode(objdata); 
                 //新密码验证
                 var newPWD = me.getComponent('RBACUser_PWD');
                 if(newPwd==newPWD.getValue()){
                    me.updateUserPassword(objdata); 
                 }else
                 {
                    Ext.Msg.alert('提示','请检查新设置密码输入是否一致！');
                 }
                    
                }
            }
        } ];       
        
           me.callParent(arguments);
          },
    afterRender:function() {
        var me = this;
        me.callParent(arguments);
        if (Ext.typeOf(me.callback) == 'function') {
            me.callback(me);
        }        
        if (me.type == 'add') {
            me.isAdd();
        } else if (me.type == 'edit') {
            me.isEdit(me.dataId);
        } else if (me.type == 'show') {
            me.isShow(me.dataId);
        } 
        //获取帐户信息
        me.getAccountInfo();
        /*if(me.userInfo!='')
        {
            var useraccount=me.getComponent('RBACUser_Account');
            var userName=me.userInfo['RBACUser_Account'];
            useraccount.setValue(userName);
        }*/
    },
    /**
     * 获取用户帐户名称
     * @param {} id
     * @param {} callback
     */
    getAccountInfo:function(){
            var me = this;
            Ext.Ajax.defaultPostHeader = 'application/json';
            Ext.Ajax.request({
                async:false,//非异步
                url:me.getAccountUrl,
                method:'GET',
                timeout:2000,
                success:function(response,opts){
                    var result = Ext.JSON.decode(response.responseText);
                    if(result.success){
                        if(result.ResultDataValue && result.ResultDataValue != ""){
                            var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
                            var addData = {
                                RBACUser_Account:ResultDataValue.RBACUserList[0]['Account'],
                                RBACUser_PWD:ResultDataValue.RBACUserList[0]['PWD'],
                                RBACUser_EnMPwd:ResultDataValue.RBACUserList[0]['EnMPwd'],
                                RBACUser_AccBeginTime:ResultDataValue.RBACUserList[0]['AccBeginTime'],
                                RBACUser_AccEndTime:ResultDataValue.RBACUserList[0]['AccEndTime'],
                                RBACUser_Id:ResultDataValue.RBACUserList[0]['Id'],
                                RBACUser_DataTimeStamp:ResultDataValue.RBACUserList[0]['DataTimeStamp']
                        };
                        //取表单的itemId
                        var RBACUser_Account2=me.getComponent('RBACUser_Account');
                        var RBACUser_EnMPwd=me.getComponent('RBACUser_EnMPwd');
                       //赋值
                        var account2=addData['RBACUser_Account'];
                        RBACUser_Account2.setValue(account2);  
                        //alert('用户帐户名称：'+RBACUser_Account2.getValue());
                        //将数据赋给userInfo得到帐户信息
                        me.userInfo=addData;       
                       
                        }else{
                            Ext.Msg.alert('提示','没有获取到应用信息！');
                        }
                    }else{
                        Ext.Msg.alert('提示','获取应用信息失败！错误信息【<b style="color:red">'+ result.ErrorInfo +"</b>】");
                    }
                },
                failure : function(response,options){ 
                    Ext.Msg.alert('提示','获取应用信息请求失败！');
                }
            });        
    },   
    
    //密码验证
    validatorPassword:function (value,pwdvalue) {
        var me=this;
        var validator = this;
        var error = true;
        Ext.Ajax.request({
            async: false,
            scope: validator,
            url:me.CheckUserPWDUrl+'?RBACUser_Account=' + value+'&&'+'?RBACUser_PWD=' + pwdvalue,            
            params: {loginname:this.value},
            method: 'GET',
            success: function (response) {
                var result = Ext.JSON.decode(response.responseText);

                if (!result.success) {
                    error = "当前输入的密码不正确,请重新输入！";
                }else
                {
                    //新密码设置                
                    var setnewPWD=me.getComponent('RBACUser_PwdExprd');
                     //新密码验证
                    var newPWD = me.getComponent('RBACUser_PWD');
                    setnewPWD.setReadOnly(false); 
                    newPWD.setReadOnly(false);
                }
            },
            failure:function(response, options) {
                Ext.Msg.alert('提示', '获取信息请求失败！');
            }
        });
        return error;
    },
    
    /**
     * 多字段验证用VType（密码验证）
     * @private
     */
    VTypes:function(){
        var bb=Ext.apply(Ext.form.VTypes,{
            password:function(val,field){
            if(field.initialPassField){
                var pwd = Ext.getCmp(field.initialPassField);
                return (val == pwd.getValue());
            }
            return true;
        },
        passwordText:'两次密码不一致'
    });
        return bb;
    },
    
    /**
     * 验证账号名长度
     * @private
     */
    LenTypes:function(){
        var bb=Ext.apply(Ext.form.VTypes,{
            isLen:function(val,field){
                //返回true，则验证通过，否则验证失败
                var exp=/^[a-zA-Z0-9_]{4,12}$/;
                var reg = val.match(exp);
                if(reg==null){
                    return false;
                }else{
                    return true;
                }
            },
        isLenText: '非法格式'   
      });
      return bb;
    },
    
    //=====修改用户密码方法
    updateUserPassword:function(strobj){       
        var me = this; 
        var myUrl='';
        if(me.editDataServerUrl!="")
        {
            myUrl=getRootPath() + "/"+me.editDataServerUrl;
        }
        var values=strobj;
        var maxLength = 0;
        //循环判断找出字段中与'_'分隔的数组最大长度如：HREmployee_DataTimeStamp为2
            for(var i in values)
               {
                var arr = i.split('_');
                if(arr.length > maxLength)
                 {
                    maxLength = arr.length;
                 }                   
               }
             var obj = {};
             var addObj = function(key,num,value)
                {
                    var keyArr = key.split('_');
                    var ob = 'obj';
                    var objSJC='';    //保存时间戳
                    for(var i=1;i<keyArr.length;i++)
                    {
                        //获取保存到数据库的字段
                          ob = ob + '[\"' + keyArr[i] + '\"]';
                          objSJC=keyArr[i];
                          if(!eval(ob))
                          {
                             eval(ob + '={};');
                          }
                      }
                      //对应字段赋值
                      if(keyArr.length == num+1)
                        {
                            if(objSJC=='DataTimeStamp')
                            {
                                value=value.split(',');
                            }
                           eval(ob + '=value;');
                        }
                 };
               for(var i=1;i<maxLength;i++)
                {
                     for(var j in values)
                        {
                          var value = values[j];
                          addObj(j,i,value);
                        }
                 }
                 var field = '';
                 if(maxLength == 2)
                    {
                       for(var i in values)
                          {
                              var keyArr = i.split('_');
                              field = field + keyArr[1] + ',';
                          }
                     }
                  if(field != '')
                    {
                        field = field.substring(0,field.length-1);
                     }
        
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            async:false,//非异步
            url:myUrl,//editDataServerUrl
            params:Ext.JSON.encode({entity:obj,fields:field}),
            method:'POST',
            timeout:7000,
            success:function(response,opts){
                var result = Ext.JSON.decode(response.responseText);
                if(result.success){
                    /*if(Ext.typeOf(callback) == "function"){
                        callback();//回调函数
                    }*/
                    Ext.Msg.alert('提示','保存成功！');                   
                }else{
                    Ext.Msg.alert('提示','保存应用组件失败！错误信息【<b style="color:red">'+ result.ErrorInfo +"</b>】");
                }
            },
            failure : function(response,options){ 
                Ext.Msg.alert('提示','保存应用组件请求失败！');
            }
        });       
    },
    /**
     * 打勾
     * @private
     */
    OverrideField:function(){
        var nn=Ext.layout.component.field.Field.override({
            getErrorStrategy:function() {
            var me = this,
            owner = me.owner,
            strategies = me.errorStrategies,
            msgTarget = owner.msgTarget;
            var strategy = !owner.preventMark && Ext.isString(msgTarget) ? strategies[msgTarget] || strategies.elementId :strategies.none;
            if (msgTarget == "side" && owner.showValidIcon) {
                if (owner.isIconInit) {
                    owner.errorEl.setDisplayed(false);
                    owner.isIconInit = true;
                }
                owner.on("validitychange", function(me, valid) {
                    me.errorEl.setDisplayed(true);
                });
                Ext.apply(strategy, {
                    adjustHorizInsets:Ext.emptyFn,
                    layoutHoriz:function(ownerContext, owner, size) {
                        ownerContext.errorContext.setProp("x", size.width);
                    },
                    layoutVert:function(ownerContext, owner) {
                        ownerContext.errorContext.setProp("y", ownerContext.insets.top);
                    },
                    prepare:function(ownerContext, owner) {
                        var errorEl = owner.errorEl;
                        errorEl.addCls(Ext.baseCSSPrefix + "form-invalid-icon");
                        errorEl.set({
                            "data-errorqtip":owner.getActiveError() || ""
                        });
                        var activeError = owner.getActiveError();
                        var hasError = !!activeError;
                        errorEl[hasError ? "removeCls" :"addCls"]("icon-yes");
                        Ext.layout.component.field.Field.initTip();
                    }
                });
            }
            return strategy;
        }
    });
        return nn;
    }

});