/**
	@name：新增站点类型与人员关系（选多个人）
	@author：liangyl
	@version 2021-08-04
 */
layui.extend({
	uxutil: 'ux/util',
	EmpList:'app/dic/hosttypeuser/emplist'
}).use(['uxutil','EmpList'], function(){
	var $ = layui.$,
		uxutil = layui.uxutil,
		EmpList = layui.EmpList;
		
	//新增数据服务
	var ADD_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddBHostTypeUser';
     //外部参数
	var PARAMS = uxutil.params.get(true);
    //站点类型ID
    var HOSTTYPEID = PARAMS.HOSTTYPEID;
    //已选数据，需剔除
    var DEFAULT_DATA = parent.ChildEmpData();
    //列表数据
    var DATA_LIST = [];
    //保存参数
    var saveErrorCount = 0,
		saveCount = 0,
		saveLength = 0;
		
	//人员选择列表实例
	var	table_ind0 = EmpList.render({
		elem:'#emp_link_table',
    	title:'模块类型',
    	height:'full-65',
    	size: 'sm', //小尺寸的表格
 
    	done: function(res, curr, count) {
			setTimeout(function(){
				var tr = table_ind0.instance.config.instance.layBody.find('tr:eq(0)');
				if(tr.length > 0){
					tr.click();
				}
			},0);
		}
	});
	table_ind0.instance.reload({data:[]});
	//模块类型列表
	table_ind0.table.on('row(emp_link_table)', function(obj){
		//标注选中样式
	    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
	});
	//按钮事件
	var active = {
		save: function() {//新增关系、
		    var list = getcheckdata();
            if(list.length ==0){ 
            	layer.msg('请先选择数据行');
            	return false;
            }
            onSave(list);//保存操作
		},
		search: function() {//查询
			var list = DATA_LIST;
			if($('#search-input').val()){
				list = table_ind0.searchList($('#search-input').val(),DATA_LIST);
			}
            table_ind0.instance.reload({data:list});
		},
		close: function() {//关闭
			parent.layer.closeAll('iframe');
		}
	};
	   //回车事件
    $("#search-input").on('keydown', function (event) {
        if (event.keyCode == 13) {
        	var list = DATA_LIST;
			if($('#search-input').val()){
				list = table_ind0.searchList($('#search-input').val(),DATA_LIST);
			}
            table_ind0.instance.reload({data:list});
            return false;
        }
    });
	$('.addlink .layui-btn').on('click', function() {
		var type = $(this).data('type');
		active[type] ? active[type].call(this) : '';
	});
	//列表查询
	function onSearch(){		
		table_ind0.loadData({},function(data){
			var list  = resultdata(data);
			DATA_LIST = list;
			table_ind0.instance.reload({data:list});
		});
	}
	//获取选中的值
	function getcheckdata(){
		var checkStatus = table_ind0.table.checkStatus('emp_link_table');
        var data = checkStatus.data;
        return data;
	}
	//单个新增
	function AddLink(entity){
		//显示遮罩层
		var config1 = {
			type: "POST",
			url: ADD_URL,
			data: JSON.stringify({entity: entity})
		};
		var index = layer.load();
		uxutil.server.ajax(config1, function(data) {
			if (data.success) {
			    saveCount++;
			} else {
				saveErrorCount++;
			}				
			if ( saveCount +  saveErrorCount ==  saveLength) {
				if ( saveErrorCount == 0){
					layer.msg('保存成功!',{icon:6,time:2000});
					parent.layer.closeAll('iframe');
					parent.afterUpdate1(data);
				}else{
					layer.msg(data.ErrorInfo, { icon: 5});
				}
			}
		});
	}
	//保存
	function onSave(arr){
		saveErrorCount = 0;
		saveCount = 0;
		saveLength = arr.length;
    	for (var i = 0; i < arr.length; i++) {
            var entity = {
                EmpID: arr[i].Id,
                HostTypeID: HOSTTYPEID,
                IsUse:1
            }
            AddLink(entity);
        }
	}
	function resultdata(list){
		var arr = [],isExec=true;
		if(DEFAULT_DATA.length>0){ //剔除已选的人员
			for(var i=0;i<list.length;i++){
				isExec=true;
				for(var j=0;j<DEFAULT_DATA.length;j++){
					if(list[i].Id == DEFAULT_DATA[j].BHostTypeUser_EmpID ){
						isExec = false;
						break;
					}
				}
				if(isExec)arr.push(list[i]);
			}
		}
		if(DEFAULT_DATA.length==0)arr =list;
		return arr;
	}
	function init(){
		onSearch();
	}
	//初始化 
	init();
//	//初始化数据
//	window.initData = function(data,afterSave){
//		if(typeof afterSave == 'function'){
//			AFTER_SAVE = afterSave;
//		}
//				//默认数据
//		DEFAULT_DATA = data;
//		
//
//	};
});