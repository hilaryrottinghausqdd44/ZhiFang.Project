/** 账号申请
**/
Ext.ns('Ext.zhifangux');
Ext.define('Ext.zhifangux.AddAccount', {
	extend:'Ext.form.Panel',
    alias:"widget.addaccount",
    title:"账号申请",
    width:425,
    height:295,
    header:true,
    objectName:"RBACUser",
    AccountValue:'',
    dateNew:new  Date(),
    isSuccessMsg:true,
    getAppInfoServerUrl:getRootPath() + "/" + "ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById",
    addDataServerUrl:"RBACService.svc/RBAC_UDTO_AddRBACUser",
    editDataServerUrl:"RBACService.svc/RBAC_UDTO_UpdateRBACUserByField",
    classCode:"BTDAppComponents_ClassCode",
    autoScroll:true,
    type:"add",
    bodyCls:"",
    isok:false,
    layout:"absolute",
    CheckUserNameUrl:getRootPath()+'/'+'RBACService.svc/RBAC_RJ_ValidateUserAccountIsExist',
    CreateUserAccountUrl:getRootPath()+'/'+'RBACService.svc/RBAC_RJ_AutoCreateUserAccount',
    RandomPwdUrl:getRootPath()+'/'+'RBACService.svc/RBAC_RJ_GetRandomNumber',
    setWinformInfo:function(record, com) {
        var me = this;
        var itemId = com.boundField;
        var value = record.get("Id");
        var text = record.get("text");
        var winformtext = me.getComponent(itemId);
        winformtext.treeNodeID = record.get("Id");
        if (winformtext.xtype == "combobox") {
            var arrTemp = [ [ value, text ] ];
            winformtext.store = new Ext.data.SimpleStore({
                fields:[ "value", "text" ],
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
        var myUrl = me.getAppInfoServerUrl + "?isPlanish=true&id=" + id;
        Ext.Ajax.defaultPostHeader = "application/json";
        Ext.Ajax.request({
            async:false,
            url:myUrl,
            method:"GET",
            timeout:2e3,
            success:function(response, opts) {
                var result = Ext.JSON.decode(response.responseText);
                if (result.success) {
                    var appInfo = "";
                    if (result.ResultDataValue && result.ResultDataValue != "") {
                        result.ResultDataValue = result.ResultDataValue.replace(/\\n/g, "\\\\u000a");
                        appInfo = Ext.JSON.decode(result.ResultDataValue);
                    }
                    if (Ext.typeOf(callback) == "function") {
                        if (appInfo == "") {
                            Ext.Msg.alert("提示", "没有获取到应用组件信息！");
                        } else {
                            callback(appInfo);
                        }
                    }
                } else {
                    Ext.Msg.alert("提示", "获取应用信息失败！");
                }
            },
            failure:function(response, options) {
                Ext.Msg.alert("提示", "获取应用信息请求失败！");
            }
        });
    },
    functionBtnClick:function(com, e, optes) {
        var me = this;
        var textItemId = com.boundField;
        var textCom = me.getComponent(textItemId);
        var appComID = textCom.appComID;
        if (appComID != "" && appComID != null && appComID != undefined) {
            var callback = function(appInfo) {
                var title = textCom.getValue();
                var ClassCode = "";
                if (appInfo && appInfo != "") {
                    ClassCode = appInfo[me.classCode];
                }
                if (ClassCode && ClassCode != "") {
                    me.openAppShowWin(title, ClassCode, com);
                } else {
                    Ext.Msg.alert("提示", "没有类代码！");
                }
            };
            me.getInfoByIdFormServer(appComID, callback);
        } else {
            Ext.Msg.alert("提示", "功能按钮没有绑定应用！");
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
                    Ext.Msg.alert("提示", "请选择一个应用！");
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
        if (myUrl == "" || myUrl == null) {
            Ext.Msg.alert("提示", myUrl);
            return null;
        } else {
            myUrl = getRootPath() + "/" + myUrl;
        }
        var localData = [];
        Ext.Ajax.request({
            async:false,
            timeout:6e3,
            url:myUrl,
            method:"GET",
            success:function(response, opts) {
                var result = Ext.JSON.decode(response.responseText);
                if (result.success) {
                    var ResultDataValue = {
                        count:0,
                        list:[]
                    };
                    if (result["ResultDataValue"] && result["ResultDataValue"] != "") {
                        ResultDataValue = Ext.JSON.decode(result["ResultDataValue"]);
                    }
                    var count = ResultDataValue["count"];
                    var mychecked = false;
                    var arrStr = [];
                    if (defaultValue != "") {
                        arrStr = defaultValue.split(",");
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
                    Ext.Msg.alert("提示", "获取信息失败！");
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
        me.VTypes();
        me.LenTypes();
        me.OverrideField();
        me.addEvents("beforeSave");
        me.addEvents("saveClick");
        me.addEvents("functionBtnClick");
        me.load = function(id) {
            Ext.Ajax.request({
                async:false,
                url:getRootPath() + "/RBACService.svc/RBAC_UDTO_SearchRBACUserById?isPlanish=true&fields=RBACUser_Id,RBACUser_DataTimeStamp,RBACUser_LabID,RBACUser_Account,RBACUser_PWD,RBACUser_EnMPwd,RBACUser_PwdExprd,RBACUser_AccLock,RBACUser_HREmployee_Id,RBACUser_HREmployee_DataTimeStamp,RBACUser_AccBeginTime,RBACUser_AccEndTime&id=" + (id ? id :-1),
                method:"GET",
                timeout:5000,
                success:function(response, opts) {
                    var result = Ext.JSON.decode(response.responseText);
                    if (result.success) {
                        if (result.ResultDataValue && result.ResultDataValue != "") {
                            if (me.type == "add") {
                                me.type == "edit";
                            }
                            var values = Ext.JSON.decode(result.ResultDataValue);
                            me.getForm().setValues(values);
                        }
                    } else {
                        Ext.Msg.alert("提示", "获取表单数据失败！");
                    }
                },
                failure:function(response, options) {
                    Ext.Msg.alert("提示", "获取表单数据请求失败！");
                }
            });
        };
        me.submit = function(objdata) {
            var me = this;
            var url = "";
            if (me.type == "add") {
                url = me.addDataServerUrl;
            } 
            if (url == "") return;
            var values = objdata;
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
                for (var i = 1; i < keyArr.length; i++) {
                    ob = ob + '["' + keyArr[i] + '"]';
                    if (Ext.typeOf(eval(ob)) === "undefined") {
                        eval(ob + "={};");
                    }
                }
              //对应字段赋值
        	    if (keyArr.length == num + 1) {
        	        if (ob == "DataTimeStamp") {
        	            value = value.split(",");
        	        }
        	        eval(ob + "=value;");
        	    }
            };
            
            var   objnull={};
            for (var i = 1; i < maxLength; i++) {
                for (var j in values) {
                    var value = values[j];
                    var tempArr = j.split("_");
                    var tempStr = tempArr[tempArr.length - 1];
                    addObj(j, i, value);
                   
                }
            }
            if (obj.Id == "" || obj.Id == null) {
                obj.Id = -1;
            }
            var bbobj={"AccBeginTime":{},"AccEndTime":{}};
            var isEmptyObject = function(obj) {
                for (var name in obj) {
                    return false;
                }
                return true;
            };
            var deleteNodeArr = [];
            for (var i in bbobj) {
                if (isEmptyObject(obj[i])) {
                    deleteNodeArr.push(i);
                }
            }
            for (var i in deleteNodeArr) {
                delete obj[deleteNodeArr[i]];
            }
            var changeDataTimeStamp = function(obj) {
                for (var i in obj) {
                    if (Ext.typeOf(obj[i]) == "object") {
                        changeDataTimeStamp(obj[i]);
                    } else {
                        if (i == "DataTimeStamp" && obj[i] && obj[i] != "") {
                            obj[i] = obj[i].split(",");
                        }
                        if (i == "DataTimeStamp" && me.type == "edit") {
                            delete obj[i];
                        }
                    }
                }
            };
            changeDataTimeStamp(obj);
            var params = {
                entity:obj
            };
            if (me.type == "edit") {
                var field = "";
                for (var i in values) {
                    var keyArr = i.split("_");
                    if (keyArr.slice(-1) != "DataTimeStamp") {
                        field = field + keyArr.slice(1).join("_") + ",";
                    }
                }
                if (field != "") {
                    field = field.slice(0, -1);
                }
                params.fields = field;
            }
            Ext.Ajax.defaultPostHeader = "application/json";
            Ext.Ajax.request({
                async:false,
                url:getRootPath() + "/" + url,
                params:obj,
                params:Ext.JSON.encode(params),
                method:"POST",
                timeout:5000,
                success:function(response, opts) {
                    var result = Ext.JSON.decode(response.responseText);
                    if (result.success) {
                        if (result.ResultDataValue && result.ResultDataValue != "") {
                            var key = me.objectName + "_Id";
                            me.getForm().setValues([ {
                                id:key,
                                value:result.ResultDataValue
                            } ]);
                        }
                        me.fireEvent("saveClick");
                        if (me.isSuccessMsg == true) {
                            Ext.Msg.alert("提示", "保存成功！");
                        }
                    } else {
                        Ext.Msg.alert("提示", '<b>处理错误！原因如下：<font style="color:red">' + action.result.ErrorInfo + "</font></b>");
                    }
                },
                failure:function(response, options) {
                    Ext.Msg.alert("提示", "保存请求失败！");
                }
            });
        };
        me.isAdd = function() {
            me.getForm().reset();
            var buts = me.getComponent("dockedItems-buttons");
            if (buts) {
                if (me.type == "show") {
                    me.setHeight(me.getHeight() + 25);
                }
                buts.show();
            }
            me.type = "add";
            me.setReadOnly(false);
            me.getForm().reset();
        };
        me.isEdit = function(id) {
            var buts = me.getComponent("dockedItems-buttons");
            if (buts) {
                if (me.type == "show") {
                    me.setHeight(me.getHeight() + 25);
                }
                buts.show();
            }
            me.type = "edit";
            me.setReadOnly(false);
            me.load(id);
        };
        me.isShow = function(id) {
            var buts = me.getComponent("dockedItems-buttons");
            if (buts) {
                if (me.type != "show") {
                    me.setHeight(me.getHeight() - 25);
                }
                buts.hide();
            }
            me.type = "show";
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
            if (data.ResultDataValue && data.ResultDataValue != "") {
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
                    if (type == "button" || type == "label"|| type == "box") {} else {
                        items2[i].setReadOnly(bo);
                    }
                }
            }
        };
        me.items = [ {
            xtype:"textfield",
            name:"RBACUser_Account",
            fieldLabel:"员工账号",
            labelWidth:80,
            labelStyle:"font-style:normal;",
            width:211,
            height:22,
            itemId:"RBACUser_Account",
            id:"RBACUser_Account",
            x:10,
            y:15,
            readOnly:false,
            appComID:"",
            treeNodeID:"",
            value:"",
            isFunctionBtn:false,
            emptyText:"3-12位不能重复的账号",
            blankText:"请输入员工账号",
            maxLengthText:"长度不能超过12个字符",
            minLengthText:"长度不能超过3个字符",
            hidden:false,
            sortNum:1,
            showValidIcon:false,
            msgTarget:"side",
            allowBlank:false,
            validateOnBlur:true,
            validateOnChange:true,
            vtype:"isLen",
            listeners:{
                scope:this,
                specialkey:function(field, e) {
                    var iNum = 1;
                    var sNumField = "sortNum";
                    var form = field.ownerCt;
                    var num = field[sNumField];
                    var items = form.items.items;
                    var max = 5;
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
                blur:function(The, eOpts) {
                    var RBACUser_Account = me.getComponent("RBACUser_Account");
                    var Account = RBACUser_Account.getValue();
                    me.validator(Account);
                }
            },
            hasReadOnly:false,
            labelAlign:"left"
        }, {
            xtype:"label",
            cls:"textfield-red",
            style:"color:red;background:white;",
            labelStyle:"font-style:normal;",
            width:82,
            height:22,
            itemId:"txtBJ",
            hidden:true,
            x:218,
            y:16,
            text:"账号已存在"
        }, {
            xtype:"label",
            cls:"textfield-red",
            style:"color:red;background:white;",
            labelStyle:"font-style:normal;",
            width:82,
            height:22,
            itemId:"txtBJCD",
            hidden:true,
            x:218,
            y:16,
            text:"账号长度不符"
        }, {
            xtype:"box",
            width:26,
            id:"yzimage",
            itemId:"yzimage",
            x:220,
            y:10,
            height:26,
            hidden:true,
            autoEl:{
                tag:"img",
                src:""
            }
        }, {
            xtype:"datefield",
            name:"RBACUser_AccBeginTime",
            fieldLabel:"账号开始日期",
            labelWidth:80,
            labelStyle:"font-style:normal;",
            width:215,
            height:22,
            format:"Y-m-d",
            itemId:"RBACUser_AccBeginTime",
            msgTarget:"side",
            x:9,
            y:70,
            readOnly:false,
            appComID:"",
            value:me.dateNew,
            isFunctionBtn:false,
            hidden:false,
            sortNum:2,
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
            xtype:"datefield",
            name:"RBACUser_AccEndTime",
            fieldLabel:"账号截止日期",
            labelWidth:80,
            format:"Y-m-d",
            labelStyle:"font-style:normal;",
            width:215,
            height:22,
            msgTarget:"side",
            itemId:"RBACUser_AccEndTime",
            x:10,
            y:98,
            readOnly:false,
            appComID:"",
            treeNodeID:"",
            value:"",
            isFunctionBtn:false,
            hidden:false,
            sortNum:3,
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
            xtype:"radiogroup",
            itemId:"pwd",
            fieldLabel:"",
            columns:2,
            labelWidth:150,
            width:340,
            x:10,
            y:125,
            vertical:true,
            listeners:{
                change:function(com, The, eOpts) {
                    var pwd = com.getValue().pwd;
                    var RBACUserNew = me.getComponent("RBACUserNew");
                    var RBACUser_PWD = me.getComponent("RBACUser_PWD");
                    if (pwd == "true") {
                        RBACUser_PWD.setValue("");
                        RBACUserNew.setValue("");
                        RBACUser_PWD.show();
                        RBACUserNew.show();
                    } else {
                        RBACUser_PWD.hide();
                        RBACUserNew.hide();
                        RBACUser_PWD.setValue("123456");
                        RBACUserNew.setValue("123456");
                    }
                }
            },
            items:[ {
                boxLabel:"设置密码",
                name:"pwd",
                inputValue:"true",
                checked:true
            }, {
                boxLabel:"默认密码(123456)",
                name:"pwd",
                inputValue:"false"
            } ]
        }, {
            xtype:"textfield",
            name:"RBACUserNew",
            fieldLabel:"密码设置",
            labelWidth:80,
            msgTarget:"side",
            labelStyle:"font-style:normal;",
            width:215,
            emptyText:"请输入密码",
            height:22,
            itemId:"RBACUserNew",
            inputType:"password",
            x:10,
            y:150,
            readOnly:false,
            appComID:"",
            treeNodeID:"",
            value:"",
            isFunctionBtn:false,
            sortNum:4,
            allowBlank:false,
            id:"password",
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
            name:"RBACUser_PWD",
            fieldLabel:"密码验证",
            emptyText:"请再一次输入密码",
            labelWidth:80,
            showValidIcon:true,
            msgTarget:"side",
            labelStyle:"font-style:normal;",
            width:220,
            height:22,
            itemId:"RBACUser_PWD",
            x:10,
            y:180,
            inputType:"password",
            readOnly:false,
            appComID:"",
            treeNodeID:"",
            value:"",
            isFunctionBtn:false,
            sortNum:4,
            allowBlank:false,
            blankText:"不允许为空",
            vtype:"password",
            initialPassField:"password",
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
            xtype:"checkboxfield",
            name:"RBACUser_EnMPwd",
            boxLabel:"允许修改密码",
            labelWidth:80,
            fieldLabel:" ",
            labelSeparator:"",
            labelStyle:"font-style:normal;",
            width:220,
            height:22,
            itemId:"RBACUser_EnMPwd",
            x:3,
            y:205,
            readOnly:false,
            appComID:"",
            treeNodeID:"",
            value:"",
            checked:true,
            isFunctionBtn:false,
            hidden:false,
            sortNum:5,
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
            xtype:"combobox",
            name:"RBACUser_HREmployee_Id",
            fieldLabel:"主键ID",
            labelWidth:60,
            labelStyle:"font-style:normal;",
            width:160,
            itemId:"RBACUser_HREmployee_Id",
            x:348,
            y:80,
            readOnly:false,
            hidden:true,
            height:22,
            value:"",
            mode:"local",
            editable:false,
            displayField:"HREmployee_CName",
            valueField:"HREmployee_Id",
            store:new Ext.data.Store({
                fields:[ "HREmployee_CName", "HREmployee_Id", "HREmployee_DataTimeStamp" ],
                proxy:{
                    type:"ajax",
                    async:false,
                    url:getRootPath() + "/" + "RBACService.svc/RBAC_UDTO_SearchHREmployeeByHQL?isPlanish=true&where=&",
                    reader:{
                        type:"json",
                        root:"list"
                    },
                    extractResponseData:me.changeStoreData
                },
                autoLoad:true,
                listeners:{
                    load:function(s, records, successful) {
                        var combo = me.getComponent("RBACUser_HREmployee_Id");
                        var com = me.getComponent("RBACUser_HREmployee_DataTimeStamp");
                        if (com) {
                            var record = s.findRecord("HREmployee_Id", combo.getValue());
                            if (record != null && record != "") {
                                var value = record.get("HREmployee_DataTimeStamp");
                                com.setValue(value);
                            }
                        }
                    }
                }
            }),
            sortNum:11,
            listeners:{
                select:function(combo, records) {
                    var com = combo.ownerCt.getComponent("RBACUser_HREmployee_DataTimeStamp");
                    if (com) {
                        var value = records[0].get("HREmployee_DataTimeStamp");
                        com.setValue(value);
                    }
                },
                scope:this,
                specialkey:function(field, e) {
                    var iNum = 1;
                    var sNumField = "sortNum";
                    var form = field.ownerCt;
                    var num = field[sNumField];
                    var items = form.items.items;
                    var max = 12;
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
            y:5,
            readOnly:false,
            appComID:"",
            treeNodeID:"",
            value:"",
            isFunctionBtn:false,
            hidden:true,
            sortNum:1,
            listeners:{
                scope:this,
                specialkey:function(field, e) {
                    var iNum = 1;
                    var sNumField = "sortNum";
                    var form = field.ownerCt;
                    var num = field[sNumField];
                    var items = form.items.items;
                    var max = 12;
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
        }, {
            xtype:"button",
            text:"自动生成账号",
            itemId:"btn",
            x:93,
            y:43,
            width:120,
            height:22,
            sortNum:10,
            handler:function(btn, e, optes) {
                //自动生成账号不做校验
                var txtBJ = me.getComponent("txtBJ");
                var txtBJCD = me.getComponent("txtBJCD");
                var imageid = me.getComponent("yzimage");

                txtBJ.setVisible(false);
                txtBJCD.setVisible(false);
                imageid.setVisible(false);//true为显示，false为隐藏
                imageid.getEl().dom.src = "";


                var RBACUser_Account = me.getComponent("RBACUser_Account");
                var RBACUser_HREmployee_Id = me.getComponent("RBACUser_HREmployee_Id");
                var name = RBACUser_Account.getValue();
                var id = RBACUser_HREmployee_Id.getValue();
                me.CreateUserAccount(id, name);
                RBACUser_Account.setValue(me.AccountValue);
            }
        }, {
            xtype:"combobox",
            name:"RBACUser_LabID",
            fieldLabel:"实验室ID",
            labelWidth:60,
            labelStyle:"font-style:normal;",
            width:160,
            height:22,
            itemId:"RBACUser_LabID",
            isFunctionBtn:false,
            appComID:"",
            x:513,
            y:54,
            readOnly:false,
            hidden:true,
            value:"",
            readOnly:false,
            mode:"local",
            editable:false,
            displayField:"text",
            valueField:"value",
            store:new Ext.data.SimpleStore({
                fields:[ "value", "text" ],
                autoLoad:true,
                data:[ [ "0", "0" ], [ "1", "1" ] ]
            }),
            sortNum:5,
            listeners:{
                scope:this,
                specialkey:function(field, e) {
                    var iNum = 1;
                    var sNumField = "sortNum";
                    var form = field.ownerCt;
                    var num = field[sNumField];
                    var items = form.items.items;
                    var max = 12;
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
            name:"RBACUser_PwdExprd",
            fieldLabel:"密码永不过期",
            labelWidth:60,
            labelStyle:"font-style:normal;",
            width:160,
            height:22,
            hidden:true,
            itemId:"RBACUser_PwdExprd",
            x:0,
            y:0,
            readOnly:false,
            appComID:"",
            treeNodeID:"",
            value:"",
            isFunctionBtn:false,
            sortNum:5,
            listeners:{
                scope:this,
                specialkey:function(field, e) {
                    var iNum = 1;
                    var sNumField = "sortNum";
                    var form = field.ownerCt;
                    var num = field[sNumField];
                    var items = form.items.items;
                    var max = 26;
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
            xtype:"radiogroup",
            name:"RBACUser_AccLock",
            fieldLabel:"账号被锁定",
            labelWidth:60,
            labelStyle:"font-style:normal;",
            width:160,
            height:22,
            columnWidth:100,
            columns:2,
            itemId:"RBACUser_AccLock",
            x:175,
            y:57,
            readOnly:false,
            vertical:true,
            padding:2,
            autoScroll:true,
            hidden:true,
            isdataValue:true,
            items:[ {
                checked:false,
                name:"RBACUser_AccLock",
                inputValue:"true",
                boxLabel:"true"
            }, {
                checked:false,
                name:"RBACUser_AccLock",
                inputValue:"false",
                boxLabel:"false"
            } ],
            tempGroupName:"RBACUser_AccLock",
            sortNum:10,
            listeners:{
                scope:this,
                specialkey:function(field, e) {
                    var iNum = 1;
                    var sNumField = "sortNum";
                    var form = field.ownerCt;
                    var num = field[sNumField];
                    var items = form.items.items;
                    var max = 12;
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
        } ];
        if (me.type == "show") {
            me.height -= 25;
        } else {
            me.dockedItems = [ {
                xtype:"toolbar",
                dock:"bottom",
                itemId:"dockedItems-buttons",
                items:[ "->", {
                    xtype:"button",
                    text:"保存",
                    iconCls:"build-button-save",
                    handler:function() {
		           	    var Account=me.getComponent('RBACUser_Account').getValue();
			           	var AccBeginTime=me.getComponent('RBACUser_AccBeginTime').getValue();
	                    var AccBeginTimeFormat=''+ Ext.util.Format.date(AccBeginTime,'Y-m-d');
	                    var  BeginValue=convertJSONDateToJSDateObject(AccBeginTimeFormat);
	                    var AccEndTime=me.getComponent('RBACUser_AccEndTime').getValue();
	                    var EndValueFormat=''+ Ext.util.Format.date(AccEndTime,'Y-m-d');
	                    var  EndValue=convertJSONDateToJSDateObject(EndValueFormat);
                     
		           	    var RBACUserPWD=me.getComponent('RBACUser_PWD').getValue();
		           	    var RBACUserNew=me.getComponent('RBACUserNew').getValue();
		           	    if(RBACUserPWD==RBACUserNew){
		           		  var pwd=RBACUserPWD;
		           	    }
		                var EnMPwd=me.getComponent('RBACUser_EnMPwd').getValue();
		                var HREmployeeId=me.getComponent('RBACUser_HREmployee_Id').getValue();
		                var DataTimeStamp=me.getComponent('RBACUser_HREmployee_DataTimeStamp').getValue();
		                var LabID=1;
		                var Id=-1;
		                var PwdExprd=true;
		                var AccLock=false;
		                var objdata={RBACUser_Id:Id,RBACUser_Account:Account,RBACUser_EnMPwd:EnMPwd,
	                		 RBACUser_PWD:pwd,RBACUser_HREmployee_Id:HREmployeeId,RBACUser_LabID:LabID,
	                		 RBACUser_PwdExprd:PwdExprd,RBACUser_AccLock:AccLock,
	                		 RBACUser_AccBeginTime:BeginValue,RBACUser_AccEndTime:EndValue,
	                		 RBACUser_HREmployee_DataTimeStamp:DataTimeStamp
	                    };
                        if(RBACUserPWD.length>2 && RBACUserPWD.length<13)
                        { me.submit(objdata);}
                        else{
                            alert('请设置密码长度为3-12个字符');
                        }
		                //me.submit(objdata);
                    }
                }, {
                    xtype:"button",
                    text:"重置",
                    iconCls:"build-button-refresh",
                    handler:function() {
                        me.getForm().reset();
                    }
                } ]
            } ];
        }
        me.callParent(arguments);
    },
  
    afterRender:function() {
        var me = this;
        me.callParent(arguments);
        if (Ext.typeOf(me.callback) == "function") {
            me.callback(me);
        }
        if (me.type == "add") {
            me.isAdd();
        } else if (me.type == "edit") {
            me.isEdit(me.dataId);
        } else if (me.type == "show") {
            me.isShow(me.dataId);
        }
    },
    
    /**
     * 自动生成账号
     * @private
     */
    CreateUserAccount:function(id,name) {
        var me = this;
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            async:false,
            //非异步
            url:me.CreateUserAccountUrl+ '?id=' + id +'&'+'?strUserAccount=' + name,
            method:'GET',
            timeout:2000,
            success: function (response) {
                var result = Ext.JSON.decode(response.responseText);
                if (result.success) {
                    var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
                    var Account = ResultDataValue.UserAccount;
                    me.AccountValue=Account;
                }
            },
            failure:function(response, options) {
                Ext.Msg.alert('提示', '获取信息请求失败！');
            }
       });
    },
    //登录名验证
    validator: function (value) {
        var me = this;
        var validator = this;
        var error = true;
        Ext.Ajax.request({
            async:false,
            scope:validator,
            url:me.CheckUserNameUrl + "?strUserAccount=" + value,
            method:"GET",
            success:function(response) {
                var result = Ext.JSON.decode(response.responseText);
                var data = Ext.JSON.decode(result.ResultDataValue);
                var txtBJ = me.getComponent("txtBJ");
                var txtBJCD = me.getComponent("txtBJCD");
                var imageid = me.getComponent("yzimage");
                var RBACUser_Account = me.getComponent("RBACUser_Account");
                var Account = RBACUser_Account.getValue();
                if (data.result == "true") {
                    if (Account.length >= 3 && Account.length <= 12) {
                        txtBJ.setVisible(true);
                        txtBJCD.setVisible(false);
                        imageid.setVisible(false);
                        imageid.getEl().dom.src = "";
                    } else {
                        txtBJ.setVisible(false);
                        txtBJCD.setVisible(true);
                        imageid.setVisible(false);
                        imageid.getEl().dom.src = "";
                    }
                }
                if (data.result == "false") {
                    if (Account.length >= 3 && Account.length <= 12) {
                        txtBJ.setVisible(false);
                        txtBJCD.setVisible(false);
                        imageid.setVisible(true);
                        imageid.getEl().dom.src=getIconRootPathBySize(64) + "/"+ 'accept.png';
                    } else {
                        txtBJ.setVisible(false);
                        txtBJCD.setVisible(true);
                        imageid.setVisible(false);
                        imageid.getEl().dom.src = "";
                    }
                }
            },
            failure:function(response, options) {
                Ext.Msg.alert("提示", "获取信息请求失败！");
            }
        });
        return error;
    },
    /**
     * 打勾打叉
     * @private
     */
    OverrideField:function(){
    	var nn=Ext.layout.component.field.Field.override({
    	    getErrorStrategy:function() {
            var me = this, owner = me.owner, strategies = me.errorStrategies, msgTarget = owner.msgTarget;
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
     			var exp=/^[a-zA-Z0-9_]{3,13}$/;
				var reg = val.match(exp);
				if(reg==null){
					return false;
				}else{
					return true;
				}
     		},
    	isLenText: '3-12位不能重复的账号'	
      });
      return bb;
    }
  
});
