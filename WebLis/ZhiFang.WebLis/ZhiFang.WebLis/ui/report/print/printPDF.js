/**PDF延时处理信息*/
var pdfTimeoutInfo = {
	/**标题类型*/
	reportformtitle:Shell.util.Path.getRequestParams().reportformtitle || "",
	/**pdf地址列表*/
	list:[],
	/**延时处理对象*/
	stime:null,
	/**循环间隔时间*/
    ftime:5000,
    /**加载等待时间*/
    wtime:2000,
    /**iframe内容加载中*/
    iframeLoading:false,
    /**加载次数*/
    loadingCount:0,
    /**打印信息*/
    printInfo:[],
    /**获取打印信息服务地址*/
	ReportPrintUrl:Shell.util.Path.rootPath + "/ServiceWCF/PrintService.svc/ReportPrint",
    /**
	 * pdf打印
	 * @param index 当前打印的pdf下标
	 */

	//记录打印次数 gwh add 2015-03-04
	printCount:function(reportID){
		$.ajax({
			url: Shell.util.Path.rootPath + '/ServiceWCF/PrintService.svc/UpdatePrintTimeByReportFormID',
			data: {ReportFormID:reportID},
			dataType: 'json',
			type: 'GET',
			timeout: 10000,
			async: true,//改成false试试
			contentType: 'application/json',//不加这个会出现错误
			success: function (data) {
				if (data.success) {
				}
			},
			error: function (data) {
				$.messager.alert('提示信息', data.ErrorInfo, 'error');
			}
		});
	},
	printPDF:function(index,loaded){
		pdfTimeoutInfo.loadingCount++;
		if(pdfTimeoutInfo.loadingCount > 10){
			pdfTimeoutInfo.printInfo.push("正在打印的第  <b>" + (index + 1) + "</b> 份文件无法加载");
			pdfTimeoutInfo.printInfo.push("<b style=\"color:red;\">打印中断</b>");
        	pdfTimeoutInfo.showPrintInfo();
            pdfTimeoutInfo.endPrintDPF();
			return;
		}
		
		var iframe = window.frames["pdfWin_iframe"];
			
		pdfTimeoutInfo.list[index].url = pdfTimeoutInfo.list[index].url || pdfTimeoutInfo.getPDFUrl(pdfTimeoutInfo.list[index].ReportFormID,pdfTimeoutInfo.reportformtitle);
		
		if(!pdfTimeoutInfo.list[index].url){
			pdfTimeoutInfo.printInfo.push("正在打印的第  <b>" + (index + 1) + "</b> 份文件未获取到文件路径");
			pdfTimeoutInfo.printInfo.push("<b style=\"color:red;\">打印中断</b>");
        	pdfTimeoutInfo.showPrintInfo();
            pdfTimeoutInfo.endPrintDPF();
			return;
		}
		
		if (!pdfTimeoutInfo.iframeLoading && loaded && pdfTimeoutInfo.list[index].url){
			pdfTimeoutInfo.iframeLoading = true;
			pdfTimeoutInfo.changeFrameContent(pdfTimeoutInfo.list[index].url);
        }
			
        if(pdfTimeoutInfo.iframeLoading && iframe && iframe.document && iframe.document.readyState == "complete"){
        	pdfTimeoutInfo.iframeLoading = false;
	        if (iframe.GetIsNoPdf()) {
	            iframe.PrintPdf();
				var ReportFormID=pdfTimeoutInfo.list[index]['ReportFormID'];
				pdfTimeoutInfo.printCount(ReportFormID);//记录打印次数 gwh add 2015-3-4
	            index++;
	            pdfTimeoutInfo.loadingCount = 0;
	            pdfTimeoutInfo.printInfo.push("正在打印第  <b>" + index + "</b> 份文件...");
	            pdfTimeoutInfo.showPrintInfo();
	            
	            if(index < pdfTimeoutInfo.list.length){
	            	//选中下一个
					setTimeout("pdfTimeoutInfo.printPDF(" + index + ",true)",pdfTimeoutInfo.ftime);
	            }else{
	            	pdfTimeoutInfo.printInfo.push("<b>打印完成</b>");
	            	pdfTimeoutInfo.showPrintInfo();
	                pdfTimeoutInfo.endPrintDPF();
	            }
	        }
	    }else{
        	setTimeout("pdfTimeoutInfo.printPDF(" + index + ",false)",pdfTimeoutInfo.wtime);
		}
	},
	/**显示打印的信息*/
	showPrintInfo:function(config){
		var win = pdfTimeoutInfo.infoWin,
			content = pdfTimeoutInfo.printInfo.join("<br/>");
			
		if(!win){
			var config = config || {},
				maxWidth = document.body.clientWidth - 20,
				maxHeight = document.body.clientHeight - 20,
				width = config.width || 280,
				height = config.height || 500;
			win = $("#messager").window({
				title:"打印进度信息",
				content:"<div style='padding:10px;'>" + content + "</div>",
				width:(maxWidth > width ? width : maxWidth),
				height:(maxHeight > height ? height : maxHeight),
				inline:true,//显示在父容器中
				minimizable:false,//不可最小化
				maximizable:false,//不可最大化
				collapsible:false,//不可折叠
				draggable:false,//不可拖拽
				resizable:false,//不可改变大小
				modal:true//模态
			});
			win.window('open').window("center");
		}else{
			win.window({content:"<div style='padding:10px;'>" + content + "</div>"});
		}
	},
	/**停止打印*/
    stopPrintPDF:function(){
    	if(pdfTimeoutInfo.printInfo.length == 0) return;
        pdfTimeoutInfo.printInfo.push("<b style='color:red;'>已停止打印</b>");
        pdfTimeoutInfo.showPrintInfo();
        pdfTimeoutInfo.endPrintDPF();
    },
    /**打印结束*/
    endPrintDPF:function(){
    	if (pdfTimeoutInfo.stime != null) {
            clearTimeout(pdfTimeoutInfo.stime);
            pdfTimeoutInfo.list = [];
        }
        pdfTimeoutInfo.printInfo = [];
    },
    /**获取PDF文件路径*/
    getPDFUrl:function(reportformId,reportformtitle,callback){
    	var url = pdfTimeoutInfo.ReportPrintUrl + "?reportformId=" + reportformId + "&reportformtitle=" + reportformtitle + "&reportformfiletype=JPG&printtype=1";
		url = encodeURI(url);
		
		var value = null;
		
		$.ajax({ 
			dataType:'json',
			contentType:'application/json',
			url:url,
			async:false,
			success:function(result){
				if(callback){
					callback(result);
				}else{
					if(result.success){
						var list = Shell.util.JSON.decode(result.ResultDataValue) || [];
						value = (list.length == 0 ? null : list[0]);
					}
				}
			},
			error:function(request,strError){
				Shell.util.Msg.showLog("获取PDF文件路径失败！错误信息：" + strError);
				if(callback) callback({success:false,ErrorInfo:"获取PDF文件路径失败！错误信息：" + strError});
			}
		});
		
		if(!callback) return value;
    },
    /**更改显示内容*/
    changeFrameContent:function(url){
    	var iframe = document.getElementById("pdfWin_iframe");
		url=encodeURI(url);
    	iframe.src = Shell.util.Path.rootPath + "/ReportPrint/PrintPDF.aspx?reportfile=../" + url;
    },
    /**显示错误信息*/
	showError:function(config){
		var config = config || {},
			options = $("#pdfWin_grid").datagrid("options"),
			maxWidth = options.width - 20,
			maxHeight = options.height - 20,
			width = config.width || 250,
			height = config.height || 100;
			
		if(maxWidth < width) width = maxWidth;
		if(maxHeight < height) height = maxHeight;
			
		$.messager.show({
			title:config.title || "错误消息",
			timeout:config.timeout || 1000,
			width:width,
			height:height,
			msg:config.msg,
			showType:config.showType || 'show',
			style:config.style || {left:'10px',top:'2px'}
		});
	}
};

