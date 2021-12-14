Ext.define('Company', {
    extend:'Ext.grid.Panel',
    alias:'widget.Company',
    //title:'外送单位',
    width:847,
    height:350,
    objectName:'BLaboratory',
    defaultWhere:'',
    internalWhere:'',
    externalWhere:'',
    autoSelect:true,
    deleteIndex:-1,
    autoScroll:true,
    sortableColumns:false,
    afterRender:function() {
        var me = this;
        me.callParent(arguments);
        me.store.on({
            load:function(store, records, successful) {
                var autoSelect = me.autoSelect;
                if (!successful) {
                    alertError('获取数据服务错误！');
                }
                if (successful && records.length > 0) {
                    var num = 0;
                    if (me.deleteIndex && me.deleteIndex != '' && me.deleteIndex != -1) {
                        num = records.length - 1 > me.deleteIndex ? me.deleteIndex :records.length - 1;
                    } else {
                        if (autoSelect) {
                            if (autoSelect === true) {
                                num = 0;
                            } else if (Ext.typeOf(autoSelect) === 'string' && autoSelect.length == 19) {
                                var index = store.find(me.objectName + '_Id', autoSelect);
                                if (index != -1) {
                                    num = index;
                                }
                            } else if (Ext.typeOf(autoSelect) === 'number') {
                                if (autoSelect >= 0) {
                                    num = autoSelect % records.length;
                                } else {
                                    num = length - Math.abs(num) % length;
                                }
                            }
                        }
                    }
                    me.deleteIndex = -1;
                    me.autoSelect = true;
                    me.getSelectionModel().select(num);
                }
            }
        });
        if (Ext.typeOf(me.callback) == 'function') {
            me.callback(me);
        }
    },
    initComponent:function() {
        var me = this;
        Ext.Loader.setPath('Ext.ux', getRootPath() + '/ui/extjs/ux');
        me.url = getRootPath() + '/SingleTableService.svc/ST_UDTO_SearchBLaboratoryByHQL?isPlanish=true&fields=BLaboratory_CName,BLaboratory_EName,BLaboratory_ShortCode,BLaboratory_IsUse,BLaboratory_Principal,BLaboratory_LinkMan,BLaboratory_PhoneNum1,BLaboratory_Address,BLaboratory_MailNo,BLaboratory_Emall,BLaboratory_PhoneNum2,BLaboratory_ClientType,BLaboratory_BmanNo,BLaboratory_ClientArea,BLaboratory_WebLisSourceOrgID,BLaboratory_GroupName,BLaboratory_Id,BLaboratory_DataTimeStamp,BLaboratory_Romark';
        me.store = Ext.create('Ext.data.Store', {
            fields:[ 'BLaboratory_CName', 'BLaboratory_EName', 'BLaboratory_ShortCode', 'BLaboratory_IsUse', 'BLaboratory_Principal', 'BLaboratory_LinkMan', 'BLaboratory_PhoneNum1', 'BLaboratory_Address', 'BLaboratory_MailNo', 'BLaboratory_Emall', 'BLaboratory_PhoneNum2', 'BLaboratory_ClientType', 'BLaboratory_BmanNo', 'BLaboratory_ClientArea', 'BLaboratory_WebLisSourceOrgID', 'BLaboratory_GroupName', 'BLaboratory_Id', 'BLaboratory_DataTimeStamp', 'BLaboratory_Romark' ],
            remoteSort:true,
            autoLoad:true,
            sorters:[],
            pageSize:1e4,
            proxy:{
                type:'ajax',
                url:getRootPath() + '/SingleTableService.svc/ST_UDTO_SearchBLaboratoryByHQL?isPlanish=true&fields=BLaboratory_CName,BLaboratory_EName,BLaboratory_ShortCode,BLaboratory_IsUse,BLaboratory_Principal,BLaboratory_LinkMan,BLaboratory_PhoneNum1,BLaboratory_Address,BLaboratory_MailNo,BLaboratory_Emall,BLaboratory_PhoneNum2,BLaboratory_ClientType,BLaboratory_BmanNo,BLaboratory_ClientArea,BLaboratory_WebLisSourceOrgID,BLaboratory_GroupName,BLaboratory_Id,BLaboratory_DataTimeStamp,BLaboratory_Romark',
                reader:{
                    type:'json',
                    root:'list',
                    totalProperty:'count'
                },
                extractResponseData:function(response) {
                    var data = Ext.JSON.decode(response.responseText);
                    if (!data.success) {
                        Ext.Msg.alert('提示', '错误信息:' + data.ErrorInfo);
                    }
                    if (data.ResultDataValue && data.ResultDataValue != '') {
                        data.ResultDataValue = data.ResultDataValue;//.replace(/[\\r\\n]+/g, '<br/>');
                        var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
                        data.list = ResultDataValue.list;
                        data.count = ResultDataValue.count;
                    } else {
                        data.list = [];
                        data.count = 0;
                    }
                    response.responseText = Ext.JSON.encode(data);
                    //me.setCount(data.count);
                    return response;
                }
            },
            listenres:{
                load:function(s, records, successful, eOpts) {
                    if (!successful) {
                        Ext.Msg.alert('提示', '获取数据服务错误！');
                    }
                }
            }
        });
        me.load = function(where) {
            if (where !== true) {
                me.externalWhere = where;
            }
            var w = '';
            if (me.externalWhere && me.externalWhere != '') {
                if (me.externalWhere.slice(-1) == '^') {
                    w += me.externalWhere;
                } else {
                    w += me.externalWhere + ' and ';
                }
            }
            if (me.defaultWhere && me.defaultWhere != '') {
                w += me.defaultWhere + ' and ';
            }
            if (me.internalWhere && me.internalWhere != '') {
                w += me.internalWhere + ' and ';
            }
            w = w.slice(-5) == ' and ' ? w.slice(0, -5) :w;
            me.store.currentPage = 1;
            me.store.proxy.url = me.url + '&where=' + w;
            me.store.load();
        };
        me.columns = [ {
            xtype:'rownumberer',
            text:'序号',
            width:35,
            align:'center'
        }, {
            text:'外送单位',
            dataIndex:'BLaboratory_CName',
            width:180,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        }, {
            text:'英文名称',
            dataIndex:'BLaboratory_EName',
            width:100,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        }, {
            text:'简称',
            dataIndex:'BLaboratory_ShortCode',
            width:100,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        }, {
            text:'是否使用',
            dataIndex:'BLaboratory_IsUse',
            width:100,
            xtype:'booleancolumn',
            trueText:'是',
            falseText:'否',
            defaultRenderer:function(value) {
                if (value === undefined) {
                    return this.undefinedText;
                }
                if (!value || value === 'false' || value === '0' || value === 0) {
                    return this.falseText;
                }
                return this.trueText;
            },
            sortable:false,
            hidden:false,
            hideable:true,
            align:'center'
        }, {
            text:'负责人',
            dataIndex:'BLaboratory_Principal',
            width:100,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        }, {
            text:'联系人',
            dataIndex:'BLaboratory_LinkMan',
            width:100,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        }, {
            text:'固定电话',
            dataIndex:'BLaboratory_PhoneNum1',
            width:100,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        }, {
            text:'地址',
            dataIndex:'BLaboratory_Address',
            width:100,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        }, {
            text:'邮编',
            dataIndex:'BLaboratory_MailNo',
            width:100,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        }, {
            text:'电子邮件',
            dataIndex:'BLaboratory_Emall',
            width:100,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        }, {
            text:'移动电话',
            dataIndex:'BLaboratory_PhoneNum2',
            width:100,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        }, {
            text:'ClientType',
            dataIndex:'BLaboratory_ClientType',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'业务员编码',
            dataIndex:'BLaboratory_BmanNo',
            width:100,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        }, {
            text:'区域',
            dataIndex:'BLaboratory_ClientArea',
            width:100,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        }, {
            text:'区域医疗机构编码',
            dataIndex:'BLaboratory_WebLisSourceOrgID',
            width:100,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        }, {
            text:'办事处',
            dataIndex:'BLaboratory_GroupName',
            width:100,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        }, {
            text:'主键ID',
            dataIndex:'BLaboratory_Id',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'时间戳',
            dataIndex:'BLaboratory_DataTimeStamp',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'备注',
            dataIndex:'BLaboratory_Romark',
            width:100,
            sortable:false,
            hidden:false,
            hideable:true,
            align:'left'
        } ];
        me.setCount = function(count) {
            var me = this;
            var com = me.getComponent('toolbar-bottom').getComponent('count');
            var str = '共' + count + '条';
            com.setText(str, false);
        };
/*        me.dockedItems = [ {
            xtype:'toolbar',
            dock:'bottom',
            itemId:'toolbar-bottom',
            items:[ {
                xtype:'label',
                itemId:'count',
                text:'共0条'
            } ]
        } ];*/
        me.deleteInfo = function(id, callback) {};
        me.fireEvent('saveClick');
        this.callParent(arguments);
    }
});