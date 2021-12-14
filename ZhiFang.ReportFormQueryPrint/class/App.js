/**
 * 根据医生查询所有报告应用
 * @author Jcall
 * @version 2016-12-12
 */
Ext.define('Shell.ReportSearchDoctor.class.App',{
	extend:'Shell.ReportPrint.class.PrintApp',
	
	layout:{type:'border',regionWeights:{west:2,south:1}},
	
	defaultWhere: '',

    /**定义的接收参数*/
	requestParamsArr: [],
    /**运算符,and/or*/
	operator: 'and',
    /**查看历史对比图*/
	showChart: false,
    /**默认每页数量*/
	defaultPageSize: 50,

    /**开启帮助文档*/
	help: false,
    /**默认报告天数*/
	defaultDates: 1,
    /**医生编号映射字段*/
    DoctorNoField:null,
    /**报告日期字段*/
	ReportDateField:null,
    /**是否开启打印功能*/
	hasPrint: false,

    /**帮助按钮处理*/
	onHelpClick: function () {
	    var url = Shell.util.Path.uiPath + "/ReportSearch/help.html";
	    Shell.util.Win.openUrl(url, {
	        title: '使用说明'
	    });
	},
	afterRender: function () {
	    var me = this;
	    me.callParent(arguments);

	    var toptoolbar = me.getComponent('PrintList').getComponent('toptoolbar');
	    toptoolbar.on({ search: function (toolbar, com, where) { me.onSearch(where); } });
	},
	/**创建内部组件*/
	createApps:function(){
		var me = this;
		var apps = [{
			className:'Shell.ReportSearchDoctor.class.List',
			itemId:'PrintList',header:false,region: 'west',
			width: 440,
			split:true,collapsible:true,
			hasPrint: me.hasPrint,
			defaultPageSize: me.defaultPageSize,
			defaultWhere:me.defaultWhere,
			defaultDates:me.defaultDates,
			defaultOrderBy:me.defaultOrderBy,
			DoctorNoField:me.DoctorNoField,
			ReportDateField:me.ReportDateField
		},{
			className:'Shell.ReportPrint.class.PrintContent',autoScroll:true,
			itemId: 'PrintContent', header: false, region: 'center',
		    hasReportPage:me.hasPrint
		}];

        //历史对比图
		if (me.showChart) {
		    apps.push({
		        className: 'Shell.ReportPrint.class.PrintChart',
		        itemId: 'PrintChart', header: false,
		        height: 250, region: 'south', collapsed: true,
		        split: true, collapsible: true
		    });
		}
		
		return apps;
	},
	
	/**初始化列表的条件内容*/
	initListWhere:function(){
		return;
	},
    //参数转换
	changeParams: function (params) {
	    return params;
	},
	initListeners:function(){
		return;
	}
});