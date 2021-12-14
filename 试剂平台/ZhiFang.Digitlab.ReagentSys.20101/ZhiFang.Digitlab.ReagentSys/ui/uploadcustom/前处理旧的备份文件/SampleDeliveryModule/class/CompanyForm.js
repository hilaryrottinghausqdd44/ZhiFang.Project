Ext.define('frmCompany', {
    extend:'Ext.form.Panel',
    alias:'widget.frmCompany',
    title:'外送单位',
    width:761,
    height:216,
    itemId :'frmCompany',
    id:'frmCompany',
    isLoadingComplete:false,
    ParentID:0,
    LevelNum:1,
    TreeCatalog:1,
    ParentName:'',
    setdataTimeStampValue:true,
    header:true,
    objectName:'BLaboratory',
    isSuccessMsg:true,
    getAppInfoServerUrl:getRootPath() + '/' + 'ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById',
    addDataServerUrl:'SingleTableService.svc/ST_UDTO_AddBLaboratory',
    editDataServerUrl:'SingleTableService.svc/ST_UDTO_UpdateBLaboratoryByField',
    classCode:'BTDAppComponents_ClassCode',
    autoScroll:true,
    type:'show',
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
            var tempArr = itemId.split('_');
            var tempItemId = '';
            for (var i = 0; i < tempArr.length - 1; i++) {
                if (i < tempArr.length - 1) {
                    tempItemId = tempItemId + tempArr[i] + '_';
                }
            }
            if (me.setdataTimeStampValue && me.setdataTimeStampValue == true) {
                var dataTimeStampValue = '' + record.get('DataTimeStamp');
                tempItemId = tempItemId + 'DataTimeStamp';
                var dataTimeStampCom = me.getComponent(tempItemId);
                if (dataTimeStampCom) {
                    dataTimeStampCom.setValue(dataTimeStampValue);
                }
            }
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
            timeout:2e3,
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
                    me.openAppShowWin(title, ClassCode, com, textCom);
                } else {
                    Ext.Msg.alert('提示', '没有类代码！');
                }
            };
            me.getInfoByIdFormServer(appComID, callback);
        } else {
            Ext.Msg.alert('提示', '功能按钮没有绑定应用！');
        }
    },
    openAppShowWin:function(title, classCode, com, textCom) {
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
            draggable:true,
            Id:'',
            selectId:textCom.getValue()
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
                appList.close();
            }
        });
    },
    GetGroupItems:function(url2, valueField, displayField, groupName, defaultValue, dataTimeStampField) {
        var myUrl = url2;
        if (myUrl == '' || myUrl == null) {
            Ext.Msg.alert('提示', myUrl);
            return null;
        } else {
            myUrl = getRootPath() + myUrl;
        }
        var localData = [];
        Ext.Ajax.request({
            async:false,
            timeout:6e3,
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
                        var dataTimeStamp = ResultDataValue.list[i][dataTimeStampField];
                        if (arrStr.length > 0) {
                            mychecked = Ext.Array.contains(arrStr, DeptID);
                        }
                        var tempItem = {
                            checked:mychecked,
                            name:groupName,
                            boxLabel:CName,
                            inputValue:DeptID,
                            DataTimeStamp:'' + dataTimeStamp
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
        me.defaultTitle = me.title || '';
        me.addEvents('beforeSave');
        me.addEvents('saveClick');
        me.addEvents('functionBtnClick');
        me.addEvents('default1buttonClick');
        me.addEvents('default2buttonClick');
        me.addEvents('default3buttonClick');
        me.load = function(id) {
            me.isLoadingComplete = false;
            if (me.type == 'edit') {
                me.setTitle(me.defaultTitle + '-修改');
            } else if (me.type == 'show') {
                me.setTitle(me.defaultTitle + '-查看');
            } else {
                me.setTitle(me.defaultTitle);
            }
            Ext.Ajax.request({
                async:false,
                url:getRootPath() + '/SingleTableService.svc/ST_UDTO_SearchBLaboratoryById?isPlanish=true&fields=BLaboratory_CName,BLaboratory_ShortCode,BLaboratory_IsUse,BLaboratory_LinkMan,BLaboratory_PhoneNum1,BLaboratory_Address,BLaboratory_MailNo,BLaboratory_Emall,BLaboratory_Principal,BLaboratory_PhoneNum2,BLaboratory_Romark,BLaboratory_GroupName,BLaboratory_Id,BLaboratory_DataTimeStamp&id=' + (id ? id :-1),
                method:'GET',
                timeout:5e3,
                success:function(response, opts) {
                    var result = Ext.JSON.decode(response.responseText);
                    if (result.success) {
                        if (result.ResultDataValue && result.ResultDataValue != '') {
                            result.ResultDataValue = result.ResultDataValue;//.replace(/[\\r\\n]+/g, '');
                            if (me.type == 'add') {
                                me.type = 'edit';
                            }
                            var values = Ext.JSON.decode(result.ResultDataValue);
                            values = changeObj(values);
                            me.getForm().setValues(values);
                            var items = me.getForm().getFields().items;
                            for (var i in items) {
                                if (values[items[i].name]) {
                                    items[i].value = values[items[i].name];
                                }
                            }
                            me.isLoadingComplete = true;
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
            var bo = me.fireEvent('beforeSave', me);
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
            for (var i in values) {
                if (!values[i] || values[i] == '') {
                    delete values[i];
                }
            }
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
                    ob = ob + '[\'' + keyArr[i] + '\']';
                    if (Ext.typeOf(eval(ob)) === 'undefined') {
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
                    var tempArr = j.split('_');
                    var tempStr = tempArr[tempArr.length - 1];
                    if (tempStr == 'BudgetBuyTimeCount' || tempStr == 'Birthday' || tempStr == 'DataAddTime' || tempStr == 'DataUpdateTime' || tempStr == 'UpdateTime') {
                        value = convertJSONDateToJSDateObject(value);
                    }
                    if (tempStr == 'AuditTime' || tempStr == 'BargainTime' || tempStr == 'StatusChangeTime') {
                        value = convertJSONDateToJSDateObject(value);
                    }
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
                params:Ext.JSON.encode(params),
                method:'POST',
                timeout:5000,
                success:function(response, opts) {
                    var result = Ext.JSON.decode(response.responseText);
                    if (result.success) {
                        if (result.ResultDataValue && result.ResultDataValue != '') {
                            var key = me.objectName + '_Id';
                            var data = Ext.JSON.decode(result.ResultDataValue);
                            var id = data.id;
                            me.getForm().setValues([ {
                                id:key,
                                value:id
                            } ]);
                        }
                        //me.fireEvent('saveClick');
                        if (me.isSuccessMsg == true) {
                            me.fireEvent('saveClick');
                            //alert('me.fireEvent(saveClick)');
                            alertInfo('保存成功!');
                        }
                    } else {
                        alertError(action.result.ErrorInfo);
                    }
                },
                failure:function(response, options) {
                    alertError('保存请求失败!');
                }
            });
        };
        me.isAdd = function() {
            me.setTitle(me.defaultTitle + '-新增');
            me.getForm().reset();
            var buts = me.getComponent('dockedItems-buttons');
            if (buts) {
                if (me.type == 'show') {
                    me.setHeight(me.getHeight() + 25);
                }
                //buts.show();
                buts.hide();
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
                //buts.show();
                buts.hide();
            }
            me.type = 'edit';
            me.setReadOnly(false);
            if (id && id != -1 & id != '-1') {
                me.load(id);
            }
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
            if (id && id != -1 & id != '-1') {
                me.load(id);
            }
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
                    if (type == 'button' || type == 'label') {
                        items2[i].setDisabled(bo);
                    } else {
                        items2[i].setReadOnly(bo);
                    }
                }
            }
        };
        me.items = [ {
            xtype:'textfield',
            name:'BLaboratory_CName',
            fieldLabel:'名   称',
            labelWidth:60,
            labelStyle:'font-style:normal;',
            width:280,
            height:22,
            itemId:'BLaboratory_CName',
            x:5,
            y:4,
            readOnly:false,
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
                    var max = 14;
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
            labelAlign:'right'
        }, {
            xtype:'textfield',
            name:'BLaboratory_ShortCode',
            fieldLabel:'简  称',
            labelWidth:60,
            labelStyle:'font-style:normal;',
            width:280,
            height:22,
            itemId:'BLaboratory_ShortCode',
            x:6,
            y:30,
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
                    var max = 14;
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
            labelAlign:'right'
        }, {
            xtype:'checkboxfield',
            itemId:'BLaboratory_IsUse',
            x:802,
            y:55,
            name:'BLaboratory_IsUse',
            fieldLabel:'是否使用',
            labelWidth:60,
            labelStyle:'font-style:normal;',
            width:100,
            readOnly:false,
            height:22,
            boxLabel:'',
            inputValue:'true',
            uncheckedValue:'false',
            checked:'',
            sortNum:10,
            listeners:{
                scope:this,
                specialkey:function(field, e) {
                    var iNum = 1;
                    var sNumField = 'sortNum';
                    var form = field.ownerCt;
                    var num = field[sNumField];
                    var items = form.items.items;
                    var max = 14;
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
            labelAlign:'right'
        }, {
            xtype:'textfield',
            name:'BLaboratory_LinkMan',
            fieldLabel:'联系人',
            labelWidth:60,
            labelStyle:'font-style:normal;',
            width:280,
            height:22,
            itemId:'BLaboratory_LinkMan',
            x:298,
            y:30,
            readOnly:false,
            appComID:'',
            treeNodeID:'',
            value:'',
            isFunctionBtn:false,
            hidden:false,
            sortNum:5,
            listeners:{
                scope:this,
                specialkey:function(field, e) {
                    var iNum = 1;
                    var sNumField = 'sortNum';
                    var form = field.ownerCt;
                    var num = field[sNumField];
                    var items = form.items.items;
                    var max = 14;
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
            labelAlign:'right'
        }, {
            xtype:'textfield',
            name:'BLaboratory_PhoneNum1',
            fieldLabel:'固定电话',
            labelWidth:60,
            labelStyle:'font-style:normal;',
            width:280,
            height:22,
            itemId:'BLaboratory_PhoneNum1',
            x:6,
            y:55,
            readOnly:false,
            appComID:'',
            treeNodeID:'',
            value:'',
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
                    var max = 14;
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
            labelAlign:'right'
        }, {
            xtype:'textfield',
            name:'BLaboratory_Address',
            fieldLabel:'地址',
            labelWidth:60,
            labelStyle:'font-style:normal;',
            width:900,
            height:22,
            itemId:'BLaboratory_Address',
            x:6,
            y:90,
            readOnly:false,
            appComID:'',
            treeNodeID:'',
            value:'',
            isFunctionBtn:false,
            hidden:false,
            sortNum:11,
            listeners:{
                scope:this,
                specialkey:function(field, e) {
                    var iNum = 1;
                    var sNumField = 'sortNum';
                    var form = field.ownerCt;
                    var num = field[sNumField];
                    var items = form.items.items;
                    var max = 14;
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
            labelAlign:'right'
        }, {
            xtype:'textfield',
            name:'BLaboratory_MailNo',
            fieldLabel:'邮编号',
            labelWidth:60,
            labelStyle:'font-style:normal;',
            width:160,
            height:22,
            itemId:'BLaboratory_MailNo',
            x:612,
            y:55,
            readOnly:false,
            appComID:'',
            treeNodeID:'',
            value:'',
            isFunctionBtn:false,
            hidden:false,
            sortNum:9,
            listeners:{
                scope:this,
                specialkey:function(field, e) {
                    var iNum = 1;
                    var sNumField = 'sortNum';
                    var form = field.ownerCt;
                    var num = field[sNumField];
                    var items = form.items.items;
                    var max = 14;
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
            labelAlign:'right'
        }, {
            xtype:'textfield',
            name:'BLaboratory_Emall',
            fieldLabel:'电子邮件',
            labelWidth:60,
            labelStyle:'font-style:normal;',
            width:290,
            height:22,
            itemId:'BLaboratory_Emall',
            x:612,
            y:2,
            readOnly:false,
            appComID:'',
            treeNodeID:'',
            value:'',
            isFunctionBtn:false,
            hidden:false,
            sortNum:7,
            listeners:{
                scope:this,
                specialkey:function(field, e) {
                    var iNum = 1;
                    var sNumField = 'sortNum';
                    var form = field.ownerCt;
                    var num = field[sNumField];
                    var items = form.items.items;
                    var max = 14;
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
            labelAlign:'right'
        }, {
            xtype:'textfield',
            name:'BLaboratory_Principal',
            fieldLabel:'负责人',
            labelWidth:60,
            labelStyle:'font-style:normal;',
            width:280,
            height:22,
            itemId:'BLaboratory_Principal',
            x:298,
            y:4,
            readOnly:false,
            appComID:'',
            treeNodeID:'',
            value:'',
            isFunctionBtn:false,
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
                    var max = 14;
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
            labelAlign:'right'
        }, {
            xtype:'textfield',
            name:'BLaboratory_PhoneNum2',
            fieldLabel:'移动电话',
            labelWidth:60,
            labelStyle:'font-style:normal;',
            width:280,
            height:22,
            itemId:'BLaboratory_PhoneNum2',
            x:298,
            y:57,
            readOnly:false,
            appComID:'',
            treeNodeID:'',
            value:'',
            isFunctionBtn:false,
            hidden:false,
            sortNum:6,
            listeners:{
                scope:this,
                specialkey:function(field, e) {
                    var iNum = 1;
                    var sNumField = 'sortNum';
                    var form = field.ownerCt;
                    var num = field[sNumField];
                    var items = form.items.items;
                    var max = 14;
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
            labelAlign:'right'
        }, {
            xtype:'textfield',
            name:'BLaboratory_Romark',
            fieldLabel:'备注',
            labelWidth:60,
            labelStyle:'font-style:normal;',
            width:900,
            height:25,
            itemId:'BLaboratory_Romark',
            x:4,
            y:118,
            readOnly:false,
            appComID:'',
            treeNodeID:'',
            value:'',
            isFunctionBtn:false,
            hidden:false,
            sortNum:12,
            listeners:{
                scope:this,
                specialkey:function(field, e) {
                    var iNum = 1;
                    var sNumField = 'sortNum';
                    var form = field.ownerCt;
                    var num = field[sNumField];
                    var items = form.items.items;
                    var max = 14;
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
            labelAlign:'right'
        }, {
            xtype:'textfield',
            name:'BLaboratory_GroupName',
            fieldLabel:'办事处',
            labelWidth:60,
            labelStyle:'font-style:normal;',
            width:290,
            height:22,
            itemId:'BLaboratory_GroupName',
            x:612,
            y:30,
            readOnly:false,
            appComID:'',
            treeNodeID:'',
            value:'',
            isFunctionBtn:false,
            hidden:false,
            sortNum:8,
            listeners:{
                scope:this,
                specialkey:function(field, e) {
                    var iNum = 1;
                    var sNumField = 'sortNum';
                    var form = field.ownerCt;
                    var num = field[sNumField];
                    var items = form.items.items;
                    var max = 14;
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
            labelAlign:'right'
        }, {
            xtype:'textfield',
            name:'BLaboratory_Id',
            fieldLabel:'主键ID',
            labelWidth:60,
            labelStyle:'font-style:normal;',
            width:160,
            height:22,
            itemId:'BLaboratory_Id',
            x:5,
            y:109,
            readOnly:false,
            appComID:'',
            treeNodeID:'',
            value:'',
            isFunctionBtn:false,
            hidden:true,
            sortNum:13,
            listeners:{
                scope:this,
                specialkey:function(field, e) {
                    var iNum = 1;
                    var sNumField = 'sortNum';
                    var form = field.ownerCt;
                    var num = field[sNumField];
                    var items = form.items.items;
                    var max = 14;
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
            labelAlign:'right'
        }, {
            xtype:'textfield',
            name:'BLaboratory_DataTimeStamp',
            fieldLabel:'时间戳',
            labelWidth:60,
            labelStyle:'font-style:normal;',
            width:160,
            height:22,
            itemId:'BLaboratory_DataTimeStamp',
            x:175,
            y:109,
            readOnly:false,
            appComID:'',
            treeNodeID:'',
            value:'',
            isFunctionBtn:false,
            hidden:true,
            sortNum:14,
            listeners:{
                scope:this,
                specialkey:function(field, e) {
                    var iNum = 1;
                    var sNumField = 'sortNum';
                    var form = field.ownerCt;
                    var num = field[sNumField];
                    var items = form.items.items;
                    var max = 14;
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
            labelAlign:'right'
        } ];
        if (me.type == 'show') {
            me.dockedItems = [ {
                xtype:'toolbar',
                dock:'bottom',
                hidden:true,  //隐藏表单工具栏
                itemId:'dockedItems-buttons',
                items:[ '->', {
                    xtype:'button',
                    text:'保存',
                    iconCls:'build-button-save',
                    handler:function(but) {
                        me.submit();
                    }
                }, {
                    xtype:'button',
                    text:'重置',
                    iconCls:'build-button-refresh',
                    handler:function(but) {
                        me.getForm().reset();
                    }
                } ]
            } ];
            me.height -= 25;
        } else {
            me.dockedItems = [ {
                xtype:'toolbar',
                dock:'bottom',
                hidden:true,  //隐藏表单工具栏
                itemId:'dockedItems-buttons',
                items:[ '->', {
                    xtype:'button',
                    text:'保存',
                    iconCls:'build-button-save',
                    handler:function(but) {
                        me.submit();
                    }
                }, {
                    xtype:'button',
                    text:'重置',
                    iconCls:'build-button-refresh',
                    handler:function(but) {
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
        if (Ext.typeOf(me.callback) == 'function') {
            me.callback(me);
        }
        if (me.type == 'add') {
            me.isAdd();
        } else if (me.type == 'edit') {
            me.isEdit(me.dataId);
        } else if (me.type == 'show') {
            me.isShow(me.dataId);
            var buts = me.getComponent('dockedItems-buttons');
            if (buts) {
                buts.hide();
            }
        }
    }
});