/**
 * @name：modules/pre/order/apply/items 医嘱项目列表
 * @author：Jcall
 * @version 2020-06-28
 */
layui.extend({
	uxutil:'ux/util',
	uxtable:'ux/table'
}).define(['uxutil','form','uxtable','element'],function(exports){
	"use strict";
	
	var $ = layui.$,
		element = layui.element,
		form = layui.form,
		uxtable = layui.uxtable,
		uxutil = layui.uxutil,
		MOD_NAME = 'PreOrderApplyItems';
	
	//获取医嘱项目列表服务地址
	var GET_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisOrderItemByHQL";
	//获得项目相关样本类型
	var GET_SAMPLETYPE_LIST_BY_ITEMID_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSampleItemByHQL?isPlanish=true';
	
	//内部列表dom
	var TABLE_DOM = [
		'<span class="layui-breadcrumb" lay-separator=":">',
			'<a href="">总价</a>',
			'<a><cite id="{tableId}_toolbar_totalprice">0</cite> 元</a>',
		'</span>',
		'<table class="layui-hide" id="{tableId}" lay-filter="{tableId}"></table>',
		'<script type="text/html" id="{barId}">',
			'<a class="layui-btn layui-btn-danger layui-btn-xs" lay-event="del">删除</a>',
		'</script>'
	];
	
	//医嘱单列表
	var PreOrderApplyItems = {
		tableId:null,//列表ID
		barId:null,//操作列ID
		//对外参数
		config:{
			domId:null,
			height:null,
			type:'show',//add:新增,edit:修改,show:查看
			orderId:null//医嘱单ID
		},
		//内部列表参数
		tableConfig:{
			elem:null,
			toolbar:null,
			//skin:'line',//行边框风格
			//even:true,//开启隔行背景
			size:'sm',//小尺寸的表格
			defaultToolbar:null,
			//height:'full-4',
			//defaultLoad:true,
			//url:GET_LIST_URL,
			page: false,
			limit:9999,
			data:[],
			where:{},
			initSort: {
				field:'ItemNo',//排序字段
				type:'asc'
			},
			cols:[[
				{field:'OrdersItemID',title:'主键',width:190,hide:true},
				{field:'HisItemNo',width:80,title:'项目编号'},
				{field:'HisItemName',minWidth:80,title:'项目名称'},
				{field:'SampleTypeID',minWidth:80,title:'选中的样本类型ID',hide:true},
				{field:'HisSampleTypeName',minWidth:80,title:'选中的样本类型名称',hide:true},
				{field: 'LBSampleType', width: 200, title: '样本类型', templet: function (data) {return data["HisSampleTypeName"];}},
				{field:'Charge',width:80,title:'价格',align:'right',templet:function(d){
					return parseFloat(d.Charge).toFixed(2);
				}}
			]]
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,PreOrderApplyItems.config,setings);
		me.tableConfig = $.extend({},me.tableConfig,PreOrderApplyItems.tableConfig);
		
		if(me.config.height){
			me.tableConfig.height = me.config.height;
		}
		
		me.tableId = me.config.domId + "-table";
		me.barId = me.config.domId + "-table-bar";
		me.tableConfig.elem = "#" + me.tableId;
		
		if (me.config.type != 'show') {
			$.each(me.tableConfig.cols[0], function (a,itemA) {
				if (itemA["field"] == "LBSampleType") {//show：不显示下拉框，add和edit显示下拉框允许操作
					me.tableConfig.cols[0][a].templet = function (data) {
						var list = data["LBSampleType"] || [],
							str = ['<option value="">请选择</option>'];
						$.each(list, function (i, item) {
							str.push('<option value="' + item["SampleTypeID"] + '" ' + (item["SampleTypeID"] == data["SampleTypeID"] ? "selected" : "") + '>' + item["CName"] + '</option>');
						});
						return '<select class="allowedOverflow" lay-filter="allowedOverflow" id="LBSampleType_' + data.OrdersItemID + '" name="LBSampleType_' + data.OrdersItemID + '">' + str + '</select>';
					}
				}
			});

			me.tableConfig.cols[0].push({
				fixed:'right',title:'操作',toolbar:"#" + me.barId,width:65,align:'center'
			});
		}
		
		me.totalPrice = 0;//总价
		
		var cols = me.tableConfig.cols[0],
			fields = [];
			
		for(var i in cols){
			fields.push('LisOrderItem_' + cols[i].field);
		}
		//查询条件
		me.tableConfig.where = {
			fields:fields.join(','),
			where:'lisorderitem.OrderFormID=' + me.config.orderId
		};
		//数据渲染完的回调
		me.tableConfig.done = function (res, curr, count) {
			form.render('select');
			//解决列表中下拉框遮挡问题
			$("#" + me.tableId + "+div").find(".allowedOverflow").parent('div.layui-table-cell').css('overflow', 'visible');
			$("#" + me.tableId + "+div").find(".layui-table-box").css('overflow', 'visible');
			$("#" + me.tableId + "+div").find(".layui-table-body").css('overflow', 'visible');
			//解决列表中下拉框大小位置
			$("#" + me.tableId + "+div").find(".allowedOverflow+div.layui-form-select").css('margin', '-5px -5px 0');//小表格存在下拉框时设置
		};
	};
	//初始化HTML
	Class.prototype.initHtml = function(){
		var me = this;
		var html = TABLE_DOM.join("").replace(/{tableId}/g,me.tableId).replace(/{barId}/g,me.barId);
		$('#' + me.config.domId).append(html);
		
		element.render('breadcrumb');
	};
	//监听事件
	Class.prototype.initListeners = function(){
		var me = this;
		me.uxtable.table.on('tool(' + me.tableId + ')',function(obj){
			if(obj.event === 'del'){//删除
				layer.confirm('确定删除吗？',function(index){
					//删除对应行（tr）的DOM结构，并更新缓存
					obj.del();
					//数据变化处理
					me.onDataChange();
					layer.close(index);
				});
			}
		});
		//下拉框修改
		form.on('select(allowedOverflow)', function (data) {
			var elem = data.elem,
				value = data.value,
				text = value ? $(elem).find('option[value=' + value + ']').html() : '',
				trIndex = $(elem).parents('tr').attr("data-index"),
				list = me.uxtable.table.cache[me.tableId];

			list[trIndex]["SampleTypeID"] = value;
			list[trIndex]["HisSampleTypeName"] = text;
		});
	};
	//查询处理
	Class.prototype.onSearch = function(){
		var me = this;
		var loadIndex = layer.load();//开启加载层
		uxutil.server.ajax({
			url:GET_LIST_URL,
			data:me.tableConfig.where
		},function(data){
			if (data.success) {
				me.getSampleTypeByItemIDList(data.value.list, function (list) {
					layer.close(loadIndex);//关闭加载层
					me.uxtable.instance.reload({
						data: list || []
					});
					//数据变化处理
					me.onDataChange();
				});
			} else {
				layer.close(loadIndex);//关闭加载层
				layer.msg(data.msg);
			}
		});
		
//		me.uxtable.reload({
//			where:me.tableConfig.where
//		});
	};
	//获得相关项目的所有样本类型  -- 可优化为定制服务返回  直接在onsearch方法返回数据加LBSampleType:[{SampleTypeID:1,CName:123},{SampleTypeID:1,CName:123}]
	Class.prototype.getSampleTypeByItemIDList = function (arr,callback) {
		var me = this,
			arr = arr || [],
			Length = arr.length,
			SaveSuccessCount = 0,
			SaveErrorCount = 0;
		if (me.config.type != "edit") {
			callback(arr);
			return;
		}
		$.each(arr, function (i, item) {
			var itemid = item["OrdersItemID"];
			if (!itemid) return true;
			setTimeout(function () {
				var url = GET_SAMPLETYPE_LIST_BY_ITEMID_URL + "&fields=LBSampleItem_LBItem_Id,LBSampleItem_LBItem_CName,LBSampleItem_LBSampleType_Id,LBSampleItem_LBSampleType_CName";
				url += "&where=ItemID=" + itemid;
				uxutil.server.ajax({
					url: url
				}, function (res) {
					if (res.success) {
						var list = (res.value && res.value.list) || [],
							LBSampleType = [];
						$.each(list, function (j, itemJ) {
							LBSampleType.push({ "SampleTypeID": itemJ["LBSampleItem_LBSampleType_Id"], "CName": itemJ["LBSampleItem_LBSampleType_CName"] });
						});
						item["LBSampleType"] = LBSampleType;
						SaveSuccessCount++;
					} else {
						item["LBSampleType"] = [];
						SaveErrorCount++;
					}
					if (SaveSuccessCount + SaveErrorCount == Length) {
						callback(arr);
					}
				});
			}, 100 * i);
		});
	};
	//增加检验项目
	Class.prototype.insertItem = function(record){
		var me = this,
			list = me.uxtable.table.cache[me.tableId],
			hasRecode = false;
			
		for(var i in list){
			if(list[i].OrdersItemID == record.OrdersItemID){
				hasRecode = true;
				break;
			}
		}
		if(!hasRecode){
			list.push(record);
			me.uxtable.instance.reload({
				data:list
			});
			//数据变化处理
			me.onDataChange();
		}
	};
	//数据变化处理
	Class.prototype.onDataChange = function(){
		var me = this,
			list = me.uxtable.table.cache[me.tableId],
			totalPrice = 0;
			
		for(var i in list){
			if(list[i].Charge){
				totalPrice += parseFloat(list[i].Charge);
			}
		}
		me.totalPrice = totalPrice;
		$("#" + me.tableId + '_toolbar_totalprice').text(totalPrice);
	};
	
	//获取列表数据
	Class.prototype.getList = function(){
		var me = this,
			tableData = me.uxtable.table.cache[me.tableId],
			list= [];
			
		for(var i in tableData){
			if(tableData[i].OrdersItemID){
				list.push(tableData[i]);
			}
		}
			
		return list;
	};
	//获取项目总价
	Class.prototype.getTotalPrice = function(){
		return this.totalPrice;
	};
	//数据更改
	Class.prototype.changeData = function(orderId){
		var me = this;
		me.tableConfig.where.where = 'lisorderitem.OrderFormID=' + orderId;
		me.onSearch();
	};
	
	//核心入口
	PreOrderApplyItems.render = function(options){
		var me = new Class(options);
		
		if(!me.config.domId){
			window.console && console.error && console.error(MOD_NAME + "模块组件错误：参数config.domId缺失！");
			return me;
		}
		//初始化HTML
		me.initHtml();
		//实例化列表
		me.uxtable = uxtable.render(me.tableConfig);
		//监听事件
		me.initListeners();
		//默认加载数据
		if(me.config.orderId){
			me.onSearch();
		}
		
		return me;
	};
	
	//暴露接口
	exports(MOD_NAME,PreOrderApplyItems);
});