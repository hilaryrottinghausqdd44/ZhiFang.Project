$(function () {
	//加载字典服务
	var GET_PUBDICT_URL = Shell.util.Path.rootPath + "/ServiceWCF/DictionaryService.svc/GetPubDict";
	//获取消费单信息服务
	//var GET_CONSUME_URL = "consume_info.json";
	var GET_CONSUME_URL = Shell.util.Path.rootPath + "/ServiceWCF/NRequestFromService.svc/OSConsumerUserOrderForm";
	/**根据检验项目ID查询检验明细服务*/
	//var GET_ITEMS_URL = "consume_items.json";
	var GET_ITEMS_URL = Shell.util.Path.rootPath + "/ServiceWCF/DictionaryService.svc/GetTestDetailByItemID";
	//获取检验项目列表数据服务
	var GET_TESTITEM_URL = Shell.util.Path.rootPath + "/ServiceWCF/DictionaryService.svc/GetTestItem";
	//保存申请单数据服务
	//var NREQUESTFORM_ADD_OR_UPDATE_URL = "save.json";
	var NREQUESTFORM_ADD_OR_UPDATE_URL = Shell.util.Path.rootPath + "/ServiceWCF/NRequestFromService.svc/SaveOSConsumerUserOrderForm";
	//取消消费采样服务
	var UN_CONSUME_URL = Shell.util.Path.rootPath + "/ServiceWCF/NRequestFromService.svc/UnConsumerUserOrderForm";
    
	//订单套餐DIV原始背景色
	var PACKAGE_DIV_BACKGROUND_COLOR = "#C0E7FE";
    //订单套餐DIV鼠标晃过背景色
	var PACKAGE_DIV_MOUSE_MOVE_COLOR = "#33CCFF";
	//订单套餐DIV前缀
	var PACKAGE_DIV_PREFIX = "itemdiv";
	//订单套餐列表列数
	var PACKAGE_DIV_COLUMNS_NUM = 1;
	
	//送检单位列表
	var CLIENT_NO_LIST = null;
	//区域字典列表
	var CLIENT_AREA_LIST = null;
	//性别字典列表
	var SEX_LIST = null;
	//年龄单位字典列表
	var AGE_UNIT_LIST = null;
	//就诊类型字典列表
	var SICK_TYPE_LIST = null;
	
	//订单套餐及明细数据
	var PACKAGE_DATA = {};
	//是否存在送检单位项目
	var HAS_CLIENT_ITEM = false;
	//当前点击的套餐DOM元素
	var CLICKED_PACKAGE_DIV = null;
	//当前用户消费码
	var PAY_CODE = null;
	//是否已经按了回车键
	var CHECK_ENTER = false;
	
	//初始化组件
    function initComponent(){
    	//送检单位下拉框
    	$('#form_ClientNo').combobox({
    		valueField: "ClIENTNO",
	        textField: "CNAME",
	        //editable: false,
	        required: true,
	        method: 'GET',
	        onHidePanel:function () {
		        var value = $('#form_ClientNo').combobox("getValue");
		        if (!value || value == "0") {
		            var data = $('#form_ClientNo').combobox('getData');
					$('#form_ClientNo').combobox('select',data[0].ClIENTNO);
		        }
		    },
	        onChange: function (newValue, oldValue) {
		        var len = (CLIENT_AREA_LIST || []).length,
					rows = $('#form_ClientNo').combobox("getData"),
					AreaID = null;
		
		        for (var i in rows) {
		            if (newValue && (rows[i].ClIENTNO + "") == newValue) {
		                AreaID = rows[i].AreaID;
		                break;
		            }
		        }
		
		        if (AreaID == null) return;
		
		        for (var i = 0; i < len; i++) {
		            if (CLIENT_AREA_LIST[i].AreaID == AreaID) {
		                $('#form_AreaNo').combobox("loadData", [{
		                	AreaID: CLIENT_AREA_LIST[i].AreaID,
		                	ClientNo: CLIENT_AREA_LIST[i].ClientNo,
		                    AreaCName: CLIENT_AREA_LIST[i].AreaCName,
		                    selected: true
		                }]);
		                break;
		            }
		        }
		    }
    	});
    	//区域-只读
	    $('#form_AreaNo').combobox({
	    	valueField: "ClientNo",
	        textField: "AreaCName",
	        readonly: true
	    });
    	//性别-只读
	    $('#form_GenderNo').combobox({
	    	valueField: "GenderNo",
	        textField: "CName",
	        readonly: true
	    });
	    //年龄单位-可编辑
	    $('#form_AgeUnitNo').combobox({
	    	valueField: "AgeUnitNo",
	        textField: "CName",
	        readonly: false
	    });
	    //就诊类型-只读
	    $('#form_jztype').combobox({
	    	valueField: "SickTypeNo",
	        textField: "CName",
	        readonly: true
	    });
	    
	    //条码信息列表
	    $("#barcode_grid").datagrid({
	        //fit: true,
	        fitColumns: true,
	        singleSelect: true,
	        loadMsg: '数据加载中...',
	        method: 'get',
	        idField: 'ColorValue',
	        toolbar: [{
	            iconCls: 'icon-save',
	            text: '保存',
	            id: "save_barcode_list",
	            handler: onSaveInfo
	        }],
	        columns: [[{
	        	field: 'ColorName', title: '颜色名称', width: 40,
			    tooltip: function (value, index, row) { return "<b>" + value + "</b>"; },
			    formatter: function (val, row, index) { return "<b>" + val + "</a>"; }
			},{
            	field: 'ColorValue', title: '颜色值', width: 30,
                styler: function (val, row, index) { return "background-color:" + val + ";"; },
                formatter: function () { return ""; }
           },{
           		field: 'BarCode', title: '条码值', width: 140,
			    styler: function (val, row, index) { return "background-color:" + row.ColorValue + ";"; },
			    formatter: function (value, row, index) {
			        var input =
						"<input id='barcode_list_row_value_" + row.ColorValue + "' style='width:100%' value='" + (row.BarCode || "") + "'/>";
			        return input;
			    }
			},{
				field: 'SampleType', title: '样本类型', width: 60,
			    styler: function (val, row, index) { return "background-color:" + row.ColorValue + ";"; },
			    formatter: function (value, row, index) {
			        var list = row.SampleTypeDetail || [],
						len = list.length,
						arr = [];
			        arr.push(
						"<select id='barcode_list_row_type_" + row.ColorValue + "'>"
					);

			        var selected = false;
			        for (vari = 0; i < len; i++) {
			            if (list[i].selected) {
			                selected = true; break;
			            }
			        }
			        if (len > 0 && !selected) {
			            list[0].selected = true;
			        }

			        for (var i = 0; i < len; i++) {
			            arr.push("<option value='" + list[i].SampleTypeID + "'" + (list[i].selected ? " selected='selected'" : "") + ">" + list[i].CName + "</option>");
			        }

			        arr.push("</select>");
			        return arr.join("");
			    }
			}]]
	    });
	    
	    //消费码回车事件
	    $("#payCode").textbox('textbox').keydown(function (e) {
            if (e.keyCode == 13) {
            	if(CHECK_ENTER) return;//已经回车的继续回车无效
            	CHECK_ENTER = true;//已经按了回车键
            	//$("#form_PatNo").textbox('textbox').focus();
            	//$("#payCode").textbox('textbox').blur();//光标移除
            	//一秒后光标复位，防止快速回车
            	setTimeout(function(){
            		CHECK_ENTER = false;//回车重新启用
            	},1000);
            	onPayCodeEnter();//消费码回车处理
            }
		});
        //消费码-默认光标
        $("#payCode").textbox('textbox').focus(); 
    }
    //初始化组件数据
    function initComponentData(){
    	$('#form_ClientNo').combobox("loadData", CLIENT_NO_LIST);
    	$('#form_AreaNo').combobox("loadData", CLIENT_AREA_LIST);
    	$('#form_GenderNo').combobox("loadData", SEX_LIST);
    	$('#form_AgeUnitNo').combobox("loadData", AGE_UNIT_LIST);
    	$('#form_jztype').combobox("loadData", SICK_TYPE_LIST);
    	
    	//送检单位-默认勾选第一个
    	$('#form_ClientNo').combobox("setValue", CLIENT_NO_LIST[0].ClIENTNO);
    	
    	//就诊类型-默认勾选"特需"
    	var sickTypeNo = null;
    	for(var i in SICK_TYPE_LIST){
    		if(SICK_TYPE_LIST[i].CName == "特需"){
    			sickTypeNo = SICK_TYPE_LIST[i].SickTypeNo;
    		}
    	}
    	//$('#form_jztype').combobox("setValue", SICK_TYPE_LIST[0].SickTypeNo);
    	$('#form_jztype').combobox("setValue", sickTypeNo);
    	
    	//年龄单位-默认"岁"
    	var AgeUnitNo = null;
    	for(var i in AGE_UNIT_LIST){
    		if(AGE_UNIT_LIST[i].CName == "岁"){
    			AgeUnitNo = AGE_UNIT_LIST[i].AgeUnitNo;
    			break;
    		}
    	}
    	$('#form_AgeUnitNo').combobox("setValue", AgeUnitNo);
    	
    	//采样人，从cookie获取
    	var EmployeeName = Shell.util.Cookie.getCookie("EmployeeName");
    	$('#form_Operator').textbox("setValue",EmployeeName);
    	
    	clearPatientInfo();//清空与患者相关数据
    }
    
    //消费码回车处理
    function onPayCodeEnter(){
    	var payCode = $("#payCode").textbox('getText');//消费码
    	
    	//获取最后一次消费码信息
    	var LastBarcodeInfo = getLastBarcodeInfo();
    	//输入的消费码为空
    	if(!payCode){
    		//最后一次消费码为空
    		if(!LastBarcodeInfo || !LastBarcodeInfo.PayCode){
    			return;
    		}else{
    			initLastBarcodeInfo(function(isSame){
    				if(isSame){
    					return;
    				}
    			});
    		}
    	}
    	
    	//消费码不变，不做处理
    	if(payCode == LastBarcodeInfo.PayCode){
    		return;
    	}
    	//弹出切换提示框
    	confirmUnConsume(function(){
    		PAY_CODE = payCode;//用户消费码
	    	$("#payCode").blur();
	    	
	    	//初始化Local里面存在锁定消费码
	    	initLastBarcodeInfo(function(){
	    		//获取消费单信息
		    	getConsumeInfo(payCode,function(data){
		    		if(data.success){
		    			clearPatientInfo();//清空与患者相关数据
		    			var info = Shell.util.JSON.decode(data.ResultDataValue) || {};
		    			
		    			////消费码-区域严格校验
		    			//var isValid = isValidByAreaId(info.AreaID);
		    			//if(!isValid){
		    			//	$.messager.alert('','该消费码不能在当前区域内消费','error');
		    			//	return;
		    			//}
		    			
		    			//获取所有套餐明细数据
		    			getPackageItemsData(info.UserOrderItem,function(){
		    				initConsumeInfo(info);//初始化消费单信息
			    			changePackageTable(info.UserOrderItem);//修改订单套餐列表内容
			    			changeBarcodeTable();//修改条码列表
		    			});
		    			
		    			//最后一次用户消费码相关信息
		    			LastBarcodeInfo.PayCode = payCode;//消费码
				    	LastBarcodeInfo.WeblisSourceOrgID = $("#form_ClientNo").combobox("getValue");//送检单位ID
				    	LastBarcodeInfo.WeblisSourceOrgName = $("#form_ClientNo").combobox("getText");//送检单位名称
				    	LastBarcodeInfo.ConsumerAreaID = $('#form_AreaNo').combobox("getData")[0].AreaID;//区域ID
				    	setLastBarcodeInfo(LastBarcodeInfo);
		    		}else{
		    			$.messager.alert('',data.ErrorInfo,'error');
		    		}
		    	});
	    	});
    	});
    }
    //弹出切换提示框
    function confirmUnConsume(callback){
    	//获取最后一次消费码信息
    	var LastBarcodeInfo = getLastBarcodeInfo();
    	//Local里面存在锁定消费码
		if(LastBarcodeInfo && LastBarcodeInfo.PayCode){
			if(!confirm('您是否要切换消费码？')){
				$('#payCode').textbox("setValue",LastBarcodeInfo.PayCode);//消费码
				return;
			}else{
				callback();
			}
		}else{
			callback();
		}
    }
    //消费码是否与区域匹配
    function isValidByAreaId(AreaID){
    	var isValid = false;
    	var AreaList =  $("#form_AreaNo").combobox("getData");
    	
    	if(AreaList.length > 0){
    		var CheckedAreaID = AreaList[0].AreaID;
	    	if(AreaID && (AreaID + "") == (CheckedAreaID + "")){
	    		isValid = true;
	    	}
    	}
    	
    	return isValid;
    }
    
    //清空与患者相关数据
    function clearPatientInfo(){
    	clearConsumeInfo();//清空消费单信息
		clearPackageTable();//清空订单套餐列表
		clearPackageItemsTable();//清空订单套餐明细列表
		clearBarcodeTable();//清空条码列表
		
		//年龄单位-默认"岁"
    	var AgeUnitNo = null;
    	for(var i in AGE_UNIT_LIST){
    		if(AGE_UNIT_LIST[i].CName == "岁"){
    			AgeUnitNo = AGE_UNIT_LIST[i].AgeUnitNo;
    			break;
    		}
    	}
    	$('#form_AgeUnitNo').combobox("setValue", AgeUnitNo);
    }
    
    //清空消费单信息
    function clearConsumeInfo(){
    	$('#form_CName').textbox("setValue","");//姓名
    	$('#form_Age').numberbox("setValue","");//年龄
    	$('#form_GenderNo').combobox("setValue","");//性别
    	$('#form_PatNo').textbox("setValue","");//病历号
    	$('#form_DeptName').textbox("setValue","");//科室
    	$('#form_DoctorName').textbox("setValue","");//医生
    	$('#form_Charge').textbox("setValue","");//收费
    	$('#form_Diag').textbox("setValue","");//临床诊断
    	
    	$('#form_CollectDate').datetimebox("setValue","");//采样时间
    	$('#form_OperDate').datetimebox("setValue","");//开单时间
    }
    //初始化消费单信息
    function initConsumeInfo(data){
    	//年龄单位
    	var SexId = null;
    	for(var i in SEX_LIST){
    		if(SEX_LIST[i].CName == data.SexName){
    			SexId = SEX_LIST[i].GenderNo;
    		}
    	}
    	//当前时间
    	var now = new Date();
    	now = Shell.util.Date.toString(now);
    	
    	$('#form_CName').textbox("setValue",data.Name);//姓名
    	$('#form_Age').numberbox("setValue",data.Age);//年龄
    	$('#form_GenderNo').combobox("setValue",SexId);//性别
    	$('#form_PatNo').textbox("setValue",data.PatNo);//病历号
    	$('#form_DeptName').textbox("setValue",data.DeptName);//科室
    	$('#form_DoctorName').textbox("setValue",data.DoctorName);//医生
    	$('#form_Charge').numberbox("setValue",data.Price);//收费
    	$('#form_Diag').textbox("setValue",data.DoctorMemo);//临床诊断
    	
    	$('#form_CollectDate').datetimebox("setValue",now);//采样时间
    	$('#form_OperDate').datetimebox("setValue",now);//开单时间
    }
    
    //获取送检单位字典数据
    function GetClientNoList(callback) {
    	var url = Shell.util.Path.rootPath + 
    		"/ServiceWCF/DictionaryService.svc/GetClientListByRBAC?page=1&rows=1000&fields=CLIENTELE.CNAME,CLIENTELE.ClIENTNO";
    	$.ajax({
            dataType: 'json',
            contentType: 'application/json',
            url: url,
            success: function (result) {
            	Shell.util.Msg.showLog("获取送检单位字典成功");
                callback(result);
            },
            error: function (request, strError) {
            	Shell.util.Msg.showError("获取送检单位字典数据失败！错误信息：" + strError);
            }
        });
    }
    //获取区域字典列表数据
    function GetClientAreaList(callback) {
    	getPubDictList("区域","ClientEleArea","AreaID,AreaCName,ClientNo",callback);
    }
    //加载性别字典列表
    function getSexList(callback){
    	getPubDictList("性别","GenderType","GenderNo,CName",callback);
    }
    //加载年龄单位字典列表数据
    function getAgeUnitList(callback){
    	getPubDictList("年龄单位","AgeUnit","AgeUnitNo,CName",callback);
    }
    //加载就诊类型字典列表数据
    function getSickTypeList(callback){
    	getPubDictList("就诊类型","SickType","SickTypeNo,CName",callback);
    }
    /**
     * 加载字典列表数据
     * @param {Object} info 字典名称
     * @param {Object} tableName 字典表名
     * @param {Object} fields 获取的数据字段
     * @param {Object} callback 回调函数
     */
    function getPubDictList(info,tableName,fields,callback){
    	$.ajax({
            dataType: 'json',
            contentType: 'application/json',
            url: GET_PUBDICT_URL + "?tableName=" + tableName + "&fields=" + fields,
            success: function (result) {
            	if (result.success) {
                    Shell.util.Msg.showLog("获取" + info + "字典成功");
                    var data = Shell.util.JSON.decode(result.ResultDataValue) || {},
						list = data.rows || [];
                    callback(list);
                } else {
                    Shell.util.Msg.showError("获取" + info + "字典失败！错误信息：" + result.ErrorInfo);
                }
            },
            error: function (request, strError) {
            	Shell.util.Msg.showError("获取" + info + "字典失败！错误信息：" + strError);
            }
        });
    }
    
    //获取消费单信息
    function getConsumeInfo(payCode,callback){
    	onMaskShow('消费单信息加载中，请稍候……');//弹出遮罩层
        var entity= {
        	PayCode:payCode,
        	WeblisSourceOrgID:$("#form_ClientNo").combobox("getValue"),//送检单位ID
	    	WeblisSourceOrgName:$("#form_ClientNo").combobox("getText"),//送检单位名称
	    	ConsumerAreaID:$('#form_AreaNo').combobox("getData")[0].AreaID//区域ID
        };
        
        $.ajax({
            type: 'post',
            dataType: 'json',
            contentType: 'application/json',
            url: GET_CONSUME_URL,
            data: Shell.util.JSON.encode({ jsonentity: entity }),
            success: function (result) {
            	onMaskHide();//取消遮罩层
                if (result.success) {
                	callback(result);
                } else {
                    $.messager.alert("", "获取消费单信息失败！错误信息：" + result.ErrorInfo, "error");
                }
            },
            error: function (request, strError) {
            	onMaskHide();//取消遮罩层
                $.messager.alert("", strError, "error");
            }
        });
    }
    
    //获取消费单套餐明细信息
    function getPackageItems(ItemNo,callback){
    	var ClientNo = $("#form_ClientNo").combobox("getValue");
    	var AreaNo = $('#form_AreaNo').combobox("getValue");
    	var labcode = HAS_CLIENT_ITEM ? ClientNo : AreaNo;
    	var url = GET_ITEMS_URL + "?itemid=" + ItemNo + "&labcode=" + labcode;
    	$.ajax({
            dataType: 'json',
            contentType: 'application/json',
            url: url,
            success: function (result) {
            	Shell.util.Msg.showLog("获取消费单套餐明细信息成功");
                callback(result);
            },
            error: function (request, strError) {
                Shell.util.Msg.showError("获取消费单套餐明细失败！错误信息：" + strError);
            }
        });
    }
    //获取一条送检单位的项目，如果存在则获取本身的项目，否则获取区域项目
    function hasClientItem(labcode,callback){
        $.ajax({
            dataType: 'json',
            contentType: 'application/json',
            url: GET_TESTITEM_URL + "?supergroupno=COMBI&page=1&rows=1&labcode=" + labcode,
            success: function (result) {
                if (result.success) {
                    var data = Shell.util.JSON.decode(result.ResultDataValue) || {total:0,rows:[]};
                    var bo = data.total == 0 ? false : true;
                    callback(bo);
                } else {
                    callback(false);
                }
            },
            error: function (request, strError) {
                callback(false);
            }
        });
    }
    
    //保存申请单信息
    function onNrequestFormAddOrUpdate(entity, callback) {
        $.ajax({
            type: 'post',
            dataType: 'json',
            contentType: 'application/json',
            url: NREQUESTFORM_ADD_OR_UPDATE_URL,
            data: Shell.util.JSON.encode({ jsonentity: entity }),
            success: function (result) {
                if (result.success) {
                	callback();
                } else {
                    $.messager.alert("", "保存申请单信息失败！错误信息：" + result.ErrorInfo, "error");
                }
            },
            error: function (request, strError) {
                $.messager.alert("", strError, "error");
            }
        });
    }
    
    //弹出遮罩层
	function onMaskShow(msg) {
		msg = '<div style="text-align:center;">' + msg + '</div>';
		$.messager.progress({
			//title: '提示', 
			msg: msg,
			text: '' 
		});
	}
	//取消遮罩层  
	function onMaskHide() {
		$.messager.progress('close');
	}
	
	//清空订单套餐列表
    function clearPackageTable(){
    	var divs = $("div[mark^='" + PACKAGE_DIV_PREFIX + "']") || [],
			len = divs.length;
			
        for (var i = 0; i < len; i++) {
            $(divs[i]).tooltip("destroy");
        }
        $("#package_grid").empty(); //清空内容
        PACKAGE_DATA = {};//订单套餐及明细数据
    }
	//修改订单套餐列表
	function changePackageTable(list){
		var table = createPackageTable(list,PACKAGE_DIV_COLUMNS_NUM);
		$("#package_grid").html(table); //加上内容
		
		var count = 0;
		for (var i in PACKAGE_DATA) {
            var div = $("div[mark='" + i + "']");
            div.on("click", onItemDivClick);
            count++;
        }
		if(count > 0){
			div[0].click();
		}
	}
	//创建订单套餐列表
    function createPackageTable(list,cellNum) {
        var rows = list || [],
			len = rows.length,
			count = cellNum || 5,
			table = [];
        
        if(len == 0){
        	return "<div style='padding:20px 10px;text-align:center;color:#169ada;'>没有套餐数据！</div>"
        }

        table.push("<table style='width:100%;'>");
        for (var i = 0; i < len; i++) {
            if (i % count == 0) {
                table.push(i == 0 ? "<tr>" : "</tr><tr>");
            }
            var td =
				"<td>" +
					"<div style='padding:10px 5px;text-align:center;cursor:pointer;border:1px solid #e0e0e0;' mark='" + 
						PACKAGE_DIV_PREFIX + list[i].ItemNo + "' " +
						"ItemNo='" + list[i].ItemNo + "'>" +
						"<span>" + list[i].Name + "</br>(" + list[i].ItemNo + ")</span>" +
					"</div>" +
				"</td>";

            table.push(td);
        }
        if (len > 0) table.push("</tr>");
        table.push("</table>");

        return table.join("");
    }
	
	//套餐单元点击事件处理
    function onItemDivClick(e) {
    	if(CLICKED_PACKAGE_DIV){
    		$(CLICKED_PACKAGE_DIV).removeClass("package-div-checked");
    	}
    	CLICKED_PACKAGE_DIV = this;
    	$(CLICKED_PACKAGE_DIV).addClass("package-div-checked");
    	
    	var ItemNo = $(this).attr("ItemNo");
    	var items = PACKAGE_DATA[PACKAGE_DIV_PREFIX + ItemNo].items;
    	clearPackageItemsTable();//清空订单套餐明细列表
        changePackageItemsTable(items);//修改订单套餐明细列表
    }
    
	//清空订单套餐明细列表
    function clearPackageItemsTable(){
        $("#items_grid").empty();//清空内容
        $('#save_barcode_list').linkbutton('disable');//保存按钮禁用
    }
    //修改订单套餐明细列表
    function changePackageItemsTable(list){
    	var table = createPackageItemsTable(list);
		$("#items_grid").html(table); //加上内容
		$('#save_barcode_list').linkbutton('enable');//启用保存按钮
    }
    //创建订单套餐明细列表
    function createPackageItemsTable(list) {
        var rows = list || [],
			len = rows.length,
			count = 1,
			html = [];

        if(len == 0){
        	return "<div style='padding:20px 10px;text-align:center;color:#169ada;'>没有套餐明细数据！</div>"
        }

        html.push("<table style='width:100%;'>");
        html.push("<thead style='text-align:center;'><th>编码</th><th>名称</th><th>英文名</th><th>价格</th></thead>");
        html .push('<tbody>');
        for (var i = 0; i < len; i++) {
            var tr =
				"<tr style='color:#ffffff;background-color:" + list[i].ColorValue + "'>" +
					"<td style='padding:5px;'>" + list[i].ItemNo + "</td>" +
					"<td style='padding:5px;'>" + list[i].CName + "</td>" +
					"<td style='padding:5px;'>" + list[i].EName + "</td>" +
					"<td style='padding:5px;'>" + list[i].Prices + "</td>" +
				"</tr>";

            html.push(tr);
        }
        
        html.push("</tbody></table>");

        return html.join("");
    }
    
	//获取所有套餐明细数据
	function getPackageItemsData(list,callback){
		var len = list.length;
		for(var i=0;i<len;i++){
			PACKAGE_DATA[PACKAGE_DIV_PREFIX + list[i].ItemNo] = list[i];
		}
		
		var hasError = false;
		//获取一个项目明细
		function getItem(index,call){
			if(index >= list.length){
				call();
				return;
			}
			var ItemNo = list[index].ItemNo;
			getPackageItems(ItemNo,function(data){
				if(data.success){
					var items = Shell.util.JSON.decode(data.ResultDataValue) || [];
					PACKAGE_DATA[PACKAGE_DIV_PREFIX + ItemNo].items = items;
					getItem(++index,call);
				}else{
					hasError = true;
				}
			});
		}
		
		onMaskShow("套餐明细加载中，请稍候……");//弹出遮罩层
		
		var ClientNo = $("#form_ClientNo").combobox("getValue");
		
		hasClientItem(ClientNo,function(data){
			HAS_CLIENT_ITEM = data;//是否存在送检单位项目
			//开始获取
			getItem(0,function(){
				onMaskHide()//取消遮罩层
				if(hasError){
					$.messager.alert('','套餐明细加载出错，请重新扫描消费码！','error',function(){
						clearPatientInfo();//清空与患者相关数据
            			$('#payCode').textbox("setValue","");//消费码
						//$("#payCode").textbox('textbox').focus();//消费码-默认光标
					});
				}else{
					callback();
				}
			});
		});
	}
	
	//清空条码列表
    function clearBarcodeTable(){
        $("#barcode_grid").datagrid("loadData",{total:0,rows:[]});//清空内容
    }
	//修改条码列表
    function changeBarcodeTable() {
        var list = getAllBarcode();
        $("#barcode_grid").datagrid("loadData",{total:list.length,rows:list});
    }
    //获取所有条码管列表
    function getAllBarcode(){
    	var info = {};
    		
    	for(var i in PACKAGE_DATA){
    		var items = PACKAGE_DATA[i].items,
    			iLen = items.length;
    			
    		for(var j=0;j<iLen;j++){
    			if(!items[j].ColorValue) continue;
    			
    			//试管颜色对象
    			info[items[j].ColorValue] = info[items[j].ColorValue] || {};
    			//试管颜色值
    			info[items[j].ColorValue].ColorValue = info[items[j].ColorValue].ColorValue || items[j].ColorValue;
    			//试管颜色中文
    			info[items[j].ColorValue].ColorName = info[items[j].ColorValue].ColorName || items[j].ColorName;
    			//该颜色试管包含的项目
    			info[items[j].ColorValue].ItemList = info[items[j].ColorValue].ItemList || [];
    			info[items[j].ColorValue].ItemList.push(items[j].ItemNo);
    			//条码
    			info[items[j].ColorValue].BarCode = "";
    			//样本类型列表
    			info[items[j].ColorValue].SampleTypeDetail = info[items[j].ColorValue].SampleTypeDetail || items[j].SampleTypeDetail;
    		}
    	}
    	
    	var list = [];
    	for(var i in info){
    		list.push(info[i]);
    	}
    	
    	return list;
    }
    
    //保存申请单数据
    function onSaveInfo(){
    	if(!PAY_CODE){
    		$.messager.alert('','请扫描消费码','error');
    		return;
    	}
    	
    	var jsonentity = {
    		PayCode:PAY_CODE,//消费码
            flag: "1",
            //申请单信息
            NrequestForm: getNrequestForm(),
            //组合项目列表
            CombiItems: getCombiItems(),
            //条码列表
            BarCodeList: getBarCodeList()
        };
        
        if (jsonentity.CombiItems.length == 0) {
        	$.messager.alert('', '订单套餐不能为空！', 'error');
        	return;
        }
        if (!jsonentity.BarCodeList) {
        	$.messager.alert('', '条码值或样本类型存在错误！', 'error');
        	return;
        }
        if (jsonentity.BarCodeList.length == 0) {
        	$.messager.alert('', '条码列表不能为空！', 'error');
        	return;
        }
        
        //判断条码的唯一性
        var BarCodeList = jsonentity.BarCodeList,
        	barcodeLength = BarCodeList.length;
        for (var i = 0; i < barcodeLength-1; i++) {
        	var isValid = barcodeIsValid(BarCodeList[i].BarCode);
        	if(isValid !== true){
        		$.messager.alert('提示', isValid, 'info');
                return;
        	}
            for (var j = i+1; j < barcodeLength; j++) {
                if (BarCodeList[i].BarCode == BarCodeList[j].BarCode) {
                    $.messager.alert('', '条码号不能有相同的！', 'info');
                    return;
                }
            }
        }
		
        onNrequestFormAddOrUpdate(jsonentity, function () {
        	setLastBarcodeInfo({});//清空Local数据
        	
            //成功提示，关闭提示框时清空消费单数据
            $.messager.alert('', '申请单保存成功！', 'info',function(){
            	clearPatientInfo();//清空与患者相关数据
            	$('#payCode').textbox("setValue","");//消费码
        		//$("#payCode").textbox('textbox').focus(); //消费码-默认光标
            });
        });
    }
    //获取申请单信息
    function getNrequestForm(){
    	var form = {
            NRequestFormNo: "0", //申请号
            ClientNo: $("#form_ClientNo").combobox("getValue") || "0",//送检单位编号
            ClientName: $("#form_ClientNo").combobox("getText"),//送检单位名称
            AreaNo: $("#form_AreaNo").combobox("getValue"),//区域编号
            AreaID:$('#form_AreaNo').combobox("getData")[0].AreaID,//区域ID
            AreaName:$('#form_AreaNo').combobox("getData")[0].AreaCName,//区域名称
            
            CName: $("#form_CName").textbox("getValue"), //姓名
            Age: $("#form_Age").numberbox("getValue"), //年龄
            AgeUnitNo: $("#form_AgeUnitNo").combobox("getValue"),//年龄单位编号
            AgeUnitName: $("#form_AgeUnitNo").combobox("getText"),//年龄单位名称
            GenderNo: $("#form_GenderNo").combobox("getValue"), //性别编号
            GenderName: $("#form_GenderNo").combobox("getText"), //性别名称
            PatNo: $("#form_PatNo").textbox("getValue"), //病历号
            
			jztype: $("#form_jztype").combobox("getValue"),//就诊类型
            jztypeName: $("#form_jztype").combobox("getText"),//就诊类型名称
            DeptName: $("#form_DeptName").textbox("getText"),//科室名称
            DoctorName: $("#form_DoctorName").textbox("getText"),//医生名称
            
            Charge: $("#form_Charge").numberbox("getValue") || "0", //收费
            Diag: $("#form_Diag").textbox("getValue"), //诊断结果
            Operator: $("#form_Operator").textbox("getValue"), //采样人
			
            OperDate: Shell.util.Date.toServerDate($("#form_OperDate").datetimebox("getValue")), //开单日期
            OperTime: Shell.util.Date.toServerDate($("#form_OperDate").datetimebox("getValue")), //开单时间
            CollectDate: Shell.util.Date.toServerDate($("#form_CollectDate").datetimebox("getValue")), //采样日期
            CollectTime: Shell.util.Date.toServerDate($("#form_CollectDate").datetimebox("getValue")), //采样时间
			
            SampleTypeNo:"0",//给样本类型默认一个值，签收的时候样本类型不能为空
            SampleType:"0"
        };
        
        //如果送检单位存在项目，则区域编码=送检单位编码
        if(HAS_CLIENT_ITEM){
        	form.AreaNo = form.ClientNo;
        }
        
        return form;
    }
    //获取组合项目列表
    function getCombiItems(){
        var info = PACKAGE_DATA,
        	combiItem = [];//组合项目（套餐）
        	
        for(var i in info){
        	var items = info[i].items || [],
				length = items.length,
				CombiItemDetailList = [];
				
			for (var j = 0; j < length; j++) {
                CombiItemDetailList.push({
                    CombiItemDetailNo: items[j].ItemNo,
                    CombiItemDetailName: items[j].CName
                });
            }
				
        	combiItem.push({
                CombiItemName: info[i].Name,
                CombiItemNo: info[i].ItemNo,
                CombiItemDetailList: CombiItemDetailList
            });
        }

        return combiItem;
    }
    //获取条码列表
    function getBarCodeList(){
    	var barcodeVlaueList = $("input[id^='barcode_list_row_value_']") || [],
        	barocdeTypeList = $("select[id^='barcode_list_row_type_']") || [],
        	grid = $("#barcode_grid"),
    		rows = grid.datagrid("getRows") || [],
			len = rows.length,
			barCodeList = [];
			
		//条码值和样本类型的值处理
		for (var i = 0; i < len; i++) {
            var vColor = barcodeVlaueList[i].id.split("_").slice(-1)[0];
            var vIndex = grid.datagrid("getRowIndex", vColor);
            rows[vIndex].BarCode = $(barcodeVlaueList[i]).val();
            
            var tColor = barocdeTypeList[i].id.split("_").slice(-1)[0];
            var tIndex = grid.datagrid("getRowIndex", tColor);
            rows[tIndex].SampleType = $(barocdeTypeList[i]).val();
        }

        for (var i = 0; i < len; i++) {
            if (!rows[i].BarCode) return null;
            if (!rows[i].SampleTypeDetail || rows[i].SampleTypeDetail.length == 0) return null;
            barCodeList.push({
                ColorValue: rows[i].ColorValue, //颜色值
                ColorName: rows[i].ColorName, //颜色名称
                BarCode: rows[i].BarCode, //条码值
                SampleType: rows[i].SampleType || rows[i].SampleTypeDetail[0].SampleTypeID, //样本类型
                ItemList: rows[i].ItemList//项目列表(id字符串数组)
            });
        }

        return barCodeList;
    }
	
    //初始化数据
    function initData(callback){
		onMaskShow('基础数据加载中，请稍候……');//弹出遮罩层
    	//需要加载送检单位字典,区域字典,性别字典,年龄单位字典,就诊类型字典
    	GetClientNoList(function(list){
    		CLIENT_NO_LIST = list || [];//送检单位字典
    		GetClientAreaList(function(list){
	    		CLIENT_AREA_LIST = list || [];//区域字典
	    		getSexList(function(list){
		    		SEX_LIST = list || [];//性别字典
		    		getAgeUnitList(function(list){
			    		AGE_UNIT_LIST = list || [];//年龄单位字典
			    		getSickTypeList(function(list){
				    		SICK_TYPE_LIST = list || [];//就诊类型字典
				    		onMaskHide();//取消遮罩层  
				    		callback();
				    	});
			    	});
		    	});
	    	});
    	});
    }
    
    /**
     * 校验条码格式
     * @version 2017-07-19
     * @param {Object} barcode
     */
    function barcodeIsValid(barcode){
    	var bar = barcode.replace(/^\s|\s$/g, '#');
    	var error = "条码: " + bar + " 格式错误，正确格式: 长度12位的纯数字！";
    	
    	if(!barcode) return error;//没有值
    	if(barcode.length != 12) return error;//非12位
    	if(isNaN(barcode)) return error;//非数字
    	
    	return true;
    }
    
    //设置最后一次消费码信息
    function setLastBarcodeInfo(info){
    	var str = Shell.util.JSON.encode(info) || '';
    	localStorage.setItem('LastBarcodeInfo', str);
    }
    //获取最后一次消费码信息
    function getLastBarcodeInfo(){
    	var str = localStorage.getItem('LastBarcodeInfo') || '';
    	var info = Shell.util.JSON.decode(str) || {};
    	return info;
    }
    //初始化Local里面存在锁定消费码
    function initLastBarcodeInfo(callback){
    	//获取最后一次消费码信息
    	var LastBarcodeInfo = getLastBarcodeInfo();
    	
    	//Local里面存在锁定消费码
    	if(LastBarcodeInfo && LastBarcodeInfo.PayCode){
    		//取消消费码锁定
    		UnConsume(LastBarcodeInfo.PayCode,function(data){
    			clearPatientInfo();//清空与患者相关数据
    			callback(false);
    		});
    	}else{
    		callback(true);
    	}
    }
    //取消消费码锁定
	function UnConsume(payCode,callback){
		onMaskShow('消费码锁定取消中，请稍候……');//弹出遮罩层
		
		//获取最后一次消费码信息
    	var LastBarcodeInfo = getLastBarcodeInfo();
    	
        var entity= {
        	PayCode:payCode,
        	WeblisSourceOrgID:LastBarcodeInfo.WeblisSourceOrgID,
        	WeblisSourceOrgName:LastBarcodeInfo.WeblisSourceOrgName,
        	ConsumerAreaID:LastBarcodeInfo.ConsumerAreaID
        };
        $.ajax({
            type: 'post',
            dataType: 'json',
            contentType: 'application/json',
            url: UN_CONSUME_URL,
            data: Shell.util.JSON.encode({ jsonentity: entity }),
            success: function (result) {
            	onMaskHide();//取消遮罩层
            	setLastBarcodeInfo({});//清空Local数据
                if (result.success) {
                	callback();
                } else {
                	callback();
                    //$.messager.alert("", "取消消费码锁定失败！错误信息：" + result.ErrorInfo, "error");
                }
            },
            error: function (request, strError) {
            	onMaskHide();//取消遮罩层
            	callback();
                //$.messager.alert("", strError, "error");
            }
        });
	}
    
    $("#unlock").on("click",function(){
    	var WeblisSourceOrgID = $("#form_ClientNo").combobox("getValue");//送检单位ID
		var WeblisSourceOrgName = $("#form_ClientNo").combobox("getText");//送检单位名称
		var ConsumerAreaID = $('#form_AreaNo').combobox("getData")[0].AreaID;//区域ID
    	var url = "../unlock/grid.html?v=2018010503&" +
    		"WeblisSourceOrgID=" + WeblisSourceOrgID +
    		"&WeblisSourceOrgName=" + WeblisSourceOrgName + 
    		"&ConsumerAreaID=" + ConsumerAreaID;
    	//window.open(url);
    	
    	$("#win").window({
            title: "锁定消费单列表",
            width: 800,
            height: 300,
            content: "<iframe src='" + url + "' width='100%' height='99%' frameborder=0></iframe>",
            modal: true,
            resizable:false,
            collapsible:false,
            minimizable:false,
            maximizable:false
        }).window('open').window('center');
    });
    
    //初始化页面
    function initHtml(){
    	//初始化组件
	    initComponent();
	    //初始化数据
    	initData(function(){
    		initComponentData();//初始化组件数据
    		//初始化最后一次消费码信息
	    	initLastBarcodeInfo(function(){
	    		//不做处理
	    	});
    	});
    }
    
    //初始化页面
    initHtml();
});