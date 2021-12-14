/**
 * 打印列表
 * @author Jcall
 * @version 2014-10-15
 */
Ext.define('Shell.ReportPrint.class.PrintList',{
    extend: 'Shell.ux.panel.Grid',

    requires: [
		'Shell.ux.form.field.ComboBox'
    ],
	
	title: '打印列表',
    id:'ext_print_list',
	width:400,
	height:300,
	multiSelect:false,
	defaultPageSize:50,
	remoteSort:false,
	pagingtoolbar:'simple',
	
	/**获取数据服务路径*/
	selectUrl: '/ServiceWCF/ReportFormService.svc/SelectReport?fields=ReportFormID,FormNo,' +
		'SAMPLENO,SECTIONNO,CNAME,CLIENTNO,SectionType,RECEIVEDATE,PRINTTIMES,ItemName,PATNO,Serialno',
	/**打印服务路径*///PrintType=A4?每一次都按勾选的纸张传递？
	printUrl:'/ServiceWCF/ReportFormService.svc/PrintReport',
	/**增加打印次数服务路径*/
	addPrintTimesUrl:Shell.util.Path.rootPath + '/ServiceWCF/ReportFormService.svc/ReportFormAddPrintTimes',
	
	/**当前打印ID*/
	printId:null,
	/**打印错误信息数组*/
	errorInfo:[],
	/**打印动作*/
	printAction:null,
	/**需要标识打印的数据ID*/
	formIds:null,
	/**开启单元格内容提示*/
	tooltip:false,//兼容移动端，避免点击两次才触发点击事件
	/**是否开启打印功能*/
	hasPrint: true,

    /**A4纸张类型，1(A4) 2(16开)*/
	A4Type: 1,
    /**默认勾选过滤框*/
	checkFilter: false,
    /**默认打印类型*/
	printType: 'A4',
    /**默认顺序*/
    defaultOrderBy:[],
	
	afterRender: function () {
	    var me = this;
	    if (me.defaultLoad) {
	        me.internalWhere = me.getInternalWhere();
	    }
	    
	    me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		//打印页面回调的获取信息方法
		Shell.util.Function.getReportPrintInfo = me.getReportPrintInfo;
		//打印页面回调的增加打印次数方法
		Shell.util.Function.addPrintTimes = me.addPrintTimes;

	    //getRowClass：更改行样式
		me.viewConfig = { getRowClass: me.changeRowClass };
		
		//功能按钮栏
		var buttons = [];
		if (me.hasPrint) {
		    buttons.push({
		        xtype: 'checkbox', itemId: 'upprint', boxLabel: '未打印&nbsp;', checked: me.checkFilter,
		        handler: function () {
		            me.onSearch();
		        }
		    });
		    buttons.push({
		        xtype: 'uxcombobox', itemId: 'dept', dataSize: 1000, width: 120, editable: true,
		        displayField: 'CName', valueField: 'DeptNo', selectFirst: true, enableKeyEvents: true,
		        firstData: [{ CName: '所有科室', DeptNo: 0, ShortCode: 'ALL' }],
		        url: '/ServiceWCF/DictionaryService.svc/GetDeptList?fields=DeptNo,CName&WHERE=',
		        tpl: '<tpl for="."><li role="option" class="x-boundlist-item">{CName}[<b style="color:blue;">{ShortCode}</b>]</li></tpl>',
		        storeConfig: {
		            fields: ['CName', 'DeptNo', 'ShortCode']
		        },
		        listeners: {
		            beforequery: function (e) {
		                var combo = e.combo;
		                if (!e.forceAll) {
		                    var value = e.query.toLocaleUpperCase();
		                    combo.store.filterBy(function (record, id) {
		                        var text = record.get(combo.displayField).toLocaleUpperCase();
		                        var shortCode = record.get('ShortCode').toLocaleUpperCase();
		                        return (text.indexOf(value) != -1 || shortCode.indexOf(value) != -1);
		                    });
		                    combo.expand();
		                    return false;
		                }
		            },
		            keypress: function (f, e) {
		                if (e.keyCode == e.ENTER) {
		                    me.onSearch();
		                }
		            }
		        }
		    });
			buttons.push('-');
			buttons.push('print');
			
			//buttons.push({xtype:'checkbox',itemId:'preview',boxLabel:'预览',handler:function(){}});
			buttons.push({
			    xtype: 'checkbox', itemId: 'hasFilter', boxLabel: '过滤', checked:me.checkFilter,
			    handler: function () { }
			});
			
			buttons.push('-');
			buttons.push({
				xtype:'combobox',itemId:'strPageName',
				mode:'local',editable:false,
	            displayField:'text',valueField:'value',
	            width: 60, value: me.printType,
				store:new Ext.data.SimpleStore({
					fields:['text','value'],
					data:[['A4','A4'],['A5','A5'],['双A5','双A5']]
				})
			});
			me.multiSelect = true;//取消复选框

		}
		buttons.push('->');
		buttons.push({xtype:'uxbutton',itemId:'collapse',text:'',iconCls:'button-left',tooltip:'<b>收缩面板</b>'});
		
		me.toolbars = me.toolbars || [{
		    dock: 'top', itemId: 'toptoolbar', buttons: ['refresh']
		}];
		me.toolbars[0].buttons = me.toolbars[0].buttons.concat(buttons);
		
	    //数据列信息
		me.columns = me.columns || [
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
			{ dataIndex: 'SAMPLENO', text: '样本号', width: 100, sortable: false },
            { dataIndex: 'PatNo', text: '病历号', width: 100, sortable: false },
			//{
			//    dataIndex: 'ItemName', text: '检验项目', width: 100, sortable: false,
			//    renderer: function (value, meta, record) {
			//        if (value) meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
			//        return value;
			//    }
			//},

			{ dataIndex: 'SECTIONNO', text: '检验小组编号', hideable: false, hidden: true },
			{ dataIndex: 'CLIENTNO', text: '送检单位编码', hideable: false, hidden: true },
			{ dataIndex: 'SectionType', text: '小组类型', hideable: false, hidden: true }
		];
		
		if (me.hasPrint) {
		    me.columns.push({ dataIndex: 'PageName', text: '纸张类型', width: 80, hideable: true, hidden: true });
		    me.columns.push({ dataIndex: 'PageCount', text: '文件页量', width: 80, hideable: true, hidden: true });
			me.columns.push({dataIndex:'PRINTTIMES',text:'打印次数',width:65,hideable:false,
				renderer:function(v,meta,record){
					var imgName = v ? "unprint" : "print";
						tootip = "已经打印<b style='color:red;'> " + v + " </b>次",
						value = v ? "  <b>" + v + "</b>" : "";
					
					meta.tdAttr = 'data-qtip="' + tootip + '"';
	                return "<img src='" + Shell.util.Path.uiPath + "/ReportPrint/images/" + imgName + ".png'/>" + value;
				}
			});
			me.selectUrl += ',PageName,PageCount';
		}
		
		//数据集属性
		me.storeConfig = me.storeConfig || {};
		me.storeConfig.proxy = me.storeConfig.proxy || {
			type:'ajax',
			url:'',
			reader:{type:'json',totalProperty:'total',root:'rows'},
			extractResponseData:function(response){
			    var result = Ext.JSON.decode(response.responseText);

			    if (result.success) {
			        var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
			        result.total = ResultDataValue.total;
			        result.rows = ResultDataValue.rows;
			    } else {
			        result.total = 0;
			        result.rows = [];
			        Shell.util.Msg.showError(result.ErrorInfo);
			    }

			    response.responseText = Ext.JSON.encode(result);
			    return response;
			}
		};
		
		me.callParent(arguments);
	},
    /**创建挂靠*/
	createDockedItems:function(){
	    var dockedItems = this.callParent(arguments);

	    dockedItems.push(Ext.create('Shell.ReportPrint.class.PrintPdfWin', {
	        itemId: 'PrintPdfWin',
	        header: false,
	        height: 0,
            width:0
	    }));

	    return dockedItems;
	},
	/**点击直接打印处理*/
	onPrintClick:function(but){
		var me = this,
			toptoolbar = me.getComponent('toptoolbar'),
			strPageName = toptoolbar.getComponent('strPageName').getValue(),
			hasFilter = me.getComponent('toptoolbar').getComponent('hasFilter'),
			hasFilter = hasFilter ? hasFilter.getValue() : null,
			records = me.getSelectionModel().getSelection(),
			len = records.length,
			filterLen = 0;
			
		if(len == 0){
			Shell.util.Msg.showError("必须选择数据才能进行打印!");
			return;
		}
		
		for(var i=0;i<len;i++){
			if(hasFilter && records[i].get('PRINTTIMES') > 0) continue;//过滤已打印的数据
			filterLen++;
		}
		if(filterLen == 0){
			Shell.util.Msg.showError("必须选择数据才能进行打印!");
			return;
		}
		
		if(strPageName == "A5"){
			var A4List = [];;
			for(var i=0;i<len;i++){
				if(hasFilter && records[i].get('PRINTTIMES') > 0) continue;//过滤已打印的数据
				if(records[i].get('PageName') == "A4"){
					A4List.push(records[i]);
				}
			}
			if(A4List.length > 0){
				var error = ["<center><b>A5方式打印时不能选择A4报告!</b></center>"];
				for(var i=0;i<A4List.length;i++){
					var a4 = A4List[i],
						text = 
							'<b style="color:red;margin:0 5px;">核收日期【</b><b>' + 
							Shell.util.Date.toString(a4.get('RECEIVEDATE'),true) + '</b>' +
							'<b style="color:red;">】</b>' +
							'<b style="color:red;margin:0 5px;">姓名【</b><b>' + 
							a4.get('CNAME') + 
							'<b style="color:red;">】</b>' +
							'<b style="color:red;margin:0 5px;">样本号【</b><b>' + 
							a4.get('SAMPLENO') + 
							'<b style="color:red;">】</b>' +
							'<b style="margin:0 5px;">A4</b>';
					error.push(text);
				}
				
				Shell.util.Msg.showError(error.join("</br>"));
				return;
			}
		}
		
		//禁用功能按钮
		me.disableControl();
		//打印勾选的报告
		me.printAll(records);
		//启用功能按钮
		me.enableControl();
	},
	/**打印勾选的报告*/
	printAll:function(records){
		//var html = 
		//	'<iframe width="100%" height="100%" src="' + Shell.util.Path.uiPath + '/printPdf/printPDF.html" ' +
		//		'height="100%" width="100%" frameborder="0" ' +
		//		'style="overflow:hidden;overflow-x:hidden;overflow-y:hidden;height:100%;width:100%;' +
		//		'position:absolute;top:0px;left:0px;right:0px;bottom:0px"' +
		//	'></iframe>'
	    //;
		
	    //var width = document.body.clientWidth * 0.9,
        //    height = document.body.clientHeight * 0.9;

		//Ext.create('Ext.window.Window',{
		//    title: 'PDF打印',
		//	width:width,
		//	height:height,
		//	html:html
		//}).show();

		var me = this;
        me.PrintPdfWin = me.getComponent('PrintPdfWin');
	    me.PrintPdfWin.print(me.getReportPrintInfo());
	},
	/**获取勾选的数据*/
	getReportPrintInfo:function(){
	    var me = Ext.getCmp('ext_print_list'),//this,
			toptoolbar = me.getComponent('toptoolbar'),
			strPageName = toptoolbar.getComponent('strPageName').getValue(),
			hasFilter = me.getComponent('toptoolbar').getComponent('hasFilter'),
			hasFilter = hasFilter ? hasFilter.getValue() : null,
			records = me.getSelectionModel().getSelection(),
			len = records.length,
			data = [];
			
		for(var i=0;i<len;i++){
			if(hasFilter && records[i].get('PRINTTIMES') > 0) continue;//过滤已打印的数据
			
			var record = records[i],
				ReportFormID = record.get('ReportFormID'),
				RECEIVEDATE = Shell.util.Date.toString(record.get('RECEIVEDATE'),true),
				fileName = RECEIVEDATE + "/" + ReportFormID + ".pdf";
				
			data.push({
				ReportFormID:ReportFormID,
				RECEIVEDATE:RECEIVEDATE,
				CNAME:record.get('CNAME'),
				SAMPLENO:record.get('SAMPLENO'),
				PageName:record.get('PageName'),//纸张类型,A4/A5
				PageCount: record.get('PageCount'),//文件页量
				url:Shell.util.Path.reportPath + "/" + fileName
			});
		}
		
		return {
		    A4Type:me.A4Type,
			strPageName:strPageName,
			data:data
		};
	},
	/**收缩*/
	onCollapseClick:function(but){
		this.collapse();
	},
	
	/**增加打印次数*/
	addPrintTimes:function(ids){
	    var me = Ext.getCmp('ext_print_list'),//this,
			url = me.addPrintTimesUrl + "?reportformidstr=" + ids;
			
		me.getToServer(url,function(v){
			Shell.util.Msg.showLog("【PrintList】打印次数累加返回的值=" + v);
			var result = Ext.JSON.decode(v);
				
			if(!result.success){
				Shell.util.Msg.showLog("【PrintList】打印次数累加,ids=" + ids + "的打印次数累加错误!");
			}else{
				Shell.util.Msg.showLog("【PrintList】打印次数累加,ids=" + ids + "打印次数累加");
			}
		},false);
	},

    /**重写查询功能*/
	onSearch: function () {
	    this.internalWhere = this.getInternalWhere();
	    this.load(null, true);
	},
    /**获取内部条件*/
	getInternalWhere: function () {
	    var me = this,
            toptoolbar = this.getComponent('toptoolbar'),
            upprint = toptoolbar.getComponent('upprint'),
            dept = toptoolbar.getComponent('dept'),
            where = [];

        //是否已打印
	    if (upprint) {
	        var checked = upprint.getValue();
	        if (checked) {
	            where.push("PRINTTIMES=0");
	        }
	    }

        //科室ID
	    if (dept) {
	        var v = dept.getValue();
	        if (v) {
	            if (Ext.typeOf(v) == 'string') {
	                v = 0;
	                dept.select('所有科室');
	            }
	        } else {
	            dept.select('所有科室');
	        }

	        if (v) {
	            where.push("DeptNo=" + v);
	        }
	    }

	    return where.join(" and ");
	},
    /**获取带查询参数的URL*/
	getLoadUrl: function () {
	    var me = this,
			url = me.callParent(arguments);
	    
	    if (me.defaultOrderBy.length > 0) {
	        var len = me.defaultOrderBy.length;
                orderby = [];

            for (var i = 0; i < len;i++){
                orderby.push(me.defaultOrderBy[i].field + " " + me.defaultOrderBy[i].order);
	        }

	        var index = url.indexOf('where=');

	        if (index != -1) {
	            url += ' ORDER BY ' + orderby.join(",").replace('\'','%27');
	        }
	    }

	    return url;
	},
    /**更改行样式*/
	changeRowClass: function (record, rowIndex, rowParams, store) {
	    if (!record.get("PRINTTIMES")) {
	        return 'x-grid-record-unprint';
	    }
	    return 'x-grid-record-print';
	}
});