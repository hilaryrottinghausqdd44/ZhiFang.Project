$(function () {
	//获取供应商地址
	var SEARCH_COMP_URL = JcallShell.System.Path.ROOT + "/ReaSysManageService.svc/ST_UDTO_SearchReaCenOrgByHQL";
	var LOCAL_DATA = {
		LastCompId:null,//最后一次选择的供应商ID
		LastCompName:null,//最后一次选择的供应商名称
		CompId:null,//当前选中的供应商ID
		CompName:null,//当前选中的供应商名称
		CompCenOrgNo:null,//当前选中的供应商平台编码
		CompList:[],
		DocInfo:JcallShell.LocalStorage.UserOrder.getDocInfo(),//主单信息
		DtlList:JcallShell.LocalStorage.UserOrder.getGoodsList()//明细列表
	};
	//本机构编号
	var ORG_NO = (JcallShell.LocalStorage.User.getAccount(true) || {}).OrgNo;
	//获取供应商列表
	function getCompListData(callback){
		var fields = [
			'ReaCenOrg_OrgNo','ReaCenOrg_CName','ReaCenOrg_Id','ReaCenOrg_PlatformOrgNo'
		];
		var where = 'reacenorg.POrgID=0 and reacenorg.OrgType=0';
		var url = SEARCH_COMP_URL + '?isPlanish=true&OrgType=0&fields=' + fields.join(',') + '&where=' + where;
		
		JcallShell.Server.ajax({
			showError:true,
			url:url
		},function(data){
			if(data.success){
				var list = ((data || {}).value || {}).list || [];
				for(var i in list){
					list[i].Id = list[i].ReaCenOrg_Id;
				}
				LOCAL_DATA.CompList = list;
				callback();
			}else{
				JcallShell.Msg.error(data.msg);
			}
		});
	}
	//初始化订单明细列表
	function initGoodsListHtml(){
		var list = LOCAL_DATA.DtlList,
			len = list.length,
			html = [];
			
		for(var i=0;i<len;i++){
			var row = createRow(list[i],true);
			html.push(row);
		}
		
		$("#list").html(html.join(''));
		initRowListener();
	}
	//创建订单明细数据行内容
	function createRow(value,checked){
		var html = [];
		
		value = value || {};
		var id = value.ReaGoodsOrgLink_ReaGoods_Id;
		var title = value.ReaGoodsOrgLink_ReaGoods_CName;
		
		var row0 = '<span style="color:#d9534f;font-weight:bold;">规格：( ' + value.ReaGoodsOrgLink_ReaGoods_UnitMemo + ' )';
		var row1 = '<span style="color:#666666;">品牌：' + value.ReaGoodsOrgLink_ReaGoods_ProdOrgName + '</span>';
		var row2 = value.ReaGoodsOrgLink_ReaGoods_GoodsClass + ' - ' + value.ReaGoodsOrgLink_ReaGoods_GoodsClassType;
		var row3 = "单价：" + value.ReaGoodsOrgLink_Price + "元";
		
		html.push('<div class="list-group-item" style="padding:0;float:left;width:100%;" data="' + id + '">');
		
		html.push('<div class="list-group-item-content">');
		html.push('<div class="list-group-item-heading">' + title + '</div>');
		html.push('<div class="list-group-item-text">' + row0 + '</div>');
		html.push('<div class="list-group-item-text touch">' + row1 + '</div>');
		html.push('<div class="list-group-item-text">' + row2 + '</div>');
		html.push('<div class="list-group-item-text">' + row3 + '</div>');
		
		if(checked){
			html.push('<div style="float:right;margin-top:-65px;">');
			
			//产品数量
			html.push('<div class="number-spinner">');
			html.push('<button class="number-spinner-decrease" data="-1">-</button>');
			html.push('<input type="number" maxlength="4" onkeyup="this.value=this.value.replace(/\\D/gi,\'\')" value="');
			html.push(value.GoodsQty || 1);
			html.push('">');
			html.push('<button class="number-spinner-increase" data="1">+</button>');
			html.push('</div>');//end number-spinner
			
			html.push('</div>');
			
			html.push('<div style="float:right;margin-top:-25px;padding:2px;text-align:center;">');
			html.push('总价：<span style="color:#d9534f;font-weight:bold;">' + parseFloat(value.TotalPrice || value.ReaGoodsOrgLink_Price).toFixed(2) + '</span>元');
			html.push('</div>');
			
			html.push('</div>');//end list-group-item-content
			
			html.push('<div class="remove-button-div" style="margin-top:-85px;"><span>删除</span></div>');
		}else{
			html.push('</div>');
		}
		
		html.push('</div>');
		
		return html.join('');
	}
	
	//已选列表监听
	function initRowListener(){
		var checkedRow = null;
		//左右滑动监听
		$(".list-group-item-content").on('touchstart',function(e) {
			//e.preventDefault();
			if(checkedRow && checkedRow != $(this)){
				checkedRow.css("WebkitTransform","translateX(0px)");
			}
			checkedRow = $(this);
		});
		$(".list-group-item-content").on('touchmove',function(e) {
			//e.preventDefault();
			var x = JcallShell.Event.touch.move_x();
			if (x > 0) {
				$(this).css("WebkitTransform","translateX(0px)");
			}else{
				$(this).css("WebkitTransform","translateX(" + x + "px)");
			}
		});
		$(".list-group-item-content").on('touchend',function(e) {
			//e.preventDefault();
			var x = JcallShell.Event.touch.move_x();
			if (x >= -40) {
				$(this).css("WebkitTransform","translateX(0px)");
			}else if(x < -40){
				$(this).css("WebkitTransform","translateX(-80px)");
			}
		});
		
		//数量加减监听
		$(".number-spinner button").on(JcallShell.Event.click,function(e){
			e.preventDefault();
			if(!JcallShell.Event.isClick()) return;
			var row = $(this).parent().parent().parent().parent(),
				id = row.attr("data"),
				numberDiv = $(this).parent(),
				input = $(numberDiv).children("input"),
				data = parseInt($(this).attr("data")),
				old = parseInt(input.val() || "0"),
				now = old + data;
				
			now = now < 1 ? 1 : now;
			changeGoodsRecord(id,'GoodsQty',now);
			input.val(now);
			//更改总价
			changeDtlTotalPrice(input,id);
		});
		//数量变化监听
		$(".number-spinner input").on("change",function(e){
			e.preventDefault();
			if(!JcallShell.Event.isClick()) return;
			var row = $(this).parent().parent().parent().parent(),
				id = row.attr("data"),
				now = parseInt($(this).val() || "0");
				
			now = now < 1 ? 1 : now;
			changeGoodsRecord(id,'GoodsQty',now);
			$(this).val(now);
			//更改总价
			changeDtlTotalPrice($(this),id);
		});
		
		$(".remove-button-div").on(JcallShell.Event.click,function(e){
			e.preventDefault();
			if(!JcallShell.Event.isClick()) return;
			var li = $(this).parent();
			var id = li.attr("data");
			JcallShell.LocalStorage.UserOrder.removeGoods(id);
			//更改所有产品总价
			changeDocTotalPrice();
			
			var arr = li.parent().children();
			if(arr.length == 1){
				li.parent().html('<div class="no-data-div">没有选中的产品!</div>');
				$("#submit-div").hide();
			}
			li.remove();
		});
		
		//更改所有产品总价
		changeDocTotalPrice();
	}
	//更改单个产品总价
	function changeDtlTotalPrice(dom,id){
		var data = getGoodsRecord(id);
		if(data){
			var span = dom.parent().parent().next().children("span");
			var TotalPrice = (data.TotalPrice || 0).toFixed(2);
			span.text(TotalPrice);
			changeDocTotalPrice();
		}
	}
	//更改所有产品总价
	function changeDocTotalPrice(){
		var list = LOCAL_DATA.DtlList || [],
			len = list.length,
			checkedCount = 0,
			TotalPrice = 0,
			checkedNum = 0;
		
		for(var i=0;i<len;i++){
			TotalPrice += parseFloat(list[i].TotalPrice || list[i].ReaGoodsOrgLink_Price || 0);
		}
		TotalPrice = TotalPrice.toFixed(2);
		$("#totalprice").text(TotalPrice);
	}
	//更改产品信息
	function changeGoodsRecord(id,field,value){
		var record = getGoodsRecord(id);
		if(record){
			record[field] = value;
			if(field = "GoodsQty"){
				record.TotalPrice = parseInt(value) * parseFloat(record.ReaGoodsOrgLink_Price || 0);
			}
			JcallShell.LocalStorage.UserOrder.editGoods(id,record);
			LOCAL_DATA.DtlList = JcallShell.LocalStorage.UserOrder.getGoodsList();
		}
	}
	//获取产品信息
	function getGoodsRecord(id){
		var len = LOCAL_DATA.DtlList.length,
			record = null;
			
		for(var i=0;i<len;i++){
			if(LOCAL_DATA.DtlList[i].Id == id){
				record = LOCAL_DATA.DtlList[i];
				break;
			}
		}
		return record;
	}
	
	//初始化页面内容
	function initPageContent(){
		$('#UrgentFlag').bootstrapSwitch({
			state:LOCAL_DATA.DocInfo.UrgentFlag === false ? false : true,
			onSwitchChange:function(){
				onDocInfoChange();//订单主单信息变更
			}
		});
		
		if(LOCAL_DATA.DocInfo.LabMemo){
			$("#LabMemo").val(LOCAL_DATA.DocInfo.LabMemo);
		}
		
		$("#add-button").on(JcallShell.Event.click,function(){
			if(LOCAL_DATA.CompId){
				location.href="checkgoods.html?compId=" + LOCAL_DATA.CompId;
			}else{
				JcallShell.Msg.error("请先选择一个供应商！");
			}
		});
		
		$("#LabMemo").on("change",function(){
			onDocInfoChange();//订单主单信息变更
		});
		
		var list = LOCAL_DATA.CompList,
			len = list.length,
			CompItems = [];
			
		for(var i=0;i<len;i++){
			CompItems.push({
				title:list[i].ReaCenOrg_CName,
				value:list[i].ReaCenOrg_Id
			});
			if(list[i].Id == LOCAL_DATA.DocInfo.CompId){
				CompItems[i].selected = true;
			}
		}
		
		$("#Comp").select({
			title: "供应商选择",
			items: CompItems,
			onChange: function(data) {
				var Id = data.values;
				onChangeComp(Id);
			}
		});
		
		LOCAL_DATA.CompId = LOCAL_DATA.DocInfo.CompId;
		LOCAL_DATA.CompName = LOCAL_DATA.DocInfo.CompName;
		LOCAL_DATA.LastCompId = LOCAL_DATA.DocInfo.CompId;
		LOCAL_DATA.LastCompName = LOCAL_DATA.DocInfo.CompName;
		$("#Comp").attr("value",LOCAL_DATA.DocInfo.CompName);
		$("#Comp").attr("data-values",LOCAL_DATA.DocInfo.CompId);
		
		$("#submit-button").on(JcallShell.Event.click,function(){
			onSaveOrder();
		});
	}
	//供应商变更处理
	function onChangeComp(Id){
		var list = LOCAL_DATA.CompList,
			len = list.length,
			Name = '',
			Code = '';
			
		for(var i=0;i<len;i++){
			if(list[i].ReaCenOrg_Id == Id){
				Name = list[i].ReaCenOrg_CName;
				Code = list[i].ReaCenOrg_PlatformOrgNo;
				break;
			}
		}
		if(!LOCAL_DATA.CompId){
			LOCAL_DATA.LastCompId = Id;
			LOCAL_DATA.LastCompName = Name;
			JcallShell.LocalStorage.UserOrder.removeAll();
			LOCAL_DATA.CompId = Id;
			LOCAL_DATA.CompName = Name;
			LOCAL_DATA.CompCenOrgNo = Code;
			onDocInfoChange();//订单主单信息变更
		}else{
			if(LOCAL_DATA.CompId != Id){
				$.confirm("供应商变更后已选的所有货品清空，您确定操作吗?", "确认变更?", function() {
					LOCAL_DATA.LastCompId = Id;
					LOCAL_DATA.LastCompName = Name;
					JcallShell.LocalStorage.UserOrder.removeAll();
					LOCAL_DATA.CompId = Id;
					LOCAL_DATA.CompName = Name;
					LOCAL_DATA.CompCenOrgNo = Code;
					LOCAL_DATA.DtlList = [];
					initGoodsListHtml();
					onDocInfoChange();
				}, function() {
					//取消操作
					$("#Comp").attr("value",LOCAL_DATA.LastCompName);
					$("#Comp").attr("data-values",LOCAL_DATA.LastCompId);
				});
			}
		}
	}
	//订单主单信息变更
	function onDocInfoChange(){
		var DocInfo = {
			CompId:LOCAL_DATA.CompId,
			CompName:LOCAL_DATA.CompName,
			CompCenOrgNo:LOCAL_DATA.CompCenOrgNo,
			UrgentFlag:$("#UrgentFlag").bootstrapSwitch('state'),
			LabMemo:$("#LabMemo").val()
		};
		JcallShell.LocalStorage.UserOrder.addDocInfo(DocInfo);
	}
	//保存订单信息
	function onSaveOrder(){
		var data = {
			info:JcallShell.LocalStorage.UserOrder.getDocInfo(),
			DtlList:JcallShell.LocalStorage.UserOrder.getGoodsList()
		};
		console.log(JcallShell.JSON.encode(data));
		//----------------------------------------
		var Info = JcallShell.LocalStorage.UserOrder.getDocInfo();
		//申请状态值：1
		var entity = {
			LabID:'',//CenOrg_LabID
			LabcName:'',//本机构名称，CenOrg_CName
			ReaServerLabcCode:'',//本机构编码，CenOrg_OrgNo
			CompID:Info.CompId,//供应商ID,ReaCenOrg
			CompanyName:Info.CompName,//供应商名称
			ReaServerCompCode:Info.CompCenOrgNo,//供应商平台编码
			Status:1,
			UrgentFlag:Info.UrgentFlag,//紧急程度
			Memo:Info.LabMemo//备注
		};
		entity.Memo = entity.Memo.replace(/\\/g, '&#92').replace(/[\r\n]/g, '<br />');
		
		//dtAddList = [{Id:,ReaGoodsID:,CompGoodsLinkID:,GoodsQty:}];
		//dtEditList = [{Id:,GoodsQty:}];
		var goods = JcallShell.LocalStorage.UserOrder.getGoodsList(),
			len = goods.length,
			dtAddList = [];
		for(var i=0;i<len;i++){
			dtAddList.push({
				Id:'-1',
				ReaGoodsID:goods[i].ReaGoodsOrgLink_ReaGoods_Id,
				CompGoodsLinkID:goods[i].ReaGoodsOrgLink_Id,
				GoodsQty:goods[i].GoodsQty
			});
		}
		//后台数据对象
		var data = {
			entity:entity,
			dtAddList:dtAddList,
			otype:2//1:PC端;2:移动端
		};
		alert(JcallShell.JSON.encode(data));
	}
	//获取供应商列表
	getCompListData(function(){
		//初始化页面内容
		initPageContent();
		//初始化订单明细列表
		initGoodsListHtml();
	});
});