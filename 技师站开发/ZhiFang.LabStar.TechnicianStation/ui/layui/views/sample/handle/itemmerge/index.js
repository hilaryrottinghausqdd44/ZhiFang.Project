/**
 * 项目合并(糖耐量)
 * @author liangyl
 * @version 2021-05-26
 */
layui.extend({
	uxutil: 'ux/util',
	uxbase: 'ux/base',
	uxtable: 'ux/table',
	uxbasic: 'views/sample/batch/uxbasic',
	patienttable: 'views/sample/handle/itemmerge/patient',
	itemtable: 'views/sample/handle/itemmerge/item',
	resulttable: 'views/sample/handle/itemmerge/result',
	echartline: 'views/sample/handle/itemmerge/line',
	tableSelect: '../src/tableSelect/tableSelect'
}).use(['uxbasic', 'uxutil','uxbase', 'form', 'patienttable', 'itemtable', 'resulttable', 'echartline', 'tableSelect'], function () {

	var $ = layui.$,
		uxbasic = layui.uxbasic,
		form = layui.form,
		patienttable = layui.patienttable,
		itemtable = layui.itemtable,
		resulttable = layui.resulttable,
		echartline = layui.echartline,
		tableSelect = layui.tableSelect,
		uxbase = layui.uxbase,
		uxutil = layui.uxutil;

	//获取项目
	var GET_ITEM_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBItemByHQL?isPlanish=true';
	//合并生成图形
	var ADD_URL = uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_LisTestItemMergeByVOEntity';
	//获取要合并的检验样本项目信息
	var GET_ITEM_MERGE_INFO_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_QueryItemMergeInfo';
	//组合项目的子项目
	var GET_GROUP_ITEM_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QueryLBItemGroupByHQL?isPlanish=true';
	//组合项目的子项的下拉html
	var GROUP_ITEM_HTML = "";

	//是否已加载
	var isLoad = false;
	//组合项目是否已加载
	var isGroupLoad = false;
	//自定义保存变量
	var saveErrorCount = 0, saveCount = 0, saveLength = 0;

	//检验样本信息列表
	var table0_Ind = patienttable.render({
		elem: '#patient_table',
		height: 'full-125',
		title: '检验样本信息列表',
		size: 'sm',
		done: function (res, curr, count) {
			if (count > 0) {
				//默认选择第一行
				var rowIndex = 0;
				//默认选择行
				uxbasic.doAutoSelect(this, rowIndex);
			} else {
				if (table1_Ind) table1_Ind.clearData();
				if (table2_Ind) table2_Ind.clearData();
				if (line_Ind) line_Ind.clearData();
			}
		}
	});
	table0_Ind.instance.reload({ data: [] });
	//单击行
	table0_Ind.table.on('row(patient_table)', function (obj) {
		//标注选中样式
		obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
		isLoad = false;

		table1_Ind.loadData(obj.data);
	});
	//样本结果详细列表
	var table1_Ind = itemtable.render({
		elem: '#item_table',
		height: 'full-350',
		title: '样本结果详细列表',
		size: 'sm',
		done: function (res, curr, count) {
			if (count > 0) {
				$("select[name='ismerge']").parent('div.layui-table-cell').css('overflow', 'visible');
				$("select[name='item']").parent('div.layui-table-cell').css('overflow', 'visible');

				$("select[name='item']").empty();
				$("select[name='item']").append(GROUP_ITEM_HTML);

				var that = this.elem.next();
				for (var i = 0; i < res.data.length; i++) {
					var IsMerge = res.data[i].LBMergeItemVO_IsMerge;
					var item = res.data[i].LBMergeItemVO_ChangeItemID;
					var trRow = that.find(".layui-table-box tbody tr[data-index='" + i + "']");
					$(trRow).find("td").each(function () {
						var fieldName = $(this).attr("data-field");
						var selectJq = $(this).find("select");
						if (selectJq.length == 1) {
							if (IsMerge == res.data[i][fieldName]) {
								$(this).children().children().val(IsMerge);
							}
							if (item == res.data[i][fieldName]) {
								$(this).children().children().val(item);
							}
						}
					});
				}

				form.render('select');
				//默认选择第一行
				var rowIndex = 0;
				//默认选择行
				uxbasic.doAutoSelect(this, rowIndex);
			} else {
				if (table2_Ind) table2_Ind.clearData();
				if (line_Ind) line_Ind.clearData();
			}
		}
	});
	table1_Ind.instance.reload({ data: [] });
	//单击行
	table1_Ind.table.on('row(item_table)', function (obj) {
		//标注选中样式
		obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');

		if (!isLoad) {
			table2_Ind.loadData({ 'where': 'listestitem.LisTestForm.Id=' + obj.data.LBMergeItemVO_LisTestItem_LisTestForm_Id });
			line_Ind.config.ITEMLIST = table1_Ind.table.cache.item_table;
			line_Ind.loadData(table1_Ind.table.cache.item_table);
		}
		isLoad = true;
	});
	//样本结果详细列表
	var table2_Ind = resulttable.render({
		elem: '#item_result_table',
		height: 'full-315',
		title: '样本结果详细列表',
		size: 'sm'
	});
	table2_Ind.instance.reload({ data: [] });
	//图形
	var line_Ind = echartline.render({});
	//关闭
	$('#close').on('click', function () {
		parent.layer.closeAll('iframe');
	});
	//合并
	$('#save').on('click', function () {
		//检验样本
		var records = table0_Ind.table.checkStatus('patient_table').data;
		if (records.length == 0) {
			uxbase.MSG.onWarn("请勾选检验样本行!");
			return false;
		}
		var items = table1_Ind.table.cache.item_table;
		itemslen = items.length;
		if (itemslen == 0) {
			uxbase.MSG.onWarn("项目不能为空!");
			return false;
		}
		//校验
		var isExec = table1_Ind.isValid();
		if (!isExec) {
			uxbase.MSG.onWarn('合并项必须有一个是"是"!');
			return;
		}
		onSaveClick(records, items);
	});
	//转化项目
	form.on('select(item)', function (data) {
		var items = table1_Ind.table.cache.item_table;
		var elem = data.othis.parents('tr');
		var dataindex = elem.attr("data-index");
		//当前操作行
		var rowobj = items[dataindex];
		//改变后的数据
		if (data.value == rowobj.LBMergeItemVO_ChangeItemID) return;
		var bo = true;
		for (var i = 0; i < items.length; i++) { //从节点中取出子节点依次遍历
			if (data.value == items[i].LBMergeItemVO_ChangeItemID) {
				uxbase.MSG.onWarn("该转化项目已存在,请重新选择!");
				table1_Ind.instance.reload({ data: items });
				bo = false;
				break;
			}
		}

		if (bo == true) {
			for (var i = 0; i < items.length; i++) { //从节点中取出子节点依次遍历
				if (rowobj.LBMergeItemVO_ChangeItemID == items[i].LBMergeItemVO_ChangeItemID) {
					items[i].LBMergeItemVO_ChangeItemID = data.value;
					items[i].LBMergeItemVO_ChangeItemName = $("#item option:selected").text();
					items[i].LBMergeItemVO_ChangeItemDispOrder = $("#item option:selected").attr("data_DispOrder");
					break;
				}
			}
			items = items.sort(compare('LBMergeItemVO_ChangeItemDispOrder'));
			table1_Ind.instance.reload({ data: items });
			//图形重新生成
			line_Ind.loadData(table1_Ind.table.cache.item_table);
		}
	});

	//合并项目
	form.on('select(ismerge)', function (data) {
		var items = table1_Ind.table.cache.item_table;
		var elem = data.othis.parents('tr');
		var dataindex = elem.attr("data-index");
		//当前操作行
		var rowobj = items[dataindex];
		//改变后的数据
		if (data.value == rowobj.LBMergeItemVO_IsMerge) return;
		for (var i = 0; i < items.length; i++) { //从节点中取出子节点依次遍历
			items[i].LBMergeItemVO_IsMerge = '0';
			if (rowobj.LBMergeItemVO_ChangeItemID == items[i].LBMergeItemVO_ChangeItemID) {
				items[i].LBMergeItemVO_IsMerge = data.value;
			}
		}
		table1_Ind.instance.reload({ data: items });
		//图形重新生成
		if (data.value == 1) line_Ind.loadData(table1_Ind.table.cache.item_table);
		else
			line_Ind.clearData();

	});
	//下拉框 -- icon 前存在icon 则点击该icon 等同于点击input
	$("input.layui-input+.layui-icon").on('click', function () {
		if (!$(this).hasClass("myDate") && !$(this).hasClass("myPhrase")) {
			$(this).prev('input.layui-input')[0].click();
			return false;//不加的话 不能弹出
		}
	});
	//初始化
	function init() {
		//日期初始化
		var today = new Date();
		var defaultvalue = uxutil.date.toString(uxutil.date.getNextDate(today, -1), true) + " - " + uxutil.date.toString(today, true);
		uxbasic.initDate('GTestDate', defaultvalue, 'form', true);
		//初始化系统下拉框
		initSystemSelect();
	}
	//初始化
	init();

	//初始化系统下拉框
	function initSystemSelect() {
		itemList('LBItem_CName', 'ItemID');
	}

	function onSaveClick(records, items) {
		saveErrorCount = 0;
		saveCount = 0;
		saveLength = records.length;
		var index = layer.load();
		//选择行的病历号
		var strPatNo = getPatNo(records, items);
		for (var i = 0; i < records.length; i++) {
			var PatNo = records[i].LBMergeItemFormVO_PatNo;
			//当前选择行的项目信息
			if (strPatNo == PatNo) {
				var listVO = listLBMergeItemVO(items);
				OneUpdate(listVO, index);
			} else {
				var CName = records[i].LBMergeItemFormVO_CName;
				var isMerge = records[i].LBMergeItemFormVO_IsMerge;
				ItemMergeInfo(PatNo, CName, isMerge, function (data) {
					OneUpdate(data, index);
				});
			}
		}
	}
	//保存封装listLBMergeItemVO
	function listLBMergeItemVO(items) {
		var listVO = [];
		for (var j = 0; j < items.length; j++) {
			var obj = {
				ParItemID: items[j].LBMergeItemVO_ParItemID,
				ParItemName: items[j].LBMergeItemVO_ParItemName,
				ChangeItemID: items[j].LBMergeItemVO_ChangeItemID,
				ChangeItemName: items[j].LBMergeItemVO_ChangeItemName,
				IsMerge: items[j].LBMergeItemVO_IsMerge ? 1 : 0,
				LBChangeItem: { Id: items[j].LBMergeItemVO_ChangeItemID, DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] },
				LisTestItem: { Id: items[j].LBMergeItemVO_LisTestItem_Id, LisTestForm: { Id: items[j].LBMergeItemVO_LisTestItem_LisTestForm_Id, MainStatusID: items[j].LBMergeItemVO_LisTestItem_LisTestForm_MainStatusID, DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] }, LBItem: { Id: items[j].LBMergeItemVO_LisTestItem_LBItem_Id, DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] } }
			};
			listVO.push(obj);
		}
		return listVO;
	}
	function OneUpdate(listLBMergeItemVO, index) {

		var params = { listLBMergeItemVO: listLBMergeItemVO };
		var config = {
			type: 'post',
			url: ADD_URL,
			data: JSON.stringify(params)
		};
		uxutil.server.ajax(config, function (data) {
			if (data.success) {
				saveCount++;
			} else {
				saveErrorCount++;
				uxbase.MSG.onError(data.msg);
			}
			onSaveEnd(index);
		});
	}
	function onSaveEnd(index) {
		if (saveCount + saveErrorCount == saveLength) {
			layer.close(index);//隐藏遮罩层
			if (saveErrorCount == 0) {
				line_Ind.loadData(table1_Ind.table.cache.item_table);
				line_Ind.onUploadImg();
			} else {
				uxbase.MSG.onError("存在失败信息，请重新保存!");
			}
		}
	}
	//获取选择行的的病历号 ,需要从项目列表匹配
	function getPatNo(recs, items) {
		var strPatNo = "";
		for (var i = 0; i < recs.length; i++) {
			var PatNo = recs[i].LBMergeItemFormVO_PatNo;
			if (strPatNo) return;
			for (var j = 0; j < items.length; j++) {
				if (PatNo == items[j].LBMergeItemVO_LisTestItem_LisTestForm_PatNo) {
					strPatNo = PatNo;
					break;
				}
			}
		}
		return strPatNo;
	}

	//获取要合并的项目信息
	function ItemMergeInfo(patNo, cName, isMerge, callback) {
		var GTestDate = $('#GTestDate').val();
		var StartDate = GTestDate.split(" - ")[0],
			EndDate = GTestDate.split(" - ")[1];
		var params = {
			beginDate: StartDate,
			endDate: EndDate,
			fields: 'LBMergeItemVO_ChangeItemID,LBMergeItemVO_ChangeItemName,LBMergeItemVO_ParItemID,LBMergeItemVO_ParItemName,LBMergeItemVO_LisTestItem_LBItem_CName,LBMergeItemVO_LisTestItem_LBItem_Id,LBMergeItemVO_LisTestItem_LisTestForm_GSampleNo,LBMergeItemVO_LisTestItem_Id,LBMergeItemVO_LisTestItem_ReportValue,LBMergeItemVO_IsMerge,LBMergeItemVO_LisTestItem_TestTime,LBMergeItemVO_LisTestItem_ReceiveTime,LBMergeItemVO_LisTestItem_LisTestForm_Id,LBMergeItemVO_LisTestItem_LisTestForm_MainStatusID,LBMergeItemVO_LisTestItem_LisTestForm_PatNo,LBMergeItemVO_LisTestItem_EquipID,LBMergeItemVO_ChangeItemDispOrder',
			isPlanish: true,
			patNo: patNo,
			cName: cName,
			itemID: $('#ItemID').val()
		};
		var config = {
			type: 'post',
			url: GET_ITEM_MERGE_INFO_LIST_URL,
			data: JSON.stringify(params)
		};
		uxutil.server.ajax(config, function (data) {
			if (data.success) {
				var list = (data.value || {}).list || [];
				var listVO = [];
				if (list.length > 0) listVO = listLBMergeItemVO(list);
				callback(listVO);
			} else {
				uxbase.MSG.onError(data.ErrorInfo);
			}
		});
	}
	var isLoad = false;
	//初始化项目下拉框
	function itemList(CNameElemID, IdElemID) {
		var CNameElemID = CNameElemID || null,
			IdElemID = IdElemID || null;
		var fields = ['Id', 'CName', 'SName', 'Shortcode'],
			url = GET_ITEM_LIST_URL + "&where=(lbitem.IsUnionItem=1 and lbitem.GroupType=1) and lbitem.IsUse=1";
		url += '&fields=LBItem_' + fields.join(',LBItem_');
		var height = $('#itemmerge').height() - 150;
		if (!CNameElemID) return;
		tableSelect.render({
			elem: '#' + CNameElemID,	//定义输入框input对象 必填
			checkedKey: 'LBItem_Id', //表格的唯一建值，非常重要，影响到选中状态 必填
			searchKey: 'lbitem.CName,lbitem.Shortcode,lbitem.SName',	//搜索输入框的name值 默认keyword
			searchPlaceholder: '小组名称/简称/代码',	//搜索输入框的提示文字 默认关键词搜索
			table: {	//定义表格参数，与LAYUI的TABLE模块一致，只是无需再定义表格elem
				url: url,
				height: height,
				autoSort: false, //禁用前端自动排序
				page: true,
				limit: 50,
				limits: [50, 100, 200, 500, 1000],
				size: 'sm', //小尺寸的表格
				cols: [[
					{ type: 'radio' },
					{ type: 'numbers', title: '行号' },
					{ field: 'LBItem_Id', width: 150, title: '主键ID', sort: false, hide: true },
					{ field: 'LBItem_CName', width: 200, title: '项目名称', sort: false },
					{ field: 'LBItem_SName', width: 150, title: '简称', sort: false },
					{ field: 'LBItem_Shortcode', width: 120, title: '快捷码', sort: false }
				]],
				text: { none: '暂无相关数据' },
				response: function () {
					return {
						statusCode: true, //成功状态码
						statusName: 'code', //code key
						msgName: 'msg ', //msg key
						dataName: 'data' //data key
					}
				},
				parseData: function (res) {//res即为原始返回的数据
					if (!res) return;
					var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};

					if (data.list && data.list.length == 1 && !isLoad) {//默认选择第一个仪器
						$("#ItemID").val(data.list[0].LBItem_Id);
						$("#LBItem_CName").val(data.list[0].LBItem_CName);
						//组合项目的子项目加载
						groupitemList(function (html) {
							GROUP_ITEM_HTML = html;
						});
					}

					return {
						"code": res.success ? 0 : 1, //解析接口状态
						"msg": res.ErrorInfo, //解析提示文本
						"count": data.count || 0, //解析数据长度
						"data": data.list || []
					};
				}
			},
			done: function (elem, data) {
				//选择完后的回调，包含2个返回值 elem:返回之前input对象；data:表格返回的选中的数据 []
				if (data.data.length > 0) {
					var record = data.data[0];
					$(elem).val(record["LBItem_CName"]);
					if (IdElemID) $("#" + IdElemID).val(record["LBItem_Id"]);
					//组合项目的子项目加载
					groupitemList(function (html) {
						GROUP_ITEM_HTML = html;
					});
				} else {
					$(elem).val("");
					if (IdElemID) $("#" + IdElemID).val("");
				}
			}
		});
		$("#LBItem_CName+i.layui-icon").click();
		tableSelect.hide();


	}

	//获取组合项目子项
	function groupitemList(callback) {
		var fields = ['Id', 'CName', 'DispOrder'],
			url = GET_GROUP_ITEM_LIST_URL + "&where=GroupItemID=" + $('#ItemID').val();
		url += '&fields=LBItemGroup_LBItem_' + fields.join(',LBItemGroup_LBItem_');
		uxutil.server.ajax({
			url: url
		}, function (data) {
			if (data) {
				var list = (data.value || {}).list || [];
				var len = list.length,
					htmls = ['<option value="">请选择项目</option>'];
				for (var i = 0; i < len; i++) {
					htmls.push("<option value='" + list[i].LBItemGroup_LBItem_Id + "' data-DispOrder='" + list[i].LBItemGroup_LBItem_DispOrder + "'>" + list[i].LBItemGroup_LBItem_CName + "</option>");
				}
				callback(htmls.join(""));
			} else {
				uxbase.MSG.onError(data.msg);
			}
		});
	}
	//用于排序
	function compare(arg) {
		return function (a, b) {
			return a[arg] - b[arg];
		}
	}

});