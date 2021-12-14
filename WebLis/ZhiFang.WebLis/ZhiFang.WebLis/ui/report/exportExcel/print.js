$(function(){
    /**错误信息*/
    var errorInfo = [],
		/**结果类型*/
		reportType = "",
		/**结果信息条件*/
		reportParams = null,
		/**结果信息*/
		reportInfo = {},
		/**结果图窗口*/
		imageWin = null,
		/**开启结果图显示功能*/
		showReportImg = false,
		/**文件类型*/
		fileType = (Shell.util.Cookie.getCookie("ReportFormFileType") || "").toLocaleUpperCase(),
		/**服务地址*/
		serverUrl = {
		    /**获取报告列表服务地址*/
		    SelectReportListUrl:Shell.util.Path.rootPath + "/ServiceWCF/ReportFromService.svc/SelectReportList",
		    /**获取报告服务地址*/
		    GetPreviewReportByIdUrl:Shell.util.Path.rootPath + "/ServiceWCF/ReportFromService.svc/GetPreviewReportById",
		    /**获取结果服务地址*/
		    GetPreviewReportResultByIdUrl:Shell.util.Path.rootPath + "/ServiceWCF/ReportFromService.svc/GetPreviewReportResultById",
		    /**获取打印信息服务地址*/
		    ReportPrintUrl:Shell.util.Path.rootPath + "/ServiceWCF/PrintService.svc/ReportPrint",
		    /**获取结果图*/
		    GETImagesUrl:Shell.util.Path.rootPath + "/ServiceWCF/ReportFromService.svc/GetPreviewReportImageById"
		};
    //$.ajax()请求服务 gwh add 2015-3-4
    function askService(serviceType, entity, where,rowIndex) {
        var serviceParam = {},//请求服务参数
			async = serviceType == 'delete' ? false : true;//删除操作，同步执行$.ajax方法

        if(serviceType=='load'){
            serviceParam.data=entity;
        }
        serviceParam = setService(serviceType, serviceParam, where);//配置服务参数
        $.ajax({
            url: Shell.util.Path.rootPath + '/ServiceWCF/' + serviceParam.serviceName,
            data: serviceParam.data,
            dataType: 'json',
            type: serviceParam.type,
            timeout: 10000,
            async: async,
            contentType: 'application/json',//不加这个会出现错误
            success: function (data) {
                if (data.success) {
                    switch (serviceType) {
                        case 'delete':$('#report_grid').datagrid('deleteRow',rowIndex);
                            break;
                        case 'load':$('#report_grid').datagrid('loadData',data);
                            break;
                    }
                }
            },
            error: function (data) {
                $.messager.alert('提示信息', data.ErrorInfo, 'error');
            }
        });

    }

    //配置服务参数 gwh add 2015-3-4
    function setService(serviceType, serviceParam, where) {

        //数据请求方式（GET,POST）
        switch (serviceType) {
            case'delete':
                serviceParam.serviceName = 'ReportFromService.svc/DeleteReportForm';
                serviceParam.type = 'GET';
                serviceParam.data = where;
                break;
            case'print':
                serviceParam.serviceName = 'PrintService.svc/UpdatePrintTimeByReportFormID';
                serviceParam.type = 'GET';
                serviceParam.data = where;
                break;
            case'load':
                serviceParam.serviceName = 'ReportFromService.svc/SelectReportList2';//SelectReportList2
                serviceParam.type = 'POST';
                break;
        }

        return serviceParam;
    }

    /**查询*/
    function onSearch(){
        //alert("查询");
        var where = getWhere();
        if(!where){
            $.messager.alert("错误提示","必须输入开始日期,且查询时间不能超过90天!","error");
            return;
        }

        $('#report_grid').datagrid("load",{wherestr:where});
    }
    /**获取查询条件*/
    function getWhere(){
        var where = [],
			groupwhere = getGroupWhere(),
			textWhere = getTextWhere();

        if(groupwhere){
            where.push(groupwhere);
        }else{
            var receivedateWhere = getRECEIVEDATE();
            if(!receivedateWhere) return null;
            where.push(receivedateWhere);
        }

        if(textWhere){
            where.push(textWhere);
        }

        return where.join(" and ");
    }
    /**获取分组的条件*/
    function getGroupWhere(){
        var DateTimeType = $("#DateTimeType").combobox("getValue");
        var datetype="RECEIVEDATE";
        if(DateTimeType==1)
        {
            datetype="RECEIVEDATE";
        }
        if(DateTimeType==2)
        {
            datetype="CHECKDATE";
        }

        if($("#search-button-today").linkbutton("options").selected){
            var date = new Date(),
				date_s = Shell.util.Date.toString(date,true),
				date_e = Shell.util.Date.toString(Shell.util.Date.getNextDate(date),true);
            return datetype+"<'" + date_e + "' and "+datetype+">='" + date_s + "'";
        }
        if($("#search-button-3days").linkbutton("options").selected){
            var date = new Date(),
				date_s = Shell.util.Date.toString(Shell.util.Date.getNextDate(date,-2),true),
				date_e = Shell.util.Date.toString(Shell.util.Date.getNextDate(date),true);
            return datetype+"<'" + date_e + "' and "+datetype+">='" + date_s + "'";
        }
        if($("#search-button-7days").linkbutton("options").selected){
            var date = new Date(),
				date_s = Shell.util.Date.toString(Shell.util.Date.getNextDate(date,-6),true),
				date_e = Shell.util.Date.toString(Shell.util.Date.getNextDate(date),true);
            return datetype+"<'" + date_e + "' and "+datetype+">='" + date_s + "'";
        }
        if($("#search-button-all").linkbutton("options").selected){
            return "";
        }
    }
    /**获取核收日期区间条件*/
    function getRECEIVEDATE(){

        var DateTimeType = $("#DateTimeType").combobox("getValue");
        var datetype="RECEIVEDATE";
        if(DateTimeType==1)
        {
            datetype="RECEIVEDATE";
        }
        if(DateTimeType==2)
        {
            datetype="CHECKDATE";
        }

        var date_s = $("#search-text-RECEIVEDATE-S").datebox("getValue"),
			date_e = $("#search-text-RECEIVEDATE-E").datebox("getValue"),
			where = [];

        //开始日期必须存在,且结束日期-开始日期<=90
        if(!date_s) return null;
        var s = Shell.util.Date.getNextDate(date_s,90).getTime(),
			e = (date_e ? Shell.util.Date.getDate(date_e) : new Date()).getTime();
        if(s < e) return null;

        //生成时间条件
        if(date_s){where.push( datetype+">='" + Shell.util.Date.toString(date_s,true) + "'");}
        if(date_e){where.push( datetype+"<'" + Shell.util.Date.toString(Shell.util.Date.getNextDate(date_e),true) + "'");}

        return where.join(" and ");
    }
    /**获取输入框条件*/
    function getTextWhere(){
        var ClientNo = $("#search-text-ClientNo").combobox("getValue"),
			CNAME = $("#search-text-CNAME").searchbox("getValue"),
			GenderNo = $("#search-text-GenderNo").combobox("getValue"),
			SAMPLENO = $("#search-text-SAMPLENO").searchbox("getValue"),
			PATNO = $("#search-text-PATNO").searchbox("getValue"),
			PrintTime = $("#search-text-statues").combobox("getValue"),
			BarCode = $("#search-text-BarCode").searchbox("getValue"),
			where = [];

        if(ClientNo && ClientNo != "0"){where.push("ClientNo='" + ClientNo.replace(/'/g,"") + "'");}
        if(CNAME){where.push("CNAME like '%" + CNAME.replace(/'/g,"") + "%'");}
        if(GenderNo && GenderNo !="0"){where.push("GenderNo='" + GenderNo.replace(/'/g,"") + "'");}
        if(SAMPLENO){where.push("SAMPLENO='" + SAMPLENO.replace(/'/g,"") + "'");}
        if(PATNO){where.push("PATNO='" + PATNO.replace(/'/g,"") + "'");}
        if (BarCode) { where.push("SERIALNO='" + BarCode.replace(/'/g, "") + "'"); }
        if(PrintTime)
        {
            if(PrintTime==0)
            {
                where.push(" PRINTTIMES=0");
            }
            if(PrintTime==1)
            {
                where.push(" PRINTTIMES>=1");
            }
            if(PrintTime==2)
            {
            }
        }

        return where.join(" and ");
    }

    /**报告列表点击事件处理*/
    function onReportGridSelect(rowIndex,rowData){
        Shell.util.Msg.showLog(rowData.CNAME);

        changeContentInfo({
            reportformId:rowData.ReportFormID,
            sectionNo:rowData.SECTIONNO,
            sectionType:rowData.SectionType
        });
    }
    /**报告/结果内容更改*/
    function changeContentInfo(params){
        //报告编号、小组编号、小组类别
        var reportformId = params.reportformId,
			sectionNo = params.sectionNo,
			sectionType = params.sectionType,
			error = [];

        if(!reportformId){error.push("<b style='color:red;'>报告编号reportformId<b>");}
        if(!sectionNo){error.push("<b style='color:red;'>小组编号sectionNo<b>");}
        if(!sectionType){error.push("<b style='color:red;'>小组类别sectionType<b>");}

        if(error.length > 0){
            var text = error.join(" ") + "<b style='color:black;'> 缺失,请传递这些参数!<b>"
            Shell.easyuiUtil.Msg.showError(text);
            return;
        }
        //延时处理
        Shell.util.Action.delay(function(){
            reportInfo = {};//清空结果
            reportParams = params;//条件重新赋值
            loadReportInfo(params);
        });
    }
    /**加载报告结果数据*/
    function loadReportInfo(params){
        //加载结果数据
        GetClientNoData(params,function(result,type){
            var content = document.getElementById("content"),
				html = "";
            if(result.success){
                html = Shell.util.JSON.decode(result.ResultDataValue) || "";
            }else{
                html = result.ErrorInfo;
            }
            reportInfo["L" + type] = html;
            content.innerHTML = html;
        });
        //加载结果图
        if(showReportImg){
            GetImageList(params.reportformId,function(result){
                if(result.success){
                    var list = Shell.util.JSON.decode(result.ResultDataValue) || [];
                    showImageWin(list);
                }else{
                    showImageWin([]);
                }
            });
        }
    }
    /**显示结果图窗口*/
    function showImageWin(list){
        var len = list.length,
			html = [];

        if(!imageWin) {initImageWin();}

        if(len == 0){
            imageWin.window("minimize");
            return;
        }

        for(var i=0;i<len;i++){
            html.push("<img width='100%' height='100%' src='" + Shell.util.Path.rootPath + "/" + list[i].replace(/\\/g,"\/") + "'></img>");
        }
        imageWin.window({content:html.join("")});
        imageWin.window("open");

    }
    /**初始化结果图窗口*/
    function initImageWin(){
        imageWin = $("#imageWin").window({
            title:"报告结果图",
            width:400,
            height:400,
            minimizable:false,//不可最小化
            collapsible:false,//不可折叠
            onBeforeClose:function(){
                $("#imageWin").window("minimize");
                return false;
            }
        }).window("minimize");
    }

    /**结果类型变化处理*/
    function onReportTypeChange(){
        var type = getRadioValueByName("reportType");
        if(reportInfo["L" + type]){
            var content = document.getElementById("content");
            content.innerHTML = reportInfo["L" + type];
        }else{
            if(reportParams.reportformId && reportParams.sectionNo && reportParams.sectionType){
                loadReportInfo(reportParams);
            }
        }
    }

    /**分组按钮处理*/
    function onButtonClick(type){
        switch(type){
            case "today" : disabledRECEIVEDATE(true);onSearch();break;
            case "3days" : disabledRECEIVEDATE(true);onSearch();break;
            case "7days" : disabledRECEIVEDATE(true);onSearch();break;
            case "all" : disabledRECEIVEDATE(false);break;
        }
    }
    /**禁用核收日期框*/
    function disabledRECEIVEDATE(bo){
        var action = bo ? "disable" : "enable";
        $("#search-text-RECEIVEDATE-S").datebox(action);
        $("#search-text-RECEIVEDATE-E").datebox(action);
    }
    /**清空结果信息*/
    function clearContent(){
        reportParams = null;
        reportInfo = {};
        var content = document.getElementById("content");
        content.innerHTML = "";
    }

    /**打印报告*/
    function printReport(preview){
        var list = $('#report_grid').datagrid("getChecked") || [],
			len = list.length,
			printConfigInfo = getPrintConfigInfo(),
			reportformtitle = printConfigInfo.titleType,
			urls = [],
			error = [];

        if(len == 0){
            $.messager.alert("提示信息","请勾选需要打印的数据!","error");
            return;
        }

        if(fileType == "PDF"){
            winPdfPrint();
        }else if(fileType == "JPG" || fileType == "JPEG"){
            for(var i=0;i<len;i++){
                GetReportPrintList(list[i].ReportFormID,reportformtitle,function(result){
                    if(result.success){
                        var list = Shell.util.JSON.decode(result.ResultDataValue) || [];
                        if(list.length == 1){urls.push(list[0]);}
                    }else{
                        error.push("<b style='color:red;'>" + result.ErrorInfo + "</b>");
                    }
                });
            }
            if(error.length > 0){
                Shell.easyuiUtil.Msg.show({
                    title:"错误信息",
                    msg:"<b style='color:red;'>" + error.join(" ") + "</b>"
                });
            }else{
                winLodopPrint(urls,preview);
            }
        }else{
            $.messager.alert("错误提示","打印方式没有配置或配置错误！","error");
        }
    }
    /**Lodop页面打印*/
    function winLodopPrint(list,preview){
        var lodop = Shell.util.Print.getLodopObj("报告单打印"),
			printConfigInfo = getPrintConfigInfo(),
			intOrient = parseInt(printConfigInfo.orientationType),
			strPageName = printConfigInfo.paperType;

        lodop.SET_PRINT_PAGESIZE(intOrient,0,0,strPageName);//方向 1:纵;2:横

        for(var i=0;i<list.length;i++){
            lodop.NEWPAGE();
            //lodop.ADD_PRINT_IMAGE(0,0,"100%","100%","<img border='0' src='" + Shell.util.Path.rootPath + "/" + list[i] + "'width='100%'/>");
            lodop.ADD_PRINT_IMAGE(0,0,"100%","100%","URL:" + Shell.util.Path.rootPath + "/" + list[i]);
            lodop.SET_PRINT_STYLEA(0,"Stretch",2);//按原图比例(不变形)缩放模式
        }

        //预览打印/直接打印
        if(preview){
            if(lodop.PREVIEWB()>0){//返回来的数值，表示打印的次数
                for(var i=0;i<ReportFormIDArr.length;i++)
                    //记录打印次数
                    askService('print',null,{ReportFormID:ReportFormIDArr[i]});
            }
        }else{
            var print=lodop.PRINT();//返回true,表示打印成功
            if(print){
                for(var i=0;i<ReportFormIDArr.length;i++)
                    //记录打印次数
                    askService('print',null,{ReportFormID:ReportFormIDArr[i]});
            }
        }
        onSearch();
    }
    function getReportCheckedData(){
        var data = $('#report_grid').datagrid("getChecked") || [];
        return data;
    }
    /**pdf页面打印*/
    function winPdfPrint(){
        var printConfigInfo = getPrintConfigInfo(),
			reportformtitle = printConfigInfo.titleType;

        var url = Shell.util.Path.uiPath + "/report/print/printPDF.html",
			data = $('#report_grid').datagrid("getChecked") || [],
			len = data.length,
			d = [],
			ids = [];

        for(var i=0;i<len;i++){
            ids.push(data[i].ReportFormID);
        }
        url += "?ids=" + ids.join(",") + "&reportformtitle=" + reportformtitle;
        //var SN = Shell.util.Path.getRequestParams()["SN"];
        parent.getReportCheckedData = parent.getReportCheckedData || getReportCheckedData;
        parent.OpenWindowFuc("PDF打印","96%","96%",url);
    }

    /**获取打印类型设置*/
    function getPrintConfigInfo(){
        var titleType = $("#titleType").combobox("getValue"),
			paperType = $("#paperType").combobox("getValue"),
			orientationType = $("#orientationType").combobox("getValue");

        return {
            titleType:titleType,
            paperType:paperType,
            orientationType:orientationType
        };
    }

    /**获取单选项的值*/
    function getRadioValueByName(name){
        if(!name) return null;
        var list = document.getElementsByName(name) || [],
			len = list.length;
        for(var i=0;i<len;i++){
            if(list[i].checked){
                return list[i].value;
            }
        }
        return null;
    }

    /**获取报告结果*/
    function GetClientNoData(params,callback){
        var type = getRadioValueByName("reportType"),
			url = "";

        switch(type){
            case "1" : url = serverUrl.GetPreviewReportByIdUrl;break;
            case "2" : url = serverUrl.GetPreviewReportResultByIdUrl;break;
        }

        if(url) url += "?reportformId=" + params.reportformId + "&sectionNo=" + params.sectionNo + "&sectionType=" + params.sectionType;
        url = encodeURI(url);

        $.ajax({
            dataType:'json',
            async:false,//同步请求
            contentType:'application/json',
            url:url,
            success:function(result){
                callback(result,type);
            },
            error:function(request,strError){
                Shell.util.Msg.showLog("获取报告结果失败！错误信息：" + strError);
                callback({success:false,ErrorInfo:"获取报告结果失败！错误信息：" + strError},type);
            }
        });
    }
    /**获取打印信息列表*/
    function GetReportPrintList(reportformId,reportformtitle,callback){
        var url = serverUrl.ReportPrintUrl + "?reportformId=" + reportformId + "&reportformtitle=" + reportformtitle + "&reportformfiletype=JPG&printtype=1";
        url = encodeURI(url);
        $.ajax({
            dataType:'json',
            async:false,//同步请求
            contentType:'application/json',
            url:url,
            success:function(result){
                callback(result);
            },
            error:function(request,strError){
                Shell.util.Msg.showLog("获取打印信息列表失败！错误信息：" + strError);
                callback({success:false,ErrorInfo:"获取打印信息列表失败！错误信息：" + strError});
            }
        });
    }
    /**获取结果图*/
    function GetImageList(reportformId,callback){
        var url = serverUrl.GETImagesUrl + "?reportformId=" + reportformId;
        url = encodeURI(url);
        $.ajax({
            dataType:'json',
            async:false,//同步请求
            contentType:'application/json',
            url:url,
            success:function(result){
                callback(result);
            },
            error:function(request,strError){
                Shell.util.Msg.showLog("获取结果图失败！错误信息：" + strError);
                callback({success:false,ErrorInfo:"获取结果图失败！错误信息：" + strError});
            }
        });
    }

    var date = new Date(),
		date_s = Shell.util.Date.toString(date,true),
		date_e = Shell.util.Date.toString(Shell.util.Date.getNextDate(date),true);

    //列表-刷新
    $("#report_grid_toolbar-refresh").bind('click', function(){onSearch();});
    //列表-直接打印
    $("#report_grid_toolbar-print").bind('click', function(){printReport();});
    //列表-预览打印
    $("#report_grid_toolbar-preview").bind('click', function(){printReport(true);});

    //项目类型列表
    $('#report_grid').datagrid({
        fit:true,
        border:false,
        fitColumns:true,
        rownumbers:true,
        loadMsg:'数据加载中...',
        //		url:serverUrl.SelectReportListUrl,
        //		queryParams:{
        //			wherestr:"RECEIVEDATE<'" + date_e + "' and RECEIVEDATE>='" + date_s + "'"
        //		},
        method:'get',
        idField:'ReportFormID',
        pagination:true,
        pageSize:10,
        pageList:[10,20,50,100,200,500],
        checkOnSelect:false,
        selectOnCheck:false,
        toolbar:'#report_grid_toolbar',
        columns:[[
			{field:'ReportFormID',title:'主键',checkbox:true},
			{field:'RECEIVEDATE',title:'核收日期',width:100,formatter:function(value,index,row){
			    if(!value) return "";
			    return value.slice(0,10).replace(/\//g,"-");
			},tooltip:function(value,index,row){
			    if(!value) return "";
			    return "<b>" + value.slice(0,10).replace(/\//g,"-") + "</b>";
			}},
			{field:'CNAME',title:'名称',width:100,tooltip:function(value,index,row){
			    return "<b>" + value + "</b>";
			}},
			{field:'SAMPLENO',title:'样本号',width:100},
			{field:'PRINTTIMES',title:'打印次数',width:100},
			{field:'SECTIONNO',title:'检验小组编号',hidden:true},
			{field:'CLIENTNO',title:'送检单位编码',hidden:true},
			{field:'SectionType',title:'小组类型',hidden:true}
        ]],
        loadFilter:function(data){
            if(data.success){
                $('#report_grid').datagrid("clearChecked");
                return Shell.util.JSON.decode(data.ResultDataValue);
            }else{
                Shell.easyuiUtil.Msg.show({
                    title:"错误信息",
                    msg:"<b style='color:red;'>" + data.ErrorInfo + "</b>"
                });
                return {"total":0,"rows":[]};
            }
        },
        onBeforeLoad:function(params){
            if(params.page == 0) return false;
        },
        onLoadSuccess:function(data){
            //默认选中第一行数据
            if(data.total == 0){
                clearContent();
            }else{
                $('#report_grid').datagrid("selectRow",0);
            }
        },
        onSelect:function(rowIndex,rowData){
            onReportGridSelect(rowIndex,rowData);
        },
        onClickRow:function(rowIndex,rowData){
            $('#report_grid').datagrid("clearSelections");
            $('#report_grid').datagrid("selectRow",rowIndex);
        }
    });

    //按钮-今天
    $('#search-button-today').bind('click',function(){onButtonClick("today");});
    //按钮-3天
    $('#search-button-3days').bind('click',function(){onButtonClick("3days");});
    //按钮-7天
    $('#search-button-7days').bind('click',function(){onButtonClick("7days");});
    //按钮-核收日期
    $('#search-button-all').bind('click',function(){onButtonClick("all");});

    //核收日期
    var date = Shell.util.Date.toString(new Date(),true);
    $("#search-text-RECEIVEDATE-S").datebox({disabled:true,value:date});
    $("#search-text-RECEIVEDATE-E").datebox({disabled:true,value:date});
    $("#search-text-RECEIVEDATE-S").next('.combo').find('input').bind('keydown',function(e){
        if(e.keyCode==13){onSearch();}
    });
    $("#search-text-RECEIVEDATE-E").next('.combo').find('input').bind('keydown',function(e){
        if(e.keyCode==13){onSearch();}
    });

    //送检单位
    $('#search-text-ClientNo').combobox({
        height:22,width:200,
        valueField:"ClIENTNO",
        textField:"CNAME",
        editable:true,
        method:'GET',
        url:Shell.util.Path.rootPath + "/ServiceWCF/DictionaryService.svc/GetClientListByRBAC?page=1&rows=1000&fields=CLIENTELE.CNAME,CLIENTELE.ClIENTNO",
        loadFilter:function(data){
            data = data || [];
            if(data.length > 0){data[0].selected = true;}
            $('#report_grid').datagrid("options").url = serverUrl.SelectReportListUrl;
            Shell.util.Action.delay(onSearch);
            return data;
        }
    });
    //查询-姓名
    $("#search-text-CNAME").searchbox({height:22,width:100,prompt:'请输入姓名',searcher:onSearch});
    //查询-性别
    $("#search-text-GenderNo").combobox({
        height:22,width:60,
        valueField:"GenderNo",
        textField:"CName",
        editable:false,
        panelMaxHeight:130,
        method:'GET',
        url:Shell.util.Path.rootPath + "/ServiceWCF/DictionaryService.svc/GetPubDict?tableName=GenderType&fields=GenderNo,CName",
        loadFilter:function(data){
            if(data.success){
                var obj = Shell.util.JSON.decode(data.ResultDataValue) || {};
                var list = obj.rows || [];
                list.unshift({"GenderNo":0,"CName":"全部","selected":true});
                return list;
            }else{
                return [{"GenderNo":0,"CName":"全部","selected":true}];
            }
        }
    });
    //查询-样本号
    $("#search-text-SAMPLENO").searchbox({height:22,width:150,prompt:'请输入样本号',searcher:onSearch});
    //查询-病历号
    $("#search-text-PATNO").searchbox({height:22,width:150,prompt:'请输入病历号',searcher:onSearch});
    //查询-条码号
    $("#search-text-BarCode").searchbox({height:22,width:150,prompt:'请输入条码号',searcher:onSearch});
    //查询-状态
    $("#search-text-statues").combobox({
        height: 22,
        width: 70,
        valueField: 'statues',
        textField: 'text',
        editable: false,
        panelHeight: 80,
        data: [
            { statues: 0, text: '未打印' },
            { statues: 1, text: '已打印' },
            { statues: 2, text: '全部' }
        ],
        onLoadSuccess: function () {
            $(this).combobox('select', 0);
        }

    });
    //报告/结果切换
    $("#reportType2").bind('onchange',onReportTypeChange);

    //初始化结果图窗口
    initImageWin();

    //PDF方式没有设置打印选项
    if(fileType == "PDF"){
        $("#printConfig").hide();
    }
});
