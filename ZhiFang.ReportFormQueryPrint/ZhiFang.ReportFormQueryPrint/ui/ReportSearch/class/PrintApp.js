/**
 * 打印应用
 * @author Jcall
 * @version 2014-10-15
 */
Ext.define('Shell.ReportSearch.class.PrintApp',{
	extend:'Shell.ReportPrint.class.PrintApp',
	
	layout:{type:'border',regionWeights:{west:2,south:1}},
	
	defaultWhere: '',

    /**定义的接收参数,病历号、核收日期*/
	requestParamsArr: ['PATNO', 'RECEIVEDATE'],
    /**运算符,and/or*/
	operator: 'and',
    /**查看历史对比图*/
	showChart: false,
    /**默认每页数量*/
	defaultPageSize: 50,

    /**开启帮助文档*/
	help: false,
	/**报告页签*/
    hasReportPage: true,
    /**结果页签*/
    hasResultPage: true,
    /**默认勾选的页签,1=报告页签，2=结果页签*/
    defaultCheckedPage:1,

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
			className:'Shell.ReportPrint.class.PrintList',
			itemId:'PrintList',header:false,region: 'west',
			width: me.hasPrint ? 562 : 430,
			split:true,collapsible:true,
			hasPrint: me.hasPrint,
			defaultPageSize: me.defaultPageSize,
			defaultWhere:me.defaultWhere,
			defaultLoad: (me.defaultWhere ? true : false),

			columns: [
                { xtype: 'rownumberer', text: '序号', width: 50, align: 'center' },
			    { dataIndex: 'ReportFormID', text: '主键', hideable: false, hidden: true, type: 'key' },
			    {
			        dataIndex: 'RECEIVEDATE', text: '核收日期', width: 100, sortable: false, renderer: function (v, meta) {
			            var value = v ? Shell.util.Date.toString(v, true) : "";
			            //meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
			            return value;
			        }
			    },
			    { dataIndex: 'CNAME', text: '姓名', width: 60, sortable: false },
			    {
			        dataIndex: 'ItemName', text: '检验项目', width: 100, sortable: false,
			        renderer: function (value, meta, record) {
			            if (value) meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
			            return value;
			        }
			    },
                { dataIndex: 'SAMPLENO', text: '样本号', width: 100, sortable: false },
			    { dataIndex: 'SECTIONNO', text: '检验小组编号', hideable: false, hidden: true },
			    { dataIndex: 'CLIENTNO', text: '送检单位编码', hideable: false, hidden: true },
			    { dataIndex: 'SectionType', text: '小组类型', hideable: false, hidden: true }
			],
			
			toolbars:[{
				dock:'top',itemId:'toptoolbar',buttons:['refresh','-',
					{btype:'searchtext',text:'',tooltip:'',emptyText:'姓名/病历号',width:110}
				],
				searchFields: 'CNAME,PATNO',
				/**获取条件*/
				getWhere:function(value){
					var me = this,
						type = Ext.typeOf(me.searchFields),
						fields = type == 'string' ? me.searchFields.split(',') : type == 'array' ? me.searchFields : [],//查询的字段
						length = fields.length,
						combowhere = me.getComboWhere(),
				    	where = '';
				    	
				    if(!value && !combowhere) return;
				    
				    if(fields.length == 0 && !combowhere) return;
				    
				    if(value){
				    	for(var i=0;i<length;i++){
							where += fields[i] + "='" + value + "' or ";
						}
						where = where.slice(-4) == ' or ' ? where.slice(0,-4) : "";
				    }
				    
					if(combowhere){
						if(where){
							where = "(" + where + ") and (" + combowhere + ")";
						}else{
							where = combowhere;
						}
					}
					
					return where == "" ? "" : "(" + where + ")";
				}
			}]
		},{
			className:'Shell.ReportPrint.class.PrintContent',autoScroll:true,
			itemId: 'PrintContent', header: false, region: 'center',
		    hasReportPage: me.hasReportPage,
			hasResultPage: me.hasResultPage,
			defaultCheckedPage:me.defaultCheckedPage
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
		var me = this,
			array = me.getParamsArray(),
			len = array.length,
			params = Shell.util.Path.getRequestParams(true),
			pars = {},
			parCount = 0;

	    //参数转换处理
		params = me.changeParams(params);
			
		if (!me.hasPrint &&(!params["HASPRINT"] || params["HASPRINT"].toLocaleLowerCase() == "false")) {
			me.hasPrint = false;
		}
			
		for(var i in params){
			var bo = Shell.util.String.inArray(i,array);
			//参数必须存在于定义参数列表中,且参数值必须存在
			if(bo && (params[i] != null && params[i] != "")){
				pars[i] = params[i];
				parCount++;
			}
		}
		
		//校验定义的参数是否符合要求
		me.errorInfo = [];
		
		//if (!params["PATNO"]) {
		//	me.errorInfo.push("请将PATNO(病历号)传递到本程序!");
		//}
		
		//参数正确
		if(me.errorInfo.length == 0){
			me.defaultWhere = '';
			var where = [], RECEIVEDATEWhere = [];
			for(var i in pars){
				if(i == "RECEIVEDATE"){//核收时间
					var arr = pars[i].split(";");
					if(arr.length == 1){
					    RECEIVEDATEWhere.push(i + ">='" + arr[0] + "'");
					}else if(arr.length == 2){
						var da = [];
						if(arr[0]){da.push(i + ">='" + arr[0] + "'");}
						if(arr[1]){da.push(i + "<'" + Shell.util.Date.toString(Shell.util.Date.getNextDate(arr[1]),true) + "'");}
						RECEIVEDATEWhere.push(da.join(" and "));
					}
				}else{
					where.push(i + "='" + pars[i] + "'");
				}
			}
			//me.defaultWhere = where.join(" (" + me.operator + ") ");
			me.defaultWhere = where.join(" " + me.operator + " ");
			if(me.defaultWhere){me.defaultWhere = "(" + me.defaultWhere + ")"}
			if (RECEIVEDATEWhere.length > 0) {
			    if (me.defaultWhere) { me.defaultWhere += " and "; }
			    me.defaultWhere += RECEIVEDATEWhere.join(" and ");
			}
		}
	},
    //参数转换
	changeParams: function (params) {
	    return params;
	},
	initListeners:function(){
		var me = this;
			
		//存在错误,显示错误信息
		if(me.errorInfo.length > 0){
			me.showError(me.errorInfo.join("</br>"));
		}
	}
});