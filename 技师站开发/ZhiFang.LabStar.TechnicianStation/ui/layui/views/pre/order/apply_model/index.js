layui.extend({
	uxutil:'ux/util',
	uxtoolbar:'ux/toolbar',
	PreOrderApplyList:'modules/pre/order/apply/list',
	PreOrderApplyForm:'modules/pre/order/apply/form',
	PreOrderApplyItems:'modules/pre/order/apply/items'
}).use(['uxutil','uxtoolbar','PreOrderApplyList','PreOrderApplyItems','PreOrderApplyForm'],function(){
	var $ = layui.$,
		PreOrderApplyList = layui.PreOrderApplyList,
		PreOrderApplyForm = layui.PreOrderApplyForm,
		PreOrderApplyItems = layui.PreOrderApplyItems,
		uxutil = layui.uxutil,
		uxtoolbar = layui.uxtoolbar;
	
	//外部参数
	var PARAMS = uxutil.params.get(true);
	//HIS科室编码
	var HISDEPTNO = PARAMS.HISDEPTNO;
	//病历号
	var PATNO = PARAMS.PATNO;
	//就诊类型
	var SICKTYPENO = PARAMS.SICKTYPENO;
	//医嘱单列表实例
	var PreOrderApplyListInstance = null;
	//医嘱单信息实例
	var PreOrderApplyFormInstance = null;
	//医嘱项目列表实例
	var PreOrderApplyItemsInstance = null;
	//选中的行
	var checkedTr = null;
	//选择的列表数据
	var checkedData = null;
	
	//初始化页面
	function initHtml(){
		//实例化功能按钮栏
		uxtoolbar.render({
			domId:'toolbar',
			buttons:['refresh','add','edit','del','print','check','uncheck'],
			event:{
				refresh:function(){onSearch();},
				add:function(){onAdd();},
				edit:function(){onEdit();},
				del:function(){onDel();},
				check:function(){onCheck();},
				uncheck:function(){onUnCheck();}
			}
		});
		//实例化医嘱单列表
		PreOrderApplyListInstance = PreOrderApplyList.render({
			domId:'OrderTable',
			height:'full-125',
			
			hisDeptNo:HISDEPTNO,//HIS科室编码
			patno:PATNO,//病历号
			sickTypeNo:SICKTYPENO//就诊类型
		});
		//实例化医嘱单信息
		PreOrderApplyFormInstance = PreOrderApplyForm.render({
			domId:'OrderForm',
			type:'show'
		});
		//实例化医嘱项目列表
		PreOrderApplyItemsInstance = PreOrderApplyItems.render({
			domId:'OrderItems',
			type:'show'
		});
	};
	//初始化监听
	function initListeners(){
		//触发行单击事件
		PreOrderApplyListInstance.uxtable.table.on('row(' + PreOrderApplyListInstance.tableId + ')', function(obj){
			//行选中背景
			if(checkedTr){
				checkedTr.removeClass('layui-table-select');
			}
			checkedTr = obj.tr;
			checkedTr.addClass('layui-table-select');
			
			//选择的列表数据
			checkedData = obj.data;
			//医嘱单内容更新
			onFormChange(checkedData.OrderFormID);
			//医嘱单项目列表更新
			onItemsChange(checkedData.OrderFormID);
		});
	};
	//查询处理
	function onSearch(){
		PreOrderApplyListInstance.onSearch();
	};
	//新增处理
	function onAdd(){
		layer.open({
			title:'医嘱单-新增',
			type:2,
			content:'add.html?t=' + new Date().getTime(),
			maxmin:true,
			toolbar:true,
			resize:true,
			area:['95%','95%'],
			success: function (layero, index) {
				var iframe = $(layero).find("iframe")[0].contentWindow;
				iframe.fireEventSaveSuccessFun(function () {
					layer.close(index);
					onSearch();
				})
			},
		});
		
	};
	//修改处理
	function onEdit(){
		if(!checkedData){
			layer.msg("请先选择一行再进行操作！");
			return;
		}
		layer.open({
			title:'医嘱单-修改',
			type:2,
			content:'edit.html?id=' + checkedData.OrderFormID + '&t=' + new Date().getTime(),
			maxmin:true,
			toolbar:true,
			resize:true,
			area:['95%','95%'],
			success: function (layero, index) {
				var iframe = $(layero).find("iframe")[0].contentWindow;
				iframe.fireEventSaveSuccessFun(function () {
					layer.close(index);
					onSearch();
				})
			},
		});
	};
	//删除处理
	function onDel(){
		PreOrderApplyListInstance.onDel();
	};
	//审核
	function onCheck(){
		PreOrderApplyListInstance.onCheck();
	};
	//取消审核
	function onUnCheck(){
		PreOrderApplyListInstance.onUnCheck();
	};
	//医嘱单内容更新
	function onFormChange(orderId){
		PreOrderApplyFormInstance.changeData(orderId);
	};
	//医嘱单项目列表更新
	function onItemsChange(orderId){
		PreOrderApplyItemsInstance.changeData(orderId);
	};
	
	//初始化
	function init(){
		//初始化页面
		initHtml();
		//初始化监听
		initListeners();
	};
	
	//初始化
	init();
});