$(function() {
	//初始化列表监听
	function initListeners(){
		$(".list-group-item").on(Shell.util.Event.click,function(){
			if(!Shell.util.Event.isClick()) return;
			
			var id = $(this).attr("data");
			//修改供货单视图
			showInfo(id);
		});
	}
	//查看供货单内容
	function showInfo(id){
		location.href = 'info.html?id=' + id;
	}
	//加载供货单列表数据
	function loadData(callback){
		var url = Shell.util.Path.ROOT + '/ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDocByHQL?isPlanish=true';
		var fields = [
			'BmsCenSaleDoc_Id','BmsCenSaleDoc_SaleDocNo','BmsCenSaleDoc_Lab_CName',
			'BmsCenSaleDoc_UrgentFlag','BmsCenSaleDoc_Status','BmsCenSaleDoc_IOFlag',
			'BmsCenSaleDoc_UserName','BmsCenSaleDoc_OperDate'
		];
		url += '&fields=' + fields.join(',');
			
		ShellComponent.mask.loading();
		Shell.util.Server.ajax({
			async:false,
			url: url
		}, function(data){
			ShellComponent.mask.hide();
			callback(data);
		});
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
		
		//供货单号+订货方+紧急标志+单据状态+提取标志
		//SaleDocNo+Lab_CName+UrgentFlag+Status+IOFlag+UserName+OperDate
		
		var id = value.BmsCenSaleDoc_Id;
		var title = '订货方:' + value.BmsCenSaleDoc_Lab_CName + '';
		var info = '供货单号:' + value.BmsCenSaleDoc_SaleDocNo;
		var UserName = '操作人员:' + value.BmsCenSaleDoc_UserName;
		var OperDate = '操作时间:' + value.BmsCenSaleDoc_OperDate;
		
		var pStyle = 'margin:2px;font-size:11px;';
		html.push('<a class="list-group-item" data="' + id + '">');
		html.push('<h4 class="list-group-item-heading">' + title + '</h4>');
		html.push('<div class="list-group-item-text" style="' + pStyle + '">' + info + '</div>');
		html.push('<div class="list-group-item-text" style="' + pStyle + '">' + UserName + '</div>');
		html.push('<div class="list-group-item-text" style="' + pStyle + '">' + OperDate + '</div>');
		
		//状态标识
		var UrgentFlag = Shell.Rea.Enum.BmsCenSaleDoc_UrgentFlag['E' + value.BmsCenSaleDoc_UrgentFlag];
		var Status = Shell.Rea.Enum.BmsCenSaleDoc_Status['E' + value.BmsCenSaleDoc_Status];
		var IOFlag = Shell.Rea.Enum.BmsCenSaleDoc_IOFlag['E' + value.BmsCenSaleDoc_IOFlag];
		html.push('<div style="float:right;margin-top:-70px;">');
		var divStyle = 'width:60px;text-align:center;padding:2px;margin:2px;font-size:11px;';
		html.push('<div style="' + divStyle + 'background-color:' + UrgentFlag.bcolor + 
			';color:' + UrgentFlag.color + ';">' + UrgentFlag.value +'</div>');
		html.push('<div style="' + divStyle + 'background-color:' + Status.bcolor + 
			';color:' + Status.color + ';">' + Status.value +'</div>');
		html.push('<div style="' + divStyle + 'background-color:' + IOFlag.bcolor + 
			';color:' + IOFlag.color + ';">' + IOFlag.value +'</div>');
		html.push('</div>');
		
		html.push('</a>');
		
		return html.join('');
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
	refreshContent();
});