$(function(){
	var data = parent.getReportCheckedData();
	$('#pdfWin_grid').datagrid({
		fit:true,
		border:false,
		fitColumns:true,
		rownumbers:true,
		loadMsg:'数据加载中...',
		method:'get',
		idField:'ReportFormID',
		data:data,
		checkOnSelect:false,
		selectOnCheck:false,
		toolbar:[{
			text:'打印',
			iconCls:'button-print',
			handler:function(){
				pdfTimeoutInfo.list = $('#pdfWin_grid').datagrid("getChecked") || [];
				
				if(pdfTimeoutInfo.list.length == 0){
					pdfTimeoutInfo.showError({
						msg:"<b style='color:red'>请选择需要打印的数据！"
					});
					return;
				}
				pdfTimeoutInfo.printInfo = [];
				pdfTimeoutInfo.printInfo.push("<b>共 " + pdfTimeoutInfo.list.length + " 份文件</b>");
				pdfTimeoutInfo.printInfo.push("<b>准备打印</b>");
				pdfTimeoutInfo.showPrintInfo();
				
//				pdfTimeoutInfo.printPDF(0,true);
				setTimeout("pdfTimeoutInfo.printPDF(0,true)",1000);
			}
//		},{
//			text:'停止',
//			iconCls:'button-cancel',
//			handler:function(){pdfTimeoutInfo.stopPrintPDF();}
		}],
		columns:[[
            {field:'ReportFormID',title:'主键',checkbox:true},
            {field:'RECEIVEDATE',title:'核收日期',width:100,formatter:function(value,index,row){
            	if(!value) return "";
            	return value.slice(0,10).replace(/\//g,"_");
            },tooltip:function(value,index,row){
            	if(!value) return "";
            	return "<b>" + value.slice(0,10).replace(/\//g,"_") + "</b>";
            }},
            {field:'CNAME',title:'名称',width:100,tooltip:function(value,index,row){
            	return "<b>" + value + "</b>";
            }},
            {field:'SAMPLENO',title:'样本号',width:100},
            {field:'SECTIONNO',title:'检验小组编号',hidden:true},
            {field:'CLIENTNO',title:'送检单位编码',hidden:true},
            {field:'SectionType',title:'小组类型',hidden:true}
        ]],
	    onLoadSuccess:function(data){
			//默认选中第一行数据
			if(data.total > 0){
				$('#pdfWin_grid').datagrid("selectRow",0);
			}
		},
		onSelect:function(rowIndex,rowData){
			//if(rowData.url){
			//	pdfTimeoutInfo.changeFrameContent(rowData.url);
			//}else{
				//获取PDF文件路径
				pdfTimeoutInfo.getPDFUrl(rowData.ReportFormID,pdfTimeoutInfo.reportformtitle,function(result){
					if(result.success){
						var list = Shell.util.JSON.decode(result.ResultDataValue) || [];
						rowData.url = list.length == 0 ? null : list[0];
						pdfTimeoutInfo.changeFrameContent(rowData.url);
					}else{
						pdfTimeoutInfo.showError({
							timeout:4000,
							width:500,
							height:500,
							msg:"<b style='color:red;'>" + result.ErrorInfo + "</b>"
						});
					}
				});
			//}
		},
		onClickRow:function(rowIndex,rowData){
			$('#pdfWin_grid').datagrid("clearSelections");
			$('#pdfWin_grid').datagrid("selectRow",rowIndex);
		}
	});
});