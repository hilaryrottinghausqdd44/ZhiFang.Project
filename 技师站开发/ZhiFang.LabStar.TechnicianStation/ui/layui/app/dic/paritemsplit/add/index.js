/**
	@name：组合项目拆分
	@author：longfc
	@version 2019-10-14
 */
layui.extend({
	uxutil: '/ux/util',
	uxtable: '/ux/table',
	tableSelect: '../src/tableSelect/tableSelect',
	paritemsplitTable: 'app/dic/paritemsplit/add/paritemsplitTable',
	cachedata: 'common/cachedata'
}).use(['uxutil', 'table','tableSelect','form', 'paritemsplitTable','cachedata'],
	function() {
		"use strict";

		var $ = layui.$,
			element = layui.element,
			table = layui.table,
            tableSelect = layui.tableSelect,
			form = layui.form,
			uxutil = layui.uxutil,
			cachedata = layui.cachedata,
			paritemsplitTable = layui.paritemsplitTable;
		//列表实例对象	
		var paritemsplitTable1 = null;
		var height = $(document).height();
		//当前选择的组合项目Id
		var curParItemId = null;
		//右侧列表当前编辑行信息
		var paritemsplitCurRow = null;
		/**默认传入参数*/
		var defaultParams = null;
		//获取组合项目下拉框数据
		var ParItemSplit_URL = uxutil.path.ROOT +"/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchNotLBParItemSplitPLBItemListByHQL?isPlanish=true";
		//初始化默认传入参数信息
		function initParams() {
			//接收传入参数
			var params = uxutil.params.get();
		};
		//获取列表高度
		function getTableHeight2() {
			var height2 = $('[lay-filter="div_toolbar_paritemsplit"]').height();
			var height3 = $('[lay-filter="div_table_paritemsplit_memo"]').height();
			var height4 = height - height2 - height3 - 45;
			return height4;
		};
		//各表单组件事件监听
		function onFormEvent() {
	
			//采样组下拉选择
			form.on('select(select_LBParItemSplit_LBSamplingGroup_Id)', function(data) {
				var tr=data.othis.parents('tr').eq(0);
				var rowIndex= tr.data('index');
				var fields =table.cache[paritemsplitTable1.config.id][rowIndex];
				var textContent = $(data.elem).find("option:selected").text();
				fields["LBParItemSplit_LBSamplingGroup_Id"] = data.value;
				fields["LBParItemSplit_LBSamplingGroup_CName"] = textContent;
				paritemsplitTable1.updateRow(fields,rowIndex) //修改当前行数据
				form.render();
			});
			//是否合并检验报告开关监听
			form.on('switch(checkbox_IsAutoUnion)', function(data) {
				var tr=data.othis.parents('tr').eq(0);
				var rowIndex= tr.data('index');
				var fields =table.cache[paritemsplitTable1.config.id][rowIndex];
				fields["LBParItemSplit_IsAutoUnion"] = data.elem.checked;
				paritemsplitTable1.updateRow(fields,rowIndex) //修改当前行数据
				form.render();
			});
			//按钮事件
			var active = {
				//保存
				save: function() {
					onSave();
				},
				//查询
				search: function() {
					
				}
			};
			$('.layui-form .layui-btn').on('click', function() {
				var type = $(this).data('type');
				active[type] ? active[type].call(this) : '';
			});
				//下拉框 -- icon 前存在icon 则点击该icon 等同于点击input
		    $("input.layui-input+.layui-icon").on('click', function () {
		        if (!$(this).hasClass("myDate") && !$(this).hasClass("myPhrase")) {
		            $(this).prev('input.layui-input')[0].click();
		            return false;//不加的话 不能弹出
		        }
		    });
		};
		//组合项目下拉选择事件
		function onSelectParitem(id) {
			//先清空当前关系列表数据
			paritemsplitTable1.clearData();
			curParItemId = id;
			paritemsplitTable1.config.curParItemId = id;
			onRefreshparitemsplitTable();
		};
		//组合项目拆分列表配置信息
		function getparitemsplitTableConfig() {
			var height3 = getTableHeight2();
			return {
				title: '组合项目拆分信息',
				elem: '#table_paritemsplit',
				id: "table_paritemsplit",
				filter: "table_paritemsplit",
				height: height3,
				size:'sm'
			};
		};
		/**初始化组合项目拆分关系列表*/
		function initparitemsplitTable() {
			var config = getparitemsplitTableConfig();
			paritemsplitTable1 = paritemsplitTable.render(config);
			onparitemsplitTable();
		};
		//组合项目拆分关系列表
		function onparitemsplitTable() {
			//行单击事件
			table.on('row(table_paritemsplit)', function(obj) {
				obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
				paritemsplitCurRow = obj;
			});
			//监听单元格编辑
			table.on('edit(table_paritemsplit)', function(obj) {

			});
		};
		//初始化列表
		function initTable() {
			initparitemsplitTable();
		};
		//刷新组合项目拆分关系列表
		function onRefreshparitemsplitTable() {
			paritemsplitCurRow = null;
			paritemsplitTable1.setParItemId(curParItemId);
			paritemsplitTable1 = paritemsplitTable1.onSearch();
		};
		//初始化组合项目下拉选择项
		function initParItem(callback) {
			var where = "lbitem.GroupType=1";
			var url = uxutil.path.ROOT +
				"/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchNotLBParItemSplitPLBItemListByHQL?isPlanish=true&page=1&start=1&limit=10000&fields=LBItem_Id,LBItem_CName";
			url = url + "&where=" + where;
			url = url + "&t=" + new Date().getTime();
			uxutil.server.ajax({
				url: url
			}, function(data) {
				var html = "<option value=''></option>";
				if (data.success) {
					var result = data.value || [];
					if (result && result.list) result = result.list;
					for (var i = 0; i < result.length; i++) {
						html += '<option value="' + result[i]["LBItem_Id"] + '">' + result[i]["LBItem_CName"] + '</option>';
					}
				}
				var filter = "select_paritem";
				$('[lay-filter="' + filter + '"]').empty().append(html);
				form.render('select', "form_paritem");
				if (callback) callback();
			});
		};
		//保存处理
		function onSave() {
			var result = paritemsplitTable1.onSaveVerify();
			if (result.success == false) {
				layer.msg(result.msg);
				return;
			}
			var entityList = paritemsplitTable1.getEntityList();
			if (!entityList || entityList.length <= 0) {
				layer.msg("获取组合项目拆分信息为空!");
				return;
			}
			var url = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBParItemSplitList';
			var entity = {
				"entityList": entityList
			};
			var params = JSON.stringify(entity);
			//显示遮罩层
			var config = {
				type: "POST",
				url: url,
				data: params
			};
			uxutil.server.ajax(config, function(data) {
				cachedata.setCache("onAddLBParItemSplit", data.success);
				//隐藏遮罩层
				if (data.success) {
					var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
					parent.layer.close(index);
//					parent.afterParItemSplitUpdate(data);
				} else {
					if (!data.msg) data.msg = '新增组合项目拆分保存失败!';
					layer.msg(data.msg, {
						icon: 5,
						anim: 6
					});
				}
			});
		};
		//初始化检验小组下拉选择项
	    function ItemList(CNameElemID, IdElemID) {
	        var CNameElemID = CNameElemID || null,
	            IdElemID = IdElemID || null;
	        var fields = ['Id','CName','SName','ItemNo','EName'],
				url = ParItemSplit_URL + "&where=lbitem.GroupType=1";
			url += '&fields=LBItem_' + fields.join(',LBItem_');
//	        var height = $('#content').height();
	        if (!CNameElemID) return;
	        tableSelect.render({
	            elem: '#' + CNameElemID,	//定义输入框input对象 必填
	            checkedKey: 'LBItem_Id', //表格的唯一建值，非常重要，影响到选中状态 必填
	            searchKey: 'lbitem.EName,lbitem.ItemNo,lbitem.SName',	//搜索输入框的name值 默认keyword
	            searchPlaceholder: '编码/简称/英文名称',	//搜索输入框的提示文字 默认关键词搜索
	            table: {	//定义表格参数，与LAYUI的TABLE模块一致，只是无需再定义表格elem
	                url: url,
//	                height: height,
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
	                    { field: 'LBItem_SName', width: 100, title: '简称', sort: false },
	                    { field: 'LBItem_EName', width: 100, title: '英文名称', sort: false },
	                	{ field: 'LBItem_ItemNo', width: 100, title: '编码', sort: false }
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

						onSelectParitem(record["LBItem_Id"]);
				
	                }else{
	                	$(elem).val("");
	                    if (IdElemID) $("#" + IdElemID).val("");
	                    if(!$("#" + IdElemID).val()){
							layer.msg("请选择组合项目");
							return;
						}
	                }
	            }
	        });
	    }
		//初始化
		function initAll() {
			ItemList('ItemCName','ItemID');
			initParams();
			initTable();
			initParItem();
			onFormEvent();
		};
		initAll();
	});
