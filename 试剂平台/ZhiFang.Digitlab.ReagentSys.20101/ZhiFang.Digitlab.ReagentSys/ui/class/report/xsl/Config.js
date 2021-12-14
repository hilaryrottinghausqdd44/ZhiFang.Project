/**
 * 模板配置
 * @author Jcall
 * @version 2014-10-15
 */
Ext.define('Shell.class.report.xsl.Config', {
    extend:'Shell.grid.Panel',
    title:'模板配置',

    /**获取数据服务路径*/
    selectUrl:'/ServiceWCF/ReportFormService.svc/LoadConfig?fileName=ReportFromShowXslConfig.xml',
    /**保存数据服务路径*/
    saveUrl:'/ServiceWCF/ReportFormService.svc/SaveConfig',
    /**xml信息*/
    xmlConfig:null,
	
    plugins: Ext.create('Ext.grid.plugin.CellEditing', {
		clicksToEdit: 1
	}),
	/**斑马线效果 */
    stripeRows: true,
    
    /**是否启用刷新按钮*/
	hasRefresh:false,
	/**是否启用新增按钮*/
	hasAdd:false,
	/**是否启用修改按钮*/
	hasEdit:false,
	/**是否启用保存按钮*/
	hasSave:false,
    
    initComponent: function () {
        var me = this;
        
        //数据列
		me.columns = me.createGridColumns();
        me.callParent(arguments);
    },
    /**创建数据列*/
    createGridColumns: function () {
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
    /**@overwrite 改变返回的数据*/
	changeResult:function(data){
		data.list = data.rows;
		data.rows = null;
		return data;
	},
    /**保存数据*/
    onSaveClick: function () {
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
        me.showMask(me.saveText);//显示遮罩层
        JShell.Server.post(me.submitUrl, Ext.JSON.encode(entity), function (data) {
            me.hideMask();//隐藏遮罩层
            if(data.success){
                me.doLoad();
            } else {
                JShell.Msg.error(data.msg);
            }
        });
    }
});