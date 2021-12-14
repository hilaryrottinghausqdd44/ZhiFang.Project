$(function () {
    /**申请单号*/
    var NRequestFormNo = "",
		/**错误信息*/
		errorInfo = [],
		/**存储子项数据*/
		itemData = {},
		/**已经加载项目明细的数据*/
		hasChildItemData = {},
		/**项目DIV前缀*/
		itemDiv = "itemdiv",
		/**项目DIV原始背景色*/
		itemDivBackgroundColor = "#C0E7FE",
		/**项目DIV原始背景色*/
		itemDivMousemoveColor = "#33CCFF",
		/**加载字典服务*/
		GetPubDictUrl = Shell.util.Path.rootPath + "/ServiceWCF/DictionaryService.svc/GetPubDict",
		/**加载项目类型数据服务*/
		GetSuperGroupListUrl = Shell.util.Path.rootPath + "/ServiceWCF/DictionaryService.svc/GetSuperGroupList?typestate=1",
		/**根据检验项目ID查询检验明细服务*/
		GetTestDetailByItemIDUrl = Shell.util.Path.rootPath + "/ServiceWCF/DictionaryService.svc/GetTestDetailByItemID",
		/**获取检验项目列表数据服务*/
		GetTestItemUrl = Shell.util.Path.rootPath + "/ServiceWCF/NrequestFromService.svc/GetTestItem_BarCodePrint",
		/**保存申请单数据服务*/
		//NrequestFormAddOrUpdateUrl = Shell.util.Path.rootPath + "/ServiceWCF/NRequestFromService.svc/NrequestFormAddOrUpdate",
		requestFormAdd_BarCodePrint = Shell.util.Path.rootPath + "/ServiceWCF/NRequestFromService.svc/requestFormAdd_BarCodePrintTaiHe",
		requestFormUpdate_BarCodePrint = Shell.util.Path.rootPath + "/ServiceWCF/NRequestFromService.svc/requestFormUpdate_BarCodePrintTaiHe",

		/**获取申请单数据服务*/
		GetNrequestFormByFormNoUrl = Shell.util.Path.rootPath + "/ServiceWCF/NrequestFromService.svc/GetNrequestFormByFormNo_BarCodePrintTaiHe";

    //判断是否有参数传入,修改/新增
    var params = Shell.util.Path.getRequestParams(),
		NRequestFormNo = params.NRequestFormNo;

    //--------------表单信息(开始)------------------
    //------------文字框-------------
    //申请号-只读
    $('#form_NRequestFormNo').textbox({ readonly: true });
    //姓名-必填
    $('#form_CName').textbox({ required: true, missingMessage: "该输入项为必输项" });
    //病历号
    $('#form_PatNo').searchbox({ required: true, missingMessage: "该输入项为必输项" });
    //病房
    $('#form_WardName').textbox();
    //诊断结果
    $('#form_Diag').textbox();
    //采样人
    $('#form_Operator').textbox();
    //床位
    $('#form_Bed').textbox();

    //------------数字框-------------
    //年龄-必填
    $('#form_Age').numberbox({ required: true });
    //收费
    $('#form_Charge').numberbox();

    //------------时间框-------------
    //开单时间
    $('#form_OperDate').datetimebox({ showSeconds: false, required: true });
    //开单时间-清空按钮
    $('#form_OperDate_clear').linkbutton({
        text: '清空', plain: true, onClick: function () {
            $('#form_OperDate').datetimebox("setValue", null);
        }
    });
    //采样时间-必填
    $('#form_CollectDate').datetimebox({ showSeconds: false, required: true });

    //------------下拉框-------------
    /**字典下拉框参数方法*/
    function getComboboxConfig(tableName, valueField, textField, fields, editable) {
        return {
            valueField: valueField,
            textField: textField,
            editable: editable || false,
            //required:true,
            method: 'GET',
            url: GetPubDictUrl + "?tableName=" + tableName + "&fields=" + (fields ? fields : valueField + "," + textField),
            loadFilter: function (data) {
                if (data.success) {
                    var obj = Shell.util.JSON.decode(data.ResultDataValue) || {};
                    var list = obj.rows || [];
                    if (tableName == "GenderType" || tableName == "AgeUnit" || tableName == "TestType" || tableName == "FolkType") {
                        if (list.length > 0 && !NRequestFormNo) { list[0].selected = true; }
                    }
                    if (tableName == "SickType") {
                        if (list.length > 3 && !NRequestFormNo) { list[3].selected = true; }
                    }
                    return list;
                } else {
                    return [];
                }
            }
        };
    }
    //年龄单位-必填
    var AgeUnitNoConfig = getComboboxConfig("AgeUnit", "AgeUnitNo", "CName");
    AgeUnitNoConfig.required = true;
    $("#form_AgeUnitNo").combobox(AgeUnitNoConfig);
    //送检单位
    var ClientConfig = {
        valueField: "ClIENTNO",
        textField: "CNAME",
        editable: true,
        required: true,
        method: 'GET',
        loadFilter: function (data) {
            data = data || [];
            if (data.length > 0 && !NRequestFormNo) { data[0].selected = true; }
            return data;
        },

        onSelect: function (record) {

        }
    };

    $('#form_PatNo').searchbox({
        searcher: function (value, name) {
            var ClIENTNO = $('#form_ClientNo').combobox('getValue'),
				PatNo = value;

            $('#form_PatNo').searchbox('disable');
            $.ajax({
                url: Shell.util.Path.rootPath + '/ServiceWCF/PublisherEvent.svc/GetPatientInfoByOrgIdAndPatientID',
                data: { OrdId: ClIENTNO, PatientId: PatNo },
                dataType: 'json',
                type: 'GET',
                timeout: 10000,
                async: false,
                contentType: 'application/json',//内容类型要匹配后台约定的内容格式
                success: function (data) {
                    switchPatNo();
                    if (data.success) {
                        var data = eval('(' + data.ResultDataValue + ')') || {},
							entity = (data.rows)[0];

                        //填充申请单信息（新增）
                        setNRequestForm(entity || {});
                    }
                },
                error: function (data) {
                    switchPatNo();
                    $.messager.alert('提示信息', data.ErrorInfo, 'error');
                }
            });

        }
    });
    function switchPatNo() {
        $('#form_PatNo').searchbox('enable');
    }

    ClientConfig.onChange = function (newValue, oldValue) {
        Shell.util.Msg.showLog("选中的值=" + newValue);
        if (!NRequestFormNo) {

            //清空已选项目列表和条码列表数据
            clearItemsAndBarCodeData(true);
        }
        $('#itemtype_grid').datagrid("options").url = GetSuperGroupListUrl;
        $('#itemtype_grid').datagrid("reload");
    };
    if (NRequestFormNo) {
        ClientConfig.readonly = true;
    }
    $('#form_ClientNo').combobox(ClientConfig);

    //科室
    var DeptNoConfig = getComboboxConfig("Department", "DeptNo", "CName", null, true);

    DeptNoConfig.onHidePanel = function () {
        var value = $('#form_DeptNo').combobox("getValue");
        if (!value || value == "0") {
            $('#form_DeptNo').combobox("clear");
        }
    };
    $('#form_DeptNo').combobox(DeptNoConfig);
    //病区
    $('#form_DistrictNo').combobox(getComboboxConfig("District", "DistrictNo", "CName"));
    //医生
    var DoctorConfig = getComboboxConfig("Doctor", "DoctorNo", "CName", null, true);

    DoctorConfig.onHidePanel = function () {
        var value = $('#form_Doctor').combobox("getValue");
        if (!value || value == "0") {
            $('#form_Doctor').combobox("clear");
        }
    };
    $('#form_Doctor').combobox(DoctorConfig);
    //性别-下拉框-必填
    var GenderNoConfig = getComboboxConfig("GenderType", "GenderNo", "CName");
    GenderNoConfig.required = true;
    $('#form_GenderNo').combobox(GenderNoConfig);
    //民族-下拉框
    $('#form_FolkNo').combobox(getComboboxConfig("FolkType", "FolkNo", "CName"));
    //就诊类型-下拉框
    $('#form_jztype').combobox(getComboboxConfig("SickType", "SickTypeNo", "CName"));
    //样本类型-下拉框
    $('#form_SampleTypeNo').combobox(getComboboxConfig("SampleType", "SampleTypeNo", "CName"));
    //检验类型-下拉框
    $('#form_TestTypeNo').combobox(getComboboxConfig("TestType", "TestTypeNo", "CName"));

    //--------------表单信息(结束)------------------

    /**创建一个组合DIV*/
    function createDiv(obj) {
        var div =
			"<div style='width:100%;height:100%;' mark='" + itemDiv + obj.ItemNo + "' " +
					"data='" + Shell.util.JSON.encodeValue(obj) + "'" +
			">" +
				//"<div style='width:80%;height:15px;border:solid 1px black;" + (obj.ColorValue ? "background:" + obj.ColorValue : "") + "'></div>" +

				"<span style='height:10px;border:solid 1px black;" + (obj.ColorValue ? "background:" + obj.ColorValue : "") + "'>&nbsp;&nbsp;</span>&nbsp;" +
				"<span>" +
					obj.CName + "<br/>(" + obj.ItemNo + ")" + "<br/><b>价格: " + priceFormat(obj.Prices) + "</b>" +
				"</span>" +
			"</div>";
        return div;
    }
    /**创建组合列表*/
    function createDivTabel(list, cellNum) {
        var rows = list || [],
			len = rows.length,
			count = cellNum || 5,
			table = [];

        itemData = {};

        table.push("<table style='width:100%'>");
        for (var i = 0; i < len; i++) {
            if (i % count == 0) {
                table.push(i == 0 ? "<tr>" : "</tr><tr>");
            }
            var td =
				"<td style='width:100px;height:64px;cursor:pointer;background:" + (i % 10 > 4 ? itemDivBackgroundColor : "#e4e4e4") + "'" +
					"onmousemove='this.style.background=\"" + itemDivMousemoveColor + "\";' " +
					"onmouseout='this.style.background=\"" + (i % 10 > 4 ? itemDivBackgroundColor : "#e4e4e4") + "\";' " +
				">" +
					createDiv(list[i]) +
				"</td>";

            table.push(td);
            itemData[itemDiv + list[i].ItemNo] = list[i];
        }
        if (len > 0) table.push("</tr>");
        table.push("</table>");

        return table.join("");
    }
    /**改变待选列表的数据*/
    function changeUncheckecGrid(data) {
        var divs = $("div[mark^='" + itemDiv + "']") || [],
			len = divs.length;
        for (var i = 0; i < len; i++) {
            $(divs[i]).tooltip("destroy");
        }

        $("#uncheck_grid").empty();//清空内容
        var table = createDivTabel(data.rows, 5);
        $("#uncheck_grid").append(table);//加上内容
        TestItemLoaded(data.total);

        for (var i in itemData) {
            var div = $("div[mark='" + i + "']");
            div.bind("dblclick", ItemDivDblClick);
            div.tooltip({
                content: (hasChildItemData[i] && hasChildItemData[i].list) ? '' : '<span>数据加载中...</span>',
                onShow: onTooltipShow
            });
        }
    }


    /**鼠标悬浮提示*/
    function onTooltipShow(e) {
        var div = this;
        $(div).tooltip('tip').css({
            backgroundColor: '#ffffff',
            borderColor: '#000000'
        });
        changeHasChildItemData(div, createTooltipContent);
    }
    /**创建Tooltip显示内容*/
    function createTooltipContent(div, data) {
        var list = data.list || [],
			len = list.length,
			content = [];

        content.push('<table class="easyui-datagrid" data-options="fit:true,border:true">');
        //表头
        content.push(
			'<thead>' +
		       '<tr style="border:1px">' +
		        	'<th data-options="field:\'ItemNo\',width:100,hidden:true">编码</th>' +
		            '<th data-options="field:\'CName\',width:100">名称</th>' +
		            '<th data-options="field:\'EName\',width:100,hidden:true">英文名</th>' +
		            '<th data-options="field:\'Prices\',width:100,hidden:true">价格</th>' +
		        '</tr>' +
		    '</thead>'
		);
        //表体
        content.push('<tbody>');

        for (var i = 0; i < len; i++) {
            content.push(
				'<tr' + (list[i].ColorValue ? ' style="color:white;background:' + list[i].ColorValue + '"' : '') + '>' +
					'<td>' + list[i].ItemNo + '</td>' +
					'<td>' + list[i].CName + '</td>' +
					'<td>' + list[i].EName + '</td>' +
					'<td>' + priceFormat(list[i].Prices) + '</td>' +
				'</tr>'
			);
        }

        content.push('</tbody>');

        content.push('</table>');

        $(div).tooltip('update', content.join(""));
    }
    /**钱格式转化*/
    function priceFormat(value) {
        var price = Shell.util.Number.retainDecimaltoString(value, 2);
        return "￥" + (price ? price : "0.00");
    }
    /**添加动态数据*/
    function changeHasChildItemData(div, callback) {
        var value = div.getAttribute('data'),//attributes[0].value,//兼容性bug
			itemno = Shell.util.JSON.decode(value).ItemNo,
			info = hasChildItemData[itemDiv + itemno];

        if (info) {
            callback(div, info);
        } else {
            Shell.util.Action.delay(function () {
                var labcode = $("#form_ClientNo").combobox("getValue");
                GetTestDetailByItemID(itemno, labcode, function (data) {
                    hasChildItemData[itemDiv + itemno] = hasChildItemData[itemDiv + itemno] || itemData[itemDiv + itemno];
                    hasChildItemData[itemDiv + itemno].list = data;
                    callback(div, hasChildItemData[itemDiv + itemno]);
                });
            });
        }
    }
    /**更改选中项目的总价*/
    function changeCheckedPrice() {
        var list = $("#checked_grid").datagrid("getRows"),
			len = list.length,
			price = 0;

        for (var i = 0; i < len; i++) {
            var p = parseFloat(list[i].Prices);
            if (isNaN(p)) continue;
            price += p;
        }
        price = "总价:" + priceFormat(price);
        $("#checked_grid_toolbar_price").text(price);
        $("#checked_grid_toolbar_price").attr("title", price);
    }


    /**选择项目数据*/
    function ItemDivDblClick(e) {
        var data = this.getAttribute('data'),
			data = Shell.util.JSON.decode(data);
        insertCheckedData(data);
    }
    /**添加选中数据*/
    function insertCheckedData(data) {
        var grid = $('#checked_grid'),
			index = grid.datagrid('getRowIndex', data.ItemNo);

        if (index == -1) {
            grid.datagrid('appendRow', data);
            grid.datagrid('showColumnRowTooltip', data);

            var rows = grid.datagrid("getRows"),
				num = grid.datagrid("getRowIndex", data.ItemNo),
				panel = grid.datagrid("getPanel"),
				col = panel.find("div.datagrid-view tr.datagrid-row");

            col.find("a.l-btn").each(function (i) {
                if (i == num) {
                    var d = rows[num];
                    $(this).click(function (e) {
                        delCheckedItemData(d);
                        e.stopPropagation();
                    });
                }
            });

            changeBarcodeListData(true);
            //更改选中项目的总价
            changeCheckedPrice();
        }
    }
    /**删除已选的项目数据*/
    function delCheckedItemData(data) {
        if (!data) {
            Shell.util.Msg.showLog("删除已选的项目数据错误,参数不能为空!");
            return;
        }
        var ItemNo = data.ItemNo;
        if (ItemNo == null) {
            Shell.util.Msg.showLog("删除已选的项目数据错误,ItemNo不能为空!");
            return;
        }
        Shell.util.Msg.showLog("删除已选的项目数据,ItemNo=" + ItemNo + ";CName=" + data.CName);

        var grid = $('#checked_grid'),
			rows = grid.datagrid('getRows'),
			index = grid.datagrid('getRowIndex', data.ItemNo);

        //清空已选组合项目列表数据
        grid.datagrid('deleteRow', index);
        changeBarcodeListData(false);
        //更改选中项目的总价
        changeCheckedPrice();
    }
    /**条码列表更改*/
    function changeBarcodeListData(isAdd) {
        //计算出已选项目列表中的所有颜色信息列表
        var colorInfoList = getAllColorItems();

        //条码列表信息
        var barcodeGrid = $('#barcode_grid'),
			barcodeGridRows = barcodeGrid.datagrid("getRows"),
			barcodeLength = barcodeGridRows.length;

        if (isAdd) {
            //计算条码管的增加数据
            var len = colorInfoList.length,
				colorList = [],
				addList = [];

            for (var i = 0; i < len; i++) {
                var nodata = true;
                for (var j = 0; j < barcodeLength; j++) {
                    if (barcodeGridRows[j].ColorValue == colorInfoList[i].ColorValue) {
                        nodata = false; break;
                    }
                }
                if (nodata) {
                    var hasColor = false;
                    for (var j in colorList) {
                        if (colorList[j] == colorInfoList[i].ColorValue) {
                            hasColor = true; break;
                        }
                    }

                    if (!hasColor) {
                        colorList.push(colorInfoList[i].ColorValue);
                        addList.push(colorInfoList[i]);
                    }
                }
            }
            for (var i = 0; i < addList.length; i++) {
                barcodeGrid.datagrid("appendRow", addList[i]);
                barcodeGrid.datagrid('showColumnRowTooltip', addList[i]);
            }
        } else {
            //计算条码管的删除数据
            var len = colorInfoList.length,
				delList = [];

            for (var i = 0; i < barcodeLength; i++) {
                var nodata = true;
                for (var j = 0; j < len; j++) {
                    if (colorInfoList[j].ColorValue == barcodeGridRows[i].ColorValue) {
                        nodata = false; break;
                    }
                }
                if (nodata) {
                    delList.push(barcodeGridRows[i]);
                }
            }
            for (var i = 0; i < delList.length; i++) {
                var index = barcodeGrid.datagrid("getRowIndex", delList[i]);
                barcodeGrid.datagrid("deleteRow", index);
            }
        }
    }

    /**计算出已选项目列表中的所有颜色信息列表*/
    function getAllColorItems() {
        var checkedGrid = $('#checked_grid'),
			checkedGridRows = checkedGrid.datagrid("getRows"),
			checkedLength = checkedGridRows.length,
			colorInfoList = [];

        //计算出已选项目列表中的所有颜色信息列表
        for (var i = 0; i < checkedLength; i++) {
            if (checkedGridRows[i].ColorValue) {
                colorInfoList.push(checkedGridRows[i]);
            }
        }
        return colorInfoList
    }
    /**待选组合项目列表加载数据中*/
    function TestItemLoading() {
        $('#uncheck_grid_pagination').pagination("loading");
    }
    /**待选组合项目列表加载数据完毕*/
    function TestItemLoaded(total) {
        var pagination = $('#uncheck_grid_pagination');
        pagination.pagination("refresh", { total: total });
        pagination.pagination("loaded");
    }
    /**待选组合项目列表加载数据*/
    function uncheckgridLoad() {
        var rowData = $('#itemtype_grid').datagrid("getSelected");
        if (rowData) {
            TestItemLoading();
            //GetTestItem(rowData.SuperGroupNo,changeUncheckecGrid);
            GetTestItem("COMBI", changeUncheckecGrid);
        }
    }

    /**清空已选项目和条码信息*/
    function clearItemsAndBarCodeData(flag) {
        if (flag) {
            //清空已选项目列表数据
            $('#checked_grid').datagrid('loadData', { total: 0, rows: [] });
            //清空条码列表数据
            $('#barcode_grid').datagrid('loadData', { total: 0, rows: [] });
        }
        else {
            if ($('#markItems').is(':checked')) {

            }
            else {
                //清空已选项目列表数据
                $('#checked_grid').datagrid('loadData', { total: 0, rows: [] });
                //清空条码列表数据
                $('#barcode_grid').datagrid('loadData', { total: 0, rows: [] });
            }           
            var rows = $('#barcode_grid').datagrid('getRows') || [],
				length = rows.length;
            for (var i = 0; i < length; i++) {
                $('#barcode_grid').datagrid('updateRow', {
                    index: i,
                    row: { BarCode: null }
                });
            }
        }
    }

    /**重置页面信息*/
    function resetInfo() {
        //申请号置空
        NRequestFormNo = "";
        //清空已选项目和条码信息
        clearItemsAndBarCodeData(false);

        var textboxList = ["form_NRequestFormNo", "form_CName", "form_PatNo", "form_WardName", "form_Diag", "form_Operator", "form_Bed"],
			datetimeboxList = ["form_OperDate", "form_CollectDate"],
			numberboxList = ["form_Age", "form_Charge"],
			comboboxList = ["form_AgeUnitNo", "form_DeptNo", "form_DistrictNo", "form_Doctor",
				"form_GenderNo", "form_FolkNo", "form_jztype", "form_SampleTypeNo", "form_TestTypeNo"];

        var list = textboxList.concat(datetimeboxList, numberboxList, comboboxList);
        for (var i in list) {
            $("#" + list[i]).textbox("reset");
        }

        for (var i in comboboxList) {
            if (comboboxList[i] == "form_AgeUnitNo" || form_GenderNo[i] == "form_Doctor") {
                var rows = $("#" + comboboxList[i]).combobox('getData'),
					valueField = $("#" + comboboxList[i]).combobox('options').valueField;
                $("#" + comboboxList[i]).combobox('select', rows[0][valueField]);
            } else {
                $("#" + comboboxList[i]).combobox('clear');
            }
        }
        //送检单位可选
        $('#form_ClientNo').combobox("readonly", false);
        //采样时间和开单时间默认当前
        var date = Shell.util.Date.toString(new Date());
        for (var i in datetimeboxList) {
            $("#" + datetimeboxList[i]).datetimebox("setValue", date);
        }

        //申请单号：修改时显示，新增时隐藏
        hideNRequestFormNo(true);
    }

    /**保存并打印*/
    function saveInfoAndPrint() {
        saveInfo(this, function (result) {
            $.messager.alert("提示信息", "<b style='color:green;'>保存成功！</b>", "info");
            //直接打印
            printBarcode(result);
            //重置页面信息
            resetInfo();
        });
    }
    /**打印条码*/
    function printBarcode(result, preview) {
        var list = [], len = 0, info = [];

        if (NRequestFormNo) {
            list = getPrintBarcodeInfoList();
        } else {
            list = Shell.util.JSON.decode(result.ResultDataValue) || [];
        }
        len = list.length;

        for (var i = 0; i < len; i++) {
            //info.push({
            //	BarCode:list[i].BarCode,
            //	ColorName:list[i].ColorName,
            //	ItemList:(list[i].ItemList || []).join(";"),
            //	ClientName:$("#form_ClientNo").combobox("getText"),
            //	Name:$("#form_CName").textbox("getValue"),
            //	Sex:$("#form_GenderNo").combobox("getText"),
            //	Age:$("#form_Age").numberbox("getValue"),
            //	AgeUnit:$("#form_AgeUnitNo").combobox("getText"),
            //	SickTypeName:$("#form_jztype").combobox("getText"),
            //	PatNo:$("#form_PatNo").searchbox("getValue"),
            //	CollectDate:$("#form_CollectDate").datetimebox("getValue"),
            //	DeptNo:$("#form_DeptNo").combobox("getText")
            //});
            Shell.taida.Print.barcode({
                BarCode: list[i].BarCode,
                ColorName: list[i].ColorName,
                ItemList: (list[i].ItemList || []).join(";"),
                ClientName: $("#form_ClientNo").combobox("getText"),
                Name: $("#form_CName").textbox("getValue"),
                Sex: $("#form_GenderNo").combobox("getText"),
                Age: $("#form_Age").numberbox("getValue"),
                AgeUnit: $("#form_AgeUnitNo").combobox("getText"),
                SickTypeName: $("#form_jztype").combobox("getText"),
                PatNo: $("#form_PatNo").searchbox("getValue"),
                CollectDate: $("#form_CollectDate").datetimebox("getValue"),
                DeptNo: $("#form_DeptNo").combobox("getText")
            });
        }
        //预览打印
        //Shell.taida.Print.barcode(info,preview);
    }
    /**获取打印的条码信息*/
    function getPrintBarcodeInfoList() {
        var rows = $("#barcode_grid").datagrid("getRows") || [],
			len = rows.length,
			colorInfoList = getAllColorItems(),//获取所有带颜色项目列表
			barCodeList = [];

        for (var i = 0; i < len; i++) {
            if (!rows[i].SampleTypeDetail || rows[i].SampleTypeDetail.length == 0) return null;

            barCodeList.push({
                BarCode: rows[i].BarCode,//条码值
                ColorValue: rows[i].ColorValue,//颜色值
                ColorName: rows[i].ColorName,//颜色名
                ItemList: []//项目列表(id字符串数组)
            });
        }

        for (var i = 0; i < barCodeList.length; i++) {
            for (var j = 0; j < colorInfoList.length; j++) {
                if (barCodeList[i].ColorValue == colorInfoList[j].ColorValue) {
                    barCodeList[i].ItemList.push(colorInfoList[j].CName);
                }
            }
        }

        return barCodeList;
    }
    /**条码设计*/
    function changeModel() {
        Shell.taida.Print.designAndSave();
    }
    /**保存申请单数据*/
    function saveInfo(e, callback) {
        //数据由三部分组成：申请单信息+组合项目列表+条码列表
        //flag(1-增加、0-修改)
        errorInfo = [];
        var jsonentity = {
            flag: NRequestFormNo ? "0" : "1",
            //申请单信息
            NrequestForm: getNrequestForm(),
            //组合项目列表
            CombiItems: getCombiItems(),
            //条码列表
            BarCodeList: getBarCodeList()
        };

        if (jsonentity.CombiItems.length == 0) {
            errorInfo.push("<b><span style='color:red;'>组合项目列表</span>不能为空！</b>");
        }
        if (jsonentity.BarCodeList.length == 0) {
            errorInfo.push("<b><span style='color:red;'>条码列表</span>不能为空！</b>");
        }
        if (!jsonentity.BarCodeList) {
            errorInfo.push("<b><span style='color:red;'>条码</span>有错！</b>");
        }

        if (errorInfo.length > 0) {
            $.messager.alert("错误信息", errorInfo.join("\r\n"), "error");
            return;
        }

        if (jsonentity.CombiItems.length == 0) delete jsonentity.CombiItems;
        if (jsonentity.BarCodeList.length == 0) delete jsonentity.BarCodeList;

        //开启提交保护

        //与后台交互数据
        NrequestFormAddOrUpdate(jsonentity, function (result) {
            if (callback) {
                if (result) callback(result);
            } else {
                if (result && result.success) {
                    $.messager.alert("提示信息", "<b style='color:green;'>保存成功！</b>", "info");
                    //重置页面信息
                    resetInfo();
                }
            }
        });
    }
    /**获取申请单信息*/
    function getNrequestForm() {
        var form = {
            NRequestFormNo: $("#form_NRequestFormNo").textbox("getValue") || "0",//申请号
            PatNo: $("#form_PatNo").searchbox("getValue"),//病历号
            CName: $("#form_CName").textbox("getValue"),//姓名

            SampleTypeNo: $("#form_SampleTypeNo").textbox("getValue"),//样本类型编号
            Bed: $("#form_Bed").textbox("getValue"),//床位
            Diag: $("#form_Diag").textbox("getValue"),//诊断结果
            Operator: $("#form_Operator").textbox("getValue"),//采样人
            Charge: $("#form_Charge").numberbox("getValue") || "0",//收费
            WardName: $("#form_WardName").textbox("getValue"),//病房

            OperDate: Shell.util.Date.toServerDate($("#form_OperDate").datetimebox("getValue")),//开单日期
            OperTime: Shell.util.Date.toServerDate($("#form_OperDate").datetimebox("getValue")),//开单时间
            CollectDate: Shell.util.Date.toServerDate($("#form_CollectDate").datetimebox("getValue")),//采样日期
            CollectTime: Shell.util.Date.toServerDate($("#form_CollectDate").datetimebox("getValue")),//采样时间

            Age: $("#form_Age").numberbox("getValue"),//年龄
            AgeUnitNo: $("#form_AgeUnitNo").combobox("getValue"),//年龄单位
            AgeUnitName: $("#form_AgeUnitNo").combobox("getText"),

            GenderNo: $("#form_GenderNo").combobox("getValue"),//性别编号
            GenderName: $("#form_GenderNo").combobox("getText"),//性别

            SampleTypeNo: $("#form_SampleTypeNo").combobox("getValue") || "0",//样本类型编号
            SampleType: $("#form_SampleTypeNo").combobox("getText"),

            FolkNo: $("#form_FolkNo").combobox("getValue") || "0",//民族
            FolkName: $("#form_FolkNo").combobox("getText"),

            jztype: $("#form_jztype").combobox("getValue") || "0",//就诊类型
            jztypeName: $("#form_jztype").combobox("getText"),

            TestTypeNo: $("#form_TestTypeNo").combobox("getValue") || "0",//检验类型
            TestTypeName: $("#form_TestTypeNo").combobox("getText"),

            DistrictNo: $("#form_DistrictNo").combobox("getValue") || "0",//病区编号
            DistrictName: $("#form_DistrictNo").combobox("getText"),

            DeptNo: $("#form_DeptNo").combobox("getValue") || "0",//科室
            DeptName: $("#form_DeptNo").combobox("getText"),

            Doctor: $("#form_Doctor").combobox("getValue") || "0",//医生
            DoctorName: $("#form_Doctor").combobox("getText"),

            ClientNo: $("#form_ClientNo").combobox("getValue") || "0",//送检单位编号
            ClientName: $("#form_ClientNo").combobox("getText"),

            AreaNo: $("#form_ClientNo").combobox("getValue")
        };

        var errorList = [];

        if (!form.ClientNo) errorList.push("<b><span style='color:red;'>送检单位</span>必须填写！</b>");
        if (!form.CName) errorList.push("<b><span style='color:red;'>姓名</span>必须填写！</b>");
        if (!form.Age) errorList.push("<b><span style='color:red;'>年龄</span>必须填写！</b>");
        if (!form.AgeUnitNo) errorList.push("<b><span style='color:red;'>年龄单位</span>必须填写！</b>");
        if (!form.GenderNo) errorList.push("<b><span style='color:red;'>性别</span>必须填写！</b>");
        if (!form.OperDate) errorList.push("<b><span style='color:red;'>开单时间</span>必须填写！</b>");
        if (!form.CollectDate) errorList.push("<b><span style='color:red;'>采样时间</span>必须填写！</b>");

        if (errorList.length > 0) {
            errorInfo = errorInfo.concat(errorList);
            return null
        }
        return form;
    }
    /**获取组合项目列表*/
    function getCombiItems() {
        var checkedGrid = $('#checked_grid'),
			rows = checkedGrid.datagrid("getRows"),
			len = rows.length,
			combiItem = [];//组合项目

        for (var i = 0; i < len; i++) {
            combiItem.push({
                CombiItemName: rows[i].CName,
                CombiItemNo: rows[i].ItemNo
                //CombiItemDetailList:[]
            });
        }

        return combiItem;
    }
    /**获取条码列表*/
    function getBarCodeList() {
        var rows = $("#barcode_grid").datagrid("getRows") || [],
			len = rows.length,
			colorInfoList = getAllColorItems(),//获取所有带颜色项目列表
			barCodeList = [];

        for (var i = 0; i < len; i++) {
            if (!rows[i].SampleTypeDetail || rows[i].SampleTypeDetail.length == 0) return null;

            barCodeList.push({
                BarCode: NRequestFormNo ? rows[i].BarCode : "",//条码值
                ColorValue: rows[i].ColorValue,//颜色值
                ColorName: rows[i].ColorName,//颜色名
                SampleType: rows[i].SampleType || rows[i].SampleTypeDetail[0].SampleTypeID,//样本类型
                SampleTypeName: rows[i].SampleTypeName || rows[i].SampleTypeDetail[0].CName,//样本类型名称
                ItemList: []//项目列表(id字符串数组)
            });
        }

        for (var i = 0; i < barCodeList.length; i++) {
            for (var j = 0; j < colorInfoList.length; j++) {
                if (barCodeList[i].ColorValue == colorInfoList[j].ColorValue) {
                    barCodeList[i].ItemList.push(colorInfoList[j].ItemNo);
                }
            }
        }

        return barCodeList;
    }

    /**获取申请单数据*/
    function getInfo() {
        GetNrequestForm(NRequestFormNo, function (data) {
            data = changeData(data);
            //填充申请单信息
            setNRequestForm(data.NrequestForm || {});
            //填充组合项目列表
            setCombiItems(data.CombiItems || []);
            //填充条码列表
            setBarCodeList(data.BarCodeList || []);
        });
    }
    /**转化获取回来的数据*/
    function changeData(data) {
        var data = data || {},
			CombiItems = data.CombiItems || [],
			BarCodeList = data.BarCodeList || [],
			combiItemsLen = CombiItems.length,
			barCodeListLen = BarCodeList.length,
			colorInfo = {};

        //转化条码列表数据
        for (var i = 0; i < barCodeListLen; i++) {
            BarCodeList[i].SampleTypeDetail = BarCodeList[i].SampleTypeDetailList;
            delete BarCodeList[i].SampleTypeDetailList;

            for (var j = 0; j < BarCodeList[i].SampleTypeDetail.length; j++) {
                if (BarCodeList[i].SampleTypeDetail[j].SampleTypeID == BarCodeList[i].SampleType) {
                    BarCodeList[i].SampleTypeDetail[j].selected = true; break;
                }
            }

            for (var o in BarCodeList[i].ItemList) {
                colorInfo[BarCodeList[i].ItemList[o]] = BarCodeList[i].ColorValue;
            }
        }

        //转化组合项目列表
        for (var i = 0; i < combiItemsLen; i++) {
            CombiItems[i].ItemNo = CombiItems[i].CombiItemNo + "";
            CombiItems[i].CName = CombiItems[i].CombiItemName;
            CombiItems[i].list = CombiItems[i].CombiItemDetailList;
            delete CombiItems[i].CombiItemNo;
            delete CombiItems[i].CombiItemName;
            delete CombiItems[i].CombiItemDetailList;

            CombiItems[i].ColorValue = colorInfo[CombiItems[i].ItemNo];
        }

        return data;
    };
    /**获取*/
    function getCombiItemsList(item, BarCodeList) {
        var len = (BarCodeList || []).length;

        for (var i = 0; i < len; i++) {
            var list = BarCodeList[i].ItemList || [],
				length = list.length;

            for (var j = 0; j < length; j++) {
                if (list[j] == (item.ItemNo + "")) {
                    item.ColorValue = BarCodeList[i].ColorValue;
                    return item;
                }
            }
        }

        return item;
    }
    /**填充申请单信息*/
    function setNRequestForm(values) {

        $("#form_NRequestFormNo").textbox("setValue", values.NRequestFormNo || "");//申请号
        $("#form_CName").textbox("setValue", values.CName || "");//姓名
        $("#form_SampleTypeNo").textbox("setValue", values.SampleTypeNo || "");//样本类型编号
        $("#form_Bed").textbox("setValue", values.Bed || "");//床位
        $("#form_Diag").textbox("setValue", values.Diag || "");//诊断结果
        $("#Operator").textbox("setValue", values.Operator || "");//采样人
        $("#form_Charge").numberbox("setValue", values.Charge || "");//收费
        $("#form_WardName").textbox("setValue", values.WardName || "");//病房

        //修改的情况
        if (NRequestFormNo) {
            $("#form_ClientNo").combobox("setValue", values.ClientNo || "");//送检单位-值
            $("#form_PatNo").searchbox("setValue", values.PatNo || "");//病历号
            $("#form_OperDate").datetimebox("setValue", Shell.util.Date.toString(values.OperDate));//开单时间
            $("#form_CollectDate").datetimebox("setValue", Shell.util.Date.toString(values.CollectDate));//采样时间

        }

        $("#form_Age").numberbox("setValue", values.Age || "");//年龄
        if (values.AgeUnitNo)
            $("#form_AgeUnitNo").combobox("setValue", values.AgeUnitNo || "");//年龄单位-值

        if (values.GenderNo == null) {
            if (values.GenderName == '男') {
                $("#form_GenderNo").combobox("setValue", 1);//性别-值
            } else if (values.GenderName == '女') {
                $("#form_GenderNo").combobox("setValue", 2);//性别-值
            } else {
                $("#form_GenderNo").combobox("setValue", 3);//性别-值
            }
        } else {
            $("#form_GenderNo").combobox("setValue", values.GenderNo || "");//性别-值
        }


        if (values.SampleTypeNo)
            $("#form_SampleTypeNo").combobox("setValue", values.SampleTypeNo || "");//样本类型-值

        if (values.FolkNo)
            $("#form_FolkNo").combobox("setValue", values.FolkNo || "");//民族-值

        if (values.jztype)
            $("#form_jztype").combobox("setValue", values.jztype || "");//就诊类型-值

        if (values.TestTypeNo)
            $("#form_TestTypeNo").combobox("setValue", values.TestTypeNo || "");//检验类型-值

        if (values.DistrictNo)
            $("#form_DistrictNo").combobox("setValue", values.DistrictNo || "");//病区-值

        if (values.DeptNo)
            $("#form_DeptNo").combobox("setValue", values.DeptNo || "");//科室-值

        if (values.Doctor)
            $("#form_Doctor").combobox("setValue", values.Doctor || "");//医生-值
    }
    /**填充组合项目列表*/
    function setCombiItems(list) {
        var grid = $('#checked_grid'),
			len = list.length;

        for (var i = 0; i < len; i++) {
            var data = list[i],
				index = grid.datagrid('getRowIndex', data.ItemNo);

            if (index != -1) continue;

            grid.datagrid('appendRow', data);
            grid.datagrid('showColumnRowTooltip', data);

            var rows = grid.datagrid("getRows"),
				num = grid.datagrid("getRowIndex", data),
				panel = grid.datagrid("getPanel"),
				col = panel.find("div.datagrid-view tr.datagrid-row");

            col.find("a.l-btn").each(function (x) {
                if (x == num) {
                    var d = rows[num];
                    $(this).click(function (e) {
                        delCheckedItemData(d);
                        e.stopPropagation();
                    });
                }
            });
            //更改选中项目的总价
            changeCheckedPrice();
        }
    }
    /**填充条码列表*/
    function setBarCodeList(list) {
        var grid = $('#barcode_grid'),
			len = list.length;

        for (var i = 0; i < len; i++) {
            var data = list[i];
            grid.datagrid("appendRow", data);
            grid.datagrid('showColumnRowTooltip', data);
        }
    }

    /**隐藏申请号*/
    function hideNRequestFormNo(bo) {
        //		if(bo){
        //			$("#form_NRequestFormNo").parent().parent().css("display","none");
        //			$("#info").panel("resize",{height:120});
        //			$(document.body).layout("resize");
        //		}else{
        //			$("#form_NRequestFormNo").parent().parent().css("display","");
        //			$("#info").panel("resize",{height:150});
        //			$(document.body).layout("resize");
        //		}
    }

    /**获取检验项目列表数据*/
    function GetTestItem(supergroupno, callback) {
        var pagination = $('#uncheck_grid_pagination'),
			options = pagination.pagination("options"),
			itemkey = $("#uncheck_grid_search").searchbox("getValue"),
			labcode = $('#form_ClientNo').combobox("getValue"),
			page = options.pageNumber || 1,
			rows = options.pageSize;
        $.ajax({
            dataType: 'json',
            contentType: 'application/json',
            url: GetTestItemUrl + "?supergroupno=" + supergroupno + "&page=" + page + "&rows=" + rows + "&itemkey=" + itemkey + "&labcode=" + labcode,
            success: function (result) {
                if (result.success) {
                    Shell.util.Msg.showLog("获取检验项目列表数据成功");
                    var data = Shell.util.JSON.decode(result.ResultDataValue) || [];
                    callback(data);
                } else {
                    Shell.util.Msg.showLog("获取检验项目列表数据失败！错误信息：" + result.ErrorInfo);
                    callback([]);
                }
            },
            error: function (request, strError) {
                Shell.util.Msg.showLog("获取检验项目列表数据失败！错误信息：" + strError);
                callback([]);
            }
        });
    }
    /**根据检验项目ID查询检验明细*/
    function GetTestDetailByItemID(itemid, labcode, callback) {
        $.ajax({
            dataType: 'json',
            contentType: 'application/json',
            url: GetTestDetailByItemIDUrl + "?itemid=" + itemid + "&labcode=" + labcode,
            success: function (result) {
                if (result.success) {
                    Shell.util.Msg.showLog("根据检验项目ID查询检验明细成功");
                    var data = Shell.util.JSON.decode(result.ResultDataValue);
                    callback(data);
                } else {
                    Shell.util.Msg.showLog("根据检验项目ID查询检验明细失败！错误信息：" + result.ErrorInfo);
                    callback([]);
                }
            },
            error: function (request, strError) {
                Shell.util.Msg.showLog("根据检验项目ID查询检验明细失败！错误信息：" + strError);
                callback([]);
            }
        });
    }
    /**保存申请单信息*/
    function NrequestFormAddOrUpdate(entity, callback) {
        $.ajax({
            type: 'post',
            dataType: 'json',
            contentType: 'application/json',
            url: NRequestFormNo ? requestFormUpdate_BarCodePrint : requestFormAdd_BarCodePrint,
            data: Shell.util.JSON.encode({ jsonentity: entity }),
            success: function (result) {
                if (result.success) {
                    Shell.util.Msg.showLog("保存申请单信息成功,保存后的主键：");
                    callback(result);
                } else {
                    Shell.util.Msg.showLog("保存申请单信息失败！错误信息：" + result.ErrorInfo);
                    $.messager.alert("错误信息", "保存申请单信息失败！错误信息：" + result.ErrorInfo, "error");
                    callback(null);
                }
            },
            error: function (request, strError) {
                Shell.util.Msg.showLog("保存申请单信息失败！错误信息：" + strError);
                $.messager.alert("错误信息", strError, "error");
                callback(null);
            }
        });
    }
    /**获取申请单信息*/
    function GetNrequestForm(id, callback) {
        $.ajax({
            dataType: 'json',
            contentType: 'application/json',
            url: GetNrequestFormByFormNoUrl + "?nrequestformno=" + id,
            success: function (result) {
                if (result.success) {
                    Shell.util.Msg.showLog("获取申请单信息成功,保存后的主键：");
                    var data = Shell.util.JSON.decode(result.ResultDataValue);
                    callback(data);
                } else {
                    Shell.util.Msg.showLog("获取申请单信息失败！错误信息：" + result.ErrorInfo);
                }
            },
            error: function (request, strError) {
                Shell.util.Msg.showLog("获取申请单信息失败！错误信息：" + strError);
            }
        });
    }
    /**获取送检单位字典数据*/
    function GetClientNoData(callback) {
        $.ajax({
            dataType: 'json',
            contentType: 'application/json',
            url: Shell.util.Path.rootPath + "/ServiceWCF/DictionaryService.svc/GetClientListByRBAC?page=1&rows=1000&fields=CLIENTELE.CNAME,CLIENTELE.ClIENTNO",
            success: function (result) {
                callback(result);
            },
            error: function (request, strError) {
                Shell.util.Msg.showLog("获取送检单位字典数据失败！错误信息：" + strError);
            }
        });
    }

    //项目类型列表_刷新按钮
    $('#itemtype_toolbar_reload').bind('click', function () {
        //加载项目类型数据
        $('#itemtype_grid').datagrid({
            url: GetSuperGroupListUrl
        });
    });

    //项目类型列表
    $('#itemtype_grid').datagrid({
        fit: true,
        border: false,
        fitColumns: true,
        singleSelect: true,
        loadMsg: '数据加载中...',
        method: 'get',
        //toolbar:'#itemtype_toolbar',
        columns: [[
            { field: 'SuperGroupNo', title: '编码', width: 100, hidden: true },
            {
                field: 'CName', title: '名称', width: 100, tooltip: function (value, index, row) {
                    return "<b>" + row.CName + "</b>";
                }
            }
        ]],
        loadFilter: function (data) {
            if (data.success) {
                return Shell.util.JSON.decode(data.ResultDataValue);
            } else {
                return { "tolal": 0, "rows": [] };
            }
        },
        onLoadSuccess: function (data) {
            //默认选中第一行数据
            $('#itemtype_grid').datagrid("selectRow", 0);
        },
        onSelect: function (rowIndex, rowData) {
            //加载项目数据
            Shell.util.Action.delay(function () {
                TestItemLoading();
                //GetTestItem(rowData.SuperGroupNo,changeUncheckecGrid);
                GetTestItem("COMBI", changeUncheckecGrid);
            });
        }
    });

    //待选组合项目列表_查询栏
    $("#uncheck_grid_search").searchbox({
        height: 24,
        width: 200,
        prompt: '中文名/英文名/简码/简称',
        searcher: function (value, name) {
            uncheckgridLoad();
        }
    });

    //待选组合项目列表_分页栏
    $('#uncheck_grid_pagination').pagination({
        pageList: [25, 50, 100, 200, 500],
        total: 0,
        pageSize: 100,
        onChangePageSize: function (pageSize) {
            $(this).pagination("refresh", { pageNumber: 1 });
        },
        onSelectPage: function () {
            uncheckgridLoad();
        }
    });

    //已选组合项目列表
    $('#checked_grid').datagrid({
        fit: true,
        border: false,
        fitColumns: true,
        singleSelect: true,
        loadMsg: '数据加载中...',
        method: 'get',
        toolbar: '#checked_grid_toolbar',
        idField: 'ItemNo',
        columns: [[
            { field: 'ItemNo', title: '编号', width: 100, hidden: true },
            {
                field: 'CName', title: '名称', width: 100, formatter: function (val, row, index) {
                    var div = "<a style='height:10px;border:solid 1px black;" +
                        (row.ColorValue ? "background:" + row.ColorValue : "") + "'>&nbsp;&nbsp;</a>&nbsp;";
                    return "<a>" + div + row.CName + "&nbsp;" + priceFormat(row.Prices) + "</a>";
                }, tooltip: function (value, index, row) {
                    var div = "<a style='width:30px;height:10px;border:solid 1px black;" +
                        (row.ColorValue ? "background:" + row.ColorValue : "") + "'>&nbsp;&nbsp;</a>&nbsp;";
                    return "<b>" + div + row.CName + "&nbsp;" + priceFormat(row.Prices) + "</b>";
                }
            },
            { field: 'EName', title: '英文名', width: 100, hidden: true },
            {
                field: 'operator', title: '操作', width: 20, formatter: function (val, row, index) {
                    return "<a class='l-btn l-btn-plain'><span class='l-btn-left'><span class='l-btn-text icon-no l-btn-icon-left'>&nbsp;</span></span></a>";
                }
            }
        ]]
    });

    //条码信息列表
    $("#barcode_grid").datagrid({
        fit: true,
        fitColumns: true,
        singleSelect: true,
        loadMsg: '数据加载中...',
        method: 'get',
        idField: 'ColorValue',
        toolbar: [{
            iconCls: 'icon-save',
            text: '保存',
            handler: saveInfo
        }, {
            iconCls: 'icon-save',
            text: '保存并打印',
            handler: saveInfoAndPrint
            //		},{
            //			iconCls:'icon-edit',
            //			text:'模板设计',
            //			handler:changeModel
        }],
        columns: [[
			{
			    field: 'ColorName', title: '颜色名称', width: 40,
			    tooltip: function (value, index, row) { return "<b>" + value + "</b>"; },
			    formatter: function (val, row, index) { return "<b>" + val + "</a>"; }
			},
            {
                field: 'ColorValue', title: '颜色值', width: 30,
                styler: function (val, row, index) { return "background-color:" + val + ";"; },
                formatter: function () { return ""; }
            },
            {
                field: 'BarCode', title: '条码', width: 140,
                styler: function (val, row, index) { return "background-color:" + row.ColorValue + ";"; },
                formatter: function (value, row, index) {
                    var input =
					"<input disabled='disabled' style='width:100%;' value='" + (row.BarCode || "") + "' onchange='" +
						"var grid=$(\"#barcode_grid\");" +
						"var index=grid.datagrid(\"getRowIndex\",\"" + row.ColorValue + "\");" +
						"var rows=grid.datagrid(\"getRows\");" +
						"rows[index].BarCode = this.value;" +
					"'/>";
                    return input;
                }
            },
            {
                field: 'SampleType', title: '样本类型', width: 60,
                styler: function (val, row, index) { return "background-color:" + row.ColorValue + ";"; },
                formatter: function (value, row, index) {
                    var list = row.SampleTypeDetail || [],
            			len = list.length,
            			arr = [];
                    arr.push(
            			"<select onchange='" +
            				"var grid=$(\"#barcode_grid\");" +
							"var index=grid.datagrid(\"getRowIndex\",\"" + row.ColorValue + "\");" +
							"var rows=grid.datagrid(\"getRows\");" +
							"rows[index].SampleType = this.value;" +
							"rows[index].SampleTypeName = this.text;" +
            			"'>"
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
            }
        ]]
    });
    //获取区域字典列表
    GetClientNoData(function (data) {
        $('#form_ClientNo').combobox("loadData", data);
        if (NRequestFormNo) {//修改状态
            //显示申请号
            hideNRequestFormNo(false);
            //获取申请单信息
            getInfo();
        }
    });

    //采样时间和开单时间默认当前
    var date = Shell.util.Date.toString(new Date());
    var datetimeboxList = ["form_OperDate", "form_CollectDate"];
    for (var i in datetimeboxList) {
        $("#" + datetimeboxList[i]).datetimebox("setValue", date);
    }
    //隐藏申请号
    hideNRequestFormNo(true);
});
