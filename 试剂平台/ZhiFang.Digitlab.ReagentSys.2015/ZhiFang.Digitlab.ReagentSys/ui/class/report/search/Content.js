/**
 * 报告内容
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.report.search.Content', {
	extend: 'Shell.ux.panel.AppPanel',
	
	Shell_class_report_search_Content:{
    	title:{
			TEXT:'报告内容'
		},
		radio:{
			REPORT:'报告',
			RESULT:'结果'
		}
    },
	
	width: 400,
	height: 400,

	layout: 'fit',
	bodyPadding: 2,
	autoScroll:true,

	/**获取数据服务路径*/
	selectUrl: '/ServiceWCF/ReportFormService.svc/PreviewReportExtPageName',
	/**数据条件对象*/
	serverParams: null,
	/**页面内容*/
	pageContent: {
		report: null,
		result: null
	},

	/**开启加载数据遮罩层*/
	hasLoadMask: true,
	/**返回数据类型,1:报告;2:结果*/
	pageType: 1,
	/**报告页签*/
	hasReportPage: true,
	/**结果页签*/
	hasResultPage: true,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		
		//替换语言包
		me.changeLangage('Shell.class.report.search.Content');
		//标题
		me.title = me.Shell_class_report_search_Content.title.TEXT;
		
		me.addEvents('typeChange');
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	createDockedItems: function() {
		var me = this,
			items = [],
			dockedItems = [];

		if (me.hasReportPage) {
			items.push({
				boxLabel: me.Shell_class_report_search_Content.radio.REPORT,
				name: 'pageType',
				inputValue: 1,
				//margin: '0 5 4 0',
				checked: me.pageType == 1 ? true : false
			});
		}
		if (me.hasResultPage) {
			items.push({
				boxLabel: me.Shell_class_report_search_Content.radio.RESULT,
				name: 'pageType',
				inputValue: 2,
				//margin: '0 5 4 0',
				checked: me.pageType == 2 ? true : false
			});
		}

		dockedItems = [{
			xtype: 'toolbar',
			dock: 'top',
			itemId: 'toptoolbar',
			items: ['->', {
				xtype: 'radiogroup',
				itemId:'type',
				columns: 2,
				width:130,
				vertical: true,
				items: items,
				margin: '0 5',
				listeners: {
					change: function(field, newValue, oldValue) {
						me.pageType = newValue.pageType;
						me.onPageTypeChange(true);
					}
				}
			}]
		}];

		return dockedItems;
	},
	/**页面类型变化*/
	onPageTypeChange: function(isPrivate) {
		var me = this;

		if (me.pageType == 1) {
			me.showReport(isPrivate);
		} else if (me.pageType == 2) {
			me.showResult(isPrivate);
		}
	},
	/**显示报告*/
	showReport: function(isPrivate) {
		var me = this;
		//已存在,不需要重新加载
		if (isPrivate && me.pageContent.report) {
			me.update(me.pageContent.report);
			return;
		}

		var RECEIVEDATE = JShell.Date.toString(me.serverParams.RECEIVEDATE, true);
		var src = JShell.System.Path.ROOT + '/' + JShell.PRI.System.Path.REPORT +
			'/' + RECEIVEDATE + '/' + me.serverParams.ReportFormID + '.pdf';

		me.pageContent.report =
			'<iframe src="' + src +
			'" style="height:100%;width:100%;border:0;padding:0;margin:0;"' +
			'></iframe>';

		me.update(me.pageContent.report);
	},
	/**显示结果*/
	showResult: function(isPrivate) {
		var me = this;
		//已存在,不需要重新加载
		if (isPrivate && me.pageContent.result) {
			me.update(me.pageContent.result);
			//默认选中数据
			me.doAutoSelect();
			return;
		}

		//me.update("");

		if (me.hasLoadMask) {
			//显示遮罩层
			me.body.mask(JShell.Server.LOADING_TEXT);
		}

		var url = JShell.System.Path.ROOT + me.selectUrl +
			"?ReportFormID=" + me.serverParams.ReportFormID +
			"&SectionNo=" + me.serverParams.SectionNo +
			"&SectionType=" + me.serverParams.SectionType +
			"&ModelType=result";
			
		//返回不同的语言模板,默认中文
		if(JShell.System.Lang && JShell.System.Lang.toLocaleUpperCase() != 'CN'){
			url += '&PageName=' + JShell.System.Lang.toLocaleUpperCase();
		}

		JShell.Server.get(url, function(text) {
			var result = Ext.JSON.decode(text),
				html = "";

			if (result.success) {
				html = result.ResultDataValue;
				//处理带图模板的图片
				html = me.changeData(html);
			} else {
				html =
					'<div style="margin:20px 10px;color:red;text-align:center;">' +
					'<div><b style="font-size:16px;">' + JShell.Msg.ERROR_TITLE + '</b></div>' +
					'<div>' + result.ErrorInfo + '</div>' +
					'</div>';
			}

			if (!html) {
				html = '<div style="margin:20px 10px;text-align:center;"><b>' + 
					JShell.Server.NO_DATA + '</b></div>';
			}

			me.pageContent.result = html;

			me.update(html);
			
			//默认选中数据
			me.doAutoSelect();

			if (me.hasLoadMask) {
				//隐藏遮罩层
				me.body.unmask();
			}

		}, null, null, true);
	},
    /**处理带图模板的图片*/
    changeData:function(data){
    	var d = data;
    	
    	d = d.replace(/<img>/g,'<img src="data:image/gif;base64,');
    	d = d.replace(/<\/img>/g,'"/>');
    	
    	return d;
    },
	/**修改报告内容*/
	changeContent: function(params) {
		var me = this;
		me.serverParams = params;
		me.enableControl();
		me.onPageTypeChange();
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
    },
    /**清空数据*/
    clearData:function(){
    	var me = this;
    	me.pageContent.report = null;
    	me.pageContent.result = null;
		me.disableControl();
		me.update("");
    },
    /**默认选中数据*/
    doAutoSelect:function(){
    	setTimeout(function(){
			var id = document.getElementById('tmptrid').value;
			if(id){
				document.getElementById(id).click();
			}
		},100);
    }
});