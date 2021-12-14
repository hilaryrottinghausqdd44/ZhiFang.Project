/**
 * 打印内容
 * @author Jcall
 * @version 2014-10-15
 */
Ext.define('Shell.ReportPrint.class.PrintContent', {
    extend: 'Shell.ux.panel.Panel',

    title: '打印内容',
    width: 400,
    height: 400,

    layout: 'fit',
    bodyPadding: 2,

    /**开启加载数据遮罩层*/
    hasLoadMask: true,
    /**加载数据提示*/
    loadingText: '数据加载中...',

    /**获取数据服务路径*/
    getImgeSrcUrl: '/ServiceWCF/ReportFormService.svc/PreviewReport',
    /**返回数据类型,1:报告;2:结果*/
    resultType: 1,

    /**数据条件对象*/
    serverParams: null,

    /**报告页签*/
    hasReportPage: false,
    /**结果页签*/
    hasResultPage: true,

    /**页面内容*/
    pageContent: { report: null, result: null },

    afterRender: function () {
        var me = this;
        me.disableControl();
        me.callParent(arguments);
    },
    initComponent: function () {
        var me = this;
        me.addEvents('typeChange');

        var radioDataArr = [];
        if (me.hasReportPage) radioDataArr.push({ text: '报告', value: 1 });
        if (me.hasResultPage) radioDataArr.push({ text: '结果', value: 2, checked: true });

        me.toolbars = [{
            dock: 'top', itemId: 'toptoolbar', buttons: ['->', {
                xtype: 'uxradiogroup', itemId: 'type', defaultSelect: 1, margin: '0 10 0 0',
                data: radioDataArr,
                listeners: { change: function () { me.changeContent(null, true); } }
            }]
        }];
        me.callParent(arguments);
    },
    /**获取返回数据类型*/
    getResultTypeValue: function () {
        var me = this,
			type = me.getComponent('toptoolbar').getComponent('type'),
			value = type.getValue(true);
        return value;
    },

    /**@public 更改内容*/
    changeContent: function (params, isPrivate) {
        var me = this,
			type = me.getResultTypeValue(),
			params = isPrivate ? me.serverParams : params;

        if (!isPrivate) me.pageContent = { report: null, result: null };

        var innerHTML = type == 1 ? me.pageContent.report : me.pageContent.result;
        if (innerHTML) {//已存在,不需要重新加载
            me.update(innerHTML);
            return;
        }

        if (!isPrivate && (!params || !params.ReportFormID || !params.SectionNo || !params.SectionType)) {
            var errorInfo = [];
            if (!params) {
                me.showError("Shell.ReportPrint.class.PrintContent的changeContent方法没有接收到参数对象!");
                return;
            }
            errorInfo.push("Shell.ReportPrint.class.PrintContent的changeContent方法接收的参数对象有错!");
            if (!params.ReportFormID) { errorInfo.push("<b style='color:red'>ReportFormID</b>参数错误!"); }
            if (!params.SectionNo) { errorInfo.push("<b style='color:red'>SectionNo</b>参数错误!"); }
            if (!params.SectionType) { errorInfo.push("<b style='color:red'>SectionType</b>参数错误!"); }

            me.showError(errorInfo.join("</br>"));
            return;
        }

        me.serverParams = params;

        me.getContent(me.serverParams, function (text) {
            var result = Ext.JSON.decode(text),
				html = "";

            if (result.success) {
                html = result.ResultDataValue;
            } else {
                html =
				'<div style="margin:20px 10px;color:red;text-align:center;">' +
					'<div><b style="font-size:16px;">错误信息</b></div>' +
					'<div>' + result.ErrorInfo + '</div>' +
				'</div>';
            }

            if (!html) html = '<div style="margin:20px 10px;text-align:center;"><b>没有数据</b></div>';

            me.update(html);

            me.pageContent[type == 1 ? "report" : "result"] = html;//储存页面信息

            me.enableControl();
            if (me.hasLoadMask) { me.body.unmask(); }//隐藏遮罩层
        });
    },
    /**根据ID获取内容*/
    getContent: function (params, callback) {
        var me = this,
			type = me.getResultTypeValue(),
			ModelType = (type == 1 ? "report" : "result");

        if (params.ReportFormID == null || params.SectionNo == null || params.SectionType == null) {
            me.showError("Shell.ReportPrint.class.PrintContent的getContent方法参数params的内容错误！");
            return;
        }

        if (ModelType == "report") {
            var RECEIVEDATE = Shell.util.Date.toString(params.RECEIVEDATE,true),
                src = Shell.util.Path.rootPath + '/' + Shell.util.Path.reportPath + '/' +
                    RECEIVEDATE + '/' + params.ReportFormID + '.pdf';

            var entity = {
                success: true,
                ResultDataValue:
                    '<iframe src="' + src + 
				        '" style="height:100%;width:100%;border:0;padding:0;margin:0;"' +
			        '></iframe>'
            };
            callback(Ext.JSON.encode(entity));
            return;
        }

        me.update("");
        if (me.hasLoadMask) { me.body.mask(me.loadingText); }//显示遮罩层

        var url = Shell.util.Path.rootPath + me.getImgeSrcUrl +
			"?ReportFormID=" + params.ReportFormID + "&SectionNo=" + params.SectionNo +
			"&SectionType=" + params.SectionType + "&ModelType=" + ModelType;

        me.disableControl();
        me.getToServer(url, callback);
    },

    /**开启功能栏*/
    enableControl: function () {
        this.disableControl(true);
    },
    /**禁用功能栏*/
    disableControl: function (bo) {
        var me = this,
			type = me.getComponent('toptoolbar').getComponent('type'),
			items = type.items.items,
			len = items.length;

        for (var i = 0; i < len; i++) {
            items[i][bo ? "enable" : "disable"]();
        }
    }
});