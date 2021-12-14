/**
 * 报告列表
 * @author Jcall
 * @version 2018-09-03
 * 代码新包迁移
 * @author Jing
 * @version 2018-09-20
 * @author Guohx
 * @version 2020-01-08
 */
Ext.define('Shell.class.historyAndBackups.historyBasic.RequestList',{
    extend: 'Shell.ux.panel.Grid',

    requires: [
		'Shell.ux.form.field.ComboBox'
    ],
	
	//title: '打印列表',
	width:400,
	height:300,
	multiSelect:false,
	defaultPageSize:50,
	//remoteSort:false,
	pagingtoolbar:'simple',
	/**获取数据服务路径*/
	selectUrl: '/ServiceWCF/ReportFormService.svc/SelectRequest',
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
    //批量打印数量配置
	printCountSetting: 9999,
    /**A4纸张类型，1(A4) 2(16开)*/
	A4Type: 1,
    /**默认勾选过滤框*/
	checkFilter: false,
    /**默认打印类型*/
	printType: 'A4',
    /**默认顺序*/
	defaultOrderBy: [],
    /**报告时间字段*/
	DateField: 'RECEIVEDATE',
    /**强制分页字段*/
	ForcedPagingField: '',
    /**点击复选框才选中行*/
    CheckOnly:false,
    
    /**最大打印数量*/
    maxPrintTimes:2,
    /**默认勾选双面打印*/
	checkDoublePrint: false,
	/**是否需要选择打印机*/
	hasPdfPrinter:false,
	/**PDF文件打印机数组*/
	pdfPrinterList: [],
	/**默认打印机*/
	_defaultPrinter:'',
	fields:'', //获取的数据字段
	/**默认选中第一行*/
	autoSelect:false,
	//选中的行Index
	clickRowIndex:null,
    /**排序字段*/
	itemOrder: [],
	//选中的行数
	selectRowCount:0,
    appType :'',//页面类型
    IsbTempReport:'',//是否开启部分审核报告
    IsQueryRequest:false,//是否查询request表
	afterRender: function () {
	    var me = this;
	    if (me.defaultLoad) {
	        me.internalWhere = me.getInternalWhere();
	    }
	    me.callParent(arguments);
	    
	    me.on({
	    	itemclick:function(view,record,item,index) {
	    		me.focus();//默认光标自动定位面板
	    		var nowSelectRowCount = me.getSelectionModel().getCount();
	    		if(nowSelectRowCount != me.selectRowCount){
	    			me.selectRowCount = nowSelectRowCount;
	    			return;
	    		}
	    		
	    		if(me.clickRowIndex != null){
	    			me.getView().removeRowCls(me.clickRowIndex,'x-grid-record-itemclick');
	    		}
				me.getView().addRowCls(index,'x-grid-record-itemclick');
				me.clickRowIndex = index;
				me.RowClickIndex = index;
			},
			afterload:function(){
				setTimeout(function(){
					me.RowClickIndex = 0;
					me.onRowClick();
				},10);
			}
	    });
	    //键盘上箭头监听
	    me.KeyUpMap = new Ext.util.KeyMap({
			target: me.getEl(),
			key: Ext.EventObject.UP,
			fn: me.onKeyUp,
			scope: me
		});
		//键盘下箭头监听
        me.KeyDownMap = new Ext.util.KeyMap({
			target: me.getEl(),
			key: Ext.EventObject.DOWN,
			fn: me.onKeyDown,
			scope: me
		});
		//默认光标自动定位面板
		me.focus();
	},
	initComponent:function(){
	    var me = this;
		
		me.addEvents('beforeSearch');
	    
		
	    //getRowClass：更改行样式
		//me.viewConfig = { getRowClass: me.changeRowClass };
	    //点击复选框才选中行
	    me.selModel = {
	    	checkOnly: me.CheckOnly
	    };
	    //创建功能按钮栏
		me.createButtonsToolbar();
        //创建数据列
		me.createGridColumns();
	    //创建数据集属性
		me.createGridStore();
		
		me.callParent(arguments);
	},
    /**创建功能按钮栏*/
	createButtonsToolbar: function () {
	    var me = this;
	    //功能按钮栏
	    var buttons = [];
	    if (me.hasPrint) {
	        buttons.push({
	            xtype: 'checkbox', itemId: 'upprint', boxLabel: '未打印&nbsp;', checked: me.checkUnprint,
	            handler: function () {
	                me.onSearch();
	            }
	        });
	        buttons.push({
	            xtype: 'uxcombobox', itemId: 'dept', dataSize: 1000, width: 120, editable: true,hidden:true,
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

	        //buttons.push({xtype:'checkbox',itemId:'preview',boxLabel:'预览',handler:function(){}});
	        buttons.push({
	            xtype: 'checkbox', itemId: 'hasFilter', boxLabel: '过滤', checked: me.checkFilter,
	            handler: function () { }
	        });

	        buttons.push('-');
	        buttons.push({
	            xtype: 'combobox', itemId: 'strPageName',
	            mode: 'local', editable: false,
	            displayField: 'text', valueField: 'value',
	            width: 60, value: me.printType,
	            store: new Ext.data.SimpleStore({
	                fields: ['text', 'value'],
	                data: [['A4', 'A4'], ['A5', 'A5'], ['双A5', '双A5']]
	            })
	        });
            //谷歌下不显示双面打印
	        if (!Ext.isChrome) {
	            buttons.push('-', {
	                xtype: 'checkbox', itemId: 'isDoublePrint', boxLabel: '双面', checked: me.checkDoublePrint,
	                handler: function () { }
	            });
	        }
            buttons.push('-', 'print');
           /* //汇总打印
            buttons.push({
                xtype: 'button',
                text: '汇总打印',
                iconCls: 'button-print',
                handler: function () {
                    me.onPrintClick(this,true);
                }
            });
            buttons.push('-');*/
			//多项目历史对比
			/*buttons.push({
				xtype: 'button', text: '项目对比',
				iconCls:'button-show',
                listeners: {
                    click: function () {
                    	var store = me.store.data;
                    	var record = me.getSelectionModel().getSelection();
                        if(record.length > 1){
                        	for (var i = 1; i < record.length; i++) {
                        		if(record[i].data.PatNo != null){
                        			if(record[0].data.PatNo != record[i].data.PatNo){
	                        		Shell.util.Msg.showInfo("请选择同一个病人做对比！");
	                        		return;
		                        	}
		                        	if(i == record.length-1){
						        		me.HistoryCompare(store,record);
						        	}
                        		}else{
                        			Shell.util.Msg.showInfo("仅含有病历号的数据可以进行对比，请您重新选择数据！");
                        			return;
                        		}
					        }	
                        }else if(record[0].data.PatNo != null){
                        	me.HistoryCompare(store,record);                       	
                        }else{
                        	Shell.util.Msg.showInfo("仅含有病历号的数据可以进行对比，请您重新选择数据！");
                        	return;
                        }
                    }
                }				
				
			});
			//下载报告单
			buttons.push({
                xtype: 'button',
                text: '下载',
                iconCls: 'button-print',
                handler: function () {
                    var record = me.getSelectionModel().getSelection();
                    if(record.length<10){
                    	if(record.length > 0){
	                    	var ReportFormID = "";
	                    	var SectionType ="";
	                    	for(var i = 0; i<record.length; i++){
	                    		if(i==0){
	                    			ReportFormID = record[i].data.ReportFormID;
	                    			SectionType = record[i].data.SectionType;
	                    		}else{
	                    			ReportFormID += "*"+record[i].data.ReportFormID;
	                    			SectionType += ";"+record[i].data.SectionType;
	                    		}
	                    	}
	                    	var data = {ReportFormID:ReportFormID,SectionType:SectionType};
	                    	Ext.Ajax.defaultPostHeader = 'application/json';
							Ext.Ajax.request({
								method: 'POST',
								async: false,
							    url: Shell.util.Path.rootPath +'/ServiceWCF/ReportFormService.svc/DownloadthePDFByReportFormID',
							    params:Ext.JSON.encode(data),
								success: function(response){
									var response = Ext.decode(response.responseText);
									var ResultDataValue = JSON.parse(response.ResultDataValue);
									if(response.success){
										window.location.href = Shell.util.Path.rootPath +"/"+ ResultDataValue;
									}else{
										Shell.util.Msg.showInfo(response.ErrorInfo);
									}							    
								},
								error : function(response){
									Shell.util.Msg.showInfo("下载失败！");
								}
							});
	                    }else{
	                    	Shell.util.Msg.showInfo("请选择要下载的报告单！");
	                    }
                   }else{
                    	Shell.util.Msg.showInfo("超过下载数量限制，最多下载十份！");
                   }
                }
            });*/
	        me.multiSelect = true;//取消复选框
	    }
	    buttons.push('->');
	    buttons.push({ xtype: 'uxbutton', itemId: 'collapse', text: '', iconCls: 'button-left', tooltip: '<b>收缩面板</b>' });

	    me.toolbars = me.toolbars || [{
	        dock: 'top', itemId: 'toptoolbar', buttons: ['refresh']
	    }];
	    me.toolbars[0].buttons = me.toolbars[0].buttons.concat(buttons);
	},
	HistoryCompare:function(store,record){
		var ReportFormID = [];
		var SectionType = [];
		
        for (var i = 0; i < record.length; i++) {
            ReportFormID.push(record[i].data.ReportFormID);
            SectionType.push(record[i].data.SectionType);
        }
        var flag = false;
        if(ReportFormID.length>0){//是否选择数据
        	var me = this;
        	var PatNO = record[0].data.PatNo;
        	var data = {ReportFormID:ReportFormID,SectionType:SectionType,PatNO:PatNO};
			Ext.Ajax.defaultPostHeader = 'application/json';
			Ext.Ajax.request({
				method: 'POST',
				async: false,
			    url: Shell.util.Path.rootPath +'/ServiceWCF/ReportFormService.svc/ResultMhistory',
			    params:Ext.JSON.encode(data),
				success: function(response){
				    flag=response;
				},
				error : function(response){
					Shell.util.Msg.showInfo("执行错误！");
				}
			});
        }else{
        	Shell.util.Msg.showInfo("请选择要对比的报告单！");
        	
        	return ;
        }
        flag ? me.Mhistor(store,record,flag):"";
	},
	Mhistor:function(store,record,response){
		var me = this;
		//Ext.QuickTips.init();
		var PatNO = record[0].data.PatNo;
		var CName = record[0].data.CNAME;
		
		var response = Ext.decode(response.responseText);
		var obresponse = JSON.parse(response.ResultDataValue);
		var bWidth = document.body.clientWidth;//获取body的宽度
		var bHeight = document.body.clientHeight;//获取body的高度
		//Shell.util.Win.open("Ext.panel.Panel",{width:300,height:300}).show();
		Shell.util.Win.open('Shell.class.historyAndBackups.historyBasic.ProjectContrast', {   
		    title: '历史对比'+'&nbsp'+'姓名:'+CName+" 病历号:"+PatNO,		    
         	width: '70%',
	        height: '70%',
	        PatNO:PatNO,
	        obresponse:obresponse
		}).show();
	},
    //默认排序
	SetDefalutOrder:function () {
	    var me = this;
	    var items = me.itemOrder;
	    if (items.length > 1) {
	        for (var i = 0; i < items.length; i++) {
	            for (var j = 0; j < items.length; j++) {
	                if (items[i].OrderDesc < items[j].OrderDesc) {
	                    var t = items[i];
	                    items[i] = items[j];
	                    items[j] = t;
	                }
	            }
	        }
	    }
	    for (var i = 0; i < items.length; i++) {
	        var order = items[i].OrderMode ? items[i].OrderMode : 'ASC';
	        me.defaultOrderBy.push({ field: items[i].ColumnName, order: order });
	        if(items[i].ColumnName =="CHECKDATE"){
	    		me.defaultOrderBy.push({ field: 'CHECKTIME', order: order });	
	    	}
	    }
	},

    //获得列配置
	getColumnsSetting:function () {
	    var me = this;
	    var columns = [];
	    
	    Ext.Ajax.defaultPOSTHander="application/json";
	    Ext.Ajax.request({
	        url: Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/GetColumnsTemplateByAppType?AppType=' + encodeURI(me.appType),
	        async: false,
	        method: 'get',
	        success: function (response, options) {
	            var reponse = Ext.JSON.decode(response.responseText);
	            if (reponse.success) {
                    var items = Ext.JSON.decode(reponse.ResultDataValue);
	                for (var i = 0; i < items.length; i++) {
                        var hash = {};
	                    hash["dataIndex"] = items[i].ColumnName;
	                    hash["text"] = items[i].ShowName;
	                    hash["width"] = items[i].Width;
	                    hash["sortable"] = false;
                        hash["hidden"] = Boolean(items[i].ISShow);
                        if (items[i].Render != null && items[i].Render.length > 3) {
                        	var  rend = Ext.JSON.decode(items[i].Render);
	                        hash["renderer"] = rend.renderer;
	                    }
	                    if (items[i].OrderFlag == 1) {
	                        me.itemOrder.push(items[i]);
	                    }
	                    columns.push(hash);
	                }
                }
	        }
	    });
	    //设置排序
	    me.SetDefalutOrder();
	    return columns;
	},
    /**创建数据列*/
	createGridColumns: function () {
	    var me = this;
	    var col = me.getColumnsSetting();
	    me.fields = col;
        //服务路径
	    me.selectUrl += '?fields=' + me.getStoreFields().join(',');

	    var columns = [];
	    columns.push({xtype: 'rownumberer', text: '序号', width: 40, align: 'center'});
	    

        //数据列信息
	    columns=Ext.Array.merge(columns, col);
	    me.columns = me.columns || columns;

	    if (me.hasPrint) {
	    	me.columns.push({ dataIndex: 'PageName', text: '纸张类型', width: 80, hideable: true, hidden: true });
		    me.columns.push({ dataIndex: 'PageCount', text: '文件页量', width: 80, hideable: true, hidden: true });
	        
	    }
	    
	    var isAddPatNo = true;
	    for(var i=0;i<col.length;i++){
	    	if(col[i].text == "病历号"){
	    		isAddPatNo = false;
	    	}
	    }
	    if(isAddPatNo){
	    	me.columns.push({dataIndex: 'PatNo', text: '病历号', width: 40, hideable: true, hidden: true});
	    }
	    me.columns.push({dataIndex: 'bTempReport', text: '部分审核', width: 40, hideable: true, hidden: true});
	    
	    
	},
    /**创建数据集属性*/
	createGridStore: function () {
	    var me = this;
	    //数据集属性
	    me.storeConfig = me.storeConfig || {};
	    me.storeConfig.proxy = me.storeConfig.proxy || {
	        type: 'ajax',
	        url: '',
	        reader: { type: 'json', totalProperty: 'total', root: 'rows' },
	        extractResponseData: function (response) {
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
	},
    /**获取数据字段*/
	getStoreFields: function () {
	    var me = this;
	    var fields = [];
	    for (var i = 0; i < me.fields.length; i++) {
	        fields.push(me.fields[i].dataIndex);
	    }
	    var bo = false,bo2 = false,boRID=false,boSENO=false,boST=false,boRE=false,boRDT=false;
        for (var i in fields) {
            if ('CHECKTIME' == fields[i]) { boRDT = true; }
		    if('RECEIVEDATE'== fields[i]){boRE = true;}
		    if('SectionType'== fields[i]){boST = true;}
		    if('SECTIONNO'== fields[i]){boSENO = true;}
			if('ReportFormID'== fields[i]){boRID = true;}
			if(me.DateField == fields[i]){bo = true;}
			if(me.ForcedPagingField && me.ForcedPagingField.dataIndex == fields[i]){bo2 = true;}
        }
        if (!boRDT) fields.push('CHECKTIME');
		if(!boRE) fields.push('RECEIVEDATE');
		if(!boST) fields.push('SectionType');
		if(!boSENO) fields.push('SECTIONNO');
		if(!boRID) fields.push('ReportFormID');
	    if(!bo) fields.push(me.DateField);
	    if(!bo2 && me.ForcedPagingField) fields.push(me.ForcedPagingField.dataIndex);
	    if (me.hasPrint) { fields.push('PageName', 'PageCount'); }
	    fields.push('PatNo');
	    if(me.IsbTempReport+"" == "true"){fields.push('bTempReport')};
	    return fields;
	},
    /**创建挂靠*/
	createDockedItems: function () {
	    var me = this;
	    var dockedItems = me.callParent(arguments);
	    
	    if(me.hasPdfPrinter){
	    	//新建一个WScript.Shell对象     
			me.Shell = new ActiveXObject("WScript.Shell");
			//默认打印机
			me._defaultPrinter = me.Shell.RegRead("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows NT\\CurrentVersion\\Windows\\Device");
			
			var defaultPrinter = me._defaultPrinter.split(',')[0],
				list = me.pdfPrinterList || [],
				len = list.length,
				data = [];
			
			data.push([defaultPrinter,'【默认打印机】' + defaultPrinter]);//默认打印机
			//报告打印机
			for(var i=0;i<len;i++){
				data.push([list[i],'【报告打印机】' + list[i]]);
			}
			
	    	//打印机选择
		    dockedItems.unshift({
		    	dock:'top',
		    	xtype:'toolbar',
		    	itemId:'toptoolbar2',
		    	items:[{
		    		xtype:'combobox',itemId:'printer',
		            mode:'local',editable:false,
		            displayField:'text',valueField:'value',
		            width:'100%',fieldLabel:'打印机',labelWidth:50,
		            labelAlign:'right',value:defaultPrinter,
		            store:new Ext.data.SimpleStore({
		                fields: ['value','text'],
		                data: data
		            })
		    	}]
		    });
	    }

	    dockedItems.push(Ext.create('Shell.class.historyAndBackups.historyBasic.PdfWinS', {
	        itemId: 'PdfWin',
	        header: false,
	        height: 0,
	        width: 0,
	        mergePageCount: me.mergePageCount,
	        clreaTimes: me.clreaTimes,
	        openAddPrintTimes: me.openAddPrintTimes,
	        DateField: me.DateField,
	        ForcedPagingField: me.ForcedPagingField,
	        hasPdfPrinter:me.hasPdfPrinter,//是否需要选择打印机
	        defaultPrinter:me._defaultPrinter,//默认打印机
	        listeners:{
	        	printStart:function(p){
	        		me.disableControl();
	        	},//禁用功能按钮
	        	printEnd:function(p,ids){
	        		me.enableControl();
	        		me.onPrintEnd(ids);
	        	}//启用功能按钮
	        }
	    }));
	    
	    return dockedItems;
	},
	/**打印完毕处理*/
	onPrintEnd:function(ids){
		var me = this,
			list = ids || [],
			len = list.length;
			
		for(var i=0;i<len;i++){
			var record = me.store.findRecord("ReportFormID",ids[i]);
			if(record){
				var num = parseInt((record.get("PRINTTIMES") || "0")) + 1;
				record.set("PRINTTIMES",num);
				record.commit();
			}
		}
		//清理所有勾选
		me.getSelectionModel().deselectAll();
	},
	/**点击直接打印处理*/
    onPrintClick: function (but,isMerge){
		var me = this,
		  	recordsisbTempReport = me.getSelectionModel().getSelection();
		
		for(var i = 0; i<recordsisbTempReport.length;i++){
			if(recordsisbTempReport[i].data.bTempReport && recordsisbTempReport[i].data.bTempReport==1){
				Shell.util.Msg.showError("部分审核的报告不允许打印!");
				return;
			}			
		}
	    /**
		 * 批量打印功能只支持IE8以上版本
		 * @JcallShell
		 * @version 2018-07-04
		 */
		var IEVersion = Shell.util.IE.getIEVersion();
		//非IE、IE6、IE7、Edge的浏览器需要下载IE8
	    if (IEVersion == -1 || IEVersion == 6 || IEVersion == 7 || IEVersion == 'edge') {
	        if (Ext.isChrome) {
	            me.getComponent("PdfWin").IsChrome = true;
	        } else {
	            Shell.util.Msg.showMsg({
	                title: '提示信息',
	                msg: '批量打印功能只支持IE8以上版本!需要下载IE8浏览器吗？',
	                icon: Ext.Msg.QUESTION,
	                buttons: Ext.Msg.OKCANCEL,
	                callback: function (btn) {
	                    if (btn != "ok") return;
	                    window.open(Shell.util.Adobe.IE8Url);
	                }
	            }, me);
	            return;
	        }
		}
		
		/**
		 * 打印必须先安装adobe组件，如果没有安装，弹出安装文件下载
		 * @JcallShell
		 * @version 2018-07-04
		 */
		var isAcrobatPluginInstall = Shell.util.Adobe.isInstall();
		if(!isAcrobatPluginInstall){
			window.open(Shell.util.Adobe.Url);
			return;
		}
		
		var me = this,
			toptoolbar = me.getComponent('toptoolbar'),
			strPageName = toptoolbar.getComponent('strPageName').getValue(),
			hasFilter = me.getComponent('toptoolbar').getComponent('hasFilter'),
			hasFilter = hasFilter ? hasFilter.getValue() : null,
			records = me.getSelectionModel().getSelection(),
			len = records.length,
			filterLen = 0;
			
		if(len == 0){
			Shell.util.Msg.showError("必须选择数据才能进行打印!",me);
			return;
		}
		
		for(var i=0;i<len;i++){
			if(hasFilter && records[i].get('PRINTTIMES') > 0) continue;//过滤已打印的数据
			filterLen++;
		}
		if(filterLen == 0){
			Shell.util.Msg.showError("必须选择数据才能进行打印!",me);
			return;
		}
        //超出设置的打印数量提示
		if (len > me.printCountSetting) {
			Shell.util.Msg.showError("超出最大勾选的打印数量:" + me.printCountSetting + ",请修改勾选的数据!",me);
		    return;
		}

		if(strPageName == "A5"){
		    var A4List = [];;
            
			//for(var i=0;i<len;i++){
			//	if(hasFilter && records[i].get('PRINTTIMES') > 0) continue;//过滤已打印的数据
			//	if(records[i].get('PageName') == "A4"){
			//		A4List.push(records[i]);
			//	}
			//}
			if(A4List.length > 0){
				var error = ["<center><b>A5方式打印时不能选择A4报告!</b></center>"];
				for(var i=0;i<A4List.length;i++){
					var a4 = A4List[i],
						text = 
							'<b style="color:red;margin:0 5px;">报告日期【</b><b>' + 
							Shell.util.Date.toString(a4.get('RECEIVEDATE'), true) + '</b>' +
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
				
				Shell.util.Msg.showError(error.join("</br>"),me);
				return;
			}
		}
		if(me.maxPrintTimes){
			var error = [];
			for(var i=0;i<len;i++){
				var rec = records[i];
				//最大打印数量
				if(rec.get('PRINTTIMES') >= me.maxPrintTimes){
					var text = 
						'<b style="color:red;margin:0 5px;">报告日期【</b><b>' + 
						Shell.util.Date.toString(rec.get('RECEIVEDATE'), true) + '</b>' +
						'<b style="color:red;">】</b>' +
						'<b style="color:red;margin:0 5px;">姓名【</b><b>' + 
						rec.get('CNAME') + 
						'<b style="color:red;">】</b>' +
						'<b style="color:red;margin:0 5px;">样本号【</b><b>' + 
						rec.get('SAMPLENO') + 
						'<b style="color:red;">】</b>' +
						'<b style="margin:0 5px;">已打印 </b><b style="color:red;">' + 
						rec.get('PRINTTIMES') + '</b><b> 次</b>';
						
					error.push(text);
				}
			}
			if(error.length > 0){
				error.unshift("<center><b>已打印" + me.maxPrintTimes + "次及以上次数的报告不能再打印!</b></center>");
				Shell.util.Msg.showError(error.join("</br>"),me);
				return;
			}
		}
		//打印勾选的报告
        if (isMerge) {
            me.mergePrintAll();
        } else {
            me.printAll(records);
        }
	},
	/**打印勾选的报告*/
	printAll:function(records){
		var me = this,
			PdfWin = me.getComponent('PdfWin');
			
		if (PdfWin) {
			if(me.hasPdfPrinter){
				var printer = me.getComponent('toptoolbar2').getComponent('printer').getValue();
				PdfWin.print(me.getReportPrintInfo(),null,printer);
			}else{
				PdfWin.print(me.getReportPrintInfo());
			}
		} else {
		    Shell.util.Msg.showError('打印组件加载失败！',me);
		}
    },
    /**合并打印勾选的报告*/
    mergePrintAll: function () {
        var me = this,
            PdfWin = me.getComponent('PdfWin');
        var InfoData = me.mergeGetReportPrintInfo();
        if (PdfWin) {
            if (me.hasPdfPrinter) {
                var printer = me.getComponent('toptoolbar2').getComponent('printer').getValue();
                PdfWin.MergePrint(InfoData, null, printer,true);
            } else {
                PdfWin.MergePrint(InfoData,null,null,true);
            }
        } else {
            Shell.util.Msg.showError('打印组件加载失败！', me);
        }
    },
	/**获取勾选的数据*/
	getReportPrintInfo:function(){
	    var me = this,
			toptoolbar = me.getComponent('toptoolbar'),
			strPageName = toptoolbar.getComponent('strPageName').getValue(),
			hasFilter = me.getComponent('toptoolbar').getComponent('hasFilter'),
			hasFilter = hasFilter ? hasFilter.getValue() : null,
			isDoublePrint = me.getComponent('toptoolbar').getComponent('isDoublePrint'),
			isDoublePrint = isDoublePrint ? isDoublePrint.getValue() : null,
			records = me.getSelectionModel().getSelection(),
			len = records.length,
			data = [];
			
		/**
		 * 解决列表勾选不能按页面顺序打印的处理
		 * @JcallShell
		 * @version 2018-07-28
		 */
		var count = me.store.getCount(),
			tempArr = [];
		for(var i=0;i<count;i++){
			tempArr.push(null);
		}
		for(var i=0;i<len;i++){
			var rec = records[i];
			tempArr[rec.index % count] = rec;
		}
			
		for(var i=0;i<count;i++){
			if(tempArr[i] == null) continue;
			if(hasFilter && records[i].get('PRINTTIMES') > 0) continue;//过滤已打印的数据
			
			var record = tempArr[i],
				ReportFormID = record.get('ReportFormID'),
				DATE = Shell.util.Date.toString(record.get('RECEIVEDATE'), true),
				fileName = DATE + "/" + ReportFormID + ".pdf";
				
			var obj = {
			    ReportFormID: ReportFormID,
			    DATE: DATE,
			    SectionNo:record.get('SECTIONNO'),
			    SectionType:record.get('SectionType'),
			    CNAME: record.get('CNAME'),
			    SAMPLENO: record.get('SAMPLENO'),
			    PageName: record.get('PageName'),//纸张类型,A4/A5
			    PageCount: record.get('PageCount'),//文件页量
			    url: Shell.util.Path.reportPath + "/" + fileName
			};
			if (me.ForcedPagingField) {
			    obj[me.ForcedPagingField.dataIndex] = record.get(me.ForcedPagingField.dataIndex);
			}
			data.push(obj);
		}
		
		return {
		    A4Type:me.A4Type,
			strPageName:strPageName,
			isDoublePrint:isDoublePrint,
			data:data
		};
    },
    /**合并获取勾选的数据*/
    mergeGetReportPrintInfo: function () {
        var me = this,
            toptoolbar = me.getComponent('toptoolbar'),
            strPageName = toptoolbar.getComponent('strPageName').getValue(),
            hasFilter = me.getComponent('toptoolbar').getComponent('hasFilter'),
            hasFilter = hasFilter ? hasFilter.getValue() : null,
            isDoublePrint = me.getComponent('toptoolbar').getComponent('isDoublePrint'),
            isDoublePrint = isDoublePrint ? isDoublePrint.getValue() : null,
            records = me.getSelectionModel().getSelection(),
            len = records.length,
            data = [];

		/**
		 * 解决列表勾选不能按页面顺序打印的处理
		 * @JcallShell
		 * @version 2018-07-28
		 */
        var count = me.store.getCount(),
            tempArr = [];
        for (var i = 0; i < count; i++) {
            tempArr.push(null);
        }
        for (var i = 0; i < len; i++) {
            var rec = records[i];
            tempArr[rec.index % count] = rec;
        }

        var ReportFormID = '';
        var IsMerge = false;
        for (var i = 0; i < count; i++) {
            var obj = {
                IsMerge: false,
                ReportFormID: "",
                DATE: DATE,
                SectionNo: 0,
                SectionType: 0,
                CNAME: "",
                SAMPLENO: "",
                PageName: "",//纸张类型,A4/A5
                PageCount: "",//文件页量
                url: Shell.util.Path.reportPath + "/"
            };
            if (tempArr[i] == null) continue;
            var record = tempArr[i],
                ReportFormid = record.get('ReportFormID'),
                DATE = Shell.util.Date.toString(record.get('RECEIVEDATE'), true);
                //fileName = DATE + "/" + ReportFormID + ".pdf";

            //如果是普通生化的合并
            if(record.get('SectionType') == 1) {
                ReportFormID += ReportFormid + ",";
                IsMerge = true;
            } else {
                obj.ReportFormID = ReportFormid;
                obj.DATE= DATE;
                obj.SectionNo= record.get('SECTIONNO');
                obj.SectionType= record.get('SectionType');
                obj.CNAME= record.get('CNAME');
                obj.SAMPLENO= record.get('SAMPLENO');
                obj.PageName= record.get('PageName');
                obj.PageCount= record.get('PageCount');
                data.push(obj);
            }   
        }
        if (IsMerge) {
            data.push({
                IsMerge : true,
                ReportFormID : ReportFormID.substring(0, ReportFormID.length - 1),
                DATE : DATE,
                SectionNo : record.get('SECTIONNO'),
                SectionType : 1,
                CNAME : record.get('CNAME'),
                SAMPLENO : record.get('SAMPLENO'),
                PageName : record.get('PageName'),
                PageCount : record.get('PageCount')
            });
        }
        
        return {
            A4Type: me.A4Type,
            strPageName: strPageName,
            isDoublePrint: isDoublePrint,
            data: data
        };
    },
	/**收缩*/
	onCollapseClick:function(but){
		this.collapse();
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
	    var arryurl = url.split("SerialNo");
	    if (arryurl.length > 1) {
	        url = arryurl[0];
	    }
	    
	    if (me.defaultOrderBy.length > 0) {
	        var len = me.defaultOrderBy.length;
                orderby = [];
	        var flag = false;
            for(var i = 0;i<len;i++){
	           	if(orderby.length == 0){
	           		orderby.push(me.defaultOrderBy[i].field + " " + me.defaultOrderBy[i].order );
	           	}else{
	           		for(var j = 0;j<orderby.length;j++){
	           			if(orderby[j] == me.defaultOrderBy[i].field + " " + me.defaultOrderBy[i].order){
	           				flag = true;
	           			}
	           		}
	           		if(!flag){
	           			orderby.push(me.defaultOrderBy[i].field + " " + me.defaultOrderBy[i].order );
	           			flag = false;
           			}
           		}
           		
            }
            
	        var index = url.indexOf('where=');

	        if (index != -1) {
	            url += ' ORDER BY ' + orderby.join(",").replace('\'','%27');
	        }

	        if (arryurl.length > 1) {
	            url += "&SerialNo=" + arryurl[1];
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
	},
	onBeforeLoad: function () {
	    var me = this;
	    me.fireEvent('beforeSearch', this);
	    me.callParent(arguments);
	},
	//行点击处理
	onRowClick:function(){
		var me = this,
			index = me.RowClickIndex,
			el = me.getView().getNode(index);
		
		if(el){
			el.focus();
			el.click();
		}
	},
	//键盘上箭头触发
	onKeyUp: function(e) {
    	var me = this;
    	if(me.RowClickIndex > 0){
    		me.RowClickIndex--;
    		me.onRowClick();
    	}
    },
    //键盘上箭头触发
    onKeyDown:function(e){
    	var me = this;
    	if(me.RowClickIndex < me.store.getCount() - 1){
    		me.RowClickIndex++;
    		me.onRowClick();
    	}
    },/**加载数据后*/
	onAfterLoad:function(records,successful){
		var me = this;
		
		me.enableControl();//启用所有的操作功能
		
		if(!successful || records.length == 0){
			me.store.removeAll();
			return;
		}
		
		var autoSelect = me.autoSelect,
			type = Ext.typeOf(autoSelect),
			num = autoSelect === true ? 0 : -1;
  		
  		if(type === 'string'){//需要选中的行主键
			num = me.store.find(me.PKColumn,autoSelect);
		}
		//选中行号为num的数据行
		if(num >= 0){me.getSelectionModel().select(num);}
	}
});