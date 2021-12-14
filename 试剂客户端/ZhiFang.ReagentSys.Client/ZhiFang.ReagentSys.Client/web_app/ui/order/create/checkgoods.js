$(function () {
	//外部参数
	var PARAMS = JcallShell.getRequestParams(true);
	//获取货品地址
	var SEARCH_GOODS_URL = JcallShell.System.Path.ROOT + "/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsOrgLinkByHQL";
	//缓存中的货品列表
	var LOCALSTORAGE_GOODS_LIST = JcallShell.LocalStorage.UserOrder.getGoodsList();//明细列表
	//货品列表
	var GOODS_LIST = [];
	
	function getData(callback){
		var fields = [
			'ReaGoodsOrgLink_ReaGoods_Id','ReaGoodsOrgLink_ReaGoods_CName','ReaGoodsOrgLink_ReaGoods_UnitName',
			'ReaGoodsOrgLink_Price','ReaGoodsOrgLink_ReaGoods_UnitMemo','ReaGoodsOrgLink_ReaGoods_Prod_CName',
			'ReaGoodsOrgLink_ReaGoods_GoodsClass','ReaGoodsOrgLink_ReaGoods_GoodsClassType',
			'ReaGoodsOrgLink_Id','ReaGoodsOrgLink_ReaGoods_ProdOrgName'
		];
		var where = 'reagoodsorglink.CenOrg.Id=' + PARAMS.COMPID;
		
		var url = SEARCH_GOODS_URL + '?isPlanish=true&fields=' + fields.join(',') + '&where=' + where;
		JcallShell.Server.ajax({
			url:url
		},function(data){
			var list = ((data || {}).value || {}).list || [];
			for(var i in list){
				list[i].Id = list[i].ReaGoodsOrgLink_ReaGoods_Id;
			}
			GOODS_LIST = list;
			callback();
		});
	}
	
	//初始化订单明细列表
	function initGoodsListHtml(){
		var list = GOODS_LIST,
			len = list.length,
			html = [];
			
		var localLen = LOCALSTORAGE_GOODS_LIST.length;
		for(var i=0;i<len;i++){
			for(var j=0;j<localLen;j++){
				if(list[i].Id == LOCALSTORAGE_GOODS_LIST[j].Id){
					list[i].checked = true;
					break;
				}
			}
		}
			
		for(var i=0;i<len;i++){
			var row = createRow(list[i],list[i].checked);
			html.push(row);
		}
		
		if(html.length == 0){
			html.push('<div style="margin:20px;padding:20px;text-align:center;color:#169ada;">没有查到数据！</div>');
		}
		
		$("#list").html(html.join(''));
		
		initRowListeners();
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
		
		html.push('<div class="weui-cells weui-cells_checkbox" style="margin:0;">');
		html.push('<label class="weui-cell weui-check__label" style="margin:0;padding:0 10px;" for="s' + id + '">');
		html.push('<div class="weui-cell__hd">');
		html.push('<input type="checkbox" class="weui-check" name="checkbox1" id="s' + id + '"');
		if(checked){html.push(' checked="checked"');}
		html.push('>');
		html.push('<i class="weui-icon-checked"></i>');
		html.push('</div>');
		html.push('<div class="weui-cell__bd">');
		
		html.push('<div class="list-group-item-content">');
		html.push('<div class="list-group-item-heading">' + title + '</div>');
		html.push('<div class="list-group-item-text">' + row0 + '</div>');
		html.push('<div class="list-group-item-text touch">' + row1 + '</div>');
		html.push('<div class="list-group-item-text">' + row2 + '</div>');
		html.push('<div class="list-group-item-text">' + row3 + '</div>');
		html.push('</div>');
		
		html.push('</div>');
		html.push('</label>');
		html.push('</div>');
		
		html.push('</div>');
		
		return html.join('');
	}
	//行处理监听
	function initRowListeners(){
		$(".weui-check").on("change",function(){
			var isChecked = $(this).is(':checked');
			var id = $(this).attr("id").slice(1);
			//选中货品变更处理
			obChangeCheckedGoods(id,isChecked);
		});
	}
	//选中货品变更处理
	function obChangeCheckedGoods(id,isChecked){
		var list = GOODS_LIST,
			len = list.length,
			info = null;
			
		for(var i=0;i<len;i++){
			if(list[i].Id == id){
				info = list[i];
				break;
			}
		}
		if(isChecked){
			JcallShell.LocalStorage.UserOrder.addGoods(info);
		}else{
			JcallShell.LocalStorage.UserOrder.removeGoods(id);
		}
		changeDocTotal();
	}
	//更改可以货品种类
	function changeDocTotal(){
		var list = JcallShell.LocalStorage.UserOrder.getGoodsList(),
			len = list.length;
		
		$("#total").text(len);
	}
	
	//初始化页面内容
	function initPageContent(){
		$("#back-to-create-button").on(JcallShell.Event.click,function(){
			location.href="index.html";
		});
		
		getData(function(){
			initGoodsListHtml();
			changeDocTotal();
		});
	}
	
	//初始化页面内容
	initPageContent();
});