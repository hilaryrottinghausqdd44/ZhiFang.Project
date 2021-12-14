var fireEventSave;
window.fireEventSaveSuccessFun = function (callback) {
	fireEventSave = callback;
};

layui.extend({
	uxutil:'ux/util',
	PreOrderApplyForm:'modules/pre/order/apply/form',
	PreOrderApplyItems:'modules/pre/order/apply/items',
	PreOrderModelItemTree:'views/pre/order/apply_model/modules/tree',
}).use(['uxutil','element','PreOrderApplyForm','PreOrderApplyItems','PreOrderModelItemTree'],function(){
	var $ = layui.$,
		element = layui.element,
		PreOrderApplyForm = layui.PreOrderApplyForm,
		PreOrderApplyItems = layui.PreOrderApplyItems,
		PreOrderModelItemTree = layui.PreOrderModelItemTree,
		uxutil = layui.uxutil;
	
	//新增服务
	var ADD_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/AddOrder";
	//外部参数
	var PARAMS = uxutil.params.get(true);
	//医嘱单信息实例
	var PreOrderApplyFormInstance = null;
	//医嘱项目列表实例
	var PreOrderApplyItemsInstance = null;
	//检验项目树实例
	var PreOrderModelItemTreeInstance = null;
	//显示的模板类型
	var OrderModelTypeArr = [1, 2, 3, 4];
	//模板类型Map
	var OrderModelTypeMap = {
		1: { text: "个人", icon: "layui-icon-username" },
		2: { text: "科室", icon: "layui-icon-user" },
		3: { text: "疾病", icon: "layui-icon-util" },
		4: { text: "全院", icon: "layui-icon-template-1" }
	};

	//初始化页面
	function initHtml(){
		//实例化医嘱单信息
		PreOrderApplyFormInstance = PreOrderApplyForm.render({
			domId:'OrderForm',
			height:null
		});
		//实例化医嘱项目列表
		PreOrderApplyItemsInstance = PreOrderApplyItems.render({
			domId: 'OrderItems',
			type:'add'
		});
		//实例化检验项目树 -- 医嘱模板树
		$.each(OrderModelTypeArr, function (i,item) {
			PreOrderModelItemTreeInstance = PreOrderModelItemTree.render({
				domId: 'TestItemTree' + item,
				OrderModelTypeID: item,
				where: null,
				listeners: {
					click: function (obj) { onTreeCheck(obj); }
				}
			});
		});
	};
	//检验项目树点击事件处理
	function onTreeCheck(obj){
		//console.log("【检验项目树点击事件处理】",obj.data); //得到当前点击的节点数据
		var list = PreOrderModelItemTreeInstance.getAllLeafByNode(obj.data);

		for(var i in list){
			var data = list[i],
				SampleTypeID = (data.info.LBSampleType && data.info.LBSampleType.length == 1) ? data.info.LBSampleType[0].SampleTypeID : "",
				HisSampleTypeName = (data.info.LBSampleType && data.info.LBSampleType.length == 1) ? data.info.LBSampleType[0].CName : "";
			PreOrderApplyItemsInstance.insertItem({
				OrdersItemID:data.id,
				HisItemName:data.title,
				HisItemNo:data.info.LBItem.ItemNo,
				LBSampleType: data.info.LBSampleType,
				SampleTypeID: SampleTypeID,
				HisSampleTypeName: HisSampleTypeName,
				Charge:data.info.LBItem.ItemCharge
			});
		}
	};
	
	//保存按钮处理
	$("#save_button").on("click",function(){
		var info = PreOrderApplyFormInstance.getInfo();
		var items = PreOrderApplyItemsInstance.getList();
		var itemName = [], msg = [];
		for(var i in items){
			itemName.push(items[i].HisItemName);
			if (!items[i].SampleTypeID) msg.push(items[i].HisItemName+"--未选择样本类型！");
		}
		var params = {
			LisPatient:$.extend({},info.LisPatient,{}),
			LisOrderForm:$.extend({},info.LisOrderForm,{
				ParItemCName:itemName.join(','),//项目名称
				Charge:PreOrderApplyItemsInstance.getTotalPrice()//费用金额
			}),
			LisOrderItems:items
		};
//		if(!params.LisOrderForm.OrderFormNo) msg.push("医嘱单号不能为空！");
		if (!params.LisPatient.CName) msg.push("姓名不能为空！");
		//if (!params.LisPatient.Birthday) msg.push("出生日期不能为空！");
		if (params.LisOrderItems.length <= 0) msg.push("医嘱项目不能为空！");

		if (msg.length > 0) {
			layer.msg(msg.join("<br >"), { icon: 0, anim: 0,time: 3000 });
			return;
		}

		//医嘱执行时间
		if(params.LisOrderForm.OrderExecTime){
			params.LisOrderForm.OrderExecTime = uxutil.date.toServerDate(params.LisOrderForm.OrderExecTime);
		}
		params.LisPatient.Birthday = uxutil.date.toServerDate(params.LisPatient.Birthday);
		
		var loadIndex = layer.load();//开启加载层
		uxutil.server.ajax({
			url:ADD_URL,
			type:'post',
			data:JSON.stringify(params)
		},function(result){
			layer.close(loadIndex);//关闭加载层
			if(result.success){
				layer.msg("保存成功！");
				fireEventSave();
			}else{
				layer.msg(result.msg,{icon:5});
			}
		},true);
	});
	//树列表查询按钮处理
	$("#TabContent").on("click", 'button', function () {
		var OrderModelType = $(this).attr("data-type"),
			CName = $(this).parent().prev().find('input').val();
		var where = "";
		if (CName) {
			where = "(CName like '%" + CName + "%' or EName like '%" + CName + "%' or SName like '%" + CName + "%' or StandCode like '%" + CName +"%' or PinYinZiTou like '%"+ CName + "%')";
		}
		PreOrderModelItemTreeInstance = PreOrderModelItemTree.render({
			domId: 'TestItemTree' + OrderModelType,
			OrderModelTypeID: OrderModelType,
			where: where,
			listeners: {
				click: function (obj) { onTreeCheck(obj); }
			}
		});
	});
	//初始化项目页签
	function initItemTab() {
		var TabTitleHtml = [],
			TabContentHtml = [];

		$.each(OrderModelTypeArr, function (i, item) {
			TabTitleHtml.push('<li lay-id="' + item + '"' + (TabTitleHtml.length > 0 ? '' : ' class="layui-this"') + '><i class="layui-icon ' + OrderModelTypeMap[item]["icon"] + '"></i> ' + OrderModelTypeMap[item]["text"] + '</li>');
			TabContentHtml.push('<div class="layui-tab-item' + (TabContentHtml.length > 0 ? '' : ' layui-show') + '"><div class="layui-inline" style="margin-left:10px;"><input type="text" name="OrderModelCName" placeholder="医嘱模板名称检索" class="layui-input" autocomplete="off" /></div><div class="layui-inline"><button type="button" data-type="' + item + '" class="layui-btn layui-btn-xs"><i class="layui-icon layui-icon-search"></i>查询</button></div><div id="TestItemTree' + item + '"></div></div >');
		});
		$("#TabTitle").html(TabTitleHtml.join(""));
		$("#TabContent").html(TabContentHtml.join(""));
		element.init(); //更新全部
	};
	//初始化
	function init() {
		//初始化项目页签
		initItemTab();
		//初始化页面
		initHtml();
	};
	
	//初始化
	init();
});