/**
 * 模板配置
 * @author Jcall
 * @version 2014-10-15
 */
Ext.define('Shell.Config.class.ReportFromShowXslConfig', {
    extend: 'Ext.grid.Panel',
    mixins: ['Shell.ux.server.Ajax'],

    title: '模板配置',

    /**获取数据服务路径*/
    selectUrl: Shell.util.Path.rootPath + '/ServiceWCF/ReportFormService.svc/LoadConfig?fileName=ReportFromShowXslConfig.xml',
    /**保存数据服务路径*/
    submitUrl: Shell.util.Path.rootPath + '/ServiceWCF/ReportFormService.svc/SaveConfig',
    /**xml信息*/
    xmlConfig:null,

    stripeRows: true, //斑马线效果  
    selType: 'cellmodel',
    plugins: [
        Ext.create('Ext.grid.plugin.CellEditing', {
            clicksToEdit: 1 //设置单击单元格编辑  
        })
    ],

    initComponent: function () {
        var me = this;
        me.store = me.createStore();
        me.columns = me.createColumns();
        me.dockedItems = me.createDockedItems();
        me.callParent(arguments);
    },
    /**
	 * 创建数据列
	 * @private
	 * @return {}
	 */
    createColumns: function () {
        var me = this;
        //小组类别,模板,页面名称,报告单
        var columns = [
            { dataIndex: 'ReportType', text: '小组类别', editor: { allowBlank: false } },
            { dataIndex: 'XSLName', text: '模板地址', editor: { allowBlank: false }, width: 200 },
            { dataIndex: 'PageName', text: '页面名称', editor: { allowBlank: false },width:200 },
            { dataIndex: 'Name', text: '样式名', editor: { allowBlank: false }, width: 200 },
            { xtype: 'actioncolumn',width: 50,align:'center',
                items: [{
                    iconCls: 'button-del',
                    tooltip: '删除本行数据',
                    handler: function (grid, rowIndex, colIndex) {
                        grid.store.removeAt(rowIndex);
                    }
                }]
            }
        ];

        return columns;
    },
    /**
	 * 创建数据集
	 * @private
	 * @return {}
	 */
    createStore: function () {
        var me = this;
        var config = {
            fields: ['ReportType', 'XSLName', 'PageName', 'Name'],
            proxy: {
                type: 'ajax',
                url: me.selectUrl,
                reader: { type: 'json', root: 'rows' },
                extractResponseData: function(response){
                    return me.extractResponseData(response);
                }
            },
            autoLoad: true
        };

        return Ext.create('Ext.data.Store', config);
    },
    /**数据转化*/
    extractResponseData:function(response){
        var me = this,
            result = Ext.JSON.decode(response.responseText);

        if (result.success) {
            var data = Ext.JSON.decode(result.ResultDataValue) || {};
            result.rows = data.DataSet.ReportFromShowXslConfig;
            result.ResultDataValue = null;
            me.xmlConfig = data["?xml"];
        } else {
            result.rows = [];
        }
        response.responseText = Ext.JSON.encode(result);

        me.enableControl();//启用所有的操作功能

        return response;
    },
    /**
	 * 创建挂靠
	 * @private
	 * @return {}
	 */
    createDockedItems: function () {
        var me = this;

        var dockedItems = [{
            xtype: 'toolbar',
            items: [
                { text: '刷新', iconCls: 'button-refresh', tooltip: '刷新数据', handler: function () { me.doLoad(); } },
                { text: '新增', iconCls: 'button-add', tooltip: '新增数据', handler: function () { me.doAdd(); } },
                { text: '保存', iconCls: 'button-save', tooltip: '保存数据', handler: function () { me.doSubmit(); } }
            ]
        }];

        return dockedItems;
    },
    /**加载数据*/
    doLoad: function () {
        var me = this;
        me.store.load();
        me.disableControl();//禁用所有的操作功能
    },
    /**新增一行数据*/
    doAdd: function () {
        this.store.add({});
    },
    /**保存数据*/
    doSubmit: function () {
        var me = this,
            records = me.store.data.items,
            len = records.length,
            list = [],
            hasNoData = false;
        
        for (var i = 0; i < len; i++) {
            var data = records[i].data;
            if (!data.ReportType || !data.XSLName || !data.PageName || !data.Name) {
                hasNoData = true;
                break;
            }
            list.push(data);
        }

        if (hasNoData) {
            Shell.util.Msg.showError("内容必须填写完整才能保存！");
            return;
        }

        var config = {
            "?xml": me.xmlConfig,
            "DataSet": { "ReportFromShowXslConfig": list }
        };
        var entity = {
            "fileName": "ReportFromShowXslConfig.xml",
            "configStr": Ext.JSON.encode(config)
        };
        me.body.mask("数据保存中...");//显示遮罩层
        me.disableControl();//禁用所有的操作功能
        me.postToServer(me.submitUrl, Ext.JSON.encode(entity), function (text) {
            me.body.unmask();//隐藏遮罩层
            me.enableControl();//启用所有的操作功能
            var result = Ext.JSON.decode(text);
            if(result.success){
                me.doLoad();
            } else {
                Shell.util.Msg.showError(result.ErrorInfo);
            }
        });
    },
    /**启用所有的操作功能*/
    enableControl: function (bo) {
        var me = this,
			enable = bo === false ? false : true,
			toolbars = me.dockedItems.items || [],
			length = toolbars.length,
			items = [];

        for (var i = 0; i < length; i++) {
            if (toolbars[i].xtype == 'header') continue;
            var fields = toolbars[i].items.items;
            items = items.concat(fields);
        }

        var iLength = items.length;
        for (var i = 0; i < iLength; i++) {
            items[i][enable ? 'enable' : 'disable']();
        }
        if (bo) { me.defaultLoad = true; }
    },
    /**禁用所有的操作功能*/
    disableControl: function () {
        this.enableControl(false);
    }
});