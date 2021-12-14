/**页面整体功能*/
var ShellPage = {};

$(function(){
	/**Logo区域*/
	ShellPage.Logo = {
		
	};
	
	/**列表区域*/
	ShellPage.List = {
		/**页码*/
		page:1,
		/**每页数量*/
		limit:20,
		/**总条数*/
		total:0,
		/**排序条件*/
		orderby:'',
		/**选中行背景色*/
		CheckedRowBackgroundColor:'#93c6ec',
		/**病历号*/
		patNO:'',
		/**获取数据的地址*/
		//url:'base.json',
		url:Shell.util.Path.rootPath + '/Ashx/SelectReportPrint.ashx?Type=SelectReport&' +
				'Fields=ReportFormID,SAMPLENO,SECTIONNO,CNAME,CLIENTNO,SectionType,RECEIVEDATE,RECEIVETIME,SampleType,CHECKDATE,CHECKTIME,ItemName',
		/**获取列头信息数组*/
		getFieldInfo:function(){
			return [
				{type:"rownumber",width:25},
				{field:"RECEIVEDATE",text:"核收日期",order:"desc",width:82,
					tooltip:function(v){return v ? Shell.util.Date.toString(v,true) : "";},
					format:function(v){return v ? Shell.util.Date.toString(v,true) : "";}},
				{field:"RECEIVETIME",text:"核收时间",order:"desc",width:68,
					tooltip:function(v){return v ? Shell.util.Date.toString(v).slice(-8) : "";},
					format:function(v){return v ? Shell.util.Date.toString(v).slice(-8) : "";}},
				{field:"SAMPLENO",text:"样本编号",width:105,format:function(v){
					return ShellPage.List.getShowValue(v,12);
				}},
				{field:"SampleType",text:"标本",width:55,format:function(v){
					return ShellPage.List.getShowValue(v,8);
				}},
				{field:"CHECKDATE",text:"报告日期",width:82,
					tooltip:function(v){return v ? Shell.util.Date.toString(v,true) : "";},
					format:function(v){return v ? Shell.util.Date.toString(v,true) : "";}},
				{field:"CHECKTIME",text:"报告时间",width:68,
					tooltip:function(v){return v ? Shell.util.Date.toString(v).slice(-8) : "";},
					format:function(v){return v ? Shell.util.Date.toString(v).slice(-8) : "";}},
				{field:'ItemName',text:'检验项目',width:80,sortable:false,format:function(v){
					return ShellPage.List.getShowValue(v,12);
				}}
			];
		},
		/**数据显示的值*/
		getShowValue:function(v,max){
			var len = Shell.util.String.lenASCII(v),
				max = max || 10;
			if(len > max) v = Shell.util.String.substrASCII(v,0,max-2) + "...";
			return v;
		},
		
		/**渲染列表表头*/
		gridHeadRender:function(){
			var me = ShellPage.List,
				gridHead = document.getElementById("grid-div-head"),
				info = me.getGridHeadInfo(),
				html = [];
				
			html.push("<table id='grid_table_head' class='table' style='witdh:100%;height:100%;'>");
			html.push(info);
			html.push("</table>");
			
			gridHead.innerHTML = html.join("");
		},
		/**渲染列表行列*/
		gridBodyRender:function(data){
			var me = ShellPage.List,
				gridBody = document.getElementById("grid-div-body"),
				info = me.getGridBodyInfo(data),
				html = [];
				
			html.push("<table id='grid_table_body' class='table' style='witdh:100%;height:100%;background-color:#ffffff;'>");
			html.push(info);
			html.push("</table>");
			
			gridBody.innerHTML = html.join("");
			
			if(info){
				var tr0 = document.getElementById("grid_div_body_tr_0");
				tr0.click();
			}
		},
		
		/**获取表头内容*/
		getGridHeadInfo:function(){
			var me = ShellPage.List,
				fieldInfoList = me.getFieldInfo() || [],
				fieldInfoLen = fieldInfoList.length,
				html = [],
				order = [];
				
			html.push("<tr class='alter'>");
			
			for(var i=0;i<fieldInfoLen;i++){
				//存在序号列
				if(fieldInfoList[i].type == "rownumber"){
					html.push("<th width=");
					html.push(fieldInfoList[i].width || 40);
					html.push("></th>");
					continue;
				}
				if(fieldInfoList[i].sortable === false){
					html.push("<th field='");
				}else{
					html.push("<th onclick='onGridSort(this);' field='");
				}
				html.push(fieldInfoList[i].field);
				html.push("' ");
				html.push("defaultText='");
				html.push(fieldInfoList[i].text || "");
				html.push("' ");
				if(fieldInfoList[i].width){
					html.push("width=");
					html.push(fieldInfoList[i].width);
				}
				html.push(">");
				html.push(fieldInfoList[i].text || "");
				if(fieldInfoList[i].order){
					html.push("<img src='css/images/grid/sort_");
					html.push(fieldInfoList[i].order == "desc" ? "desc"  : "asc");
					html.push(".gif'/>");
					//排序字段
					order.push(fieldInfoList[i].field + (fieldInfoList[i].order == "desc" ? " desc"  : " asc"));
				}
				html.push("</th>");
			}
			
			html.push("<th width=12></th>");
			
			html.push("</tr>");
			
			if(order.length > 0){
				ShellPage.List.orderby = "order by " + order.join(",");
			}
			
			return html.join("");
		},
		/**创建数据内容*/
		getGridBodyInfo:function(data){
			var me = ShellPage.List,
				fieldInfoList = me.getFieldInfo() || [],
				fieldInfoLen = fieldInfoList.length,
				dataList = (data || {}).rows || [],
				dataLen = dataList.length,
				html = [];
				
			//渲染数据行
			for(var i=0;i<dataLen;i++){
				html.push("<tr id='grid_div_body_tr_" + i + "' onMouseOut='this.className=\"mouseOut\"' onMouseOver='this.className=\"mouseOver\"' " +
						"onclick='onRowClick(this,\"" + dataList[i].ReportFormID + "\",\"" + dataList[i].SECTIONNO + "\",\"" + dataList[i].SectionType + "\");'>");
				var obj = dataList[i];
				
				for(var j=0;j<fieldInfoLen;j++){
					//存在序号列
					if(fieldInfoList[j].type == "rownumber"){
						html.push("<td class='rownumber' style='text-align:center;' width=");
						html.push(fieldInfoList[j].width || 40);
						html.push(">");
						html.push(i+1);
						html.push("</td>");
						continue;
					}
					
					html.push("<td ");
					//-------tooltip-----
					if(Shell.util.typeOf(fieldInfoList[j].tooltip) == "function"){
						html.push("title='");
						html.push(fieldInfoList[j].tooltip(obj[fieldInfoList[j].field]));
						html.push( "'");
					}else{
						html.push("title='");
						html.push(obj[fieldInfoList[j].field]);
						html.push( "'");
					}
					//-----------------
					if(i == 0 && fieldInfoList[j].width){
						html.push("width=");
						html.push(fieldInfoList[j].width);
					}
					html.push(">");
					if(Shell.util.typeOf(fieldInfoList[j].format) == "function"){
						html.push(fieldInfoList[j].format(obj[fieldInfoList[j].field]));
					}else{
						html.push(obj[fieldInfoList[j].field]);
					}
					html.push("</td>");
				}
				html.push("</tr>");
			}
			return html.join("");
		},
		/**获取数据*/
		getData:function(params,callback){
			var me = ShellPage.List,
				url = me.url,
				parArr = [];
			for(var i in params){
				parArr.push(i + "=" + params[i]);
			}
			if(parArr.length > 0){
				url += "&" + parArr.join("&");
			}
			
			$.ajax({ 
				dataType:'json',
				contentType:'application/json',
				url:url,
				success:function(result){
					Shell.util.Msg.showLog("获取检验报告列表数据成功");
					callback(result);
				},
				error:function(request,strError){
					Shell.util.Msg.showLog("获取检验报告数据失败！错误信息：" + strError);
					callback(null);
				} 
			});
		},
		/**刷新列表*/
		load:function(){
			var me = ShellPage.List,
				where = me.getWhere();
				
			if(!where){
				var msg = "缺少参数:patNO(病历号)未传递到本程序!";
				ShellPage.Function.showError(msg);
				return;
			}
				
			var params = {
				page:me.page,
				limit:me.limit,
				where:where
			};
			
			me.getData(params,function(result){
				var dataList = (result || {}).rows || [];
				if(dataList.length == 0){
					ShellPage.Chart.hideChart(true);
				}
				me.changePagetoolbarInfo(result);
				me.gridBodyRender(result);//渲染列表行列
			});
		},
		/**获取条件*/
		getWhere:function(){
			var me = ShellPage.List,
				patNO = me.patNO,
				where = [];
				
			if(!patNO) return null;
			
			where.push("patNO='" + patNO + "'");
			
			var start = document.getElementById("grid-div-toolbar-date-s").value,
				end = document.getElementById("grid-div-toolbar-date-e").value;
				
			if(start){
				where.push("RECEIVEDATE>='" + start + "'");
			}
			if(end){
				end = Shell.util.Date.toString(Shell.util.Date.getNextDate(end),true);
				where.push("RECEIVEDATE<'" + end + "'");
			}
			
			return where.join(" and ") + " " + me.orderby;
		},
		/**更改分页栏信息*/
		changePagetoolbarInfo:function(data){
			var me = ShellPage.List,
				info = document.getElementById("grid-div-pagingtoolbar-info"),
				pageInfo = document.getElementById("grid-div-pagingtoolbar-page"),
				data = data || {},
				total = data.total || 0;
				
			me.total = total;
			info.innerHTML = "共 <b>" + total + "</b> 条";
			
			var pages = parseInt(total / me.limit),
			num = total % me.limit;
			
			pages = num == 0 ? pages : pages + 1;
			pages = pages == 0 ? 1 : pages;
			pageInfo.innerHTML = "第 <b>" + me.page + "</b> / <b>" + pages + "</b>页";
		},
		/**获取表格宽度*/
		getGridWidth:function(){
			//设置列表抬头和内容的宽度
			var fieldInfo = ShellPage.List.getFieldInfo(),
				len = fieldInfo.length,
				width = 0;
				
			for(var i=0;i<len;i++){
				width += parseInt(fieldInfo[i].width || 100);
			}
			
			width += 53;
			
			return width;
		}
	};
	
	/**内容区域*/
	ShellPage.Content = {
		/**获取数据的地址*/
		//url:'base.json',
		url:Shell.util.Path.rootPath + '/Ashx/SelectReportPrint.ashx',
		/**更改结果内容*/
		changeContent:function(params){
			this.getData(params,this.changeDivInfo);
		},
		/**更改内容*/
		changeDivInfo:function(html){
			var div = document.getElementById("content-div");
			div.innerHTML = html;
			
			var tr = document.getElementById("tr_1");
			if(tr){tr.click();}
		},
		/**获取数据*/
		getData:function(params,callback){
			var url = this.url;
				
			if(!params || params.ReportFormID == null || params.SectionNo == null || params.SectionType == null){
				Shell.util.Msg.showLog("Shell.ReportPrint.class.PrintContent的getContent方法参数params的内容错误！");
				return;
			}
			url += "?Type=Preview&ModelType=result&ReportFormID=" + params.ReportFormID + "&SectionNo=" + 
				params.SectionNo + "&SectionType=" + params.SectionType;
				
			$.ajax({
				dataType:'html',
				url:url,
				success:function(result){
					Shell.util.Msg.showLog("获取结果数据成功");
					callback(result);
				},
				error:function(request,strError){
					Shell.util.Msg.showLog("获取结果数据失败！错误信息：" + strError);
					callback(null);
				} 
			});
		}
	};
	
	/**图表区域*/
	ShellPage.Chart = {
		/**条件参数*/
		params:{},
		/**图对象*/
		chart:null,
		/**X轴字段*/
		xField:'ReceiveDate',
		/**Y轴字段*/
		yField:'ReportValue',
		/**点上显示数据*/
		showLabel:true,
		/**获取数据的地址*/
		//url:'base.json',
		url:Shell.util.Path.rootPath + '/Ashx/SelectReportPrint.ashx',
		/**更新图表*/
		changeChart:function(params,isPrivate){
			var me = ShellPage.Chart;
			me.hideChart(false);
			me.params = params;
			me.load(isPrivate);
		},
		/**获取条件和标题*/
		getWhereAndTitle:function(value){
			var me = ShellPage.Chart,
				title = {text:"历史对比数据",subtext:""},
				where = [];
			
			var start = document.getElementById("chart-div-toolbar-date-s").value,
				end = document.getElementById("chart-div-toolbar-date-e").value;
				
			if(start){
				where.push("rf.RECEIVEDATE>='" + start + "'");
			}
			if(end){
				end = Shell.util.Date.toString(Shell.util.Date.getNextDate(end),true);
				where.push("rf.RECEIVEDATE<'" + end + "'");
			}
			title.subtext = (start ? start : "开始") + " ~ " + (end ? end : "至今");
			
			return {where:where.join(" and "),title:title};
		},
		/**获取图表属性*/
		getConfig:function(list){
			var me = ShellPage.Chart,
				list = list || [],
				len = list.length,
				lineName = "结果",
				option = {
					tooltip:{trigger:'axis',formatter:'核收时间：{b}</br>检验结果：{c}'},
					//legend:{data:[lineName],show:false},
					xAxis:[{
						type:'category',//name:'核收日期',
						axisLine:{lineStyle:{color:'blue',width:1}},
						axisLabel:{rotate:0,formatter:function(value){return value.slice(0,10);}},data:[]
					}],
					yAxis:[{
						type:'value',//name:'检验结果',
						axisLine:{lineStyle:{color:'blue',width:1}},
						scale:true,precision:2
					}],
					series:[{type:'line',name:lineName,data:[]}],
					toolbox:{
				        show:true,
				        orient:'vertical',
				        x:'right',
				        y:'center',
				        feature:{
				            mark:{show:true},
				            magicType:{show:true,type:['line','bar']},
				            restore:{show:true},
				            saveAsImage:{show:true}
				        }
				    },
				    grid: {
				        x:50,
				        y:30,
				        x2:30,
				        y2:25,
				        borderWidth:0,
				        borderColor:'#ccc'
				    }
				};
				
			for(var i=0;i<len;i++){
				//option.xAxis[0].data.push(list[i][me.xField]);
				option.series[0].data.push(list[i][me.yField]);
				option.xAxis[0].data.push(list[i]["CheckDate"] + " " + list[i]["CheckTime"]);
			}
			
			var max = 0,min = 0;
			if(me.showLabel){
				var arr = Shell.util.Array.reorder(list,me.yField);
				if(arr.length > 1){
					option.yAxis.min = arr[0][me.yField];
					option.yAxis.max = arr[arr.length-1][me.yField];
				}
				option.series[0].itemStyle = {
					normal:{
						lineStyle:{color:'blue',width:1},
						label:{show:true,textStyle:{fontFamily:'Arial',color:'blue'}}
					},
					emphasis:{
						lineStyle:{color:'blue',width:3},
						label:{show:true,textStyle:{fontFamily:'Arial',fontWeight:'bold'}}
					}
				};
			}
				
			return option;
		},
		/**创建图表*/
		createChart:function(config){
			var me = ShellPage.Chart,
				requireId = ['echarts','echarts/chart/bar'];
			
			require.config({
			    paths:{
			        'echarts':Shell.util.Path.uiPath+'/echarts/echarts',
			        'echarts/chart/bar':Shell.util.Path.uiPath+'/echarts/echarts'
			    }
			});
			
			require(requireId,function(ec){
		        // 基于准备好的dom，初始化echarts图表
		        me.chart = ec.init(document.getElementById("chart-div-body"));
		        // 为echarts对象加载数据 
		        me.chart.setOption(config);
		    });
		},
		/**获取数据*/
		getData:function(params,where,callback){
			var me = ShellPage.Chart,
				url = me.url;
			//延迟加载
			//Shell.util.Action.delay(function(){
				url += "?Type=History&PatNo=" + params.PatNo + "&ItemNo=" + params.ItemNo + "&Table=" + params.Table;
				
				if(where){url += "&where=" + where;}
					
				$.ajax({ 
					dataType:'json',
					contentType:'application/json',
					url:url,
					success:function(result){
						Shell.util.Msg.showLog("获取图表数据成功");
						callback(result);
					},
					error:function(request,strError){
						Shell.util.Msg.showLog("获取图表数据失败！错误信息：" + request.responseText);
						ShellPage.Function.showError("获取图表数据失败！错误信息：" + request.responseText);
						callback(null);
					} 
				});
			//},null,100);
		},
		/**清空数据*/
		clearData:function(){
			var me = ShellPage.Chart;
			me.chart && me.chart.clear();
		},
		/**刷新图表*/
		load:function(isPrivate){
			var me = ShellPage.Chart,
				params = me.params,
				isValid = me.isValid(isPrivate),
				info = me.getWhereAndTitle();
				
			me.getData(params,info.where,function(list){
				if(!list || list.length == 0){
					me.clearData();
					return;
				}
				var option = me.getConfig(list);
				if(!option){
					me.clearData();
				}else{
					//option.title = info.title;
					if(me.chart){
						me.chart.setOption(option,true);
					}else{
						me.createChart(option);
					}
				}
			});
		},
		/**参数合法性校验*/
		isValid:function(isPrivate){
			var me = ShellPage.Chart,
				params = me.params;
				
			if(!isPrivate && (!params || !params.PatNo || !params.ItemNo || !params.Table || !params.ReceiveDate)){
				var errorInfo = [];
				if(!params){
					ShellPage.Function.showError("Shell.print.class.PrintChart的load方法没有接收到参数对象!");
					me.clearData();
					return false;
				}
				errorInfo.push("Shell.print.class.PrintChart的load方法接收的参数对象有错!");
				if(!params.PatNo){errorInfo.push("<b style='color:red'>PATNO</b>参数错误!");}
				if(!params.ItemNo){errorInfo.push("<b style='color:red'>ITEMNO</b>参数错误!");}
				if(!params.Table){errorInfo.push("<b style='color:red'>SECTIONTYPE</b>参数错误!");}
				if(!params.ReceiveDate){errorInfo.push("<b style='color:red'>RECEIVEDATE</b>参数错误!");}
				
				ShellPage.Function.showError(errorInfo.join("</br>"));
				me.clearData();
				return false;
			}
			
			if(!isPrivate){
				//默认一个月数据
				var start = document.getElementById("chart-div-toolbar-date-s"),
					end = document.getElementById("chart-div-toolbar-date-e"),
					date_e = Shell.util.Date.getDate(params.ReceiveDate),
					date_s = Shell.util.clone(date_e);
					
				date_s = date_s.setMonth(date_s.getMonth() - 1);
				start.value = Shell.util.Date.toString(date_s,true);
				end.value = Shell.util.Date.toString(date_e,true);
			}
			return true;
		},
		/**隐藏图表*/
		hideChart:function(bo){
			if(bo){
				$("#chart-div").hide();
			}else{
				$("#chart-div").show();
			}
		}
	};
	
	/**初始化*/
	ShellPage.Init = {
		/**初始化*/
		init:function(){
			var me = this;
			//页面加载成功后处理
			window.onload = function(){
				ShellPage.List.gridHeadRender();//渲染列表表头
				ShellPage.List.load();
			}
			//浏览器大小发生变化时处理
			window.onresize=function(){
				//Shell.util.Action.delay(function(){me.onWindowResize();},null,10);
				me.onWindowResize();
			}
			$("#grid-div-toolbar-refresh").bind("click",function(){ShellPage.List.load(true);});
			$("#grid-div-toolbar-search").bind("click",function(){ShellPage.List.load(true);});
			$("#chart-div-toolbar-refresh").bind("click",function(){ShellPage.Chart.load(true);});
			$("#chart-div-toolbar-search").bind("click",function(){ShellPage.Chart.load(true);});
			
			//获取传递的病历号
			var params = Shell.util.Path.getRequestParams();
			ShellPage.List.patNO = params.patNO;
			
			//默认一个月数据
//			var start = document.getElementById("grid-div-toolbar-date-s"),
//				end = document.getElementById("grid-div-toolbar-date-e"),
//				date_e = new Date(),
//				date_s = Shell.util.clone(date_e);
//				
//			date_s = date_s.setMonth(date_s.getMonth() - 1);
//			start.value = Shell.util.Date.toString(date_s,true);
//			end.value = Shell.util.Date.toString(date_e,true);
			
			//分页栏按钮事件
			$("#grid-div-pagingtoolbar-first").bind("click",function(){
				var List = ShellPage.List;
				if(List.page > 1){
					List.page = 1;
					List.load(true);
				}
			});
			$("#grid-div-pagingtoolbar-prev").bind("click",function(){
				var List = ShellPage.List;
				if(List.page > 1){
					List.page--;
					List.load(true);
				}
			});
			$("#grid-div-pagingtoolbar-next").bind("click",function(){
				var List = ShellPage.List,
					page = parseInt(List.total / List.limit),
					num = List.total % List.limit;
				page = num == 0 ? page : page + 1;
				if(List.page < page){
					List.page++;
					List.load(true);
				}
			});
			$("#grid-div-pagingtoolbar-last").bind("click",function(){
				var List = ShellPage.List,
					page = parseInt(List.total / List.limit),
					num = List.total % List.limit;
				page = num == 0 ? page : page + 1;
				if(List.page < page){
					List.page = page;
					List.load(true);
				}
			});
			
			//设置列表抬头和内容的宽度
			var width = ShellPage.List.getGridWidth();
			
			$("#grid-div").css({width:width});
			$("#grid-div-head").css({width:width});
			$("#grid-div-body").css({width:width - 17});
			$("#content-chart-div").css({left:width + 4});
		},
		/**浏览器大小发生变化时处理*/
		onWindowResize:function(){
			var width = $(window).width(),
				height = $(window).height(),
				gridWidth = ShellPage.List.getGridWidth();
				
			var grid_div_height = height - 80 - 4,
				grid_div_form_height = grid_div_height - 29 - 29,
				grid_div_body_height = grid_div_form_height - 24,
				
				content_div_width = width - gridWidth - 6;
//				content_div_height = height > 384 ? 300 : 
//					(height - 80 > 0 ? height/2 - 40 : 0),
					
//				chart_div_top = 80 + content_div_height + 4,
//				chart_div_top = chart_div_top > height ? 0 : chart_div_top,
//				chart_div_width = content_div_width,
//				chart_div_height = grid_div_height - content_div_height - 4,
//				chart_div_body_height = chart_div_height - 29;
			
			//列表
			$("#grid-div").css({height:grid_div_height});
			//列表表体
			$("#grid-div-form").css({height:grid_div_form_height});
			//列表内容
			$("#grid-div-body-div").css({height:grid_div_body_height});
			//分页栏
			$("#grid-div-pagingtoolbar").css({top:grid_div_form_height + 28});
			//结果-图表
			$("#content-chart-div").css({width:content_div_width,height:grid_div_height});
//			//结果
//			$("#content-div").css({width:content_div_width,height:content_div_height});
//			//图表
//			$("#chart-div").css({top:chart_div_top,width:chart_div_width,height:chart_div_height});
//			$("#chart-div-body").css({height:chart_div_body_height});
			
			//图表大小变化
			if(ShellPage.Chart.chart){
				ShellPage.Chart.chart.resize();
			}
		}
	};
	
	/**公共方法*/
	ShellPage.Function = {
		/**显示错误信息*/
		showError:function(value){
			if($ && $.messager && $.messager.alert){
				$.messager.alert("错误信息",value,"error");
			}else{
				alert(value);
			}
		}
	};
	
	ShellPage.Init.onWindowResize();
	ShellPage.Init.init();
});

