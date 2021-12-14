/**
 * 自助打印
 * @author Jcall
 * @version 2018-09-03
 */
Ext.define('Shell.class.selfhelp.app.App', {
	extend: 'Ext.panel.Panel',
	title: '自助打印',
	id: "selfhelpApp",
	printPageType: 'A4', //打印类型  A4 , A5 , 双A5
	selectColumn: 'SerialNo', // 设置查询字段
	lastDay: 90, //查询多少天之前的记录
	printtimes: 1, //限制检验之星打印次数
	clientprin: 1, //限制自助打印次数
	//倒计时关闭
	tackTime: 5,
	staticTackTime: '',
	notPrintSectionNo: '', //小组打印限制
	IsLabSignature: '', //是否显示电子章
	pintnumvadd: 0, //打印计数
	defaultCondition: '', //默认查询条件

	//打印计数名字位置
	printnumnameTop: 13,
	printnumnameLeft: 26,
	printnumnameFontSize: 26,
	printnumnameColor: '#0e121a',
	printnumnameIsHidden: false,
	//打印计数框设置
	printnumTop: 14,
	printnumLeft: 34,
	printnumFontSize: 26,
	printnumColor: '#0e121a',
	printnumIsHidden: false,
	//清除打印计数
	closeprintnumTop: 13,
	closeprintnumLeft: 41,
	closeprintnumIsHidden: false,
	//检验中的报告单显示位置
	tabgridTop: 34,
	tabgridLeft: 46,
	tabgridHangTouFontSize: 20,
	tabgridNeiRongFontSize: 16,
	tabgridHidden: false,
	//自助打印四个字样式设置
	selfhelpTextFontSize: 35,
	selfhelpTextColor: 'black',
	selfhelpTextTop: 16,
	selfhelpTextLeft: 16,
	selfhelpTextIsHidden: false,
	//时间样式设置
	DateTimeFontSize: 26,
	DateTimeColor: '#0e121a',
	DateTimeTop: 13,
	DateTimeLeft: 6,
	DateTimeIsHidden: false,
	//输入框设置
	caridWidth: 260,
	caridHeight: 30,
	caridTop: 68,
	caridLeft: 15,
	caridFontSize: 20,
	caridIsHidden: false,
	//打开小键盘按钮设置
	openJianpanTop: 68,
	openJianpanLeft: 48,
	openJianpanIsHidden: false,
	//关闭小键盘按钮设置
	closeJianpanTop: 68,
	closeJianpanLeft: 48,
	//关闭提示文字设置
	closeFontSize: 16,
	closeColor: 'black',
	closeTop: 69,
	closeLeft: 15,
	//查询显示设置
	reportviewFontSize: 14,
	reportviewColor: 'black',
	reportviewTop: 70,
	reportviewLeft: 28,
	//中心显示位置
	panelTop: 36,
	panelLeft: 15,
	panelWidth: 600,
	panelHeight: 242,
	//读卡按钮
	readCardButtonTop: 68,
	readCardButtonLeft: 65,
	readCardButtonWidth: 100,
	readCardButtonHeight: 36,
	readCardButtonIsHidden: true,
	//检验完成病人信息列表
	reportformlistsTop: 66,
	reportformlistsLeft: 8,
	reportformlistsFontSize: 20,
	reportformlistsIsHidden: false,
	//查询按钮
	printreportButtonTop: 68,
	printreportButtonLeft: 80,
	printreportButtonWidth: 100,
	printreportButtonHeight: 36,
	printreportButtonIsHidden: false,
	//bigReportview 大字体提示
	bigReportviewTop: 50,
	bigReportviewLeft: 6,
	bigReportviewFontSize: 30,
	bigReportviewIsHidden: false,
	//ReportNumber
	ReportNumber: 1,

	//医嘱项目过滤
	noparitemname: '',
	//是否使用clodop方式打印
	IsUseClodopPrint : 'false',
	//bodyPadding:'40% 10%',
	//设置背景图片  
	bodyStyle: {
		background: 'url(http://localhost/ZhiFang.ReportFormQueryPrint/ui/SelfHelpPrint/images/sunset.jpg) no-repeat #00FFFF'
	},

	backgroundImageUrl: '/class/selfhelp/images/selfhelpbg.png',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.getComponent("panel").getComponent("carid").focus(true, 1000);
		var urlParameter = Shell.util.Path.getRequestParams(false);
		if(urlParameter.where) {
			var where = urlParameter.where;
			me.getComponent("panel").getComponent("carid").setValue(urlParameter.where);
			me.printReport(where /*.replace(/;/g,"=")*/ );
		}
		//document.write("<style type='text/css'>#readCardButton .x-btn-inner{font-size:30px;color:red;}</style>")
	},
	initComponent: function() {
		var me = this;
		me.staticTackTime = 0;
		me.staticTackTime = me.staticTackTime + me.tackTime;
		var bodyStyle = [];
		bodyStyle.push("background-image:url(" + Shell.util.Path.uiPath + me.backgroundImageUrl + ")");
		bodyStyle.push("background-repeat:no-repeat");
		bodyStyle.push("background-size:100% 100%");
		bodyStyle.push("-moz-background-size:100% 100%");
		me.bodyStyle = bodyStyle.join(';');
		var IsChrome = '';
		if(Ext.isChrome) {
			IsChrome = true;
		}
		me.IsUseClodopPrint+='';
		//打印组件
		me.PdfWin = Ext.create("Shell.class.selfhelp.app.PdfWinS", {
			width: 0,
			height: 0,
			IsChrome: IsChrome,
			showProgressInfoWin: false,
			IsUseClodopPrint:me.IsUseClodopPrint,
			listeners: {
				printEnd: function(m, ids) {
					me.staticTackTime = 0;
					me.staticTackTime = me.staticTackTime + me.tackTime;
					me.showRepot();
					Ext.TaskManager.start(me.task); //启动计时器
					me.getComponent("panel").getComponent("reportview").setText("打印完毕");
					setTimeout(function() {
						me.getComponent('tabgrid').update("");
						me.getComponent('reportformlists').update("");
						me.getComponent('bigReportview').update("");
						me.getComponent("panel").getComponent("carid").focus(true, 1000);
					}, 4000 + 2000 * me.ReportNumber);
				}
			}
		});
		//页面组件
		me.items = me.createItems();
		//提示信息
		me.task = {
			run: function() {
				me.getComponent("panel").getComponent("close").setText("关闭(" + me.staticTackTime + ")");
				if(me.staticTackTime == 0) {
					me.hideRepot();
					Ext.TaskManager.stop(me.task);
					me.getComponent("panel").getComponent("carid").focus(true, 1000);
				}
				me.staticTackTime--;
			},
			interval: 1000
		};
		me.onTimeChange();
		me.callParent(arguments);
	},
	//创建页面组件
	createItems: function() {
		var me = this;
		var items = [{
			xtype: 'label',
			itemId: 'selfheipText',
			text: '自助打印',
			hidden: me.selfhelpTextIsHidden,
			style: 'font-size:' + me.selfhelpTextFontSize + 'px;color:' + me.selfhelpTextColor + ';width:100%;display:inline-block;position:absolute;top:' + me.selfhelpTextTop + '%;left:' + me.selfhelpTextLeft + '%;'
		}, {
			xtype: 'textfield',
			itemId: 'carid',
			id: 'carid',
			//fieldLabel:!me.showName ? "卡名" : me.showName,
			width: me.caridWidth,
			height: me.caridHeight,
			emptyText: "   请输入卡号",
			hidden: me.caridIsHidden,
			//labelWidth:80,
			//labelAlign:"right",
			fieldStyle: 'border:0;border-radius:15px;background-color:#F7F4FB;background-image:none;padding:0 0 0 10px;font-size:' + me.caridFontSize + 'px;',
			style: 'position:absolute;top:' + me.caridTop + '%;left:' + me.caridLeft + '%;',
			listeners: {
				specialkey: function(field, e) {
					if(e.getKey() == Ext.EventObject.ENTER) {
						if (field.getValue() == null || field.getValue().replace(/\s*/g, "")==""){
							me.staticTackTime = 0;
							me.staticTackTime = me.staticTackTime + me.tackTime;
							me.showRepot();
							Ext.TaskManager.start(me.task);
							me.getComponent("panel").getComponent("reportview").setText("请传入病人信息！");
						}else{
							me.printReport(field.getValue());
						}
					}
				}
			}
		}, {
			xtype: 'button',
			itemId: 'readCardButton',
			labelAlign: "right",
			width: me.readCardButtonWidth,
			height: me.readCardButtonHeight,
			style: 'border-radius:10px;font-size:50px;position:absolute;top:' + me.readCardButtonTop + '%;left:' + me.readCardButtonLeft + '%;',
			hidden: me.readCardButtonIsHidden,
			id: 'readCardButton',
			text: '读卡',
			disabled: false,
			handler: function() {
				//山东定制读卡
				//var activieX = document.getElementById("activieX");				
				//var actstring = activieX.GetCardId();
				//北京中医药大学东方医院定制读卡
						
				var CertCtl = document.getElementById("CertCtl");
				if(CertCtl){
					alert("未检测到读卡控件！");
				}
				var actstring = "";
				try {
					    var lianjie = CertCtl.connect();//连接读卡器
						var result = CertCtl.readCert();//读卡
						var resultObj = eval('('+result+')');//toJson(result);
						if (resultObj.resultFlag == 0) 
						{
							actstring = resultObj.resultContent.certNumber;
							console.log(actstring);
						}
					} catch(e) 
					{
				            alert(e);
					}
				if(actstring.indexOf("-1") == -1) {
					me.getComponent("panel").getComponent("carid").setValue(actstring);
					if(actstring == "" || actstring == null){
						me.staticTackTime = 0;
						me.staticTackTime = me.staticTackTime + me.tackTime;
						me.showRepot();
						Ext.TaskManager.start(me.task);
						me.getComponent("panel").getComponent("reportview").setText("请传入病人信息！");
					}else{
						me.printReport(actstring);
					}
					me.getComponent("panel").getComponent("readCardButton").disabled = true;
					setTimeout(function() {
						me.getComponent("panel").getComponent("readCardButton").disabled = false;
					}, 6000);
				} else {
					me.showRepot();
					Ext.TaskManager.start(me.task); //启动计时器
					actstring = actstring.replace("-1", "");
					me.getComponent("panel").getComponent("reportview").setText(actstring);
				}
				//alert(activieX.GetCardId()); //调用ActivieX里公开的方法GetCardId()获取卡号
				/*var selfhelpButton = document.getElementById("selfhelpButton");
				var selfhelpText = document.getElementById("selfhelpText");
				selfhelpButton.addEventListener("click", function () {
				});
				selfhelpButton.click();*/

			}
		}, {
			xtype: 'button',
			itemId: 'printreportButton',
			labelAlign: "right",
			width: me.printreportButtonWidth,
			height: me.printreportButtonHeight,
			hidden: me.printreportButtonIsHidden,
			style: 'border-radius:10px;position:absolute;top:' + me.printreportButtonTop + '%;left:' + me.printreportButtonLeft + '%;',
			//hidden:true,
			text: '查询',
			disabled: false,
			handler: function() {
				var value = me.getComponent("panel").getComponent("carid").getValue();
				if(value == "" || value == null){	
					me.staticTackTime = 0;
					me.staticTackTime = me.staticTackTime + me.tackTime;
					me.showRepot();
					Ext.TaskManager.start(me.task);
					me.getComponent("panel").getComponent("reportview").setText("请传入病人信息！");
				}else{
					me.printReport(value);
				}
			}
		}, {
			xtype: 'button',
			itemId: 'openJianpan',
			labelAlign: "right",
			width: 100,
			height: 36,
			style: 'border:0;border-radius:10px;background:url(' + Shell.util.Path.uiPath + '/class/selfhelp/images/openKeyboard.png' + ') no-repeat;background-size:cover;position:absolute;top:' + me.openJianpanTop + '%;left:' + me.openJianpanLeft + '%;',
			hidden: me.openJianpanIsHidden,
			handler: function() {
				var left = document.getElementById("carid").getBoundingClientRect().left;
				var top = document.getElementById("carid").getBoundingClientRect().top;
				var x = left;
				var y = top + me.getComponent("panel").getComponent("carid").lastBox.height + 33;
				var url = Shell.util.Path.uiPath + "/class/selfhelp/app/SoftKeyboard.html";
				me.ruanjianpan = Shell.util.Win.openUrl(url, {
					header: false,
					width: 486,
					height: 145,
					x: x,
					y: y,
					//cls:'position:absolute;top:70%;left:5%;',
					modal: false
				});
				me.ruanjianpan.show();
				me.getComponent("panel").getComponent("openJianpan").hide();
				me.getComponent("panel").getComponent("closeJianpan").show();
			}
		}, {
			xtype: 'button',
			itemId: 'closeJianpan',
			labelAlign: "right",
			width: 100,
			height: 36,
			style: 'border:0;border-radius:10px;background:url(' + Shell.util.Path.uiPath + '/class/selfhelp/images/closeopenKeyboard.png' + ') no-repeat;background-size:cover;position:absolute;top:' + me.closeJianpanTop + '%;left:' + me.closeJianpanLeft + '%;',
			//text:'关闭小键盘',
			hidden: true,
			handler: function() {
				me.getComponent("panel").getComponent("closeJianpan").hide();
				me.getComponent("panel").getComponent("openJianpan").show();
				me.ruanjianpan.close();
			}
		}, {
			xtype: 'label',
			itemId: 'close',
			style: 'font-size:' + me.closeFontSize + 'px;color:' + me.closeColor + ';font-family:AdobeHeitiStd-Regular 微软雅黑;width:100%;display:inline-block;position:absolute;top:' + me.closeTop + '%;left:' + me.closeLeft + '%;',
			text: '关闭',
			hidden: true,
			listeners: {
				render: function() { //渲染后添加click事件
					Ext.fly(this.el).on('click',
						function(e, t) {
							me.hideRepot();
							Ext.TaskManager.stop(me.task);
						});
				},
				scope: this
			}
		}, {
			xtype: 'label',
			itemId: 'reportview',
			style: 'font-size:' + me.reportviewFontSize + 'px;color:' + me.reportviewColor + ';font-family:AdobeHeitiStd-Regular 微软雅黑;width:100%;display:inline-block;position:absolute;top:' + me.reportviewTop + '%;left:' + me.reportviewLeft + '%;',
			text: '查询中...',
			hidden: true
		}];
		items.push(me.PdfWin);
		return [{
			xtype: 'panel',
			itemId: 'panel',
			bodyPadding: 50,
			border: false,
			style: 'position:absolute;top:' + me.panelTop + '%;left:' + me.panelLeft + '%;margin-top:-121px;margin-left:-200px;height:' + me.panelHeight + 'px;width:' + me.panelWidth + 'px;',
			bodyStyle: {
				background: 'transparent'
			},
			items: items
		}, {
			xtype: 'label',
			itemId: 'DateTime',
			hidden: me.DateTimeIsHidden,
			style: 'font-size:' + me.DateTimeFontSize + 'px;color:' + me.DateTimeColor + ';width:100%;font-family:AdobeHeitiStd-Regular 微软雅黑;display:inline-block;position:absolute;top:' + me.DateTimeTop + '%;left:' + me.DateTimeLeft + '%;'
		}, {
			xtype: 'label',
			itemId: 'tabgrid',
			width: 700,
			height: 200,
			hidden: me.tabgridHidden,
			style: 'border:0;border-radius:0px;background:url(' + Shell.util.Path.uiPath + '/class/selfhelp/images/requestbg.png) no-repeat;background-size:cover;position:absolute;top:' + me.tabgridTop + '%;left:' + me.tabgridLeft + '%;',
			text: "",
			html: ""
		}, {
			xtype: 'label',
			itemId: 'printnumname',
			width: 120,
			text: '打印计数:',
			hidden: me.printnumnameIsHidden,
			style: 'font-size:' + me.printnumnameFontSize + 'px;color:' + me.printnumnameColor + ';width:100%;font-family:AdobeHeitiStd-Regular 微软雅黑;position:absolute;top:' + me.printnumnameTop + '%;left:' + me.printnumnameLeft + '%;'
		}, {
			xtype: 'field',
			itemId: 'printnum',
			width: 100,
			hidden: me.printnumIsHidden,
			fieldStyle: 'border:0;background-image:none;font-size:' + me.printnumFontSize + 'px;color:' + me.printnumColor + ';width:100%;font-family:AdobeHeitiStd-Regular 微软雅黑;',
			style: 'position:absolute;top:' + me.printnumTop + '%;left:' + me.printnumLeft + '%;',
			value: 0
		}, {
			xtype: 'button',
			itemId: 'closeprintnum',
			width: 111,
			height: 33,
			hidden: me.closeprintnumIsHidden,
			style: 'border:0;border-radius:10px;background:url(' + Shell.util.Path.uiPath + '/class/selfhelp/images/clearprintnum.png' + ') no-repeat;background-size:cover;position:absolute;top:' + me.closeprintnumTop + '%;left:' + me.closeprintnumLeft + '%;',
			handler: function() {
				me.getComponent('printnum').setValue(0);
			}
		}, {
			xtype: 'label',
			itemId: 'reportformlists',
			width: 700,
			height: 200,
			hidden: me.reportformlistsIsHidden,
			style: 'border:0;border-radius:0px;background:url(' + Shell.util.Path.uiPath + '/class/selfhelp/images/requestbg.png' + ') no-repeat;background-size:cover;position:absolute;top:' + me.reportformlistsTop + '%;left:' + me.reportformlistsLeft + '%;',
			text: "",
			html: ""

		}, {
			xtype: 'label',
			itemId: 'bigReportview',
			width: 800,
			height: 50,
			hidden: me.bigReportviewIsHidden,
			style: 'border:0;border-radius:0px;background-size:cover;position:absolute;top:' + me.bigReportviewTop + '%;left:' + me.bigReportviewLeft + '%;',
			text: "",
			html: ""
		}];
	},
	printReport: function(value) {
		var me = this;
		me.showRepot();
		var arr = me.getReport(value);
		/* me.staticTackTime =0;
		me.staticTackTime=me.staticTackTime+me.tackTime;*/
		var data = [];
		me.getComponent("panel").getComponent("carid").setValue("");
		for(var i = 0; i < arr.length; i++) {
			var obj = {
				ReportFormID: arr[i].ReportFormID,
				// DATE: DATE,
				SectionNo: '',
				SectionType: 1,
				CNAME: '',
				SAMPLENO: '',
				PageName: arr[i].PageName, //纸张类型,A4/A5
				PageCount: arr[i].PageCount, //文件页量
				url: "/"
			};
			data.push(obj);
		}
		var conf = {
			A4Type: 1,
			strPageName: me.printPageType, //双A5
			isDoublePrint: null,
			data: data
		};
		me.PdfWin.print(conf, false);

		if(arr.length > 0) {
			me.ReportNumber = arr.length;
			me.getComponent("panel").getComponent("close").hide();
			me.getComponent("panel").getComponent("reportview").setText("共" + arr.length + "份报告,正在打印请稍等");

			var pintnumv = me.getComponent('printnum').value;
			var pintnumvadd = Number(pintnumv) + 1;
			me.pintnumvadd = pintnumvadd;
			me.getComponent('printnum').setValue(pintnumvadd);
			me.ReadVoice("共有" + arr.length + "份报告检验完成,正在打印中,请稍等");
		} else {
			Ext.TaskManager.start(me.task); //启动计时器
			me.getComponent("panel").getComponent("reportview").setText("没有可打印的报告");
			me.ReadVoice("没有可打印的报告单");
		}
		setTimeout(function() {
			me.getComponent('tabgrid').update("");
			me.getComponent('reportformlists').update("");
			me.getComponent('bigReportview').update("");
			me.getComponent('reportformlists').update("");
			me.getComponent("panel").getComponent("carid").focus(true, 1000);
		}, 6000);

		me.getComponent("panel").getComponent("carid").setValue("");

	},
	//查询报告
	getReport: function(reportValue) {		
		var me = this,
			uri = Shell.util.Path.rootPath + "/ServiceWCF/ReportFormService.svc/SelectReport",
			arr = [],
			requestarr =[],
			dateEnd = Shell.util.Date.toString(new Date()).split(" ")[0],
			dateStart = Shell.util.Date.getNextDate(dateEnd, -me.lastDay), // 时间范围
			dateStart = Shell.util.Date.toString(dateStart),
			urlParameter = Shell.util.Path.getRequestParams(false),
			where = "",
			reportWhere="",
			requestWhere = "",
			selectColumn = me.selectColumn.split(','),
			selectColumnurl = "",
			notPrintSectionNo = me.notPrintSectionNo,
			noparitemname = me.noparitemname;
		var reportValueArr = reportValue.split(":"),
			queryValue = reportValueArr[0];
			
		
		//山东潍坊定制根据传参调用接口查询HIS数据库中的值
		/*var queryData={	"Sql": "select BRBH from vi_lisjk_brkh where BRKH='"+reportValueArr[0]+"'",
				"ConnName": "门诊"};
		var curWwwPath=window.document.location.href,
		 	pathName=window.document.location.pathname,
		 	pos=curWwwPath.indexOf(pathName),
			localhostPaht = curWwwPath.substring(0,pos);
		Ext.Ajax.defaultPostHeader = 'application/json';
		Ext.Ajax.request({
			url: localhostPaht+"/LisInterface2018/Service/WeiFangFuShu.ashx?action=GetHisData",
			async: false,
			method: 'POST',
			params : Ext.JSON.encode(queryData),
			success: function(response, options) {
				var rs = Ext.JSON.decode(response.responseText);
				if(rs.data) {					
					queryValue = rs.data[0].BRBH;
				}
			}
		});
		
		//山东威海市立医院定制
		if(reportValue.length > 64 ){
			Ext.Ajax.defaultPostHeader = 'application/json';
			Ext.Ajax.request({
				url: Shell.util.Path.rootPath + "/ServiceWCF/ReportFormService.svc/SelfhelpCustomizationServiceGetWhere?barCode="+reportValue,
				async: false,
				method: 'get',
				success: function(response, options) {
					var response = Ext.decode(response.responseText);					
					if(response.success){
						var ResultDataValue = JSON.parse(response.ResultDataValue);
						queryValue = ResultDataValue.cardNo;
					}else{
						me.staticTackTime = 0;
						me.staticTackTime = me.staticTackTime + me.tackTime;
						me.showRepot();
						Ext.TaskManager.start(me.task);
						me.getComponent("panel").getComponent("reportview").setText("未查询到报告！");
						return;
					}
				}
			});
		}*/
		
		//济南市中心医院定制，扫描健康码后需要调第三方服务获取病人相关信息
        //扫描健康码后得到电子健康码刷出来带有冒号，AAECBCFFEBCAFBFEEECCFFEFCDABBEE:0:624A153AD87FD96B9A33F147C66F::
        //临时电子健康码规则:临时电子健康码的开头为DW_T，DW_T_A49B79255CE99ACF2797A3F2460CAC1B8C56F5719ADC9CDE5CEE0765F9A
	    /*if(reportValue.indexOf(":")!=-1 || reportValue.indexOf("DW_T")==0 ){
			Ext.Ajax.defaultPostHeader = 'application/json';
			Ext.Ajax.request({
				url: Shell.util.Path.rootPath + "/ServiceWCF/ReportFormService.svc/SelfhelpCustomizationServiceGetWhereJiNan?barCode="+reportValue,
				async: false,
				method: 'get',
				success: function(response, options) {
					var response = Ext.decode(response.responseText);					
					if(response.success){
						var ResultDataValue = JSON.parse(response.ResultDataValue);
						queryValue = ResultDataValue.sfzhm;
					}else{
						me.staticTackTime = 0;
						me.staticTackTime = me.staticTackTime + me.tackTime;
						me.showRepot();
						Ext.TaskManager.start(me.task);
						me.getComponent("panel").getComponent("reportview").setText("未查询到报告！");
						return;
					}
				}
			});
		}*/
		
		for(var i = 0; i < selectColumn.length; i++) {
			if(i == 0) {
				selectColumnurl = selectColumn[i] + " = '" + queryValue + "' ";
			} else {
				selectColumnurl += " or " + selectColumn[i] + " = '" + queryValue + "' ";
			}
		}
		if(urlParameter.Model) {
			if(urlParameter.Model == 0) {
				where = "isnull(clientprint,0) < "+me.printtimes + " and RECEIVEDATE>='" + dateStart + "' and RECEIVEDATE<='" + dateEnd + "' and ( " + selectColumnurl + ")";
				reportWhere=" RECEIVEDATE>='" + dateStart + "' and RECEIVEDATE<='" + dateEnd + "' and ( " + selectColumnurl + ")";
			} else if(urlParameter.Model == 1) {
				where = "isnull(printtimes,0) < 1 and  isnull(clientprint,0) < "+me.printtimes + " and (" + selectColumnurl + ") and RECEIVEDATE>='" + dateStart + "' and RECEIVEDATE<='" + dateEnd + "'";
				reportWhere= "(" + selectColumnurl + ") and RECEIVEDATE>='" + dateStart + "' and RECEIVEDATE<='" + dateEnd + "'";
			}
		} else {
			where = "isnull(clientprint,0) < "+me.printtimes + " and (" + selectColumnurl + ") and RECEIVEDATE>='" + dateStart + "' and RECEIVEDATE<='" + dateEnd + "'";
			reportWhere="(" + selectColumnurl + ") and RECEIVEDATE>='" + dateStart + "' and RECEIVEDATE<='" + dateEnd + "'";
			//where =  "(" + selectColumnurl + ") and RECEIVEDATE>='" + dateStart + "' and RECEIVEDATE<='" + dateEnd + "'";
		}

		requestWhere = "(" + selectColumnurl + ") and RECEIVEDATE>='" + dateStart + "' and RECEIVEDATE<='" + dateEnd + "'";
		var useWebConfigNotPrintSectionNo = true;
		var useWebConfigNoparitemname = true;
		if (urlParameter.NeedSectionNo) {
			useWebConfigNotPrintSectionNo = false;
			where += " and SectionNo in (" + urlParameter.NeedSectionNo + ")";
			reportWhere += " and SectionNo in (" + urlParameter.NeedSectionNo + ")";
			requestWhere += " and SectionNo in (" + urlParameter.NeedSectionNo + ")";
		}
		if (urlParameter.NotNeedSectionNo) {
			useWebConfigNotPrintSectionNo = false;
			where += " and SectionNo not in (" + urlParameter.NotNeedSectionNo + ")";
			reportWhere += " and SectionNo not in (" + urlParameter.NotNeedSectionNo + ")";
			requestWhere += " and SectionNo not in (" + urlParameter.NotNeedSectionNo + ")";
		}
		if (urlParameter.NeedParitemName) {
			useWebConfigNoparitemname = false;
			var NeedParitemNamesArray = urlParameter.NeedParitemName.split(",");
			var NeedParitemNames = "";
			for (var i = 0; i < NeedParitemNamesArray.length; i++) {
				if (NeedParitemNamesArray.length == 1 || i == NeedParitemNamesArray.length-1) {
					NeedParitemNames += "'" + NeedParitemNamesArray[i] + "'"
				} else {
					NeedParitemNames += "'" + NeedParitemNamesArray[i] + "',"
				}

			}
			where += " and paritemname in (" + NeedParitemNames + ")";
			reportWhere += " and paritemname in (" + NeedParitemNames + ")";
			requestWhere += " and paritemname in (" + NeedParitemNames + ")";
		}
		if (urlParameter.NotNeedParitemName) {
			useWebConfigNoparitemname = false;
			var notNeedParitemNamesArray = urlParameter.NotNeedParitemName.split(",");
			var notNeedParitemNames = "";
			for (var i = 0; i < notNeedParitemNamesArray.length; i++) {
				if (notNeedParitemNamesArray.length == 1 || i == notNeedParitemNamesArray.length-1) {
					notNeedParitemNames += "'" + notNeedParitemNamesArray[i] + "'"
				} else {
					notNeedParitemNames += "'" + notNeedParitemNamesArray[i] + "',"
                }
				
			}
			where += " and paritemname not in (" + notNeedParitemNames + ")";
			reportWhere += " and paritemname not in (" + notNeedParitemNames + ")";
			requestWhere += " and paritemname not in (" + notNeedParitemNames + ")";
		}
		if (notPrintSectionNo && useWebConfigNotPrintSectionNo) {
			var arrnotPrintSectionNo = notPrintSectionNo.split(',');
			for(var i = 0; i < arrnotPrintSectionNo.length; i++) {
				where += " and sectionno != " + arrnotPrintSectionNo[i];
				reportWhere += " and sectionno != " + arrnotPrintSectionNo[i];
				requestWhere += " and sectionno != " + arrnotPrintSectionNo[i];
			}
		}
		if (noparitemname && useWebConfigNoparitemname) {
			var arrnoparitemname = noparitemname.split(';');
			for(var i = 0; i < arrnoparitemname.length; i++) {
				where += " and paritemname != '" + arrnoparitemname[i] + "'";
				reportWhere +=" and paritemname != '" + arrnoparitemname[i] + "'";
				requestWhere += " and paritemname != '" + arrnoparitemname[i] + "'";
			}
		}

		if(me.defaultCondition) {
			var defaultConditionarr = me.defaultCondition.split(",");
			for(var i = 0; i < defaultConditionarr.length; i++) {
				where += " and " + defaultConditionarr[i];
				reportWhere +=" and " + defaultConditionarr[i];
				requestWhere += " and " + defaultConditionarr[i];
			}
		}

		var fields = 'CName,ZDY5,ZDY8,CheckDate,CheckTime,clientprint,paritemname,itemName,ReportFormID,PageName,PageCount';
		var requestrow;
		Ext.Ajax.request({
			url: uri,
			async: false,
			method: 'GET',
			params: {
				fields: fields,
				where: where,
				page: 1,
				limit: 100
			},
			success: function(response, options) {
				var rs = Ext.JSON.decode(response.responseText);
				if(rs.success) {
					var v = Ext.JSON.decode(rs.ResultDataValue);
					arr = v.rows;					
				}
			}
		});
		Ext.Ajax.request({
			url: uri,
			async: false,
			method: 'GET',
			params: {
				fields: fields,
				where: reportWhere,
				page: 1,
				limit: 100
			},
			success: function(response, options) {
				var rs = Ext.JSON.decode(response.responseText);
				if(rs.success) {
					var v = Ext.JSON.decode(rs.ResultDataValue);
					arr1 = v.rows;
					if(arr1.length > 0) {

						var html = "<div><p style='font-size:" + me.reportformlistsFontSize + "px;font-family:AdobeHeitiStd-Regular 微软雅黑;padding:30px 0 0 40px'>检验完成的报告</p></div>";
						html += "<div style='padding:10px 0 0 40px'><table style='width:700px;font-size:" + me.reportformlistsFontSize + "px;font-family:AdobeHeitiStd-Regular 微软雅黑;' ><tr style='background:#73a5ff;color:#ffffff;font-size:20px;font-family:AdobeHeitiStd-Regular 微软雅黑;'><th>姓名</th><th>检验项目</th><th>发票号</th><th>检验时间</th><th>状态</th></tr>";

						for(var i = 0; i < arr1.length; i++) {
							var isprint = "未打印"
							if(arr1[i].clientprint>0){
								isprint = "已打印";
							}
							html += "<tr>" +
								"<td>" + arr1[i].CNAME + "</td>" +
								"<td>" + arr1[i].ParItemName + "</td>" +
								"<td>" + arr1[i].ZDY5 + "</td>" +
								"<td>" + arr1[i].CHECKDATE.split(" ")[0] + " " + arr1[i].CHECKTIME.split(" ")[1] + "</td>" +
								"<td>" + isprint + "</td>" +
								"</tr>";
						}
						html += "</table></div>";
						me.getComponent('reportformlists').update(html);
					} else {
						me.getComponent('reportformlists').update("<div><p style='font-size:" + me.reportformlistsFontSize + "px;font-family:AdobeHeitiStd-Regular 微软雅黑;padding:25px 0 0 24px'>&nbsp&nbsp&nbsp&nbsp没有检验完成的报告</p></div>");
					}
				}
			}
		});
		//查询检验中的报告单
		//var fields = 'SuperGroupName,SuperGroupNo,ReportFormID,COUNT(SuperGroupName) as SuperGroupNameNum';
		Ext.Ajax.request({
			url: Shell.util.Path.rootPath + "/ServiceWCF/ReportFormService.svc/SelectRequest",
			async: false,
			method: 'GET',
			params: {
				fields: fields,
				//where: requestWhere + " group by SuperGroupName,SuperGroupNo,FormNo order by SuperGroupName ",
				where:reportWhere,
				page: 1,
				limit: 100
			},
			success: function(response, options) {
				var rs = Ext.JSON.decode(response.responseText);
				/*if(rs.success) {
					var v = Ext.JSON.decode(rs.ResultDataValue);
					console.log(v.rows)
					//me.getComponent('tabgrid').store.loadData(v.rows);
					var num = 0;

					var html = "<div><p style='font-size:" + me.tabgridHangTouFontSize + "px;font-family:AdobeHeitiStd-Regular 微软雅黑;padding:18px 0 0 24px'>检验中的报告</p></div>" + "<div style='padding:2px 0 0 24px'><p style='width:180px;background:#73a5ff;color:#ffffff;font-size:16px;font-family:AdobeHeitiStd-Regular 微软雅黑;padding-left:10px;'>类型&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp数量</p></div>";
					requestrow = v.rows.length;
					for(var i = 0; i < v.rows.length; i++) {
						num += v.rows[i].SuperGroupNameNum;
						if(num > 0 && num % 2 == 0) {
							html += "<div style='padding:2px 0 0 24px'><p style='width:180px;background:#73a5ff;color:#888888;font-size:" + me.tabgridNeiRongFontSize + "px;font-family:AdobeHeitiStd-Regular 微软雅黑;padding-left:10px;'>" + v.rows[i].SuperGroupName + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp" + v.rows[i].SuperGroupNameNum + "</p></div>";
						} else if(num > 0 && num % 2 != 0) {
							html += "<div style='padding:2px 0 0 24px'><p style='width:180px;color:#888888;font-size:" + me.tabgridNeiRongFontSize + "px;font-family:AdobeHeitiStd-Regular 微软雅黑;padding-left:10px;'>" + v.rows[i].SuperGroupName + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp" + v.rows[i].SuperGroupNameNum + "</p></div>";
						}
					}
					if(v.rows.length == 0) {
						html = "<div><p style='font-size:" + me.tabgridHangTouFontSize + "px;font-family:AdobeHeitiStd-Regular 微软雅黑;padding:18px 0 0 24px'>没有检验中的报告</p></div>";
					}
					me.getComponent('tabgrid').update(html);
					if(num > 0) {
						me.ReadVoice("正在检验中的报告有" + num + "份");
					}
				}*/
				if(rs.success) {
					var v = Ext.JSON.decode(rs.ResultDataValue);
					requestarr = v.rows;
					if(requestarr.length > 0) {

						var html = "<div><p style='font-size:" + me.tabgridHangTouFontSize + "px;font-family:AdobeHeitiStd-Regular 微软雅黑;padding:30px 0 0 40px'>检验中的报告</p></div>";
						html += "<div style='padding:10px 0 0 40px'><table style='width:700px;font-size:" + me.tabgridNeiRongFontSize + "px;font-family:AdobeHeitiStd-Regular 微软雅黑;' ><tr style='background:#73a5ff;color:#ffffff;font-size:20px;font-family:AdobeHeitiStd-Regular 微软雅黑;'><th>姓名</th><th>检验项目</th><th>发票号</th><th>状态</th></tr>";

						for(var i = 0; i < requestarr.length; i++) {
							var isprint = "未打印"
							if(requestarr[i].clientprint>0){
								isprint = "已打印";
							}
							html += "<tr>" +
								"<td>" + requestarr[i].CNAME + "</td>" +
								"<td>" + requestarr[i].ParItemName + "</td>" +
								"<td>" + requestarr[i].ZDY5 + "</td>" +
								//"<td>" + requestarr[i].CHECKDATE.split(" ")[0] + " " + requestarr[i].CHECKTIME.split(" ")[1] + "</td>" +
								"<td>" + isprint + "</td>" +
								"</tr>";
						}
						html += "</table></div>";
						me.getComponent('tabgrid').update(html);
					} else {
						me.getComponent('tabgrid').update("<div><p style='font-size:" + me.tabgridHangTouFontSize + "px;font-family:AdobeHeitiStd-Regular 微软雅黑;padding:25px 0 0 24px'>&nbsp&nbsp&nbsp&nbsp没有检验中的报告</p></div>");
					}
				}
			}
		});
		if(arr.length > 0 || requestrow > 0) {
			//var  bigReportviewzongsize= arr.length + requestrow;
			var bigReportviewhtml = "";
			if(arr.length > 0) {
				bigReportviewhtml = "<div><p style='font-size:" + me.bigReportviewFontSize + "px;font-family:AdobeHeitiStd-Regular 微软雅黑;padding:25px 0 0 24px'>有" + arr.length + "份报告检验完成,正在打印" + arr.length + "份</p></div>";
			}
			if(arr.length <= 0 && requestrow > 0) {
				bigReportviewhtml = "<div><p style='font-size:" + me.bigReportviewFontSize + "px;font-family:AdobeHeitiStd-Regular 微软雅黑;padding:25px 0 0 24px'>正在检验中的报告有" + requestrow + "份</p></div>";
			}
			me.getComponent('bigReportview').update(bigReportviewhtml);
			me.staticTackTime = 0;
			me.staticTackTime = me.staticTackTime + me.tackTime;
		} else {
			me.staticTackTime = 5;
		}

		return arr;
	},

	//隐藏显示
	showRepot: function() {
		var me = this,
			panel = me.getComponent("panel"),
			card = panel.getComponent("carid"),
			vie = panel.getComponent("reportview"),
			close = panel.getComponent("close");

		vie.show();
		card.hide();
		close.show();
	},
	hideRepot: function() {
		var me = this,
			panel = me.getComponent("panel"),
			card = panel.getComponent("carid"),
			vie = panel.getComponent("reportview"),
			close = panel.getComponent("close");

		vie.hide();
		card.show();
		close.hide();
	},
	//当前时间变化
	onTimeChange: function() {
		var me = this;
		//底部时间
		Ext.TaskManager.start({
			run: function() {
				me.getComponent("DateTime").setText(Ext.Date.format(new Date(), 'Y-m-d H:i:s'));
			},
			interval: 1000
		});
	},
	ReadVoice: function(aa) {
		var me = this;
		//创建 Sapi SpVoice 对象
		//var VoiceObj =new ActiveXObject("Sapi.SpVoice");
		//将文本读出来
		//var voice = document.getElementById("txtVoice").value;
		///VoiceObj.Voice=VoiceObj.GetVoices().Item(0);//
		//VoiceObj.Speak(aa, 0);
		Ext.Ajax.request({
			url: Shell.util.Path.rootPath + "/ServiceWCF/ReportFormService.svc/CreatVoice",
			async: true,
			method: 'GET',
			params: {
				txt: aa
			},
			success: function(response, options) {
				var rs = Ext.JSON.decode(response.responseText);
				if(rs.success) {
					var src = rs.ResultDataValue.replace("\",\"/");
					var file = [];
					file['mp3'] = Shell.util.Path.rootPath + "/" + src;
					//file['ogg'] = 'music.ogg'; //此格式为兼容IE外部分浏览器
					me.audioplayer('audioplane', file, false); // 播放（true为是否循环播放）
				}
			}
		});
	},
	audioplayer: function(id, file, loop) {
		var audioplayer = document.getElementById(id);
		if(audioplayer != null) {
			document.body.removeChild(audioplayer);
		}
		if(typeof(file) != 'undefined') {
			if(!!window.ActiveXObject || "ActiveXObject" in window) { // IE 
				document.getElementById("embed").innerHTML = "<embed name=\"player\" TYPE=\"application/x-mplayer2\" src=\"" + file['mp3'] + "\" loop=\"false\" autostart=\"true\" hidden=\"true\"></embed>";
			} else { // Other FF Chome Safari Opera 
				var player = document.createElement('audio');
				player.id = id;
				player.setAttribute('autoplay', 'autoplay');
				if(loop) {
					player.setAttribute('loop', 'loop');
				}
				document.body.appendChild(player);

				var mp3 = document.createElement('source');
				mp3.src = file['mp3'];
				mp3.type = 'audio/mpeg';
				player.appendChild(mp3);

				var ogg = document.createElement('source');
				ogg.src = file['ogg'];
				ogg.type = 'audio/ogg';
				player.appendChild(ogg);
			}
		}
	}

});