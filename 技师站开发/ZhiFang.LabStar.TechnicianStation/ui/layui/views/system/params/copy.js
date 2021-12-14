layui.extend({
	uxutil:'/ux/util',
	uxtable:'ux/table'
}).use(['form','uxutil','uxtable'],function(){
	var $ = layui.$,
		form = layui.form,
		uxutil = layui.uxutil,
		uxtable = layui.uxtable;
		
	var params = uxutil.params.get(true);
	//查询个性设置信息列表
	var SELECT_INFO_URL= uxutil.path.ROOT +'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QueryParaSystemTypeInfo';
    //复制系统个性参数设置
	var ADD_COPY_URL= uxutil.path.ROOT +'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddCopySystemParaItem';
    //列表选中行
    var CHECKROWDARA = [];
    //参数相关性
    var SYSTEMTYPECODE =  params.SYSTEMTYPECODE;
    
    var PARATYPECODE =  params.PARATYPECODE;
    //类型编码(站点类型)
    var OBJECTID = params.OBJECTID;
     //类型编码名称(站点类型)
    var OBJECTNAME =params.OBJECTNAME;
    //个性类型列表实例
	var table0_Ind = uxtable.render({
		elem:'#table',
		height:'full-80',
		title:'个性列表',
		cols: [[
			{field:'BParaItem_ObjectID',title:'ID',width:150,sort:false,hide:true},
			{field:'BParaItem_ObjectName',title:'名称',minWidth:120,flex:1,sort:false},			
		]],
		loading:false,
		done:function(res,curr,count){
			if(count==0)CHECKROWDARA=[];
			setTimeout(function(){
				var tr = table0_Ind.instance.config.instance.layBody.find('tr:eq(0)');
				if(tr.length > 0){
					tr.click();
				}
			},0);
		}
	});
	//列表查询
	function onSearch0(systemTypeCode){
		var me = this;
		
		table0_Ind.instance.reload({data:[]});//清空列表数据
		
		var index = layer.load();
		//获取列表
		onLoadData(function(data){
			layer.close(index);
			if(data.success){
				var list = data.value || [];
				var arr = [];
				for(var i=0;i<list.length;i++){
					var obj={
						BParaItem_ObjectID:list[i][0],
						BParaItem_ObjectName:list[i][1]
					};
					arr.push(obj);
				}
				table0_Ind.instance.reload({data:arr});
			}else{
				table0_Ind.instance.config.instance.layMain.html('<div class="layui-none">' + data.msg + '</div>');
			}
		});
	}
	
    function onLoadData(callback){
		var url  = SELECT_INFO_URL+'?systemTypeCode='+SYSTEMTYPECODE+'&paraTypeCode=' + PARATYPECODE;

		uxutil.server.ajax({
			url:url
		},function(data){
			callback(data);
		});
	};
	
    table0_Ind.table.on('row(table)', function(obj){
    	CHECKROWDARA=[];
    	CHECKROWDARA.push(obj.data);
		//标注选中样式
	    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
	});
	//保存
    $('#save').on('click',function(){     
    	if(CHECKROWDARA.length==0){
			layer.msg('请选择列表数据行');
			return;
		}
		var params={
			fromObjectID:CHECKROWDARA[0].BParaItem_ObjectID,//源个性实体ID，例如：从生化检验小组复制参数
			toObjectID:OBJECTID,
			toObjectName:OBJECTNAME
		};
		if (!params) return;
		params = JSON.stringify(params);
	
		var config = {
			type: "POST",
			url:ADD_COPY_URL,
			data: params
		};
		var index = layer.load();
		uxutil.server.ajax(config, function(data) {
			layer.close(index);
			if (data.success) {
				layer.msg("保存成功",{icon:6,time:2000});
                parent.layer.closeAll('iframe');
				parent.afterCopyUpdate(data);
			} else {
				layer.msg(data.msg,{ icon: 5, anim: 6 });
			}
		});
    });
    //取消
    $('#cancel').on('click',function(){     	
    	parent.layer.closeAll('iframe');
    });
  
	//初始化
	function init(){
	    onSearch0();
	};
	init();
});