/**列表行点击事件*/
function onRowClick(tr,ReportFormID,SectionNo,SectionType){
	//选中行背景色改变
	var oldCheckedRow = ShellPage.List.checkedRow;
	if(oldCheckedRow) $(oldCheckedRow).css("background-color","");
	$(tr).css("background-color",ShellPage.List.CheckedRowBackgroundColor);
	ShellPage.List.checkedRow = tr;
	ShellPage.Chart.hideChart(true);
	ShellPage.Content.changeContent({
		ReportFormID:ReportFormID,
		SectionNo:SectionNo,
		SectionType:SectionType
	});
}
/**用于结果页面的行点击调用,变更历史对比信息*/
function printResult(PatNo,ItemNo,Table,ReceiveDate){
	ShellPage.Chart.changeChart({
		PatNo:PatNo,
		ItemNo:ItemNo,
		Table:Table,
		ReceiveDate:ReceiveDate
	});
}
/**清除排序信息*/
function clearSortInfo(thObj,list){
	if(!list || list.length == 0) return;
	for(var i=0;i<list.length;i++){
		var th = thObj[list[i]];
		if(th){
			$(th).attr("asc","");
			th.innerHTML = $(th).attr("defaultText");
		}
	}
}
/**列表排序*/
function onGridSort(th){
	var asc = $(th).attr("asc"),
		field = $(th).attr("field");
		
	if(asc == "true"){
		th.innerHTML = $(th).attr("defaultText") + "<img src='css/images/grid/sort_desc.gif'/>";
		$(th).attr("asc","false");
	}else{
		th.innerHTML = $(th).attr("defaultText") + "<img src='css/images/grid/sort_asc.gif'/>";
		$(th).attr("asc","true");
	}
	
	var ths = $(th).siblings("th"),
		len = ths.length,
		thsObj = {};
		
	for(var i=0;i<len;i++){
		var f = $(ths[i]).attr("field");
		if(f){
			thsObj[f] = ths[i];
		}
	}
	
	var order = ["order by "];
	switch(field){
		case "RECEIVEDATE" : 
			order.push(" receivedate " + (asc == "true" ? "desc" : "asc"));
			order.push(" ,receivetime " + ($(thsObj.RECEIVETIME).attr("asc") == "true" ? "asc" : "desc"));
			clearSortInfo(thsObj,["SAMPLENO","SampleType","CHECKDATE","CHECKTIME"]);
			break;
		case "RECEIVETIME" : 
			order.push(" receivedate " + ($(thsObj.RECEIVEDATE).attr("asc") == "true" ? "asc" : "desc"));
			order.push(",receivetime " + (asc == "true" ? "desc" : "asc"));
			clearSortInfo(thsObj,["SAMPLENO","SampleType","CHECKDATE","CHECKTIME"]);
			break;
		case "SAMPLENO" : 
			order.push(" sampleno " + (asc == "true" ? "desc" : "asc"));
			clearSortInfo(thsObj,["RECEIVEDATE","RECEIVETIME","SampleType","CHECKDATE","CHECKTIME"]);
			break;
		case "SampleType" : 
			order.push(" sampletype " + (asc == "true" ? "desc" : "asc"));
			clearSortInfo(thsObj,["RECEIVETIME","RECEIVETIME","SAMPLENO","CHECKDATE","CHECKTIME"]);
			break;
		case "CHECKDATE" : 
			order.push(" checkdate " + (asc == "true" ? "desc" : "asc"));
			order.push(" ,checktime " + ($(thsObj.CHECKTIME).attr("asc") == "true" ? "asc" : "desc"));
			clearSortInfo(thsObj,["RECEIVETIME","RECEIVETIME","SAMPLENO","SampleType","CHECKTIME"]);
			break;
		case "CHECKTIME" : 
			order.push(" checkdate " + ($(thsObj.CHECKDATE).attr("asc") == "true" ? "asc" : "desc"));
			order.push(" ,checktime " + (asc == "true" ? "desc" : "asc"));
			clearSortInfo(thsObj,["RECEIVETIME","RECEIVETIME","SAMPLENO","SampleType"]);
			break;
	}
	//排序属性
	ShellPage.List.orderby = order.join("");
	//加载数据
	ShellPage.List.load();
}