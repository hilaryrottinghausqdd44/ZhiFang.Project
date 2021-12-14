/**
	@name：复制人员个人站点类型
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
	 //外部参数
	var PARAMS = uxutil.params.get(true);
    //人员ID
    var EMPID = PARAMS.EMPID;
    //复制个人站点类型服务
    var COPY_URL = uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_BHostTypeUserCopy';
	//人员所有数据
	var DATA_LIST = [];
	//人员选择列表实例
	var	table_ind0 = EmpList.render({
		elem:'#emp_copy_table',
    	title:'人员选择',
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
	table_ind0.table.on('row(emp_copy_table)', function(obj){
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
            onCopyClick();
		},
		search: function() {//查询
			var list = DATA_LIST;
			if($('#search-input').val())list = table_ind0.searchList($('#search-input').val(),DATA_LIST);
            table_ind0.instance.reload({data:list});
		},
		close: function() {//关闭
			parent.layer.closeAll('iframe');
		}
	};
	$('.empselect .layui-btn').on('click', function() {
		var type = $(this).data('type');
		active[type] ? active[type].call(this) : '';
	});
	//列表查询
	function onSearch(){		
		table_ind0.loadData({},function(data){
			var list = [];
			for(var i=0;i<data.length;i++){
				if(data[i].Id !=EMPID)list.push(data[i]);
			}
			DATA_LIST = list;
			table_ind0.instance.reload({data:list});
		});
	}
	//获取选中的值
	function getcheckdata(){
		var checkStatus = table_ind0.table.checkStatus('emp_copy_table');
        var data = checkStatus.data;
        var arr = [];
    	//数组去重复
        for (var i = 0; i < data.length; i++) {
        	var istrue = uxutil.string.inArray(data[i].HREmpIdentity_HREmployee_Id ,arr);
        	if(!istrue)arr.push(data[i].HREmpIdentity_HREmployee_Id);
        }
        return arr;
	} 
	//取被复制的人
	function getCheckList(){
		var checkStatus = table_ind0.table.checkStatus('emp_copy_table');
        var data = checkStatus.data;
        var ids ="",arr=[];
        for(var i=0;i<data.length;i++){
        	arr.push(data[i].Id);
        }
        if(arr.length>0)ids = arr.join(',');
        return ids;
	}
	 //复制
	function onCopyClick(){
	    var Copyusers  = getCheckList();//被复制人的id
		//显示遮罩层
		var config1 = {
			type: "POST",
			url: COPY_URL,
			data: JSON.stringify({pasteuser: EMPID,Copyusers:Copyusers})
		};
		var index = layer.load();
		uxutil.server.ajax(config1, function(data) {
			if (data.success) {
			    layer.msg('保存成功!',{icon:6,time:2000});
			    parent.layer.closeAll('iframe');
				parent.afterCopyUpdate(data);
			} else {
				layer.msg(data.ErrorInfo, { icon: 5});
			}				
		});
	}
	function init(){
		onSearch();
	}
	//初始化
	init();
});