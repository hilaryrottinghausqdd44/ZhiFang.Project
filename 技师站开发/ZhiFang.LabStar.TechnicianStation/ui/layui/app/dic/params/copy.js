/**
	@name：复制站点类型
	@author：liangyl
	@version 2021-08-04
 */
layui.extend({
	uxutil: 'ux/util'
}).use(['uxutil','table'], function(){
	var $ = layui.$,
		uxutil = layui.uxutil,
		table = layui.table;
	 //外部参数
	var PARAMS = uxutil.params.get(true);
    //查询个性设置参数
	var GET_PARAITEM_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QuerySystemParaItem?isPlanish=true";
	//保存服务
	var SAVE_URL = uxutil.path.ROOT  + '/ServerWCF/LabStarPreService.svc/LS_UDTO_UpdateParSystemParaItem';
	//接收子窗体数组
	var DATA_LIST = [];
	//当前选择行数据
	var CHECK_ROW_DATA = [];
	//获取个性参数查询字段
	var FILEDS = ['Id','BPara_Id','BPara_ParaNo','ParaValue','DispOrder'];
	//申请单列表实例
	var table_Ind0 = table.render({
		elem: '#copy_table',
		height:'full-45',
		title: '站点类型选择',
		size:'sm',
		initSort:false,
		limit: 500,
		cols:[[
			{field:'Code',title:'Code',width:150,sort:true,hide:true},
			{field:'paraTypeCode',title:'paraTypeCode',width:150,sort:true,hide:true},
		    {field:'Name',title:'站点类型',minWidth:150,flex:1}
		]],
		loading:true,
		page: false,
		text: {none: '暂无相关数据' },
		done: function (res, curr, count) {
			if(count>0){
				setTimeout(function () {
                    $("#copy_table+div .layui-table-body table.layui-table tbody tr:first-child")[0].click();
                }, 0);
		    }else{
		    	CHECK_ROW_DATA = [];
		    }
		}
	});
    
	table.on('row(copy_table)', function(obj){
		CHECK_ROW_DATA = [];
		CHECK_ROW_DATA.push(obj.data);
		//标注选中样式
	    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
	});
    //按钮事件
	var active = {
		save: function() {//
			//查询选择行的站点类型
			getParaItemList(function(list){
				layer.confirm('是否覆盖已设置的参数?',{ icon: 3, title: '提示' }, function(index) {
			        onSave(list);//保存操作
		        });
			});
		},
		close: function() {//关闭
			parent.layer.closeAll('iframe');
		}
	};
	$('.copyselect .layui-btn').on('click', function() {
		var type = $(this).data('type');
		active[type] ? active[type].call(this) : '';
	});
	function onSave(list){
		var entityList = [];
		for(var i in list){
			var obj = {Id:list[i].BParaItem_BPara_Id,DataTimeStamp: [0,0,0,0,0,0,0,0]};
			entityList.push({
				BPara:obj,
				DispOrder:list[i].BParaItem_DispOrder,
				Id:list[i].BParaItem_Id,
				IsUse:1,
				ObjectID:PARAMS.OBJECTID,
				ObjectName:PARAMS.OBJECTNAME,
				ParaNo:list[i].BParaItem_BPara_ParaNo,
				ParaValue:list[i].BParaItem_ParaValue
			});
		}
		//新增实体
		var entity = {
			ObjectID:PARAMS.OBJECTID,
			entityList:entityList
		};
		var params = JSON.stringify(entity);
		//显示遮罩层
		var config = {
			type: "POST",
			url: SAVE_URL,
			data: params
		};
		uxutil.server.ajax(config, function(data) {
			//隐藏遮罩层
			if (data.success) {
				var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
	            parent.layer.close(index); //再执行关闭
			} else {
				layer.msg(data.ErrorInfo, { icon: 5});
			}
		});
	}
	 //获取个性列表
	function getParaItemList(callback){
        if(CHECK_ROW_DATA.length==0){
        	layer.msg('请选择要复制的行',{icon:5});
        	return false;
        }
		var url  = GET_PARAITEM_LIST_URL+'&systemTypeCode=1&paraTypeCode='+CHECK_ROW_DATA[0].paraTypeCode;
		url+='&where=bparaitem.ObjectID='+ CHECK_ROW_DATA[0].Code+'&fields=BParaItem_' + FILEDS.join(',BParaItem_');
		url+="&sort=[{property: 'BParaItem_DispOrder',direction: 'ASC'}]";	

		uxutil.server.ajax({
			url:url
		},function(data){
			var list = (data.value ||{}).list || [];		
			callback(list);
		});
	};
	function init(){
		var DATA_LIST = $('#data-copy-list').val() || '[]';
		DATA_LIST = JSON.parse(DATA_LIST); 
		table_Ind0.reload({data:DATA_LIST});
	}
	//初始化
	init();
});