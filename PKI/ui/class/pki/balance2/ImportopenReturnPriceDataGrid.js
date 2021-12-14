/**
 * 批量返差价导入
 * @author liangyl	
 * @version 2017-04-17
 */
Ext.define('Shell.class.pki.balance2.ImportopenReturnPriceDataGrid', {
    extend: 'Shell.ux.grid.PostPanel',

    title: '批量返差价导入',

    width: 1000,
    height: 600,

    /**带功能按钮栏*/
    hasButtontoolbar: true,
    /**默认不加载*/
    defaultLoad: false,
    defaultPageSize: 10000,
    defaultDisableControl: false,
    defaultOrderBy: [{
        property: '存在状态',
        direction: 'DESC'
    }],
    plugins: Ext.create('Ext.grid.plugin.CellEditing', {
        clicksToEdit: 1
    }),
    hasDel: true,
    /**删除标志字段*/
    DelField: 'delState',
    datas: null,
    /**带分页栏*/
    hasPagingtoolbar: false,
    /**查询栏参数设置*/
    searchToolbarConfig: {},
    checkOne: false,
    saveText: "批量返差价导入中...",
    selectUrl: '/StatService.svc/Stat_UDTO_ImportPersonalInvoiceData',
    editUrl: '/StatService.svc/Stat_UDTO_EditPersonalSpread',
    afterRender: function () {
        var me = this;
        me.callParent(arguments);
        //创建数据集
        if (me.datas && me.datas != null) {
            me.store.loadData(me.datas);
        }
        me.on({
            beforeedit: function (editor, e) {

            }
        });
    },

    initComponent: function () {
        var me = this;
        me.addEvents('save', me);
        me.editUrl = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
        //自定义按钮功能栏
        me.buttonToolbarItems = [{
            text: '确定返差价批量导入',
            iconCls: 'button-text-person',
            tooltip: '<b>返差价批量导入</b>',
            handler: function () {
                me.onSaveClick();
            }
        }]; //, '-', 'save'
        if (me.checkOne == false) {
            //复选框
            me.multiSelect = true;
            me.selType = 'checkboxmodel';
        }
        //数据列
        me.columns = me.createGridColumns();
        me.callParent(arguments);
    },
    /**创建数据列*/
    createGridColumns: function () {
        var me = this;
        var columns = [{
            dataIndex: 'ID',
            text: 'ID',
            hidden: true,
            hideable: false,
            isKey: true
        }, {
            dataIndex: '存在状态',
            text: '存在状态',
            width: 70,
            renderer: function (value, meta, record, rowIndex, colIndex, store, view) {
                var v = value || '';
                var color = "#FFFFFF";
                switch (v) {
                    case "否":
                        color = "red";
                        break;
                    case "是":
                        color = "green";
                        break;
                    default:
                        color = "#FFFFFF";
                        break;
                }
                meta.style = 'background-color:' + color;
                return v;
            }
        }, {
            dataIndex: '导入状态',
            text: '导入状态',
            width: 70,
            renderer: function (value, meta, record, rowIndex, colIndex, store, view) {
                var v = value || '未导入';
                if (v == "") {
                    v = "未导入";
                }
                var color = "#FFFFFF";
                switch (v) {
                    case "导入失败":
                        color = "red";
                        break;
                    case "导入成功":
                        color = "green";
                        break;
                    default:
                        color = "#FFFFFF";
                        break;
                }
                meta.style = 'background-color:' + color;
                return v;
            }
        }, {
            dataIndex: '财务锁定状态',
            text: '财务锁定状态',
            width: 80,
            renderer: function (value, meta) {
                var v = JShell.PKI.Enum.IsFinanceLocked['E' + value] || '';
                if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
                meta.style = 'background-color:' + JcallShell.PKI.Enum.IsFinanceLockedColor['E' + value] || '#FFFFFF';
                return v;
            }
        }, {
            dataIndex: '是否返差价',
            text: '是否返差价',
            width: 70,
//          isBool: true,
//			align: 'center',
//			type: 'bool'
            renderer: function (value, meta, record, rowIndex, colIndex, store, view) {
                var v = value || '';
                var color = "#FFFFFF";
                switch (v) {
                    case "0":
                        color = "red";
                        v='否';
                        break;
                    case "1":
                        color = "green";
                         v='是';
                        break;
                    default:
                        color = "#FFFFFF";
                        break;
                }
                meta.style = 'background-color:' + color;
                return v;
            }
        }, {
            dataIndex: '姓名(系统)',
            text: '姓名(系统)',
            width: 100,
            defaultRenderer: true
        }, {
            dataIndex: '姓名',
            text: '姓名(导入)',
            width: 100,
            defaultRenderer: true
        }, {
            dataIndex: '采样日期',
            text: '采样日期',
            width: 90,
            defaultRenderer: true
        }, {
            dataIndex: '送检医院',
            text: '送检医院',
            width: 120,
            defaultRenderer: true
        }, {
            dataIndex: '实验室条码',
            text: '实验室条码',
            defaultRenderer: true
        }, {
            dataIndex: '医嘱项目',
            text: '医嘱项目',
            width: 125,
            defaultRenderer: true
        }, {
            dataIndex: '返差价备注',
            text: '返差价备注',
            width: 115,
            defaultRenderer: true
        }, {
            dataIndex: '个人开票名称',
            text: '个人开票名称',
            width: 85,
            defaultRenderer: true
        }, {
            dataIndex: '开票金额',
            text: '开票金额',
            width: 75,
            defaultRenderer: true
        }, {
            dataIndex: '收件人',
            text: '收件人',
            width: 85,
            defaultRenderer: true
        }, {
            dataIndex: '收件人电话',
            text: '收件人电话',
            width: 85,
            defaultRenderer: true
        }, {
            dataIndex: '寄送地址',
            text: '寄送地址',
            width: 85,
            defaultRenderer: true
        }, {
            dataIndex: '付款信息',
            text: '付款信息',
            width: 85,
            defaultRenderer: true
        }, {
            dataIndex: '备注',
            text: '备注',
            width: 85,
            defaultRenderer: true
        }];

        return columns;
    },
    /**保存数据*/
    onSaveClick: function () {
        var me = this,
			records = me.getSelectionModel().getSelection();
			
        if (records.length == 0) {
            JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
            return;
        }

        var inputArr = [];
        var isExec = true;
        for (var i = 0; i < records.length; i++) {
            var rec = records[i];
            var id = rec.get("ID");
            isExec = true;
//            未返差价和财务锁定的样本、存在状态为是 的能
            var IsFinanceLocked = rec.get("财务锁定状态");
            var IsLocked = rec.get("是否返差价"); 
            var IsExist = rec.get("存在状态");
            if (id == "" || id == "-1") {
                isExec = false;
            }
            if (isExec && IsFinanceLocked != "1") {
                isExec = false;
            }
            if(isExec && IsExist=="否"){
            	 isExec = false;
            }
            if (isExec && (IsLocked == "1" || IsLocked == 1)) {
                isExec = false;
            }
            if (isExec) {
                inputArr.push(rec);
            } else {
                break;
            }
        }
        if (isExec == false) {
            JShell.Msg.error("当前选择的样本在财务系统中不存在<br />或存在为已返差价已导入样本");
            return;
        }

        //me.showMask(me.saveText); //显示遮罩层
        me.saveErrorCount = 0;
        me.saveCount = 0;
        me.saveLength = records.length;

        var params = {
            idList: ''
        };

        me.showMask(me.saveText); //显示遮罩层
        for (var i = 0; i < records.length; i++) {
            var rec = records[i];
            var id = rec.get("ID");
            var isExec = true;
            // 未返差价和财务锁定的样本、存在状态为是 的能
            var IsFinanceLocked = rec.get("财务锁定状态");
            var IsLocked = rec.get("是否返差价"); 
            var IsExist = rec.get("存在状态");
            if (id == "" || id == "-1") {
                isExec = false;
            }
            if (isExec && IsFinanceLocked != "1") {
                isExec = false;
            }
            if (isExec && (IsLocked == "1" || IsLocked == 1)) {
                isExec = false;
            }
             if(isExec && IsExist=="否"){
            	 isExec = false;
            }
            if (isExec) {
                params.idList = id;
                params.spreadMemo = rec.get('返差价备注');
                params.isSpread = 1;
                me.editOneById(id, params);
            }
        }

        //me.hideMask(); //隐藏遮罩层
    },
    /**单行记录导入*/
    editOneById: function (id, params) {
        var me = this;
        var params = Ext.JSON.encode(params);
        JShell.Server.post(me.editUrl, params, function (data) {
            var record = me.store.findRecord("ID", id);
            if (data.success) {
                if (record) {
                    record.set(me.DelField, true);
                    record.set("存在状态", "是");
                    record.set("财务锁定状态", "1"); //账务锁定
                    record.set("是否返差价", "是"); //个人开票
                    record.set("导入状态", "导入成功");
                    record.commit();
                }
                me.saveCount++;
            } else {
                me.saveErrorCount++;
                if (record) {
                    record.set(me.DelField, false);
                    record.set("导入状态", "导入失败");
                    record.commit();
                }
            }
            if (me.saveCount + me.saveErrorCount == me.saveLength) {
                me.fireEvent('save', me);
                me.hideMask(); //隐藏遮罩层
                if (me.saveErrorCount == 0) {
                    this.close();
                };
            }
        }, false);
    }
});