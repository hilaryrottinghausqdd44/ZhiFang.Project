/**
	@name：项目样本类型维护-按样本类型选择项目
	@author：longfc
	@version 2019-09-27
 */
layui.extend({
	uxutil: '/ux/util',
	uxtable: '/ux/table',
	lefttable: 'app/dic/sampleitem/transfer/oflbsample/leftlist',
	righttable: 'app/dic/sampleitem/transfer/oflbsample/rightlist',
	dictselect: 'modules/pre/dictselect',
	cachedata: 'common/cachedata'
}).use(['uxutil', 'table', 'lefttable', 'righttable', 'dictselect', 'form', 'cachedata'], function() {
	"use strict";

	var $ = layui.$,
		uxutil = layui.uxutil,
		lefttable = layui.lefttable,
		righttable = layui.righttable,
		dictselect = layui.dictselect,
		cachedata = layui.cachedata,
		table = layui.table,
		form = layui.form;

	var tableInsLeft = null,
		tableInsRight = null;
	/**默认传入参数*/
	var defaultParams = {
		"chooseset": "", //按样本类型设置:of_sampletype;按检验项目设置:of_testitem;
		"id": "", //按样本类型设置时为样本类型Id;按按检验项目设置时为检验项目Id;
		"cname": ""
	};
	//初始化默认传入参数信息
	function initParams() {
		//接收传入参数
		var params = uxutil.params.get();
		if (params["chooseset"]) defaultParams.chooseset = params["chooseset"];
		if (params["id"]) defaultParams.id = params["id"];
		if (params["cname"]) defaultParams.cname = params["cname"];
	};
	//左列表配置信息
	function getLeftTableConfig() {
		var where = "lbsampleitem.LBItem.Id=" + defaultParams.id;
		var leftobj = {
			elem: '#lefttable',
			title: '项目样本类型信息',
			height: 'full-85',
			defaultWhere: where,
			size:'sm',
			onAfterLoad: function(list) {
				//联动右列表
				onRefreshRightTable(list);
			}
		};
		return leftobj;
	};
	//右列表配置信息
	function getRightTableConfig() {
		var rightobj = {
			elem: '#righttable',
			title: '样本类型列表',
			size:'sm',
			height: 'full-85'
		};
		return rightobj;
	};
	/**初始化左列表*/
	function initLeftTable() {
		tableInsLeft = lefttable.render(getLeftTableConfig());
		onLeftTable();
	};
	/**初始化右列表*/
	function initRightTable() {
		tableInsRight = righttable.render(getRightTableConfig());
		onRightTable();
	};
	//刷新左列表
	function onRefreshLeftTable() {
		tableInsLeft.config.defaultParams = defaultParams;
		tableInsLeft = tableInsLeft.onSearch();
	};
	//获取右列表默认条件
	function getDefaultWhere(data) {
		var params = [];
		var where = "";
		if (!data) data = table.cache['lefttable'];
		if (!data) data = [];
		for (var i = 0; i < data.length; i++) {
			params.push(data[i]["LBSampleItem_LBSampleType_Id"]);
		}
		if (params.length > 0) where = params.join(',');
		return where;
	};
	//刷新右列表
	function onRefreshRightTable(list) {
		tableInsRight.config.defaultParams = defaultParams;
		var where = getDefaultWhere(list);
		if (where) where = "lbsampletype.Id not in(" + where + ")";
		tableInsRight.setDefaultWhere(where);
		tableInsRight = tableInsRight.onSearch();
	};
	/**监听左列表*/
	function onLeftTable() {
		//监听行双击事件
		table.on('rowDouble(lefttable)', function(obj) {
			var tableBox = $(this).parents('.layui-table-box');
			var index = $(this).attr('data-index');
			var tableDiv = tableBox.find(".layui-table-body.layui-table-main");
			var checkCell = tableDiv.find("tr[data-index=" + index + "]").find(
				"td div.laytable-cell-checkbox div.layui-form-checkbox I");
			if (checkCell.length > 0) {
				checkCell.click();
			}
			addRight(obj);
		});
		//选中状态
		table.on('checkbox(lefttable)', function(obj) {

		});
	};
	/**监听右列表*/
	function onRightTable() {
		//监听行双击事件
		table.on('rowDouble(righttable)', function(obj) {
			var tableBox = $(this).parents('.layui-table-box');
			var index = $(this).attr('data-index');
			var tableDiv = tableBox.find(".layui-table-body.layui-table-main");
			var checkCell = tableDiv.find("tr[data-index=" + index + "]").find(
				"td div.laytable-cell-checkbox div.layui-form-checkbox I");
			if (checkCell.length > 0) {
				checkCell.click();
			}
			addLeft(obj);
		});
		//选中状态
		table.on('checkbox(righttable)', function(obj) {});
	};
	//表单项事件监听
	function onFormEvent() {
		var active = {
			add: function() {
				addLeft();
			},
			remove: function() {
				addRight();
			},
			reset: function() {
				onRefreshLeftTable();
				//onRefreshRightTable();
			},
			save: function() {
				onSaveClick();
			},
			search_rightTable: function() {
				onRefreshRightTable();
			}
		};
		$('.layui-btn').on('click', function() {
			var type = $(this).data('type');
			active[type] ? active[type].call(this) : '';
		});
		//检验小组下拉选择
		form.on('select(select_section)', function(data) {
			onSelectSection(data);
		});
		$(".fiexdHeight").css("height", ($(window).height() - 50) + "px"); //设置中间容器高度
		// 窗体大小改变时，调整高度显示
		$(window).resize(function() {
			var width = $(this).width();
			var height = $(this).height();
			//表单高度
			var height = $(document).height() - $(".fiexdHeight").offset().top - 50;
			$('#fiexdHeight').css("height", height);
		});
		//按样本类型列表的查询输入框查询
		$('#table_right_like_search').on('keydown', function(event) {
			if (event.keyCode == 13) onRefreshRightTable();
		});
	};
	//检验小组下拉选择事件
	function onSelectSection(data) {
		tableInsRight.config.lbsectionId = data.value;
		onRefreshRightTable();
	};
	/**
	 * 向左列表添加数据
	 * @param {Object} curObj 右列表当前选择的行,如果为空,取列表当前勾选的行
	 */
	function addLeft(curObj) {
		var leftData = table.cache['lefttable'];
		var rightData = table.cache['righttable'];
		var addArr = [];
		if (curObj)
			addArr.push(curObj.data)
		else
			addArr = table.checkStatus('righttable').data;
		//获取右列表选择数据
		if (addArr.length == 0) {
			layer.msg("未选择项目！", {
				icon: 5,
				anim: 6
			});
			return;
		}
		leftData.reverse();
		for (var i = 0; i < addArr.length; i++) {
			var obj = addArr[i];
			var obj2 = {
				'LBSampleItem_LBItem_Id': defaultParams.id,
				'LBSampleItem_LBItem_CName': defaultParams.cname,
				'LBSampleItem_LBSampleType_Id': obj.LBSampleType_Id,
				'LBSampleItem_LBSampleType_CName': obj.LBSampleType_CName,
				'LBSampleItem_LBSampleType_SName': obj.LBSampleType_SName,				
				'LBSampleItem_Id': obj.LBSampleItem_Id
			};
			//判断是否已存在左列表
			leftData.push(obj2);
		}
		//删除右列表对应的行
		rightData = delRightRows(rightData, addArr);
		table.reload('lefttable', {
			url: '',
			where: {},
			data: leftData
		});
		onRefreshRightTable();
//		table.reload('righttable', {
//			url: '',
//			where: {},
//			data: rightData
//		});
	};
	/**
	 * 向右边列表添加数据
	 * @param {Object} curObj 左列表当前选择的行,如果为空,取列表当前勾选的行
	 */
	function addRight(curObj) {
		var leftData = table.cache['lefttable'];
		var rightData = table.cache['righttable'];
		var addArr = [];
		if (curObj)
			addArr.push(curObj.data)
		else
			addArr = table.checkStatus('lefttable').data;
		//获取右列表选择数据
		if (addArr.length == 0) {
			layer.msg("未选择项目！", {
				icon: 5,
				anim: 6
			});
			return;
		}
		rightData.reverse();
		for (var i = 0; i < addArr.length; i++) {
			var obj = addArr[i];
			var obj2 = {
				'LBSampleType_Id': obj.LBSampleItem_LBSampleType_Id,
				'LBSampleType_CName': obj.LBSampleItem_LBSampleType_CName,
				'LBSampleType_SName': obj.LBSampleItem_LBSampleType_SName,	
				'LBSampleItem_Id': obj.LBSampleItem_Id
			};
			//判断是否已存在左列表
			rightData.push(obj2);
		}
		//删除左列表对应的行
		leftData = delLeftRows(leftData, addArr);
		table.reload('lefttable', {
			url: '',
			where: {},
			data: leftData
		});
		table.reload('righttable', {
			url: '',
			where: {},
			data: rightData
		});
	};
	/**
	 * 删除左列表数据的部分行
	 * @param {Object} allData 列表当前的数据集合
	 * @param {Object} delArr 列表需要被删除的集合
	 */
	function delLeftRows(allData, delArr) {
		for (var i = 0; i < delArr.length; i++) {
			var obj1 = delArr[i]["LBSampleItem_LBSampleType_Id"];
			for (var j = 0; j < allData.length; j++) {
				var obj2 = allData[j]["LBSampleItem_LBSampleType_Id"];
				if (obj1 == obj2) {
					allData.splice(j, 1);
					break;
				}
			}
		}
		return allData;
	};
	/**
	 * 删除右列表数据的部分行
	 * @param {Object} allData 列表当前的数据集合
	 * @param {Object} delArr 列表需要被删除的集合
	 */
	function delRightRows(allData, delArr) {
		for (var i = 0; i < delArr.length; i++) {
			var obj1 = delArr[i]["LBItem_Id"];
			for (var j = 0; j < allData.length; j++) {
				var obj2 = allData[j]["LBItem_Id"];
				if (obj1 == obj2) {
					allData.splice(j, 1);
					break;
				}
			}
		}
		return allData;
	};
	//保存
	function onSaveClick() {
		var url = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBSampleItemList';
		//新增实体
		var entityList = lefttable.getEntityList();
		var entity = {
			"entityList": entityList, //包括已经存在的关系及需要新增保存的关系
			"ofType": defaultParams.chooseset,
			"isHasDel": true //是否需要删除处理不存在当前集合的关系
		};
		var params = JSON.stringify(entity);
		//显示遮罩层
		var config = {
			type: "POST",
			url: url,
			data: params
		};
		uxutil.server.ajax(config, function(data) {
			cachedata.setCache("onAddLBSampleItem", data.success);
			//隐藏遮罩层
			if (data.success) {
				var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
				parent.layer.close(index); //再执行关闭
			} else {
				if (!data.msg) data.msg = '保存失败';
				layer.msg(data.msg, {
					icon: 5,
					anim: 6
				});
			}
		});
	};
	//相关表单数据项默认值赋值
	function initDefaultVal() {
		$("#sampletypeCName").text(defaultParams.cname);
	};
	//初始化检验小组下拉选择项
	function initSelectSection(callback) {
		dictselect.dictList.LBSection(function(html) {
			var filter = "select_section";
			$('[lay-filter="' + filter + '"]').empty().append(html);
			form.render('select', "form_right");
			if (callback) callback();
		});
	};
	//初始化入口
	function init() {
		initParams();
		initLeftTable();
		initRightTable();
		onFormEvent();
		initDefaultVal();
		initSelectSection();
	};
	init();
});
