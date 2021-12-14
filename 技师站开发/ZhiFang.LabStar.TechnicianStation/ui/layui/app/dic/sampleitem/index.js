/**
	@name：项目样本类型维护
	@author：longfc
	@version 2019-09-21
 */
layui.extend({
	uxutil: '/ux/util',
	uxtable: '/ux/table',
	dictselect: 'modules/pre/dictselect',
	sampletypeTable: 'app/dic/sampleitem/sampletypeTable',
	lbItemTable: 'app/dic/sampleitem/lbItemTable',
	sampleitemTable: 'app/dic/sampleitem/sampleitemTable',
	cachedata: 'common/cachedata'
}).use(['uxutil', 'dictselect', 'table', 'form', 'sampletypeTable', 'lbItemTable', 'sampleitemTable', 'cachedata'],
	function() {
		"use strict";

		var $ = layui.$,
			element = layui.element,
			table = layui.table,

			form = layui.form,
			uxutil = layui.uxutil,
			dictselect = layui.dictselect,
			sampletypeTable = layui.sampletypeTable,
			lbItemTable = layui.lbItemTable,
			cachedata = layui.cachedata,
			sampleitemTable = layui.sampleitemTable;
		//列表实例对象	
		var sampletypeTable1 = null,
			lbItemTable1 = null,
			sampleitemTable1 = null;
		var height = $(document).height();
		//左侧列表当前选择行信息
		var curRowInfo = {

		};
		/**默认传入参数*/
		var defaultParams = null;
		//初始化默认传入参数信息
		function initParams() {
			//接收传入参数
			var params = uxutil.params.get();
		};
		//获取左侧列表高度
		function getTableHeight1() {
			var height1 = $('[lay-filter="div_filter_of_set"]').height();
			var height2 = $('[lay-filter="div_filter_of_sampletype"]').height();
			var height3 = height - height1 - height2 - 51;
			return height3;
		};
		//按样本类型设置列表配置信息
		function getSampleTypeTableConfig() {
			var height3 = getTableHeight1();
			return {
				title: '按样本类型设置',
				elem: '#table_sampletype',
				id: "table_sampletype",
				filter: "table_sampletype",
				height: height3,
				size:'sm'
			};
		};
		//按检验项目设置列表配置信息
		function getLBItemTableConfig() {
			var height3 = getTableHeight1();
			return {
				title: '按检验项目设置',
				elem: '#table_testitem',
				id: "table_testitem",
				filter: "table_testitem",
				height: height3,
				size:'sm'
			};
		};
		//获取右侧列表高度
		function getTableHeight2() {
			var height1 = $('[lay-filter="div_filter_of_set"]').height();
			var height2 =0;// $('[lay-filter="div_choose_fieldset"]').height();
			var height3 = height - height1 - height2 - 46;
			return height3;
		};
		//项目样本类型列表配置信息
		function getSampleItemTableConfig() {
			var height3 = getTableHeight2();
			return {
				title: '项目样本类型列表',
				elem: '#table_sampleitem',
				id: "table_sampleitem",
				filter: "table_sampleitem",
				height: height3,
				size:'sm'
			};
		};

		//按样本类型设置列表事件监听
		function onSampleTypeTable() {
			table.on('row(table_sampletype)', function(obj) {
				//行选中样式
				obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
				curRowInfo = obj.data;
				curRowInfo.choose_set = "of_sampletype";
				setTimeout(function() {
					onRefreshSampleItemTable();
				}, 300);
			});
		};
		//按检验项目设置列表事件监听
		function onLBItemTable() {
			table.on('row(table_testitem)', function(obj) {
				//行选中样式
				obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
				curRowInfo = obj.data;
				curRowInfo.choose_set = "of_testitem";
				setTimeout(function() {
					onRefreshSampleItemTable();
				}, 300);
			});
		};
		//项目样本类型列表事件监听
		function onSampleItemTable() {
			//行单击事件
			table.on('row(table_sampleitem)', function(obj) {
				obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');			
			});
		};
		//各表单组件事件监听
		function onFormEvent() {
			//设置表单的单选事件
			form.on('radio(form_left_of_set)', function(data) {
				setTimeout(function() {
					onRadioOfSet(data);
				}, 800);
			});
			//检验小组下拉选择
			form.on('select(select_section)', function(data) {
				onSelectSection(data);
			});
			//按钮事件
			var active = {
				//选择样本类型
				choose_testitem: function() {
					onChooseTestitem();
				},
				//选择检验项目
				choose_sampletype: function() {
					onChooseSampletype();
				},
				search_testitem: function() {
					onRefreshSampleItemTable();
				},
				//删除关系
				delete: function() {
					onDelete();
				}
			};
			$('.div_choose_testitem .layui-btn').on('click', function() {
				var type = $(this).data('type');
				active[type] ? active[type].call(this) : '';
			});
			//按样本类型列表的查询输入框查询
			$('#table_sampletype_like_search').on('keydown', function(event) {
				if (event.keyCode == 13) onRefreshSampleTypeTable();
			});
		};
		//设置表单的单选事件
		function onRadioOfSet(data) {
			curRowInfo.choose_set = data.value;
			sampleitemTable1.clearData();
			switch (data.value) {
				case "of_sampletype":
					$('[lay-filter="filter_of_testitem"]').removeClass("layui-show").addClass("layui-hide");
					$('[lay-filter="filter_of_sampletype"]').removeClass("layui-hide").addClass("layui-show");

					$('[lay-filter="div-choose_testitem"]').removeClass("layui-hide").addClass("layui-inline");
					$('[lay-filter="div-choose_sampletype"]').removeClass("layui-show").addClass("layui-hide");
					
					$('#choose_testitem').text('选择检验项目');
					//$('[data-type="choose_testitem"]').removeClass("layui-hide").addClass("layui-show");
					//$('[data-type="choose_sampletype"]').removeClass("layui-show").addClass("layui-hide");
					onRefreshSampleTypeTable();
					break;
				default:
					$('[lay-filter="filter_of_sampletype"]').removeClass("layui-show").addClass("layui-hide");
					$('[lay-filter="filter_of_testitem"]').removeClass("layui-hide").addClass("layui-show");

					$('[lay-filter="div-choose_sampletype"]').removeClass("layui-hide").addClass("layui-inline");
					$('[lay-filter="div-choose_testitem"]').removeClass("layui-show").addClass("layui-hide");
					//$('[data-type="choose_sampletype"]').removeClass("layui-hide").addClass("layui-show");
					//$('[data-type="choose_testitem"]').removeClass("layui-show").addClass("layui-hide");					
					
					$('#choose_testitem').text('选择样本类型');
					onRefreshLBItemTable();
					break;
			}
			//重新加载项目样本类型列表

		};
		//检验小组下拉选择事件
		function onSelectSection(data) {
			//先清空当前关系列表数据
			sampleitemTable1.clearData();
			lbItemTable1.config.lbsectionId = data.value;
			onRefreshLBItemTable();
		};
		//选择检验项目
		function onChooseTestitem() {
			//var choose_set="of_testitem";
			onOpenWin();
		};
		//选择样本类型
		function onChooseSampletype() {
			//var choose_set="of_sampletype";
			onOpenWin();
		};

		function onDelete() {
			var checkStatus = table.checkStatus("table_sampleitem");
			var data = checkStatus.data; //得到选中的数据
			if (data.length === 0) {
				return layer.msg('请选择数据后再操作');
			}
			layer.confirm('确定要删除当前选择行信息吗？', function(index) {
				layer.close(index);
				sampleitemTable1.onDelete(data, function(result) {
					if (result.success == true) onRefreshSampleItemTable();
				});
			});
		};
		//弹出选择窗体
		function onOpenWin(callback) {
			if (!curRowInfo) {
				layer.msg("请在左侧列表选择行后再操作!");
			}
			var title = "按检验项目维护项目样本类型关系";
			var content = '../sampleitem/transfer/oflbitem/index.html?';
			var params = [];
			params.push("chooseset=" + curRowInfo.choose_set);
			switch (curRowInfo.choose_set) {
				case "of_sampletype":
					title = '按样本类型维护项目样本类型关系';
					params.push("id=" + curRowInfo["LBSampleType_Id"]);
					params.push("cname=" + curRowInfo["LBSampleType_CName"]);
					break;
				default:
					content = '../sampleitem/transfer/oflbsample/index.html?';
					params.push("id=" + curRowInfo["LBItem_Id"]);
					params.push("cname=" + curRowInfo["LBItem_CName"]);
					break;
			}
			params = params.join('&');
			var url = content + params;
			layer.open({
				type: 2,
				title: title,
				area: ['88%', '92%'],
				content: url,
				id: "transfer_index",
				btn: null,
				yes: function(index, layero) {
					//me.onSave(index, layero, 1);
					//return false;
				},
				end: function() {
					var success = cachedata.getCache("onAddLBSampleItem");
					if (success == true && callback) {
						cachedata.delete("onAddLBSampleItem");
						callback();
					} else {
						onRefreshSampleItemTable();
					}
				},
				cancel: function(index, layero) {

				}
			});
		};
		/**初始化按样本类型设置列表*/
		function initSampleTypeTable() {
			var config = getSampleTypeTableConfig();
			sampletypeTable1 = sampletypeTable.render(config);
			onSampleTypeTable();
		};
		/**初始化按检验项目设置列表*/
		function initLBItemTable() {
			var config = getLBItemTableConfig();
			lbItemTable1 = lbItemTable.render(config);
			onLBItemTable();
		};
		/**初始化项目样本类型列表*/
		function initSampleItemTable() {
			var config = getSampleItemTableConfig();
			sampleitemTable1 = sampleitemTable.render(config);
			onSampleItemTable();
		};
		//初始化列表
		function initTable() {
			initSampleTypeTable();
			initLBItemTable();
			initSampleItemTable();
		};
		//刷新按样本类型设置列表
		function onRefreshSampleTypeTable() {
			sampletypeTable1.config.defaultParams = defaultParams;			
			sampletypeTable1 = sampletypeTable1.onSearch();
			onSampleTypeTable();
		};
		//刷新按检验项目设置列表
		function onRefreshLBItemTable() {
			lbItemTable1.config.defaultParams = defaultParams;
			lbItemTable1 = lbItemTable1.onSearch();
			onSampleTypeTable();
		};
		//刷新项目样本类型列表
		function onRefreshSampleItemTable() {
			var where = "";
			if (curRowInfo) {
				switch (curRowInfo.choose_set) {
					case "of_sampletype":
						where = "lbsampleitem.LBSampleType.Id=" + curRowInfo["LBSampleType_Id"];
						break;
					default:
						where = "lbsampleitem.LBItem.Id=" + curRowInfo["LBItem_Id"];
						break;
				}
			}
			if (where) sampleitemTable1.setExternalWhere(where);
			sampleitemTable1 = sampleitemTable1.onSearch(where);
		};
		//初始化检验小组下拉选择项
		function initSelectSection(callback) {
			dictselect.dictList.LBSection(function(html) {
				var filter = "select_section";
				$('[lay-filter="' + filter + '"]').empty().append(html);
				form.render('select', "form_testitem");
				if (callback) callback();
			});
		};
		//初始化
		function initAll() {
			initParams();
			initSelectSection();
			initTable();
			onFormEvent();
		};
		initAll();
	});
