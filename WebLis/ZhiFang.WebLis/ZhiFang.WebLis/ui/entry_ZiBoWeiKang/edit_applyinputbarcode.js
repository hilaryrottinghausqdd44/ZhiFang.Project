$(function () {
    //给datagrid组件扩展行提示方法
    $.extend($.fn.datagrid.methods, {
        showColumnRowTooltip: function (taget, row) {
            var t = $(taget);
            this.initColumnRowTooltip(t, row);
        },
        initColumnRowTooltip: function (t, row, tr) {
            var opts = t.datagrid("options"),
				index = t.datagrid('getRowIndex', row);

            tr = tr || t.datagrid("getPanel").find("div.datagrid-view div.datagrid-body table tr.datagrid-row[datagrid-row-index=" + index + "]");

            if (opts.rowTooltip) {
                var onShow = function (e) {
                    var tt = $(this), text = $.isFunction(opts.rowTooltip) ? opts.rowTooltip.call(tr, index, row) : buildText(row);
                    tt.tooltip("update", text);
                };
                tr.each(function () { $(this).tooltip({ onShow: onShow }); });
            } else {
                tr.children("td[field]").each(function () {
                    var td = $(this), field = td.attr("field"), colOpts = t.datagrid("getColumnOption", field);
                    if (!colOpts || !colOpts.tooltip) { return; }
                    var cell = td.find("div.datagrid-cell"), onShow = function (e) {
                        var tt = $(this), text = $.isFunction(colOpts.tooltip) ? colOpts.tooltip.call(cell, row[field], index, row) : row[field];
                        tt.tooltip("update", text);
                    };
                    $(cell).tooltip({ onShow: onShow });
                });
            }
            function buildText(row) {
                var fields = t.datagrid("getColumnFields"),
	            	content = $("<table></table>").css({ padding: "5px" });;
                $.each(fields, function (i, field) {
                    var colOpts = t.datagrid("getColumnOption", field);
                    if (!colOpts || !colOpts.field || !colOpts.title) { return; }
                    content.append("<tr><td style='text-align: right; width: 150px;'>" + colOpts.title + ":</td><td style='width: 250px;'>" + row[field] + 

"</td></tr>");
                });
                return content;
            };
        },
        showCellTip: function (t, data) {
            var rows = t.datagrid("getRows"),
				opts = t.datagrid("options");

            t.datagrid("getPanel").find("div.datagrid-view div.datagrid-body table tr.datagrid-row").each(function () {
                var tr = $(this), index = parseInt(tr.attr("datagrid-row-index")), row = rows[index];
                this.initColumnRowTooltip(t, row, tr);
            });
        }
    });

    /**申请单号*/
    var NRequestFormNo = "",
    /**错误信息*/
		errorInfo = [],
    /**存储子项数据*/
		itemData = {},
    /**已经加载项目明细的数据*/
		hasChildItemData = {},
    /**区域字典列表*/
		clientEleAreaList = null,
    /**项目DIV前缀*/
		itemDiv = "itemdiv",
    /**项目DIV原始背景色*/
		itemDivBackgroundColor = "#C0E7FE",
    /**项目DIV原始背景色*/
		itemDivMousemoveColor = "#33CCFF",
    //固定项目列表
		checkedItemsCookies = [],
    //固定项目列表cookie名
		checkedCookieName = 'checkedItems',
    //项目锁定状态
		lockType = 'unlock',
    /**加载字典服务*/
		GetPubDictUrl = Shell.util.Path.rootPath + "/ServiceWCF/DictionaryService.svc/GetPubDict",
    /**加载项目类型数据服务*/
		GetSuperGroupListUrl = Shell.util.Path.rootPath + "/ServiceWCF/DictionaryService.svc/GetSuperGroupList?typestate=1",
    /**根据检验项目ID查询检验明细服务*/
		GetTestDetailByItemIDUrl = Shell.util.Path.rootPath + "/ServiceWCF/DictionaryService.svc/GetTestDetailByItemID",
    /**获取检验项目列表数据服务*/
		GetTestItemUrl = Shell.util.Path.rootPath + "/ServiceWCF/DictionaryService.svc/GetTestItem",
    /**保存申请单数据服务*/
		NrequestFormAddOrUpdateUrl = Shell.util.Path.rootPath + "/ServiceWCF/NRequestFromService.svc/NrequestFormAddOrUpdate",
    /**获取申请单数据服务*/
		GetNrequestFormByFormNoUrl = Shell.util.Path.rootPath + "/ServiceWCF/NRequestFromService.svc/GetNrequestFormByFormNo";
    /**获取实验室项目服务精确匹配*/
    GetLabTestItemByLabItemNoUrl = Shell.util.Path.rootPath + "/ServiceWCF/DictionaryService.svc/GetLabTestItemByLabItemNo";

    //判断是否有参数传入,修改/新增
    var params = Shell.util.Path.getRequestParams(),
		NRequestFormNo = params.NRequestFormNo;
    //设置cookie过期时间
    var curDate = new Date(),
		expireDate = null;

    curDate.setDate(curDate.getDate() + 365);
    expireDate = curDate.toGMTString();
    //从cookie读取固定项目 
    var keyValue = getCookieValue(checkedCookieName) || "",
		value = keyValue ? decodeURI(keyValue) : null,
		rows = value ? eval('(' + value + ')') : [];
    checkedItemsCookies = rows;
    //--------------表单信息(开始)------------------
    //------------文字框-------------
    //申请号-只读
    $('#form_NRequestFormNo').textbox({ readonly: true });
    //姓名-必填
    $('#form_CName').textbox({ required: true, missingMessage: "该输入项为必输项" });
    //就诊类型
    $('#form_jztype').combobox({ required: true });
    //病历号
    $('#form_PatNo').textbox();
    //病房
    $('#form_WardNo').textbox();
    //诊断结果
    $('#form_Diag').textbox();
    //采样人
    $('#form_Operator').textbox();
    //床位
    $('#form_Bed').textbox();

    $('#form_zdy3').datebox();
    $('#form_zdy4').textbox();
    $('#form_zdy5').textbox();
    $('#form_zdy7').textbox();
    $('#form_zdy8').textbox();
    $('#form_zdy10').textbox();

    //------------数字框-------------
    //年龄-必填
    $('#form_Age').numberbox({ required: true });
    //收费
    $('#form_Charge').numberbox();

    //------------时间框-------------
    //开单时间
    $('#form_OperDate').datetimebox({ showSeconds: false, required: false });
    //开单时间-清空按钮
    $('#form_OperDate_clear').linkbutton({
        text: '清空', plain: true, onClick: function () {
            $('#form_OperDate').datetimebox("setValue", null);
        }
    });
    //采样时间-必填
    $('#form_CollectDate').datetimebox({ showSeconds: false, required: true });
    $('#form_ReceiveDate').datetimebox({ showSeconds: false, required: false });
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
                    if (tableName == "GenderType" || tableName == "AgeUnit" || tableName == "SickType") {
                        if (list.length > 0 && !NRequestFormNo) { list[0].selected = true; }
                    }
                    return list;
                } else {
                    return [];
                }
            },
            filter: function (q, row) {
                var opts = $(this).combobox('options'),
					shortCode = row['ShortCode'] || "",
					CName = row[opts.textField] || "";

                if (CName.indexOf(q) > -1) {
                    return true;
                }
                q = q.toUpperCase();
                if (shortCode.indexOf(q) > -1) {
                    return true;
                }
                return false;
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
        //editable: false,
        required: true,
        method: 'GET',
        loadFilter: function (data) {
            data = data || [];
            if (data.length > 0 && !NRequestFormNo) { data[0].selected = true; }
            return data;
        }
    };
    ClientConfig.onHidePanel = function () {
        var value = $('#form_ClientNo').combobox("getValue");
        if (!value || value == "0") {
            var data = $('#form_ClientNo').combobox('getData');
            $('#form_ClientNo').combobox('select', data[0].ClIENTNO);
        }
    };
    ClientConfig.onChange = function (newValue, oldValue) {
        Shell.util.Msg.showLog("选中的值=" + newValue);
        var len = clientEleAreaList.length,
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
            if (clientEleAreaList[i].AreaID == AreaID) {
                $('#form_AreaNo').combobox("loadData", [{
                    text: clientEleAreaList[i].AreaCName,
                    value: clientEleAreaList[i].ClientNo,
                    selected: true
                }]);
                break;
            }
        }

        /**
		 * @author Jcall
		 * @version 2016-12-13
		 */
        //重新获取医生列表
        onReloadDoctor();

        /**
		 * @author Jcall
		 * @version 2017-01-11
		 */
        if (!NRequestFormNo) {
            //清空已经加载的项目明细数据
            hasChildItemData = {};
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
    //var DoctorConfig = getComboboxConfig("B_Lab_Doctor", "LabDoctorNo", "CName", 'LabDoctorNo,CName,ShortCode', false);

    //DoctorConfig.onHidePanel = function () {
    //    var value = $('#form_Doctor').combobox("getValue");
    //    if (!value || value == "0") {
    //        $('#form_Doctor').combobox("clear");
    //    }
    //};
    //$('#form_Doctor').combobox(DoctorConfig);

    $('#form_Doctor').combobox({
        valueField: "LabDoctorNo",
        textField: "CName",
        //editable: false,
        method: "GET",
        loadFilter: function (data) {
            if (data.success) {
                var obj = Shell.util.JSON.decode(data.ResultDataValue) || {};
                var list = obj.rows || [];
                for (var i in list) {
                    list[i].CName = list[i].CName + '(' + (list[i].ShortCode || '') + ')' + '(' + (list[i].LabDoctorNo || '') + ')';
                }
                return list;
            } else {
                return [];
            }
        },
        filter: function (q, row) {
            var opts = $(this).combobox('options'),
				shortCode = row['ShortCode'] || "",
				CName = row[opts.textField] || "";

            if (CName.indexOf(q) > -1) {
                return true;
            }
            q = q.toUpperCase();
            if (shortCode.indexOf(q) > -1) {
                return true;
            }
            return false;
        },
        onHidePanel: function () {
            var value = $('#form_Doctor').combobox("getValue");
            if (!value || value == "0") {
                $('#form_Doctor').combobox("clear");
            }
        }
    });

    $("#dgdoctor").datagrid({
        toolbar: "#toolbardoctor",
        singleSelect: true,
        fit: true,
        border: false,
        pagination: true,
        rownumbers: true,
        collapsible: false,
        idField: 'LabDoctorNo',
        //url: searchcurl,
        method: 'get',
        striped: true,

        columns: [[
             //{ field: 'cb', checkbox: 'true' },
             { field: 'LabDoctorNo', title: 'ID' },
             { field: 'CName', title: '名称', width: '80%' },
            {
                field: 'action', title: '操作', width: 50, align: 'center',
                formatter: function (value, row, index) {
                    var b = '<a href="javascript:doctordeleterow(\'' + row.LabDoctorNo + '\')"class="ope-save">删除</a>';
                    return b;
                }
            }
        ]],
        loadFilter: function (data) {
            var result = eval("(" + data.ResultDataValue + ")");
            return { total: result.total || 0, rows: result.rows || [] };
        }
    });

    //性别-下拉框-必填
    var GenderNoConfig = getComboboxConfig("GenderType", "GenderNo", "CName");
    GenderNoConfig.required = true;
    $('#form_GenderNo').combobox(GenderNoConfig);
    $('#form_GenderNo').combobox({
        onLoadSuccess: function () {
            $('#form_GenderNo').combobox('select', 2);
        }
    });
    //民族-下拉框
    $('#form_FolkNo').combobox(getComboboxConfig("FolkType", "FolkNo", "CName"));
    //就诊类型-下拉框
    $('#form_jztype').combobox(getComboboxConfig("SickType", "SickTypeNo", "CName"));
    //样本类型-下拉框
    //$('#form_SampleTypeNo').combobox(getComboboxConfig("SampleType", "SampleTypeNo", "CName"));
    //检验类型-下拉框
    $('#form_TestTypeNo').combobox(getComboboxConfig("TestType", "TestTypeNo", "CName"));
    //区域-只读
    $('#form_AreaNo').combobox({
        readonly: true,
        disabled: true,
        onChange: function (newValue) {
            //          if (!NRequestFormNo) {
            //              //清空已经加载的项目明细数据
            //              hasChildItemData = {};
            //              //清空已选项目列表和条码列表数据
            //              clearItemsAndBarCodeData(true);
            //          }
            //          $('#itemtype_grid').datagrid("options").url = GetSuperGroupListUrl;
            //          $('#itemtype_grid').datagrid("reload");
        }
    });

    //--------------表单信息(结束)------------------

    /**创建一个组合DIV*/
    function createDiv(obj) {
        var div =
			"<div mark='" + itemDiv + obj.ItemNo + "' " +
					"data='" + Shell.util.JSON.encodeValue(obj) + "'" +
			">" +
				"<span>" +
					obj.CName + "</br>(" + obj.ItemNo + ")" +
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
				"<td style='width:100px;height:56px;cursor:pointer;background:" + (i % 10 > 4 ? itemDivBackgroundColor : "#e4e4e4") + "'" +
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
    /**改变改选列表的数据*/
    function changeUncheckecGrid(data) {
        var divs = $("div[mark^='" + itemDiv + "']") || [],
			len = divs.length;
        for (var i = 0; i < len; i++) {
            $(divs[i]).tooltip("destroy");
        }
        $("#uncheck_grid").empty(); //清空内容

        var table = createDivTabel(data.rows, 5);

        $("#uncheck_grid").append(table); //加上内容

        for (var i in itemData) {
            var div = $("div[mark='" + i + "']");
            div.tooltip({
                content: (hasChildItemData[i] && hasChildItemData[i].list) ? '' : '<span>数据加载中...</span>',
                onShow: onTooltipShow
            });
            div.bind("dblclick", ItemDivDblClick);
        }
        TestItemLoaded(data.total);
    }
    /**鼠标悬浮提示*/
    function onTooltipShow(e) {
        var div = this;
        $(div).tooltip('tip').css({
            backgroundColor: '#ffffff',
            borderColor: '#000000'
        });
        if (!$(div).tooltip('tip').attr("isShow")) {
            $(div).tooltip('tip').on({
                mouseover: function () {
                    console.log("mouseover");
                    $(div).tooltip("show");
                },
                mouseleave: function () {
                    console.log("mouseleave");
                    $(div).tooltip("hide");
                }
            });
            changeHasChildItemData(div, createTooltipContent);
        }
        $(div).tooltip('tip').attr("isShow", "true");
    }
    /**创建Tooltip显示内容*/
    function createTooltipContent(div, data) {
        var list = data.list || [],
			len = list.length,
			content = [];

        content.push('<div style="height:200px;width:500px;overflow:auto;">');
        content.push('<table class="easyui-datagrid" data-options="fit:true,border:true" style="width:100%;">');
        //表头
        content.push(
			'<thead>' +
		       '<tr style="border:1px">' +
		        	'<th data-options="field:\'ItemNo\',width:100,hidden:true">编码</th>' +
		            '<th data-options="field:\'CName\',width:100">名称</th>' +
		            '<th data-options="field:\'EName\',width:100,hidden:true">英文名</th>' +
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
				'</tr>'
			);
        }

        content.push('</tbody>');
        content.push('</table>');
        content.push('</div>');

        $(div).tooltip('update', content.join(""));
        $(div).tooltip('reposition');
    }

    /**选择项目数据*/
    function ItemDivDblClick(e) {
        var div = this;
        changeHasChildItemData(div, insertCheckedData);
    }
    /**添加选中数据*/
    function insertCheckedData(div, data, lock) {
        var grid = $('#checked_grid'),
			rowsExist = $('#checked_grid').datagrid('getRows') || [],
			length = rowsExist.length,
			index = grid.datagrid('getRowIndex', data);

        for (var i = 0; i < length; i++) {
            if (rowsExist[i].CName == data.CName) {
                return;
            }
        }
        //判断是否是固定项目
        //从cookie读取固定项目
        var keyValue = getCookieValue(checkedCookieName) || "",
			value = keyValue ? decodeURI(keyValue) : null,
			rows = value ? eval('(' + value + ')') : [];

        for (var i = 0; i < rows.length; i++) {
            if (rows[i].CName == data.CName) {
                lock = 'locked';
                break;
            }
        }
        if (index == -1) {
            grid.datagrid('appendRow', data);
            grid.datagrid('showColumnRowTooltip', data);

            var rows = grid.datagrid("getRows"),
				num = grid.datagrid("getRowIndex", data),
				panel = grid.datagrid("getPanel"),
				col = panel.find("div.datagrid-view tr.datagrid-row");

            if (lock == 'locked') {
                grid.datagrid('checkRow', num);
                lockType = lock;
            }
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
			index = grid.datagrid('getRowIndex', data);

        //清空已选组合项目列表数据
        grid.datagrid('deleteRow', index);
        changeBarcodeListData(false);
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
                colorInfoList[i].BarCode = colorInfoList[i].BarCode ? null : colorInfoList[i].BarCode; //新增清空条码
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
            var list = checkedGridRows[i].list || [],
				len = list.length;

            for (var j = 0; j < len; j++) {
                if (list[j].ColorValue) {
                    colorInfoList.push(list[j]);
                }
            }
        }
        return colorInfoList
    }

    /**添加动态数据*/
    function changeHasChildItemData(div, callback) {
        var value = div.getAttribute('data'), //attributes[0].value,//兼容性bug
			itemno = Shell.util.JSON.decode(value).ItemNo,
			info = hasChildItemData[itemDiv + itemno];

        if (info) {
            callback(div, info);
        } else {
            Shell.util.Action.delay(function () {
                var labcode = HasClientItem ? $("#form_ClientNo").combobox("getValue") : $("#form_AreaNo").combobox("getValue");

                GetTestDetailByItemID(itemno, labcode, function (data) {
                    hasChildItemData[itemDiv + itemno] = hasChildItemData[itemDiv + itemno] || itemData[itemDiv + itemno];
                    hasChildItemData[itemDiv + itemno].list = data;
                    callback(div, hasChildItemData[itemDiv + itemno]);
                });
            });
        }
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

        if (NRequestFormNo) {
            return; //修改状态，不需要从cookies加载条码列表
        }
        //从cookie读取固定项目 ganwh add 2015-6-11
        var keyValue = getCookieValue(checkedCookieName) || "",
			value = keyValue ? decodeURI(keyValue) : null,
			rows = value ? eval('(' + value + ')') : [],
			length = rows.length,
			labcode = $('#form_AreaNo').combobox("getValue");

        /**
	     * 获取送检单位的项目，如果存在则获取本身的项目，否则获取区域项目
	     * @author Jcall
	     * @version 2016-12-21
	     */
        //获取送检单位的项目，如果存在则获取本身的项目，否则获取区域项目
        labcode = HasClientItem ? $("#form_ClientNo").combobox("getValue") : labcode;

        checkedItemsCookies = rows;
        for (var i = 0; i < length; i++) {
            $.ajax({//GetPubDict?tableName=b_lab_testitem&labcode=1028140051&filervalue=90000240&page=1&rows=1
                //url: encodeURI(GetPubDictUrl + "?tableName=b_lab_testitem&page=1&rows=1&filervalue=" + rows[i].ItemNo + "&labcode=" + labcode),
                url: GetLabTestItemByLabItemNoUrl + "?LabItemNo=" + rows[i].ItemNo + "&labcode=" + labcode,
                dataType: 'json',
                contentType: 'application/json',
                method: 'GET',
                success: function (data) {
                    var data = eval('(' + data.ResultDataValue + ')') || {},
						 row = data.rows;

                    getCombItems2(labcode, row[0]);
                },
                error: function (data) {
                    Shell.util.Msg.showLog("获取组合项目失败！错误信息：" + data.ErrorInfo);
                }
            });
        }
    }

    //获取组合项目中的细项
    function getCombItems2(labcode, row) {
        $.ajax({
            url: GetTestDetailByItemIDUrl + "?itemid=" + row.ItemNo + "&labcode=" + labcode,
            dataType: 'json',
            contentType: 'application/json',
            method: 'GET',
            success: function (data) {
                var data = eval('(' + data.ResultDataValue + ')') || {};
                row.list = data;
                insertCheckedData(this, row, 'locked');
            },
            error: function (data) {
                Shell.util.Msg.showLog("根据检验项目ID查询检验明细失败！错误信息：" + data.ErrorInfo);
            }
        });
    }

    //获取cookie值 
    function getCookieValue(name) {
        var key = name + '=',
			cookies = document.cookie;

        if (cookies.indexOf(key) > -1) {
            var startSet = cookies.indexOf(key),
				keyLength = key.length,
				endSet = cookies.indexOf(';', startSet),
				keyValue = '';
            startSet += keyLength;
            if (endSet == -1) {
                keyValue = cookies.substring(startSet);
            } else {
                keyValue = cookies.substring(startSet, endSet);
            }
            return keyValue;
        }
        return;
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
    function clearItemsAndBarCodeData(all) {
        if (all) {
            //清空已选项目列表数据
            $('#checked_grid').datagrid('loadData', { total: 0, rows: [] });
            //清空条码列表数据
            $('#barcode_grid').datagrid('loadData', { total: 0, rows: [] });
        } else {
            var allRows = $('#checked_grid').datagrid('getRows') || [],
				length2 = allRows.length,
				checkedRows = $('#checked_grid').datagrid('getChecked') || [],
				length3 = checkedRows.length,
				checkedItemNo = '';

            for (var i = 0; i < length3; i++) {
                checkedItemNo += checkedRows[i].ItemNo + ',';
            }
            for (var i = length2 - 1; i >= 0; i--) {
                if (checkedItemNo.indexOf(allRows[i].ItemNo) > -1) {
                    continue;
                }
                $('#checked_grid').datagrid('deleteRow', i);
                changeBarcodeListData(false);
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
        //		var url = location.href,
        //			arr = url.split("?"),
        //			url = arr[0];
        //		//清空数据
        //		location.href = url;

        //清空已选项目和条码信息
        clearItemsAndBarCodeData(false);

        //setNRequestForm({});

        var textboxList = ["form_PersonID", "form_NRequestFormNo", "form_CName", "form_PatNo", "form_WardNo", "form_Diag", "form_Operator", "form_Bed", 

"form_zdy3", "form_zdy4", "form_zdy5", "form_zdy7", "form_zdy8", "form_zdy10"],
			datetimeboxList = ["form_OperDate", "form_CollectDate"],
			numberboxList = ["form_Age", "form_Charge"],
			comboboxList = ["form_AgeUnitNo", "form_DeptNo", "form_DistrictNo", "form_Doctor",
				"form_GenderNo", "form_FolkNo", "form_TestTypeNo"];
        $("input[name='form_zdy9']").removeAttr("checked");
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

    /**保存申请单数据*/
    function saveInfo() {
        //提高兼容性，操作DOM获取数据
        var barcodeVlaueList = $("input[id^='barcode_list_row_value_']") || [];
        var barocdeTypeList = $("select[id^='barcode_list_row_type_']") || [];
        var len = barcodeVlaueList.length;
        var grid = $("#barcode_grid");
        var rows = grid.datagrid("getRows");
        var barCodeAry = [];
        for (var i = 0; i < len; i++) {
            var colorValue = barcodeVlaueList[i].id.split("_").slice(-1)[0];
            var index = grid.datagrid("getRowIndex", colorValue);
            rows[index].BarCode = $(barcodeVlaueList[i]).val();
            barCodeAry.push($(barcodeVlaueList[i]).val());
            rows[index].SampleType = $(barocdeTypeList[i]).val();
        }
        //判断条码的唯一性
        for (var i = 0; i < barCodeAry.length; i++) {
            var isValid = barcodeIsValid(barCodeAry[i]);
            if (isValid !== true) {
                $.messager.alert('提示', isValid, 'info');
                return;
            }
            for (var j = 0; j < barCodeAry.length; j++) {
                if (i == j)
                    continue; //不能和自己做比较
                if (barCodeAry[i] == barCodeAry[j]) {
                    $.messager.alert('提示', '条码号不能有相同的！', 'info');
                    return;
                }
            }
        }
        $('#save_barcode_list').linkbutton('disable');
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
            BarCodeList: getBarCodeList() || []
        };

        if (jsonentity.CombiItems.length == 0) {
            errorInfo.push("<b><span style='color:red;'>组合项目列表</span>不能为空！</b>");
        }
        if (jsonentity.BarCodeList.length == 0) {
            errorInfo.push("<b><span style='color:red;'>条码列表</span>不能为空！</b>");
        }
        else {
            jsonentity.NrequestForm.BarCode = jsonentity.BarCodeList[0].BarCode;
        }
        if (!jsonentity.BarCodeList) {
            errorInfo.push("<b><span style='color:red;'>条码</span>有错！</b>");
        }

        if (errorInfo.length > 0) {
            $.messager.alert("错误信息", errorInfo.join("\r\n"), "error");
            $('#save_barcode_list').linkbutton('enable');
            return;
        }

        if (jsonentity.CombiItems.length == 0) delete jsonentity.CombiItems;
        if (jsonentity.BarCodeList.length == 0) delete jsonentity.BarCodeList;

        NrequestFormAddOrUpdate(jsonentity, function () {
            $.messager.alert("提示信息", "<b style='color:green;'>保存成功！</b>", "info");
            parent.CloseWindowFuc();
        });
    }
    /**获取申请单信息*/
    function getNrequestForm() {
        var form = {
            NRequestFormNo: $("#form_NRequestFormNo").textbox("getValue") || "0", //申请号
            PatNo: $("#form_PatNo").textbox("getValue"), //病历号
            CName: $("#form_CName").textbox("getValue"), //姓名
            PersonID: $("#form_PersonID").textbox("getValue"),//身份证
            //SampleTypeNo: $("#form_SampleTypeNo").textbox("getValue"), //样本类型编号
            zdy1: $("#form_Bed").textbox("getValue"), //床位
            zdy2: $("#form_WardNo").textbox("getValue"), //病房
            Diag: $("#form_Diag").textbox("getValue"), //诊断结果
            Operator: $("#form_Operator").textbox("getValue"), //采样人
            Charge: $("#form_Charge").numberbox("getValue") || "0", //收费
            zdy3: $("#form_zdy3").datebox("getValue"), //末次月经日期
            zdy4: $("#form_zdy4").textbox("getValue"), //孕周
            zdy5: $("#form_zdy5").textbox("getValue"), //孕周+
            ZDY6: $("#form_zdy6").combobox("getValue"), //样本状态
            ZDY7: $("#form_zdy7").textbox("getValue"), //实验编号
            ZDY8: $("#form_zdy8").textbox("getValue"), //体重
            ZDY9: $("input[name='form_zdy9']:checked").val(), //男方女方
            ZDY10: $("#form_zdy10").textbox("getValue"), //通讯地址

            OperDate: Shell.util.Date.toServerDate($("#form_OperDate").datetimebox("getValue")), //开单日期
            OperTime: Shell.util.Date.toServerDate($("#form_OperDate").datetimebox("getValue")), //开单时间
            CollectDate: Shell.util.Date.toServerDate($("#form_CollectDate").datetimebox("getValue")), //采样日期
            CollectTime: Shell.util.Date.toServerDate($("#form_CollectDate").datetimebox("getValue")), //采样时间
            ReceiveDate: Shell.util.Date.toServerDate($("#form_ReceiveDate").datetimebox("getValue")), //开单时间

            Age: $("#form_Age").numberbox("getValue"), //年龄
            AgeUnitNo: $("#form_AgeUnitNo").combobox("getValue"), //年龄单位
            AgeUnitName: $("#form_AgeUnitNo").combobox("getText"),

            GenderNo: $("#form_GenderNo").combobox("getValue"), //性别编号
            GenderName: $("#form_GenderNo").combobox("getText"), //性别

            //SampleTypeNo: $("#form_SampleTypeNo").combobox("getValue") || "0", //样本类型编号
            //SampleType: $("#form_SampleTypeNo").combobox("getText"),
            SampleTypeNo: "0",//给样本类型默认一个值，签收的时候样本类型不能为空
            SampleType: "0",
            FolkNo: $("#form_FolkNo").combobox("getValue") || "0", //民族
            FolkName: $("#form_FolkNo").combobox("getText"),

            jztype: $("#form_jztype").combobox("getValue"), //就诊类型
            jztypeName: $("#form_jztype").combobox("getText"),

            TestTypeNo: $("#form_TestTypeNo").combobox("getValue") || "0", //检验类型
            TestTypeName: $("#form_TestTypeNo").combobox("getText"),

            DistrictNo: $("#form_DistrictNo").combobox("getValue") || "0", //病区编号
            DistrictName: $("#form_DistrictNo").combobox("getText"),

            //DeptNo: $("#form_DeptNo").combobox("getValue") || "0", //科室
            //DeptName: $("#form_DeptNo").combobox("getText"),

            DeptNo: $("#form_ClientNo").combobox("getValue") || "0", //科室
            DeptName: $("#form_ClientNo").combobox("getText"),

            Doctor: $("#form_Doctor").combobox("getValue") || "0", //医生
            DoctorName: $("#form_Doctor").combobox("getText").split("(")[0],

            ClientNo: $("#form_ClientNo").combobox("getValue") || "0", //送检单位编号
            ClientName: $("#form_ClientNo").combobox("getText"),

            AreaNo: $("#form_AreaNo").combobox("getValue")
        };

        /**
	     * 如果送检单位存在项目，则区域编码=送检单位编码
	     * @author Jcall
	     * @version 2016-12-21
	     */
        if (HasClientItem) {
            form.AreaNo = form.ClientNo;
        }

        /*
	     * 年龄类型单选
	     * @author Jcall
	     * @version 2016-10-25
	     */
        var checked = $("input:radio[name='ageRadio']:checked").val();
        if (checked == "1") {
            form.Age = 200;//成人200岁
            var ageUnitNo = $("#form_AgeUnitNo");//年龄单位
            var data = ageUnitNo.combobox("getData");//年龄单位列表数据

            for (var i in data) {
                if (data[i].CName == '岁') {
                    form.AgeUnitNo = data[i].AgeUnitNo;
                    form.AgeUnitName = data[i].CName;
                    break;
                }
            }
        }

        var errorList = [];

        if (!form.ClientNo || form.ClientNo == "0") errorList.push("<b><span style='color:red;'>送检单位</span>必须填写！</b>");
        if (!form.CName) errorList.push("<b><span style='color:red;'>姓名</span>必须填写！</b>");
        if (!form.Age) errorList.push("<b><span style='color:red;'>年龄</span>必须填写！</b>");
        if (!form.AgeUnitNo) errorList.push("<b><span style='color:red;'>年龄单位</span>必须填写！</b>");
        if (!form.GenderNo) errorList.push("<b><span style='color:red;'>性别</span>必须填写！</b>");
        if (!form.jztype) errorList.push("<b><span style='color:red;'>就诊类型</span>必须填写！</b>");
        //if (!form.OperDate) errorList.push("<b><span style='color:red;'>开单时间</span>必须填写！</b>");
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
			combiItem = []; //组合项目

        for (var i = 0; i < len; i++) {
            var list = rows[i].list || [],
				length = list.length,
				CombiItemDetailList = [];

            for (var j = 0; j < length; j++) {
                CombiItemDetailList.push({
                    CombiItemDetailNo: list[j].ItemNo,
                    CombiItemDetailName: list[j].CName
                });
            }

            combiItem.push({
                CombiItemName: rows[i].CName,
                CombiItemNo: rows[i].ItemNo,
                CombiItemDetailList: CombiItemDetailList
            });
        }

        return combiItem;
    }
    /**获取条码列表*/
    function getBarCodeList() {
        var rows = $("#barcode_grid").datagrid("getRows") || [],
			len = rows.length,
			colorInfoList = getAllColorItems(), //获取所有带颜色项目列表
			barCodeList = [];

        for (var i = 0; i < len; i++) {
            if (!rows[i].BarCode) return null;
            if (!rows[i].SampleTypeDetail || rows[i].SampleTypeDetail.length == 0) return null;
            barCodeList.push({
                ColorValue: rows[i].ColorValue, //颜色值
                ColorName: rows[i].ColorName, //颜色名称
                BarCode: rows[i].BarCode, //条码值
                SampleType: rows[i].SampleType || rows[i].SampleTypeDetail[0].SampleTypeID, //样本类型
                ItemList: []//项目列表(id字符串数组)
            });
        }

        for (var i = 0; i < barCodeList.length; i++) {
            for (var j = 0; j < colorInfoList.length; j++) {
                if (barCodeList[i].ColorValue == colorInfoList[j].ColorValue) {
                    barCodeList[i].ItemList.push(colorInfoList[j].ItemNo);
                }
            }
            delete barCodeList[i].ColorValue;
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
			barCodeListLen = BarCodeList.length;

        //转化条码列表数据
        for (var i = 0; i < barCodeListLen; i++) {
            BarCodeList[i].SampleTypeDetail = BarCodeList[i].SampleTypeDetailList;
            delete BarCodeList[i].SampleTypeDetailList;

            for (var j = 0; j < BarCodeList[i].SampleTypeDetail.length; j++) {
                if (BarCodeList[i].SampleTypeDetail[j].SampleTypeID == BarCodeList[i].SampleType) {
                    BarCodeList[i].SampleTypeDetail[j].selected = true; break;
                }
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

            var list = CombiItems[i].list;
            if (list) {//子项
                var len = list.length;
                for (var j = 0; j < len; j++) {
                    list[j].ItemNo = list[j].CombiItemDetailNo + "";
                    list[j].CName = list[j].CombiItemDetailName;
                    delete list[j].CombiItemDetailNo;
                    delete list[j].CombiItemDetailName;
                    list[j] = getCombiItemsList(list[j], BarCodeList);
                }
            }
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
        $("#form_NRequestFormNo").textbox("setValue", values.NRequestFormNo || ""); //申请号
        $("#form_PatNo").textbox("setValue", values.PatNo || ""); //病历号
        $("#form_CName").textbox("setValue", values.CName || ""); //姓名
        $("#form_PersonID").textbox("setValue", values.PersonID || "");//身份证

        //$("#form_SampleTypeNo").textbox("setValue", values.SampleTypeNo || ""); //样本类型编号
        $("#form_Bed").textbox("setValue", values.zdy1 || ""); //床位
        $("#form_WardNo").textbox("setValue", values.zdy2 || ""); //病房
        $("#form_Diag").textbox("setValue", values.Diag || ""); //诊断结果
        $("#Operator").textbox("setValue", values.Operator || ""); //采样人
        $("#form_Charge").numberbox("setValue", values.Charge || ""); //收费

        $("#form_OperDate").datetimebox("setValue", Shell.util.Date.toString(values.OperDate)); //开单时间
        $("#form_CollectDate").datetimebox("setValue", Shell.util.Date.toString(values.CollectDate)); //采样时间
        $("#form_ReceiveDate").datetimebox("setValue", Shell.util.Date.toString(values.ReceiveDate));//签收时间

        //$("#form_Age").numberbox("setValue", values.Age || ""); //年龄
        //$("#form_AgeUnitNo").combobox("setValue", values.AgeUnitNo || ""); //年龄单位-值
        //$("#form_AgeUnitNo").combobox("setText",values.AgeUnitName || "");//年龄单位-文字

        /*
	     * 年龄类型单选
	     * @author Jcall
	     * @version 2016-10-25
	     */
        if (values.Age == 200) {
            //禁用年龄和年龄单位
            $("#form_Age").numberbox("disable", true);
            $("#form_AgeUnitNo").combobox("disable", true);
            $("input[name=ageRadio]:eq(1)").attr("checked", "checked");
        } else {
            $("#form_Age").numberbox("setValue", values.Age || ""); //年龄
            $("#form_AgeUnitNo").combobox("setValue", values.AgeUnitNo || ""); //年龄单位-值
        }

        $("#form_GenderNo").combobox("setValue", values.GenderNo || ""); //性别-值
        //$("#form_GenderNo").combobox("setText",values.GenderName || "");//性别-文字

        //$("#form_SampleTypeNo").combobox("setValue", values.SampleTypeNo || ""); //样本类型-值
        //$("#form_SampleTypeNo").combobox("setText",values.SampleType || "");//样本类型-文字

        $("#form_FolkNo").combobox("setValue", values.FolkNo || ""); //民族-值
        //$("#form_FolkNo").combobox("setText",values.FolkName || "");//民族-文字

        $("#form_jztype").combobox("setValue", values.jztype || ""); //就诊类型-值
        //$("#form_jztype").combobox("setText",values.jztypeName || "");//就诊类型-文字

        $("#form_TestTypeNo").combobox("setValue", values.TestTypeNo || ""); //检验类型-值
        //$("#form_TestTypeNo").combobox("setText",values.TestTypeName || "");//检验类型-文字

        $("#form_DistrictNo").combobox("setValue", values.DistrictNo || ""); //病区-值
        //$("#form_DistrictNo").combobox("setText",values.DistrictName || "");//病区-文字

        //$("#form_DeptNo").combobox("setValue", values.DeptNo || ""); //科室-值
        //$("#form_DeptNo").combobox("setText",values.DeptName || "");//科室-文字

        $("#form_Doctor").combobox("setValue", values.Doctor || ""); //医生-值
        //$("#form_Doctor").combobox("setText",values.DoctorName || "");//医生-文字

        $("#form_ClientNo").combobox("setValue", values.ClientNo || ""); //送检单位-值
        //$("#form_ClientNo").combobox("setText",values.ClientName || "");//送检单位-文字
        $("#form_zdy3").datebox("setValue", values.zdy3);
        $("#form_zdy4").textbox("setValue", values.zdy4 || "");
        $("#form_zdy5").textbox("setValue", values.zdy5 || "");
        $("#form_zdy6").combobox("setValue", values.ZDY6 || "");
        $("#form_zdy7").textbox("setValue", values.ZDY7 || "");
        $("#form_zdy8").textbox("setValue", values.ZDY8 || "");
        $("#form_zdy9").textbox("setValue", values.ZDY9 || "");
        $("#form_zdy10").textbox("setValue", values.ZDY10 || "");
    }
    /**填充组合项目列表*/
    function setCombiItems(list) {
        var grid = $('#checked_grid'),
			lock,
			len = list.length;

        //判断是否是固定项目
        //从cookie读取固定项目 ganwh add 2015-6-11
        var keyValue = getCookieValue(checkedCookieName) || "",
			value = keyValue ? decodeURI(keyValue) : null,
			rowsCook = value ? eval('(' + value + ')') : [];

        for (var i = 0; i < len; i++) {
            var data = list[i],
				index = grid.datagrid('getRowIndex', data);

            if (index != -1) continue;

            grid.datagrid('appendRow', data);
            grid.datagrid('showColumnRowTooltip', data);

            lock = "";
            for (var j = 0; j < rowsCook.length; j++) {
                if (rowsCook[j].CName == data.CName) {
                    lock = 'locked';
                    if (lock == 'locked') {
                        grid.datagrid('checkRow', i);
                        lockType = lock;
                    }
                    break;
                }
            }
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
        if (bo) {
            $("#form_NRequestFormNo").parent().parent().css("display", "none");
            $("#info").panel("resize", { height: 500 });
            $(document.body).layout("resize");
        } else {
            $("#form_NRequestFormNo").parent().parent().css("display", "");
            $("#info").panel("resize", { height: 2300 });
            $(document.body).layout("resize");
        }
    }

    /**获取检验项目列表数据*/
    function GetTestItem(supergroupno, callback) {
        var pagination = $('#uncheck_grid_pagination'),
			options = pagination.pagination("options"),
			itemkey = encodeURI($("#uncheck_grid_search").searchbox("getValue")),
			labcode = $('#form_AreaNo').combobox("getValue"),
			page = options.pageNumber || 1,
			rows = options.pageSize;

        /**
	     * 获取送检单位的项目，如果存在则获取本身的项目，否则获取区域项目
	     * @author Jcall
	     * @version 2016-12-21
	     */
        //获取送检单位的项目，如果存在则获取本身的项目，否则获取区域项目
        var ClientNo = $("#form_ClientNo").combobox("getValue");
        getLabItem(ClientNo, function () {
            labcode = HasClientItem ? ClientNo : labcode;

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
        });

        //      $.ajax({
        //          dataType: 'json',
        //          contentType: 'application/json',
        //          url: GetTestItemUrl + "?supergroupno=" + supergroupno + "&page=" + page + "&rows=" + rows + "&itemkey=" + itemkey + "&labcode=" + labcode,
        //          success: function (result) {
        //              if (result.success) {
        //                  Shell.util.Msg.showLog("获取检验项目列表数据成功");
        //                  var data = Shell.util.JSON.decode(result.ResultDataValue) || [];
        //                  callback(data);
        //              } else {
        //                  Shell.util.Msg.showLog("获取检验项目列表数据失败！错误信息：" + result.ErrorInfo);
        //                  callback([]);
        //              }
        //          },
        //          error: function (request, strError) {
        //              Shell.util.Msg.showLog("获取检验项目列表数据失败！错误信息：" + strError);
        //              callback([]);
        //          }
        //      });
    }

    /**
     * 获取送检单位的项目，如果存在则获取本身的项目，否则获取区域项目
     * @author Jcall
     * @version 2016-12-21
     */
    function getLabItem(labcode, callback) {
        $.ajax({
            dataType: 'json',
            contentType: 'application/json',
            url: GetTestItemUrl + "?supergroupno=COMBI&page=1&rows=1&labcode=" + labcode,
            success: function (result) {
                if (result.success) {
                    var data = Shell.util.JSON.decode(result.ResultDataValue) || { total: 0, rows: [] };
                    HasClientItem = data.total == 0 ? false : true;
                    callback();
                } else {
                    HasClientItem = false;
                    callback();
                }
            },
            error: function (request, strError) {
                HasClientItem = false;
                callback();
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
            url: NrequestFormAddOrUpdateUrl,
            data: Shell.util.JSON.encode({ jsonentity: entity }),
            success: function (result) {
                if (result.success) {
                    Shell.util.Msg.showLog("保存申请单信息成功,保存后的主键：");
                    callback();
                } else {
                    Shell.util.Msg.showLog("保存申请单信息失败！错误信息：" + result.ErrorInfo);
                    $.messager.alert("错误信息", "保存申请单信息失败！错误信息：" + result.ErrorInfo, "error");
                    $('#save_barcode_list').linkbutton('enable');
                }
            },
            error: function (request, strError) {
                Shell.util.Msg.showLog("保存申请单信息失败！错误信息：" + strError);
                $.messager.alert("错误信息", strError, "error");
                $('#save_barcode_list').linkbutton('enable');
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
    /**获取区域字典列表*/
    function GetClientEleArea(callback) {
        $.ajax({
            dataType: 'json',
            contentType: 'application/json',
            url: GetPubDictUrl + "?tableName=ClientEleArea&fields=AreaID,AreaCName,ClientNo",
            success: function (result) {
                if (result.success) {
                    Shell.util.Msg.showLog("获取区域字典成功");
                    var data = Shell.util.JSON.decode(result.ResultDataValue) || {},
						list = data.rows || [];
                    callback(list);
                } else {
                    Shell.util.Msg.showLog("获取区域字典失败！错误信息：" + result.ErrorInfo);
                }
            },
            error: function (request, strError) {
                Shell.util.Msg.showLog("获取区域字典失败！错误信息：" + strError);
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
        checkOnSelect: false,
        selectOnCheck: false,
        loadMsg: '数据加载中...',
        method: 'get',
        toolbar: '#checked_grid_toolbar',
        columns: [[
			{ field: 'chk', checkbox: true, hidden: false },
            { field: 'ItemNo', title: '编号', width: 100, hidden: true },
            {
                field: 'CName', title: '名称', width: 100, tooltip: function (value, index, row) {
                    return "<b>" + row.CName + "</b>";
                }
            },
            { field: 'EName', title: '英文名', width: 100, hidden: true },
            {
                field: 'operator', title: '操作', width: 20, formatter: function (val, row, index) {

                    var delIcon = "<a class='l-btn l-btn-plain'><span class='l-btn-left'><span class='l-btn-text icon-no l-btn-icon-left'>&nbsp;</span></span></a>";
                    return delIcon;

                }
            }
        ]],

        onCheck: function (index, row) {
            var rowData = {};
            rowData.ItemNo = row.ItemNo;
            rowData.CName = row.CName;
            if (checkedItemsCookies != []) {
                var length = checkedItemsCookies.length,
					exist = false,
					 rowCookie = null;
                for (var i = 0; i < length; i++) {
                    rowCookie = checkedItemsCookies[i];
                    if (rowCookie.ItemNo == row.ItemNo) {
                        exist = true;
                        break;
                    }
                }
                if (!exist) {
                    checkedItemsCookies.push(rowData);
                }
            } else {
                checkedItemsCookies.push(rowData);
            }

            document.cookie = checkedCookieName + '=' + JSON.stringify(checkedItemsCookies) + ';expires=' + expireDate + ';path=\/';
        },
        onUncheck: function (index, row) {
            if (checkedItemsCookies != []) {
                var length = checkedItemsCookies.length,
					rowCookie = null;
                for (var i = length - 1; i >= 0; i--) {
                    rowCookie = checkedItemsCookies[i];
                    if (rowCookie.ItemNo == row.ItemNo) {
                        checkedItemsCookies.splice(i, 1);
                    }
                }
                document.cookie = checkedCookieName + '=' + JSON.stringify(checkedItemsCookies) + ';expires=' + expireDate + ';path=\/';
            }

        },
        onCheckAll: function (rows) {
            var length = rows.length,
				rowData = {};
            checkedItemsCookies = [];
            for (var i = 0; i < length; i++) {
                rowData = {};
                rowData.ItemNo = rows[i].ItemNo;
                rowData.CName = rows[i].CName;
                checkedItemsCookies.push(rowData);
            }
            document.cookie = checkedCookieName + '=' + JSON.stringify(checkedItemsCookies) + ';expires=' + expireDate + ';path=\/';
        },
        onUncheckAll: function (rows) {
            checkedItemsCookies = [];
            document.cookie = checkedCookieName + '=' + JSON.stringify(checkedItemsCookies) + ';expires=' + expireDate + ';path=\/';
        }
    });

    //批量固定/取消项目
    $('#markItems').bind('click', function () {
        if ($(this).is(':checked')) {
            $('#checked_grid').datagrid('checkAll');
        } else {
            $('#checked_grid').datagrid('uncheckAll');
        }
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
            handler: saveInfo
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
						"<input id='barcode_list_row_value_" + row.ColorValue + "' style='width:100%' value='" + (row.BarCode || "") + 

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
			            arr.push("<option value='" + list[i].SampleTypeID + "'" + (list[i].selected ? " selected='selected'" : "") + ">" + list

[i].CName + "</option>");
			        }

			        arr.push("</select>");
			        return arr.join("");
			    }
			}
        ]]
    });

    //获取区域字典列表
    GetClientEleArea(function (list) {
        clientEleAreaList = list || [];
        GetClientNoData(function (data) {
            $('#form_ClientNo').combobox("loadData", data);
            if (NRequestFormNo) {//修改状态
                //显示申请号
                hideNRequestFormNo(false);
                //获取申请单信息
                getInfo();
            }
        });
    });

    //采样时间和开单时间默认当前
    var date = Shell.util.Date.toString(new Date());
    var datetimeboxList = ["form_OperDate", "form_CollectDate"];
    //var datetimeboxList = ["form_CollectDate"];
    for (var i in datetimeboxList) {
        $("#" + datetimeboxList[i]).datetimebox("setValue", date);
    }
    //隐藏申请号
    hideNRequestFormNo(true);

    /*
     * 年龄类型单选
     * @author Jcall
     * @version 2016-10-25
     */
    $("input:radio[name='ageRadio']").change(function () {
        var age = $("#form_Age"),
			ageUnitNo = $("#form_AgeUnitNo"),
			checked = $("input:radio[name='ageRadio']:checked").val();

        if (checked == "0") {
            //启用年龄和年龄单位
            age.numberbox("enable", true);
            ageUnitNo.combobox("enable", true);
        } else {
            //禁用年龄和年龄单位
            age.numberbox("disable", true);
            ageUnitNo.combobox("disable", true);
        }
    });

    /**
	 * @author Jcall
	 * @version 2016-12-13
	 */
    //添加医生
    $('#form_Doctor_Add').click(function (e) {
        openDoctorWin();
    });
    //弹出添加医生窗口
    function openDoctorWin() {
        //清空内容
        $("#form_Doctor_Add_Name").textbox("setValue", "");
        $("#form_Doctor_Add_No").numberbox('clear');
        $("#form_Doctor_Add_ShortCode").textbox("setValue", "");
        //显示窗口
        $("#form_Doctor_Add_Win").window('open');
    }
    //保存按钮
    $("#form_Doctor_Add_Win_Buttons_Save").click(function (e) {
        var Name = $("#form_Doctor_Add_Name").val();
        var No = $("#form_Doctor_Add_No").val();
        var ShortCode = $("#form_Doctor_Add_ShortCode").val();


        if (!Name || !No || !ShortCode) return;

        //保存医生信息
        saveDoctor(Name, No, ShortCode);
    });
    //取消按钮
    $("#form_Doctor_Add_Win_Buttons_Cancel").click(function (e) {
        //清空内容
        $("#form_Doctor_Add_Name").textbox("setValue", "");
        $("#form_Doctor_Add_No").numberbox('clear');
        $("#form_Doctor_Add_ShortCode").textbox("setValue", "");
        //隐藏窗口
        $("#form_Doctor_Add_Win").window('close');
    });

    //删除医生
    $('#form_Doctor_Del').click(function (e) {
        $("#dlg_Doctor").window('open');
    });


    //保存医生信息
    function saveDoctor(Name, No, ShortCode) {
        var url = Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/AddLabDoctorModel';

        var LabCode = $('#form_ClientNo').combobox('getValue');
        if (!LabCode) {
            Shell.util.Msg.showError("请选择送检单位！");
            return;
        }

        //判断该送检单位的医生是否已存在
        var hasNo = false;
        var list = $('#form_Doctor').combobox("getData");
        for (var i in list) {
            if (list[i].LabCode == LabCode && list[i].LabDoctorNo == No) {
                hasNo = true;
                break;
            }
        }
        if (hasNo) {
            Shell.util.Msg.showError("已存在相同编号的医生，请换一个编号！");
            return;
        }

        var entity = {
            LabCode: LabCode,
            LabDoctorNo: No,
            CName: Name,
            ShortCode: ShortCode,
            Visible: 1,
            UseFlag: 1
        };

        $.ajax({
            url: url,
            dataType: 'json',
            data: Shell.util.JSON.encode({ jsonentity: entity }),
            contentType: 'application/json',
            method: 'POST',
            success: function (data) {
                if (data.success) {
                    //重新获取医生列表
                    onReloadDoctor();
                    //隐藏窗口
                    $("#form_Doctor_Add_Win").window('close');

                } else {
                    var index = data.ErrorInfo.indexOf("PRIMARY KEY");
                    data.ErrorInfo = index == -1 ? data.ErrorInfo : "已存在相同编号的医生，请换一个编号";
                    Shell.util.Msg.showError(data.ErrorInfo);
                }
            },
            error: function (data) {
                var index = data.ErrorInfo.indexOf("PRIMARY KEY");
                data.ErrorInfo = index == -1 ? data.ErrorInfo : "已存在相同编号的医生，请换一个编号";
                Shell.util.Msg.showError(data.ErrorInfo);
            }
        });
    }
    //重新获取医生列表
    function onReloadDoctor(callback) {
        var url = GetPubDictUrl + "?tableName=B_Lab_Doctor&fields=LabDoctorNo,CName,ShortCode&labcode=" +
    		$('#form_ClientNo').combobox('getValue');
        $('#form_Doctor').combobox('reload', url);
        var urlg = GetPubDictUrl + "?tableName=B_Lab_Doctor&labcode=" +
           $('#form_ClientNo').combobox('getValue');
        $('#dgdoctor').datagrid('reload', urlg);
    }
    /**
     * 校验条码格式
     * @version 2017-07-19
     * @param {Object} barcode
     */
    /**
     * 校验条码格式
     * @version 2017-07-19
     * @param {Object} barcode
     */
    function barcodeIsValid(barcode) {
        //var bar = barcode.replace(/^\s|\s$/g, '#');
        //var error = "条码: " + bar + " 格式错误，正确格式: 长度10位的纯数字！";

        //if (!barcode) return error;//没有值
        //if (barcode.length != 10) return error;//非7位
        //if (isNaN(barcode)) return error;//非数字

        return true;
    }
    $("#Edit-Save-btn").click(function (e) {
        saveInfo();
    });
});

function doctordeleterow(doctorno) {
    //alert(doctorno);
    var LabCode = $('#form_ClientNo').combobox('getValue');
    if (!LabCode) {
        Shell.util.Msg.showError("请选择送检单位！");
        return;
    }
    var url = Shell.util.Path.rootPath + '/ServiceWCF/DictionaryService.svc/DeleteLabDoctorModelByID?labCode=' + LabCode + '&labDoctorNo=' + doctorno;
    
    $.ajax({
        url: url,
        dataType: 'json',
        contentType: 'application/json',
        method: 'GET',
        success: function (data) {
            if (data.success) {
                var GetPubDictUrl = Shell.util.Path.rootPath + "/ServiceWCF/DictionaryService.svc/GetPubDict"
                //重新获取医生列表
                var url = GetPubDictUrl + "?tableName=B_Lab_Doctor&fields=LabDoctorNo,CName,ShortCode&labcode=" +
           $('#form_ClientNo').combobox('getValue');
                $('#form_Doctor').combobox('reload', url);
                var urlg = GetPubDictUrl + "?tableName=B_Lab_Doctor&labcode=" +
                   $('#form_ClientNo').combobox('getValue');
                $('#dgdoctor').datagrid('reload', urlg);
                //隐藏窗口
                $("#dlg_Doctor").window('close');

            } else {
                Shell.util.Msg.showError(data.ErrorInfo);
            }
        },
        error: function (data) {
            Shell.util.Msg.showError(data.ErrorInfo);
        }
    });
}
