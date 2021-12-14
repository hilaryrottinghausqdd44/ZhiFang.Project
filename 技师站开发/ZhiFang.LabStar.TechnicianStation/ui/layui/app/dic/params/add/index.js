/**
	@name：选择取单分类项目
	@author：liangyl
	@version 2021-06-02
 */
layui.extend({
	uxutil: 'ux/util',
	tableSelect: '../src/tableSelect/tableSelect',
	ListForm:'app/dic/params/form'//列表嵌入的表单
}).use(['uxutil','form','table','tableSelect','ListForm'],function(){
	var $ = layui.$,
		uxutil = layui.uxutil,
		table = layui.table,
		tableSelect = layui.tableSelect,
		ListForm = layui.ListForm,
		form = layui.form;
		
	 //外部参数
	var PARAMS = uxutil.params.get(true);
	//PARATYPENAME 
	var PARATYPENAME = PARAMS.PARATYPENAME;
	//PARATYPECODE 
	var PARATYPECODE = PARAMS.PARATYPECODE;
	//DEFAULTPARATYPECODE
	var DEFAULTPARATYPECODE = PARAMS.DEFAULTPARATYPECODE;
	var HOSTTYPENAME= PARAMS.NAME;
	//状态（编辑或者新增）
	var TYPE  =  PARAMS.TYPE;

	//获取默认参数列表数据
	var GET_DEFAULT_PARA_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QuerySystemDefaultPara?isPlanish=true';
	//获取个性参数列表数据
	var GET_PARAITEM_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QuerySystemParaItem?isPlanish=true';
	//查询站点类型
	var GET_HOSTTYPE_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_SearchBHostTypeNotPara';
	//保存服务
	var SAVE_URL = uxutil.path.ROOT  + '/ServerWCF/LabStarPreService.svc/LS_UDTO_UpdateParSystemParaItem';
    	//新增数据服务
	var ADD_INFO_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddBHostType';
	//修改数据服务
	var EDIT_INFO_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateBHostTypeByField';
    //获取指定实体字段的最大号
    var GET_MAXNO_URL =  uxutil.path.ROOT + '/ServerWCF/LabStarCommonService.svc/GetMaxNoByEntityField?entityName=BHostType&entityField=DispOrder';
    //获取列表服务
    var GET_INFO_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchBHostTypeById?isPlanish=true';

    //当前左列表选择行
    var LEFT_SELECT_DATA =[];
    //当前右列表选择行
    var RIGHT_SELECT_DATA =[];
    //是否第一次加载
    var IS_LOAD = true;
    //左列表 原始数据（刷新后最初的数据）
    var OLD_LEFT_DATA = [];
    //下拉数据
    var COM_DATA_LIST = parent.childComDataUpate(); 
    //
    ListForm.SELECT_DATA_LIST  =  COM_DATA_LIST;
     //右列表 数据
    var RIGHT_DATA = [];
	//左列表实例
	var table_Ind0 = table.render({
		elem: '#lefttable',
		height:'full-75',
		title: '左列表',
		size:'sm',
		initSort:false,
		limit: 5000,
		cols: [[
			{type: 'checkbox',fixed: 'left'},
			{type: 'numbers',title: '行号',fixed: 'left'},
			{field:'BParaItem_Id',title:'ID',width:150,hide:true},
	        {field:'BParaItem_BPara_Id',title:'参数ID',width:150,hide:true},
			{field:'BParaItem_ParaNo',title:'参数编号',width:100,hide:true},
			{field:'BParaItem_BPara_CName',title:'参数名称',flex:1,minWidth:100},
			{field:'BParaItem_ParaValue',title:'默认值',flex:1,minWidth:100,
			templet: function (data) {
				//显示还原
				var ParaValue = data.BParaItem_ParaValue;
				var ParaEditInfo = data.BParaItem_BPara_ParaEditInfo;
				var str = createEditInfo(data,ParaValue,ParaEditInfo);
                return str;
            }},
			{field:'BParaItem_BPara_TypeCode',title:'TypeCode',width:100,hide:true},
			{field:'BParaItem_BPara_ParaType',title:'ParaType',width:100,hide:true},
			{field:'BParaItem_BPara_ParaDesc',title:'ParaDesc',width:100,hide:true},
			{field:'BParaItem_BPara_ParaEditInfo',title:'ParaEditInfo',width:100,hide:true},
			{field:'BParaItem_DispOrder',title:'DispOrder',width:100,hide:true}
		]],
		defaultOrderBy: JSON.stringify([{property: 'BParaItem_DispOrder',direction: 'ASC'}]),
		text: {none: '暂无相关数据' },
		loading:true,
		page: false,
		parseData: function(res){ //res 即为原始返回的数据
			if(!res) return;
			var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
			if(IS_LOAD)OLD_LEFT_DATA = data.list || [];
			return {
				"code": res.success ? 0 : 1, //解析接口状态
				"msg": res.ErrorInfo, //解析提示文本
				"count": data.count || 0, //解析数据长度
				"data": data.list || []
			};
		}
	});

    table_Ind0.reload({data:[]});
	//右列表实例
	var table_Ind1 = table.render({
		elem: '#righttable',
		height:'full-95',
		title: '右列表实例',
		size:'sm',
		initSort:false,
		toolbar: '#toolbarDemo', //开启头部工具栏，并为其绑定左侧模板
		defaultToolbar:[],
		cols: [[
		    {type: 'checkbox',fixed: 'left'},
		    {type: 'numbers',title: '行号',fixed: 'left'},
		   	{field:'BPara_Id',title:'ID',width:150,hide:true},
			{field:'BPara_ParaNo',title:'参数编号',width:100,hide:true},
			{field:'BPara_CName',title:'参数名称',flex:1,minWidth:100},
			{field:'BPara_ParaValue',title:'默认值',flex:1,minWidth:100,templet: function (data) {
				//显示还原
				var ParaValue = data.BPara_ParaValue;
				var ParaEditInfo = data.BPara_ParaEditInfo;
				var str = createEditInfo(data,ParaValue,ParaEditInfo); 
                return str;
            }},
			{field:'BPara_TypeCode',title:'TypeCode',width:100,hide:true},
			{field:'BPara_ParaType',title:'ParaType',width:100,hide:true},
			{field:'BPara_ParaDesc',title:'ParaDesc',width:100,hide:true},
			{field:'BPara_ParaEditInfo',title:'ParaEditInfo',width:100,hide:true},
			{field:'BPara_DispOrder',title:'DispOrder',width:100,hide:true},
			{field:'BParaItem_Id',title:'ID',width:150,hide:true},
			{field:'ParaNo',title:'参数编号,供查询使用,只保留数字',width:100,hide:true}
		]],
		limit: 5000,
		loading:true,
		page: false,
		defaultOrderBy: JSON.stringify([{property: 'BPara_DispOrder',direction: 'ASC'}]),
	    parseData: function(res){ //res 即为原始返回的数据
			if(!res) return;
			var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
			var list =[];
			if(data.list && data.list.length>0)list=resultData(data.list);
			return {
				"code": res.success ? 0 : 1, //解析接口状态
				"msg": res.ErrorInfo, //解析提示文本
				"count": data.count || 0, //解析数据长度
				"data": list || []
			};
		},
		text: {none: '暂无相关数据' }
	});
	table_Ind1.reload({data:[]});
	//监听行双击事件
    table.on('rowDouble(lefttable)', function (obj) {
    	var tableBox = $(this).parents('.layui-table-box');
    	var index = $(this).attr('data-index');
        var tableDiv = tableBox.find(".layui-table-body.layui-table-main");
    	var checkCell = tableDiv.find("tr[data-index=" + index + "]").find("td div.laytable-cell-checkbox div.layui-form-checkbox I");
	    if (checkCell.length>0)checkCell.click();
        changeData('right');
    });
    //选中状态
    table.on('checkbox(lefttable)', function(obj){
        var leftcheckStatus = table.checkStatus('lefttable');
        LEFT_SELECT_DATA = leftcheckStatus.data;
    });
	    
    //右列表监听
    //监听行双击事件
    table.on('rowDouble(righttable)', function (obj) {
        var tableBox = $(this).parents('.layui-table-box');
    	var index = $(this).attr('data-index');
        var tableDiv = tableBox.find(".layui-table-body.layui-table-main");
    	var checkCell = tableDiv.find("tr[data-index=" + index + "]").find("td div.laytable-cell-checkbox div.layui-form-checkbox I");
	    if (checkCell.length>0)checkCell.click();
        changeData('left');
    });
    //选中状态
    table.on('checkbox(righttable)', function(obj){
        var rightcheckStatus = table.checkStatus('righttable');
        RIGHT_SELECT_DATA = rightcheckStatus.data;
    });
    // 按钮事件
	//向左移动
	$('#add').on('click',function(){
		changeData('left');
	});
	//向右移动
	$('#remove').on('click',function(){
		changeData('right');
	});
	//重置
	$('#reset').on('click',function(){
        loadData0();
        loadData1();
	});
	//保存
	$('#save').on('click',function(){
		onSaveClick();
	});
	//下拉框 -- icon 前存在icon 则点击该icon 等同于点击input
    $("input.layui-input+.layui-icon").on('click', function () {
        if (!$(this).hasClass("myDate")) {
            (this).prev('input.layui-input')[0].click();
            return false;//不加的话 不能弹出
        }
    });
    //头工具栏事件
	table.on('toolbar(righttable)', function(obj){
	    var checkStatus = table.checkStatus(obj.config.id);
	    switch(obj.event){
	      case 'search':
	         var list = searchText($('#search_Text_add').val());
			 table_Ind1.reload({data:list});
	      break;
	    };
	  });
	//数据还原
	function createEditInfo(d,ParaValue,ParaEditInfo){
		var value = '';
		switch (ListForm.getComType(ParaEditInfo)){
			case 'E':
    			var arr = [
					'<div style="color:#FF5722;">否</div>',
					'<div style="color:#009688;">是</div>'
				];
				value = ParaValue == '1' ? arr[1] : arr[0];
				break;  
			case 'CL':
                if(ParaValue)value = ListForm.setDisplayName(ParaValue,ListForm.getClList(ParaEditInfo));
                break;
			case 'DB':
                if(ParaValue)value = ListForm.setDisplayName(ParaValue,ListForm.getSelectList(ParaEditInfo));
                break;
            case 'SH':
                if(ParaValue){
            	    var sh_data = ListForm.getSelectList(ParaEditInfo);
			        var cllist = ListForm.getSHType(ParaEditInfo); //sh下拉数据
			        value = ListForm.setSHValue(ParaValue,sh_data,cllist);
                }
                break;
            case 'BH':
                if(ParaValue){
            	    var bh_data = ListForm.getSelectList(ParaEditInfo);
				    value = ListForm.setBHValue(ParaValue,bh_data);
                }
                break;
			default: //默认为文本框C
		     	value = ParaValue;
				break;
		} 
		return value;
	}
	
	//右列表 剔除已选左列表数据
	function resultData(data){
		//左列表现有数据
    	var leftData = table.cache['lefttable'];
    	for(var j=0;j<leftdata.length;j++){
			for(var i=0;i<data.length;i++){
				if(data[i].BPara_ParaNo == leftData[j].BParaItem_ParaNo){
					data.splice(i,1); //删除下标为i的元素
					break;
				}
			}
		}
    	return data;
	}
    //type向那边添加数据
    function changeData(type){
    	//左列表现有数据
    	var leftData = table.cache['lefttable'];
        var rightData =  table.cache['righttable'];
    	if(type=='left'){//向左移动数据
		   var obj = addLeft(leftData,rightData);
    	}
    	if(type=='right'){//向右移动数据
		   var obj = addRight(leftData,rightData);
    	}
    	
    	if(!obj)return;
    	IS_LOAD=false;
    	table.reload('lefttable', {
            data: obj.leftData
        });
        table.reload('righttable', {
            data: obj.rightData
        });
    }
   
   //向左列表添加数据
    function addLeft(leftData,rightData){
//  	if(!$('#BHostType_Id').val()){
//  		layer.msg("请先选择站点类型", { icon: 5, anim: 6 });
//          return;
//  	}
   	    //获取右列表选择数据
	    if(RIGHT_SELECT_DATA.length==0){
            layer.msg("请选择待选参数！", { icon: 5, anim: 6 });
            return;
        }
        var r_d=[]; 
        leftData.reverse()
        var d1 = [];
        for (var i = 0; i < rightData.length; i++) {
            if(rightData[i].LAY_CHECKED && rightData[i].LAY_CHECKED===true){
                delete rightData[i].LAY_CHECKED
                rightData[i].LAY_TABLE_INDEX && delete rightData[i].LAY_TABLE_INDEX
                 var obj = {
                    'BParaItem_BPara_Id':  rightData[i].BPara_Id,
                    'BParaItem_ParaNo':  rightData[i].BPara_ParaNo,
                    'BParaItem_BPara_CName':  rightData[i].BPara_CName,
                    'BParaItem_ParaValue':  rightData[i].BPara_ParaValue,
                    'BParaItem_BPara_TypeCode':  rightData[i].BPara_TypeCode,
                    'BParaItem_BPara_ParaType':  rightData[i].BPara_ParaType,
                    'BParaItem_BPara_ParaDesc':  rightData[i].BPara_ParaDesc,
                    'BParaItem_BPara_ParaEditInfo':  rightData[i].BPara_ParaEditInfo,
                    'BParaItem_DispOrder':  rightData[i].BPara_DispOrder,
                    'BParaItem_Id':  rightData[i].BParaItem_Id
                };
                leftData.push(obj)
            }else{
                delete rightData[i].LAY_TABLE_INDEX
                r_d.push(rightData[i])
            }
        }
        rightData = r_d;
        RIGHT_DATA = rightData;
        return {leftData:leftData,rightData:rightData};
    }
    
    //向右边列表添加数据
    function addRight(leftData,rightData){
    	
    	if(!$('#BHostType_Id').val()){
    		layer.msg("请先选择站点类型", { icon: 5, anim: 6 });
            return;
    	}
    	//获取右列表选择数据
	    if(LEFT_SELECT_DATA.length==0){
            layer.msg("未选择项目！", { icon: 5, anim: 6 });
            return;
        }
        var l_d=[]; 
        rightData.reverse();
        var d1 = [];
        for (var i = 0; i < leftData.length; i++) {
            if(leftData[i].LAY_CHECKED && leftData[i].LAY_CHECKED===true){
                delete leftData[i].LAY_CHECKED
                leftData[i].LAY_TABLE_INDEX && delete leftData[i].LAY_TABLE_INDEX
                 var obj = {
                    'BPara_Id': leftData[i].BParaItem_BPara_Id,
                    'BPara_ParaNo': leftData[i].BParaItem_ParaNo,
                    'BPara_CName': leftData[i].BParaItem_BPara_CName,
                    'BPara_ParaValue': leftData[i].BParaItem_ParaValue,
                    'BPara_TypeCode' : leftData[i].BParaItem_BPara_TypeCode,
                    'BPara_ParaType': leftData[i].BParaItem_BPara_ParaType,
                    'BPara_ParaDesc': leftData[i].BParaItem_BPara_ParaDesc,
                    'BPara_ParaEditInfo' : leftData[i].BParaItem_BPara_ParaEditInfo,
                    'BPara_DispOrder': leftData[i].BParaItem_DispOrder,
                    'BParaItem_Id' : leftData[i].BParaItem_Id
                };
                rightData.push(obj)
            }else{
                delete leftData[i].LAY_TABLE_INDEX
                l_d.push(leftData[i])
            }
        }
        leftData = l_d;
        RIGHT_DATA = rightData;
        return {leftData:leftData,rightData:rightData};
    }

	//左列表数据加载
	function loadData0(){
		onParaItemList(function(list){
			table_Ind0.reload({data:list});
		});
	}
	 //获取个性列表
	function onParaItemList(callback){
		var me = this;
		var ID = $('#BHostType_Id').val();
	    if(!ID){
    		layer.msg("请先选择站点类型", { icon: 5, anim: 6 });
        	return;
        } 
		var	cols = table_Ind0.config.cols[0],
			fields = [];
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url  = GET_PARAITEM_LIST_URL+'&systemTypeCode=1&paraTypeCode='+DEFAULTPARATYPECODE;
		url+='&where=bparaitem.ObjectID='+ ID+'&fields=' + fields.join(',');
		uxutil.server.ajax({
			async:false,
			url:url
		},function(data){
			var list = data.value.list || [];
			if(list.length>0)list.sort(compare('BParaItem_DispOrder'));
			OLD_LEFT_DATA = list;
			callback(list);
		});
	}
	 //获取默认参数数据
	function onParaList(callback){
		var	cols = table_Ind1.config.cols[0],
			fields = [];
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_DEFAULT_PARA_LIST_URL+'&paraTypeCode='+DEFAULTPARATYPECODE;
	        url+='&fields='+fields.join(',')+'&sort='+table_Ind1.config.defaultOrderBy;
		uxutil.server.ajax({
			async:false,
			url:url
		},function(data){
			var list = data.value.list || [];
			if(list.length>0)list.sort(compare('BPara_DispOrder'));
			callback(list);
		});
	}
	//右列表数据加载
	function loadData1(){
		onParaList(function(list){
			for(var j=0;j<OLD_LEFT_DATA.length;j++){
				for(var i=0;i<list.length;i++){
					if(list[i].BPara_ParaNo == OLD_LEFT_DATA[j].BParaItem_ParaNo){
						list.splice(i,1); //删除下标为i的元素
						break;
					}
				}
			}
			RIGHT_DATA = list;
			table_Ind1.reload({data:list});
		});
	}
	 //保存
	function onSaveClick() {
		if(!$('#BHostType_CName').val()){
			layer.msg('站点类型不能为空！',{icon:5});
			return false;
		}
		var DispOrder =  $('#BHostType_DispOrder').val();
		if(!DispOrder){
			layer.msg('站点类型的显示次序不能为空！',{icon:5});
			return false;
		}else{
			var isnum = myIsNaN(Number(DispOrder));
			if(!isnum){
				layer.msg('显示次序只能是数字！',{icon:5});
			    return false;
			}
		}
		var leftData = table.cache['lefttable'];
        if(leftData.length==0){
			layer.msg('请先选择参数！',{icon:5});
			return false;
		}
		//保存站点类型
        hosttypeSave(function(data){
        	//id
        	if(TYPE=='add')$('#BHostType_Id').val(data.value.id);
        	//保存站点类型参数
        	paramsSave();
        });
	}
	//获取站点类型实体
	function getHosttypeParams(){
		
		var entity ={
			CName:$('#BHostType_CName').val(),
			SName:$('#BHostType_SName').val(),
			HostTypeDesc:$('#BHostType_HostTypeDesc').val(),
			IsUse:1,
			DispOrder:$('#BHostType_DispOrder').val() || 0
		};
		if(TYPE == 'edit'){
			entity.Id = $('#BHostType_Id').val();
			var fields = "Id,CName,SName,DispOrder,HostTypeDesc";
			return {entity:entity,fields:fields};
		}else{
			return {entity:entity};
		}
	}
	//站点类型保存
    function hosttypeSave(callback){
    	var url = TYPE == 'add' ? ADD_INFO_URL : EDIT_INFO_URL;
		var params = getHosttypeParams();
		//显示遮罩层
		var config1 = {
			type: "POST",
			url: url,
			data: JSON.stringify(params)
		};
		uxutil.server.ajax(config1, function(data) {
			//隐藏遮罩层
			if (data.success) {
				callback(data);
			} else {
				layer.msg(data.ErrorInfo,{ icon: 5, anim: 6 });
			}
		});
    }
	//参数实体保存
	function paramsSave(){
		//新增实体
		var entity = getEntity();
		if(entity.length==0)entity = null;
		var params = JSON.stringify({ObjectID:$('#BHostType_Id').val(),entityList:entity});
		//显示遮罩层
		var config = {
			type: "POST",
			url: SAVE_URL,
			data: params
		};
		uxutil.server.ajax(config, function(data) {
			//隐藏遮罩层
			if (data.success) {
				parent.afterUpdate($('#BHostType_Id').val());
				var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
	            parent.layer.close(index); //再执行关闭
			} else {
				layer.msg(data.ErrorInfo,{ icon: 5, anim: 6 });
			}
		});
	}
	function getEntity(){
    	var entity={},addList=[];
    	var leftData = table.cache['lefttable'];
    	//新增
    	for (var i = 0; i < leftData.length; i++) {
         	var obj={
         		ParaNo:leftData[i].BParaItem_ParaNo,
                IsUse:1,
                ParaValue:leftData[i].BParaItem_ParaValue,
                ObjectID:$('#BHostType_Id').val(),
			    ObjectName:$('#BHostType_CName').val()
         	};
            if(leftData[i].BParaItem_BPara_Id){
				obj.BPara={
	        		Id:leftData[i].BParaItem_BPara_Id,
	        		DataTimeStamp:[0,0,0,0,0,0,0,0]
	        	};
			}
            if(leftData[i].BParaItem_Id)obj.Id = leftData[i].BParaItem_Id; 
            if(leftData[i].BParaItem_DispOrder)obj.DispOrder = leftData[i].BParaItem_DispOrder; 
            addList.push(obj);
        }
    	return addList;
    }
	function compare(property){
	    return function(a,b){
	        var value1 = a[property];
	        var value2 = b[property];
	        return value1 - value2;
	    }
	}
	//初始化
	function init(){
		//默认高度加载
		$(".fiexdHeight").css("height", ($(window).height() - 50) + "px");//设置中间容器高度
		$('#BHostTypeCName').text(PARATYPENAME);
        if(PARAMS.TYPE=='edit'){ //还原
            loadDataById(PARATYPECODE,function(data){
            	var obj = changeResult(data);
                form.val('BHostType',obj);
                //左列表数据加载
                IS_LOAD = true;
        	    loadData0();
        	    loadData1();
            });
		}else{
			getMaxNo(function (val) {
	            document.getElementById('BHostType_DispOrder').value = val;
	        });
	        loadData1();
		}
       
	}
	 //获取指定实体字段的最大号
    function getMaxNo(callback) {
        var me = this;
        var result = "";
        uxutil.server.ajax({
            url: GET_MAXNO_URL
        }, function (data) {
            if (data) {
                callback(data.ResultDataValue);
            } else {
                layer.msg(data.msg);
            }
        });
    }
    //通过仪器id 加载数据
	function loadDataById(id,callback){
		var me = this;
		var loadIndex = layer.load();//开启加载层
		uxutil.server.ajax({
			url:GET_INFO_LIST_URL,
			type:'get',
			data:{
				id:id,
				fields:'BHostType_Id,BHostType_CName,BHostType_SName,BHostType_DispOrder,BHostType_HostTypeDesc'
			}
		},function(data){
			layer.close(loadIndex);//关闭加载层
			if(data.success){
				var list = 
				callback(data.value || {} || {});
			}else{
				layer.msg(data.ErrorInfo,{icon:5});
			}
		},true);
	};
	  /**@overwrite 返回数据处理方法*/
	function changeResult(data){
		var me = this;
		data = JSON.stringify(data);
		data = data.replace(/\\u000d\\u000a/g,'').replace(/\\u000a/g,'<br />').replace(/[\r\n]/g,'<br />');
		var list =  JSON.parse(data);
		var reg = new RegExp("<br />", "g");
		list.BHostType_HostTypeDesc = list.BHostType_HostTypeDesc.replace(reg, "\r\n");
		return list;
	}
	// true:数值型的，false：非数值型
	function myIsNaN(value) {
	    return typeof value === 'number' && !isNaN(value);
    }
	/*一维数组对象模糊搜索
	  dataList 为一维数组数据结构
	  value 为input框的输入值
	  type 为指定想要搜索的字段名，array格式 ["name", "number"];
	 */
   function searchText(value){
		var list = RIGHT_DATA;
		for(var i in list){
			var ParaNo = list[i].BPara_ParaNo;
			if(ParaNo){
				var index = ParaNo.lastIndexOf("_");  
                ParaNo  = ParaNo.substring(index + 1, ParaNo.length);
				list[i].ParaNo = ParaNo;
			}
		}
		let filters = ["BPara_CName","ParaNo"];
	    var datalist = list.filter(function(item, index, arr) {
	    for (let j = 0; j < filters.length; j++) {
	      if (item[filters[j]] != undefined || item[filters[j]] != null) {
	        if (item[filters[j]].indexOf(value) >= 0) {
	          return item;
	        }
	      }
	    }
	  });
	  return datalist;
    };
	//初始化
	init();
});