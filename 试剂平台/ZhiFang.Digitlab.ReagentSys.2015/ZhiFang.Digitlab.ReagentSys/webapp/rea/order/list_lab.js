$(function() {
	//条件类型:临时0、待验收1、已验收2
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
	//获取条件
	function getWhere (){
		var where = [];
		
		//console.log("CENORGID=" + Shell.util.LocalStorage.get(Shell.Rea.LocalStorage.map.CENORGID));
		where.push("bmscenorderdoc.Lab.Id=" + Shell.util.LocalStorage.get(Shell.Rea.LocalStorage.map.CENORGID));
		//where.push("bmscenorderdoc.UserID=" + Shell.util.Cookie.get(Shell.util.Cookie.map.USERID));
		//临时0、已提交1、已确认2、已出货3、已验收4
		switch (TYPE){
			case "0": where.push("bmscenorderdoc.Status=0");
				break;
			case "1": where.push("(bmscenorderdoc.Status=1 or bmscenorderdoc.Status=2 or bmscenorderdoc.Status=3)");
				break;
			case "2": where.push("bmscenorderdoc.Status=4");
				break;
			default:
				break;
		}
		
		return where.join(" and ");
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
				callback(data);
			}else{
				ShellComponent.messagebox.msg(data.msg);
			}
		});
	}
	
	//删除订单
	function delDocById(id,callback){
//		物理删除
//		var url = Shell.util.Path.ROOT + '/ReagentSysService.svc//ST_UDTO_DelBmsCenOrderDoc?id=' + id;
//			
//		ShellComponent.mask.del();
//		Shell.util.Server.ajax({
//			url: url
//		}, function(data){
//			ShellComponent.mask.hide();
//			callback(data);
//		});
		
		//逻辑删除，置状态Status=999
		updateStatus(id,"999",callback);
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
	//修改订货单内容
	function editInfo(id){
		//ShellComponent.messagebox.msg(id);
		location.href = 'entry.html?type=0&id=' + id;
	}
	//查看供货单内容
	function showInfo(id){
		//ShellComponent.messagebox.msg(id);
		location.href = 'info.html?type=0&id=' + id;
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
		
		html.push('<div class="list-group-item" style="padding:0;" data="' + value.BmsCenOrderDoc_Id + '">');
		//内容DIV
		html.push('<div class="list-group-item-content">');
		html.push('<h4 class="list-group-item-heading">' + value.BmsCenOrderDoc_Comp_CName + '</h4>');
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
		
		//临时0、已提交1、已确认2、已出货3、已验收4
		if(value.BmsCenOrderDoc_Status == "0"){//临时
			
		}else if(value.BmsCenOrderDoc_Status == "1"){//已提交
			html.push('<div data="back" class="block60" style="float:right;margin-top:-65px;color:#ffffff;background-color:#ccc;border-color:#ccc;">撤回</div>');
		}else if(value.BmsCenOrderDoc_Status == "2"){//已确认
			
		}else if(value.BmsCenOrderDoc_Status == "3"){//已出货
			html.push('<div data="accept" class="block60" style="float:right;margin-top:-65px;color:#ffffff;background-color:#f0ad4e;border-color:#f0ad4e;">验收</div>');
		}else if(value.BmsCenOrderDoc_Status == "4"){//已验收
			
		}
		html.push('</div>');//end list-group-item-content
		if(value.BmsCenOrderDoc_Status == "0"){//临时
			html.push('<div class="remove-button-div" style="margin-top:-75px;"><span>删除</span></div>');
		}
		
		html.push('</div>');
		
		return html.join('');
	}
	
	//监听
	function initListeners(){
		var CHECKEDROW = null;
		
		
		if(TYPE == "0"){//临时
			//左右滑动监听
			$(".list-group-item-content").on('touchstart',function(event) {
				if(CHECKEDROW && CHECKEDROW != $(this)){
					CHECKEDROW.css("WebkitTransform","translateX(0px)");
				}
				CHECKEDROW = $(this);
			});
			$(".list-group-item-content").on('touchmove',function(event) {
				var x = Shell.util.Event.touch.move_x();
				if (x < -40) {
					$(this).css("WebkitTransform","translateX(-80px)");
				}else if (x > 0) {
					$(this).css("WebkitTransform","translateX(0px)");
				}else{
					$(this).css("WebkitTransform","translateX(" + x + "px)");
				}
			});
			$(".list-group-item-content").on('touchend',function(event) {
				var x = Shell.util.Event.touch.move_x();
				if (x >= -40) {
					$(this).css("WebkitTransform","translateX(0px)");
				}
			});
			//删除订单
			$(".remove-button-div").on(Shell.util.Event.click,function(){
				if(!Shell.util.Event.isClick()) return;
				var div = $(this).parent();
				var id = div.attr("data");
				//删除订单
				delDocById(id,function(data){
					if(data.success){
						div.remove();
						ShellComponent.messagebox.msg("删除成功");
					}
				});
			});
			//修改订单
			$(".list-group-item-content").on(Shell.util.Event.click,function(event){
				if(event.target.className != "block60"){
					if(!Shell.util.Event.isClick()) return;
					var div = $(this).parent();
					var id = div.attr("data");
					//修改订货单视图
					editInfo(id);
				}
			});
		}else if(TYPE == "1"){
			//已提交-撤回
			$(".block60[data=back]").on(Shell.util.Event.click,function(event) {
				if(!Shell.util.Event.isClick()) return;
				var div = $(this).parent().parent();
				var id = div.attr("data");
				StatusBack(div,id);
			});
			//待收货-验收
			$(".block60[data=accept]").on(Shell.util.Event.click,function(event) {
				if(!Shell.util.Event.isClick()) return;
				var div = $(this).parent().parent();
				var id = div.attr("data");
				StatusAccept(div,id);
			});
			//查看详情
			$(".list-group-item-content").on(Shell.util.Event.click,function(event){
				if(event.target.className != "block60"){
					if(!Shell.util.Event.isClick()) return;
					var div = $(this).parent();
					var id = div.attr("data");
					//修改订货单视图
					showInfo(id);
				}
			});
		}else if(TYPE == "2"){////已验收
			//查看详情
			$(".list-group-item-content").on(Shell.util.Event.click,function(event){
				if(event.target.className != "block60"){
					if(!Shell.util.Event.isClick()) return;
					var div = $(this).parent();
					var id = div.attr("data");
					//修改订货单视图
					showInfo(id);
				}
			});
		}
	}
	//撤回
	function StatusBack(div,id){
		updateStatus(id,"0",function(){
			div.remove();
		});
	}
	//验收
	function StatusAccept(div,id){
		updateStatus(id,"4",function(){
			div.remove();
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
		var dom = $("#type-ul li[index='" + (TYPE ? TYPE : "0") + "']");
		typeTouch(dom);
	},100);
});