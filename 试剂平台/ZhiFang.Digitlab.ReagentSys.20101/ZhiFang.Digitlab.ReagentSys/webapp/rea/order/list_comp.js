$(function() {
	//条件类型:待确认1、待验收2、待发货3、已验收4
	var TYPE = Shell.util.getRequestParams(true).TYPE;
	//当前选中的类型
	var CHECKED_TYPE = null;
	
	//加载订货单列表数据
	function loadData(callback){
		var url = Shell.util.Path.ROOT + '/ReagentSysService.svc/ST_UDTO_SearchBmsCenOrderDocByHQL?isPlanish=true';
		var fields = [
			'BmsCenOrderDoc_Id','BmsCenOrderDoc_OrderDocNo','BmsCenOrderDoc_SaleDocNo',
			'BmsCenOrderDoc_Lab_CName','BmsCenOrderDoc_Comp_CName','BmsCenOrderDoc_UrgentFlag',
			'BmsCenOrderDoc_Status','BmsCenOrderDoc_DataAddTime','BmsCenOrderDoc_OperDate'
		];
		url += '&fields=' + fields.join(',');
		url += '&where=' + getWhere();
			
		ShellComponent.mask.loading();
		Shell.util.Server.ajax({
			url: url
		}, function(data){
			ShellComponent.mask.hide();
			callback(data);
		});
	}
	//更新订单状态
	function updateStatus(id,status,callback){
		var data = {
			entity:{
				Id:id,
				Status:status
			},
			fields:"Id,Status"
		};
		data = Shell.util.JSON.encode(data);
		
		var url = Shell.util.Path.ROOT + '/ReagentSysService.svc/ST_UDTO_UpdateBmsCenOrderDocByField';
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
	//获取条件
	function getWhere (){
		var where = [];
		
		where.push("bmscenorderdoc.Comp.Id=" + Shell.util.LocalStorage.get(Shell.Rea.LocalStorage.map.CENORGID));
		where.push("bmscenorderdoc.Status=" + TYPE);
		
		return where.join(" and ");
	}
	
	//刷新列表数据
	function refreshContent(){
		//加载数据
		loadData(function(data){
			var html = createList(data);
			$("#ContentDiv").html(html);
			initListeners();
		});
		
	}
	
	//查看订货单内容
	function editInfoSatus(id,status){
		//ShellComponent.messagebox.msg(id);
		location.href = 'edit.html?type=' + status + '&id=' + id;
	}
	//创建列表内容
	function createList(data){
		var html = [];
		if(!data.success){
			html.push('<div class="error-div">' + data.msg + '</div>');//错误信息
		}else{
			var list = data.value.list || [],
				len = list.length;
				
			for(var i=0;i<len;i++){
				var row = createRow(list[i]);
				html.push(row);
			}
			
			if(len == 0){
				html.push('<div class="no-data-div">没有找到数据!</div>');//没有数据
			}
		}
		
		return html.join('');
	}
	//创建数据行内容
	function createRow(value){
		var html = [];
		
		html.push('<div class="list-group-item" style="padding:0;" data="' + 
			value.BmsCenOrderDoc_Id + ',' + value.BmsCenOrderDoc_Status + '">');
		//内容DIV
		html.push('<div class="list-group-item-content">');
		html.push('<h4 class="list-group-item-heading">' + value.BmsCenOrderDoc_Lab_CName + '</h4>');
		html.push('<div class="list-group-item-text" style="font-size:13px;">订货单号：' + (value.BmsCenOrderDoc_OrderDocNo || '') + '</div>');
		
		if(value.BmsCenOrderDoc_Status == '0'){
			html.push('<div class="list-group-item-text" style="font-size:13px;">新增时间：' + 
				'<span style="color:#d9534f">' + (Shell.util.Date.toString(value.BmsCenOrderDoc_DataAddTime) || '') + 
				'</span></div>');
		}else{
			html.push('<div class="list-group-item-text" style="font-size:13px;">提交时间：' + 
				'<span style="color:#d9534f">' + (Shell.util.Date.toString(value.BmsCenOrderDoc_OperDate) || '') + 
				'</span></div>');
		}
		
		html.push('<div style="height:25px;">');
		//紧急程度
		var UrgentFlag = Shell.Rea.Enum.BmsCenSaleDoc_UrgentFlag['E' + value.BmsCenOrderDoc_UrgentFlag];
		html.push('<div class="enum-div" style="float:left;background-color:' + UrgentFlag.bcolor + 
			';color:' + UrgentFlag.color + ';">' + UrgentFlag.value +'</div>');
			
		//订单状态
		var Status = Shell.Rea.Enum.BmsCenSaleDoc_Status['E' + value.BmsCenOrderDoc_Status];
		html.push('<div class="enum-div" style="float:left;background-color:' + Status.bcolor + 
			';color:' + Status.color + ';">' + Status.value +'</div>');
		html.push('</div>');
		html.push('</div>');//end list-group-item-content
		html.push('</div>');
		
		return html.join('');
	}
	
	//监听
	function initListeners(){
		//查看详情
		$(".list-group-item-content").on(Shell.util.Event.click,function(event){
			if(event.target.className != "block60"){
				if(!Shell.util.Event.isClick()) return;
				var div = $(this).parent();
				var arr = div.attr("data").split(",");
				var id = arr[0];
				var status = arr[1];
				//修改订货单状态
				editInfoSatus(id,status);
			}
		});
	}
	function typeTouch(dom){
		if(CHECKED_TYPE){CHECKED_TYPE.removeClass("active");}
		CHECKED_TYPE = dom;
		CHECKED_TYPE.addClass("active");
		TYPE = CHECKED_TYPE.attr("index");
		refreshContent();
	}
	$("#type-ul li").on(Shell.util.Event.click, function(){
		typeTouch($(this));
	});
	
	//默认选中类型,刷新数据
	setTimeout(function(){
		var dom = $("#type-ul li[index='" + (TYPE ? TYPE : "1") + "']");
		typeTouch(dom);
	},100);
});