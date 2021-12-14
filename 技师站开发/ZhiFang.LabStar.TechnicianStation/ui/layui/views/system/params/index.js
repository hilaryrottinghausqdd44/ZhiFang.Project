/**
	@name：参数
	@author：liangyl
	@version 2020-02-04
 */
layui.extend({
	uxutil:'ux/util',
	uxtable:'ux/table',
    paraform:'views/system/params/form',
    factoryform:'views/system/params/factory',
    defalutform:'views/system/params/defalut',
    personalityform:'views/system/params/personality',
}).use(['uxutil','uxtable','element','factoryform','defalutform','personalityform','table'], function(){
	var $ = layui.$,
		uxutil = layui.uxutil,
		element = layui.element,
		paraform = layui.paraform,
		factoryform=layui.factoryform,
		defalutform = layui.defalutform,
		personalityform = layui.personalityform,
		table = layui.table,
		uxtable = layui.uxtable;

	//类型字典编码
	var TYPE_ENUM_CODE ="Para_MoudleType";
	//类型分组字典编码
	var TYPE_GROUP_ENUM_CODE ="Para_QCType";
	//获取所有枚举
	var GET_TYPE_LIST_URL = '/ServerWCF/CommonService.svc/GetClassDic';
    //当前类型列表选择行
    var TYPE_ROW_TR="";
    
	//当前激活的页签,默认默认设置页签
	var CURRTABINDEX=1;
	//当前分组列表选择行
	var GROUPROWDATA=[];

    //参数数据
	var MOUDLETYPEDATA= []; 
		
	//获取个性设置服务
	var GET_BPARAITEM_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QueryParaSystemTypeInfo?isPlanish=true';
    //删除个性参数
    var DEL_URL =uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DeleteSystemParaItem';
    //相关性
	var SYSTEMTYPECODE="";
	//分组类型Code
	var PARATYPECODE = "";
	//默认设置数组
	var DEFALUTLIST=[];
	//已添加的个性设置
	var BPARAITEMLIST=[];
	//参数查找,根据参数名称查找
	var SELCT_PARANAME_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QueryParaInfoByParaName?isPlanish=true';
	//类型列表实例
	var table0_Ind = uxtable.render({
		elem:'#moudeltype_table',
		height:'full-58',
		title:'系统列表',
		initSort:{field:'Id',type:'asc'},
		size: 'sm', //小尺寸的表格
		cols: [[
			{field:'Id',title:'ID',width:150,sort:true,hide:true},				
			{field:'Code',title:'Code',width:150,sort:true,hide:true},
			{field:'Name',title:'参数名称',minWidth:150,sort:true,flex:1}
		]],
		loading:false,
		done:function(res,curr,count){
			if(count==0)TYPE_ROW_TR="";
			setTimeout(function(){
				var tr = table0_Ind.instance.config.instance.layBody.find('tr:eq(0)');
				if(tr.length > 0){
					tr.click();
				}
			},0);
		}
	});

    //类型列表查询
	function onSearch0(){
		
		table1_Ind.instance.reload({data:[]});//清空分组列表数据
		form1_Ind.clearData('#ContentDiv');//清空表单面板数据
		form2_Ind.clearData('#ContentDiv2');//清空表单面板数据
		form3_Ind.clearData('#ContentDiv3');//清空表单面板数据
		var index = layer.load();
		//获取类型列表
		onLoadTypeList(TYPE_ENUM_CODE,function(data){
			layer.close(index);
			if(data.success){
				var list = data.value || [];
				MOUDLETYPEDATA = list;
				table0_Ind.instance.reload({data:list});
			}else{
				table0_Ind.instance.config.instance.layMain.html('<div class="layui-none">' + data.msg + '</div>');
				GROUPROWDATA =[];//清空分组列表选择行数据
				form1_Ind.clearData('#ContentDiv');//清空表单面板数据
				form2_Ind.clearData('#ContentDiv2');//清空表单面板数据
				form3_Ind.clearData('#ContentDiv3');//清空表单面板数据
				table2_Ind.instance.reload({data:[]});
			}
		});
	};
	
	table0_Ind.table.on('row(moudeltype_table)', function(obj){
		TYPE_ROW_TR = obj;
		//标注选中样式
	    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
		onSearch1(obj.data);
	});

	//类型组列表实例
	var table1_Ind = uxtable.render({
		elem:'#group_table',
		height:'full-82',
		title:'参数组列表',
		initSort:{field:'Id',type:'asc'},
		size: 'sm', //小尺寸的表格
		cols: [[
			{field:'Id',title:'ID',width:150,sort:true,hide:true},				
			{field:'Code',title:'Code',width:150,sort:true,hide:true},
			{field:'Name',title:'参数组',minWidth:150,sort:true,flex:1}
		]],
		loading:false,
		done:function(res,curr,count){
			if(count==0){
				GROUPROWDATA =[];//清空分组列表选择行数据
				form1_Ind.clearData('#ContentDiv');//清空表单面板数据
				form2_Ind.clearData('#ContentDiv2');//清空表单面板数据
				form3_Ind.clearData('#ContentDiv3');//清空表单面板数据
			}
			setTimeout(function(){
				var tr = table1_Ind.instance.config.instance.layBody.find('tr:eq(0)');
				if(tr.length > 0){
					tr.click();
				}
			},0);
		}
	});
	 //类型组列表查询
	function onSearch1(obj){
		var index = layer.load();
		//获取类型组列表
		onLoadTypeList(obj.Code,function(data){
			layer.close(index);
			if(data.success){
				var list = data.value || [];
				table1_Ind.instance.reload({data:list});
			}else{
				GROUPROWDATA =[];//清空分组列表选择行数据
				form1_Ind.clearData('#ContentDiv');//清空表单面板数据
				form2_Ind.clearData('#ContentDiv2');//清空表单面板数据
				form3_Ind.clearData('#ContentDiv3');//清空表单面板数据
				table2_Ind.instance.reload({data:[]});
				table1_Ind.instance.config.instance.layMain.html('<div class="layui-none">暂时未定义参数类型</div>');
			}
		});
	};
	//类型分组列表
	table1_Ind.table.on('row(group_table)', function(obj){
		//标注选中样式
	    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
		GROUPROWDATA =[];
		GROUPROWDATA.push(obj.data);
		SYSTEMTYPECODE = obj.data.DefaultValue;
		PARATYPECODE = obj.data.Code;
		onSearchTab(obj.data.Code,obj.data.DefaultValue);
	});
	
	//获取类型列表
	function onLoadTypeList(classname,callback){
		var url  = uxutil.path.ROOT + GET_TYPE_LIST_URL;
		url += '?classnamespace=ZhiFang.Entity.LabStar&classname=' + classname;

		uxutil.server.ajax({
			url:url
		},function(data){
			callback(data);
		});
	};
	
	//根据参数名称查询参数分类
	function getParaInfoByName(ParaName,callback){
		var url  = SELCT_PARANAME_URL;
		url += '&paraName='+ParaName+'&fields=BaseClassDicEntity_Name,BaseClassDicEntity_Id,BaseClassDicEntity_Code';

		uxutil.server.ajax({
			url:url
		},function(data){
			callback(data);
		});
	};
	
    var table2_Ind = uxtable.render({
		elem:'#personality_table',
		height:'full-175',
		title:'个性列表',
		size: 'sm', //小尺寸的表格
		initSort:{field:'BParaItem_ObjectName',type:'asc'},
		cols: [[
		    {type:'checkbox', fixed: 'left'},
			{field:'BParaItem_ObjectID',title:'ID',width:150,sort:false,hide:true},
			{field:'BParaItem_ObjectName',title:'名称',minWidth:150,flex:1,sort:false},			
            {fixed: 'right', title:'操作', toolbar: '#barDemo',width:64}
		]],
		loading:false,
		done:function(res,curr,count){
			if(count==0)form3_Ind.clearListData();
			else  
			   doAutoSelect(this,0);
		}
	});
	table2_Ind.table.on('row(personality_table)', function(obj){
		obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
		form3_Ind.onSearch(obj.data.BParaItem_ObjectID,obj.data.BParaItem_ObjectName);
	});
    //事件监听
	table2_Ind.table.on('tool(personality_table)', function(obj){
		switch(obj.event){
			case 'del'://删除
			var objectInfo = [{
				ObjectID:obj.data.BParaItem_ObjectID,
				ObjectName:obj.data.BParaItem_ObjectName,
			}];
			if (objectInfo.length==0) return;
    	
	    	layer.confirm('确定删除选中项?',{ icon: 3, title: '提示' }, function(index) {
				var params = JSON.stringify({objectInfo:JSON.stringify(objectInfo)});
				var config = {
					type: "POST",
					url:DEL_URL,
					data: params
				};
				var index = layer.load();
				uxutil.server.ajax(config, function(data) {
					layer.close(index);
					if (data.success) {
						layer.msg("删除成功",{icon:6,time:2000});
		                onSearch2(SYSTEMTYPECODE,PARATYPECODE);
					} else {
						layer.msg(data.msg,{ icon: 5, anim: 6 });
					}
				});
			});
			break;
		}
	});
	 //个性设置列表查询
	function onSearch2(systemTypeCode,paraTypeCode){
		var index = layer.load();
		//获取类型组列表
		onLoadPersonalityList(systemTypeCode,paraTypeCode,function(data){
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
				//已添加的个性设置
	            BPARAITEMLIST = arr;
				table2_Ind.instance.reload({data:arr});
			}else{
				table2_Ind.instance.config.instance.layMain.html('<div class="layui-none">暂时未定义参数类型</div>');
			}
		});
	};
	//获取个性设置列表
	function onLoadPersonalityList(systemTypeCode,paraTypeCode,callback){
		var url = GET_BPARAITEM_LIST_URL+'&systemTypeCode='+systemTypeCode+'&paraTypeCode=' + paraTypeCode;

		uxutil.server.ajax({
			url:url
		},function(data){
			callback(data);
		});
	}
	
	//出厂设置
	var form1_Ind  = factoryform.render({});
	//默认设置
	var form2_Ind  = defalutform.render({});
	//个性设置
	var form3_Ind  = personalityform.render({});
	
	function onSearchTab(Code,systemTypeCode){		
		//查询当前页签
		switch (CURRTABINDEX){
			case 0://出厂设置
			    form1_Ind.loadData(Code);
				break;
			case 1://默认设置
			    form2_Ind.loadData(Code);
				break;
			default: //个性设置
			    table2_Ind.instance.reload({data:[]});
			    form3_Ind.loadDefault(systemTypeCode,Code,function(list){
			    	DEFALUTLIST = list;
			    	 //高度
			        $("#Personality").css("height", ($(window).height() - 125) + "px");//设置表单容器高度
			        if($('.cardHeight').width()<570){
			        	$("#Personality").css("width", '500px');
			        }else{
				    	$("#Personality").css("width", $('.cardHeight').width()-200+ "px");
				    }
			        
			    	if(list.length>0)onSearch2(systemTypeCode,Code);
				    
				});
				break;
		}
	}
	//新增设置
	$('#add').on('click',function(){
		layer.open({
            type: 2,
            area: ['400px', '380px'],
            fixed: false,
            maxmin: false,
            title:'新增设置',
            content: 'add.html?systemTypeCode='+SYSTEMTYPECODE,
            cancel: function (index, layero) {
	        	parent.layer.closeAll('iframe');
            }
        });
	});
    //批量删除
	$('#batchdel').on('click',function(){
		
		var checkStatus =table2_Ind.table.checkStatus('personality_table'),
	        data = checkStatus.data;

	    var objectInfo =[];
	    for(var i=0;i<data.length;i++){
	    	objectInfo.push({
	    		ObjectID:data[i].BParaItem_ObjectID,
				ObjectName:data[i].BParaItem_ObjectName,
	    	});
	    }
	    if (objectInfo.length==0) {
	    	layer.msg('请勾选需要删除的数据行');
	    	return;
	    }
    	layer.confirm('确定删除选中项?',{ icon: 3, title: '提示' }, function(index) {
			var params = JSON.stringify({objectInfo:JSON.stringify(objectInfo)});
			var config = {
				type: "POST",
				url:DEL_URL,
				data: params
			};
			var index = layer.load();
			uxutil.server.ajax(config, function(data) {
				layer.close(index);
				if (data.success) {
					layer.msg("删除成功",{icon:6,time:2000});
	                 onSearch2(SYSTEMTYPECODE,PARATYPECODE);
				} else {
					layer.msg(data.msg,{ icon: 5, anim: 6 });
				}
			});
		});
	});
	element.on('tab(tabs)', function(obj){
		CURRTABINDEX = obj.index;
		if(GROUPROWDATA.length>0 && !GROUPROWDATA[0].Code )return;
        onSearchTab(GROUPROWDATA[0].Code,GROUPROWDATA[0].DefaultValue);
    });
    
	 //参数查找监听查询
	$('#search').on('click',function(){ 
		var searchText = document.getElementById('searchText').value;
		if(searchText)searchText = searchText.toUpperCase();
		getParaInfoByName(searchText,function(data){
			var list = data.value.list || [];
			if(list.length==0)return;
			var arr = [];
			table1_Ind.instance.reload({data:[]});
			for(var i=0;i<list.length;i++){
			    var obj ={
			    	Id:list[i].BaseClassDicEntity_Id,
			    	Code:list[i].BaseClassDicEntity_Code,
			    	Name:list[i].BaseClassDicEntity_Name
			    }
			    arr.push(obj);
			}
			table1_Ind.instance.reload({data:arr});
			//取消分类列表数据选择行
			TYPE_ROW_TR.tr.removeClass('layui-table-click');
		});
	});
	/***默认选择行
	 * @description 默认选中并触发行单击处理 
	 * @param that:当前操作实例对象
	 * @param rowIndex: 指定选中的行
	 * */
	function doAutoSelect(that, rowIndex) {
		var me = this;	
		var data = table.cache[that.instance.key] || [];
		if (!data || data.length <= 0) return;

		rowIndex = rowIndex || 0;
		var filter = that.elem.attr('lay-filter');
		var thatrow = $(that.instance.layBody[0]).find('tr:eq(' + rowIndex + ')');
		var obj = {
			tr: thatrow,
			data: data[rowIndex] || {},
			del: function() {
				table.cache[that.instance.key][index] = [];
				tr.remove();
				that.instance.scrollPatch();
			},
			updte: {}
		};
		setTimeout(function() {
			layui.event.call(thatrow, 'table', 'row' + '(' + filter + ')', obj);
		}, 100);
	}
	//传递参数给子窗体默认设置值
    function afterUpdate1(){
		return DEFALUTLIST;
    }
    //从父窗体传递给子窗体个性设置列表值
    function afterUpdate2(){
		return BPARAITEMLIST;
    }
    //新增设置成功后回调
	function afterUpdate(data){
    	if(data.success)onSearch2(SYSTEMTYPECODE,PARATYPECODE); 
    }
	//初始化
    function init(){
    	 //tab高度
        $(".cardHeight").css("height", ($(window).height() - 52) + "px");//设置表单容器高度
       
        // 窗体大小改变时，调整高度显示
    	$(window).resize(function() {
			 //表单高度
		    $(".cardHeight").css("height", ($(window).height() - 52) + "px");//设置表单容器高度
		    $("#Personality").css("height", ($(window).height() - 125) + "px");//设置表单容器高度
		    if($('.cardHeight').width()<570){
		    	$("#Personality").css("width", '500px');
		    }else{
		    	$("#Personality").css("width", $('.cardHeight').width()-200+ "px");
		    }
    	});
        onSearch0();
    }
    //初始化
    init();
    window.afterUpdate1 = afterUpdate1;
    window.afterUpdate2 = afterUpdate2;
	window.afterUpdate = afterUpdate;
});