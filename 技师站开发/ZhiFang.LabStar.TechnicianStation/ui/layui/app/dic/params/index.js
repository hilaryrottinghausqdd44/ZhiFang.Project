/**
   @Name：参数设置
   @Author：liangyl
   @version 2021-07-01
 */
layui.extend({
	uxutil:'/ux/util',
    uxtable:'/ux/table',
    commonzf:'/modules/common/zf',
	ParameterList:'app/dic/params/list',//分组
	ParameterTypeList:'app/dic/params/typelist',//参数类型
	ParaItemList:'app/dic/params/paraitemlist'
}).use(['uxutil','table','ParameterList','ParameterTypeList','ParaItemList','commonzf'],function(){
	var $ = layui.$,
		uxutil=layui.uxutil,
		ParameterList = layui.ParameterList,
		ParameterTypeList = layui.ParameterTypeList,
		ParaItemList = layui.ParaItemList,
		commonzf = layui.commonzf,
		table = layui.table;
	
	//参数类型实例
	var table_ind0=null;
	//参数实例
	var table_ind1=null;
    //个性设置实例
	var table_ind2=null;
	//默认参数
	//第一個列表選擇行
	var ROW_CHECK_DATA = [];
	//第二個列表選擇行
	var ROW_CHECK_DATA_T = [];
	//新增修改参数保存的id 用于定位行
	var CODE ="";
	//字段数据集
	var COM_DATA_LIST = [];
	 //获取下拉数据集
    var GET_PARA_DIC_URL = uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_GetParaDicData';

	//模块类型
	table_ind0 = ParameterTypeList.render({
		elem:'#table',
    	title:'模块类型',
    	height:'full-33',
    	size: 'sm', //小尺寸的表格
    	done: function(res, curr, count) {
			setTimeout(function(){
				var tr = table_ind0.instance.config.instance.layBody.find('tr:eq(0)');
				if(tr.length > 0){
					tr.click();
				}else{
					ROW_CHECK_DATA = [];
				}
			},0);
		}
	});
	table_ind0.instance.reload({data:[]});
	//
	table_ind1 = ParameterList.render({
		elem:'#para_table',
    	title:'站点类型',
    	height:'full-55',
    	size: 'sm', //小尺寸的表格
    	done: function(res, curr, count) {
			setTimeout(function(){
				var rowIndex = 0;
	            for (var i = 0; i < res.data.length; i++) {
	                if (res.data[i].Code == CODE) {
	              	  rowIndex=i;
	              	  break;
	                }
	            }
				var tr = table_ind1.instance.config.instance.layBody.find('tr:eq('+rowIndex+')');
				if(tr.length > 0){
					tr.click();
				}else{
					ROW_CHECK_DATA_T = [];
				}
			},0);
		}
	});
	table_ind1.instance.reload({data:[]});
	//
	table_ind2 = ParaItemList.render({
		elem:'#paraitem_table',
    	title:'个性参数',
    	height:'full-58',
    	size: 'sm'
	});
	table_ind2.instance.reload({data:[]});
	//类型分组列表
	table_ind0.table.on('row(table)', function(obj){
		//标注选中样式
	    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
		ROW_CHECK_DATA =[];
		ROW_CHECK_DATA.push(obj.data);
		table_ind1.loadData(obj.data.DefaultValue,obj.data.Name,COM_DATA_LIST);
	});
	//类型分组列表
	table_ind1.table.on('row(para_table)', function(obj){
		//标注选中样式
	    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');		
		
		if(obj.data.Code.indexOf("_DefaultPara") == -1){ //默认参数查询时隐藏操作列与参数值列
			$('#edit').removeClass('layui-hide');
			$('#del').removeClass('layui-hide');
			$('#copy').removeClass('layui-hide');
		}else{
			$('#edit').addClass('layui-hide');
			$('#del').addClass('layui-hide');
			$('#copy').addClass('layui-hide');
		}
		ROW_CHECK_DATA_T = [];
		ROW_CHECK_DATA_T.push(obj.data);
		table_ind2.loadData(ROW_CHECK_DATA[0].DefaultValue,obj.data.Code,obj.data.Name,COM_DATA_LIST);
	});
	//删除个性设置
	$('#del').on('click',function(){
		var objectInfo = [{
			ObjectID:ROW_CHECK_DATA_T[0].Code,
			ObjectName:ROW_CHECK_DATA_T[0].Name
		}];
		layer.confirm('确定删除选中项?',{ icon: 3, title: '提示' }, function(index) {
	       table_ind1.onDelParaItemClick(objectInfo,function(){
	       	    delHistoryInfo();
	    		layer.msg("删除成功！", { icon: 6, anim: 0 ,time:2000});
	    		table_ind1.loadData(ROW_CHECK_DATA[0].DefaultValue,ROW_CHECK_DATA[0].Name,COM_DATA_LIST);
	    		
	    	});
        });
	});
	//监听工具条
	table_ind2.table.on('tool(paraitem_table)', function(obj){
	    var data = obj.data;
	    if(obj.event === 'del'){
	    	if(!data.Id)return;
	    	table_ind2.onDelClick(data.Id,function(){
	    	    var list = table.cache['paraitem_table'];
                if(list.length==1 && list[0].Id == data.Id){ //删除默认值
                	var objectInfo = [{
						ObjectID:ROW_CHECK_DATA_T[0].Code,
						ObjectName:ROW_CHECK_DATA_T[0].Name
					}];
                	table_ind1.onDelParaItemClick(objectInfo,function(){
                		table_ind1.loadData(ROW_CHECK_DATA[0].DefaultValue,ROW_CHECK_DATA[0].Name,COM_DATA_LIST);
                	});
                }else{
                	table_ind2.loadData(ROW_CHECK_DATA[0].DefaultValue,ROW_CHECK_DATA_T[0].Code,ROW_CHECK_DATA_T[0].Name,COM_DATA_LIST);
                }
	    	});
	    } else if (obj.event === 'up') { //上移
            table_ind2.move(data.Id, obj.event);
        } else if (obj.event === 'down') { //下移
            table_ind2.move(data.Id, obj.event);
        }
	});
	//修改参数分类
	$('#edit').on('click',function(){
		table_ind1.openWin('修改站点类型','edit',ROW_CHECK_DATA_T[0].Code,ROW_CHECK_DATA_T[0].Name);
	});
	//新增参数分类
	$('#add').on('click',function(){
		table_ind1.openWin('新增站点类型','add',ROW_CHECK_DATA_T[0].Code);
	});
	//复制
	$('#copy').on('click',function(){
		var flag = false;
		layer.open({
            type: 2,
		    area:['400px','320px'],
            fixed: false,
            maxmin: false,
            title:'复制站点类型',
            content: 'copy.html?ObjectID='+ROW_CHECK_DATA_T[0].Code+'&ObjectName='+ROW_CHECK_DATA_T[0].Name+'&t='+ new Date().getTime(),
            success: function(layero, index){
       	        var body = layer.getChildFrame('body', index);//这里是获取打开的窗口元素
       	        var arr = [];
				var list = table_ind1.table.cache['para_table'];
				if(list.length==0)return false;
				for(var i=0;i<list.length;i++){
					if(list[i].Code.indexOf("_DefaultPara") == -1 && list[i].Code!=ROW_CHECK_DATA_T[0].Code ){ //去掉默认参数
						list[i].paraTypeCode = ROW_CHECK_DATA[0].DefaultValue;
						arr.push(list[i]);
		            }
				}
       	        body.find('#data-copy-list').val(JSON.stringify(arr));
	        },
            cancel: function (index, layero) {
                flag = true;
            },
	        end : function() {
	        	if(flag)return;
//	        	table_ind1.loadData(ROW_CHECK_DATA[0].DefaultValue,ROW_CHECK_DATA[0].Name,COM_DATA_LIST);
			    table_ind2.loadData(ROW_CHECK_DATA[0].DefaultValue,ROW_CHECK_DATA_T[0].Code,ROW_CHECK_DATA_T[0].Name,COM_DATA_LIST);

//	        	table_ind.loadData(table_ind.paraTypeCode,table_ind.paraTypeName);
	        }
        });
	});
	//下拉参数初始化
	function initParaDic(callback){
		var arr_list = [];
		commonzf.classdict.init('Pre_ParaDicType',function(){
			var list=commonzf.classdict.Pre_ParaDicType;
			for(var i=0;i<list.length;i++){
				var code = list[i].Code;
				getParaDicByCode(code,function(data){
					arr_list.push({
						Code:code,
						List:data
					});
				});
		    }
			callback(arr_list);
		}); 
	}
	//删除模块站点类型记录{HostTypeID:'',HostTypeName:''}
	function delHistoryInfo(){
		var me = this,
			empId = uxutil.cookie.get(uxutil.cookie.map.USERID);
		//删除站点类型时把记录也删除	
		var HistoryInfo  = 'PreSampleBarcodeBasicHostType_' + ROW_CHECK_DATA[0].DefaultValue + empId;
		if(uxutil.localStorage.get(HistoryInfo)){
			uxutil.localStorage.remove(HistoryInfo);
		}
	};
	
	//获取下拉框数据集加载方法
	function getParaDicByCode(code,callback){
		var url = GET_PARA_DIC_URL +'?type='+code;
		uxutil.server.ajax({
			url:url,
			async:false
		},function(data){
			var list = data.value || [];
			callback(list);
		});
	}
	//新增设置成功后回调
	function afterUpdate(id){
    	CODE = id;
    }
	window.afterUpdate = afterUpdate;
	function init(){
		//下拉数据集加载
		initParaDic(function(data){
			COM_DATA_LIST = data;		
		});
		table_ind0.loadData();
	}
	//初始化
	init();
});