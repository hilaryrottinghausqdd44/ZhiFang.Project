/**
 * 测试内容
 * @author Jing
 * @version 2018-09-20
 */
Ext.define('Shell.class.debug.PrintContentTest', {
    extend: 'Ext.grid.Panel',
    width: 420,
    height: 320,
    NextIndex:1,
    fromId: '',
    viewConfig: {
        getRowClass: function (record, rowIndex, rowParams, store) {
            if (record.get('success')) {
                return "x-grid-record-printtrue";
            } else {
                return "x-grid-record-printfalse";
            }
        }
    },
    afterRender: function () {
        var me = this;
        me.disableControl();
        me.callParent(arguments);
    },
    initComponent: function () {
        var me = this;
        
        me.addEvents('typeChange');
        me.columns = me.createCollumns();
        me.store = me.createStore();
        me.dockedItems = [{
            dock: 'top',
            itemId: 'toptoolbar',
	    xtype:'toolbar',
 	    items:['->',{
		xtype: 'button',
            	text: '下一步',
		handler:function(){
		   var rec = me.store.getAt(0);
		   var flg = rec.get("ResultDataValue").indexOf("不重新生成");//. > 0;
                   if (me.fromId != '' && flg <= 0 && me.NextIndex < 4) {
                        Shell.util.Action.delay(me.GetNext(me.fromId, me.NextIndex + 1), null, 200);
                   }
		}
		}]
        }];
        me.callParent(arguments);
    },

    createStore:function(){
        var me = this;
        var store = Ext.data.SimpleStore({
            fields: ["success", "ResultDataValue"],
            data:[]
        });
        return store;
    },

    createCollumns:function(){
        var me = this;
        var colums = [{
            xtype: 'rownumberer',
            text: '序号',
            width: '8%',
            align: 'center'
        }, {
            text: '测试结果',
            dataIndex: 'success',
            width: '12%',
            //renderer: function (v, metaData) {
            //    if (!v) {
            //        metaData.style = 'background-color:red !important;';
            //        return v;
            //    }
            //    metaData.style = 'background-color:#00ff00 !important;';
            //    return v;
            //}
        }, {
            text: '结果',
            dataIndex: 'ResultDataValue',
            width: '80%',
            renderer: function (v, meta, record) {
                me.getComponent("toptoolbar").enable();
                meta.style = 'white-space:normal;background-color:#ffffff';//word-break:break-all;
                if (v.indexOf("模板查询条件") >= 0) {
                    var arr = v.split("。");
                    v="";
                    for (var i = 0; i < arr.length; i++) {
                        v += arr[i] + "<br>";
                    }
                }
                if (v.indexOf("报告生成成功") >= 0) {
                    var arr = v.split(":");
                    arr[1] = arr[1].substring(arr[1].lastIndexOf("/"));
                    me.getComponent("toptoolbar").disable();
                    return arr[0] + ": <a href='" + Shell.util.Path.rootPath + "/" + arr[1] + "' target='view_window'>" + arr[1] + "</a>"
                }
                return v;
            }
        }
        ];
        return colums;
    },
    /**开启功能栏*/
    enableControl: function () {
        this.disableControl(true);
    },
    /**禁用功能栏*/
    disableControl: function (bo) {
        var me = this;
		var type = me.getComponent('toptoolbar'),
			items = type,
			len = items.length;

        for (var i = 0; i < len; i++) {
            items[i][bo ? "enable" : "disable"]();
        }
    },

    GetNext: function (fromId, nextIndex) {
        var me = this;
        me.getComponent("toptoolbar").disable();
        if (nextIndex == 1) {
            me.store.removeAll();
        }
        me.NextIndex = nextIndex;
        me.fromId = fromId;
        var uri = Shell.util.Path.rootPath + "/ServiceWCF/ReportFormService.svc/GetReportFormPDFByReportFormIDTest";
        Ext.Ajax.request({
                url: uri,
                method: 'GET',
                params: {
                    ReportFormID: me.fromId,
                    nextIndex: me.NextIndex
                },
                success : function(response, options) {
                    var rs = Ext.JSON.decode(response.responseText);
                    if (rs.success) {
                        me.store.add([{ "success": rs.success, "ResultDataValue": rs.ResultDataValue }]);
                    } else {
                        me.store.add([{ "success": rs.success, "ResultDataValue": rs.ErrorInfo }]);
                    }
                }
         });
    }
});