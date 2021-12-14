layui.extend({
	uxutil:'ux/util',
	PreOrderApplyForm:'modules/pre/order/apply/form',
	PreOrderApplyItems:'modules/pre/order/apply/items',
	PreOrderApplyItemTree:'modules/pre/order/apply/item/tree',
}).use(['uxutil','element','PreOrderApplyForm','PreOrderApplyItems','PreOrderApplyItemTree'],function(){
	var $ = layui.$,
		element = layui.element,
		PreOrderApplyForm = layui.PreOrderApplyForm,
		PreOrderApplyItems = layui.PreOrderApplyItems,
		PreOrderApplyItemTree = layui.PreOrderApplyItemTree,
		uxutil = layui.uxutil;
	
	//修改服务
	var EDIT_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/EditOrder";
	//外部参数
	var PARAMS = uxutil.params.get(true);
	//医嘱单ID
	var orderId = PARAMS.ID;
	
	//医嘱单信息实例
	var PreOrderApplyFormInstance = null;
	//医嘱项目列表实例
	var PreOrderApplyItemsInstance = null;
	//检验项目树实例
	var PreOrderApplyItemTreeInstance = null;
	
	//初始化页面
	function initHtml(){
		//实例化医嘱单信息
		PreOrderApplyFormInstance = PreOrderApplyForm.render({
			domId:'OrderForm',
			type:'edit',
			orderId:orderId,
			height:null
		});
		//实例化医嘱项目列表
		PreOrderApplyItemsInstance = PreOrderApplyItems.render({
			domId:'OrderItems',
			orderId: orderId,
			type: 'edit'
		});
		//实例化检验项目树
		PreOrderApplyItemTreeInstance = PreOrderApplyItemTree.render({
			domId:'TestItemTree',
			listeners:{
				click:function(obj){onTreeCheck(obj);}
			}
		});
	};
	//检验项目树点击事件处理
	function onTreeCheck(obj){
		//console.log("【检验项目树点击事件处理】",obj.data); //得到当前点击的节点数据
		var list = PreOrderApplyItemTreeInstance.getAllLeafByNode(obj.data);
		for (var i in list) {
			var data = list[i],
				SampleTypeID = (data.info.LBSampleType && data.info.LBSampleType.length == 1) ? data.info.LBSampleType[0].SampleTypeID : "",
				HisSampleTypeName = (data.info.LBSampleType && data.info.LBSampleType.length == 1) ? data.info.LBSampleType[0].CName : "";
			PreOrderApplyItemsInstance.insertItem({
				OrdersItemID:data.id,
				HisItemName:data.title,
				HisItemNo: data.info.LBItem.ItemNo,
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
			if (!items[i].SampleTypeID) msg.push(items[i].HisItemName + "--未选择样本类型！");
		}
		var params = {
			LisPatient:$.extend({},info.LisPatient,{}),
			LisPatientFields:info.LisPatientFields,
			LisOrderForm:$.extend({},info.LisOrderForm,{
				ParItemCName:itemName.join(','),//项目名称
				Charge:PreOrderApplyItemsInstance.getTotalPrice()//费用金额
			}),
			LisOrderFormFields:info.LisOrderFormFields,
			LisOrderItems:items
		};
		//if(!params.LisOrderForm.OrderFormNo) msg.push("医嘱单号不能为空！");
		if (!params.LisPatient.CName) msg.push("姓名不能为空！");
		if (params.LisOrderItems.length <= 0) msg.push("医嘱项目不能为空！");

		if (msg.length > 0) {
			layer.msg(msg.join("<br >"), { icon: 0, anim: 0, time: 3000 });
			return;
		}

		//医嘱执行时间
		if(params.LisOrderForm.OrderExecTime){
			params.LisOrderForm.OrderExecTime = uxutil.date.toServerDate(params.LisOrderForm.OrderExecTime);
		}
		
		var loadIndex = layer.load();//开启加载层
		uxutil.server.ajax({
			url:EDIT_URL,
			type:'post',
			data:JSON.stringify(params)
		},function(result){
			layer.close(loadIndex);//关闭加载层
			if(result.success){
				layer.msg("保存成功！");
				parent.layer.closeAll('iframe');
				parent.onSearch();
			}else{
				layer.msg(result.msg,{icon:5});
			}
		},true);
	});
	
	//初始化
	function init(){
		//初始化页面
		initHtml();
	};
	
	//初始化
	init();
});