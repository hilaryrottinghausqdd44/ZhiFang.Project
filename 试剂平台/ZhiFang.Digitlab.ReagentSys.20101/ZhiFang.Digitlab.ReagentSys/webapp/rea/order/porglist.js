$(function() {
	//机构ID
	var CENORGID = Shell.util.LocalStorage.get(Shell.Rea.LocalStorage.map.CENORGID);
	//初始化列表监听
	function initListeners(){
		$(".list-group-item").on(Shell.util.Event.click,function(){
			if(!Shell.util.Event.isClick()) return;
			
			var data = $(this).attr("data");
			//修改订货单视图
			showInfo(data);
		});
	}
	//查看供货单内容
	function showInfo(data){
		location.href = 'entry.html?comp=' + data;
	}
	//加载订货单列表数据
	function loadData(callback){
		var url = Shell.util.Path.ROOT + '/ReagentSysService.svc/ST_UDTO_SearchCenOrgConditionByHQL?isPlanish=true';
		var fields = [
			'CenOrgCondition_cenorg1_OrgNo','CenOrgCondition_cenorg1_CName',
			'CenOrgCondition_cenorg1_CenOrgType_CName','CenOrgCondition_cenorg1_Id'
		];
		url += '&fields=' + fields.join(',');
		url += '&where=' + getWhere();
			
		ShellComponent.mask.loading();
		Shell.util.Server.ajax({
			async:true,
			url: url
		}, function(data){
			ShellComponent.mask.hide();
			callback(data);
		});
	}
	//获取条件
	function getWhere (){
		return "cenorgcondition.cenorg2.Id=" + CENORGID;
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
		
		var id = value.CenOrgCondition_cenorg1_Id;
		var name = value.CenOrgCondition_cenorg1_CName;
		var orgno = value.CenOrgCondition_cenorg1_OrgNo;
		var type = value.CenOrgCondition_cenorg1_CenOrgType_CName;
		
		var pStyle = 'margin:2px;font-size:11px;';
		html.push('<div class="list-group-item" style="padding:10px;" data="' + id + ',' + name + '">');
		html.push('<h4 class="list-group-item-heading">' + name + '</h4>');
		html.push('<div class="list-group-item-text" style="' + pStyle + '">' + orgno + '</div>');
		html.push('<div class="list-group-item-text" style="' + pStyle + '">' + type + '</div>');
		
		html.push('</div>');
		
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
	//刷新数据
	setTimeout(function(){
		refreshContent();
	},500);
});