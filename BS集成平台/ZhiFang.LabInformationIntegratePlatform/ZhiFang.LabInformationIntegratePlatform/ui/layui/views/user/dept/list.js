layui.extend({
	uxutil: 'ux/util'
}).use(['table','uxutil','form'], function(){
	var $ = layui.$,
		table = layui.table,
		form = layui.form,
		uxutil = layui.uxutil;
		
	//获取列表服务
	var GET_LINK_LIST_URL = uxutil.path.ROOT + '/ServerWCF/IMService.svc/ST_UDTO_SearchCVCriticalValueEmpIdDeptLinkByHQL';
	//删除数据服务
	var DEL_LINK_URL = uxutil.path.ROOT + '/ServerWCF/IMService.svc/ST_UDTO_DelCVCriticalValueEmpIdDeptLink';
	
	var DEPT_LIST = null,
		USER_LIST = null,
		FIELDS = [''];
		
	//page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}
	//列表配置
	var config = {
		elem: '#table',
		toolbar: '#table-toolbar-top',
		height:'full-40',
		title: '用户数据表',
		initSort: {
			field:'EmpName',
			type:'asc'
		},
		cols: [[
			{type:'checkbox', fixed: 'left'},
			{field:'Id', width:180, title: 'ID', sort: true,templet:function(d){
				return '<span style="color:orange;">\'' + d.Id + '\'</span>';
			}},
			{field:'DeptName', width:160, title: '科室', sort: true,templet:function(d){
				return '<span style="color:red;">' + d.DeptName + '</span>';
			}},
			{field:'EmpName', width:100, title: '员工', sort: true,templet:function(d){
				return '<span style="color:green;">' + d.EmpName + '</span>';
			}},
			{field:'Memo', minWidth:150, title: '备注', sort: true}
		]],
		loading:true,
		page: true,
		parseData: function(res){ //res 即为原始返回的数据
			if(!res) return;
			var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
			return {
				"code": res.success ? 0 : 1, //解析接口状态
				"msg": res.ErrorInfo, //解析提示文本
				"count": data.count || 0, //解析数据长度
				"data": data.list || []
			};
		},
		defaultToolbar: ['filter', 'exports', {
			title: '自定义导出',
			layEvent: 'exports_1',
			icon: 'layui-icon-export'
		}, {
			title: '带样式导出',
			layEvent: 'exports_2',
			icon: 'layui-icon-export'
		},'print']
	};
	//列表实例
	var tableInd = table.render(config);
	
	//头工具栏事件
	table.on('toolbar(table)', function(obj){
		switch(obj.event){
			case 'search':onSearch();break;
			case 'add':onAddClick();break;
			case 'del':onDelClick();break;
			case 'exports_1':onExports1();break;
			case 'exports_2':onExports2();break;
		}
	});
	
	form.on('select(Dept)', function(data){
		onSearch();
	});
	form.on('select(User)', function(data){
		onSearch();
	});
	//自定义导出
	function onExports1(){
		var data = table.clearCacheKey(table.cache[tableInd.config.id]);
		for(var i in data){
			data[i].Id = "@" + data[i].Id;
		}
		table.exportFile(tableInd.config.id,data,'xls');
	};
	//带样式导出
	function onExports2(){
		tableToExcel(tableInd); 
	};
	//列表转Excel下载
	function tableToExcel(tabelInstance){
		var config = tabelInstance.config,
			type = 'xls',
			textType = 'application/vnd.ms-excel',
			alink = document.createElement("a");
		
		alink.href = 'data:'+ textType +';charset=utf-8,\ufeff'+ encodeURIComponent(function(){
			var tableHtml = $(config.elem).next().find('.layui-table-box')[0].innerHTML;
			var template = [
				'<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40">',
					'<head>',
						'<!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet>',
						'<x:Name>' + config.title + '</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets>',
						'</x:ExcelWorkbook></xml><![endif]-->',
						'<style type="text/css">',
						'</style>',
					'</head>',
					'<body ><table class="excelTable">' + tableHtml + '</table></body>',
				'</html>'
			].join();
			return template;
	    }());
    
		alink.download = (config.title || 'table_'+ (config.index || '')) + '.' + type; 
		document.body.appendChild(alink);
		alink.click();
		document.body.removeChild(alink); 
	};
	
	//查询
	function onSearch(){
		var deptId = $('#Dept option:selected').val(),
			userId = $('#User option:selected').val();
			
		var where = [];
		if(deptId){
			where.push("cvcriticalvalueempiddeptlink.DeptID=" + deptId);
		}
		if(userId){
			where.push("cvcriticalvalueempiddeptlink.EmpID=" + userId);
		}
			
		onLoad({"where":where.join(' and ')});
		initSelect();
		
		$("#Dept").val(deptId);
		$("#User").val(userId);
	};
	window.onSearch = onSearch;
	//新增
	function onAddClick(){
		layer.open({
			title:'新增挂靠',
			type:2,
			content:'form.html?t=' + new Date().getTime(),
			maxmin:true,
			toolbar:true,
			resize:true,
			area:['300px','400px']
		});
	}
	//删除
	var DelErrorList = [];
	function onDelClick(){
		//获取选中数据
		var checkStatus = table.checkStatus('table'),
			data = checkStatus.data,
			len = data.length;
		
		if(len > 0){
			layer.confirm('确定需要删除吗？', function(index){
				var ids = [];
				for(var i=0;i<len;i++){
					ids.push(data[i].Id);
				}
				DelErrorList = [];
				delOneInfo(ids,0);
				layer.close(index);
			});
		}else{
			layer.msg("请选择需要删除的数据！");
		}
	}
	//删除数据
	function delOneInfo(ids,index){
		var id = ids[index];
		
		uxutil.server.ajax({
			url:DEL_LINK_URL + '?id=' + id
		},function(data){
			if(!data.success){
				DelErrorList.push(data.msg);
			}
			if(index < ids.length-1){
				delOneInfo(ids,++index);
			}else{
				if(DelErrorList.length > 0){
					layer.msg(DelErrorList.join('</br>'));
				}else{
					onSearch();
					layer.msg("删除完毕！");
				}
			}
		});
	}
	
	//加载数据
	function onLoad(whereObj){
		var cols = config.cols[0],
			fields = [];
			
		for(var i in cols){
			fields.push('CVCriticalValueEmpIdDeptLink_' + cols[i].field);
		}
			
		tableInd.reload({
			url:GET_LINK_LIST_URL,
			where:$.extend({},whereObj,{
				fields:fields.join(',')
			})
		});
	}
	
	//初始化select组件
	function initSelect(){
		//科室
		if(DEPT_LIST){
			createSelect("Dept","科室",DEPT_LIST);
		}else{
			getDeptList(function(){
				createSelect("Dept","科室",DEPT_LIST);
			});
		}
		//员工
		if(USER_LIST){
			createSelect("User","员工",USER_LIST);
		}else{
			getUserList(function(){
				createSelect("User","员工",USER_LIST);
			});
		}
	}
	//创建select组件
	function createSelect(domId,defaultName,list){
		var len = list.length,
			htmls = ['<option value="">' + defaultName + '</option>'];
			
		for(var i=0;i<len;i++){
			htmls.push('<option value="' + list[i].Id + '">' + list[i].CName + '</option>');
		}
		$("#" + domId).html(htmls.join(""));
		form.render('select');
	}
	//获取部门列表
	function getDeptList(callback){
		var url = uxutil.path.ROOT + "/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHRDeptByHQL";
		url += "?fields=HRDept_CName,HRDept_Id&where=hrdept.IsUse=1";
		
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data.success){
				DEPT_LIST = (data.value || {}).list || [];
				callback();
			}
		});
	}
	//获取员工列表
	function getUserList(callback){
		var url = uxutil.path.ROOT + "/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHREmployeeByHQL";
		url += "?fields=HREmployee_CName,HREmployee_Id&where=hremployee.IsUse=1";
		
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data.success){
				USER_LIST = (data.value || {}).list || [];
				callback();
			}
		});
	}
	
	//默认查询
	onSearch();
});