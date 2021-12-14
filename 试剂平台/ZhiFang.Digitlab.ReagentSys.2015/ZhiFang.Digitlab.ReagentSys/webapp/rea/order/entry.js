$(function() {
	//=================全局变量-开始=================
	//订单审核服务地址（带推送给供应商）
    var CHECK_BMS_CEN_ORDER_DOC_URL = Shell.util.Path.ROOT + "/ReagentService.svc/RS_UDTO_CheckBmsCenOrderDocByID";
	var LOCAL_DATA = {
		GoodsList:[],//产品列表 (已选属性checked=true;产品数量GoodsQty;总价TotalPrice);明细ID(DtlID)
		ProdList:[],//厂商列表
		GoodsClassList:[],//一级列表
		GoodsClassTypeList:[],//二级列表
		DocInfo:{},//主单信息
		DtlList:[]//明细列表
	};
	//当前选中的类型
	var CHECKED_TYPE_DOM = null;
	//供应商ID
	var COMP_ID = null;
	//供应商名称
	var COMP_NAME = null;
	//默认类型
	var TYPE = null;
	//订单主键
	var DOC_ID = null;
	//机构ID
	var CENORGID = Shell.util.LocalStorage.get(Shell.Rea.LocalStorage.map.CENORGID);
	//确定的状态值
	var IsWriteExternalSystem = Shell.Rea.Order.IsWriteExternalSystem;
	//点击的操作按钮
	var CHECKED_BUTTON = null;
	//=================全局变量-结束=================
	
	//=================数据加载-开始=================
	//加载供货单信息数据
	function loadInfoData(callback){
		var url = Shell.util.Path.ROOT + '/ReagentSysService.svc/ST_UDTO_SearchBmsCenOrderDocById?isPlanish=true';
		url += '&id=' + DOC_ID;
		var fields = [
			'BmsCenOrderDoc_Id','BmsCenOrderDoc_OrderDocNo','BmsCenOrderDoc_Lab_CName','BmsCenOrderDoc_Comp_CName',
			'BmsCenOrderDoc_UrgentFlag','BmsCenOrderDoc_Status','BmsCenOrderDoc_Lab_Id','BmsCenOrderDoc_Comp_Id',
			'BmsCenOrderDoc_ReqDeliveryTime','BmsCenOrderDoc_LabMemo'
		];
		url += '&fields=' + fields.join(',');
			
		ShellComponent.mask.loading();
		Shell.util.Server.ajax({
			url: url
		}, function(data){
			ShellComponent.mask.hide();
			callback(data);
		});
	}
	//加载供货单明细列表数据
	function loadDtlData(callback){
		var url = Shell.util.Path.ROOT + '/ReagentSysService.svc/ST_UDTO_SearchBmsCenOrderDtlByHQL?isPlanish=true';
		var fields = [
			'BmsCenOrderDtl_Id','BmsCenOrderDtl_GoodsName','BmsCenOrderDtl_ProdGoodsNo',
			'BmsCenOrderDtl_GoodsQty','BmsCenOrderDtl_Price','BmsCenOrderDtl_ProdOrgName',
			'BmsCenOrderDtl_GoodsUnit','BmsCenOrderDtl_UnitMemo'
		];
		url += '&fields=' + fields.join(',');
		url += '&where=bmscenorderdtl.BmsCenOrderDoc.Id=' + DOC_ID
			
		ShellComponent.mask.loading();
		Shell.util.Server.ajax({
			url: url
		}, function(data){
			ShellComponent.mask.hide();
			callback(data);
		});
	}
	
	//加载实验室产品列表数据(该供应商)
	function loadGoodsList(callback){
		//RS_UDTO_SearchGoodsByHQL(long compID, int page, int limit, string fields, string where, string sort, bool isPlanish)
		//var url = Shell.util.Path.ROOT + '/ReagentSysService.svc/ST_UDTO_SearchGoodsByHQL?isPlanish=true';
		var url = Shell.util.Path.ROOT + '/ReagentService.svc/RS_UDTO_SearchGoodsByHQL?isPlanish=true';
		var fields = [
			'Goods_Id','Goods_CName','Goods_EName','Goods_Prod_CName',
			'Goods_Prod_Id','Goods_GoodsClass','Goods_GoodsClassType','Goods_Price',
			'Goods_ProdGoodsNo','Goods_UnitName','Goods_UnitMemo','Goods_ShortCode','Goods_GoodsSource'
		];
		url += '&fields=' + fields.join(',');
		//url += '&where=goods.CenOrg.Id=' + CENORGID + ' and goods.Comp.Id=' + COMP_ID;
		url += '&compID=' + COMP_ID;
		
		ShellComponent.mask.loading();
		Shell.util.Server.ajax({
			showError:true,
			url: url
		}, function(data){
			ShellComponent.mask.hide();
			callback(data);
		});
	}
	
	//新增订单
	function addDocData(data,callback){
		var url = Shell.util.Path.ROOT + '/ReagentService.svc/RS_UDTO_AddBmsCenOrderDoc';
		ShellComponent.mask.save();
		Shell.util.Server.ajax({
			type:'post',
			url: url,
			data:data
		}, function(data){
			ShellComponent.mask.hide();
			if(data.success){
				callback(data.value.Id);
			}else{
				ShellComponent.messagebox.msg(data.msg);
			}
		});
	}
	//修改订单
	function updateDocData(data,callback){
		var url = Shell.util.Path.ROOT + '/ReagentService.svc/RS_UDTO_UpdateBmsCenOrderDoc';
		ShellComponent.mask.save();
		Shell.util.Server.ajax({
			type:'post',
			url: url,
			data:data
		}, function(data){
			ShellComponent.mask.hide();
			if(data.success){
				callback();
			}else{
				ShellComponent.messagebox.msg(data.msg);
			}
		});
	}
	
	//初始化参数
	function initParamsData(){
		var params = Shell.util.getRequestParams(true);
		if(params.COMP){
			var Comp = params.COMP.split(',');
			COMP_ID = Comp[0];
			COMP_NAME = Comp[1];
		}
		
		TYPE = params.TYPE || "";
		DOC_ID = params.ID;
	}
	//=================数据加载-结束=================
	
	//=================数据处理-开始=================
	//更改产品信息
	function changeGoodsRecord(id,field,value){
		var record = getGoodsRecord(id);
		if(record){
			record[field] = value;
			if(field = "GoodsQty"){
				record.TotalPrice = parseInt(value) * parseFloat(record.Goods_Price || 0);
			}
		}
	}
	//获取产品信息
	function getGoodsRecord(id){
		var len = LOCAL_DATA.GoodsList.length,
			record = null;
			
		for(var i=0;i<len;i++){
			if(LOCAL_DATA.GoodsList[i].Goods_Id == id){
				record = LOCAL_DATA.GoodsList[i];
				break;
			}
		}
		return record;
	}
	
	//初始化已选的产品
	function initCheckedGoodsData(){
		if(!LOCAL_DATA.DtlList || LOCAL_DATA.DtlList.length == 0){return;}
		
		var len = LOCAL_DATA.DtlList.length;
		var goodsLen = LOCAL_DATA.GoodsList.length;
		for(var i=0;i<len;i++){
			for(var j=0;j<goodsLen;j++){
				if(LOCAL_DATA.DtlList[i].BmsCenOrderDtl_ProdGoodsNo == LOCAL_DATA.GoodsList[j].Goods_ProdGoodsNo){
					LOCAL_DATA.GoodsList[j].GoodsQty = LOCAL_DATA.DtlList[i].BmsCenOrderDtl_GoodsQty;
					LOCAL_DATA.GoodsList[j].DtlID = LOCAL_DATA.DtlList[i].BmsCenOrderDtl_Id;
					LOCAL_DATA.GoodsList[j].checked=true;
					break;
				}
			}
		}
	}
	
	//初始化产品类型数据
	function initTypeData(){
		LOCAL_DATA.ProdList = [];
		LOCAL_DATA.GoodsClassList = [];
		LOCAL_DATA.GoodsClassTypeList = [];
		
		var Prod = {},GoodsClass = {},GoodsClassType = {};
		if(LOCAL_DATA.success){
			var list = LOCAL_DATA.GoodsList,
				len = list.length;
			
			for(var i=0;i<len;i++){
				if(!Prod[list[i].Goods_Prod_Id]){
					Prod[list[i].Goods_Prod_Id] = {
						Id:list[i].Goods_Prod_Id,
						CName:list[i].Goods_Prod_CName
					};
				}
				if(!GoodsClass[list[i].Goods_GoodsClass]){
					GoodsClass[list[i].Goods_GoodsClass] = list[i].Goods_GoodsClass;
				}
				if(!GoodsClassType[list[i].Goods_GoodsClassType]){
					GoodsClassType[list[i].Goods_GoodsClassType] = list[i].Goods_GoodsClassType;
				}
			}
		}
		
		for(var i in Prod){
			LOCAL_DATA.ProdList.push(Prod[i]);
		}
		for(var i in GoodsClass){
			LOCAL_DATA.GoodsClassList.push(GoodsClass[i]);
		}
		for(var i in GoodsClassType){
			LOCAL_DATA.GoodsClassTypeList.push(GoodsClassType[i]);
		}
	}
	//初始化产品数据
	function initLocalGoodsData(callback){
		//加载数据
		loadGoodsList(function(data){
			LOCAL_DATA = LOCAL_DATA || {};
			if(data.success){
				LOCAL_DATA.success = true;
				var value = data.value || {};
				LOCAL_DATA.count = value.count || 0;
				LOCAL_DATA.GoodsList = value.list || [];
				if(callback) callback();
			}else{
				$("#ContentDiv").html('<div class="error-div">' + data.msg + '</div>');
			}
			
		});
	}
	
	//初始化订单数据
	function initOrderData(callback){
		if(DOC_ID){
			//加载供货单信息数据
			loadInfoData(function(data){
				if(data.success){
					LOCAL_DATA.DocInfo = data.value || {};
					COMP_ID = LOCAL_DATA.DocInfo.BmsCenOrderDoc_Comp_Id;
					COMP_NAME = LOCAL_DATA.DocInfo.BmsCenOrderDoc_Comp_CName;
					if(LOCAL_DATA.DocInfo.BmsCenOrderDoc_LabMemo){
						LOCAL_DATA.DocInfo.BmsCenOrderDoc_LabMemo = 
							LOCAL_DATA.DocInfo.BmsCenOrderDoc_LabMemo.replace(/<BR\/>/g,"\n");
					}
					//加载供货单明细列表数据
					loadDtlData(function(value){
						if(value.success){
							LOCAL_DATA.DtlList = (value.value || {}).list || [];
							//初始化产品数据
							initLocalGoodsData(callback);
						}else{
							$("#ContentDiv").html('<div class="error-div">' + data.msg + '</div>');
						}
					});
				}else{
					$("#ContentDiv").html('<div class="error-div"">' + data.msg + '</div>');
				}
			});
		}else{
			//初始化产品数据
			initLocalGoodsData(callback);
		}
	}
	
	//数据过滤处理
	function filter(){
		var type = CHECKED_TYPE_DOM || $("#type-ul li")[0],
			id = type.attr('id'),
			list = LOCAL_DATA.GoodsList || [],
			result = [];
		
		switch (id){
			case "checked":
				result = getCheckedGoods(list);
				break;
			case "all":
				result = getAllGoods(list);
				break;
			case "Prod":
				result = getGoodsByProd(list);
				break;
			case "GoodsClass":
				result = getGoodsByGoodsClass(list);
				break;
			case "GoodsClassType":
				result = getGoodsByGoodsClassType(list);
				break;
			default:
				break;
		}
		
		return result;
	}
	//获取已选的产品
	function getCheckedGoods(data){
		var list = data || [],
			len = list.length,
			result = [];
		
		for(var i=0;i<len;i++){
			if(list[i].checked){
				result.push(list[i]);
			}
		}
		return result;
	}
	//获取所有的产品
	function getAllGoods(data){
		var list = data || [],
			len = list.length,
			result = [];
		
		for(var i=0;i<len;i++){
			if(!list[i].checked){
				result.push(list[i]);
			}
		}
		return result;
	}
	//根据品牌获取产品
	function getGoodsByProd(data){
		var list = getAllGoods(data),
			len = list.length,
			result = [],
			prodId = $("#Prod").attr("data") || "";
			
		if(prodId && prodId != "all"){
			prodId = prodId == "other" ? "" : prodId;
			for(var i=0;i<len;i++){
				if(list[i].Goods_Prod_Id == prodId){
					result.push(list[i]);
				}
			}
		}else{
			result = list;
		}
		
		return result;
	}
	//根据一级获取产品
	function getGoodsByGoodsClass(data){
		var list = getGoodsByProd(data),
			len = list.length,
			GoodsClass = $("#GoodsClass").attr("data") || "",
			result = [];
			
		//品牌过滤+一级过滤
		if(GoodsClass && GoodsClass != "all"){
			GoodsClass = GoodsClass == "other" ? "" : GoodsClass;
			for(var i=0;i<len;i++){
				if(list[i].Goods_GoodsClass == GoodsClass){
					result.push(list[i]);
				}
			}
		}else{
			result = list;
		}
		
		return result;
	}
	//根据二级获取产品
	function getGoodsByGoodsClassType(data){
		var list = getGoodsByGoodsClass(data),
			len = list.length,
			GoodsClassType = $("#GoodsClassType").attr("data") || "",
			result = [];
			
		//品牌过滤+一级过滤+二级过滤
		if(GoodsClassType && GoodsClassType != "all"){
			GoodsClassType = GoodsClassType == "other" ? "" : GoodsClassType;
			for(var i=0;i<len;i++){
				if(list[i].Goods_GoodsClassType == GoodsClassType){
					result.push(list[i]);
				}
			}
		}else{
			result = list;
		}
		
		return result;
	}
	
	//保存数据
	function submit(type){
		if(DOC_ID){
			updateDoc(type);
		}else{
			addDoc(type);
		}
	}
	function addDoc(type){
		//获取订单信息
		var entity = getCenOrderDocInfo();
		//单据状态
		entity.Status = type;
		
		//获取明细列表+总价{TotalPrice:0,DtlList:[],add:[],update:[],del:[]}
		var Dtl = getCenOrderDtlList();
		//总价
		entity.TotalPrice = Dtl.TotalPrice;
		//明细列表
		entity.BmsCenOrderDtlList = Dtl.add;
		//确定的状态值
		entity.IsWriteExternalSystem = IsWriteExternalSystem;
		
		addDocData(Shell.util.JSON.encode({entity:entity}),function(id){
			if(type == "1"){
				onCheckBmsCenOrderDocById(id,function(){
					location.href = "list_lab.html?type=" + type;
				});
			}else{
				location.href = "list_lab.html?type=" + type;
			}
		});
	}
	//更新平台订货总单和明细
	function updateDoc(type){
		//Url:'ReagentService.svc/UpdateBmsCenOrderDocAndBmsCenOrderDtl',
		//Post:'bmsCenOrderDoc, mainFields, listAddBmsCenOrderDtl, 
		//listUpdateBmsCenOrderDtl, childFields, delBmsCenOrderDtlID'
		
		//获取订单信息
		var DocInfo = getCenOrderDocInfo();
		//订单主键
		DocInfo.Id = DOC_ID;
		//单据状态
		DocInfo.Status = type;
		//获取明细列表+总价{TotalPrice:0,DtlList:[],add:[],update:[],del:[]}
		var Dtl = getCenOrderDtlList();
		//总价
		DocInfo.TotalPrice = Dtl.TotalPrice;
		//确定的状态值
		DocInfo.IsWriteExternalSystem = IsWriteExternalSystem;
		
		var data = {
			bmsCenOrderDoc:DocInfo,
			mainFields:"Id,ReqDeliveryTime,UrgentFlag,LabMemo,Status,TotalPrice",
			listAddBmsCenOrderDtl:Dtl.add,
			listUpdateBmsCenOrderDtl:Dtl.update,
			childFields:"Id,GoodsQty",
			delBmsCenOrderDtlID:Dtl.del.join(",")
		};
		
		for(var i=0;i<data.listAddBmsCenOrderDtl.length;i++){
			data.listAddBmsCenOrderDtl[i].BmsCenOrderDoc = {Id:DOC_ID};
		}
		
		updateDocData(Shell.util.JSON.encode(data),function(){
			if(type == "1"){
				onCheckBmsCenOrderDocById(DOC_ID,function(){
					location.href = "list_lab.html?type=" + type;
				});
			}else{
				location.href = "list_lab.html?type=" + type;
			}
		});
	}
	
	//审核一个订货单
	function onCheckBmsCenOrderDocById(id,callback){
		var url = CHECK_BMS_CEN_ORDER_DOC_URL;
		ShellComponent.mask.save();
		Shell.util.Server.ajax({
			type:'post',
			url: url,
			data:Shell.util.JSON.encode({id:id})
		}, function(data){
			ShellComponent.mask.hide();
			if(data.success){
				callback();
			}else{
				ShellComponent.messagebox.msg(data.msg);
			}
		});
	}
	//获取订单主单信息
	function getCenOrderDocInfo(){
		var ReqDeliveryTime = $("#ReqDeliveryTime").val(),
			UrgentFlag = $("#UrgentFlag").bootstrapSwitch('state'),
			LabMemo = $("#LabMemo").val(),
			entity = {};
			
		//送货时间
		if(ReqDeliveryTime){
			entity.ReqDeliveryTime = Shell.util.Date.toServerDate(ReqDeliveryTime);
		}
		//紧急程度
		entity.UrgentFlag = UrgentFlag ? "0" : "1";
		//订货方备注
		entity.LabMemo = LabMemo.replace(/\n/g,"<BR/>") || "";
		//实验室ID
		entity.Lab = {Id:CENORGID}
		//实验室名称
		entity.LabName = Shell.util.LocalStorage.get(Shell.Rea.LocalStorage.map.CENORGNAME);
		//供应商ID
		entity.Comp = {Id:COMP_ID}
		//供应商名称
		entity.CompanyName = COMP_NAME;
		//单据状态
		entity.Status = "0";
		//主单订单号
		entity.OrderDocNo = "sys-test";
		
		return entity;
	}
	//获取明细列表+总价{TotalPrice:0,DtlList:[],add:[],update:[],del:[]}
	function getCenOrderDtlList(){
		var list = LOCAL_DATA.GoodsList,
			len = list.length,
			entity = {
				TotalPrice:0,
				add:[],
				update:[],
				del:[]
			};
			
		for(var i=0;i<len;i++){
			if(list[i].checked){
				var info = getCenOrderDtlInfo(list[i]);
				var TotalPrice = parseFloat(info.Price) * parseInt(info.GoodsQty);
				entity.TotalPrice += TotalPrice;//订单总价累计
				
				if(list[i].DtlID){
					entity.update.push({
						Id:list[i].DtlID,
						GoodsQty:info.GoodsQty
					});//修改
				}else{
					entity.add.push(info);//新增
				}
			}else{
				if(list[i].DtlID){
					entity.del.push(list[i].DtlID);//删除
				}
			}
		}
		entity.TotalPrice = (entity.TotalPrice || 0).toFixed(2);
		return entity;
	};
	//获取订单明细信息
	function getCenOrderDtlInfo(data){
		var entity = {};
		entity.ProdGoodsNo = data.Goods_ProdGoodsNo;//厂商产品编号
		entity.ProdOrgName = data.Goods_Prod_CName;//厂商名称
		entity.GoodsName = data.Goods_CName;//货品名称
		entity.GoodsUnit = data.Goods_UnitName;//包装单位
		entity.UnitMemo = data.Goods_UnitMemo;//包装单位描述
		entity.GoodsQty = data.GoodsQty || 1;//订货数量
		entity.Price = data.Goods_Price || 0;//单价
		
		//标志为1时，表示不是平台的产品
//		if(!data.Goods_Id && data.Goods_GoodsSource != "1"){
//			entity.Goods = {Id:data.Goods_Id};//产品ID
//		}
		entity.Goods = {Id:data.Goods_Id};//产品ID
		if(data.Prod_Id){
			entity.Prod = {Id:data.Prod_Id};//厂商ID
		}
		entity.OrderDtlNo = "sys-test";
		entity.OrderDocNo = "sys-test";
		return entity;
	}
	
	//=================数据处理-结束=================
	
	//=================页面渲染-开始=================
	//初始化页面内容
	function initPageContent(){
		$("#comp-name").html(COMP_NAME);
		$("#UrgentFlag_Div").show();
		$('#UrgentFlag_Div').children("input").bootstrapSwitch('state',LOCAL_DATA.DocInfo.BmsCenOrderDoc_UrgentFlag == "1" ? false : true);
		
		var currYear = (new Date()).getFullYear();
		$("#ReqDeliveryTime").mobiscroll({
			preset : 'date',
			theme: 'android-ics light', //皮肤样式
	        display: 'modal', //显示方式 
	        mode: 'scroller', //日期选择模式
			dateFormat: 'yyyy-mm-dd',
			lang: 'zh',
			showNow: true,
			nowText: "今天",
	        startYear: currYear - 30, //开始年份
	        endYear: currYear + 30 //结束年份
		});
		if(LOCAL_DATA.DocInfo.BmsCenOrderDoc_ReqDeliveryTime){
			var date = new Date(LOCAL_DATA.DocInfo.BmsCenOrderDoc_ReqDeliveryTime);;
			$("#ReqDeliveryTime").mobiscroll('setDate',new Date(date),true);
		}
		if(LOCAL_DATA.DocInfo.BmsCenOrderDoc_LabMemo){
			$("#LabMemo").val(LOCAL_DATA.DocInfo.BmsCenOrderDoc_LabMemo);
		}
		
		
	}
	//创建产品列表内容
	function createGoodsList(list){
		var type = CHECKED_TYPE_DOM || $("#type-ul li")[0],
			id = type.attr('id'),
			checked = id == "checked" ? true : false;
		
		var html = [];
		var len = list.length;
			
		for(var i=0;i<len;i++){
			var row = createRow(list[i],checked);
			html.push(row);
		}
		
		if(len == 0){
			var msg = "";
			if(checked){
				msg = "没有选中的产品!";
			}else{
				msg = "没有产品!";
			}
			html.push('<div class="no-data-div">' + msg +'</div>');//没有数据
		}else{
//			if(checked){
//				html.push('<div class="button-div" style="margin:40px 20px;">');
//				html.push('<button id="save" style="width:30%;">暂存</button>');
//				html.push('<button id="submit" style="width:30%;float:right;">提交</button>');
//				html.push('</div>');
//			}
		}
		
		return html.join('');
	}
	//创建数据行内容
	function createRow(value,checked){
		var html = [];
		
		//供货单号+订货方+紧急标志+单据状态+提取标志
		//SaleDocNo+Lab_CName+UrgentFlag+Status+IOFlag+UserName+OperDate
		
		var id = value.Goods_Id;
		var title = value.Goods_CName;
		
		var row0 = '<span style="color:#d9534f;font-weight:bold;">规格：( ' + value.Goods_UnitMemo + ' )';
		var row1 = '<span style="color:#666666;">品牌：' + value.Goods_Prod_CName + '</span>';
		var row2 = value.Goods_GoodsClass + ' - ' + value.Goods_GoodsClassType;
		var row3 = "单价：" + value.Goods_Price + "元";
		
		html.push('<div class="list-group-item" style="padding:0;float:left;width:100%;" data="' + id + '">');
		
		html.push('<div class="list-group-item-content">');
		html.push('<h4 class="list-group-item-heading">' + title + '</h4>');
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
			html.push('总价：<span style="color:#d9534f;font-weight:bold;">' + parseFloat(value.TotalPrice || value.Goods_Price).toFixed(2) + '</span>元');
			html.push('</div>');
			
			html.push('</div>');//end list-group-item-content
			
			html.push('<div class="remove-button-div" style="margin-top:-85px;"><span>删除</span></div>');
		}else{
			html.push('</div>');
		}
		
		html.push('</div>');
		
		return html.join('');
	}
	//刷新产品类型内容
	function refreshTypeInfo(){
		var top = 
			'<li class="disabled" style="text-align:center;"><h4 href="#">{title}</h4></li>' + 
			'<li role="separator" class="divider"></li>' + 
			'<li role="presentation" data="all"><a href="#">全部</a></li>' + 
			'<li role="separator" class="divider"></li>';
		var bottom = 
			'<li role="separator" class="divider"></li>' +
			'<li role="presentation" data="other"><a href="#">其他</a></li>';
			
		//品牌
		var prodHtml = [];
		prodHtml.push(top.replace(/{title}/,'品牌'));
		for(var i=0;i<LOCAL_DATA.ProdList.length;i++){
			prodHtml.push('<li role="presentation" data="' + LOCAL_DATA.ProdList[i].Id + 
				'"><a href="#">' + LOCAL_DATA.ProdList[i].CName + '</a></li>');
		}
		prodHtml.push(bottom);
		$("#Prod ul").html(prodHtml.join(""));
		
		//一级
		var goodsClassHtml = [];
		goodsClassHtml.push(top.replace(/{title}/,'一级'));
		for(var i=0;i<LOCAL_DATA.GoodsClassList.length;i++){
			goodsClassHtml.push('<li role="presentation" data="' + LOCAL_DATA.GoodsClassList[i] + 
				'"><a href="#">' + LOCAL_DATA.GoodsClassList[i] + '</a></li>');
		}
		goodsClassHtml.push(bottom);
		$("#GoodsClass ul").html(goodsClassHtml.join(""));
		
		//二级
		var goodsClassTypeHtml = [];
		goodsClassTypeHtml.push(top.replace(/{title}/,'二级'));
		for(var i=0;i<LOCAL_DATA.GoodsClassTypeList.length;i++){
			goodsClassTypeHtml.push('<li role="presentation" data="' + LOCAL_DATA.GoodsClassTypeList[i] + 
				'"><a href="#">' + LOCAL_DATA.GoodsClassTypeList[i] + '</a></li>');
		}
		goodsClassTypeHtml.push(bottom);
		$("#GoodsClassType ul").html(goodsClassTypeHtml.join(""));
		
		//初始化产品类型监听
		initTypeListeners();
	}
	//刷新列表数据
	function refreshContent(){
		if(LOCAL_DATA){
			var list = filter();
			var html = createGoodsList(list);
			$("#ContentDiv").html(html);
			initListeners();
		}else{
			//加载数据
			initLocalGoodsData(function(){
				var list = filter();
				var html = createGoodsList(list);
				$("#ContentDiv").html(html);
				initListeners();
			});
		}
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
		var list = LOCAL_DATA.GoodsList || [],
			len = list.length,
			checkedCount = 0,
			TotalPrice = 0,
			checkedNum = 0;
		
		for(var i=0;i<len;i++){
			if(list[i].checked){
				TotalPrice += parseFloat(list[i].TotalPrice || list[i].Goods_Price || 0);
				checkedCount += parseInt(list[i].GoodsQty || "1");
				checkedNum++;
			}
		}
		TotalPrice = TotalPrice.toFixed(2);
		
		$("#checked_num").text(checkedNum);
		$("#checked_count").text(checkedCount);
		$("#checked_totalprice").text(TotalPrice);
	}
	//创建已选的产品列表
	function createCheckedGoodsListHtml(){
		var list = getCheckedGoods(LOCAL_DATA.GoodsList || []),
			len = list.length,
			html = [];
			
		for(var i=0;i<len;i++){
			var row = createRow(list[i],true);
			html.push(row);
		}
		
		return html.join('');
	}
	//=================页面渲染-结束=================
	
	//=================事件监听-开始=================
	//初始化监听
	function initListeners(){
		var type = CHECKED_TYPE_DOM || $("#type-ul li")[0],
			id = type.attr('id');
			
		switch (id){
			case "checked":
				initCheckedListener();
				break;
			case "all":
			case "Prod":
			case "GoodsClass":
			case "GoodsClassType":
				initUnCheckedListener();
				break;
			default:
				break;
		}
	}
	//已选列表监听
	function initCheckedListener(){
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
			var x = Shell.util.Event.touch.move_x();
			if (x > 0) {
				$(this).css("WebkitTransform","translateX(0px)");
			}else{
				$(this).css("WebkitTransform","translateX(" + x + "px)");
			}
		});
		$(".list-group-item-content").on('touchend',function(e) {
			//e.preventDefault();
			var x = Shell.util.Event.touch.move_x();
			if (x >= -40) {
				$(this).css("WebkitTransform","translateX(0px)");
			}else if(x < -40){
				$(this).css("WebkitTransform","translateX(-80px)");
			}
		});
		
		//数量加减监听
		$(".number-spinner button").on(Shell.util.Event.click,function(e){
			e.preventDefault();
			if(!Shell.util.Event.isClick()) return;
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
			if(!Shell.util.Event.isClick()) return;
			var row = $(this).parent().parent().parent().parent(),
				id = row.attr("data"),
				now = parseInt($(this).val() || "0");
				
			now = now < 1 ? 1 : now;
			changeGoodsRecord(id,'GoodsQty',now);
			$(this).val(now);
			//更改总价
			changeDtlTotalPrice($(this),id);
		});
		
		$(".remove-button-div").on(Shell.util.Event.click,function(e){
			e.preventDefault();
			if(!Shell.util.Event.isClick()) return;
			var li = $(this).parent();
			var id = li.attr("data");
			changeGoodsRecord(id,'checked',false);
			changeGoodsRecord(id,'GoodsQty',1);
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
	//待选列表监听
	function initUnCheckedListener(){
		$("#ContentDiv div").on(Shell.util.Event.click,function(){
			if(!Shell.util.Event.isClick()) return;
			var id = $(this).attr("data");
			var len = LOCAL_DATA.GoodsList.length;
			for(var i=0;i<len;i++){
				if(LOCAL_DATA.GoodsList[i].Goods_Id == id){
					LOCAL_DATA.GoodsList[i].checked = true;
					break;
				}
			}
			
			$(this).remove();
			//更改所有产品总价
			changeDocTotalPrice();
		});
	}
	//初始化产品类型监听
	function initTypeListeners(){
		//监听
		$("#Prod ul li").on(Shell.util.Event.click,function(){
			$("#Prod").attr("data",$(this).attr("data"));
			$("#Prod a").first().html($(this).text() + ' <span class="caret"></span>');
		});
		//监听
		$("#GoodsClass ul li").on(Shell.util.Event.click,function(){
			$("#GoodsClass").attr("data",$(this).attr("data"));
			$("#GoodsClass a").first().html($(this).text() + ' <span class="caret"></span>');
		});
		//监听
		$("#GoodsClassType ul li").on(Shell.util.Event.click,function(){
			$("#GoodsClassType").attr("data",$(this).attr("data"));
			$("#GoodsClassType a").first().html($(this).text() + ' <span class="caret"></span>');
		});
	}
	
	//购物车
	$("#checked-goods").on(Shell.util.Event.click,function (e) {
		e.preventDefault();
		CheckGoodsTouch();
	});
	function CheckGoodsTouch(){
		var html = createCheckedGoodsListHtml();
		if(CHECKED_TYPE_DOM){
			CHECKED_TYPE_DOM.removeClass("active");
		}
		if(html){
			$("#submit-div").show();
		}else{
			html = '<div class="no-data-div">没有选中的产品!</div>';
			$("#submit-div").hide();
		}
		$("#checked-goods").css("background-color","#d9534f");
		$("#checked-goods").css("border-color","#d9534f");
		$("#ContentDiv").html(html);
		initCheckedListener();
	}
	
	$("#submit-div div").on(Shell.util.Event.click,function (e) {
		e.preventDefault();
		if(!Shell.util.Event.isClick()) return;
		//submit($(this).attr("data"));//保存数据
		CHECKED_BUTTON = $(this);
		$("#InfoModal").modal({ backdrop: 'static', keyboard: false });
	});
	
	//回到顶部监听
	$("#returnTop").on(Shell.util.Event.click,function () {
		var speed=200;//滑动的速度
		$('body,html').animate({ scrollTop: 0 }, speed);
		return false;
	});
	
	//类型点击监听
	$("#type-ul li").on(Shell.util.Event.click, function(){
		typeTouch($(this));
	});
	$("#type-ul li").on("click", function(){});
	function typeTouch(dom){
		if(CHECKED_TYPE_DOM){
			CHECKED_TYPE_DOM.removeClass("active");
		}
		CHECKED_TYPE_DOM = dom;
		CHECKED_TYPE_DOM.addClass("active");
		TYPE = CHECKED_TYPE_DOM.attr("data");
		$("#submit-div").hide();
		$("#checked-goods").css("background-color","#169ada");
		$("#checked-goods").css("border-color","#169ada");
		//刷新列表数据
		refreshContent();
	}
	
	//确定操作
	$("#ok-button").on(Shell.util.Event.click,function(){
		if(!CHECKED_BUTTON) return;
		submit(CHECKED_BUTTON.attr("data"));//保存数据
	});
	
	//=================事件监听-结束=================
	
	//初始化执行
	setTimeout(function(){
		//初始化参数
		initParamsData();
		
//		//初始化产品数据
//		initLocalGoodsData(function(){
//			//初始化产品类型数据
//			initTypeData();
//			//刷新过滤选项内容
//			refreshTypeInfo();
//			//默认选中类型,刷新数据
//			$("#type-ul li")[TYPE ? parseInt(TYPE) : 1].click();
//		});
		
		//初始化订单数据
		initOrderData(function(){
			//初始化已选的产品
			initCheckedGoodsData();
			//初始化页面内容
			initPageContent();
			//初始化产品类型数据
			initTypeData();
			//刷新过滤选项内容
			refreshTypeInfo();
			if(DOC_ID){
				//显示购物车信息
				CheckGoodsTouch();
			}else{
				//选中产品类型
				var dom = $("#type-ul li[index='" + (TYPE ? TYPE : "1") + "']");
				typeTouch(dom);
			}
		});
	},500);
});