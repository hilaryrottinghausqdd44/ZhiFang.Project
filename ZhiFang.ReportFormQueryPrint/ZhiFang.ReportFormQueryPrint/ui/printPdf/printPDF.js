/**PDF延时处理信息*/
var pdfTimeoutInfo = {
	/**标题类型*/
	reportformtitle:Shell.util.Path.getRequestParams().reportformtitle || "",
	/**勾选的报告列表*/
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
    loadingCount: 0,
    /**打印信息*/
    printInfo:[],
    /**获取打印信息服务地址*/
    ReportPrintUrl: Shell.util.Path.rootPath + "/ServiceWCF/PrintService.svc/ReportPrint",

    //----------------------------------------------------------------
    /**纸张类型*/
    strPageName: "",
    /**A4纸张类型，1(A4) 2(16开)*/
    A4Type: 1,
    /**最大加载次数*/
    maxLoadingCount: 15,
    /**打印文件列表*/
    printList: [],
    /**合并的数量*/
    mergePageCount:10,
    /**合并文件服务A4*/
    DobuleA5MergeA4PDFFiles: "/ServiceWCF/ReportFormService.svc/DobuleA5MergeA4PDFFiles",
    Dobule32KMerge16KPDFFiles: "/ServiceWCF/ReportFormService.svc/Dobule32KMerge16KPDFFiles",
    /**开始准备打印*/
    startPrint: function () {
        pdfTimeoutInfo.printInfo = [];
        pdfTimeoutInfo.printList = [];

        if (pdfTimeoutInfo.strPageName == "双A5") {
            pdfTimeoutInfo.printDA5();
        } else {
            pdfTimeoutInfo.printA4A5();
        }
    },
    /**双A5打印*/
    printDA5: function () {
        var list = pdfTimeoutInfo.list,
            len = list.length,
            mergeCount = 0,
            mergeIndexs = [];

        for (var i = 0; i < len; i++) {
            var obj = list[i];
            if (obj.PageName == "A4") {//A4纸独开一张
                pdfTimeoutInfo.printList.push({
                    indexList: [].concat(mergeIndexs)
                });

                mergeCount = 0;
                mergeIndexs = [];

                pdfTimeoutInfo.printList.push({
                    url: obj.url,
                    indexList: [i]
                });
            } else {//连续的A5报告合并
                mergeIndexs.push(i);
                mergeCount += parseInt(obj.PageCount || "1");

                if (mergeCount >= pdfTimeoutInfo.mergePageCount) {
                    pdfTimeoutInfo.printList.push({
                        indexList: [].concat(mergeIndexs)
                    });

                    mergeCount = 0;
                    mergeIndexs = [];
                }
            }
        }

        if (mergeIndexs.length > 0) {
            pdfTimeoutInfo.printList.push({
                indexList: [].concat(mergeIndexs)
            });
        }

        pdfTimeoutInfo.printBegin();
    },
    /**A4/A5打印*/
    printA4A5: function () {
        var list = pdfTimeoutInfo.list,
           len = list.length;

        for (var i = 0; i < len; i++) {
            pdfTimeoutInfo.printList.push({
                url: list[i].url,
                indexList: [i]
            });
        }

        pdfTimeoutInfo.printBegin();
    },
    /**打印开始*/
    printBegin: function () {
        pdfTimeoutInfo.printInfo.push("<b>共 " + pdfTimeoutInfo.printList.length + " 份打印文件</b>");
        pdfTimeoutInfo.printInfo.push("<b>准备打印</b>");
        pdfTimeoutInfo.showPrintInfo();

        setTimeout("pdfTimeoutInfo.printPDF(0,true)", 1000);
    },
    /**合并文件*/
    fileMerge: function (indexList) {
        var list = pdfTimeoutInfo.list,
            A4Type = pdfTimeoutInfo.A4Type,
            urls = [];
            
        for (var i = 0; i < indexList.length; i++) {
            urls.push(list[indexList[i]].url);
        }

        var url = null;
        switch(A4Type){
            case 1 : url = pdfTimeoutInfo.DobuleA5MergeA4PDFFiles;break;
            case 2 : url = pdfTimeoutInfo.Dobule32KMerge16KPDFFiles;break;
        }
        url = Shell.util.Path.rootPath + url + "?fileList=" + urls.join(",");
        url = encodeURI(url);

        var value = null;

        $.ajax({
            dataType: 'json',
            contentType: 'application/json',
            url: url,
            async: false,
            success: function (result) {
                if (result.success) {
                    value = result.ResultDataValue;
                } else {
                    var errorInfo = "获取PDF文件路径失败！错误信息：" + result.ErrorInfo;
                    pdfTimeoutInfo.showError({ msg: errorInfo });
                }
            },
            error: function (request, strError) {
                var errorInfo = "获取PDF文件路径失败！错误信息：" + request.status;
                pdfTimeoutInfo.showError({msg:errorInfo});
            }
        });

        return value;
    },
    /**文件打印*/
    printPDF: function (index, loaded) {
        pdfTimeoutInfo.loadingCount++;
        if (pdfTimeoutInfo.loadingCount > pdfTimeoutInfo.maxLoadingCount) {
            pdfTimeoutInfo.printInfo.push("正在打印的第  <b>" + (index + 1) + "</b> 份文件无法加载");
            pdfTimeoutInfo.stopPrintPDF();
            return;
        }

        var iframe = window.frames["pdfWin_iframe"];

        pdfTimeoutInfo.printList[index].url = pdfTimeoutInfo.printList[index].url || 
            pdfTimeoutInfo.fileMerge(pdfTimeoutInfo.printList[index].indexList);

        if (!pdfTimeoutInfo.printList[index].url) {
            pdfTimeoutInfo.printInfo.push("正在打印的第  <b>" + (index + 1) + "</b> 份文件未获取到文件路径");
            pdfTimeoutInfo.stopPrintPDF();
            return;
        }

        if (!pdfTimeoutInfo.iframeLoading && loaded && pdfTimeoutInfo.printList[index].url) {
            pdfTimeoutInfo.iframeLoading = true;
            pdfTimeoutInfo.changeFrameContent(pdfTimeoutInfo.printList[index].url);
        }

        if (pdfTimeoutInfo.iframeLoading && iframe && iframe.document && iframe.document.readyState == "complete") {
            pdfTimeoutInfo.iframeLoading = false;
            if (iframe.GetIsNoPdf()) {
                iframe.PrintPdf();
                index++;
                pdfTimeoutInfo.loadingCount = 0;
                pdfTimeoutInfo.isPrinting(index);

                if (index < pdfTimeoutInfo.printList.length) {
                    //选中下一个
                    setTimeout("pdfTimeoutInfo.printPDF(" + index + ",true)", pdfTimeoutInfo.ftime);
                } else {
                    pdfTimeoutInfo.printIsOver();
                }
            }
        } else {
            setTimeout("pdfTimeoutInfo.printPDF(" + index + ",false)", pdfTimeoutInfo.wtime);
        }
    },
    /**正在打印中*/
    isPrinting: function (index) {
        var list = pdfTimeoutInfo.list,
            obj = pdfTimeoutInfo.printList[index - 1],
            indexList = obj.indexList,
            len = indexList.length,
            ids = [];

        pdfTimeoutInfo.printInfo.push("正在打印第  <b>" + index + "</b> 份文件...");

        var arr = ["<a style='margin-left:20px;'>【文件包含报告】</a>"];
        for (var i = 0; i < len; i++) {
            var row = list[indexList[i]];
            ids.push(row.ReportFormID);
            arr.push("<a style='margin-left:20px;'>" + row.RECEIVEDATE + " " + row.CNAME + 
                " " + row.PageName + " " + row.PageCount + "张</a>");
        }
        pdfTimeoutInfo.printInfo.push(arr.join("</br>"));

        pdfTimeoutInfo.showPrintInfo();

        //增加打印次数
        parent.Shell.util.Function.addPrintTimes(ids.join(","));
    },
    /**打印完成*/
    printIsOver: function () {
        pdfTimeoutInfo.printInfo.push("<b>打印完成</b>");
        pdfTimeoutInfo.showPrintInfo();
        pdfTimeoutInfo.endPrintDPF();
    },

    //----------------------------------------------------------------


    /**
	 * pdf打印
	 * @param index 当前打印的pdf下标
	 */
	printPDF2:function(index,loaded){
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
        pdfTimeoutInfo.printInfo.push("<b style='color:red;'>打印中断</b>");
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
   
    /**更改显示内容*/
    changeFrameContent:function(url){
    	var iframe = document.getElementById("pdfWin_iframe");
    	iframe.src = Shell.util.Path.rootPath + "/ReportPrint/PrintPDF.aspx?reportfile=" + url;
    },
    /**显示错误信息*/
	showError:function(config){
		var config = config || {},
			options = $("#pdfWin_grid").datagrid("options"),
			maxWidth = options.width - 20,
			maxHeight = options.height - 20,
			width = config.width || 250,
			height = config.height || 100;

        //控制台错误信息显示
		Shell.util.Msg.showLog(config.msg);

		config.msg = "<b style='color:red'>" + config.msg + "</b>";
			
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
    var info = parent.Shell.util.Function.getReportPrintInfo(),
		data = info.data;
    //纸张类型
    pdfTimeoutInfo.strPageName = info.strPageName;
    pdfTimeoutInfo.A4Type = info.A4Type;
		
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
					pdfTimeoutInfo.showError({ msg: "请选择需要打印的数据！"});
					return;
				}

                //开始打印
				pdfTimeoutInfo.startPrint();
			}
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
            { field: 'SectionType', title: '小组类型', hidden: true },
            { field: 'PageName', title: '纸张类型', hidden: true },
            { field: 'PageCount', title: '文件页数', hidden: true }
        ]],
	    onLoadSuccess:function(data){
			//默认选中第一行数据
			if(data.total > 0){
				$('#pdfWin_grid').datagrid("selectRow",0);
			}
		},
		onSelect:function(rowIndex,rowData){
			if(rowData.url){
				pdfTimeoutInfo.changeFrameContent(rowData.url);
			}else{
			    pdfTimeoutInfo.showError({
			        timeout: 4000,
			        width: 500,
			        height: 500,
			        msg: "数据行中不存在报告文件路径"
			    });
			}
		},
		onClickRow:function(rowIndex,rowData){
			$('#pdfWin_grid').datagrid("clearSelections");
			$('#pdfWin_grid').datagrid("selectRow",rowIndex);
		}
	});
});